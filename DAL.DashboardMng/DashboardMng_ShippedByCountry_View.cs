//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DashboardMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class DashboardMng_ShippedByCountry_View
    {
        public long PrimaryID { get; set; }
        public string Season { get; set; }
        public string ClientCountryNM { get; set; }
        public string MaterialNM { get; set; }
        public Nullable<decimal> TotalQnt { get; set; }
        public Nullable<decimal> TotalShipped { get; set; }
        public Nullable<decimal> ToBeShipped { get; set; }
    }
}
