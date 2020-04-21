using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2Mng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory payment list");

            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(iRequesterID, out notification);
        }

        public DTO.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public List<DTO.PurchasingInvoiceSearchResult> QuickSearchInvoice(int iRequesterID, int supplierId, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchInvoice(iRequesterID, supplierId, query, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int supplierID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get factory payment");
            return factory.GetData(iRequesterID, id, supplierID, season, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update factory payment");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete factory payment");
            return factory.DeleteData(iRequesterID, id, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve factory payment: " + id.ToString());
            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "reset factory payment: " + id.ToString());
            return factory.Reset(iRequesterID, id, ref dtoItem, out notification);
        }
    }
}
