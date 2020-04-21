using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class SubTotalDTO
    {
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalOrderQnt40HC { get; set; }
        public decimal? SaleAmountEUR { get; set; }
        public decimal? SaleAmountUSD { get; set; }
        public decimal? SaleAmountConvertedUSD { get; set; }
        public decimal? PurchasingAmountUSD { get; set; }
        public decimal? TargetPurchasingAmountUSD { get; set; }
        public decimal? CommissionUSD { get; set; }
        public decimal? TransportationUSD { get; set; }
        public decimal? BonusUSD { get; set; }
        public decimal? InterestUSD { get; set; }
        public decimal? LCCostUSD { get; set; }
        public decimal? DamageUSD { get; set; }
        public decimal? TargetDamageUSD { get; set; }
        public decimal? Delta6Amount { get; set; }
        public decimal? TargetDelta6Amount { get; set; }
        public bool IsAdditionalDataLoaded { get; set; }
    }
}
