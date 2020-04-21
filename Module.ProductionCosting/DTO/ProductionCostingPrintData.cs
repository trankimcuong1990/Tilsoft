using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionCosting.DTO
{
    public class ProductionCostingPrintData
    {
        public int BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? CompanyID { get; set; }
        public int? ParentBOMID { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string WorkCenterNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public int? WorkOrderQnt { get; set; }
        public decimal? PlanQnt { get; set; }
        public decimal? UsingQnt { get; set; }
        public decimal? VarianceQnt { get; set; }
        public decimal? PlanCosting { get; set; }
        public decimal? UsingCosting { get; set; }
        public decimal? VarianceValue { get; set; }
        public decimal? VarianceCosting { get; set; }
        public int? WorkOrderStatusID { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public decimal? RemaintQnt { get; set; }
        public decimal? Quantity { get; set; }
        public string Description { get; set; }
        public int? PieceIndex { get; set; }
        public int? WorkCenterID { get; set; }
        public int? CountChildBOM { get; set; }
        public List<ProductionCostingPrintData> ProductionCostingDTOs { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? TotalReceivedQnt { get; set; }
    }
}
