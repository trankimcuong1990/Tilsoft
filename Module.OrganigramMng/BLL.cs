using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.OrganigramMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL() { }

        public BLL(string tempFolder)
        {
            factory = new DAL.DataFactory(tempFolder);
        }

        public DTO.SearchFormData GetDataWithFilter(int iRequestID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, 0, "Get company list.");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int iRequestID, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, 0, string.Format("Get organigram {0}", id.ToString()));
            return factory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update organigram");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }
        //
        // CUSTOM FUNCTION
        //
        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }
    }
}
