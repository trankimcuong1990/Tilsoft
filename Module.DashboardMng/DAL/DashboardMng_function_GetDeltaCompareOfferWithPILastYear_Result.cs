//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DashboardMng.DAL
{
    using System;
    
    public partial class DashboardMng_function_GetDeltaCompareOfferWithPILastYear_Result
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public decimal TotalPurchaseLastYear { get; set; }
        public Nullable<decimal> TotalMarginLastYear { get; set; }
        public Nullable<decimal> Delta7Percent { get; set; }
        public decimal OfferTotalPurchaseLastYear { get; set; }
        public Nullable<decimal> OfferTotalMarginLastYear { get; set; }
        public Nullable<decimal> OfferDelta7Percent { get; set; }
    }
}