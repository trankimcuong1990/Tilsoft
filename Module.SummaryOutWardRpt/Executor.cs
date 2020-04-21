using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.SummaryOutWardRpt
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
            int? month = (input.ContainsKey("month") && input["month"] != null && !string.IsNullOrEmpty(input["month"].ToString()) ? (int?)Convert.ToInt32(input["month"].ToString()) : null);
            int? year = (input.ContainsKey("year") && input["year"] != null && !string.IsNullOrEmpty(input["year"].ToString()) ? (int?)Convert.ToInt32(input["year"].ToString()) : null);
            string workOrderStatusNM = (input.ContainsKey("workOrderStatusNM") && input["workOrderStatusNM"] != null && !string.IsNullOrEmpty(input["workOrderStatusNM"].ToString()) ? Convert.ToString(input["workOrderStatusNM"]) : "");
            switch (identifier.Trim())
            {
                case "getdatawithfilters":
                    return Bll.GetDataWithFilters(userId, month, year, workOrderStatusNM, out notification);
                case "getexcelreport":
                    return Bll.GetExcelReportData(userId, month, year, workOrderStatusNM, out notification);

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
