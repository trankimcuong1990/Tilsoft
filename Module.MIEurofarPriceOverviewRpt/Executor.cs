using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIEurofarPriceOverviewRpt
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
            return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            switch (identifier.Trim().ToLower())
            {
                case "getexcelreport":
                    if (input.ContainsKey("Season"))
                    {
                        return bll.GetExcelReportData(userId, input["Season"].ToString(), out notification);
                    }
                    else
                    {
                        notification.Message = "unknow season";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    break;

                case "gethtmlreport":
                    if (input.ContainsKey("Season"))
                    {
                        return bll.GetHtmlReportData(userId, input["Season"].ToString(), out notification);
                    }
                    else
                    {
                        notification.Message = "unknow season";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    break;

                case "get-admin-dashboard":
                    if (input.ContainsKey("season"))
                    {
                        return bll.GetSalePerformance(userId, input["season"].ToString(), out notification);
                    }
                    else
                    {
                        notification.Message = "unknow season";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    break;

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
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
