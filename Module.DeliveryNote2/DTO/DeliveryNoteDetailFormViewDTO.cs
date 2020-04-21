using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class DeliveryNoteDetailFormViewDTO
    {
        public int? DeliveryNoteDetailID { get; set; }
        public int? DeliveryNoteID { get; set; }
        public  int? ProductionItemID { get; set; }
        public string ProductionItemNM { get; set; }
        public decimal? Qty { get; set; }
        public string FactoryWarehousePalletNM { get; set; }
        public string Description { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public int? BOMID { get; set; }
        public int? QNTBarCode { get; set; }
        public decimal? QtyByUnit { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public decimal? BOMQnt { get; set; }
        public decimal? BOMQntTotal { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public decimal? TotalDelivery { get; set; }
        public decimal? StockQnt { get; set; }
        public string UnitNM { get; set; }
        public string ParentProductionItemNM { get; set; }
    }
}
