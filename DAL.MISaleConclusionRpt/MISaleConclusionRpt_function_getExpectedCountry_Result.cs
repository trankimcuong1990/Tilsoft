//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.MISaleConclusionRpt
{
    using System;
    
    public partial class MISaleConclusionRpt_function_getExpectedCountry_Result
    {
        public long PrimaryID { get; set; }
        public Nullable<int> ClientCountryID { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public Nullable<decimal> TotalExpectedAmount { get; set; }
        public Nullable<decimal> TotalExpectedCont { get; set; }
        public Nullable<int> TotalClient { get; set; }
        public Nullable<decimal> TotalExpectedAmountLastYear { get; set; }
        public Nullable<decimal> TotalCommercialInvoiceAmountLastYear { get; set; }
    }
}
