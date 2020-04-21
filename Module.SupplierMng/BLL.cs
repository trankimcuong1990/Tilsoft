using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SupplierMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of suppliers");
            return factory.GetDataWithFilter(new Hashtable(), pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get supplier " + id.ToString());
            return factory.GetData(id, iRequesterID, out notification);
        }

        public bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete supplier " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int id, ref object dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update supplier " + id.ToString());
            return factory.UpdateData(id, ref dtoItem, iRequesterID, out notification);
        }
    }
}
