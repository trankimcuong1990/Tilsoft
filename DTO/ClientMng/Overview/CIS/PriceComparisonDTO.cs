using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.CIS
{
    public class PriceComparisonDTO
    {
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalAmountConvertToEUR { get; set; }
        public decimal? UnitPriceUSD { get; set; }
        public decimal? UnitPriceEUR { get; set; }
        public int? CompareToTotalOrderQnt { get; set; }
        public decimal? CompareToTotalAmountConvertToEUR { get; set; }
        public decimal? CompareToUnitPriceUSD { get; set; }
        public decimal? CompareToUnitPriceEUR { get; set; }
    }
}
