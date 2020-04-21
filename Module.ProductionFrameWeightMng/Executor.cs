using Library.Base;
using Library.DTO;
using System;
using System.Collections;

namespace Module.ProductionFrameWeightMng
{
    public class Executor : IExecutor
    {
        private readonly BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object CustomFunction(int userID, string identifier, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            //string workOrderUD = (filters.ContainsKey("workOrderUD") && filters["workOrderUD"] != null && !string.IsNullOrEmpty(filters["workOrderUD"].ToString()) ? Convert.ToString(filters["workOrderUD"]) : "");
            //string clientUD = (filters.ContainsKey("clientUD") && filters["clientUD"] != null && !string.IsNullOrEmpty(filters["clientUD"].ToString()) ? Convert.ToString(filters["clientUD"]) : "");
            //string proformaInvoiceNo = (filters.ContainsKey("proformaInvoiceNo") && filters["proformaInvoiceNo"] != null && !string.IsNullOrEmpty(filters["proformaInvoiceNo"].ToString()) ? Convert.ToString(filters["proformaInvoiceNo"]) : "");
            //string productionItem = (filters.ContainsKey("productionItem") && filters["productionItem"] != null && !string.IsNullOrEmpty(filters["productionItem"].ToString()) ? Convert.ToString(filters["productionItem"]) : "");
            int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? Convert.ToInt32(filters["id"].ToString().Trim()) : 0;

            switch (identifier.ToLower())
            {
                case "getdata":
                    return bll.GetData(userID, id, out notification);

                case "updatedata":
                    return bll.UpdateData(userID, id, filters, out notification);

                case "deletedata":
                    return bll.DeleteData(userID, id, out notification);

                case "getexcelreport":
                    string workOrderUD = filters["workOrderUD"] == null ? null : filters["workOrderUD"].ToString();
                    string clientUD = filters["clientUD"] == null ? null : filters["clientUD"].ToString();
                    string proformaInvoiceNo = filters["proformaInvoiceNo"] == null ? null : filters["proformaInvoiceNo"].ToString();
                    string productionItem = filters["productionItem"] == null ? null : filters["productionItem"].ToString();
                    return bll.GetExcelReportData(workOrderUD, clientUD, proformaInvoiceNo, productionItem, out notification);

                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can't found identifier matched to handle";

                    return null;
            }
        }

        public bool DeleteData(int userID, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetData(int userID, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userID, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Reset(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}