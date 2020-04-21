using Library.DTO;
using System;
using System.Collections;

namespace Module.PurchaseQuotationMng
{
    public class Executor : Library.Base.IExecutor
    {
        BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.ApproveData(userId, id, ref dtoItem, out notification);
        }

        public object CustomFunction(int userId, string identifier, Hashtable filters, out Notification notification)
        {
            switch (identifier.Trim())
            {
                #region ** Code developer Luu Nhut **
                #endregion

                #region ** Code developer Truong Son **

                case "getDefaultPrice":
                    return bll.GetInitDefaultPrice(filters, out notification);

                case "PreparingDefaultPriceData":
                    return bll.PreparingDefaultPriceData(userId, filters, out notification);

                case "SetDefaultPrice":
                    return bll.SetDefaultPrice(userId, filters, out notification);

                case "GetProductionItemDefaultPrice":
                    return bll.GetProductionItemDefaultPrice(userId, filters["searchQuery"].ToString(), out notification);

                #endregion

                case "GetInitDataDefaultPrice":
                    return bll.GetInitDataDefaultPrice(userId, out notification);

                case "exportexcel":
                    return bll.GetContentPurchasingPriceFactory(userId, filters, out notification);

                case "GetInitForm":
                    return bll.GetInitForm(userId, out notification);

                case "GetMaterial":
                    return bll.GetMaterial(userId, filters, out notification);

                case "GetPurchasingPriceFactory":
                    int pageSize = (filters.ContainsKey("pageSize") && filters["pageSize"] != null && !string.IsNullOrEmpty(filters["pageSize"].ToString().Trim())) ? Convert.ToInt32(filters["pageSize"].ToString().Trim()) : 0;
                    int pageIndex = (filters.ContainsKey("pageIndex") && filters["pageIndex"] != null && !string.IsNullOrEmpty(filters["pageIndex"].ToString().Trim())) ? Convert.ToInt32(filters["pageIndex"].ToString().Trim()) : 0;
                    string orderBy = (filters.ContainsKey("orderBy") && filters["orderBy"] != null && !string.IsNullOrEmpty(filters["orderBy"].ToString().Trim())) ? filters["orderBy"].ToString().Trim() : null;
                    string orderDirection = (filters.ContainsKey("orderDirection") && filters["orderDirection"] != null && !string.IsNullOrEmpty(filters["orderDirection"].ToString().Trim())) ? filters["orderDirection"].ToString().Trim() : null;
                    int totalRows = (filters.ContainsKey("totalRows") && filters["totalRows"] != null && !string.IsNullOrEmpty(filters["totalRows"].ToString().Trim())) ? Convert.ToInt32(filters["totalRows"].ToString().Trim()) : 0;
                    return bll.GetPurchasingPriceFactory(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);

                case "cancel":                  
                    object dtoItem = filters["dtoItem"];
                    return bll.Cancel(userId, Convert.ToInt32(filters["id"]), ref dtoItem, out notification);

                default:
                    notification = new Library.DTO.Notification();
                    notification.Type = NotificationType.Error;
                    notification.Message = "Custom function's identifier not matched";
                    break;
            }
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.DeleteData(id, out notification);
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            return bll.GetData(userId, id, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }
    }
}
