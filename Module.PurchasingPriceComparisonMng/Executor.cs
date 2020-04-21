using Library.DTO;
using System;
using System.Collections;

namespace Module.PurchasingPriceComparisonMng
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
            notification = new Notification();
            notification.Type = NotificationType.Success;

            switch (identifier.Trim().ToLower())
            {
                case "getreportdata":
                    if (!input.ContainsKey("Season") || input["Season"] == null || string.IsNullOrEmpty(input["Season"].ToString()))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Please input Season to search data!";

                        return null;
                    }
                    else
                    {
                        string season = input["Season"].ToString();

                        return bll.GetReportData(userId, season, out notification);
                    }

                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can't matched identifier!";

                    return null;
            }
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitForm(userId, out notification);
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
            throw new NotImplementedException();
        }
    }
}
