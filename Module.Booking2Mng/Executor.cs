using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Booking2Mng
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
            return bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string identifier, System.Collections.Hashtable input, out Library.DTO.Notification notification)
        {
            switch (identifier.ToLower())
            {
                case "getexcelreport":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.GetExcelReportData(userId, Convert.ToInt32(input["id"].ToString()), out notification);
                case "getloadingplanreport":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    return bll.GetLoadingPlanReportData(userId, Convert.ToInt32(input["id"].ToString()), out notification);
                case "getloadingplanreport_pl":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("reportOption"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow reportOption" };
                        return null;
                    }
                    return bll.GetLoadingPlanReportData_PL(userId, Convert.ToInt32(input["id"].ToString()), Convert.ToInt32(input["reportOption"].ToString()), out notification);
                case "confirmeta":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("etaType"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow ETA type" };
                        return null;
                    }
                    if (!input.ContainsKey("dtoItem"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow dto" };
                        return null;
                    }
                    object dtoItem = input["dtoItem"];
                    return bll.ConfirmETA(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["etaType"]), ref dtoItem, out notification);

                case "getsearchfilter":
                    return bll.GetSearchFilter(userId, out notification);

                case "getdata":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("clientId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow clientId" };
                        return null;
                    }
                    if (!input.ContainsKey("supplierId"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow supplierId" };
                        return null;
                    }
                    if (!input.ContainsKey("season"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow season" };
                        return null;
                    }
                    else
                    {
                        if (input["season"] == null)
                        {
                            input["season"] = string.Empty;
                        }
                    }
                    return bll.GetData(userId, Convert.ToInt32(input["id"]), Convert.ToInt32(input["supplierId"]), Convert.ToInt32(input["clientId"]), input["season"].ToString(), out notification);
                case "getsetdefault":
                    if (!input.ContainsKey("clientID"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow client" };
                        return null;
                    }
                    return bll.GetSetDefault(userId, Convert.ToInt32(input["clientID"]), out notification);

                case "confirm-all-loading":
                    if (!input.ContainsKey("id"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow id" };
                        return null;
                    }
                    if (!input.ContainsKey("dtoItem"))
                    {
                        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = "Unknow dto" };
                        return null;
                    }
                    object dtoItem2 = input["dtoItem"];
                    return bll.ConfirmAllLoading(userId, Convert.ToInt32(input["id"]), ref dtoItem2, out notification);

                case "getfactoryorder":
                    string pi = (input.ContainsKey("pi") && input["pi"] != null && !string.IsNullOrEmpty(input["pi"].ToString()) ? Convert.ToString(input["pi"]) : null);
                    int? bookingID = (input.ContainsKey("bookingID") && input["bookingID"] != null && !string.IsNullOrEmpty(input["bookingID"].ToString()) ? (int?)Convert.ToInt32(input["bookingID"].ToString()) : null);
                    return bll.GetFactoryOrder(Convert.ToInt32(input["clientID"]), input["season"].ToString(), pi, bookingID,  out notification);
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
