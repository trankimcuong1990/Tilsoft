using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryGoodsProcedure
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public BLL()
        {
        }

        public DTO.SearchFormDataDTO GetDataWithFilter(int iRequestID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // Keep log entry.
            fwBLL.WriteLog(iRequestID, 0, "Get factory goods procedure list.");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormDataDTO GetData(int iRequestID, int id, out Library.DTO.Notification notification)
        {
            // Keep log entry.
            fwBLL.WriteLog(iRequestID, 0, string.Format("Get factory goods procedure {0}", id.ToString()));
            return factory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequestID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, id, string.Format("Update factory goods procedure {0}", id.ToString()));
            return factory.UpdateData(iRequestID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequestID, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, id, string.Format("Delete factory goods procedure {0}", id.ToString()));
            return factory.DeleteData(id, out notification);
        }

        public int EditDetail(int iRequestID, int factoryGoodsProcedureID, object factoryGoodsProcedureDetail, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, 0, "Edit factory goods procedure detail.");
            return factory.EditDetail(factoryGoodsProcedureID, factoryGoodsProcedureDetail, out notification);
        }

        public bool DeleteDetail(int iRequestID, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequestID, 0, "Delete factory goods procedure detail.");
            return factory.DeleteDetail(id, out notification);
        }
    }
}
