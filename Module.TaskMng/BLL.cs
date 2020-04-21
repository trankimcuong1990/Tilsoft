using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get task list");

            filters["UserID"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.SearchFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(iRequesterID, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get task");
            return factory.GetData(id, out notification);
        }

        public DTO.ViewFormData GetViewData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get task");
            return factory.GetViewData(id, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete task");
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update task");
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.DetailFormData GetDetailData(int iRequesterID, int id, out Library.DTO.Notification notification)
        { 
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "get task step detail");
            return factory.GetDetailData(id, out notification);
        }

        public bool UpdateStepStatus(int iRequesterID, int id, int statusId, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update task step status ");
            return factory.UpdateStepStatus(iRequesterID, id, statusId, out notification);
        }

        public bool UpdateComment(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "update task step comment ");
            return factory.UpdateComment(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteComment(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, id, "delete task step comment ");
            return factory.DeleteComment(iRequesterID, id, out notification);
        }
    }
}
