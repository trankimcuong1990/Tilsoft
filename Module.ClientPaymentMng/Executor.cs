using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();
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
            return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return bll.GetInit(out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {            
            switch (identifier.ToLower())
            { 
                case "getdata":
                    if (!input.ContainsKey("ClientID"))
                    { 
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow client" };
                        return null;
                    }
                    if (!input.ContainsKey("ID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow payment id" };
                        return null;
                    }
                    if (!input.ContainsKey("ClientPaymentMethodID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow payment type" };
                        return null;
                    }
                    if (!input.ContainsKey("Currency"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow currency" };
                        return null;
                    }
                    return bll.GetData(userId, input["Currency"].ToString(), Convert.ToInt32(input["ClientPaymentMethodID"]), Convert.ToInt32(input["ClientID"]), Convert.ToInt32(input["ID"]), out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched"};
            return null;
        }

        public string identifier
        {
            get
            {
                return _identifier;
            }
            set
            {
                _identifier = value;
            }
        }
        private string _identifier = string.Empty;
    }
}
