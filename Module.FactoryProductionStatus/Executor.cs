using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus
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
            int factoryID=0;
            string season="";
            switch (identifier.Trim())
            {
                case "GetData":
                    int factoryProductionStatusID = Convert.ToInt32(filters["factoryProductionStatusID"]);
                    factoryID = Convert.ToInt32(filters["factoryID"]);
                    season = filters["season"].ToString();
                    return bll.GetData(userId, factoryProductionStatusID, factoryID,season, out notification);
                case "GetProductionOverview":
                    factoryID = Convert.ToInt32(filters["factoryID"]);
                    season = filters["season"].ToString();
                    bool isGetSupportData = Convert.ToBoolean(filters["isGetSupportData"]);
                    return bll.GetProductionOverview(userId, factoryID, season, isGetSupportData, out notification);
                case "GetOrderDetail":
                    int factoryOrderID = Convert.ToInt32(filters["factoryOrderID"]);
                    string excludeFactoryOrderDetailIDs = filters["excludeFactoryOrderDetailIDs"].ToString();
                    return bll.GetOrderDetail(userId, factoryOrderID, excludeFactoryOrderDetailIDs, out notification);
                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
