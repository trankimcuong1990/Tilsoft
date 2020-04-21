using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StorageCardRpt
{
    internal class BLL
    {
        private DAL.ReportFactory reportFactory = new DAL.ReportFactory();

        private DAL.DataFactory dataFactory = new DAL.DataFactory();

        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return reportFactory.GetInitData(iRequesterID, out notification);
        }

        public string ExportStorageCard(int iRequesterID, int productionItemID, int factoryWarehouseID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            return reportFactory.ExportStorageCard(iRequesterID, productionItemID, factoryWarehouseID, startDate, endDate, out notification);
        }

        public object GetInitFromInventoryOverview(int iRequesterID, int productionItemID, int factoryWarehouseID, string startDate, string endDate, int branchID, out Library.DTO.Notification notification)
        {
            return dataFactory.GetInitData(iRequesterID, productionItemID, factoryWarehouseID, startDate, endDate, branchID, out notification);
        }

        public object GetDataWithFilters(int iRequesterID, int productionItemID, int factoryWarehouseID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            return dataFactory.GetDataWithFilters(iRequesterID, productionItemID, factoryWarehouseID, startDate, endDate, out notification);
        }
    }
}
