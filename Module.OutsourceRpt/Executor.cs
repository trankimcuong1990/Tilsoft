namespace Module.OutsourceRpt
{
    using Library.DTO;
    using System.Collections;

    public class Executor : Library.Base.IExecutor
    {
        private BLL bll = new BLL();

        public string identifier { get; set; }

        public bool Approve(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public object CustomFunction(int userID, string identifier, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            switch (identifier)
            {
                case "GetProductionItem":
                    return bll.GetProductionItem(userID, filters, out notification);
                case "GetDocumentNote":
                    return bll.GetDocumentNote(userID, filters, out notification);
                case "ExportOutsourceReport":
                    return bll.ExportOutsourceReport(userID, filters, out notification);
                default:
                    notification.Type = NotificationType.Error;
                    notification.Message = "Can not matched function";
                    return null;
            }
        }

        public bool DeleteData(int userID, int id, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public object GetData(int userID, int id, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public object GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return bll.GetDataWithFilter(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userID, out Notification notification)
        {
            return bll.GetInitData(userID, out notification);
        }

        public bool Reset(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateData(int userID, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }
    }
}
