using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.RAPVNRpt
{
    public class Executor : Library.Base.IExecutor
    {
        public BLL Bll = new BLL();
        public string identifier { get; set; }


        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int? factoryID = (input.ContainsKey("factoryID") && input["factoryID"] != null && !string.IsNullOrEmpty(input["factoryID"].ToString()) ? (int?)Convert.ToInt32(input["factoryID"].ToString()) : null);
            string season = (input.ContainsKey("season") && input["season"] != null && !string.IsNullOrEmpty(input["season"].ToString()) ? Convert.ToString(input["season"]) : "");
            switch (identifier.Trim())
            {
                case "getdatawithfilters":
                    if (!input.ContainsKey("isRAPEU"))
                    {
                        notification.Message = "unknow isRAPEU";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    
                    return Bll.GetDataWithFilters(input, userId, out int totalRows, out notification);
                case "getexcelreport":
                    return Bll.GetExcelReportData(false, season, factoryID, userId, out notification);

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
            throw new NotImplementedException();
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return Bll.GetInitData(userId, out notification);
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
