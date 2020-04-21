using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoice2Mng
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
            object dtoItem = null;
            switch (identifier.ToLower())
            {
                case "getreport":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.GetReportData(userId, Convert.ToInt32(input["id"]), out notification);

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
                    if (!input.ContainsKey("clientid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow clientid" };
                        return null;
                    }
                    if (!input.ContainsKey("factoryid"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factoryid" };
                        return null;
                    }
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    else 
                    {
                        if (input["season"] == null)
                        {
                            input["season"] = string.Empty;
                        }
                    }
                    return bll.GetData(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["clientid"]), Convert.ToInt32(input["factoryid"]), input["season"].ToString(), out notification);

                case "searchclient":
                    if (!input.ContainsKey("query"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }
                    return bll.QuickSearchClient(userId, input["query"].ToString(), out notification);

                case "searchitem":
                    if (!input.ContainsKey("clientId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow clientId" };
                        return null;
                    }
                    if (!input.ContainsKey("factoryId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factoryid" };
                        return null;
                    }
                    if (!input.ContainsKey("factoryProformaInvoiceId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow factoryProformaInvoiceId" };
                        return null;
                    }
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    if (!input.ContainsKey("description"))
                    {
                        input["description"] = string.Empty;
                    }
                    if (!input.ContainsKey("factoryOrderUd"))
                    {
                        input["factoryOrderUd"] = string.Empty;
                    }
                    if (!input.ContainsKey("itemType"))
                    {
                        input["itemType"] = string.Empty;
                    }
                    return bll.QuickSearchItem(userId, Convert.ToInt32(input["clientId"].ToString()), Convert.ToInt32(input["factoryId"].ToString()), Convert.ToInt32(input["factoryProformaInvoiceId"].ToString()), input["season"].ToString(), input["description"].ToString(), input["factoryOrderUd"].ToString(), input["itemType"].ToString(), out notification);

                case "furnindoconfirm":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("dtoItem"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    dtoItem = input["dtoItem"];
                    if (bll.FurnindoConfirm(userId, Convert.ToInt32(input["id"].ToString()), ref dtoItem, out notification))
                    {
                        return dtoItem;
                    }
                    return null;

                case "furnindounconfirm":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("dtoItem"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    dtoItem = input["dtoItem"];
                    if (bll.FurnindoUnConfirm(userId, Convert.ToInt32(input["id"].ToString()), ref dtoItem, out notification))
                    {
                        return dtoItem;
                    }
                    return null;

                case "factoryconfirm":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("dtoItem"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    dtoItem = input["dtoItem"];
                    if (bll.FactoryConfirm(userId, Convert.ToInt32(input["id"].ToString()), ref dtoItem, out notification))
                    {
                        return dtoItem;
                    }
                    return null;

                case "factoryunconfirm":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("dtoItem"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    dtoItem = input["dtoItem"];
                    if (bll.FactoryUnConfirm(userId, Convert.ToInt32(input["id"].ToString()), ref dtoItem, out notification))
                    {
                        return dtoItem;
                    }
                    return null;
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
