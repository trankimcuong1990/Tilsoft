using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.RevenueCostingRpt
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
            int? factoryRawMaterialID = (input.ContainsKey("factoryRawMaterialID") && input["factoryRawMaterialID"] != null && !string.IsNullOrEmpty(input["factoryRawMaterialID"].ToString()) ? (int?)Convert.ToInt32(input["factoryRawMaterialID"].ToString()) : null);
            string temp = "";
            string startDate = (input.ContainsKey("startDate") && input["startDate"] != null && !string.IsNullOrEmpty(input["startDate"].ToString()) ? Convert.ToString(input["startDate"]) : temp);
            string endDate = (input.ContainsKey("endDate") && input["endDate"] != null && !string.IsNullOrEmpty(input["endDate"].ToString()) ? Convert.ToString(input["endDate"]) : temp);
            switch (identifier.Trim())
            {
                case "getexcelreport":
                    return Bll.GetExcelReportData(userId, factoryRawMaterialID, startDate, endDate, out notification);

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
