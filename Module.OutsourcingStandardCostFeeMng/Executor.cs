using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.OutsourcingStandardCostFeeMng
{
    public class Executor : Library.Base.IExecutor2
    {
        BLL bll = new BLL();
        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            switch (identifier.ToLower())
            { 
            
                case "get-detail-model":
                    int? modelID = Convert.ToInt32(input["modelID"]);
                    return bll.GetDetailModel(userId, modelID, out notification);

                case "reset":
                    int osid = Convert.ToInt32(input["OutsourcingStandardCostFeeID"]);
                    return bll.Reset(userId, osid, out notification);

                case "insert-data":
                    return bll.InsertData(userId, out notification);

                default:
                    break;
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, Hashtable param, out Notification notification)
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

        public object GetSearchFilter(int userId, out Notification notification)
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
