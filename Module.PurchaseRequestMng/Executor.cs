using System;

namespace Module.PurchaseRequestMng
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int id;
            int? branchID = null;
            string workOrderIDs = null;
            string searchItem = null;
            string productionItemIDs = null;
            string stickGroupdIDs = null;

            switch (identifier.Trim())
            {
                case "GetData":
                    id = Convert.ToInt32(filters["id"]);
                    return bll.GetData(userId, id, filters, out notification);

                case "GetRequestingItem":
                    return bll.GetRequestingItem(userId, out notification);

                case "GetSearchFilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "ApprovePrice":
                    int purchaseRequestDetailID = Convert.ToInt32(filters["purchaseRequestDetailID"]);
                    int purchaseQuotationDetailID = Convert.ToInt32(filters["purchaseQuotationDetailID"]);
                    decimal? approvedQnt = Convert.ToInt32(filters["approvedQnt"]);
                    return bll.ApprovePrice(userId, purchaseRequestDetailID, purchaseQuotationDetailID, approvedQnt, out notification);

                case "GetProductionItemBaseOn":
                    if (filters.ContainsKey("branchID") && filters["branchID"] != null && !string.IsNullOrEmpty(filters["branchID"].ToString()))
                    {
                        branchID = Library.Helper.ConvertStringToInt(filters["branchID"].ToString());
                    }

                    if (filters.ContainsKey("workOrderIDs") && filters["workOrderIDs"] != null && !string.IsNullOrEmpty(filters["workOrderIDs"].ToString()))
                    {
                        workOrderIDs = filters["workOrderIDs"].ToString();
                    }

                    if (filters.ContainsKey("searchItem") && filters["searchItem"] != null && !string.IsNullOrEmpty(filters["searchItem"].ToString()))
                    {
                        searchItem = filters["searchItem"].ToString();
                    }

                    if (string.IsNullOrEmpty(searchItem))
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Please input string to search item valid with condition!";
                        return null;
                    }

                    return bll.GetProductionItemBaseOn(userId, branchID, workOrderIDs, searchItem, out notification);

                case "ReloadMaterialNotFilterByWorkOrderAndMaterialGroup":
                    if (filters.ContainsKey("branchID") && filters["branchID"] != null && !string.IsNullOrEmpty(filters["branchID"].ToString()))
                    {
                        branchID = Library.Helper.ConvertStringToInt(filters["branchID"].ToString());
                    }

                    if (filters.ContainsKey("workOrderIDs") && filters["workOrderIDs"] != null && !string.IsNullOrEmpty(filters["workOrderIDs"].ToString()))
                    {
                        workOrderIDs = filters["workOrderIDs"].ToString();
                    }

                    if (filters.ContainsKey("productionItemIDs") && filters["productionItemIDs"] != null && !string.IsNullOrEmpty(filters["productionItemIDs"].ToString()))
                    {
                        productionItemIDs = filters["productionItemIDs"].ToString();
                    }

                    if (filters.ContainsKey("stickGroupIDs") && filters["stickGroupIDs"] != null && !string.IsNullOrEmpty(filters["stickGroupIDs"].ToString()))
                    {
                        stickGroupdIDs = filters["stickGroupIDs"].ToString();
                    }

                    return bll.ReloadItemIsSetGroup(userId, branchID, productionItemIDs, workOrderIDs, stickGroupdIDs, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
