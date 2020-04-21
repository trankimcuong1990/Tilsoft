using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionTestingMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get material testing list");
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public object GetCushionColor(int userId, Hashtable filters, out Notification notification)
        {
            return factory.GetCushionColor(filters, out notification);
        }

        public DTO.EditFormData GetData(int userID, int id, out Notification notification)
        {
            return factory.GetEdit(id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dto, out Notification notification)
        {
            return factory.Update(userId, id, ref dto, out notification);
        }

        public bool DeleteData(int userID, int id, out Notification notification)
        {
            return factory.Delete(id, out notification);
        }
    }
}
