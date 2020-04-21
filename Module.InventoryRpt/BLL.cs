using Library.DTO;

namespace Module.InventoryRpt
{
    internal class BLL
    {
        private DAL.DataFactory dataFactory = new DAL.DataFactory();

        public object GetInitData(int userID, int? branchID, out Notification notification)
        {
            return dataFactory.GetInitData(userID, branchID, out notification);
        }

        public string ExportInventoryReport(int iRequesterID, int factoryWarehouseID, int? productionTeamID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            return dataFactory.ExportInventoryReport(iRequesterID, factoryWarehouseID, productionTeamID, startDate, endDate, out notification);
        }

        public object GetDataWithFilters(int iRequesterID, int? factoryWarehouseID, int? productionTeamID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            return dataFactory.GetDataWithFilters(iRequesterID, factoryWarehouseID, productionTeamID, startDate, endDate, out notification);
        }

        public string ExportInventoryReportDetail(int iRequesterID, int factoryWarehouseID, int? productionTeamID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            return dataFactory.ExportInventoryReportDetail(iRequesterID, factoryWarehouseID, productionTeamID, startDate, endDate, out notification);
        }

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return dataFactory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public object CustomGetDataWithFilter(int userID, System.Collections.Hashtable filters, out Notification notification)
        {
            return dataFactory.CustomGetDataWithFilter(userID, filters, out notification);
        }

        public object CustomExportDataWithFilter(int userID, System.Collections.Hashtable filters, out Notification notification)
        {
            return dataFactory.CustomExportDataWithFilter(userID, filters, out notification);
        }
    }
}
