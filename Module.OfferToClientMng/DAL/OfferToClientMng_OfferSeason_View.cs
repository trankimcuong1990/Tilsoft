//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OfferToClientMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferToClientMng_OfferSeason_View
    {
        public string Season { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> CommissionPercent { get; set; }
        public Nullable<decimal> TransportationFee { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string Remark { get; set; }
        public int OfferSeasonID { get; set; }
        public string OfferSeasonUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientUD { get; set; }
        public int ClientID { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
