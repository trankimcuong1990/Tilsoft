﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.FactorySaleInvoice
{
    public class Executor : Library.Base.IExecutor2
    {
        private BLL bll = new BLL();
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

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            switch (identifier.Trim())
            {
                case "get-factorysaleorder-data":
                    int pageSize = Convert.ToInt32(input["pageSize"]);
                    int pageIndex = Convert.ToInt32(input["pageIndex"]);
                    string sortedBy = input["sortedBy"].ToString();
                    string sortedDirection = input["sortedDirection"].ToString();

                    int totalRows = 0;
                    return bll.GetFactorySaleOrderData(userId, input, pageSize, pageIndex, sortedBy, sortedDirection, out totalRows, out notification);

                case "GetProductionItem":
                    return bll.GetProductionItem(userId, input);

                case "set-factorysaleinvoice-status":
                    int factorySaleInvoiceID = Convert.ToInt32(input["factorySaleInvoiceID"]);
                    int factorySaleInvoiceStatusID = Convert.ToInt32(input["factorySaleInvoiceStatusID"]);
                    return bll.SetFactorySaleInvoiceStatus(userId, factorySaleInvoiceID, factorySaleInvoiceStatusID, out notification);

                case "get-supplierpaymentterm-data":
                    int factoryRawMaterialID = Convert.ToInt32(input["factoryRawMaterialID"]);
                    return bll.GetSupplierPaymentTerm(factoryRawMaterialID, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public object GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            return bll.GetData(userId, id, param, out notification);
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
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
