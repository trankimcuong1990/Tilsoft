using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PermissionOverviewRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public List<DTO.UserPermission> GetReportDataDetail(int iRequesterID, int? moduleId, int? userId, int? userGroupId, int? officeId)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get permission overview data detail");

            // query data
            return factory.GetReportDataDetail(moduleId, userId, userGroupId, officeId);
        }
        public List<DTO.Module> GetReportData(int iRequesterID, int? moduleId, int? userId, int? userGroupId, int? officeId)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get permission overview data");

            // query data
            return factory.GetReportData(moduleId, userId, userGroupId, officeId);
        }
        public DTO.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }
    }
}
