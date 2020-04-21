namespace Module.WorkOrder.DAL
{
    using Library;
    using Library.DTO;
    using Module.WorkOrder.DTO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;

    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private readonly string moduleCode = "WorkOrderMng";

        public DataFactory() { }

        private WorkOrderEntities CreateContext()
        {
            return new WorkOrderEntities(Library.Helper.CreateEntityConnectionString("DAL.WorkOrderModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.WorkOrder.FirstOrDefault(o => o.WorkOrderID == id);

                    // Check dbItem is null
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not found data!";

                        return false;
                    }

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedBy = userId;
                    dbItem.ConfirmedDate = DateTime.Now;
                    context.SaveChanges();
                }

                dtoItem = GetData(id, out notification).Data;

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WorkOrderEntities context = CreateContext())
                {
                    var dbItem = context.WorkOrder.Where(o => o.WorkOrderID == id).FirstOrDefault();

                    if (dbItem.BOM.Count > 0)
                    {
                        throw new Exception("Can't Delete WorkOrder !");
                    }
                    context.WorkOrder.Remove(dbItem);
                    context.SaveChanges();
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
                using (WorkOrderEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        WorkOrderMng_WorkOrder_View dbItem;
                        dbItem = context.WorkOrderMng_WorkOrder_View.FirstOrDefault(o => o.WorkOrderID == id);
                        editFormData.Data = converter.DB2DTO_WorkOrder(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.WorkOrderDTO();
                    }
                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    editFormData.WorkOrderStatuses = support_factory.GetWorkOrderStatus();
                    editFormData.WorkOrderTypes = support_factory.GetWorkOrderType();
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
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            searchFormData.WeavingStatus = new List<DTO.WeavingStatus>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            try
            {
                string workOrderUD = null;
                string productArticleCode = null;
                string productDescription = null;
                string modelUD = null;
                string clientUD = null;
                string proformaInvoiceNo = null;
                string createdDate = null;
                bool? isCreatedBOM = null;
                string createdDateBOM = null;
                bool? isConfirmed = null;
                string startDate = null;
                string finishDate = null;
                string updatedDate = null;
                int? workOrderStatusID = null;
                string workOrderPreOrderUD = null;
                int? productionItemGroupID = null;
                bool? hasPurchaseRequest = null;

                int userId = Convert.ToInt32(filters["userId"]);
                int? companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("workOrderUD") && !string.IsNullOrEmpty(filters["workOrderUD"].ToString()))
                {
                    workOrderUD = filters["workOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("workOrderPreOrderUD") && !string.IsNullOrEmpty(filters["workOrderPreOrderUD"].ToString()))
                {
                    workOrderPreOrderUD = filters["workOrderPreOrderUD"].ToString().Replace("'", "''");
                }
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
                if (filters.ContainsKey("clientUD") && !string.IsNullOrEmpty(filters["clientUD"].ToString()))
                {
                    clientUD = filters["clientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("proformaInvoiceNo") && !string.IsNullOrEmpty(filters["proformaInvoiceNo"].ToString()))
                {
                    proformaInvoiceNo = filters["proformaInvoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("createdDate") && !string.IsNullOrEmpty(filters["createdDate"].ToString()))
                {
                    createdDate = filters["createdDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("isCreatedBOM") && filters["isCreatedBOM"] != null && !string.IsNullOrEmpty(filters["isCreatedBOM"].ToString()))
                {
                    switch (filters["isCreatedBOM"].ToString().ToLower())
                    {
                        case "true":
                            isCreatedBOM = true;
                            break;
                        case "false":
                            isCreatedBOM = false;
                            break;
                        default:
                            isCreatedBOM = null;
                            break;
                    }
                }
                if (filters.ContainsKey("createdDateBOM") && !string.IsNullOrEmpty(filters["createdDateBOM"].ToString()))
                {
                    createdDateBOM = filters["createdDateBOM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("isConfirmed") && filters["isConfirmed"] != null && !string.IsNullOrEmpty(filters["isConfirmed"].ToString()))
                {
                    switch (filters["isConfirmed"].ToString().ToLower())
                    {
                        case "true":
                            isConfirmed = true;
                            break;
                        case "false":
                            isConfirmed = false;
                            break;
                        default:
                            isConfirmed = null;
                            break;
                    }
                }
                if (filters.ContainsKey("startDate") && !string.IsNullOrEmpty(filters["startDate"].ToString()))
                {
                    startDate = filters["startDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("finishDate") && !string.IsNullOrEmpty(filters["finishDate"].ToString()))
                {
                    finishDate = filters["finishDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("updatedDate") && !string.IsNullOrEmpty(filters["updatedDate"].ToString()))
                {
                    updatedDate = filters["updatedDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("workOrderStatusID") && filters["workOrderStatusID"] != null)
                {
                    workOrderStatusID = Convert.ToInt32(filters["workOrderStatusID"]);
                }
                if (filters.ContainsKey("productionItemGroupID") && filters["productionItemGroupID"] != null)
                {
                    productionItemGroupID = Convert.ToInt32(filters["productionItemGroupID"]);
                }
                if (filters.ContainsKey("hasPurchaseRequest") && filters["hasPurchaseRequest"] != null && !string.IsNullOrEmpty(filters["hasPurchaseRequest"].ToString()))
                {
                    switch (filters["hasPurchaseRequest"].ToString().ToLower())
                    {
                        case "true":
                            hasPurchaseRequest = true;
                            break;
                        case "false":
                            hasPurchaseRequest = false;
                            break;
                        default:
                            hasPurchaseRequest = null;
                            break;
                    }
                }
                using (WorkOrderEntities context = CreateContext())
                {

                    //string productDescriptionTemp = null;
                    //if (productDescription != null)
                    //{
                    //    List<string> input = productDescription.Split(' ').ToList();
                    //    input = input.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    //    foreach (string word in input)
                    //    {
                    //        productDescriptionTemp += " AND ProductDescription LIKE '%" + word + "%' ";
                    //    }
                    //}

                    //var dbItem = context.WorkOrderMng_function_SearchWorkOrder(orderBy, orderDirection, companyID, workOrderUD, productArticleCode, productDescriptionTemp, modelUD, clientUD, proformaInvoiceNo, createdDate, isCreatedBOM, createdDateBOM, isConfirmed, startDate, finishDate, updatedDate, workOrderStatusID, workOrderPreOrderUD, productionItemGroupID, hasPurchaseRequest);
                    var sumData = context.WorkOrderMng_function_SearchWorkOrder(orderBy, orderDirection, companyID, workOrderUD, productArticleCode, productDescription, modelUD, clientUD, proformaInvoiceNo, createdDate, isCreatedBOM, createdDateBOM, isConfirmed, startDate, finishDate, updatedDate, workOrderStatusID, workOrderPreOrderUD, productionItemGroupID, hasPurchaseRequest).ToList();
                    searchFormData.TotalWorkOrderQnt = sumData.Sum(x => x.Quantity);
                    totalRows = context.WorkOrderMng_function_SearchWorkOrder(orderBy, orderDirection, companyID, workOrderUD, productArticleCode, productDescription, modelUD, clientUD, proformaInvoiceNo, createdDate, isCreatedBOM, createdDateBOM, isConfirmed, startDate, finishDate, updatedDate, workOrderStatusID, workOrderPreOrderUD, productionItemGroupID, hasPurchaseRequest).Count();
                    var result = context.WorkOrderMng_function_SearchWorkOrder(orderBy, orderDirection, companyID, workOrderUD, productArticleCode, productDescription, modelUD, clientUD, proformaInvoiceNo, createdDate, isCreatedBOM, createdDateBOM, isConfirmed, startDate, finishDate, updatedDate, workOrderStatusID, workOrderPreOrderUD, productionItemGroupID, hasPurchaseRequest);
                    searchFormData.Data = converter.DB2DTO_WorkOrderSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //List<WorkOrderMng_ProductionItemGroup_View> materialGroups = context.WorkOrderMng_ProductionItemGroup_View.ToList();
                    //foreach(var item in searchFormData.Data)
                    //{
                    //    var itemMaterialGroup = materialGroups.Where(o => o.WorkOrderID == item.WorkOrderID);
                    //    foreach(var sItem in itemMaterialGroup)
                    //    {
                    //        if(item.MaterialGroups != null && item.MaterialGroups != "")
                    //            item.MaterialGroups += ", " + sItem.ProductionItemGroupNM;
                    //        else
                    //            item.MaterialGroups = sItem.ProductionItemGroupNM;
                    //    }
                    //}

                    //support data
                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    searchFormData.WorkOrderStatuses = support_factory.GetWorkOrderStatus();
                    searchFormData.WeavingStatus = converter.DB2DTO_WeavingStatus(context.WorkOrderMng_WeavingStatus_View.ToList());
                    searchFormData.productionItemGroupDTOs = converter.DB2DTO_ProductionItemGroup(context.ProductionItemMng_ProductionItemGroup_View.ToList());
                }
                return searchFormData;
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
                return searchFormData;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.WorkOrderDTO dtoWorkOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.WorkOrderDTO>();
            try
            {
                //get companyID                
                int? companyID = fw_factory.GetCompanyID(userId);
                using (WorkOrderEntities context = CreateContext())
                {
                    WorkOrder dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new WorkOrder();
                        context.WorkOrder.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.WorkOrder.Where(o => o.WorkOrderID == id).FirstOrDefault();
                    }

                    // Check workOrder is using as pre-workOrder, and opSequence different warning "Can't change opSequence of workOrder"
                    if (!IsValidationWorkOrder(id, dtoWorkOrder.WorkOrderOPSequences.Count(), context))
                    {
                        throw new Exception("Can't change opSequence of workOrder");
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Data not found!";
                        return false;
                    }
                    else
                    {
                        //validate workorder status
                        if (id <= 0 && dtoWorkOrder.WorkOrderStatusID != 1)
                        {
                            throw new Exception("Work order status must be open in case create work order !!!");
                        }

                        //validate OPSequence in case BOM has already created.
                        if (id > 0)
                        {
                            var bom = context.BOM.Where(o => o.WorkOrderID == id).FirstOrDefault();
                            if (bom != null)
                            {
                                int? oldOPSequenceID = dbItem.OPSequenceID;
                                if (oldOPSequenceID != dtoWorkOrder.OPSequenceID)
                                {
                                    throw new Exception("BOM has been created. So you can refresh page and change OPSequence");
                                }
                            }
                        }

                        //convert dto to db
                        converter.DTO2DB_WorkOrder(dtoWorkOrder, ref dbItem);

                        dbItem.CompanyID = companyID;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        if (id == 0)
                        {
                            dbItem.CreatedDate = DateTime.Now;
                        }

                        if (dbItem.WorkOrderStatusID == 1 || (dbItem.WorkOrderStatusID != 1 && fw_factory.CanPerformAction(userId, moduleCode, Library.DTO.ModuleAction.CanReset)))
                        {
                            dbItem.StartDate = dtoWorkOrder.StartDate.ConvertStringToDateTime();
                            dbItem.FinishDate = dtoWorkOrder.FinishDate.ConvertStringToDateTime();
                        }

                        context.WorkOrderDetail.Local.Where(o => o.WorkOrder == null).ToList().ForEach(o => context.WorkOrderDetail.Remove(o));
                        context.WorkOrderOPSequence.Local.Where(o => o.WorkOrder == null).ToList().ForEach(o => context.WorkOrderOPSequence.Remove(o));

                        /*
                        *  WorkOrderTypeID :
                        *      1 : MTO
                        *      3 : SAMPLE
                        *      4 : PRE-ORDER
                        *      5 : SPAREPART
                        */
                        if (dbItem.WorkOrderTypeID == 1)
                        {
                            //calculate total produce in case work order type is MTO
                            int totalProduce = 0;
                            foreach (var item in dbItem.WorkOrderDetail)
                            {
                                totalProduce += (item.Quantity.HasValue ? item.Quantity.Value : 0);
                            }
                            dbItem.Quantity = totalProduce;
                            //assing modelID
                            var dtoDetal = dtoWorkOrder.WorkOrderDetailDTOs.FirstOrDefault();
                            if (dtoDetal != null)
                            {
                                dbItem.ProductID = dtoDetal.ProductID;
                                dbItem.ModelID = dtoDetal.ModelID;
                            }
                        }
                        //else if (dbItem.WorkOrderTypeID == 2)
                        //{
                        //    int? productID = dbItem.ProductID;
                        //    var x = context.WorkOrderMng_Product_View.Where(o => o.ProductID == productID).FirstOrDefault();
                        //    dbItem.ModelID = x.ModelID;
                        //}
                        else if (dbItem.WorkOrderTypeID == 3 || dbItem.WorkOrderTypeID == 5)
                        {
                            //auto create BOM base on OPSequence
                            var x = context.BOM.Where(o => o.WorkOrderID == dbItem.WorkOrderID).FirstOrDefault();
                            BOM dbBOM;
                            BOM dbParentBOM;
                            if (x == null)
                            {
                                //create BOM root node
                                dbBOM = new BOM();
                                dbBOM.CompanyID = companyID;
                                dbBOM.IsActive = true;
                                dbBOM.UpdatedBy = userId;
                                dbBOM.UpdatedDate = DateTime.Now;
                                dbBOM.IsDeleted = false;
                                dbItem.BOM.Add(dbBOM);

                                //keep the parent BOM
                                dbParentBOM = dbBOM;

                                //create child node is workCenters of OPSequence
                                foreach (var item in new Module.Support.DAL.DataFactory().GetOPSequenceDetail(dbItem.OPSequenceID).OrderByDescending(s => s.SequenceIndexNumber))
                                {
                                    dbBOM = new BOM();
                                    dbBOM.OPSequenceDetailID = item.OPSequenceDetailID;
                                    dbBOM.CompanyID = companyID;
                                    dbBOM.IsActive = true;
                                    dbBOM.UpdatedBy = userId;
                                    dbBOM.UpdatedDate = DateTime.Now;
                                    dbBOM.IsDeleted = false;
                                    dbBOM.Quantity = 1;
                                    dbParentBOM.BOM1.Add(dbBOM);
                                    dbItem.BOM.Add(dbBOM);

                                    //keep the parent BOM
                                    dbParentBOM = dbBOM;
                                }
                            }
                        }
                        //validate quantity
                        if (!dbItem.Quantity.HasValue || dbItem.Quantity.Value == 0)
                        {
                            if (dbItem.WorkOrderTypeID != 3 && dbItem.WorkOrderTypeID != 5) //do not need qnt in case SAMPLE and SPAREPART
                            {
                                throw new Exception("Quantity must be filled-in");
                            }
                        }

                        if (dbItem.PreOrderDescription == "" || dbItem.PreOrderDescription == null)
                        {
                            dbItem.PreOrderDescription = dtoWorkOrder.ProductDescription;
                        }

                        // required create code WorkOrder
                        if(id == 0)
                        {
                            if (dbItem.WorkOrderTypeID == null || dbItem.WorkOrderTypeID <= 0)
                            {
                                throw new Exception("Please select Type");
                            }
                            else
                            {
                                if(dbItem.WorkOrderTypeID == 1)
                                {
                                    foreach (var item in dbItem.WorkOrderDetail.ToList())
                                    {
                                        if (item.FactoryOrderDetailID == null)
                                        {
                                            throw new Exception("Factory is empty. Please check Factory");
                                        }
                                    }
                                }

                                if(dbItem.WorkOrderTypeID == 4 && dbItem.PreOrderWorkOrderID != null)
                                {
                                    if(dbItem.WorkOrderOPSequence.Count == 0 || dbItem.WorkOrderOPSequence == null)
                                    {
                                        throw new Exception("OP Sequence is empty. Please input OP Sequence");
                                    }
                                }
                            }
                            if (dbItem.CreatedDate == null)
                            {
                                throw new Exception("Please input Create Date");
                            }
                        }
                        
                        //save data
                        context.SaveChanges();

                        if (id == 0)
                        {
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT TOP 1 0 FROM WorkOrder WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    // Update code WorkOrder
                                    string newCode = context.WorkOrderMng_function_GenerateWorkOrderUD(dbItem.WorkOrderID).FirstOrDefault();
                                    if (string.IsNullOrEmpty(newCode)) throw new Exception("Failed to generate Work Order Code!");
                                    dbItem.WorkOrderUD = newCode;
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    scope.Commit();
                                }
                            }                            

                            //int workOrderID = dbItem.WorkOrderID;
                            //string createdDate = DateTime.Now.ToString("dd/MM/yyyy");
                            //string yearMonth = DateTime.Now.ToString("yyMM");

                            //if (dbItem.WorkOrderTypeID == 4)
                            //{
                            //    context.WorkOrderMng_function_GeneratePreWorkOrder(workOrderID, createdDate, yearMonth);
                            //}
                            //else
                            //{
                            //    string oneCharFactory = string.Empty;

                            //    if (dbItem.WorkOrderTypeID == 1)
                            //    {
                            //        foreach (var dtoWorkOrderDetail in dtoWorkOrder.WorkOrderDetailDTOs.ToList())
                            //        {
                            //            if ("PAL-01-VHM".Equals(dtoWorkOrderDetail.FactoryUD))
                            //            {
                            //                oneCharFactory = "A";
                            //            }

                            //            if ("PAL-02-VHM".Equals(dtoWorkOrderDetail.FactoryUD))
                            //            {
                            //                oneCharFactory = "B";
                            //            }
                            //        }
                            //    }

                            //    if (dbItem.WorkOrderTypeID == 3) // Generate first character "S" with type MAKE SAMPLE
                            //    {
                            //        oneCharFactory = "S";
                            //    }

                            //    if (dbItem.WorkOrderTypeID == 5) // Generate first character "T" with type MAKE SPAREPART
                            //    {
                            //        oneCharFactory = "T";
                            //    }

                            //    context.WorkOrderMng_function_GenerateWorkOrder(workOrderID, createdDate, yearMonth, oneCharFactory);
                            //}
                        }

                        //get return data
                        dtoItem = GetData(userId, dbItem.WorkOrderID, dtoWorkOrder.BranchID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return false;
            }
        }

        public DTO.EditFormData GetData(int userId, int id, int branchID, out Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            editFormData.SupportOPSequence = new List<Support.DTO.OPSequence>();
            editFormData.WorkOrderListPreOrderDTOs = new List<DTO.WorkOrderListPreOrderDTO>();
            editFormData.WorkOrderReportDTOs = new List<WorkOrderReportDTO>();
            editFormData.WorkOrderReportHeaderDTOs = new List<WorkOrderReportHeaderDTO>();
            editFormData.WorkOrderReportContentDTOs = new List<WorkOrderReportContentDTO>();
            editFormData.ItemNotBOMDTOs = new List<ItemNotBOMDTO>();
            
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (WorkOrderEntities context = CreateContext())
                {
                    int? companyID = fw_factory.GetCompanyID(userId);
                    if (id > 0)
                    {
                        WorkOrderMng_WorkOrder_View dbItem;
                        //dbItem = context.WorkOrderMng_WorkOrder_View.FirstOrDefault(o => o.CompanyID == companyID && o.WorkOrderID == id);
                        dbItem = context.WorkOrderMng_WorkOrder_View.FirstOrDefault(o => o.WorkOrderID == id);
                        editFormData.Data = converter.DB2DTO_WorkOrder(dbItem);

                        //get bom of workorder
                        int? bomID = dbItem.BOMID;
                        editFormData.BOMData = converter.DB2DTO_BOM(context.WorkOrderMng_BOM_View.Where(o => o.BOMID == bomID/* && o.BranchID == branchID*/).FirstOrDefault());
                        editFormData.ItemNotBOMDTOs = AutoMapper.Mapper.Map<List<WorkOrderMng_ItemNotBOM_View>, List<DTO.ItemNotBOMDTO>>(context.WorkOrderMng_ItemNotBOM_View.Where(o => o.WorkOrderID == id).ToList());
                        if (editFormData.BOMData == null)
                        {
                            editFormData.BOMData = new DTO.BOMDTO();
                        }
                        if (dbItem.WorkOrderTypeID == 1) // MAKE TO ORDER
                        {
                            var idPre = context.WorkOrderMng_WorkOrderListPreOrder_View.Where(o => o.WorkOrderID == id).FirstOrDefault().PreOrderWorkOrderID;
                            editFormData.WorkOrderListPreOrderDTOs = converter.DB2DTO_WorkOrderListPreOrder(context.WorkOrderMng_WorkOrderListPreOrder_View.Where(o => o.WorkOrderID == idPre).ToList());
                        }
                        if (dbItem.WorkOrderTypeID == 4 || dbItem.WorkOrderTypeID == 2) //PreOrder And Make To Stock
                        {
                            editFormData.WorkOrderListPreOrderDTOs = converter.DB2DTO_WorkOrderListPreOrder(context.WorkOrderMng_WorkOrderListPreOrder_View.Where(o => o.PreOrderWorkOrderID == id).ToList());
                        }

                        List<WorkOrderMng_WorkOrderOPSequence_View> preWorkOrderOPSequences = new List<WorkOrderMng_WorkOrderOPSequence_View>();
                        int? preOrderWorkOrderID = dbItem.PreOrderWorkOrderID;

                        while (preOrderWorkOrderID.HasValue)
                        {
                            List<WorkOrderMng_WorkOrderOPSequence_View> preOrderOPSequences = new List<WorkOrderMng_WorkOrderOPSequence_View>();
                            preOrderOPSequences = GetOPSequenceDetailByPreOrderWorkOrder(preOrderWorkOrderID, context);

                            foreach (var item in preOrderOPSequences.ToList())
                            {
                                preWorkOrderOPSequences.Add(item);
                            }

                            var workOrder = context.WorkOrder.FirstOrDefault(o => o.WorkOrderID == preOrderWorkOrderID);
                            if (workOrder != null)
                            {
                                preOrderWorkOrderID = workOrder.PreOrderWorkOrderID;
                            }
                        }

                        var opSequenceDetails = context.WorkOrderMng_OPSequenceDetail_View.Where(o => o.OPSequenceID == dbItem.OPSequenceID && o.WorkCenterID != 10);
                        editFormData.OPSequenceDetails = converter.DB2DTO_OPSequenceDetail(opSequenceDetails.ToList());

                        foreach (var preWorkOrderOPSequence in preWorkOrderOPSequences.ToList())
                        {
                            foreach (var opSequenceDetail in editFormData.OPSequenceDetails.ToList())
                            {
                                if (opSequenceDetail.OPSequenceDetailID == preWorkOrderOPSequence.OPSequenceDetailID)
                                {
                                    opSequenceDetail.IsActived = preWorkOrderOPSequence.IsActived;
                                    opSequenceDetail.IsDisabled = (dbItem.PreOrderWorkOrderID.HasValue);
                                }
                            }
                        }

                        //bind list WorkOrder child
                        editFormData.WorkOrderReportHeaderDTOs = converter.DB2DTO_WorkOrderReportHeaderDTO(context.WorkOrderMng_function_WorkOrderReportHeader(id).ToList());
                        

                        List<WorkOrderReportDTO> data = new List<WorkOrderReportDTO>();

                        WorkOrder originalWorkOrder = context.WorkOrder.Where(s => s.WorkOrderID == id).FirstOrDefault();                        
                        WorkOrderReportDTO orginalItem = new WorkOrderReportDTO()
                        {
                            WorkOrderID = originalWorkOrder.WorkOrderID,
                            WorkOrderUD = originalWorkOrder.WorkOrderUD
                        };
                        data.Add(orginalItem);
                        GetWorkOrderChild(ref data, 0);
                        List<ProductionItemDTO> tempProductionItemDTO = converter.DB2DTO_ProductionItemDTO(context.WorkOrderMng_ProductionItem_View.ToList());
                        foreach (var item in data)
                        { 
                            item.ProductionItemDTOs = tempProductionItemDTO.Where(s => s.WorkOrderID == item.WorkOrderID).ToList();

                            List<WorkOrderReportContentDTO> lstAdd = new List<WorkOrderReportContentDTO>();
                            if (item.ProductionItemDTOs.Count > 0)
                            {                              
                                // bind dynamic column cho item child
                                foreach (var productionItemDTO in item.ProductionItemDTOs)
                                {
                                    List<WorkOrderMng_ReceivingNote_View> tempReceiving = context.WorkOrderMng_ReceivingNote_View.Where(s => s.ProductionItemID == productionItemDTO.ProductionItemID
                                                            && s.WorkCenterID == productionItemDTO.WorkCenterID && s.WorkOrderID == productionItemDTO.WorkOrderID.ToString()).ToList();
                                    List<WorkOrderMng_DeliveryNote_View> tempDeliveryNote = context.WorkOrderMng_DeliveryNote_View.Where(s => s.ProductionItemID == productionItemDTO.ProductionItemID
                                                            && s.WorkCenterID == productionItemDTO.WorkCenterID && s.WorkOrderID == productionItemDTO.WorkOrderID.ToString()).ToList();

                                    foreach (DTO.WorkOrderReportHeaderDTO workOrderReportHeaderDTO in editFormData.WorkOrderReportHeaderDTOs)
                                    {
                                        int qntRemain = 0;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            WorkOrderReportContentDTO workOrderReportContentDTO = new WorkOrderReportContentDTO();
                                            workOrderReportContentDTO.ProductionItemID = productionItemDTO.ProductionItemID;
                                            workOrderReportContentDTO.WorkCenterID = workOrderReportHeaderDTO.WorkCenterID.Value;
                                            workOrderReportContentDTO.WorkOrderID = -1;
                                            workOrderReportContentDTO.Type = i;
                                            if (productionItemDTO.WorkCenterID == workOrderReportHeaderDTO.WorkCenterID)
                                            {
                                                int quantity = 0;
                                                switch (i)
                                                {
                                                    case 0:
                                                        workOrderReportContentDTO.Quantity = productionItemDTO.Quantity.Value;
                                                        break;
                                                    case 1:
                                                        quantity = Decimal.ToInt32(tempReceiving.Sum(s => s.Quantity).Value);                                                      
                                                        qntRemain = quantity;
                                                        workOrderReportContentDTO.Quantity = quantity;
                                                        break;
                                                    case 2: 
                                                        quantity = Decimal.ToInt32(tempDeliveryNote.Sum(s => s.Qty).Value);
                                                        qntRemain -= quantity;
                                                        workOrderReportContentDTO.Quantity = quantity;
                                                        break;
                                                    case 3:
                                                        workOrderReportContentDTO.Quantity = qntRemain;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }

                                            lstAdd.Add(workOrderReportContentDTO);                                       
                                        }
                                    }
                                }
                            }

                            editFormData.WorkOrderReportContentDTOs.AddRange(lstAdd);

                            // bind dynamic column cho item parent
                            foreach (DTO.WorkOrderReportHeaderDTO workOrderReportHeaderDTO in editFormData.WorkOrderReportHeaderDTOs)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    WorkOrderReportContentDTO workOrderReportContentDTO = new WorkOrderReportContentDTO();
                                    workOrderReportContentDTO.ProductionItemID = -1;
                                    workOrderReportContentDTO.WorkOrderID = item.WorkOrderID;
                                    workOrderReportContentDTO.WorkCenterID = workOrderReportHeaderDTO.WorkCenterID.Value;
                                    workOrderReportContentDTO.Type = i;
                                    workOrderReportContentDTO.Quantity = 0;
                                    if (lstAdd.Count > 0)
                                        workOrderReportContentDTO.Quantity = lstAdd.Where(s => s.Type == i && s.WorkCenterID == workOrderReportHeaderDTO.WorkCenterID.Value).Min(s => s.Quantity);
                                    editFormData.WorkOrderReportContentDTOs.Add(workOrderReportContentDTO);
                                }
                            }
                        }
                        //add record sum quantity at last index
                        WorkOrderReportDTO totalRecord = new WorkOrderReportDTO();
                        totalRecord.WorkOrderUD = "Total";
                        totalRecord.WorkOrderID = -2;
                        totalRecord.ProductionItemDTOs = new List<ProductionItemDTO>();
                        data.Add(totalRecord);

                        foreach (DTO.WorkOrderReportHeaderDTO workOrderReportHeaderDTO in editFormData.WorkOrderReportHeaderDTOs)
                        {                            
                            for (int i = 0; i < 4; i++)
                            {
                                WorkOrderReportContentDTO workOrderReportContentDTO = new WorkOrderReportContentDTO();
                                workOrderReportContentDTO.ProductionItemID = 0;  //0: sum all, -1: sum, > 0: detail
                                workOrderReportContentDTO.WorkCenterID = workOrderReportHeaderDTO.WorkCenterID.Value;
                                workOrderReportContentDTO.WorkOrderID = -2; //-2: sum all
                                workOrderReportContentDTO.Type = i;

                                //lấy ra danh sách workorder cha
                                List<WorkOrderReportContentDTO> temp = editFormData.WorkOrderReportContentDTOs.Where(s => s.WorkCenterID == workOrderReportHeaderDTO.WorkCenterID && s.WorkOrderID > 0).ToList();
                                workOrderReportContentDTO.Quantity = temp.Where(s => s.Type == i).Sum(s => s.Quantity);

                                editFormData.WorkOrderReportContentDTOs.Add(workOrderReportContentDTO);
                            }
                        }

                        editFormData.WorkOrderReportDTOs = data;                       
                    }
                    else
                    {
                        editFormData.Data = new DTO.WorkOrderDTO();
                        editFormData.Data.WorkOrderStatusID = 1; //1: Open
                        editFormData.BOMData = new DTO.BOMDTO();
                    }

                    Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                    editFormData.WorkOrderStatuses = support_factory.GetWorkOrderStatus();
                    editFormData.WorkOrderTypes = support_factory.GetWorkOrderType();
                    editFormData.SupportOPSequence = supportFactory.GetOPSequence(companyID);

                    //get sample product base on sample order in case workorder type is SAMPLE
                    editFormData.SampleProductDTOs = AutoMapper.Mapper.Map<List<WorkOrderMng_SampleProduct_View>, List<DTO.SampleProductDTO>>(context.WorkOrderMng_SampleProduct_View.Where(o => o.SampleOrderID == editFormData.Data.SampleOrderID).ToList());
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

        public List<DTO.ProductDTO> GetProduct(Hashtable filters, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WorkOrderEntities context = CreateContext())
                {
                    List<DTO.ProductDTO> result = new List<DTO.ProductDTO>();
                    string searchQuery = filters["searchQuery"].ToString();
                    var dbItems = context.WorkOrderMng_function_GetProduct(searchQuery).ToList();
                    return AutoMapper.Mapper.Map<List<WorkOrderMng_Product_View>, List<DTO.ProductDTO>>(dbItems);
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
                return new List<DTO.ProductDTO>();
            }
        }

        public List<DTO.SaleOrderDTO> GetSaleOrder(Hashtable filters, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WorkOrderEntities context = CreateContext())
                {
                    List<DTO.SaleOrderDTO> result = new List<DTO.SaleOrderDTO>();
                    string searchQuery = filters["searchQuery"].ToString();
                    int? clientID = Convert.ToInt32(filters["clientID"]);
                    var dbItems = context.WorkOrderMng_function_GetSaleOrder(searchQuery, clientID).ToList();
                    return AutoMapper.Mapper.Map<List<WorkOrderMng_SaleOrder_View>, List<DTO.SaleOrderDTO>>(dbItems);
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
                return new List<DTO.SaleOrderDTO>();
            }
        }

        public List<DTO.FactoryOrderDetailDTO> GetFactoryOrderDetail(int? userId, Hashtable filters, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WorkOrderEntities context = CreateContext())
                {
                    List<DTO.FactoryOrderDetailDTO> result = new List<DTO.FactoryOrderDetailDTO>();
                    int? clientID = Convert.ToInt32(filters["clientID"]);
                    int? saleOrderID = Convert.ToInt32(filters["saleOrderID"]);
                    int? modelID = null;
                    if (filters["modelID"] != null)
                    {
                        modelID = Convert.ToInt32(filters["modelID"]);
                    }
                    var dbItems = context.WorkOrderMng_function_GetFactoryOrderDetail(userId, clientID, saleOrderID, modelID).ToList();
                    return AutoMapper.Mapper.Map<List<WorkOrderMng_FactoryOrderDetail_View>, List<DTO.FactoryOrderDetailDTO>>(dbItems);
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
                return new List<DTO.FactoryOrderDetailDTO>();
            }
        }

        public int? SetWorkOrderStatus(int userId, Hashtable param, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Set status success !!!" };
            int? currentWorkOrderStatusID = null;
            try
            {
                int? workOrderID = null;
                int? workOrderStatusID = null;

                if (param["workOrderID"] != null) workOrderID = Convert.ToInt32(param["workOrderID"]);
                if (param["workOrderStatusID"] != null) workOrderStatusID = Convert.ToInt32(param["workOrderStatusID"]);

                using (WorkOrderEntities context = CreateContext())
                {
                    //get current work order status to validate
                    var dbWorkOrder = context.WorkOrder.Where(o => o.WorkOrderID == workOrderID).FirstOrDefault();
                    if (dbWorkOrder == null)
                    {
                        throw new Exception("Could not find work order");
                    }
                    currentWorkOrderStatusID = dbWorkOrder.WorkOrderStatusID;

                    //validate input status value
                    if (!workOrderStatusID.HasValue)
                    {
                        throw new Exception("Can not set empty status for work order !");
                    }
                    /*
                     *  WorkOrderStatusID :
                     *      1 : Open
                     *      5 : Confirm Frame
                     *      2 : Confimred
                     *      3 : Finished
                     *      4 : Cancel                     
                     */

                    if (workOrderStatusID == 5 || workOrderStatusID == 2 || workOrderStatusID == 3 || workOrderStatusID == 4)
                    {
                        if (!fw_factory.CanPerformAction(userId, moduleCode, Library.DTO.ModuleAction.CanApprove))
                        {
                            throw new Exception("You don't have permission to set status for work-order");
                        }
                    }

                    if (workOrderStatusID == 2 || workOrderStatusID == 3 || workOrderStatusID == 5)
                    {
                        if (dbWorkOrder.WorkOrderTypeID != 3 && dbWorkOrder.WorkOrderTypeID != 5)
                        {
                            var bom = context.BOM.Where(o => o.WorkOrderID == workOrderID && (!o.IsDeleted.HasValue || o.IsDeleted.Value == false)).FirstOrDefault();
                            if (bom == null)
                            {
                                throw new Exception("Work order has not been created BOM. You can not " + (workOrderStatusID == 2 || workOrderStatusID == 5 ? "confirm" : "finish"));
                            }
                        }
                    }

                    if (currentWorkOrderStatusID == 5) //current is confirm frame
                    {
                        if (workOrderStatusID == 1)
                        {
                            if (context.WorkOrderMng_function_CheckCreateDeliveryNoteAndReceivingNoteForWorkOrder(workOrderID).FirstOrDefault().Value > 0)
                            {
                                throw new Exception("Work order has been created receiving note & delivery note, you can not set to open status");
                            }

                            if (!fw_factory.CanPerformAction(userId, moduleCode, Library.DTO.ModuleAction.CanReset))
                            {
                                throw new Exception("You don't have permission to set open status after work-order has been confirmed");
                            }
                        }
                    }
                    else if (currentWorkOrderStatusID == 2)  // current is confirm
                    {
                        if (workOrderStatusID == 1 || workOrderStatusID == 5)
                        {
                            if (context.WorkOrderMng_function_CheckCreateDeliveryNoteAndReceivingNoteForWorkOrder(workOrderID).FirstOrDefault().Value > 0)
                            {
                                throw new Exception("Work order has been created receiving note & delivery note, you can not set to " + (workOrderStatusID == 1 ? "open" : "confirm frame") + " status");
                            }

                            if (!fw_factory.CanPerformAction(userId, moduleCode, Library.DTO.ModuleAction.CanReset))
                            {
                                throw new Exception("You don't have permission to set " + (workOrderStatusID == 1 ? "open" : "confirm frame") + " status after work-order has been confirmed");
                            }
                        }
                    }
                    else if (currentWorkOrderStatusID == 3) //current is finished
                    {
                        if (workOrderStatusID == 1)
                        {
                            throw new Exception("Work order has been finished, you can not set to open status");
                        }
                        else if (workOrderStatusID == 5)
                        {
                            throw new Exception("Work order has been finished, you can not set to confirm frame status");
                        }
                        else if (workOrderStatusID == 2)
                        {
                            throw new Exception("Work order has been finished, you can not set to confirm status");
                        }
                    }
                    else if (currentWorkOrderStatusID == 4) //current is cancel
                    {
                        if (workOrderStatusID == 1)
                        {
                            throw new Exception("Work order has been cancel, you can not set to open status");
                        }
                        else if (workOrderStatusID == 5)
                        {
                            throw new Exception("Work order has been cancel, you can not set to confirm frame status");
                        }
                        else if (workOrderStatusID == 2)
                        {
                            throw new Exception("Work order has been cancel, you can not set to confirm status");
                        }
                        else if (workOrderStatusID == 3)
                        {
                            throw new Exception("Work order has been cancel, you can not set to finish status");
                        }
                    }
                    if (dbWorkOrder.IsLoadCost != true)
                    {
                        if (workOrderStatusID == 2 || workOrderStatusID == 5)
                        {
                            try
                            {
                                context.WorkOrderMng_function_Insert_Production2WorkOrderCost(dbWorkOrder.WorkOrderID);
                                dbWorkOrder.IsLoadCost = true;
                            }
                            catch
                            { }
                        }
                    }
                    dbWorkOrder.WorkOrderStatusID = workOrderStatusID;
                    dbWorkOrder.SetStatusBy = userId;
                    dbWorkOrder.SetStatusDate = DateTime.Now;
                    context.SaveChanges();
                    return dbWorkOrder.WorkOrderStatusID;
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
                return currentWorkOrderStatusID;
            }
        }

        public bool ChangeOPSequence(int userId, Hashtable param, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Change OP Sequence success !!!" };
            try
            {
                int? workOrderID = null;
                int? opSequenceID = null;

                if (param["workOrderID"] != null) workOrderID = Convert.ToInt32(param["workOrderID"]);
                if (param["opSequenceID"] != null) opSequenceID = Convert.ToInt32(param["opSequenceID"]);

                if (!opSequenceID.HasValue)
                {
                    throw new Exception("Can not set to empty OPSequence");
                }
                using (WorkOrderEntities context = CreateContext())
                {
                    if (workOrderID > 0)
                    {
                        var bom = context.BOM.Where(o => o.WorkOrderID == workOrderID).FirstOrDefault();
                        if (bom != null)
                        {
                            context.WorkOrderMng_function_ChangeOPSequence(workOrderID, opSequenceID);
                        }
                        else
                        {
                            throw new Exception("WorkOrder did not create BOM");
                        }
                    }
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

        public List<DTO.SampleOrderDTO> GetSampleOrder(int? userID, Hashtable filters, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WorkOrderEntities context = CreateContext())
                {
                    List<DTO.SampleOrderDTO> result = new List<DTO.SampleOrderDTO>();
                    string searchQuery = filters["searchQuery"].ToString();
                    var dbItems = context.WorkOrderMng_function_GetSampleOrder(userID, searchQuery).ToList();
                    result = AutoMapper.Mapper.Map<List<WorkOrderMng_SampleOrder_View>, List<DTO.SampleOrderDTO>>(dbItems);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = ex.Message;
                notification.Type = Library.DTO.NotificationType.Error;
                return new List<DTO.SampleOrderDTO>();
            }
        }

        public List<DTO.PreOrderWorkOrderDTO> GetPreOrderWorkOrder(string workOrderUD, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                string workOrder = null;
                if (!string.IsNullOrEmpty(workOrderUD))
                {
                    workOrder = workOrderUD.ToString().Replace("'", "''");
                }
                using (WorkOrderEntities context = CreateContext())
                {
                    List<DTO.PreOrderWorkOrderDTO> result = new List<DTO.PreOrderWorkOrderDTO>();
                    var dbItems = context.WorkOrderMng_function_SearchListPreOrder(workOrder).ToList();
                    result = AutoMapper.Mapper.Map<List<WorkOrderMng_PreOrderWorkOrder_View>, List<DTO.PreOrderWorkOrderDTO>>(dbItems);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = ex.Message;
                notification.Type = Library.DTO.NotificationType.Error;
                return null;
            }
        }

        public List<DTO.WorkCenterDTO> GetWorkCenter(out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WorkOrderEntities context = CreateContext())
                {
                    List<DTO.WorkCenterDTO> workCenters = new List<DTO.WorkCenterDTO>();
                    workCenters = converter.DB2DTO_WorkCenter(context.SupportMng_WorkCenter_View.Where(s=>s.WorkCenterUD != "FINISHING").ToList());
                    return workCenters;
                }
            }
            catch (Exception ex)
            {
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = ex.Message;
                notification.Type = Library.DTO.NotificationType.Error;
                return new List<DTO.WorkCenterDTO>();
            }
        }

        public string GetProductionItemExportToExcelList(string workOrders, string workCenters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportWorkOrderDataObject ds = new ReportWorkOrderDataObject();

            //try to get data
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WorkOrderMng_function_ExportToExcelList", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@WorkOrderList", workOrders);
                adap.SelectCommand.Parameters.AddWithValue("@WorkCenterList", workCenters);
                adap.TableMappings.Add("Table", "ExportToExcelWorkOrder");
                adap.TableMappings.Add("Table1", "ExportToExcelBOM");
                adap.TableMappings.Add("Table2", "ExportToExcelBOMMaterial");
                adap.TableMappings.Add("Table3", "ProductionItemUnit");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ListWorkOrder");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return string.Empty;
            }
        }

        public int? OpenWorkOrderStatus(int userId, Hashtable param, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Set status to Confirm All Success !!!" };
            try
            {
                int? workOrderID = null;
                if (param["workOrderID"] != null) workOrderID = Convert.ToInt32(param["workOrderID"]);

                using (WorkOrderEntities context = CreateContext())
                {
                    //get current work order status to validate
                    var dbWorkOrder = context.WorkOrder.Where(o => o.WorkOrderID == workOrderID).FirstOrDefault();
                    if (dbWorkOrder == null)
                    {
                        throw new Exception("Could not find work order");
                    }
                    if (dbWorkOrder.WorkOrderStatusID == 3)
                    {
                        dbWorkOrder.WorkOrderStatusID = 2;
                    }
                    else
                    {
                        throw new Exception("excess action !");
                    }
                    dbWorkOrder.SetStatusBy = userId;
                    dbWorkOrder.SetStatusDate = DateTime.Now;
                    context.SaveChanges();
                    return dbWorkOrder.WorkOrderStatusID;
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
                return null;
            }
        }

        public List<DTO.OPSequenceDetailDTO> GetOPSequenceDetails(int? opSequenceID, int? preWorkOrderID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.OPSequenceDetailDTO> data = new List<DTO.OPSequenceDetailDTO>();
            int? preOrderWorkOrderID = preWorkOrderID;

            try
            {
                using (var context = CreateContext())
                {
                    List<WorkOrderMng_WorkOrderOPSequence_View> preWorkOrderOPSequences = new List<WorkOrderMng_WorkOrderOPSequence_View>();

                    while (preOrderWorkOrderID.HasValue)
                    {
                        List<WorkOrderMng_WorkOrderOPSequence_View> preOrderOPSequences = new List<WorkOrderMng_WorkOrderOPSequence_View>();
                        preOrderOPSequences = GetOPSequenceDetailByPreOrderWorkOrder(preOrderWorkOrderID, context);

                        foreach (var item in preOrderOPSequences.ToList())
                        {
                            preWorkOrderOPSequences.Add(item);
                        }

                        var workOrder = context.WorkOrder.FirstOrDefault(o => o.WorkOrderID == preOrderWorkOrderID);
                        if (workOrder != null)
                        {
                            preOrderWorkOrderID = workOrder.PreOrderWorkOrderID;
                        }
                    }

                    var dbItem = context.WorkOrderMng_OPSequenceDetail_View.Where(o => o.OPSequenceID == opSequenceID && o.WorkCenterID != 10);
                    data = converter.DB2DTO_OPSequenceDetail(dbItem.ToList());

                    foreach (var preWorkOrderOPSequence in preWorkOrderOPSequences.OrderBy(o => o.WorkOrderID).OrderBy(o => o.WorkCenterID).ToList())
                    {
                        foreach (var opSequenceDetail in data.ToList())
                        {
                            if (opSequenceDetail.OPSequenceDetailID == preWorkOrderOPSequence.OPSequenceDetailID)
                            {
                                opSequenceDetail.IsActived = preWorkOrderOPSequence.IsActived;
                                opSequenceDetail.IsDisabled = (preWorkOrderID.HasValue);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<WorkOrderMng_WorkOrderOPSequence_View> GetOPSequenceDetailByPreOrderWorkOrder(int? preOrderWorkOrderID, WorkOrderEntities context)
        {
            var dbItem = context.WorkOrderMng_WorkOrderOPSequence_View.Where(o => o.WorkOrderID == preOrderWorkOrderID && o.WorkCenterID != 10).OrderBy(o => o.PrimaryID);
            return dbItem.ToList();
        }

        public bool IsValidationWorkOrder(int workOrderID, int cntWorkOrderOPSequenceInDTO, WorkOrderEntities context)
        {
            if (workOrderID > 0)
            {
                int cntPreOrder = context.WorkOrder.Where(o => o.PreOrderWorkOrderID == workOrderID).Count();
                int cntWorkOrderOPSequenceInDB = context.WorkOrderOPSequence.Where(o => o.WorkOrderID == workOrderID).Count();

                if ((cntPreOrder > 0) && (cntWorkOrderOPSequenceInDTO != cntWorkOrderOPSequenceInDB))
                {
                    return false;
                }
            }

            return true;
        }

        public List<DTO.PreOrderWorkOrderManagement> GetPreOrderWorkOrderManagements(int preOrderWorkOrderID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.PreOrderWorkOrderManagement> data = new List<DTO.PreOrderWorkOrderManagement>();

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_PreOrderWorkOrderManagement(context.WorkOrderMng_function_PreOrderWorkOrderManangement(preOrderWorkOrderID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.WorkOrderBaseOnManagement> GetWorkOrderBaseOnManagements(int workOrderID, int preOrderWorkOrderID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.WorkOrderBaseOnManagement> data = new List<DTO.WorkOrderBaseOnManagement>();

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_WorkOrderBaseOnManagement(context.WorkOrderMng_function_WorkOrderBaseOnManagement(workOrderID, preOrderWorkOrderID).ToList());

                    foreach (var item in data.ToList())
                    {
                        var dbItem = context.WorkOrderMng_function_PreOrderWorkOrderBaseOnManagement(item.WorkOrderID, preOrderWorkOrderID).ToList();
                        item.PreOrderWorkOrderBaseOnManagements = converter.DB2DTO_PreOrderWorkOrderBaseOnManagement(dbItem);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public bool UpdateTransferPre2Work(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                DTO.TransferWorkOrderDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["TransferWorkOrder"]).ToObject<DTO.TransferWorkOrderDTO>();

                using (var context = CreateContext())
                {
                    TransferWorkOrder dbItem;

                    if (dtoItem.TransferWorkOrderID <= 0)
                    {
                        dbItem = new TransferWorkOrder();
                        context.TransferWorkOrder.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.TransferWorkOrder.FirstOrDefault(o => o.TransferWorkOrderID == dtoItem.TransferWorkOrderID);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }

                    // TransferWorkOrder
                    if (dtoItem.TransferWorkOrderID == 0)
                    {
                        dbItem.TransferWorkOrderDate = DateTime.Now;
                        dbItem.WorkOrderID = dtoItem.WorkOrderID;
                        dbItem.CreatedBy = userID;
                        dbItem.CreatedDate = DateTime.Now;
                        int increaseID = context.TransferWorkOrder.Where(o => DbFunctions.TruncateTime(o.TransferWorkOrderDate) == (DbFunctions.TruncateTime(DateTime.Now))).Count();
                        dbItem.TransferWorkOrderUD = "PRE2WO-" + DateTime.Now.ToString("yyMMdd") + "-" + Convert.ToString(increaseID + 1).PadLeft(3, '0');
                    }
                    else
                    {
                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = DateTime.Now;
                    }

                    // TransferWorkOrderDetail
                    string errorComponents = string.Empty;
                    foreach (var dtoItemDetail in dtoItem.TransferWorkOrderDetails.ToList())
                    {
                        TransferWorkOrderDetail dbItemDetail;

                        if (dtoItemDetail.TransferWorkOrderDetailID <= 0)
                        {
                            dbItemDetail = new TransferWorkOrderDetail();
                            dbItem.TransferWorkOrderDetail.Add(dbItemDetail);
                        }
                        else
                        {
                            dbItemDetail = context.TransferWorkOrderDetail.FirstOrDefault(o => o.TransferWorkOrderDetailID == dtoItemDetail.TransferWorkOrderDetailID);
                        }

                        // Comparision remain of pre-order and remain of work-order with transfer quantity
                        if (dtoItemDetail.TransferWorkOrderDetailID <= 0)
                        {
                            if (dtoItemDetail.TransferQuantity == 0 || dtoItemDetail.PreOrderWorkOrderQuantity < dtoItemDetail.TransferQuantity || dtoItemDetail.WorkOrderQuantity < dtoItemDetail.TransferQuantity)
                            {
                                if (!string.IsNullOrEmpty(errorComponents))
                                {
                                    errorComponents += ", ";
                                }

                                errorComponents += dtoItemDetail.ProductionItemUD;
                            }
                        }
                        else
                        {
                            var currentItemDetail = context.WorkOrderMng_TransferWorkOrderDetail_View.FirstOrDefault(o => o.TransferWorkOrderDetailID == dtoItemDetail.TransferWorkOrderDetailID);
                            if (dtoItemDetail.TransferQuantity == 0 || currentItemDetail.PreOrderWorkOrderQuantity < dtoItemDetail.TransferQuantity || currentItemDetail.WorkOrderQuantity < dtoItemDetail.TransferQuantity)
                            {
                                if (!string.IsNullOrEmpty(errorComponents))
                                {
                                    errorComponents += ", ";
                                }

                                errorComponents += dtoItemDetail.ProductionItemUD;
                            }
                        }

                        if (!string.IsNullOrEmpty(errorComponents))
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Error data in component: " + errorComponents;
                            return false;
                        }

                        if (dbItemDetail != null)
                        {
                            dbItemDetail.PreOrderWorkOrderID = dtoItemDetail.PreOrderWorkOrderID;
                            dbItemDetail.ProductionItemID = dtoItemDetail.ProductionItemID;
                            dbItemDetail.TransferQuantity = dtoItemDetail.TransferQuantity;
                        }
                    }

                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
                return true;
            }
        }

        public List<DTO.HistoryTransferPreOrderToWorkOrderDTO> GetHistoryTransferPreOrderToWorkOrders(int workOrderID, int preOrderWorkOrderID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.HistoryTransferPreOrderToWorkOrderDTO> data = new List<DTO.HistoryTransferPreOrderToWorkOrderDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_HistoryTransferPreOrderToWorkOrder(context.WorkOrderMng_function_HistoryTransferPreOrderToWorkOrder(workOrderID, preOrderWorkOrderID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.TransferWorkOrderDTO GetTransferWorkOrder(int id, int workOrderID, int preOrderWorkOrderID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.TransferWorkOrderDTO data = new DTO.TransferWorkOrderDTO();

            try
            {
                using (var context = CreateContext())
                {
                    if (id == 0)
                    {
                        var baseWorkOrder = context.WorkOrderMng_function_TransferWorkOrder(workOrderID).FirstOrDefault();
                        var basePreOrderWorkOrder = context.WorkOrderMng_function_TransferPreOrderWorkOrder(workOrderID, preOrderWorkOrderID).ToList();

                        if (baseWorkOrder != null)
                        {
                            data.WorkOrderID = workOrderID;
                            data.WorkOrderUD = baseWorkOrder.WorkOrderUD;
                            data.ProformaInvoiceNo = baseWorkOrder.ProformaInvoiceNo;
                            data.ProductionQuantity = baseWorkOrder.Quantity;
                            data.LDS = baseWorkOrder.LDS.HasValue ? baseWorkOrder.LDS.Value.ToString("dd/MM/yyyy") : null;
                        }

                        int detailItemID = -1;
                        foreach (var item in basePreOrderWorkOrder.ToList())
                        {
                            DTO.TransferWorkOrderDetailDTO detailItem = new DTO.TransferWorkOrderDetailDTO();
                            detailItem.TransferWorkOrderDetailID = detailItemID;
                            detailItem.ProductionItemID = item.ProductionItemID;
                            detailItem.ProductionItemUD = item.ProductionItemUD;
                            detailItem.PreOrderWorkOrderQuantity = item.PreOrderRemainQnt;
                            detailItem.WorkOrderQuantity = item.WorkOrderRemainQnt;
                            detailItem.PreOrderWorkOrderID = preOrderWorkOrderID;

                            data.TransferWorkOrderDetails.Add(detailItem);
                            detailItemID--;
                        }
                    }
                    else
                    {
                        var dbItem = context.WorkOrderMng_TransferWorkOrder_View.FirstOrDefault(o => o.TransferWorkOrderID == id);

                        if (dbItem == null)
                        {
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not find data!";
                            return data;
                        }

                        data = AutoMapper.Mapper.Map<WorkOrderMng_TransferWorkOrder_View, DTO.TransferWorkOrderDTO>(dbItem);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        private void GetWorkOrderChild(ref List<WorkOrderReportDTO> param, int index)
        {            
            using (var context = CreateContext())
            {
                List<WorkOrderReportDTO> listAdd = new List<WorkOrderReportDTO>();               
                foreach (var item in param)
                { 
                    if(param.IndexOf(item) >= index)
                    {
                        List<WorkOrderReportDTO> lstChild = new List<WorkOrderReportDTO>();
                        lstChild = converter.DB2DTO_WorkOrderReportDTOs(context.WorkOrderMng_function_WorkOrderChild(item.WorkOrderID).ToList());
                        listAdd.AddRange(lstChild);
                    }                   
                }
                int countListAdd = listAdd.Count;
                param.AddRange(listAdd);
                if (param.Count - 1 > index)
                {                   
                    GetWorkOrderChild(ref param, param.Count - countListAdd);
                }                                
            }                
        }
    }
}
