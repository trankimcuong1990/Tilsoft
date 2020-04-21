using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.PaymentNoteMng
{
    public class Executor: Library.Base.IExecutor2
    {
        BLL bll = new BLL();
        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            int supplierID = 0;
            string year = "";
            string currency = "";
            switch (identifier.ToLower())
            {
                case "query-supplier":
                    return bll.QuyerySupplier(input, out notification);

                case "search-purchase-invoice":
                    int pageSize = Convert.ToInt32(input["pageSize"]);
                    int pageIndex = Convert.ToInt32(input["pageIndex"]);
                    string sortedBy = input["sortedBy"].ToString();
                    string sortedDirection = input["sortedDirection"].ToString();
                    return bll.SearchPurchasingInvoice(userId, input, pageSize, pageIndex, sortedBy, sortedDirection, out notification);

                case "set-status":
                    int paymentNoteID = Convert.ToInt32(input["paymentNoteID"]);
                    int statusID = Convert.ToInt32(input["statusID"]);
                    int paymentNoteTypeID = Convert.ToInt32(input["paymentNoteTypeID"]);
                    return bll.SetStatus(userId, paymentNoteID, statusID, paymentNoteTypeID, out notification);

                case "get-paymentnote-printout":
                    int paymentNoteprintID = Convert.ToInt32(input["paymentNoteID"]);
                    return bll.GetPaymentPrintout(userId, paymentNoteprintID, out notification);

                case "get-paymentbydeposit":
                    supplierID = Convert.ToInt32(input["supplierID"]);
                    year = input["year"].ToString();
                    currency = input["currency"].ToString();
                    return bll.GetPaymentByDeposit(supplierID, year, currency, out notification);

                case "get-totaldeposit":
                    supplierID = Convert.ToInt32(input["supplierID"]);
                    year = input["year"].ToString();
                    currency = input["currency"].ToString();
                    return bll.GetTotalDeposit(supplierID, year, currency, out notification);

                case "search-po":
                    int pageSize_1 = Convert.ToInt32(input["pageSize"]);
                    int pageIndex_1 = Convert.ToInt32(input["pageIndex"]);
                    string sortedBy_1 = input["sortedBy"].ToString();
                    string sortedDirection_1 = input["sortedDirection"].ToString();
                    return bll.SearchPO(userId, input, pageSize_1, pageIndex_1, sortedBy_1, sortedDirection_1, out notification);

                case "get-po-from-invoice":
                    int purchaseInvoiceID = Convert.ToInt32(input["purchaseInvoiceID"]);
                    return bll.GetPOFromInvoice(purchaseInvoiceID, out notification);

                default:
                    break;
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }


        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }


        public object GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            return bll.GetData(userId, id, param, out notification);
        }


        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }


        public object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetSearchFilter(int userId, out Notification notification)
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
