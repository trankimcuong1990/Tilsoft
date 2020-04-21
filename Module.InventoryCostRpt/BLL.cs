namespace Module.InventoryCostRpt
{
    using Library.DTO;

    public class BLL
    {
        private DAL.DataFactory dataFactory = new DAL.DataFactory();
        public object GetInitData(int userID, out Notification notification)
        {
            return dataFactory.GetInitData(userID, out notification);
        }
        public object GetDataWithFilters(int iRequesterID, int? factoryWarehouseID, int? productionItemTypeID, string startDate, out Library.DTO.Notification notification)
        {
            return dataFactory.GetDataWithFilters(iRequesterID, factoryWarehouseID, productionItemTypeID, startDate, out notification);
        }

        public string GetExcelReportData(int iRequesterID, int? factoryWarehouseID, int? productionItemTypeID, string startDate, out Library.DTO.Notification notification)
        {
            return dataFactory.GetExcelReportData(iRequesterID, factoryWarehouseID,  productionItemTypeID, startDate, out notification);
        }
    }
}
