using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.ProductBreakDownMng
{
    public class Executor : Library.Base.IExecutor
    {
        private readonly BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            switch (identifier.ToLower())
            {
                case "getdata":
                    return bll.GetData(userId, input, out notification);

                case "updatedata":
                    return bll.UpdateData(userId, input, out notification);

                case "deletedata":
                    return bll.DeleteData(userId, input, out notification);

                case "approvedata":
                    return bll.ApproveData(userId, input, out notification);

                case "quicksearchmodel":
                    return bll.GetModelSearch(userId, input, out notification);

                case "quicksearchsampleproduct":
                    return bll.GetSampleProductSearch(userId, input, out notification);

                case "getsupportclient":
                    return bll.GetSupportClient(userId, input, out notification);

                case "autofillproductbreakdowncategory":
                    return bll.GetProductBreakDownCategory(userId, input, out notification);

                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "Unknown match";
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
