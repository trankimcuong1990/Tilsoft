using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.PurchaseOrderMng
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

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            switch (identifier.Trim())
            {               
                case "getPurchaseRequest":
                    return bll.GetPurchaseRequest(userId, filters, out notification);

                case "getPurchaseOrderDetailByPurchaseRequestID":
                    return bll.GetPurchaseOrderDetailByPurchaseRequestID(userId, filters, out notification);
                case "getexcelreport":
                    if (!filters.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    return bll.GetExcelReportData(userId, Convert.ToInt32(filters["id"]), out notification);

                case "GetPurchaseQuotationDetail":
                    int factoryRawMaterialID1 = Convert.ToInt32(filters["factoryRawMaterialID"]);
                    System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
                    DateTime purchaseOrderDate;
                    if (!filters.ContainsKey("purchaseOrderDate") || filters["purchaseOrderDate"] == null || string.IsNullOrEmpty(filters["purchaseOrderDate"].ToString()))
                    {
                        notification.Message = "Unknow order date!";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    else
                    {
                        if (DateTime.TryParse(filters["purchaseOrderDate"].ToString(), nl, System.Globalization.DateTimeStyles.None, out purchaseOrderDate))
                        {
                            return bll.GetPurchaseQuotationDetail(userId, factoryRawMaterialID1, purchaseOrderDate, out notification);
                        }
                    }
                    notification.Message = "Unknow order date!";
                    notification.Type = Library.DTO.NotificationType.Error;
                    return null;

                case "GetHTMLReportData":
                    return bll.GetHTMLReportData(userId, Convert.ToInt32(filters["purchaseOrderID"]), out notification);

                case "GetPurchaseOrderPrintout":
                    int purchaseOrderID = Convert.ToInt32(filters["purchaseOrderID"]);
                    return bll.GetPurchaseOrderPrintout(userId, purchaseOrderID, out notification);

                case "GetSupplierPaymentTerm":
                    int factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                    return bll.GetSupplierPaymentTerm(userId, factoryRawMaterialID, out notification);

                case "cancel":
                    if (!filters.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    object dtoItem = filters["dtoItem"];
                    return bll.Cancel(userId, Convert.ToInt32(filters["id"]), ref dtoItem, out notification);

                case "finish":
                    if (!filters.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    object dtoItemFinish = filters["dtoItem"];
                    return bll.Finish(userId, Convert.ToInt32(filters["id"]), ref dtoItemFinish, out notification);

                case "revise":
                    if (!filters.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    object dtoItemRevise = filters["dtoItem"];
                    return bll.Revise(userId, Convert.ToInt32(filters["id"]), ref dtoItemRevise, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
