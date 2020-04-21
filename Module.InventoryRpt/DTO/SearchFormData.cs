using System.Collections.Generic;

namespace Module.InventoryRpt.DTO
{
    public class SearchFormData
    {
        public List<InventoryData> InventoryOverview { get; set; }
        public List<InventoryDetailData> InventoryOverviewDetail { get; set; }
        public List<InventoryFinishProductData> InventoryFinishProduct { get; set; }
        public List<InventoryFinishProductDetailData> InventoryFinishProductDetail { get; set; }
        public List<InventoryOverviewClientData> InventoryOverviewClient { get; set; }

        public SearchFormData(int factoryWarehouseID)
        {
            if (factoryWarehouseID == 46)
            {
                InventoryFinishProduct = new List<InventoryFinishProductData>();
                InventoryFinishProductDetail = new List<InventoryFinishProductDetailData>();
            }
            else
            {
                InventoryOverview = new List<InventoryData>();
                InventoryOverviewDetail = new List<InventoryDetailData>();
            }

            InventoryOverviewClient = new List<InventoryOverviewClientData>();
        }
    }
}
