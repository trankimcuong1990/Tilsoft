using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class ProductBreakDownDetailPALData
    {
        public int ProductBreakDownDetailID { get; set; }
        public int? ProductBreakDownCategoryID { get; set; }
        public string ProductBreakDownDetailNM { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DimensionL { get; set; }
        public decimal? DimensionW { get; set; }
        public decimal? DimensionH { get; set; }
        public string Profile { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Volume { get; set; }
        public decimal? Acreage { get; set; }
        public string Remark { get; set; }
        public int? ProductBreakDownCategoryTypeID { get; set; }
        public string Unit { get; set; }
        public int? DisplayOrder { get; set; }

        public int? ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }

        public string ProductionItemVNNM { get; set; }
        public int? UnitID { get; set; }
        public string ProductBreakDownDetailVNNM { get; set; }

        public string UnitNMNotProductionItem { get; set; }
    }
}
