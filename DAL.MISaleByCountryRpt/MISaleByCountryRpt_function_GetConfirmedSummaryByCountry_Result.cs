//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.MISaleByCountryRpt
{
    using System;
    
    public partial class MISaleByCountryRpt_function_GetConfirmedSummaryByCountry_Result
    {
        public string Season { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public string ClientCodeForLog { get; set; }
        public Nullable<decimal> TotalCont { get; set; }
        public Nullable<decimal> TotalEURAmount { get; set; }
        public Nullable<decimal> USDAmount { get; set; }
        public Nullable<decimal> EURAmount { get; set; }
        public Nullable<int> TotalClient { get; set; }
    }
}
