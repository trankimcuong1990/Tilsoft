using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionPriceMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int id;
            int productionItemTypeID;
            int month;
            int year;
            int productionItemID;
            switch (identifier.Trim())
            {
                case "GetData":
                    id = Convert.ToInt32(filters["id"]);
                    return bll.GetData(userId, id, filters, out notification);

                case "GetSearchFilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "CalculateAvgPrice":
                    productionItemTypeID = Convert.ToInt32(filters["productionItemTypeID"]);
                    month = Convert.ToInt32(filters["month"]);
                    year = Convert.ToInt32(filters["year"]);
                    return bll.CalculateAvgPrice(userId, productionItemTypeID, month, year, out notification);

                case "LockPrice":
                    id = Convert.ToInt32(filters["id"]);
                    bool isLocked = Convert.ToBoolean(filters["isLocked"]);
                    return bll.LockPrice(userId, id, isLocked, out notification);

                case "GetReceiptByProductionItem":
                    productionItemID = Convert.ToInt32(filters["productionItemID"]);
                    month = Convert.ToInt32(filters["month"]);
                    year = Convert.ToInt32(filters["year"]);
                    return bll.GetReceiptByProductionItem(userId, productionItemID, month, year, out notification);

                case "ApplyPriceOnReceipt":
                    var productionPriceID = Convert.ToInt32(filters["productionPriceID"]);
                    return bll.ApplyPriceOnReceipt(productionPriceID, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
