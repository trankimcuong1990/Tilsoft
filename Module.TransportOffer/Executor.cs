using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportOffer
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
            return bll.GetData(userId, id, out notification);
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
            int id = -1;
            object dtoItem = null;
            switch (identifier.Trim())
            {
                case "GetTransportCostItem":
                    id = Convert.ToInt32(filters["transportCostItemID"]);
                    return bll.GetTransportCostItem(userId,id, out notification);
                case "GetTransportConditionItem":
                    id = Convert.ToInt32(filters["transportConditionItemID"]);
                    return bll.GetTransportConditionItem(userId,id, out notification);
                case "UpdateTransportCostItem":
                    id = Convert.ToInt32(filters["transportCostItemID"]);
                    dtoItem = filters["dtoItem"];
                    return bll.UpdateTransportCostItem(userId, id, ref dtoItem, out notification);
                case "UpdateTransportConditionItem":
                    id = Convert.ToInt32(filters["transportConditionItemID"]);
                    dtoItem = filters["dtoItem"];
                    return bll.UpdateTransportConditionItem(userId, id, ref dtoItem, out notification);
                case "GetReportTransportOfferOverview":
                    string validFrom = filters["validFrom"].ToString();
                    string validTo = filters["validTo"].ToString();
                    return bll.GetReportTransportOfferOverview(userId, validFrom, validTo, out notification);
                case "DeleteTransportCostItem":
                    id = Convert.ToInt32(filters["transportCostItemID"]);
                    return bll.DeleteTransportCostItem(userId, id, out notification);
                case "DeleteTransportConditionItem":
                    id = Convert.ToInt32(filters["transportConditionItemID"]);
                    return bll.DeleteTransportConditionItem(userId, id, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
