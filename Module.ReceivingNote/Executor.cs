using System;

namespace Module.ReceivingNote
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.GetData(userId, id, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            string workOrderIDs = null;
            string searchQuery = null;
            string receivingNoteDate = null;
            int? workCenterID = null;
            int? branchID = null;

            int receivingNoteID = 0;

            switch (identifier.Trim())
            {
                case "GetData":
                    return bll.GetData(userId, filters, out notification);

                case "GetReceivingNotePrintout":
                    receivingNoteID = Convert.ToInt32(filters["receivingNoteID"]);
                    return bll.GetReceivingNotePrintout(userId, receivingNoteID, out notification);

                case "getexcelbarcode":
                    return bll.GetExportBarcode(userId, filters, out notification);

                case "GetReceivingNotePrintoutHTML":
                    receivingNoteID = Convert.ToInt32(filters["receivingNoteID"]);
                    return bll.GetReceivingNotePrintoutHTML(userId, receivingNoteID, out notification);

                case "getsubsupplier":
                    if (!filters.ContainsKey("searchQuery"))
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Unknown matched!";

                        return null;
                    }
                    return bll.GetSubSupplier(userId, filters["searchQuery"].ToString(), out notification);

                case "DeleteWithPermission":
                    if (!filters.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.DeleteWithPermission(Convert.ToInt32(filters["id"]), Convert.ToInt32(filters["createdBy"]), userId, out notification);

                case "GetPurchaseOrder":
                    string purchaseOrderUD = filters["purchaseOrderUD"].ToString();
                    return bll.GetPurchaseOrder(userId, purchaseOrderUD, out notification);

                case "GetProductionItem":
                    searchQuery = (filters["search-query"] != null) ? filters["search-query"].ToString() : null;
                    receivingNoteDate = (filters["receivingNoteDate"] != null) ? filters["receivingNoteDate"].ToString() : null;

                    workCenterID = (filters["workCenterID"] != null) ? (int?)Convert.ToInt32(filters["workCenterID"]) : null;
                    workOrderIDs = (filters["workOrderIDs"] != null) ? filters["workOrderIDs"].ToString() : null;

                    branchID = (filters["branchID"] != null) ? (int?)Convert.ToInt32(filters["branchID"]) : null;

                    return bll.GetProductionItem(searchQuery, branchID, receivingNoteDate, workOrderIDs, workCenterID, userId, out notification);

                case "GetBOM":
                    workOrderIDs = filters["work-order-ids"].ToString();
                    workCenterID = (filters["workCenterID"] != null) ? (int?)Convert.ToInt32(filters["workCenterID"]) : null;
                    return bll.GetBOM(userId, Convert.ToInt32(filters["branchID"]), workOrderIDs, filters["receivingNoteDate"].ToString(), workCenterID, out notification);

                case "GetListReceivingNote":
                    return bll.GetReceivingNoteExportToExcelList(userId, filters, out notification);

                case "GetWorkOrderItems":
                    return bll.GetWorkOrderItems(userId, Convert.ToInt32(filters["productionItemID"]), out notification);

                case "GetPurchaseOrderDetail":
                    return bll.GetPurchaseOrderDetail(userId, Convert.ToInt32(filters["PurchaseOrderID"]), Convert.ToInt32(filters["BranchID"]), filters["ReceivingNoteDate"].ToString(), out notification);

                case "SetContentDetail":
                    return bll.SetContentDetail(userId, Convert.ToInt32(filters["ProductionItemID"]), Convert.ToInt32(filters["FactoryWarehouseID"]), out notification);

                case "getBOMProductionItem":
                    workOrderIDs = (filters["WorkOrderIDs"] != null) ? filters["WorkOrderIDs"].ToString() : null;
                    searchQuery = (filters["ProductionItemUD"] != null) ? filters["ProductionItemUD"].ToString() : null;
                    receivingNoteDate = (filters["ReceivingNoteDate"] != null) ? filters["ReceivingNoteDate"].ToString() : null;
                    workCenterID = (filters["WorkCenterID"] != null) ? (int?)Convert.ToInt32(filters["WorkCenterID"]) : null;
                    branchID = (filters["BranchID"] != null) ? (int?)Convert.ToInt32(filters["BranchID"]) : null;

                    return bll.GetBOMProductionItem(userId, searchQuery, workOrderIDs, workCenterID, branchID, receivingNoteDate, out notification);

                case "getSearchDetail":
                    return bll.GetSearchDetail(userId, out notification);

                case "script-update-factoryproductionmonitiring":
                    return bll.UpdateFactoryProductionMonitiring(userId, out notification);

                case "get-factory-sale-order":
                    searchQuery = filters["searchQuery"].ToString();
                    return bll.GetFactorySaleOrder(searchQuery, out notification);

                case "get-factory-sale-order-detail":
                    int factorySaleOrderID = Convert.ToInt32(filters["factorySaleOrderID"]);
                    return bll.GetFactorySaleOrderDetail(factorySaleOrderID, filters["receivingNoteDate"].ToString(), out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
