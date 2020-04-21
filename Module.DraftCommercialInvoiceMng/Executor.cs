using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.DraftCommercialInvoiceMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.Confirm(userId, id, ref dtoItem, out notification);
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            string temp = "";
            string proformaInvoiceNo = (input.ContainsKey("proformaInvoiceNo") && input["proformaInvoiceNo"] != null && !string.IsNullOrEmpty(input["proformaInvoiceNo"].ToString()) ? Convert.ToString(input["proformaInvoiceNo"]) : temp);
            string articleCode = (input.ContainsKey("articleCode") && input["articleCode"] != null && !string.IsNullOrEmpty(input["articleCode"].ToString()) ? Convert.ToString(input["articleCode"]) : temp);
            switch (identifier.Trim())
            {
                case "getClient":
                    return bll.GetSupportClient(userId, input["client"].ToString(), out notification);

                case "getsaleorder":
                    return bll.GetSaleOrder(Convert.ToInt32(input["clientID"]), Convert.ToInt32(input["checkSelect"]), input["season"].ToString(), proformaInvoiceNo, articleCode, out notification);

                case "getOverView":
                    return bll.GetOverView(userId, Convert.ToInt32(input["id"]), out notification);

                case "getDraftInvoicePrintoutData":
                    return bll.GetDraftInvoicePrintoutData(userId, Convert.ToInt32(input["id"]), out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
