using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPLCMng
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

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "getreport":
                    if (!input.ContainsKey("ids"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow ids" };
                        return null;
                    }
                    return bll.GetReportData(userId, input["ids"].ToString(), out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "getinitdata":
                    return bll.GetInitData(userId, out notification);

                case "getdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("bookingid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow bookingid" };
                        return null;
                    }
                    if (!input.ContainsKey("factoryid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factoryid" };
                        return null;
                    }
                    if (!input.ContainsKey("offerlineid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerlineid" };
                        return null;
                    }
                    if (!input.ContainsKey("offerlinesparepartid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerlinesparepartid" };
                        return null;
                    }
                    if (!input.ContainsKey("offerlinesampleproductid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow offerlinesampleproductid" };
                        return null;
                    }
                    return bll.GetData(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["bookingid"]), Convert.ToInt32(input["factoryid"]), Convert.ToInt32(input["offerlineid"]), Convert.ToInt32(input["offerlinesampleproductid"]), Convert.ToInt32(input["offerlinesparepartid"]), out notification);

                case "searchbooking":
                    if (!input.ContainsKey("factoryid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory" };
                        return null;
                    }
                    if (!input.ContainsKey("query"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }
                    return bll.QuickSearchBooking(userId, Convert.ToInt32(input["factoryid"]), input["query"].ToString(), out notification);

                case "searchitem":
                    if (!input.ContainsKey("factoryid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factory" };
                        return null;
                    }
                    if (!input.ContainsKey("bookingid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow booking" };
                        return null;
                    }
                    if (!input.ContainsKey("query"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }
                    return bll.QuickSearchItem(userId, Convert.ToInt32(input["factoryid"]), Convert.ToInt32(input["bookingid"]), input["query"].ToString(), out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
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
