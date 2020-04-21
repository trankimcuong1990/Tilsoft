using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIEurofarPriceOverviewRpt.DTO
{
    public class SalePerformanceDTO
    {
        public int SaleID { get; set; }
        public string SaleUD { get; set; }
        public string SaleNM { get; set; }
        public string DefaultColor { get; set; }
        public Nullable<decimal> PiDelta5AmountThisYear { get; set; }
        public Nullable<decimal> PiPurchasingAmountThisYear { get; set; }
        public Nullable<decimal> PiSaleAmountThisYear { get; set; }
        public Nullable<decimal> PiDelta5PercentThisYear { get; set; }
        public Nullable<decimal> PiContThisYear { get; set; }
        public Nullable<decimal> PiCommissionAmountInUSDThisYear { get; set; }
        public Nullable<decimal> PiDamagesCostInUSDThisYear { get; set; }
        public Nullable<decimal> PiLCCostAmountInUSDThisYear { get; set; }
        public Nullable<decimal> PiInterestAmountInUSDThisYear { get; set; }
        public Nullable<decimal> PiTransportationInUSDThisYear { get; set; }
        public Nullable<decimal> PiBonusUSDThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5AmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedPurchasingAmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedSaleAmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5PercentThisYear { get; set; }
        public Nullable<decimal> PiConfirmedContThisYear { get; set; }
        public Nullable<decimal> PiConfirmedCommissionAmountInUSDThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDamagesCostInUSDThisYear { get; set; }
        public Nullable<decimal> PiConfirmedLCCostAmountInUSDThisYear { get; set; }
        public Nullable<decimal> PiConfirmedInterestAmountInUSDThisYear { get; set; }
        public Nullable<decimal> PiConfirmedTransportationInUSDThisYear { get; set; }
        public Nullable<decimal> PiConfirmedBonusUSDThisYear { get; set; }

    }
}
