using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.MasterExchangeRateMng
{
    public class Executor : Library.Base.IExecutor
    {
        BLL bll = new BLL();

        public string identifier { get ; set ; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = NotificationType.Success };

            switch (identifier.Trim())
            {
                //case "getdataspecification":
                //    if (!input.ContainsKey("id"))
                //    {
                //        notification.Type = NotificationType.Error;
                //        notification.Message = "Unknown id!";

                //        return null;
                //    }

                //    if (!input.ContainsKey("sampleProductID"))
                //    {
                //        notification.Type = NotificationType.Error;
                //        notification.Message = "Unknown sampleProductID!";

                //        return null;
                //    }

                //    int id = (input["id"] != null && !string.IsNullOrEmpty(input["id"].ToString())) ? Convert.ToInt32(input["id"].ToString()) : 0;
                //    int? sampleProductID = (input["sampleProductID"] != null && !string.IsNullOrEmpty(input["sampleProductID"].ToString())) ? (int?)Convert.ToInt32(input["sampleProductID"].ToString()) : null;
                //    int? productID = (input["productID"] != null && !string.IsNullOrEmpty(input["productID"].ToString())) ? (int?)Convert.ToUInt32(input["productID"].ToString()) : null;

                //    return bll.GetDataSpecification(userId, id, sampleProductID, productID, out notification);

                case "updateindex":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    return bll.UpdateIndexData(userId, input["data"], out notification);


                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = NotificationType.Error;
                    break;
            }
            return null;
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
