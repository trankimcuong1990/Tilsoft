using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.AccountPayableRpt
{
    public class Executor : Library.Base.IExecutor2
    {
        BLL bll = new BLL();
        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
       
        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "getliabilities":
                    return bll.GetLiabilities(userId, input, out notification);
                case "query-supplier":
                    return bll.QuerySupplier(input, out notification);
                //case "get-thcnpthu":
                //    return bll.GetCongNoPhaiThu(userId, input, out notification);
                case "query-client":
                    return bll.QueryCustomer(input, out notification);
                case "process":
                    return bll.Process(userId, input, out notification);
                case "getpurchaseinvoice":
                    string sortedBy="";
                    string sortedDirection="";
                    return bll.GetPurchaseInvoice(userId, input, sortedBy, sortedDirection, out notification);
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
            throw new NotImplementedException();
        }
       
        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }
        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
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
            throw new NotImplementedException();
        }
    }
}
