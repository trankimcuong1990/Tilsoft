using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SystemSetting2Mng
{
    internal class BLL
    {
        private Framework.BLL fwBLL = new Framework.BLL();

        private DAL.DataFactory factory = new DAL.DataFactory();

        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "Get init data support");
            return factory.GetInitData(out notification);
        }
        public object CoppySeasion(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get seasion");

            // query data
            return factory.CoppySeassions(season, out notification);
            //return factory.CoppySeassions(season, iRequesterID, out notification);
        }

        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "Get search data filter");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "Update data");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "Delete data");
            return factory.DeleteData(id, out notification);
        }
    }
}
