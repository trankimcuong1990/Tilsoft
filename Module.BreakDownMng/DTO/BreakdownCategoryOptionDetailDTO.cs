using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownCategoryOptionDetailDTO
    {
        public int BreakdownCategoryOptionDetailID { get; set; }
        public int? BreakdownCategoryOptionID { get; set; }
        public string Description { get; set; }
        public string DescriptionEN { get; set; }
        public int? ProductionItemID { get; set; }
        public int? UnitID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? QuantityPercent { get; set; }
        public decimal? WastedRatePercent { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemNMEN { get; set; }
        public string UnitNM { get; set; }
        public decimal? AVFPrice { get; set; }
        public decimal? AVTPrice { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public int ProductionItemGroupID { get; set; }
        public int? BreakdownCategoryOptionGroupID { get; set; }
        public int ProductionItemTypeID { get; set; }
    }
}
