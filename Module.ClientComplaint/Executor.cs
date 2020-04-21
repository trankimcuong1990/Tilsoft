using System;

namespace Module.ClientComplaint
{
    public class Executor : Library.Base.IExecutor
    {
        private BLL _bll = new BLL();

        public string identifier { get; set; }
        public object GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return _bll.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            return _bll.DeleteData(userId, id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            _bll = new BLL(identifier);
            return _bll.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return _bll.Approve(userId, id, ref dtoItem, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return _bll.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return _bll.GetInitData(userId, out notification);
        }

        public object CustomFunction(int userId, string actionName, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            switch (actionName.ToLower())
            {
                case "getdata":
                    return _bll.GetData(userId, Convert.ToInt32(filters["id"]), Convert.ToInt32(filters["clientId"]), filters["season"].ToString(), out notification);

                case "quicksearchfactoryorderdetails":
                    return _bll.QuickSearchFactoryOrderDetailItems(userId, filters, out notification);

                case "quicksearchpis":
                    return _bll.QuickSearchPIs(userId, filters, out notification);

                case "getshipmentstatusofpisolution":
                    return _bll.GetShipmentStatusOfPiSolution(userId, filters, out notification);

                case "exportexcelclientcomplaint":
                    return _bll.ExportExcelClientComplaint(userId, filters, out notification);

                case "exportexcelclientcomplaintitem":
                    return _bll.ExportExcelClientComplaintItem(userId, filters, out notification);

                default:
                    notification.Message = "function identifier do not match";
                    notification.Type = Library.DTO.NotificationType.Error;
                    break;
            }
            return default(object);
        }
    }

}
