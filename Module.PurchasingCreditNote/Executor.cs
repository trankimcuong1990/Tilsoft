using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingCreditNote
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetPurchasingCreditNoteSearchResult(userId,filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
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
            return bll.UpdatePurchasingCreditNote(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return bll.GetCreditNoteType(out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int purchasingCreditNoteID;
            int? creditNoteType;
            int? purchasingInvoiceID;
            int? supplierID;
            switch (identifier.Trim())
            {
                case "GetEditData":
                    purchasingCreditNoteID = (int)filters["purchasingCreditNoteID"];
                    creditNoteType = (int?)filters["creditNoteType"];
                    purchasingInvoiceID = (int?)filters["purchasingInvoiceID"];
                    supplierID = (int?)filters["supplierID"];
                    return bll.GetEditData(userId, purchasingCreditNoteID, creditNoteType, purchasingInvoiceID, supplierID, out notification);

                case "GetPurchasingInvoice":
                    purchasingInvoiceID = (int?)filters["purchasingInvoiceID"];
                    return bll.GetPurchasingInvoice(userId, purchasingInvoiceID, out notification);

                case "GetPrintoutCreditNote":
                    int id = (int)filters["purchasingCreditNoteID"];
                    return bll.GetPrintoutCreditNote(userId, id, out notification);

                case "GetPurchasingInvoiceSearchResult":
                    return bll.GetPurchasingInvoiceSearchResult(userId, filters,out notification);

                case "GetSupplier":
                    return bll.GetSupplier(userId);

                case "SetStatus":
                    purchasingCreditNoteID = (int)filters["purchasingCreditNoteID"];
                    bool status = (bool)filters["status"];
                    return bll.SetStatus(purchasingCreditNoteID, userId, status, out notification);
               
                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }

        
    }
}
