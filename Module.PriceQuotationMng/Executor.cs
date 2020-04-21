using Library.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using System.Collections;

namespace Module.PriceQuotationMng
{
    public class Executor : IExecutor
    {
        private BLL bLL = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            if (id == 0)
            {
                return bLL.Approve(userId, id, ref dtoItem, out notification);
            }
            else
            {
                return bLL.UnConfirmData(userId, id, ref dtoItem, out notification);
            }
        }

        public object CustomFunction(int userId, string identifier, Hashtable filters, out Notification notification)
        {
            notification = new Library.DTO.Notification()
            {
                Type = Library.DTO.NotificationType.Success
            };

            switch (identifier)
            {
                case "GetInitData":
                    return bLL.GetInitData(filters, out notification);
                case "GetPrice":
                    return bLL.GetPriceSeason(userId, filters, out notification);
                case "UpdateAllDifferenceCode":
                    return bLL.UpdateAllDifferenceCode(userId, filters, out notification);
                case "GetDataForPopupWithFilters":
                    return bLL.GetData(userId, filters, out notification);
                case "GetPricePreviousSeason":
                    return bLL.GetPricePreviousSeason(userId, filters, out notification);
                case "UpdateData":
                    return bLL.UpdateData(userId, filters["data"], out notification);
                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    return default(object);
            }
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
            return bLL.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
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
            return bLL.UpdateData(userId, id, ref dtoItem, out notification);
        }
    }
}
