﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.ReceiptProductionRpt
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
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            switch (identifier.Trim())
            {
                case "GetWorkOrder":
                    if (!input.ContainsKey("searchQuery") || input["searchQuery"] == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Unknow search query";

                        return null;
                    }
                    else
                    {
                        return bll.GetWorkOrder(userId, input["searchQuery"].ToString().Trim(), out notification);
                    }
                //case "GetHistoryDeliveryNote":
                //    return bll.GetHistoryDeliveryNote(userId, Convert.ToInt32(input["workOrderID"]), Convert.ToInt32(input["workCenterID"]), Convert.ToInt32(input["productionItemID"]), out notification);
                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }

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
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitData(userId, out notification);
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
