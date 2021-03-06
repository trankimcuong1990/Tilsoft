﻿using Library.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using System.Collections;

namespace Module.SampleLocationMng
{
    public class Executor : IExecutor
    {
        private BLL bLL = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification()
            {
                Type = Library.DTO.NotificationType.Success
            };

            switch (identifier.ToUpper())
            {
                case "GETDATA":
                    return bLL.GetData(userId, input, out notification);
                case "UPDATEDATA":
                    return bLL.UpdateData(userId, input, out notification);
                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "function identifier do not match";
                    break;
            }

            return default(object);
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            return bLL.GetData(userId, id, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bLL.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bLL.UpdateData(userId, id, ref dtoItem, out notification);
        }
    }
}
