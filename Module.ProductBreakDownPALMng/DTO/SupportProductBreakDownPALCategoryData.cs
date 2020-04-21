using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class SupportProductBreakDownPALCategoryData
    {
        public int ProductBreakDownDefaultCategoryID { get; set; }
        public string ProductBreakDownDefaultCategoryNM { get; set; }
        public int? ProductBreakDownCalculationTypeID { get; set; }
        public string Unit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? OptionTotalID { get; set; }
        public int? OptionToGetPriceID { get; set; }
        public decimal? QuantityPercent { get; set; }
        public decimal? PercentWastage { get; set; }
    }
}
