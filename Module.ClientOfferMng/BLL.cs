using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get client offer list");
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

        public DTO.ClientCostItemDTO GetClientCostItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetClientCostItem(id, out notification);
        }

        public DTO.ClientConditionItemDTO GetClientConditionItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetClientConditionItem(id, out notification);
        }

        public DTO.ClientCostItemDTO UpdateClientCostItem(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateClientCostItem(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.ClientConditionItemDTO UpdateClientConditionItem(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateClientConditionItem(iRequesterID, id, ref dtoItem, out notification);
        }

        public string GetReportClientOfferOverview(int iRequesterID, string validFrom, string validTo, out Library.DTO.Notification notification)
        {
            return factory.GetReportClientOfferOverview(validFrom, validTo, out notification);
        }

        public bool DeleteClientCostItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteClientCostItem(id, out notification);
        }
        public bool DeleteClientConditionItem(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteClientConditionItem(id, out notification);
        }

        public string ClientOffer_Printout(int iRequesterID, int printOptionValue, int id, out Library.DTO.Notification notification)
        {
            return factory.ClientOffer_Printout(id, printOptionValue, out notification);
        }
    }
}
