using Library.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using System.Collections;

namespace Module.ClientSpecificationMng
{
    public class Executor : IExecutor
    {
        private BLL bll = new BLL();

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
                case "postcomment":
                    return bll.PostComment(userId, input["data"], out notification);
                case "deletecomment":
                    return bll.DeleteComment(userId, Convert.ToInt32(input["id"]), out notification);
                case "getstandardfile":
                    return bll.GetStandardFile(userId, Convert.ToInt32(input["typeID"]), Convert.ToInt32(input["clientID"]), out notification);
                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = NotificationType.Error;
                    return default(object);
            }
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            return bll.GetData(userId, id, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
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
