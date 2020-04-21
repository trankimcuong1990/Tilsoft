using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
namespace Module.BOMStandardMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private string _tempFolder;
        private DataConverter converter = new DataConverter();
        public DataFactory() {}

        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }

        private BOMStandardMngEntities CreateContext()
        {
            return new BOMStandardMngEntities(Library.Helper.CreateEntityConnectionString("DAL.BOMStandardMngModel"));
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
                using (BOMStandardMngEntities context = CreateContext())
                {
                    context.BOMStandardMng_function_DeleteBOMStandard(id);
                }
                return true;
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
                return false;
            }
            
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMStandardMngEntities context = CreateContext())
                {
                    BOMStandardMng_BOMStandard_View dbItem;
                    dbItem = context.BOMStandardMng_BOMStandard_View.FirstOrDefault(o => o.BOMStandardID == id);
                    editFormData.Data = converter.DB2DTO_BOMStandard(dbItem);
                    return editFormData;
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
                return editFormData;
            }
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
            DTO.BOMStandardDTO dtoBOM = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.BOMStandardDTO>();
            try
            {                               
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (BOMStandardMngEntities context = CreateContext())
                {
                    //delete bom standard is deleted
                    context.BOMStandardMng_function_DeleteBOMStandardIsDeleted();
                    //get bom standard
                    BOMStandard dbItem = null;
                    if (id > 0)
                    {
                        dbItem = context.BOMStandard.Where(o => o.BOMStandardID == id).FirstOrDefault();
                    }
                    else
                    {
                        dbItem = new BOMStandard();
                        context.BOMStandard.Add(dbItem);
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {                        
                        //convert dto to db
                        converter.DTO2DB_BOMStandard(dtoBOM, ref dbItem);

                        //validate unit
                        this.ValidateUnit(dbItem);

                        //adjust some info tracking
                        this.SetTrackingInfoForBOMStandard(userId, companyID, ref dbItem);
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.BOMStandardID, out notification).Data;
                        return true;
                    }
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
                return false;
            }
        }

        public DTO.EditFormData GetData(int userId, int id, Hashtable param , out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMStandardMngEntities context = CreateContext())
                {
                    //delete bom standard is deleted
                    context.BOMStandardMng_function_DeleteBOMStandardIsDeleted();
                    //get companyID
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (id > 0)
                    {
                        BOMStandardMng_BOMStandard_View dbItem;
                        dbItem = context.BOMStandardMng_BOMStandard_View.Where(o =>o.CompanyID==companyID).FirstOrDefault(o => o.BOMStandardID == id);
                        editFormData.Data = converter.DB2DTO_BOMStandard(dbItem);
                    }
                    else
                    {
                        int productionProcessID = Convert.ToInt32(param["productionProcessID"]);
                        var productionProcess = context.BOMStandardMng_ProductionProcess_SearchView.Where(o => o.ProductionProcessID == productionProcessID).FirstOrDefault();
                        editFormData.Data = new DTO.BOMStandardDTO();
                        editFormData.Data.BOMStandardDTOs = new List<DTO.BOMStandardDTO>();

                        //init workorder info
                        editFormData.Data.ProductionProcessID = productionProcessID;
                        editFormData.Data.ProductArticleCode = productionProcess.ProductArticleCode;
                        editFormData.Data.ProductDescription = productionProcess.ProductDescription;
                        editFormData.Data.SampleProductUD = productionProcess.ProductArticleCode;
                        editFormData.Data.SampleArticleDescription = productionProcess.ProductDescription;
                        editFormData.Data.OPSequenceNM = productionProcess.OPSequenceNM;
                        editFormData.Data.OPSequenceID = productionProcess.OPSequenceID;
                        editFormData.Data.ModelID = productionProcess.ModelID;
                    }
                    //set price
                    int year = Convert.ToInt32(param["year"]);
                    int month = Convert.ToInt32(param["month"]);
                    bool? isLock = null;
                    if (param.ContainsKey("isLocked") && param["isLocked"] != null && !string.IsNullOrEmpty(param["isLocked"].ToString()))
                    {
                        isLock = (Convert.ToBoolean(param["isLocked"]) == true) ? true : false;
                    }
                    if(isLock == false)
                    {
                        isLock = null;
                    }

                    List<DTO.PriceList> listPrices = new List<DTO.PriceList>();
                    var priceList = context.BOMStandardMng_ProductionPrice_View.Where(o => o.Month == month && o.Year == year && o.CompanyID == companyID);
                    if (isLock.HasValue && isLock.Value)
                    {
                        priceList = priceList.Where(o => o.IsLocked == isLock);
                    }

                    listPrices = converter.DB2DTO_PriceList(priceList.ToList());

                    var bomStandardData = editFormData.Data;
                    SetPrice(listPrices, ref bomStandardData);

                    //Breakdown
                    int? idProcess = editFormData.Data.ProductionProcessID;
                    var ProductionProcess = context.BreakdownMng_BOMProductOption_View.FirstOrDefault(o => o.ProductionProcessID == idProcess);
                    
                    if(ProductionProcess != null)
                    {
                        editFormData.Data.CheckBreakdown = true;
                    }
                    else
                    {
                        editFormData.Data.CheckBreakdown = false;
                    }

                    //get support list
                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    editFormData.OPSequenceDetails = support_factory.GetOPSequenceDetail(editFormData.Data.OPSequenceID);
                    editFormData.ProductionItemTypes = support_factory.GetProductionItemType();
                    editFormData.ProductionTeams = support_factory.GetProductionTeam(companyID);                 
                    return editFormData;
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
                return editFormData;
            }
        }

        public DTO.ViewFormData GetViewData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            DTO.ViewFormData viewFormData = new DTO.ViewFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMStandardMngEntities context = CreateContext())
                {
                    //delete bom standard is deleted
                    context.BOMStandardMng_function_DeleteBOMStandardIsDeleted();
                    //get companyID
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (id > 0)
                    {
                        BOMStandardMng_BOMStandardView_View dbItem;
                        dbItem = context.BOMStandardMng_BOMStandardView_View.FirstOrDefault(o => o.BOMStandardID == id);
                        viewFormData.Data = converter.DB2DTO_BOMStandardView(dbItem);
                    }
                    else
                    {
                        int productionProcessID = Convert.ToInt32(param["productionProcessID"]);
                        var productionProcess = context.BOMStandardMng_ProductionProcess_SearchView.Where(o => o.ProductionProcessID == productionProcessID).FirstOrDefault();
                        viewFormData.Data = new DTO.BOMStandardViewDTO();
                        viewFormData.Data.BOMStandardViewDTOs = new List<DTO.BOMStandardViewDTO>();

                        //init workorder info
                        //viewFormData.Data.ProductionProcessID = productionProcessID;
                        viewFormData.Data.ProductArticleCode = productionProcess.ProductArticleCode;
                        viewFormData.Data.ProductDescription = productionProcess.ProductDescription;
                        //viewFormData.Data.SampleProductUD = productionProcess.ProductArticleCode;
                        //viewFormData.Data.SampleArticleDescription = productionProcess.ProductDescription;
                        viewFormData.Data.OPSequenceNM = productionProcess.OPSequenceNM;
                        //viewFormData.Data.OPSequenceID = productionProcess.OPSequenceID;
                        //viewFormData.Data.ModelID = productionProcess.ModelID;
                    }
                    //set price
                    int year = Convert.ToInt32(param["year"]);
                    int month = Convert.ToInt32(param["month"]);
                    bool? isLock = null;
                    if (param.ContainsKey("isLocked") && param["isLocked"] != null && !string.IsNullOrEmpty(param["isLocked"].ToString()))
                    {
                        isLock = (Convert.ToBoolean(param["isLocked"]) == true) ? true : false;
                    }
                    if (isLock == false)
                    {
                        isLock = null;
                    }

                    List<DTO.PriceList> listPrices = new List<DTO.PriceList>();
                    var priceList = context.BOMStandardMng_ProductionPrice_View.Where(o => o.Month == month && o.Year == year && o.CompanyID == companyID);
                    if (isLock.HasValue && isLock.Value)
                    {
                        priceList = priceList.Where(o => o.IsLocked == isLock);
                    }

                    listPrices = converter.DB2DTO_PriceList(priceList.ToList());

                    var bomStandardData = viewFormData.Data;
                    SetViewPrice(listPrices, ref bomStandardData);

                    //get support list
                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    //viewFormData.OPSequenceDetails = support_factory.GetOPSequenceDetail(viewFormData.Data.OPSequenceID);
                    viewFormData.ProductionItemTypes = support_factory.GetProductionItemType();
                    //viewFormData.ProductionTeams = support_factory.GetProductionTeam(companyID);
                    return viewFormData;
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
                return viewFormData;
            }
        }

        private void SetTrackingInfoForBOMStandard(int? userID, int? companyID, ref BOMStandard dbBOM)
        {
            dbBOM.UpdatedBy = userID;
            dbBOM.UpdatedDate = DateTime.Now;
            dbBOM.CompanyID = companyID;
            if (dbBOM.IsDeleted.HasValue && dbBOM.IsDeleted.Value)
            {
                dbBOM.DeletedBy = userID;
                dbBOM.DeletedDate = DateTime.Now;
            }
            foreach (var item in dbBOM.BOMStandard1)
            {
                BOMStandard x = item;
                SetTrackingInfoForBOMStandard(userID, companyID, ref x);
            }
        }

        private void SetPrice(List<DTO.PriceList> PriceList, ref DTO.BOMStandardDTO BOMStadardData)
        {
            int? productionItemID = BOMStadardData.ProductionItemID;
            int? productionItemTypeID = BOMStadardData.ProductionItemTypeID;

            var priceItem = PriceList.Where(o => o.ProductionItemTypeID == productionItemTypeID && o.ProductionItemID == productionItemID).FirstOrDefault();
            if(priceItem != null && priceItem.Price != null)
            {
                BOMStadardData.Price = priceItem.Price;
            }         
            foreach (var item in BOMStadardData.BOMStandardDTOs)
            {
                DTO.BOMStandardDTO BOMStadardDataChild = item;
                SetPrice(PriceList, ref BOMStadardDataChild);
            }
        }

        private void SetViewPrice(List<DTO.PriceList> PriceList, ref DTO.BOMStandardViewDTO BOMStadardData)
        {
            int? productionItemID = BOMStadardData.ProductionItemID;
            int? productionItemTypeID = BOMStadardData.ProductionItemTypeID;

            var priceItem = PriceList.Where(o => o.ProductionItemTypeID == productionItemTypeID && o.ProductionItemID == productionItemID).FirstOrDefault();
            if (priceItem != null && priceItem.Price != null)
            {
                BOMStadardData.Price = priceItem.Price;
            }
            foreach (var item in BOMStadardData.BOMStandardViewDTOs)
            {
                DTO.BOMStandardViewDTO BOMStadardDataChild = item;
                SetViewPrice(PriceList, ref BOMStadardDataChild);
            }
        }

        private void ValidateUnit(BOMStandard dbBOM)
        {
            if (dbBOM.ParentBOMStandardID.HasValue && !dbBOM.UnitID.HasValue)
            {
                throw new Exception("There are some item that does not have unit (red cell). You need edit and fill-in unit before save BOM");
            }
            foreach (var item in dbBOM.BOMStandard1)
            {
                ValidateUnit(item);
            }
        }

        public List<DTO.ProductionItemDTO> GetListProductionItem(int userId, object dtoBOMProduct, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.BOMStandardImportDTO> bomImportData = ((Newtonsoft.Json.Linq.JArray)dtoBOMProduct).ToObject<List<DTO.BOMStandardImportDTO>>();

            //get articleCode of product on workOrder
            string articleCode = bomImportData.FirstOrDefault().Code;

            //only get excel item that have Sequence
            bomImportData = bomImportData.Where(o => !string.IsNullOrEmpty(o.Sequence)).ToList();
            try
            {
                //auto create products for pieces that are declared on model
                using (BOMStandardMngEntities context = CreateContext())
                {
                    var workOrderProduct = context.Product.Select(o => new { o.ProductID, o.ArticleCode }).Where(s => s.ArticleCode == articleCode).FirstOrDefault();
                    if (workOrderProduct == null)
                    {
                        throw new Exception("Could not find product of workorder in system");
                    }
                    else {
                        context.ProductMng_function_CreateProductPiece(workOrderProduct.ProductID);
                    }                    
                }

                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (BOMStandardMngEntities context = CreateContext())
                {
                    ProductionItem dbProductionItem;
                    ProductionItemWarehouse dbProductionItemWarehouse;
                    var dtoProduct = bomImportData.Select(o => new { o.Code}).Distinct();
                    foreach (var item in dtoProduct)
                    {
                        var p = context.ProductionItem.Where(o => o.ProductionItemUD == item.Code).Count();
                        if (p == 0)
                        {
                            if (item.Code.Length > 100)
                            {
                                throw new Exception("Can not create code for component: " + item.Code + ". Code must be less than 100 character");
                            }

                            //get bom item base on production item code
                            DTO.BOMStandardImportDTO dtoBOMItem = bomImportData.Where(o => o.Code == item.Code).FirstOrDefault();

                            //create production item
                            dbProductionItem = new ProductionItem();
                            dbProductionItem.ProductionItemUD = item.Code;
                            dbProductionItem.CompanyID = companyID;
                            dbProductionItem.UpdatedBy = userId;
                            dbProductionItem.UpdatedDate = DateTime.Now;
                            dbProductionItem.ProductionItemNM = dtoBOMItem.Description;
                            dbProductionItem.Unit = dtoBOMItem.Unit;
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
                                else {
                                    dbProductionItem.ProductionItemTypeID = 1; //component
                                }

                                //create factory warehouse default for this component
                                if (dtoBOMItem.DefaultFactoryWarehouseID.HasValue)
                                {
                                    dbProductionItemWarehouse = new ProductionItemWarehouse();
                                    dbProductionItemWarehouse.FactoryWarehouseID = dtoBOMItem.DefaultFactoryWarehouseID;
                                    dbProductionItemWarehouse.IsDefault = true;
                                    dbProductionItem.ProductionItemWarehouse.Add(dbProductionItemWarehouse);
                                }                                
                            }
                            context.ProductionItem.Add(dbProductionItem);
                        }
                    }
                    context.SaveChanges();
                    //get list production item
                    List<string> codes = bomImportData.Select(o => o.Code).ToList();
                    var dbItems = context.BOMStandardMng_ProductionItem_View.Where(o => codes.Contains(o.ProductionItemUD)).ToList();
                    return AutoMapper.Mapper.Map<List<BOMStandardMng_ProductionItem_View>, List<DTO.ProductionItemDTO>>(dbItems);
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
                return new List<DTO.ProductionItemDTO>();
            }
        }

        /// <summary>
        /// create BOM excel template so user can easily fill-in material for every component and import back to database
        /// base on product of one production process system will find all piece of this product and generate template
        /// </summary>
        /// <param name="productionProcessID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public string CreateImportTemplate(int productionProcessID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ImportExcelTemplateDataObject ds = new ImportExcelTemplateDataObject();

            try
            {
                //auto create products for pieces that are declared on model
                using (BOMStandardMngEntities context = CreateContext())
                {
                    int? productID = context.ProductionProcess.Select(o => new { o.ProductionProcessID,o.ProductID }).Where(s => s.ProductionProcessID == productionProcessID).FirstOrDefault().ProductID;
                    context.ProductMng_function_CreateProductPiece(productID);
                }

                //create template
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("BOMStandardMng_function_CreateImportTemplate", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ProductionProcessID", productionProcessID);

                adap.TableMappings.Add("Table", "ProductionProcess");
                adap.TableMappings.Add("Table1", "Piece");
                adap.TableMappings.Add("Table2", "WorkCenter");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "BOMStandard_ExcelImportTemplate");
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

        /// <summary>
        /// create data to make BOM template base on OP Sequence of workOrder and product of workOrder
        /// base on product of workOrder system will find all piece of this product and generate template
        /// </summary>
        /// <param name="productionProcessID"></param>
        /// <param name="copyFromproductionProcessID">get material from this production process to apply for components of prodcution process : productionProcessID</param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public DTO.ImportTemplate.ImportTemplateData CreateBOMTemplateData(int userId, int productionProcessID, int? copyFromproductionProcessID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ImportExcelTemplateDataObject ds = new ImportExcelTemplateDataObject();

            DTO.ImportTemplate.ImportTemplateData data = new DTO.ImportTemplate.ImportTemplateData();
            try
            {
                //auto create products for pieces that are declared on model
                using (BOMStandardMngEntities context = CreateContext())
                {
                    int? productID = context.ProductionProcess.Select(o => new { o.ProductionProcessID, o.ProductID }).Where(s => s.ProductionProcessID == productionProcessID).FirstOrDefault().ProductID;
                    context.ProductMng_function_CreateProductPiece(productID);
                }

                //read db to dataset
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("BOMStandardMng_function_CreateImportTemplate", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ProductionProcessID", productionProcessID);

                adap.TableMappings.Add("Table", "ProductionProcess");
                adap.TableMappings.Add("Table1", "Piece");
                adap.TableMappings.Add("Table2", "WorkCenter");
                adap.Fill(ds);

                //read from dataset to dto workOrder
                var wo = ds.ProductionProcess.FirstOrDefault();
                data.ProductionProcess.ProductionProcessID = wo.ProductionProcessID;
                data.ProductionProcess.ProductID = wo.ProductID;
                data.ProductionProcess.OPSequenceID = wo.OPSequenceID;
                data.ProductionProcess.ModelID = wo.ModelID;
                data.ProductionProcess.ArticleCode = wo.ArticleCode;
                data.ProductionProcess.Description = wo.Description;
                data.ProductionProcess.ModelUD = wo.ModelUD;
                data.ProductionProcess.ModelNM = wo.ModelNM;

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
                string productionItemUD = "";
                string productionItemNM = "";
                int? productionItemTypeID = null;
                int? pieceProductID = null;

                //get max sequence index in OP Sequence so we can know which workcenter is final in sequence
                int? maxIndex = data.WorkCenters.Max(o => o.SequenceIndexNumber);

                //db to save data
                ProductionItem dbProductionItem;
                ProductionItemWarehouse dbProductionItemWarehouse;

                //declare list product code 
                List<string> productionItemCodes = new List<string>();

                using (BOMStandardMngEntities context = CreateContext())
                {
                    foreach (var pi in data.Pieces)
                    {
                        foreach (var wc in data.WorkCenters)
                        {
                            if (wc.SequenceIndexNumber == maxIndex) //workCenter final in the OP Sequence (example PACKING in FRAME-->PAINT-->WEAVING-->FINISHED-->PACKING)
                            {
                                productionItemUD = pi.PieceArticleCode;
                                productionItemNM = pi.PieceDescription;
                                productionItemTypeID = 3; //Piece
                                pieceProductID = pi.PieceProductID;
                            }
                            else {                                
                                productionItemUD = productionProcessID + "-" + pi.PieceArticleCode + "-" + wc.WorkCenterNM;
                                productionItemNM = pi.PieceNM + "-" + wc.WorkCenterNM;
                                productionItemTypeID = 1; //Component
                                pieceProductID = null;
                            }
                            
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
                                dbProductionItem.Unit = "PCS";
                                dbProductionItem.UnitID = 25;
                                dbProductionItem.ProductionItemTypeID = productionItemTypeID;
                                dbProductionItem.ProductID = pieceProductID;

                                //create factory warehouse default for this component
                                int? defaultWarehouseID = context.WorkCenter.Where(o => o.WorkCenterNM == wc.WorkCenterNM).FirstOrDefault().DefaultFactoryWarehouseID;
                                dbProductionItemWarehouse = new ProductionItemWarehouse();
                                dbProductionItemWarehouse.FactoryWarehouseID = defaultWarehouseID;
                                dbProductionItemWarehouse.IsDefault = true;
                                dbProductionItem.ProductionItemWarehouse.Add(dbProductionItemWarehouse);

                                //add to context
                                context.ProductionItem.Add(dbProductionItem);
                            }

                            //add to list product code
                            productionItemCodes.Add(productionItemUD);
                        }
                    }
                    context.SaveChanges();

                    //get list production item
                    var productionItems = context.BOMStandardMng_ProductionItem_View.Where(o => productionItemCodes.Contains(o.ProductionItemUD)).ToList();
                    data.ProductionItems = AutoMapper.Mapper.Map<List<BOMStandardMng_ProductionItem_View>, List<DTO.ProductionItemDTO>>(productionItems);

                    //get list BOM source by workOrder
                    var bomStandardSource = context.BOMStandardMng_CreateImportTemplate_BOMStandard_View.Where(o => o.ProductionProcessID == copyFromproductionProcessID).ToList();
                    data.BOMStandardSources = AutoMapper.Mapper.Map<List<BOMStandardMng_CreateImportTemplate_BOMStandard_View>, List<DTO.ImportTemplate.BOMStandardSourceDTO>>(bomStandardSource); ;

                }

                return data;
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
                return new DTO.ImportTemplate.ImportTemplateData();
            }
        }
        
        /// <summary>
        ///     every production process for every product, so we need set default for production process that have same modelID
        /// </summary>
        /// <param name="productionProcessID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool SetBOMStandardDefault(int productionProcessID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMStandardMngEntities context = CreateContext())
                {
                    //set default is false for all production process is same model
                    var currentProductionProcess = context.BOMStandardMng_ProductionProcess_SearchView.Select(o => new { o.ProductionProcessID, o.ModelID, o.FrameMaterialID }).Where(o => o.ProductionProcessID == productionProcessID).FirstOrDefault();
                    int? modelID = currentProductionProcess.ModelID;
                    int? frameMaterialID = currentProductionProcess.FrameMaterialID;
                    List<int> productionProcessIDs = context.BOMStandardMng_ProductionProcess_SearchView.Where(o => o.ModelID == modelID && o.FrameMaterialID==frameMaterialID).Select(o =>o.ProductionProcessID).ToList();
                    var allProductionProcess = context.ProductionProcess.Where(o => productionProcessIDs.Contains(o.ProductionProcessID)).ToList();
                    allProductionProcess.ForEach(o => { o.IsDefaultOfModel = false; });

                    //set deafult is true for selected production process
                    var x = context.ProductionProcess.Where(o => o.ProductionProcessID == productionProcessID).FirstOrDefault();
                    x.IsDefaultOfModel = true;

                    //save change
                    context.SaveChanges();
                    return true;
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
                return false;
            }
        }

        /// <summary>
        ///     get list all production process that are same model
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public List<DTO.ProductionProcessSearchDTO> GetProductionProcessByModel(int? modelID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.ProductionProcessSearchDTO> data = new List<DTO.ProductionProcessSearchDTO>();
            try
            {
                using (BOMStandardMngEntities context = CreateContext())
                {
                    var x = context.BOMStandardMng_ProductionProcess_SearchView.Where(o => o.ModelID == modelID).ToList();
                    data = AutoMapper.Mapper.Map<List<BOMStandardMng_ProductionProcess_SearchView>, List<DTO.ProductionProcessSearchDTO>>(x);
                    return data;
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
                return data;
            }
        }

        public DTO.EditProductionProcessFormData GetProductionProcessData(int userId, int productionProcessID, out Library.DTO.Notification notification)
        {
            DTO.EditProductionProcessFormData data = new DTO.EditProductionProcessFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMStandardMngEntities context = CreateContext())
                {
                    Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (productionProcessID > 0)
                    {
                        BOMStandardMng_ProductionProcess_View dbItem;
                        dbItem = context.BOMStandardMng_ProductionProcess_View.FirstOrDefault(o => o.CompanyID == companyID && o.ProductionProcessID == productionProcessID);
                        data.Data = AutoMapper.Mapper.Map<BOMStandardMng_ProductionProcess_View, DTO.ProductionProcessDTO>(dbItem);
                    }
                    else
                    {
                        data.Data = new DTO.ProductionProcessDTO();
                    }
                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    data.OPSequences = support_factory.GetOPSequence(companyID);
                    return data;
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
                return data;
            }
        }

        public int UpdateProductionProcess(int userId, int productionProcessID, object dtoData, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProductionProcessDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoData).ToObject<DTO.ProductionProcessDTO>();
            try
            {
                //get companyID
                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                int? companyID = fw_factory.GetCompanyID(userId);
                using (BOMStandardMngEntities context = CreateContext())
                {
                    ProductionProcess dbItem = null;
                    if (productionProcessID == 0)
                    {
                        dbItem = new ProductionProcess();
                        context.ProductionProcess.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ProductionProcess.Where(o => o.ProductionProcessID == productionProcessID).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return -1;
                    }
                    else
                    {
                        //convert dto to db
                        AutoMapper.Mapper.Map<DTO.ProductionProcessDTO, ProductionProcess>(dtoItem, dbItem);
                        dbItem.CompanyID = companyID;
                        dbItem.CreatedBy = userId;
                        dbItem.CreatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        //save data
                        context.SaveChanges();
                        //get return data
                        return dbItem.ProductionProcessID;
                    }
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
                return -1;
            }
        }

        public DTO.SearchProductionProcessFormData SearchProductionProcess(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchProductionProcessFormData data = new DTO.SearchProductionProcessFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                int? companyID = null;
                string productArticleCode = null;
                string productDescription = null;
                string modelUD = null;
                string sampleProductUD = null;
                string sampleArticleDescription = null;
                string opSequenceNM = null;

                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("productArticleCode") && !string.IsNullOrEmpty(filters["productArticleCode"].ToString()))
                {
                    productArticleCode = filters["productArticleCode"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("productDescription") && !string.IsNullOrEmpty(filters["productDescription"].ToString()))
                {
                    productDescription = filters["productDescription"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("modelUD") && !string.IsNullOrEmpty(filters["modelUD"].ToString()))
                {
                    modelUD = filters["modelUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("sampleProductUD") && !string.IsNullOrEmpty(filters["sampleProductUD"].ToString()))
                {
                    sampleProductUD = filters["sampleProductUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("sampleArticleDescription") && !string.IsNullOrEmpty(filters["sampleArticleDescription"].ToString()))
                {
                    sampleArticleDescription = filters["sampleArticleDescription"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("opSequenceNM") && !string.IsNullOrEmpty(filters["opSequenceNM"].ToString()))
                {
                    opSequenceNM = filters["opSequenceNM"].ToString().Replace("'", "''");
                }

                using (BOMStandardMngEntities context = CreateContext())
                {
                    totalRows = context.BOMStandardMng_function_SearchProductionProcess(orderBy, orderDirection, companyID, productArticleCode, productDescription, modelUD, sampleProductUD, sampleArticleDescription, opSequenceNM).Count();
                    var result = context.BOMStandardMng_function_SearchProductionProcess(orderBy, orderDirection, companyID, productArticleCode, productDescription, modelUD, sampleProductUD, sampleArticleDescription, opSequenceNM);
                    data.Data = converter.DB2DTO_ProductionProcessSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return data;
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
                return data;
            }
        }

        public string ExportBOMStandardToExcel(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BOMStandardMngEntities context = CreateContext())
                {
                    //delete bom standard is deleted
                    context.BOMStandardMng_function_DeleteBOMStandardIsDeleted();
                    BOMStandardMng_ExportToExcel_View dbItem;
                    dbItem = context.BOMStandardMng_ExportToExcel_View.Where(o => o.BOMStandardID == id).FirstOrDefault();

                    //fill-in data to data set 
                    ExportBOMStandardObject dsResult = new ExportBOMStandardObject();
                    this.ParseBOMStandardToList(dbItem, ref dsResult);
                    return Library.Helper.CreateReportFileWithEPPlus2(dsResult, "BOMStandard_ExcelExport");
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

        private void ParseBOMStandardToList(BOMStandardMng_ExportToExcel_View bomData, ref ExportBOMStandardObject dsResult)
        {
            if (bomData != null && bomData.BOMStandardMng_ExportToExcel_View1 != null)
            {
                //push to list result
                ExportBOMStandardObject.BOMStandardRow dr;
                dr = dsResult.BOMStandard.NewBOMStandardRow();
                if (bomData.PieceIndex.HasValue) dr.PieceIndex = bomData.PieceIndex.Value;
                if (!string.IsNullOrEmpty(bomData.ProductionItemTypeNM)) dr.ProductionItemTypeNM = bomData.ProductionItemTypeNM;
                if(!string.IsNullOrEmpty(bomData.WorkCenterNM)) dr.WorkCenterNM = bomData.WorkCenterNM;
                if (!string.IsNullOrEmpty(bomData.ProductionItemUD)) dr.ProductionItemUD = bomData.ProductionItemUD;
                if (!string.IsNullOrEmpty(bomData.ProductionItemNM)) dr.ProductionItemNM = bomData.ProductionItemNM;
                if (bomData.Quantity.HasValue) dr.Norm = bomData.Quantity.Value;
                if (!string.IsNullOrEmpty(bomData.Unit)) dr.UnitNM = bomData.UnitNM;
                if (!string.IsNullOrEmpty(bomData.ProductArticleCode)) dr.ProductArticleCode = bomData.ProductArticleCode;
                if (!string.IsNullOrEmpty(bomData.ProductDescription)) dr.ProductDescription = bomData.ProductDescription;
                dsResult.BOMStandard.AddBOMStandardRow(dr);

                //it just only assign piece index for child of root node
                if (bomData.ParentBOMStandardID == null)
                {
                    int i = 1;
                    foreach (var item in bomData.BOMStandardMng_ExportToExcel_View1.ToList())
                    {
                        item.PieceIndex = i;
                        i++;
                    }
                }

                foreach (var item in bomData.BOMStandardMng_ExportToExcel_View1.OrderBy(o =>o.BOMStandardMng_ExportToExcel_View1.Count))
                {
                    if (!item.PieceIndex.HasValue) {
                        item.PieceIndex = bomData.PieceIndex;
                    }
                    ParseBOMStandardToList(item, ref dsResult);
                }                
            }
        }
        public DTO.EditFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.ProductionItemTypes = new List<Support.DTO.ProductionItemType>();

            //try to get data
            try
            {
                using(var context = CreateContext())
                {
                    data.DateProductionPrices = converter.DB2DTO_Date(context.BOMStandardMng_DateOfPrice_View.ToList());
                }              
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public object GetWorkOrderByBOMStandardID(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<BOMStandardMng_WorkOrder_View> data = new List<BOMStandardMng_WorkOrder_View>();

            try
            {
                using (var context = CreateContext())
                {
                    data = context.BOMStandardMng_WorkOrder_View.Where(o => o.BOMStandardID == id).ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetWorkOrderApplyByBOMStandardID(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<BOMStandardMng_WorkOrderApply_View> data = new List<BOMStandardMng_WorkOrderApply_View>();

            try
            {
                using (var context = CreateContext())
                {
                    data = context.BOMStandardMng_WorkOrderApply_View.Where(o => o.BOMStandardID == id).ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetWorkCenterByBOMStandardID(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<BOMStandardMng_WorkCenter_View> data = new List<BOMStandardMng_WorkCenter_View>();

            try
            {
                using (var context = CreateContext())
                {
                    data = context.BOMStandardMng_WorkCenter_View.Where(o => o.BOMStandardID == id).ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public bool UpdateWorkOrderApply(int userID, int id, string workOrderIDs, string opSequenceDetailIDs, int applyTypeID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    if (applyTypeID == 1) // Add New
                    {
                        context.BOMStandardMng_function_AddNewApplyBOM(id, workOrderIDs, opSequenceDetailIDs, applyTypeID);
                    }

                    if (applyTypeID == 2) // Replace
                    {
                        var documentNoteCreated = context.BOMStandardMng_function_CheckDocumentNoteCreated(id, workOrderIDs, opSequenceDetailIDs, applyTypeID).FirstOrDefault();

                        if (documentNoteCreated.HasValue && documentNoteCreated.Value > 0)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "You can not change material link work order and work center selected";
                            return false;
                        }

                        context.BOMStandardMng_function_ReplaceApplyBOM(id, workOrderIDs, opSequenceDetailIDs, applyTypeID);
                    }

                    if (applyTypeID == 3) // Update
                    {
                        context.BOMStandardMng_function_UpdateApplyBOM(id, workOrderIDs, opSequenceDetailIDs, applyTypeID);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }
    }
}
