using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NotificationMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get price list");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }


        public DTO.EditFormDataDTO GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public bool Update(int userId, int id, ref object dto, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(userId, id, ref dto, out notification);
        }

        public object GetModuleStatuses(int iRequesterID, int moduleID, int notificationGroupID, out Library.DTO.Notification notification)
        {
            return factory.GetModuleStatuses(moduleID, notificationGroupID, out notification);
        }

        public object GetUserFilter(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get price list");
            return factory.GetUser(filters, out notification);
        }

        public object UpdateModuleStatus(int id, int moduleID, string statusUD, string statusNM, out Library.DTO.Notification notification)
        {
            return factory.UpdateModuleStatus(id, moduleID, statusUD, statusNM, out notification);
        }
    }
}
