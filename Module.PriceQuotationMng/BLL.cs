using Library.DTO;
using Module.PriceQuotationMng.DAL;
using Module.PriceQuotationMng.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceQuotationMng
{
    public class BLL
    {
        private DataFactory factory = new DataFactory();
        private Framework.BLL bLL = new Framework.BLL();

        public SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            bLL.WriteLog(userID, 0, "Log get data with filters.");
            return factory.GetDataWithFilter(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool UpdateData(int userID, int id, ref object dtoItem, out Notification notification)
        {
            bLL.WriteLog(userID, id, "Log update data.");
            return factory.UpdateData(userID, id, ref dtoItem, out notification);
        }

        public object GetPriceSeason(int userID, Hashtable filters, out Notification notification)
        {
            return factory.GetSeasonWithFilter(filters, out notification);
        }

        public bool UpdateAllDifferenceCode(int userID, Hashtable filters, out Notification notification)
        {
            return factory.UpdateAllDifferenceCode(filters, out notification);
        }

        public EditFormData GetData(int userID, Hashtable filters, out Notification notification)
        {
            bLL.WriteLog(userID, 0, "Log get data with filters.");
            return factory.GetData(filters, out notification);
        }

        public List<PriceQuotationSearchResultData> GetPricePreviousSeason(int userID, Hashtable filters, out Notification notification)
        {
            return factory.GetPricePreviousSeason(filters, out notification);
        }

        public bool UnConfirmData(int userID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UnConfirmData(userID, id, ref dtoItem, out notification);
        }

        public bool Approve(int userID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Approve(userID, id, ref dtoItem, out notification);
        }

        public object GetInitData(Hashtable filters, out Notification notification)
        {
            return factory.GetInitData(filters, out notification);
        }

        public List<PriceQuotationSearchResultData> UpdateData(int userId, object filters, out Notification notification)
        {
            return factory.UpdateData(userId, filters, out notification);
        }
    }
}
