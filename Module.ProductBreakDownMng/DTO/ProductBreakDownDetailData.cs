using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DTO
{
    public class ProductBreakDownDetailData
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
    }
}
