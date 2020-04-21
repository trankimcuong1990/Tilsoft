using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.CashBookReceiptMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bLL = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            switch (identifier)
            {
                case "UpdatePostCostData":
                    return bLL.UpdatePostCostData(userId, input["data"], out notification);
                case "DeletePostCostData":
                    return bLL.DeletePostCostData(userId, Convert.ToInt32(input["id"]), out notification);
                case "UpdateCostItemData":
                    return bLL.UpdateCostItemData(userId, input["data"], out notification);
                case "DeleteCostItemData":
                    return bLL.DeleteCostItemData(userId, Convert.ToInt32(input["id"]), out notification);
                case "UpdateCostItemDetailData":
                    return bLL.UpdateCostItemDetailData(userId, input["data"], out notification);
                case "DeleteCostItemDetailData":
                    return bLL.DeleteCostItemDetailData(userId, Convert.ToInt32(input["id"]), out notification);
                case "ExportCashBook":
                    return bLL.ExportCashBook(userId, input, out notification);
                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "function identifier do not match";
                    return default(object);
            }
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bLL.DeleteData(userId, id, out notification);
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            return bLL.GetData(userId, id, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bLL.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bLL.GetInitData(out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bLL.UpdateData(userId, id, ref dtoItem, out notification);
        }
    }
}
