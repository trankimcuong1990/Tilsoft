using System.Collections.Generic;

namespace Module.ProductionFrameWeightMng.DTO
{
    public class ProductionFrameWeightSearchResultData
    {
        public int ProductionFrameWeightID { get; set; }
        public int? ProductionItemID { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public decimal? FrameWeight { get; set; }
        public int RowIndex { get; set; }
        public string Remark { get; set; }
        
        public string MaterialTypeNM { get; set; }

        public List<SaleOrderData> ProductionFrameWeightSaleOrder { get; set; }
        public List<ClientData> ProductionFrameWeightClient { get; set; }
        public List<WorkOrderData> ProductionFrameWeightWorkOrder { get; set; }

        public ProductionFrameWeightSearchResultData()
        {
            ProductionFrameWeightSaleOrder = new List<SaleOrderData>();
            ProductionFrameWeightClient = new List<ClientData>();
            ProductionFrameWeightWorkOrder = new List<WorkOrderData>();
        }
    }
}
