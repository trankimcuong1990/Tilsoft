namespace Module.OutsourceRpt
{
    using Library.DTO;
    using Module.OutsourceRpt.DAL;
    using System.Collections;

    internal class BLL
    {
        private readonly DataFactory factory = new DataFactory();
        private readonly ReportFactory rptFactory = new ReportFactory();

        public object GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return factory.GetDataWithFilter(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(int userID, out Notification notification)
        {
            return factory.GetInitData(userID, out notification);
        }

        public object GetProductionItem(int userID, Hashtable filters, out Notification notification)
        {
            return factory.GetProductionItemWithWorkOrder(userID, filters, out notification);
        }

        public object GetDocumentNote(int userID, Hashtable filters, out Notification notification)
        {
            return factory.GetDocumentNoteWithProductionItem(userID, filters, out notification);
        }

        public object ExportOutsourceReport(int userID, Hashtable filters, out Notification notification)
        {
            return rptFactory.ExportOutsourceReport(userID, filters, out notification);
        }
    }
}
