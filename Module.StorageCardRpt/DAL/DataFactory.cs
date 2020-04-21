using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StorageCardRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();

        public DTO.InitNewForm GetInitData(int userID, int productionItemID, int factoryWarehouseID, string startDate, string endDate, int branchID, out Library.DTO.Notification notification)
        {
            DTO.InitNewForm data = new DTO.InitNewForm();
            data.Data = new DTO.ProductionItem();
            data.FactoryWarehouses = new List<DTO.FactoryWarehouseDTO>();

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                // Declare data factory of support to get data.
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

                // Get company of end-user.
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);

                //data.FactoryWarehouses = supportFactory.GetFactoryWarehouse(companyID);

                using (var context = CreateContext())
                {
                    data.Data = converter.DB2DTO_Result(context.ProductionItemMng_ProductionItem_View.FirstOrDefault(o => o.ProductionItemID == productionItemID));
                    data.FactoryWarehouses = converter.DB2DTO_FactoryWarehouses(context.StorageCardRpt_FactoryWarehouse_View.Where(o => o.BranchID == branchID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public DTO.SearchForm GetDataWithFilters(int userID, int productionItemID, int factoryWarehouseID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchForm data = new DTO.SearchForm();
            data.FinalBeginning = 0;
            data.TotalPurchaseOrderQnt = null;
            data.StorageCards = new List<DTO.StorageCard>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    data.FinalBeginning = context.StorageCardRpt_function_GetTotalBeginning(productionItemID, factoryWarehouseID, startDate, endDate, companyID).FirstOrDefault();

                    var results = context.StorageCardRpt_function_StorageCardSearchResult(productionItemID, factoryWarehouseID, startDate, endDate, companyID);
                    data.StorageCards = converter.DB2DTO_SearchResults(results.ToList());

                    //get from PurchaseOrder
                    var POItem = context.StorageCardRpt_ListItemPurchaseOrder_View.Where(o => o.ProductionItemID == productionItemID).ToList();
                    decimal? totalQnt = 0;
                    int? checkPR = null;
                    string strPurchaseOrder = "";
                    for (int i = 0; i < POItem.Count; i++)
                    {
                        var item = POItem[i];
                        totalQnt += item.OrderQnt;
                        if(item.PurchaseOrderID != null)
                        {
                            if(checkPR != item.PurchaseOrderID)
                            {
                                if(strPurchaseOrder != "")
                                {
                                    strPurchaseOrder += ",";
                                }
                                strPurchaseOrder += item.PurchaseOrderID;
                                checkPR = item.PurchaseOrderID;
                            }
                        }
                    }
                    data.TotalPurchaseOrderQnt = totalQnt;
                    data.PurchaseOrderIDs = strPurchaseOrder;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        private StorageCardRptEntities CreateContext()
        {
            return new StorageCardRptEntities(Library.Helper.CreateEntityConnectionString("DAL.StorageCardRptModel"));
        }
    }
}
