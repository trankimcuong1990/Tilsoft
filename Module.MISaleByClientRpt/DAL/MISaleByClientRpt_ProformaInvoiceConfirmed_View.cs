//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MISaleByClientRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MISaleByClientRpt_ProformaInvoiceConfirmed_View
    {
        public int ClientID { get; set; }
        public string SaleUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientCountryUD { get; set; }
        public Nullable<decimal> PiConfirmedContThisYear { get; set; }
        public Nullable<decimal> PiConfirmedSaleAmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5AmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5PercentThisYear { get; set; }
        public Nullable<decimal> PiConfirmedPurchasingAmountThisYear { get; set; }
        public Nullable<decimal> CiAmountInUSDLastYear { get; set; }
        public Nullable<decimal> CiContLastYear { get; set; }
        public Nullable<decimal> PiConfirmedContLastYear { get; set; }
        public Nullable<decimal> PiConfirmedSaleAmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5AmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5PercentLastYear { get; set; }
        public Nullable<decimal> PiConfirmedPurchasingAmountLastYear { get; set; }
    }
}
