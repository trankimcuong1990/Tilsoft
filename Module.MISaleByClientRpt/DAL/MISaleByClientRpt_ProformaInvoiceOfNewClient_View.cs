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
    
    public partial class MISaleByClientRpt_ProformaInvoiceOfNewClient_View
    {
        public int ClientID { get; set; }
        public string SaleUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientCountryUD { get; set; }
        public Nullable<decimal> PICont { get; set; }
        public Nullable<decimal> PIAmountInUSD { get; set; }
        public Nullable<decimal> DeltaAfterAllInEUR { get; set; }
        public Nullable<decimal> DeltaAfterAllPercent { get; set; }
        public Nullable<decimal> PurchasingAmountInEUR { get; set; }
    }
}