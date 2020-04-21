﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PSRpt
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
            throw new NotImplementedException();
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "" };
            switch (identifier.ToLower())
            {
                case "getexcelreport":
                    if (!input.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    return bll.GetExcelReportData(userId, Convert.ToInt32(input["id"]), out notification);
                case "getexcelreportproduct":
                    if (!input.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    return bll.GetExcelReportDataProduct(userId, Convert.ToInt32(input["id"]), out notification);

                case "getexcelfactoryorderdetail":
                    if (!input.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }

                    return bll.GetExcelReport2FactoryOrderDetail(userId, Convert.ToInt32(input["id"]), out notification);

                case "getexcelproduct":
                    if (!input.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }

                    return bll.GetExcelReport2Product(userId, Convert.ToInt32(input["id"]), out notification);
            }
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
