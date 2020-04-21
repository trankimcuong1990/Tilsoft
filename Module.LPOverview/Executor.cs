﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LPOverview
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
            switch (identifier.ToLower())
            {
                case "getdata":
                    if (!input.ContainsKey("ID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow labeling packaging id" };
                        return null;
                    }
                    return bll.GetData(userId, Convert.ToInt32(input["ID"]), out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(out notification);

                case "revisedata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.ReviseData(userId, Convert.ToInt32(input["id"]), out notification);

                case "getexceldata":
                    return bll.GetExcelData(userId, Convert.ToInt32(input["id"]), out notification);

                case "getexceldatanl":
                    return bll.GetExcelDataNL(userId, Convert.ToInt32(input["id"]), out notification);

                case "getexcelreport":
                    return bll.GetExcelReportData(userId, input, out notification);
                case "getexceldatav2":
                    return bll.GetExcelDataV2(userId, Convert.ToInt32(input["id"]), Convert.ToString(input["ListLPDetails"]), Convert.ToString(input["ListLPSparepartDetails"]), out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }
    }
}
