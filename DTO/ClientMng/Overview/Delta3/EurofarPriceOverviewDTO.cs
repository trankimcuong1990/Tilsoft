using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng.Overview.Delta3
{
    public class EurofarPriceOverviewDTO
    {
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public Nullable<decimal> AvgFOBPrice { get; set; }
        public Nullable<decimal> AvgUnitPriceUSD { get; set; }
        public Nullable<int> TotalOrderQnt { get; set; }
        public int Qnt40HC { get; set; }
        public Nullable<decimal> TotalOrderedQnt40HC { get; set; }
        public Nullable<decimal> TotalPurchasingAmount { get; set; }
        public Nullable<decimal> TotalSaleAmount { get; set; }
        public Nullable<decimal> GrossMargin { get; set; }
        public Nullable<decimal> GrossMarginPercent { get; set; }
        public decimal? LCCostAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public decimal? DamagesCost { get; set; }
        public decimal? TransportCost { get; set; }
    }
    
}
