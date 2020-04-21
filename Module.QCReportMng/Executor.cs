using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.QCReportMng
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            switch (identifier.Trim())
            {

                case "searchpi":
                    return bll.SearchPI(userId, input, out notification);

                case "getdata":
                    int? clientID_1 = (input.ContainsKey("clientID") && input["clientID"] != null && !string.IsNullOrEmpty(input["clientID"].ToString()) ? (int?)Convert.ToInt32(input["clientID"].ToString()) : null);
                    string temp_1 = "";
                    string articleCode_1 = (input.ContainsKey("articleCode") && input["articleCode"] != null && !string.IsNullOrEmpty(input["articleCode"].ToString()) ? Convert.ToString(input["articleCode"]) : temp_1);
                    string itemFactoryOrderLink = (input.ContainsKey("itemFactoryOrderLink") && input["itemFactoryOrderLink"] != null && !string.IsNullOrEmpty(input["itemFactoryOrderLink"].ToString()) ? Convert.ToString(input["itemFactoryOrderLink"]) : temp_1);
                    return bll.GetData(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["saleOrderDetailID"]), Convert.ToInt32(input["factoryID"]), itemFactoryOrderLink, clientID_1, articleCode_1, out notification);
                case "getinspection":
                    return bll.GetInspection(userId, input["param"].ToString(), out notification);

                case "getexcelreport":
                    if (!input.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    return bll.GetExcelReportData(userId, Convert.ToInt32(input["id"]), out notification);

                case "get-pdf-report":
                    if (!input.ContainsKey("id"))
                    {
                        notification.Message = "unknow id";
                        notification.Type = Library.DTO.NotificationType.Error;
                    }
                    return bll.GetExportPDF(userId, Convert.ToInt32(input["id"]), out notification);

                case "getlistfactoryorder":
                    int? factoryID = (input.ContainsKey("factoryID") && input["factoryID"] != null && !string.IsNullOrEmpty(input["factoryID"].ToString()) ? (int?)Convert.ToInt32(input["factoryID"].ToString()) : null);
                    int? clientID = (input.ContainsKey("clientID") && input["clientID"] != null && !string.IsNullOrEmpty(input["clientID"].ToString()) ? (int?)Convert.ToInt32(input["clientID"].ToString()) : null);
                    string temp = "";
                    string articleCode = (input.ContainsKey("articleCode") && input["articleCode"] != null && !string.IsNullOrEmpty(input["articleCode"].ToString()) ? Convert.ToString(input["articleCode"]) : temp);
                    return bll.GetListPIFromFactoryOrderDTO(articleCode, clientID, factoryID, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return null;
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return bll.DeleteData(userId, id, out notification);
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return bll.UpdateData(userId, id, ref dtoItem, out notification);
        }
    }
}
