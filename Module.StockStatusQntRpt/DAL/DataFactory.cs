using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockStatusQntRpt.DAL
{
    internal class DataFactory
    {
        private readonly DataConverter converter = new DataConverter();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        private StockStatusQntRptEntities CreateContext()
        {
            return new StockStatusQntRptEntities(Library.Helper.CreateEntityConnectionString("DAL.StockStatusQntRptModel"));
        }

        public object GetInitData(int userID,int branchID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            int? CompanyID = fwFactory.GetCompanyID(userID);
            DTO.InitFormDTO data = new DTO.InitFormDTO();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.ProductionItemGroups = this.GetProductionItemGroup().ToList();
                using (var context = CreateContext())
                {
                    data.FactoryWarehouseDtos = converter.DB2DTO_FactoryWarehouses(context.StockStatusQntRpt_FactoryWarehouse_View.Where(o => o.BranchID == branchID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public List<DTO.ProductionItemGroup> GetProductionItemGroup()
        {
            List<DTO.ProductionItemGroup> result = new List<DTO.ProductionItemGroup>();
            try
            {
                using (StockStatusQntRptEntities context = CreateContext())
                {
                    result = converter.DB2DTO_ProductionItemGroup(context.ProductionItemMng_ProductionItemGroup_View.ToList());
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public object GetDataWithFilter(int userID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            totalRows = 0;

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            int? companyID = fwFactory.GetCompanyID(userID);

            DTO.SearchFormDTO data = new DTO.SearchFormDTO();

            try
            {
                int? alertLevel = (filters.ContainsKey("AlertLevel") && filters["AlertLevel"] != null && !string.IsNullOrEmpty(filters["AlertLevel"].ToString())) ? (int?)Convert.ToInt32(filters["AlertLevel"].ToString()) : null;
                int? factoryWarehouseID = (filters.ContainsKey("FactoryWarehouseID") && filters["FactoryWarehouseID"] != null && !string.IsNullOrEmpty(filters["FactoryWarehouseID"].ToString())) ? (int?)Convert.ToInt32(filters["FactoryWarehouseID"].ToString()) : null;
                int? materialGroupID = (filters.ContainsKey("MaterialGroupID") && filters["MaterialGroupID"] != null && !string.IsNullOrEmpty(filters["MaterialGroupID"].ToString())) ? (int?)Convert.ToInt32(filters["MaterialGroupID"].ToString()) : null;

                using (var context = CreateContext())
                {
                    var dbItem = context.StockStatusQntRpt_function_GetDataStockStatusQnt(alertLevel,factoryWarehouseID,materialGroupID,orderBy,orderDirection).Where(o=>o.CompanyID == companyID).ToList();
                    data.stockStatusQntDTOs = converter.DB2DTO_StockStatus(dbItem.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}
