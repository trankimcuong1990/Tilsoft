//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OfferSeasonMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferSeasonMng_ClientEstimatedAdditionalCost_View
    {
        public int ClientEstimatedAdditionalCostID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Season { get; set; }
        public Nullable<decimal> InterestCostPercent { get; set; }
        public Nullable<decimal> InspectionCostUSD { get; set; }
        public Nullable<decimal> LCCostPercent { get; set; }
        public Nullable<decimal> SampleCostUSD { get; set; }
        public Nullable<decimal> BonusPercent { get; set; }
        public Nullable<decimal> SparepartCostUSD { get; set; }
    
        public virtual OfferSeasonMng_Client_View OfferSeasonMng_Client_View { get; set; }
    }
}