//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientMng_DDCDetail_View
    {
        public int DDCDetailID { get; set; }
        public int DDCID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<decimal> MinUSD { get; set; }
        public Nullable<decimal> AvgUSD { get; set; }
        public Nullable<decimal> MaxUSD { get; set; }
        public Nullable<decimal> MinEUR { get; set; }
        public Nullable<decimal> AvgEUR { get; set; }
        public Nullable<decimal> MaxEUR { get; set; }
        public Nullable<decimal> WickerContQty { get; set; }
        public Nullable<decimal> WickerPromoContQty { get; set; }
        public Nullable<decimal> WoodAcaciaContQty { get; set; }
        public Nullable<decimal> WoodTeakContQty { get; set; }
        public Nullable<decimal> ChinaContQty { get; set; }
        public Nullable<decimal> IndoContQty { get; set; }
        public string Remark { get; set; }
        public string Season { get; set; }
    
        public virtual ClientMng_Client_View ClientMng_Client_View { get; set; }
    }
}
