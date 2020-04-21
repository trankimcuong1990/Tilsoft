namespace Module.ProductBreakDownPALMng
{
    internal class BLL
    {
        private readonly DAL.DataFactory factory = new DAL.DataFactory();
        private readonly Framework.BLL fwBLL = new Framework.BLL();

        public object GetDataWithFilters(int userID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get data with filter for product break down for PAL");
            return factory.GetDataWithFilters(userID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get data with id for product break down for PAL");
            return factory.GetData(userID, filters, out notification);
        }

        public object GetInitData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get init data for product break down for PAL");
            return factory.GetInitData(userID, filters, out notification);
        }

        public object UpdateData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Update data for product break down for PAL");
            return factory.UpdateData(userID, filters, out notification);
        }

        public bool DeleteData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Delete data for product break down for PAL");
            return factory.DeleteData(userID, filters, out notification);
        }

        public object GetModelWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get list model with filter for Product Break Down for PAL");
            return factory.GetModelWithFilters(userID, filters, out notification);
        }

        public object GetSampleWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get list sample with filter for Product Break Down for PAL");
            return factory.GetSampleWithFilters(userID, filters, out notification);
        }

        public object GetClientWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get list client with filter for Product Break Down for PAL");
            return factory.GetClientWithFilters(userID, filters, out notification);
        }

        public object GetProductionItemWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get list production item with filter for Product Break Down for PAL");
            return factory.GetProductionItemWithFilters(userID, filters, out notification);
        }

        public object GetProductBreakDownCategoryPAL(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get list category with filter for Product Break Down for PAL");
            return factory.GetProductBreakDownCategoryPAL(userID, filters, out notification);
        }

        public object GetCategoryPALWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get list category with filter for Product Break Down for PAL");
            return factory.GetCategoryPALWithFilters(userID, filters, out notification);
        }

        public object ApproveData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Approve data for product break down for PAL");
            return factory.ApproveData(userID, filters, out notification);
        }

        public object GetProductWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userID, 0, "Get list product with filter for Product Break Down for PAL");
            return factory.GetProductWithFilters(userID, filters, out notification);
        }

        public DTO.ModelDefaultOption GetModelDefaultOption(int modelID, out Library.DTO.Notification notification)
        {
            return factory.GetModelDefaultOption(modelID, out notification);
        }

        public object ExportExcel(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcel(userID, filters, out notification);
        }
    }
}
