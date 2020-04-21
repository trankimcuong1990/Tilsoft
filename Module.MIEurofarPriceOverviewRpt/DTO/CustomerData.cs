using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIEurofarPriceOverviewRpt.DTO
{
    public class CustomerData
    {
        public object PrimaryID { get; set; }
        public string Season { get; set; }
        public string ClientNM { get; set; }
        public string SaleVNNM { get; set; }
        public string Sale2NM { get; set; }
        public string AgentNM { get; set; }
        public string SaleNM { get; set; }
        public decimal? OrderQnt40HC { get; set; }
        public decimal? FOBCostUSD { get; set; }
        public decimal? SaleAmountEUR { get; set; }
        public decimal? SaleAmountUSD { get; set; }
        public decimal? TotalTransportEUR { get; set; }
        public decimal? FOBAmountUSD { get; set; }
        public decimal? CommissionAmountUSD { get; set; }
        public decimal? ShippedQnt40HC { get; set; }
        public decimal? ToBeShippedQnt40HC { get; set; }
        public decimal? SaleAmountConvertToEUR { get; set; }
        public decimal? CommissionPercentage { get; set; }
        public decimal? DamagesCost { get; set; }
        public decimal? NetGrossMargin { get; set; }
        public decimal? NetGrossMarginInPercentage { get; set; }
        public decimal? ShippedContInPercentage { get; set; }
        public decimal? ToBeShippedContInPercentage { get; set; }
        public decimal? LCCostAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? InspectionCostUSD { get; set; }
        public decimal? SampleCostUSD { get; set; }
        public decimal? TransportationInUSD { get; set; }
    }
}
