using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ModuleStatusMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public bool Update(int userId, int id, ref object dto, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(userId, id, ref dto, out notification);
        }
        public DTO.SupportFormData GetInit(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }
        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get purchase order list");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
    }
}
