﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestMng
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
            return bll.GetData(userId, id, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
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
            int id;
            object data;

            switch (identifier.ToLower())
            {
                case "getsearchfilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "addcomment":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.AddComment(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "assign":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.Assign(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "unassign":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.UnAssign(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "priority":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.ChangePriority(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "status":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.ChangeStatus(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "estenddate":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    id = Convert.ToInt32(input["id"]);
                    data = input["data"];
                    if (bll.ChangeEstEndDate(userId, id, ref data, out notification))
                    {
                        return data;
                    }
                    return null;

                case "esthourspend":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    return bll.UpdateEstHourSpend(userId, Convert.ToInt32(input["id"]), input["data"], out notification);
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
