using System.Collections.Generic;

namespace Module.ReceiptNoteMng
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
        public DTO.SupplierDTO GetSupplier(int userID, out Library.DTO.Notification notification)
        {
            return factory.GetSupplier(userID, out notification);
        }   
        public DTO.SearchPurchasing SearchPurchasingInvoice(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            return factory.SearchPurchasingInvoice(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out notification);
        }

        public int SetStatus(int userId, int receiptNoteID, int statusID, int receiptNoteTypeID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "set status");
            return factory.SetStatus(userId, receiptNoteID, statusID, receiptNoteTypeID, out notification);
        }

        public List<DTO.ReceiptSupportSearchSupplier> QuyerySupplier(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.QuyerySupplier(param, out notification);
        }
        public DTO.SearchFactorySaleInvoice SearchFactorySaleInvoice(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            return factory.SearchFactorySaleInvoice(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out notification);
        }
        public List<DTO.EmployeeDTO> QuyeryEmployee(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.QuyeryEmployee(param, out notification);
        }
        public List<DTO.ProductionItemDTO> QueryProductionItem(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            return factory.QueryProductionItem(param, out notification);
        }
        //public string GetReceiptPrintout(int iRequesterID, int receiptNoteprintID, out Library.DTO.Notification notification)
        //{
        //    // keep log entry
        //    fwBLL.WriteLog(iRequesterID, 0, "receiptNote printout: " + receiptNoteprintID.ToString());
        //    return factory.GetReceiptPrintout(receiptNoteprintID, out notification);
        //}
    }
}
