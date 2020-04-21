using Module.QAQCMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng
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
            throw new NotImplementedException();
            //return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {           
            switch (identifier.ToLower())
            {               
                case "api-get-qaqc-by-userid":
                    return bll.GetQAQCByUserID(userId, out notification);
                case "api-get-category":
                    return bll.GetCategory(out notification);
                case "api-update-report":
                    if (!input.ContainsKey("report"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow report" };
                        return null;
                    }
                    return bll.UpdateReportFromMobile(userId, input["report"], out notification);

                case "api-get-status":
                    return bll.GetStatusQAQC(userId, out notification);

                case "api-make-process":
                    string managementStringID = input["managementStringID"].ToString();
                    return bll.MakeToProcess(userId, managementStringID, out notification);

                case "search-data-report":
                    int pageSize = Convert.ToInt32(input["pageSize"]);
                    int pageIndex = Convert.ToInt32(input["pageIndex"]);
                    string sortedBy = input["sortedBy"].ToString();
                    string sortedDirection = input["sortedDirection"].ToString();
                    return bll.SearchReportData(userId, input, pageSize, pageIndex, sortedBy, sortedDirection, out notification);

                case "get-data-report":
                    int id = Convert.ToInt32(input["reportQAQCID"]);
                    return bll.GetReportQAQC(userId, id, out notification);

                case "set-status-report":
                    int reportQAQCID = Convert.ToInt32(input["reportQAQCID"]);
                    int statusId = Convert.ToInt32(input["statusID"]) ;
                    string comment  = input["comment"].ToString();
                    return bll.SetStatusReport(userId, reportQAQCID, statusId, comment, out notification);

                case "set-tracking-factory-order":
                    if (!input.ContainsKey("data") || input["data"] == null || string.IsNullOrEmpty(input["data"].ToString()))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow data!" };
                        return null;
                    }
                    return bll.SetTrackingFacoryOrDer(userId, input["data"], out notification);

                case "get-loginspector":
                    int qaqcId = Convert.ToInt32(input["QAQCID"]);
                    return bll.GetLogInspector(userId, qaqcId, out notification);

                case "login":
                    return bll.LoginUser(userId, out notification);


            }
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Custom function's identifier not matched" };
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
