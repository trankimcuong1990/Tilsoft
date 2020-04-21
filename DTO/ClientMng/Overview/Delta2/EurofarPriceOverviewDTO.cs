using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Delta2
{
    public class EurofarPriceOverviewDTO
    {
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public decimal? AvgFOBPrice { get; set; }
        public decimal? AvgUnitPriceUSD { get; set; }
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalPurchasingAmount { get; set; }
        public decimal? TotalSaleAmount { get; set; }
        public decimal? GrossMargin { get; set; }
        public decimal? GrossMarginPercent { get; set; }
        public int Qnt40HC { get; set; }
        public decimal? TotalOrderedQnt40HC { get; set; }
        public decimal? LCCostAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public decimal? DamagesCost { get; set; }
        public decimal? TransportCost { get; set; }
    }
}
