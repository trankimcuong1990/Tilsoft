using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class ProductBreakDownDefaultCategoryPALData
    {
        public int ProductBreakDownDefaultCategoryID { get; set; }
        public string ProductBreakDownDefaultCategoryNM { get; set; }
        public int? ProductBreakDownCalculationTypeID { get; set; }
        public string ProductBreakDownCalculationTypeNM { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? CreatedBy { get; set; }
        public string InformationCreatorNM { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string InformationUpdatorNM { get; set; }
        public string UpdatedDate { get; set; }
        public int? OptionTotalID { get; set; }
        public string OptionTotalNM { get; set; }
        public int? DisplayOrder { get; set; }
        public int? OptionToGetPriceID { get; set; }
        public string OptionToGetPriceNM { get; set; }
        public decimal? QuantityPercent { get; set; }
        public decimal? PercentWastage { get; set; }
    }
}
