using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class ProductionItem
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public int? UnitID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public decimal? FrameWeight { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalReceive { get; set; }
        public decimal? StockQnt { get; set; }
        public bool? IsHasFrameWeight { get; set; }
        public List<ProductionItemUnit> ProductionItemUnits { get; set; }
        public int? BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public decimal? BOMQnt { get; set; }
        public decimal? BOMQntTotal { get; set; }
        public string WorkOrderUD { get; set; }
        public int? ParentProductionItemID { get; set; }
        public string ParentProductionItemUD { get; set; }
        public string ParentProductionItemNM { get; set; }
        public int? BranchID { get; set; }
        public int PrimaryID { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductionItem()
        {
            ProductionItemUnits = new List<ProductionItemUnit>();
        }
    }
}
