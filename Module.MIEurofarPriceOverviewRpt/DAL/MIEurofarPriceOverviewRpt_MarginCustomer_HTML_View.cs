//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MIEurofarPriceOverviewRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MIEurofarPriceOverviewRpt_MarginCustomer_HTML_View
    {
        public long PrimaryID { get; set; }
        public string Season { get; set; }
        public string ClientNM { get; set; }
        public string SaleVNNM { get; set; }
        public string Sale2NM { get; set; }
        public string AgentNM { get; set; }
        public string SaleNM { get; set; }
        public decimal OrderQnt40HC { get; set; }
        public decimal FOBCostUSD { get; set; }
        public decimal SaleAmountEUR { get; set; }
        public decimal SaleAmountUSD { get; set; }
        public decimal TotalTransportEUR { get; set; }
        public decimal FOBAmountUSD { get; set; }
        public decimal CommissionAmountUSD { get; set; }
        public decimal ShippedQnt40HC { get; set; }
        public decimal ToBeShippedQnt40HC { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal DamagesCost { get; set; }
        public decimal ShippedContInPercentage { get; set; }
        public decimal ToBeShippedContInPercentage { get; set; }
        public Nullable<decimal> LCCostAmount { get; set; }
        public Nullable<decimal> InterestAmount { get; set; }
        public decimal InspectionCostUSD { get; set; }
        public decimal SampleCostUSD { get; set; }
        public decimal TransportationInUSD { get; set; }
    }
}
