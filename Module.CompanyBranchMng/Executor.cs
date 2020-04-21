using Library.Base;
using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng
{
    public class Executor : IExecutor2
    {
        private readonly BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            switch (identifier.ToLower())
            {
                case "quick-search-branch":
                    return bll.GetQuickSearchBranch(userId, input, out notification);

                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not find identifier";
                    return null;
            }
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public object GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            return bll.GetData(userId, id, param, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }
    }
}
