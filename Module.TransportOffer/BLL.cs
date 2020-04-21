using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportOffer
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get transport offer list");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete transport offer" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update transport offer" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get transport offer" + id.ToString());

            return factory.GetData(iRequesterID, id, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.TransportCostItemDTO GetTransportCostItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetTransportCostItem(id, out notification);
        }

        public DTO.TransportConditionItemDTO GetTransportConditionItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetTransportConditionItem(id, out notification);
        }

        public DTO.TransportCostItemDTO UpdateTransportCostItem(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateTransportCostItem(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.TransportConditionItemDTO UpdateTransportConditionItem(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateTransportConditionItem(iRequesterID, id, ref dtoItem, out notification);
        }

        public string GetReportTransportOfferOverview(int iRequesterID, string validFrom, string validTo, out Library.DTO.Notification notification)
        {
            return factory.GetReportTransportOfferOverview(validFrom, validTo, out notification);
        }

        public bool DeleteTransportCostItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteTransportCostItem(id, out notification);
        }
        public bool DeleteTransportConditionItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteTransportConditionItem(id, out notification);
        }
    }
}
