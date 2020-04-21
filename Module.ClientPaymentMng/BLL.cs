using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get client payment list");

            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete client payment: " + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update client payment: " + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve client payment: " + id.ToString());

            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        public DTO.InitFormData GetInit(out Library.DTO.Notification notification)
        {
            return factory.GetInit(out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, string currency, int methodId, int clientId, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get client payment: " + id.ToString());

            return factory.GetData(currency, methodId, clientId, id, out notification);
        }
    }
}
