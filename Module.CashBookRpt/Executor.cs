using Library.DTO;
using System;

namespace Module.CashBookRpt
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

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
            return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            int? bankAccount = (input.ContainsKey("bankAccount") && input["bankAccount"] != null && !string.IsNullOrEmpty(input["bankAccount"].ToString()) ? (int?)Convert.ToInt32(input["bankAccount"].ToString()) : null);
            switch (identifier.ToLower())
            {
                case "getsearchfilter":
                    if (!input.ContainsKey("paymentType"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow type cashBook";

                        return null;
                    }

                    if (!input.ContainsKey("bankAccount"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow bank account";

                        return null;
                    }

                    if (!input.ContainsKey("startDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow start date";

                        return null;
                    }

                    if (!input.ContainsKey("endDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow end date";

                        return null;
                    }
                    return bll.GetDataWithFilters(userId, Convert.ToInt32(input["paymentType"].ToString()), bankAccount, input["startDate"].ToString().Trim(), input["endDate"].ToString().Trim(), out notification);
                case "excel-getsearchfilter":
                    if (!input.ContainsKey("paymentType"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow type cashBook";

                        return null;
                    }

                    if (!input.ContainsKey("bankAccount"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow bank account";

                        return null;
                    }

                    if (!input.ContainsKey("startDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow start date";

                        return null;
                    }

                    if (!input.ContainsKey("endDate"))
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Unknow end date";

                        return null;
                    }

                    return bll.ExportExcel(Convert.ToInt32(input["paymentType"].ToString()), bankAccount, input["startDate"].ToString().Trim(), input["endDate"].ToString().Trim(), out notification);
            }

            notification.Type = NotificationType.Error;
            notification.Message = "Custom function's identifier not matched";

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
