﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccManagerPerformanceRpt
{
    public class Executor : Library.Base.IExecutor
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.GetData(userId, id, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "get-acc-performance":
                    if (!input.ContainsKey("saleId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow account manager!" };
                        return null;
                    }
                    return factory.GetAccPerformance(Convert.ToInt32(input["saleId"]), out notification);
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
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
