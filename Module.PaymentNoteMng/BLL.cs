using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.SearchForm GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "search Receipt");
            return factory.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditForm GetData(int userId, int id, System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get Receipt");
            return factory.GetData(userId, id, param, out notification);
        }
        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "delete Receipt" + id.ToString());
            return factory.DeleteData(userId, id, out notification);
        }
        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "update Receipt" + id.ToString());
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }
        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve Receipt");
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }
        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve Receipt");
            return factory.Reset(userId, id, ref dtoItem, out notification);
        }
        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get init Receipt");
            return factory.GetInitData(userId, out notification);
        }
        public object GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get search filter Receipt");
            return factory.GetInitData(userId, out notification);
        }

        //customize function

        public List<DTO.PaymentSupportSearchSupplier> QuyerySupplier(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.QuyerySupplier(param, out notification);
        }
        public DTO.SearchPurchasingInvoice SearchPurchasingInvoice(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            return factory.SearchPurchasingInvoice(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out notification);
        }

        public int SetStatus(int userId, int paymentNoteID, int statusID, int paymentNoteTypeID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "set status");
            return factory.SetStatus(userId, paymentNoteID, statusID, paymentNoteTypeID, out notification);
        }
        public string GetPaymentPrintout(int iRequesterID, int paymentNoteprintID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "paymentNote printout: " + paymentNoteprintID.ToString());
            return factory.GetPaymentPrintout(paymentNoteprintID, out notification);
        }

        public decimal GetPaymentByDeposit(int supplierID, string year, string currency, out Library.DTO.Notification notification)
        {
            return factory.GetPaymentByDeposit(supplierID, year, currency, out notification);
        }

        public decimal GetTotalDeposit(int supplierID, string year, string currency, out Library.DTO.Notification notification)
        {
            return factory.GetTotalDeposit(supplierID, year, currency, out notification);
        }

        public DTO.SearchPO SearchPO(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            return factory.SearchPO(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out notification);
        }

        public List<DTO.POFromInvoiceDTO> GetPOFromInvoice(int purchaseInvoiceID, out Library.DTO.Notification notification) {
            return factory.GetPOFromInvoice(purchaseInvoiceID, out notification);
        }
    }
}
