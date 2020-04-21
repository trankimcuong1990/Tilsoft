﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

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
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            bll = new BLL();
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

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            switch (identifier.Trim())
            {
                case "SearchComponentNeedToImport":
                    return bll.SearchComponentNeedToImport(userId, filters, out notification);
                case "SearchComponentNeedToExport":
                    return bll.SearchComponentNeedToExport(userId, filters, out notification);
                case "SearchComponentNeedToImportWithoutOrdesBuyDirectly":
                    return bll.SearchComponentNeedToImportWithoutOrdesBuyDirectly(userId, filters, out notification);
                case "SearchComponentNeedToImportWithoutOrdesInProgress":
                    return bll.SearchComponentNeedToImportWithoutOrdesInProgress(userId, filters, out notification);
                case "SearchComponentNeedToExportWithoutOrdesInProgress":
                    return bll.SearchComponentNeedToExportWithoutOrdesInProgress(userId, filters, out notification);
                case "SearchComponentNeedToImportOrdersBuyDirectlies":
                    return bll.SearchComponentNeedToImportOrdersBuyDirectlies(userId, filters, out notification);
                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}