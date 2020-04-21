using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.CIS
{
    public class ItemDTO
    {
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalOrderQnt40HC { get; set; }
        public int? TotalShippedQnt { get; set; }
        public decimal? TotalShippedQnt40HC { get; set; }
        public int? TotalToBeShippedQnt { get; set; }
        public decimal? TotalToBeShippedQnt40HC { get; set; }
        public decimal? UnitPriceUSD { get; set; }
        public decimal? TotalAmountUSD { get; set; }
        public decimal? UnitPriceEUR { get; set; }
        public decimal? TotalAmountEUR { get; set; }
        public decimal? TotalAmountConvertToEUR { get; set; }
        public decimal? TotalShippedPrice { get; set; }
    }
}
