using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get loading plan list");

            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public Task<DTO.SearchFormData> GetDataWithFilterAsync(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get loading plan list");
            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilterAsync(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(iRequesterID, out notification);
        }

        public DTO.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public List<DTO.BookingSearchResult> QuickSearchBooking(int iRequesterID, int factoryid, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchBooking(iRequesterID, factoryid, query, out notification);
        }

        public List<DTO.LoadingPlan> QuickSearchParentLoadingPlan(int iRequesterID, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchParentLoadingPlan(query, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int bookingID, int factoryID, int parentID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get loading plan");
            return factory.GetData(iRequesterID, id, bookingID, factoryID, parentID, out notification);
        }

        public List<DTO.FactoryOrderDetailSearchResult> QuickSearchFactoryOrderDetail(int iRequesterID, int factoryid, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchFactoryOrderDetail(iRequesterID, factoryid, query, out notification);
        }

        public List<DTO.FactoryOrderSparepartDetailSearchResult> QuickSearchFactoryOrderSparepartDetail(int iRequesterID, int factoryid, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchFactoryOrderSparepartDetail(iRequesterID, factoryid, query, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update loading plan");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete loading plan");
            return factory.DeleteData(iRequesterID, id, out notification);
        }
    }
}
