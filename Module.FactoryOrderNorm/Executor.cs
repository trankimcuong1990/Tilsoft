using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryOrderNorm
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
            bll = new BLL();
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
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
            switch (identifier.Trim())
            {
                case "CreateNewFactoryFinishedProduct":
                    string code = filters["factoryFinishedProductUD"].ToString();
                    string name = filters["factoryFinishedProductNM"].ToString();
                    return bll.CreateNewFactoryFinishedProduct(userId, code, name, out notification);
                case "GetListClient":
                    return bll.GetListClient(userId, filters, out notification);
                case "CreateOrderNorm":
                    string season = filters["season"].ToString();
                    int clientID = (int)filters["clientID"];
                    int productID = (int)filters["productID"];
                    return bll.CreateOrderNorm(userId, season, clientID, productID, out notification);
                case "CreateComponentNorm":
                    int factoryNormID = Convert.ToInt32(filters["factoryNormID"]);
                    DTO.FactoryFinishedProductOrderNorm item = ((Newtonsoft.Json.Linq.JObject)filters["dtoItem"]).ToObject<DTO.FactoryFinishedProductOrderNorm>();
                    return bll.CreateComponentNorm(userId, factoryNormID, item, out notification);

                case "CreateSubComponentNorm":
                    int parentFactoryFinishedProductNormID = Convert.ToInt32(filters["parentFactoryFinishedProductNormID"]);
                    DTO.FactoryFinishedProductOrderNorm sItem = ((Newtonsoft.Json.Linq.JObject)filters["dtoItem"]).ToObject<DTO.FactoryFinishedProductOrderNorm>();
                    return bll.CreateSubComponentNorm(userId, parentFactoryFinishedProductNormID, sItem, out notification);

                case "CreateFactoryMaterialNorm":
                    int factoryFinishedProductNormID = Convert.ToInt32(filters["factoryFinishedProductNormID"]);
                    DTO.FactoryMaterialOrderNorm mItem = ((Newtonsoft.Json.Linq.JObject)filters["dtoItem"]).ToObject<DTO.FactoryMaterialOrderNorm>();
                    return bll.CreateFactoryMaterialNorm(userId, factoryFinishedProductNormID, mItem, out notification);

                case "DeleteFinishedProductNorm":
                    int fID = Convert.ToInt32(filters["id"]);
                    return bll.DeleteFinishedProductNorm(userId, fID, out notification);

                case "DeleteMaterialNorm":
                    int mID = Convert.ToInt32(filters["id"]);
                    return bll.DeleteMaterialNorm(userId, mID, out notification);
                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
