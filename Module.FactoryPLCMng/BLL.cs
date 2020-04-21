using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get plc list");

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

        public List<DTO.BookingSearchResult> QuickSearchBooking(int iRequesterID, int factoryid, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchBooking(iRequesterID, factoryid, query, out notification);
        }

        public List<DTO.ItemSearchResult> QuickSearchItem(int iRequesterID, int factoryid, int bookingid, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchItem(iRequesterID, factoryid, bookingid, query, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int bookingID, int factoryID, int offerLineID, int offerLineSampleProductID, int offerLineSparepartID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get plc");
            return factory.GetData(iRequesterID, id, bookingID, factoryID, offerLineID, offerLineSampleProductID, offerLineSparepartID, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update plc");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete plc");
            return factory.DeleteData(iRequesterID, id, out notification);
        }

        public string GetReportData(int iRequesterID, string PLCIDs, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(iRequesterID, PLCIDs, out notification);
        }
    }
}
