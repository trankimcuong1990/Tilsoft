﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProduct
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
                case "CreateFactoryFinishedProductPrice":
                    int factoryFinishedProductID = Convert.ToInt32(filters["factoryFinishedProductID"]);
                    return bll.CreateFactoryFinishedProductPrice(userId, factoryFinishedProductID, filters["dtoItem"], out notification);
                case "DeleteFactoryFinishedProductPrice":
                    int factoryFinishedProductPriceID = Convert.ToInt32(filters["factoryFinishedProductPriceID"]);
                    return bll.DeleteFactoryFinishedProductPrice(userId, factoryFinishedProductPriceID, out notification);
                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }
    }
}
