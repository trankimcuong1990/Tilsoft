using System.Collections.Generic;

namespace Module.DeliveryNote2.DTO
{
    public class PurchaseOrderDetailDTO
    {
        public int PurchaseOrderDetailID { get; set; }
        public int? PurchaseOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int? UnitID { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public int? BranchID { get; set; }
        public decimal? StockQnt { get; set; }
        public decimal? TotalDelivery { get; set; }
        public List<DTO.ProductionItemUnit> ProductionItemUnits { get; set; }
        public decimal? OrderQnt { get; set; }

        public PurchaseOrderDetailDTO()
        {
            ProductionItemUnits = new List<ProductionItemUnit>();
        }
    }
}
