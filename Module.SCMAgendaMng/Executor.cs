using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.GetData(userId, id, out notification);
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
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
                case "getsupportdata":
                    if (!input.ContainsKey("formName"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow form name" };
                        return null;
                    }
                    return bll.GetSupportData(input["formName"].ToString(), out notification);

                case "searchclient":
                    if (!input.ContainsKey("query"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow search query" };
                        return null;
                    }
                    return bll.QuickSearchClient(userId, input["query"].ToString(), out notification);

                case "listqcreport":
                    int? factoryID = (input.ContainsKey("factoryID") && input["factoryID"] != null && !string.IsNullOrEmpty(input["factoryID"].ToString()) ? (int?)Convert.ToInt32(input["factoryID"].ToString()) : null);
                    string temp = "";
                    string qcReportUD = (input.ContainsKey("qcReportUD") && input["qcReportUD"] != null && !string.IsNullOrEmpty(input["qcReportUD"].ToString()) ? Convert.ToString(input["qcReportUD"]) : temp);
                    string clientUD = (input.ContainsKey("clientUD") && input["clientUD"] != null && !string.IsNullOrEmpty(input["clientUD"].ToString()) ? Convert.ToString(input["clientUD"]) : temp);
                    string articleCode = (input.ContainsKey("articleCode") && input["articleCode"] != null && !string.IsNullOrEmpty(input["articleCode"].ToString()) ? Convert.ToString(input["articleCode"]) : temp);
                    string proformaInvoiceNo = (input.ContainsKey("proformaInvoiceNo") && input["proformaInvoiceNo"] != null && !string.IsNullOrEmpty(input["proformaInvoiceNo"].ToString()) ? Convert.ToString(input["proformaInvoiceNo"]) : temp);

                    return bll.ListQCReport(qcReportUD, factoryID, clientUD, articleCode, proformaInvoiceNo, out notification);
            }
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
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
