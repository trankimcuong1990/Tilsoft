using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.InventoryRpt.DTO
{
    public class InventoryFinishProductDetailData
    {
        public int PrimaryID { get; set; }

        public int? ProductionItemID { get; set; }

        public string ProductionItemUD { get; set; }

        public string ProductionItemNM { get; set; }

        public int? FactoryWarehouseID { get; set; }

        public int? WorkOrderID { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? OrderQnt { get; set; }

        public decimal? BeginningQnt { get; set; }

        public decimal? ReceivingQnt { get; set; }

        public decimal? DeliveringQnt { get; set; }
    }
}
