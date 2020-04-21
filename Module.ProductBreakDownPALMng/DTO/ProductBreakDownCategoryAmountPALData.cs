using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class ProductBreakDownCategoryAmountPALData
    {
        public int ProductBreakDownCategoryID { get; set; }
        public int? ProductBreakDownID { get; set; }
        public string ProductBreakDownCategoryNM { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
        public int? DisplayOrder { get; set; }
        public int? ProductBreakDownCalculationTypeID { get; set; }
        public string ProductBreakDownCalculationTypeNM { get; set; }
        public int PrimaryID { get; set; }
        public int? OptionTotalID { get; set; }
        public int? OptionToGetPriceID { get; set; }
        public string OptionToGetPriceNM { get; set; }
        public decimal? QuantityPercent { get; set; }
        public bool IsSave { get; set; }
        public decimal? PercentWastage { get; set; }

        public List<ProductBreakDownCategoryImagePALData> ProductBreakDownCategoryImagePAL { get; set; }
        public List<ProductBreakDownCategoryTypePALData> ProductBreakDownCategoryTypePAL { get; set; }

        public ProductBreakDownCategoryAmountPALData()
        {
            ProductBreakDownCategoryImagePAL = new List<ProductBreakDownCategoryImagePALData>();
            ProductBreakDownCategoryTypePAL = new List<ProductBreakDownCategoryTypePALData>();
        }
    }
}
