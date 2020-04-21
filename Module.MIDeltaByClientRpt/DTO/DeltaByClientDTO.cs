using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIDeltaByClientRpt.DTO
{
    public class DeltaByClientDTO
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int SaleID { get; set; }
        public string SaleUD { get; set; }
        public string Season { get; set; }
        public int? TotalOrderQnt { get; set; }
        public decimal? TotalOrderedQnt40HC { get; set; }
        public decimal? Delta40HC { get; set; }
        public decimal? PurchasingAmountUSD { get; set; }
        public decimal? SaleAmountInUSD { get; set; }
        public decimal? TotalTransportUSD { get; set; }
        public decimal? LCCostAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public decimal? BonusAmount { get; set; }
        public decimal? DamagesCost { get; set; }
        public decimal? GrossMarginAmount { get; set; }
        public decimal? GrossMarginPercent { get; set; }
        public decimal? InspectionCostUSD { get; set; }
        public decimal? SampleCostUSD { get; set; }
        public decimal? SparepartCostUSD { get; set; }
    }
}
