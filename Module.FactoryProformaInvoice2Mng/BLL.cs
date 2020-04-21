using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoice2Mng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory proforma invoice list");

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

        public List<Support.DTO.Client> QuickSearchClient(int iRequesterID, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchClient(iRequesterID, query, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int clientID, int factoryID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get factory proforma invoice");
            return factory.GetData(iRequesterID, id, clientID, factoryID, season, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update factory proforma invoice");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete factory proforma invoice");
            return factory.DeleteData(iRequesterID, id, out notification);
        }

        public List<DTO.FactoryOrderItemSearchResult> QuickSearchItem(int userId, int clientId, int factoryId, int factoryProformaInvoiceId, string season, string description, string factoryOrderUd, string itemType, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchItem(userId, clientId, factoryId, factoryProformaInvoiceId, season, description, factoryOrderUd, itemType, out notification);
        }

        public bool FurnindoConfirm(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.FurnindoConfirm(userId, id, ref dtoItem, out notification);
        }
        public bool FurnindoUnConfirm(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.FurnindoUnConfirm(userId, id, ref dtoItem, out notification);
        }
        public bool FactoryConfirm(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.FactoryConfirm(userId, id, ref dtoItem, out notification);
        }
        public bool FactoryUnConfirm(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.FactoryUnConfirm(userId, id, ref dtoItem, out notification);
        }

        public string GetReportData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(iRequesterID, id, out notification);
        }
    }
}
