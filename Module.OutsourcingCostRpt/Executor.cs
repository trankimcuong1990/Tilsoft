using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.OutsourcingCostRpt
{
    public class Executor : Library.Base.IExecutor2
    {
        BLL bll = new BLL();
        private DAL.DataFactory dataFactory = new DAL.DataFactory();
        public string identifier { get; set; }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userId, string identifier, Hashtable input, out Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "get-report-data":
                    int pageSize = Convert.ToInt32(input["pageSize"]);
                    int pageIndex = Convert.ToInt32(input["pageIndex"]);
                    return dataFactory.GetReportData(userId, input, pageSize, pageIndex, out notification);

                case "get-report-data-detail":
                    return dataFactory.GetReportDataDetail(input, out notification);

                case "get-init-report":
                    return dataFactory.GetInitDataReport(userId, out notification);

                case "get-excel-report":
                    return dataFactory.GetExcelReportData(input, out notification);

                default:
                    break;
            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
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
            throw new NotImplementedException();
        }
    }
}
