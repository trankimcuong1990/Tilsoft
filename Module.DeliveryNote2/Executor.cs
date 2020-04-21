using System;

namespace Module.DeliveryNote2
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
            throw new NotImplementedException();
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

            string deliveryNoteDate = null;

            switch (identifier.Trim())
            {
                case "GetData":
                    return bll.GetData(userId, filters, out notification);

                case "GetDataBranch":
                    return bll.GetData(userId, Convert.ToInt32(filters["id"]), Convert.ToInt32(filters["branchID"]), out notification);

                case "GetDeliveryNotePrintout":
                    int deliveryNoteID = Convert.ToInt32(filters["deliveryNoteID"]);
                    return bll.GetDeliveryNotePrintout(userId, deliveryNoteID, out notification);
                case "GetDeliveryNotePrintoutHTML":
                    int deliveryNoteID1 = Convert.ToInt32(filters["deliveryNoteID"]);
                    return bll.GetDeliveryNotePrintoutHTML(userId, deliveryNoteID1, out notification);
                case "DeleteWithPermission":
                    if (!filters.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }

                    return bll.DeleteWithPermission(Convert.ToInt32(filters["id"]), Convert.ToInt32(filters["createdBy"]), userId, out notification);
                case "GetOverView":
                    if (!filters.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    int id = Convert.ToInt32(filters["id"]);
                    return bll.GetFormView(userId, id, out notification);

                case "GetProductionItem":
                    string searchQuery = filters["search-query"].ToString();
                    string deliveryNoteDateStr = filters["deliveryNoteDate"].ToString();
                    int branchID = Convert.ToInt32(filters["branchID"]);
                    return bll.GetProductionItem(searchQuery, userId, deliveryNoteDateStr, branchID, out notification);

                case "GetBOM":
                    string workOrderIDs = filters["workOrderIDs"].ToString();

                    // Use delivery note date to get Unit Price
                    deliveryNoteDate = filters.ContainsKey("DeliveryNoteDate") && filters["DeliveryNoteDate"] != null ? filters["DeliveryNoteDate"].ToString() : null;
                    if (string.IsNullOrEmpty(deliveryNoteDate))
                    {
                        deliveryNoteDate = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    return bll.GetBOM(workOrderIDs, Convert.ToInt32(filters["branchID"]), deliveryNoteDate, out notification);

                case "GetFactorySaleOrder":
                    return bll.GetFactorySaleOrder(userId, filters["factorySaleOrderUD"].ToString(), out notification);

                case "GetListDeliveryNote":
                    return bll.GetDeliveryNoteExportToExcelList(userId, filters, out notification);

                case "getSearchDetail":
                    return bll.GetSearchDetail(userId, out notification);

                case "purchaseorderquicksearch":
                    return bll.PurchaseOrderQuickseach(userId, filters["PurchaseOrderUD"].ToString(), Convert.ToInt32(filters["BranchID"]), out notification);

                case "purchaseorderdetailquicksearch":
                    return bll.PurchaseOrderDetailQuicksearch(userId, Convert.ToInt32(filters["PurchaseOrderID"]), Convert.ToInt32(filters["BranchID"]), filters["DeliveryNoteDate"].ToString(), out notification);

                case "factorysaleorderdetailquicksearch":
                    return bll.FactorySaleOrderDetailQuicksearch(userId, Convert.ToInt32(filters["FactorySaleOrderID"]), Convert.ToInt32(filters["BranchID"]), filters["DeliveryNoteDate"].ToString(), out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
