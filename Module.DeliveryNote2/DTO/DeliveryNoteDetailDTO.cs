using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class DeliveryNoteDetailDTO
    {
        public int? DeliveryNoteDetailID { get; set; }
        public int? DeliveryNoteID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? Qty { get; set; }
        public int? FactoryWarehousePalletID { get; set; }
        public string Description { get; set; }
        public int? FromFactoryWarehouseID { get; set; }
        public int? BOMID { get; set; }
        public int? QNTBarCode { get; set; }
        public decimal? QtyByUnit { get; set; }
        public int? UnitID { get; set; }

        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string FactoryWarehousePalletNM { get; set; }
        public decimal? BOMQnt { get; set; }
        public decimal? BOMQntTotal { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public decimal? TotalDelivery { get; set; }
        public decimal? StockQnt { get; set; }        
        public string UnitNM { get; set; }        
        public string ParentProductionItemNM { get; set; }
        public int? ParentProductionItemID { get; set; }
        public string ParentProductionItemUD { get; set; }

        public List<ProductionItemUnit> ProductionItemUnits { get; set; }
        public decimal? UnitPrice { get; set; }
        public int PrimaryID { get; set; }
        public string FromFactoryWarehouseNM { get; set; }

        public decimal? OrderQnt { get; set; }
        public decimal? TotalDeliverWorkOrder { get; set; }
        public int? OutsourcingCostID { get; set; }
        public string OutsourcingCostNM { get; set; }
        public int? PurchaseOrderDetailID { get; set; }
        public decimal? OtherPrice { get; set; }
        public int? ReasonOtherPriceID { get; set; }
        public int? Quantity { get; set; }
        public int? FactorySaleOrderDetailID { get; set; }
        public DeliveryNoteDetailDTO()
        {
            ProductionItemUnits = new List<ProductionItemUnit>();
        }
    }
}
