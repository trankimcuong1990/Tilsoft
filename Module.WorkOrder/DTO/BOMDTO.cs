using System.Collections.Generic;

namespace Module.WorkOrder.DTO
{
    public class BOMDTO
    {
        public int? BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ParentBOMID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public decimal? Quantity { get; set; }
        public int? WorkOrderQnt { get; set; }
        public decimal? TotalQnt { get; set; }
        public decimal? TotalDeliveryQnt { get; set; }
        public decimal? TotalReceivedQnt { get; set; }
        public decimal? RemaintQnt { get; set; }
        public string Unit { get; set; }
        public string WorkCenterNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public string Description { get; set; }
        public decimal? QtyByUnit { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public int? ProductionItemID { get; set; }
        public int? StockQnt { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public List<BOMDTO> BOMDTOs { get; set; }
        public int? BranchID { get; set; }
        public decimal WastagePercent { get; set; }
        public decimal? Delta { get; set; }
        public decimal? StockQntPAL_1 { get; set; }
        public decimal? StockQntPAL_2 { get; set; }
        public BOMDTO()
        {
            BOMDTOs = new List<BOMDTO>();
        }
    }
}
