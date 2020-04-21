using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientLPMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get client LP");

            return factory.GetEditData(iRequesterID, id, out notification);
        }
        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update client LP" + id.ToString());

            return factory.Update(iRequesterID, id, ref dtoItem, out notification);
        }

        public object GetEmployee(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetEmployee(filters,out notification);
        }
        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get client LP list");
            return factory.GetDataWithFilters(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.InitData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get as InitData");

            // query data
            return factory.GetInitData(out notification);

        }
        public bool DeleteData(int iRequesterID, int id, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, id, "Delete data width " + id.ToString());
            return factory.Delete(id, out notification);
        }
    }
}
