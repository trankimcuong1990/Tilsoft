using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientOfferMng
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
            int printOptionValue = 1;
            object dtoItem = null;
            switch (identifier.Trim())
            {
                case "GetClientCostItem":
                    id = Convert.ToInt32(filters["clientCostItemID"]);
                    return bll.GetClientCostItem(userId, id, out notification);
                case "GetClientConditionItem":
                    id = Convert.ToInt32(filters["clientConditionItemID"]);
                    return bll.GetClientConditionItem(userId, id, out notification);
                case "UpdateClientCostItem":
                    id = Convert.ToInt32(filters["clientCostItemID"]);
                    dtoItem = filters["dtoItem"];
                    return bll.UpdateClientCostItem(userId, id, ref dtoItem, out notification);
                case "UpdateClientConditionItem":
                    id = Convert.ToInt32(filters["clientConditionItemID"]);
                    dtoItem = filters["dtoItem"];
                    return bll.UpdateClientConditionItem(userId, id, ref dtoItem, out notification);
                case "GetReportClientOfferOverview":
                    string validFrom = filters["validFrom"].ToString();
                    string validTo = filters["validTo"].ToString();
                    return bll.GetReportClientOfferOverview(userId, validFrom, validTo, out notification);
                case "DeleteClientCostItem":
                    id = Convert.ToInt32(filters["clientCostItemID"]);
                    return bll.DeleteClientCostItem(userId, id, out notification);
                case "DeleteClientConditionItem":
                    id = Convert.ToInt32(filters["clientConditionItemID"]);
                    return bll.DeleteClientConditionItem(userId, id, out notification);

                case "ClientOffer_Printout":
                    id = Convert.ToInt32(filters["clientOfferID"]);
                    printOptionValue = Convert.ToInt32(filters["printOptionValue"]);
                    return bll.ClientOffer_Printout(userId, printOptionValue, id, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
