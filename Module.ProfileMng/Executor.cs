﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProfileMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                case "updateemployee":
                    if (!input.ContainsKey("data"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data" };
                        return null;
                    }
                    return bll.UpdateEmployee(userId, userId, input["data"], out notification);

                case "get-api-key":
                    return bll.GetAPIKey(userId, out notification);
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
