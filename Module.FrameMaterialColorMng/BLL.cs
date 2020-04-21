using Module.FrameMaterialColorMng.DAL;
using Module.FrameMaterialColorMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FrameMaterialColorMng
{
    public class BLL 
    {
        private DataFactory factory = new DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of frame material color");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get frame material color " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public bool DeleteData(int id,out Library.DTO.Notification notification)
        {           
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update frame material color " + id.ToString());

            // query data
            return factory.UpdateDataFrameMaterialColor(userId, id, ref dtoItem, out notification);
        }
               
    }
}
