using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace Module.BOM.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private string _tempFolder;
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }

        private BOMEntities CreateContext()
        {
            return new BOMEntities(Library.Helper.CreateEntityConnectionString("DAL.BOMModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMEntities context = CreateContext())
                {
                    var dbItem = context.BOM.Where(o => o.BOMID == id).FirstOrDefault();
                    context.BOM.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return false;
            }

        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.BOMDTO dtoBOM = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.BOMDTO>();
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (BOMEntities context = CreateContext())
                {
                    //delete bom is deleted
                    context.BOMMng_function_DeleteBOMIsDeleted();

                    //get bom
                    BOM dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.BOM.Where(o => o.BOMID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new BOM();
                        context.BOM.Add(dbItem);
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //prepare data to check edit BOM data
                        int? workOrderID = dtoBOM.WorkOrderID;
                        var wo = context.BOMMng_WorkOrder_View.Where(o => o.WorkOrderID == workOrderID).FirstOrDefault();
                        int? workOrderStatusID = wo.WorkOrderStatusID;
                        int? opSequenceID = wo.OPSequenceID;
                        var bomHasArising = context.BOMMng_BOMHasArising_View.Where(o => o.WorkOrderID == wo.WorkOrderID).ToList(); ;

                        //check OPSequenceDetail in bom of every piece is match to OPSequenceDetail of workOrder
                        var opSequenceDetail = context.OPSequenceDetail.Where(o => o.OPSequenceID == opSequenceID).ToList();
                        List<int> opSequenceDetailInBOM = new List<int>();
                        foreach (var pieceItem in dtoBOM.BOMDTOs)
                        {
                            opSequenceDetailInBOM = new List<int>();
                            this.GetOPSequenceInBOM(pieceItem, ref opSequenceDetailInBOM);

                            foreach (var item in opSequenceDetail)
                            {
                                if (!opSequenceDetailInBOM.Contains(item.OPSequenceDetailID))
                                {
                                    //throw new Exception("WorkCenter in BOM must be match with WorkCenter of WorkOrder");
                                }
                            }
                        }

                        //convert dto to db
                        converter.DTO2DB_BOM(dtoBOM, ref dbItem, workOrderStatusID, bomHasArising);

                        //validate Unit
                        this.ValidateUnit(dbItem);

                        //adjust some info tracking
                        this.SetTrackingInfoForBOM(userId, companyID, ref dbItem);

                        //save BOM
                        context.SaveChanges();

                        //get return data
                        dtoItem = GetData(userId, dbItem.BOMID, dbItem.WorkOrderID.Value, out notification).Data;

                        //delete BOM is deleted before create BOMStandard from this BOM
                        context.BOMMng_function_DeleteBOMIsDeleted();

                        //create BOM Standard
                        this.CreateBOMStandardFromBOM(userId, dbItem.BOMID, out notification);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return false;
            }
        }

        public DTO.EditFormData GetData(int userId, int id, int? workOrderID, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMEntities context = CreateContext())
                {
                    //delete bom is deleted
                    context.BOMMng_function_DeleteBOMIsDeleted();

                    //get companyID
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (id > 0)
                    {
                        BOMMng_BOM_View dbItem;
                        //dbItem = context.BOMMng_BOM_View.Where(o =>o.CompanyID==companyID).FirstOrDefault(o => o.BOMID == id);
                        dbItem = context.BOMMng_BOM_View.FirstOrDefault(o => o.BOMID == id);
                        editFormData.Data = converter.DB2DTO_BOM(dbItem);
                    }
                    else
                    {
                        var workOrder = context.BOMMng_WorkOrder_View.Where(o => o.WorkOrderID == workOrderID).FirstOrDefault();
                        editFormData.Data = new DTO.BOMDTO();
                        editFormData.Data.BOMDTOs = new List<DTO.BOMDTO>();

                        //init workorder info
                        editFormData.Data.WorkOrderID = workOrder.WorkOrderID;
                        editFormData.Data.WorkOrderUD = workOrder.WorkOrderUD;
                        editFormData.Data.WorkOrderDescription = workOrder.WorkOrderDescription;
                        editFormData.Data.WorkOrderQnt = workOrder.WorkOrderQnt;
                        editFormData.Data.OPSequenceID = workOrder.OPSequenceID;
                        editFormData.Data.OPSequenceNM = workOrder.OPSequenceNM;
                        editFormData.Data.ClientUD = workOrder.ClientUD;
                        editFormData.Data.ModelID = workOrder.ModelID;
                        editFormData.Data.ModelUD = workOrder.ModelUD;
                        editFormData.Data.ModelNM = workOrder.ModelNM;
                        editFormData.Data.WorkOrderStatusID = workOrder.WorkOrderStatusID;
                        editFormData.Data.WorkOrderTypeNM = workOrder.WorkOrderTypeNM;
                        editFormData.Data.WorkOrderStatusNM = workOrder.WorkOrderStatusNM;
                        if (workOrder.StartDate.HasValue) editFormData.Data.StartDate = workOrder.StartDate.Value.ToString("dd/MM/yyyy");
                        if (workOrder.FinishDate.HasValue) editFormData.Data.FinishDate = workOrder.FinishDate.Value.ToString("dd/MM/yyyy");
                        editFormData.Data.ProductArticleCode = workOrder.ProductArticleCode;
                        editFormData.Data.ProductDescription = workOrder.ProductDescription;
                        editFormData.Data.WorkOrderWastagePercent = workOrder.WastagePercent;
                        editFormData.Data.ProductID = workOrder.ProductID;
                    }
                    //get support list
                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    //editFormData.OPSequenceDetails = support_factory.GetOPSequenceDetail(editFormData.Data.OPSequenceID);
                    editFormData.ProductionItemTypes = support_factory.GetProductionItemType();
                    editFormData.ProductionTeams = support_factory.GetProductionTeam(companyID);

                    editFormData.OPSequenceDetails = AutoMapper.Mapper.Map<List<BOMMng_SupportWorkOrderOPSequence_View>, List<DTO.SupportWorkOrderOPSequenceDTO>>(context.BOMMng_SupportWorkOrderOPSequence_View.Where(o => o.WorkOrderID == editFormData.Data.WorkOrderID).ToList());

                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return editFormData;
            }
        }

        public DTO.ProductionItemDTO GetProductionItem(int userId, int id, out Library.DTO.Notification notification)
        {
            DTO.ProductionItemDTO productionItem = new DTO.ProductionItemDTO();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMEntities context = CreateContext())
                {
                    //get companyID
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (id > 0)
                    {
                        BOMMng_ProductionItem_View dbItem;
                        dbItem = context.BOMMng_ProductionItem_View.FirstOrDefault(o => o.ProductionItemID == id);
                        productionItem = converter.DB2DTO_ProductionItem(dbItem);
                    }
                    else
                    {
                        productionItem = new DTO.ProductionItemDTO();
                    }
                    return productionItem;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return productionItem;
            }
        }

        public DTO.ProductionItemDTO UpdateProductionItem(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProductionItemDTO dtoProductionItem = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ProductionItemDTO>();
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (BOMEntities context = CreateContext())
                {
                    ProductionItem dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ProductionItem();
                        context.ProductionItem.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductionItem.Where(o => o.ProductionItemID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return new DTO.ProductionItemDTO();
                    }
                    else
                    {
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        if (dtoProductionItem.ProductImage_HasChange.HasValue && dtoProductionItem.ProductImage_HasChange.Value)
                        {
                            dtoProductionItem.ProductImage = fwFactory.CreateFilePointer(_tempFolder, dtoProductionItem.ProductImage_NewFile, dtoProductionItem.ProductImage);
                        }
                        //convert dto to db
                        converter.DTO2DB_ProductionItem(dtoProductionItem, ref dbItem);
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.CompanyID = companyID;
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoProductionItem = GetProductionItem(userId, dbItem.ProductionItemID, out notification);
                        return dtoProductionItem;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return new DTO.ProductionItemDTO();
            }
        }

        //public int CreateBOMFromBOMStandard(int userId, int workOrderID, int bomStandardID, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
        //    try
        //    {
        //        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        //        int? companyID = fw_factory.GetCompanyID(userId);
        //        using (BOMEntities context = CreateContext())
        //        {
        //            BOM dbBOM = new BOM();
        //            BOMStandard dbBOMStandard = context.BOMStandard.Where(o => o.BOMStandardID == bomStandardID).FirstOrDefault();
        //            if (dbBOMStandard == null) {
        //                throw new Exception("Can not find BOM Standard of this model");
        //            }
        //            converter.DB2DB_BOM(dbBOMStandard, ref dbBOM);
        //            this.SetWorkOrderForBOM(workOrderID, ref dbBOM);
        //            this.SetTrackingInfoForBOM(userId, companyID, ref dbBOM);
        //            context.BOM.Add(dbBOM);
        //            context.SaveChanges();
        //            return dbBOM.BOMID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;
        //        notification.DetailMessage.Add(ex.Message);
        //        if (ex.GetBaseException() != null)
        //        {
        //            notification.DetailMessage.Add(ex.GetBaseException().Message);
        //        }
        //        return -1;
        //    }

        //}

        private bool CreateBOMStandardFromBOM(int userId, int bomID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (BOMEntities context = CreateContext())
                {
                    //delete BOMStandard is deleted before create new BOMStandard
                    context.BOMMng_function_DeleteBOMStandardIsDeleted();
                    //get product and opSequence in workOrder of BOM
                    var workOrder = context.BOM.Where(o => o.BOMID == bomID).FirstOrDefault().WorkOrder;
                    int? productID = workOrder.ProductID;
                    int? opSequenceID = workOrder.OPSequenceID;

                    if (workOrder.WorkOrderTypeID == 4) //Pre-Order. Do not need create bom standard incase type is Pre-Order
                    {
                        return true;
                    }

                    //check this product is create BOM Standard ?
                    ProductionProcess dbProductionProcess = context.ProductionProcess.Where(o => o.ProductID == productID).FirstOrDefault();
                    if (dbProductionProcess == null)
                    {
                        //create production process to hold BOMStandard
                        dbProductionProcess = new ProductionProcess();
                        context.ProductionProcess.Add(dbProductionProcess);
                    }
                    else
                    {
                        //set all BOMStandard of this production process to isDeleted (*)
                        int productionProcessID = dbProductionProcess.ProductionProcessID;
                        var bomStandard = context.BOMStandard.Where(o => o.ProductionProcessID == productionProcessID).ToList();
                        bomStandard.ForEach(o => { o.IsDeleted = true; o.DeletedBy = userId; o.DeletedDate = DateTime.Now; o.Description = "auto delete to create new when update BOM"; });
                    }
                    //assign production process info from workorder info of BOM
                    dbProductionProcess.ProductID = productID;
                    dbProductionProcess.OPSequenceID = opSequenceID;
                    dbProductionProcess.CompanyID = companyID;
                    dbProductionProcess.CreatedBy = userId;
                    dbProductionProcess.CreatedDate = DateTime.Now;
                    dbProductionProcess.UpdatedBy = userId;
                    dbProductionProcess.UpdatedDate = DateTime.Now;

                    //copy data from BOM to BOM Standard
                    BOMStandard dbBOMStandard = new BOMStandard();
                    BOM dbBOM = context.BOM.Where(o => o.BOMID == bomID).FirstOrDefault();
                    if (dbBOM == null)
                    {
                        throw new Exception("Can not find BOM to create BOM Standard");
                    }
                    converter.DB2DB_BOMStandard(dbBOM, ref dbBOMStandard);
                    this.SetTrackingInfoForBOMStandard(userId, companyID, ref dbBOMStandard);
                    this.SetProductionProcessForBOMStandard(dbProductionProcess.ProductionProcessID, ref dbBOMStandard);

                    //mock BOMStandard to Production Process
                    dbProductionProcess.BOMStandard.Add(dbBOMStandard);

                    //save data
                    context.SaveChanges();

                    //delete BOMStandard is deleted that we set at step (*) after create new BOMStandard success
                    context.BOMMng_function_DeleteBOMStandardIsDeleted();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return false;
            }
        }

        private void SetWorkOrderForBOM(int? workOrderID, ref BOM dbBOM)
        {
            dbBOM.WorkOrderID = workOrderID;
            foreach (var item in dbBOM.BOM1)
            {
                BOM x = item;
                SetWorkOrderForBOM(workOrderID, ref x);
            }
        }

        private void SetTrackingInfoForBOMStandard(int? userID, int? companyID, ref BOMStandard dbBOMStandard)
        {
            dbBOMStandard.UpdatedBy = userID;
            dbBOMStandard.UpdatedDate = DateTime.Now;

            dbBOMStandard.CompanyID = companyID;
            if (dbBOMStandard.IsDeleted.HasValue && dbBOMStandard.IsDeleted.Value)
            {
                dbBOMStandard.DeletedBy = userID;
                dbBOMStandard.DeletedDate = DateTime.Now;
            }
            foreach (var item in dbBOMStandard.BOMStandard1)
            {
                BOMStandard x = item;
                SetTrackingInfoForBOMStandard(userID, companyID, ref x);
            }
        }

        private void SetProductionProcessForBOMStandard(int? productionProcessID, ref BOMStandard dbBOMStandard)
        {
            dbBOMStandard.ProductionProcessID = productionProcessID;
            foreach (var item in dbBOMStandard.BOMStandard1)
            {
                BOMStandard x = item;
                SetProductionProcessForBOMStandard(productionProcessID, ref x);
            }
        }

        private void SetTrackingInfoForBOM(int? userID, int? companyID, ref BOM dbBOM)
        {
            dbBOM.UpdatedBy = userID;
            dbBOM.UpdatedDate = DateTime.Now;
            dbBOM.CompanyID = companyID;
            if (dbBOM.IsDeleted.HasValue && dbBOM.IsDeleted.Value)
            {
                dbBOM.DeletedBy = userID;
                dbBOM.DeletedDate = DateTime.Now;
            }
            foreach (var item in dbBOM.BOM1)
            {
                BOM x = item;
                SetTrackingInfoForBOM(userID, companyID, ref x);
            }
        }

        private void ValidateUnit(BOM dbBOM)
        {
            if (dbBOM.ParentBOMID.HasValue && !dbBOM.UnitID.HasValue)
            {
                throw new Exception("There are some item that does not have unit (red cell). You need edit and fill-in unit before save BOM");
            }
            foreach (var item in dbBOM.BOM1)
            {
                ValidateUnit(item);
            }
        }

        public List<DTO.ProductionItemDTO> GetListProductionItem(int userId, object dtoBOMProduct, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.BOMImportDTO> bomImportData = ((Newtonsoft.Json.Linq.JArray)dtoBOMProduct).ToObject<List<DTO.BOMImportDTO>>();

            //get articleCode of product on workOrder
            string articleCode = bomImportData.FirstOrDefault().Code;

            //only get excel item that have Sequence
            bomImportData = bomImportData.Where(o => !"Product".Equals(o.Type)/*!string.IsNullOrEmpty(o.Sequence) && !o.Type.Equals("Product")*/).ToList();
            try
            {
                //auto create products for pieces that are declared on model
                using (BOMEntities context = CreateContext())
                {
                    var workOrderProduct = context.Product.Select(o => new { o.ProductID, o.ArticleCode }).Where(s => s.ArticleCode == articleCode).FirstOrDefault();
                    if (workOrderProduct == null)
                    {
                        throw new Exception("Could not find product of workorder in system");
                    }
                    else
                    {
                        context.ProductMng_function_CreateProductPiece(workOrderProduct.ProductID);
                    }
                }

                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (BOMEntities context = CreateContext())
                {
                    ProductionItem dbProductionItem;
                    ProductionItemWarehouse dbProductionItemWarehouse;
                    var dtoProduct = bomImportData.Select(o => new { o.Code }).Distinct();
                    foreach (var item in dtoProduct)
                    {
                        var p = context.ProductionItem.Where(o => o.ProductionItemUD == item.Code).Count();
                        if (p == 0)
                        {
                            if (item.Code.Length > 100)
                            {
                                throw new Exception("Can not create code for component: " + item.Code + ". Item code must be less than 100 character");
                            }

                            //get bom item base on production item code
                            DTO.BOMImportDTO dtoBOMItem = bomImportData.Where(o => o.Code == item.Code).FirstOrDefault();

                            //create production item
                            dbProductionItem = new ProductionItem();
                            dbProductionItem.ProductionItemUD = item.Code;
                            dbProductionItem.CompanyID = companyID;
                            dbProductionItem.UpdatedBy = userId;
                            dbProductionItem.UpdatedDate = DateTime.Now;
                            dbProductionItem.ProductionItemNM = dtoBOMItem.Description;
                            dbProductionItem.UnitID = 25;

                            //get type of product
                            if (dtoBOMItem.Type == "Material")
                            {
                                //dbProductionItem.ProductionItemTypeID = 2; //material
                                throw new Exception("Can not find material: " + item.Code + " (" + dtoBOMItem.Description + ") in system. You need to create this material before import");
                            }
                            else if (dtoBOMItem.Type == "Component")
                            {
                                //find production item type
                                if (dtoBOMItem.IsEndOfSequence.HasValue && dtoBOMItem.IsEndOfSequence.Value)
                                {
                                    dbProductionItem.ProductionItemTypeID = 3; //piece

                                    //assing productID for productionItem
                                    var pieceProduct = context.Product.Select(o => new { o.ProductID, o.ArticleCode }).Where(s => s.ArticleCode == item.Code).FirstOrDefault();
                                    dbProductionItem.ProductID = pieceProduct.ProductID;
                                }
                                else
                                {
                                    dbProductionItem.ProductionItemTypeID = 1; //component
                                }

                                //create factory warehouse default for this component
                                // get list default factory warehouse with workcenter.
                                var dbItem = context.BOMMng_WorkCenterDetailDefaultFactoryWarehouse_View.Where(o => o.WorkCenterUD == dtoBOMItem.Sequence).ToList();
                                foreach (var sItem in dbItem)
                                {
                                    dbProductionItemWarehouse = new ProductionItemWarehouse();
                                    dbProductionItemWarehouse.FactoryWarehouseID = sItem.FactoryWarehouseID;
                                    dbProductionItemWarehouse.IsDefault = true;
                                    dbProductionItemWarehouse.BranchID = sItem.BranchID;
                                    dbProductionItem.ProductionItemWarehouse.Add(dbProductionItemWarehouse);
                                }

                                //if (dtoBOMItem.DefaultFactoryWarehouseID.HasValue)
                                //{
                                //    dbProductionItemWarehouse = new ProductionItemWarehouse();
                                //    dbProductionItemWarehouse.FactoryWarehouseID = dtoBOMItem.DefaultFactoryWarehouseID;
                                //    dbProductionItemWarehouse.IsDefault = true;
                                //    dbProductionItem.ProductionItemWarehouse.Add(dbProductionItemWarehouse);
                                //}                                
                            }
                            context.ProductionItem.Add(dbProductionItem);
                        }
                    }
                    context.SaveChanges();
                    //get list production item
                    List<string> codes = bomImportData.Select(o => o.Code).ToList();
                    var dbItems = context.BOMMng_ProductionItem_View.Where(o => codes.Contains(o.ProductionItemUD)).ToList();
                    return AutoMapper.Mapper.Map<List<BOMMng_ProductionItem_View>, List<DTO.ProductionItemDTO>>(dbItems);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return new List<DTO.ProductionItemDTO>();
            }
        }

        /// <summary>
        /// create BOM excel template so user can easily fill-in material for every component and import back to database
        /// base on product of workOrder system will find all piece of this product and generate template
        /// </summary>
        /// <param name="workOrderID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public string CreateImportTemplate(int workOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ImportExcelTemplateDataObject ds = new ImportExcelTemplateDataObject();

            try
            {
                //auto create products for pieces that are declared on model
                using (BOMEntities context = CreateContext())
                {
                    int? productID = context.BOMMng_WorkOrder_View.Select(o => new { o.WorkOrderID, o.ProductID }).Where(s => s.WorkOrderID == workOrderID).FirstOrDefault().ProductID;
                    context.ProductMng_function_CreateProductPiece(productID);
                }

                //create template
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("BOMMng_function_CreateImportTemplate", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@WorkOrderID", workOrderID);

                adap.TableMappings.Add("Table", "WorkOrder");
                adap.TableMappings.Add("Table1", "Piece");
                adap.TableMappings.Add("Table2", "WorkCenter");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "BOM_ExcelImportTemplate");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return string.Empty;
            }
        }

        public string CreateImportTemplateGeneral(int workOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ImportExcelTemplateDataObject ds = new ImportExcelTemplateDataObject();

            try
            {
                //auto create products for pieces that are declared on model
                using (BOMEntities context = CreateContext())
                {
                    int? productID = context.BOMMng_WorkOrder_View.Select(o => new { o.WorkOrderID, o.ProductID }).Where(s => s.WorkOrderID == workOrderID).FirstOrDefault().ProductID;
                    context.ProductMng_function_CreateProductPiece(productID);
                }

                //create template
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("BOMMng_function_CreateImportTemplate", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@WorkOrderID", workOrderID);

                adap.TableMappings.Add("Table", "WorkOrder");
                adap.TableMappings.Add("Table1", "Piece");
                adap.TableMappings.Add("Table2", "WorkCenter");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "BOM_ExcelImportTemplateGeneral");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// create data to make BOM template base on OP Sequence of workOrder and product of workOrder
        /// base on product of workOrder system will find all piece of this product and generate template
        /// </summary>
        /// <param name="workOrderID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.ImportTemplate.ImportTemplateData CreateBOMTemplateData(int userId, int workOrderID, int? copyFromProductionProcessID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ImportExcelTemplateDataObject ds = new ImportExcelTemplateDataObject();

            DTO.ImportTemplate.ImportTemplateData data = new DTO.ImportTemplate.ImportTemplateData();
            data.WorkOrder = new DTO.ImportTemplate.WorkOrderDTO();
            data.Pieces = new List<DTO.ImportTemplate.PieceDTO>();
            data.WorkCenters = new List<DTO.ImportTemplate.WorkCenterDTO>();
            data.ProductionItems = new List<DTO.ProductionItemDTO>();
            try
            {
                //auto create products for pieces that are declared on model
                using (BOMEntities context = CreateContext())
                {
                    int? productID = context.BOMMng_WorkOrder_View.Select(o => new { o.WorkOrderID, o.ProductID }).Where(s => s.WorkOrderID == workOrderID).FirstOrDefault().ProductID;
                    context.ProductMng_function_CreateProductPiece(productID);
                }

                //read db to dataset
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("BOMMng_function_CreateImportTemplate", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@WorkOrderID", workOrderID);

                adap.TableMappings.Add("Table", "WorkOrder");
                adap.TableMappings.Add("Table1", "Piece");
                adap.TableMappings.Add("Table2", "WorkCenter");
                adap.Fill(ds);

                //read from dataset to dto workOrder
                var wo = ds.WorkOrder.FirstOrDefault();
                data.WorkOrder.WorkOrderID = wo.WorkOrderID;
                data.WorkOrder.ProductID = wo.ProductID;
                data.WorkOrder.OPSequenceID = wo.OPSequenceID;
                data.WorkOrder.ModelID = wo.ModelID;
                data.WorkOrder.ArticleCode = wo.ArticleCode;
                data.WorkOrder.Description = wo.Description;
                data.WorkOrder.ModelUD = wo.ModelUD;
                data.WorkOrder.ModelNM = wo.ModelNM;
                data.WorkOrder.WorkOrderUD = wo.WorkOrderUD;

                //read from dataset to dto piece
                DTO.ImportTemplate.PieceDTO piece;
                foreach (var item in ds.Piece)
                {
                    piece = new DTO.ImportTemplate.PieceDTO();
                    data.Pieces.Add(piece);
                    piece.ModelID = item.ModelID;
                    piece.PieceID = item.PieceID;
                    piece.PieceUD = item.PieceUD;
                    piece.PieceNM = item.PieceNM;
                    piece.Quantity = item.Quantity;
                    piece.PieceArticleCode = item.PieceArticleCode;
                    piece.PieceDescription = item.PieceDescription;
                    piece.PieceProductID = item.PieceProductID;
                    piece.FrameMaterialNM = item.FrameMaterialNM;
                    piece.FrameMaterialColorNM = item.FrameMaterialColorNM;
                    piece.MaterialNM = item.MaterialNM;
                    piece.MaterialTypeNM = item.MaterialTypeNM;
                    piece.MaterialColorNM = item.MaterialColorNM;
                }

                //read from dataset to dto workCenter
                DTO.ImportTemplate.WorkCenterDTO workCenter;
                foreach (var item in ds.WorkCenter)
                {
                    workCenter = new DTO.ImportTemplate.WorkCenterDTO();
                    data.WorkCenters.Add(workCenter);
                    workCenter.OPSequenceDetailID = item.OPSequenceDetailID;
                    workCenter.OPSequenceID = item.OPSequenceID;
                    workCenter.WorkCenterNM = item.WorkCenterNM;
                    workCenter.SequenceIndexNumber = item.SequenceIndexNumber;
                }

                /*
                 * create production item base on workCenter & piece of product (product of workOrder)
                 */

                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);

                //prepare info
                string workOrderUD = data.WorkOrder.WorkOrderUD;
                string productionItemUD = "";
                string productionItemNM = "";
                int? productionItemTypeID = null;
                int? pieceProductID = null;

                using (var context = CreateContext())
                {
                    data.WorkCenters = new List<DTO.ImportTemplate.WorkCenterDTO>();

                    var dbWorkOrderOPSequences = context.BOMMng_WorkOrderOPSequence_View.Where(o => o.WorkOrderID == workOrderID);
                    foreach (var dbWorkOrderOPSequence in dbWorkOrderOPSequences.ToList())
                    {
                        DTO.ImportTemplate.WorkCenterDTO dtoWorkCenter = new DTO.ImportTemplate.WorkCenterDTO();
                        data.WorkCenters.Add(dtoWorkCenter);

                        dtoWorkCenter.OPSequenceDetailID = dbWorkOrderOPSequence.OPSequenceDetailID;
                        dtoWorkCenter.OPSequenceID = dbWorkOrderOPSequence.OPSequenceID;
                        dtoWorkCenter.WorkCenterNM = dbWorkOrderOPSequence.WorkCenterNM;
                        dtoWorkCenter.SequenceIndexNumber = dbWorkOrderOPSequence.SequenceIndexNumber;

                    }
                }

                //get max sequence index in OP Sequence so we can know which workcenter is final in sequence
                int? maxIndex = data.WorkCenters.Max(o => o.SequenceIndexNumber);

                //db to save data
                ProductionItem dbProductionItem;
                ProductionItemWarehouse dbProductionItemWarehouse;

                //declare list product code 
                List<string> productionItemCodes = new List<string>();

                using (BOMEntities context = CreateContext())
                {
                    foreach (var pi in data.Pieces)
                    {
                        // add piece
                        productionItemUD = pi.PieceArticleCode;
                        productionItemNM = pi.PieceDescription;
                        productionItemTypeID = 3; //Piece
                        pieceProductID = pi.PieceProductID;

                        var p = context.ProductionItem.Where(o => o.ProductionItemUD == productionItemUD).Count();

                        if (p == 0)
                        {
                            if (productionItemUD.Length > 100)
                            {
                                throw new Exception("Can not create code for component: " + productionItemUD + ". Item code must be less than 100 character");
                            }

                            dbProductionItem = new ProductionItem();
                            dbProductionItem.ProductionItemUD = productionItemUD;
                            dbProductionItem.CompanyID = companyID;
                            dbProductionItem.UpdatedBy = userId;
                            dbProductionItem.UpdatedDate = DateTime.Now;
                            dbProductionItem.ProductionItemNM = productionItemNM;
                            dbProductionItem.UnitID = 25;
                            dbProductionItem.ProductionItemTypeID = productionItemTypeID;
                            dbProductionItem.ProductID = pieceProductID;

                            //add to context
                            context.ProductionItem.Add(dbProductionItem);
                        }

                        //add to list product code
                        productionItemCodes.Add(productionItemUD);

                        foreach (var wc in data.WorkCenters.OrderByDescending(o => o.SequenceIndexNumber))
                        {
                            //if (wc.WorkCenterNM == "PACKING")
                            //{
                            //    productionItemCodes.Add(productionItemUD);
                            //}

                            //if (wc.WorkCenterNM != "PACKING")
                            //{
                            productionItemUD = GetProductionItemUD(pi.PieceArticleCode, wc.WorkCenterNM);/*workOrderUD + "-" + pi.PieceArticleCode + "-" + wc.WorkCenterNM;*/
                            productionItemNM = GetProductionItemNM(wc.WorkCenterNM, pi.PieceNM, pi.FrameMaterialNM, pi.FrameMaterialColorNM, pi.MaterialNM, pi.MaterialTypeNM, pi.MaterialColorNM, pi.PieceDescription);
                            productionItemTypeID = 1; //Component
                            pieceProductID = null;

                            bool isExist = IsExistProductionItemCode(productionItemUD, productionItemCodes);
                            if (isExist)
                            {
                                continue;
                            }

                            var c = context.ProductionItem.Where(o => o.ProductionItemUD == productionItemUD).Count();
                            if (c == 0)
                            {
                                if (productionItemUD.Length > 100)
                                {
                                    throw new Exception("Can not create code for component: " + productionItemUD + ". Item code must be less than 100 character");
                                }

                                dbProductionItem = new ProductionItem();
                                dbProductionItem.ProductionItemUD = productionItemUD;
                                dbProductionItem.CompanyID = companyID;
                                dbProductionItem.UpdatedBy = userId;
                                dbProductionItem.UpdatedDate = DateTime.Now;
                                dbProductionItem.ProductionItemNM = productionItemNM;
                                dbProductionItem.UnitID = 25;
                                dbProductionItem.ProductionItemTypeID = productionItemTypeID;
                                dbProductionItem.ProductID = pieceProductID;

                                //create factory warehouse default for this component
                                foreach (var workCenterDetail in context.BOMMng_WorkCenterDetailDefaultFactoryWarehouse_View.Where(o => o.WorkCenterNM.Equals(wc.WorkCenterNM)).ToList())
                                {
                                    dbProductionItemWarehouse = new ProductionItemWarehouse();
                                    dbProductionItemWarehouse.FactoryWarehouseID = workCenterDetail.FactoryWarehouseID;
                                    dbProductionItemWarehouse.IsDefault = true;
                                    dbProductionItem.ProductionItemWarehouse.Add(dbProductionItemWarehouse);
                                }

                                //add to context
                                context.ProductionItem.Add(dbProductionItem);
                            }
                            else
                            {
                                var dbItem = context.ProductionItem.FirstOrDefault(o => o.ProductionItemUD == productionItemUD);
                                dbItem.ProductionItemUD = productionItemUD;
                                dbItem.ProductionItemNM = productionItemNM;
                            }

                            //add to list product code
                            productionItemCodes.Add(productionItemUD);
                        }
                    }

                    context.SaveChanges();

                    //get list production item
                    var productionItems = context.BOMMng_ProductionItem_View.Where(o => productionItemCodes.Contains(o.ProductionItemUD)).ToList();
                    data.ProductionItems = AutoMapper.Mapper.Map<List<BOMMng_ProductionItem_View>, List<DTO.ProductionItemDTO>>(productionItems);

                    //get list BOM Standard base on ProductionProcessID
                    var bomStandardSource = context.BOMMng_CreateImportTemplate_BOMStandard_View.Where(o => o.ProductionProcessID == copyFromProductionProcessID).ToList();
                    data.BOMStandardSources = AutoMapper.Mapper.Map<List<BOMMng_CreateImportTemplate_BOMStandard_View>, List<DTO.ImportTemplate.BOMStandardSourceDTO>>(bomStandardSource);

                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return new DTO.ImportTemplate.ImportTemplateData();
            }
        }

        /// <summary>
        ///     - allow set default workorder on model, 
        ///     - many workorders is same model so we need one workorder default
        ///     - workorder default that mean BOM is default, so we can use it to make another BOM
        /// </summary>
        /// <param name="workOrderID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool SetDefaultWorkOrder(int workOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMEntities context = CreateContext())
                {
                    //set default BOM for this workOrder
                    var workOrder = context.WorkOrder.Where(o => o.WorkOrderID == workOrderID).FirstOrDefault();
                    workOrder.IsDefaultOfModel = true;

                    //get workOrders that have same model with this workorder and set default is false
                    int? modelID = workOrder.ModelID;
                    var workOrders = context.WorkOrder.Where(o => o.ModelID == modelID && o.WorkOrderID != workOrderID).ToList();
                    workOrders.ForEach(o => { o.IsDefaultOfModel = false; });

                    //save change
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return false;
            }
        }

        /// <summary>
        ///     -  get list production process
        ///     -  every production process have one BOM Standard
        ///     - That mean show all BOM Standard so user can use it to copy and make new BOM to apply to workOrder
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.WorkOrderAndProductionProcessForm.ProductionProcessFormData GetProductionProcessFormData(int? modelID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.WorkOrderAndProductionProcessForm.ProductionProcessFormData data = new DTO.WorkOrderAndProductionProcessForm.ProductionProcessFormData();
            try
            {
                using (BOMEntities context = CreateContext())
                {
                    //get production process that contain BOM Standard
                    var dbProductionProcess = context.BOMMng_ProductionProcess_View.Where(o => o.ModelID == modelID).ToList();
                    data.ProductionProcesses = AutoMapper.Mapper.Map<List<BOMMng_ProductionProcess_View>, List<DTO.WorkOrderAndProductionProcessForm.ProductionProcessDTO>>(dbProductionProcess);

                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return data;
            }
        }

        public DTO.EditFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.DatePrices = new List<DTO.DatePrices>();

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    data.DatePrices = converter.DB2DTO_Date(context.BOMMng_DateOfPrice_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string ExportBOMToExcel(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //set price
                int year = Convert.ToInt32(param["year"]);
                int month = Convert.ToInt32(param["month"]);
                using (var context = CreateContext())
                {
                    //delete bom is deleted
                    context.BOMMng_function_DeleteBOMIsDeleted();

                    BOMMng_ExportToExcel_View dbItem;
                    dbItem = context.BOMMng_ExportToExcel_View.Where(o => o.BOMID == id).FirstOrDefault();

                    //get companyID
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    List<DTO.PriceList> listPrices = new List<DTO.PriceList>();
                    var priceList = context.BOMMng_ProductionPrice_View.Where(o => o.Month == month && o.Year == year && o.CompanyID == companyID);

                    listPrices = converter.DB2DTO_PriceList(priceList.ToList());

                    //fill-in data to data set 
                    ExportBOMObject dsResult = new ExportBOMObject();
                    this.ParseBOMList(listPrices, dbItem, ref dsResult);
                    return Library.Helper.CreateReportFileWithEPPlus2(dsResult, "BOM_ExcelExport");
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return string.Empty;
            }
        }

        private void ParseBOMList(List<DTO.PriceList> listPrices, BOMMng_ExportToExcel_View bomData, ref ExportBOMObject dsResult)
        {

            if (bomData != null && bomData.BOMMng_ExportToExcel_View1 != null)
            {
                //Add Price
                int? productionItemID = bomData.ProductionItemID;
                int? productionItemTypeID = bomData.ProductionItemTypeID;
                var priceItem = listPrices.Where(o => o.ProductionItemTypeID == productionItemTypeID && o.ProductionItemID == productionItemID).FirstOrDefault();
                if (priceItem != null && priceItem.Price != null)
                {
                    bomData.Price = priceItem.Price;
                    bomData.PricePerItem = priceItem.Price * bomData.Quantity;
                }

                //push to list result
                ExportBOMObject.BOMSRow dr;
                dr = dsResult.BOMS.NewBOMSRow();
                if (bomData.PieceIndex.HasValue) dr.PieceIndex = bomData.PieceIndex.Value;
                if (!string.IsNullOrEmpty(bomData.ProductionItemTypeNM)) dr.ProductionItemTypeNM = bomData.ProductionItemTypeNM;
                if (!string.IsNullOrEmpty(bomData.WorkCenterNM)) dr.WorkCenterNM = bomData.WorkCenterNM;
                if (!string.IsNullOrEmpty(bomData.ProductionItemUD)) dr.ProductionItemUD = bomData.ProductionItemUD;
                if (!string.IsNullOrEmpty(bomData.ProductionItemNM)) dr.ProductionItemNM = bomData.ProductionItemNM;
                if (bomData.Quantity.HasValue) dr.Quantity = bomData.Quantity.Value;
                if (!string.IsNullOrEmpty(bomData.UnitNM)) dr.UnitNM = bomData.UnitNM;
                if (!string.IsNullOrEmpty(bomData.ProductArticleCode)) dr.ProductArticleCode = bomData.ProductArticleCode;
                if (!string.IsNullOrEmpty(bomData.ProductDescription)) dr.ProductDescription = bomData.ProductDescription;
                if (bomData.WorkOrderQnt.HasValue) dr.WorkOrderQnt = bomData.WorkOrderQnt.Value;
                if (bomData.TotalNorm.HasValue) dr.TotalNorm = bomData.TotalNorm.Value;
                if (bomData.Price.HasValue) dr.Price = bomData.Price.Value;
                if (bomData.PricePerItem.HasValue) dr.PricePerItem = bomData.PricePerItem.Value;
                dsResult.BOMS.AddBOMSRow(dr);

                //it just only assign piece index for child of root node
                if (bomData.ParentBOMID == null)
                {
                    int i = 1;
                    foreach (var item in bomData.BOMMng_ExportToExcel_View1.ToList())
                    {
                        item.PieceIndex = i;
                        i++;
                    }
                }

                foreach (var item in bomData.BOMMng_ExportToExcel_View1.OrderBy(o => o.BOMMng_ExportToExcel_View1.Count))
                {
                    if (!item.PieceIndex.HasValue)
                    {
                        item.PieceIndex = bomData.PieceIndex;
                    }
                    ParseBOMList(listPrices, item, ref dsResult);
                }
            }
        }

        private void GetOPSequenceInBOM(DTO.BOMDTO data, ref List<int> opSequenceDetails)
        {
            if (data.OPSequenceDetailID.HasValue && (!data.IsDeleted.HasValue || !data.IsDeleted.Value)) opSequenceDetails.Add(data.OPSequenceDetailID.Value);
            foreach (var item in data.BOMDTOs)
            {
                GetOPSequenceInBOM(item, ref opSequenceDetails);
            }
        }

        //public List<BOMMng_WorkOrderOPSequence_View> GetOPSequenceDetailByPreOrderWorkOrder(int? preOrderWorkOrderID, BOMEntities context)
        //{
        //    var dbWorkOrderOPSequences = context.BOMMng_WorkOrderOPSequence_View.Where(o => o.WorkOrderID == preOrderWorkOrderID);
        //    return dbWorkOrderOPSequences.ToList();
        //}

        public string GetProductionItemUD(string articleCode, string workCenterNM)
        {
            if (workCenterNM == "FRAME")
            {
                return articleCode.Substring(0, 6) + "-FRAME";
            }

            if (workCenterNM == "PAINTED")
            {
                return articleCode.Substring(0, 8) + "-PAINTED";
            }

            if (workCenterNM == "WEAVING")
            {
                return articleCode.Substring(0, 15) + "-WEAVING";
            }

            if (workCenterNM == "FINISHING")
            {
                return articleCode.Substring(0, 22) + "-FINISHING";
            }

            if (workCenterNM == "ASSEMBLY")
            {
                return articleCode.Substring(0, 22) + "-ASSEMBLY";
            }

            if (workCenterNM == "PACKING")
            {
                return articleCode;
            }

            return string.Empty;
        }

        public string GetProductionItemNM(string workCenterNM, string modelNM, string frameMaterialNM, string frameMaterialColorNM, string materialNM, string materialTypeNM, string materialColorNM, string pieceDescription)
        {
            string description = modelNM;

            if (workCenterNM == "FRAME")
            {
                return description = description + " / " + frameMaterialNM;
            }

            if (workCenterNM == "PAINTED")
            {
                return description = description + " / " + frameMaterialNM + " " + frameMaterialColorNM;
            }

            if (workCenterNM == "WEAVING")
            {
                return description = description + " / " + frameMaterialNM + " " + frameMaterialColorNM + " / " + materialNM + " " + materialTypeNM + " " + materialColorNM;
            }

            if (workCenterNM == "FINISHING")
            {
                return description = pieceDescription;
            }

            if (workCenterNM == "ASSEMBLY")
            {
                return description = pieceDescription;
            }

            if (workCenterNM == "PACKING")
            {
                return description = pieceDescription;
            }

            return string.Empty;
        }

        public bool IsExistProductionItemCode(string code, List<string> listCodes)
        {
            if (listCodes.Count == 0)
            {
                return false;
            }

            foreach (var item in listCodes)
            {
                if (code.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
