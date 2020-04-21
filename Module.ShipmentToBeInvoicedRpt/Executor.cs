﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.ShipmentToBeInvoicedRpt
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "exportexcelwithseason":
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Notification();
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow season";

                        return null;
                    }

                    return bll.ExportExcel(userId, input["season"].ToString().Trim(), out notification);
            }

            notification = new Library.DTO.Notification();
            notification.Type = NotificationType.Error;
            notification.Message = "Custom function's identifier not matched";

            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetSupportSeasons(userId, out notification);
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
