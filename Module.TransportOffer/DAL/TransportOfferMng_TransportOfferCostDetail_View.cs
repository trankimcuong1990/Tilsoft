//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.TransportOffer.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransportOfferMng_TransportOfferCostDetail_View
    {
        public int TransportOfferCostDetailID { get; set; }
        public Nullable<int> TransportOfferID { get; set; }
        public Nullable<int> TransportCostItemID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> Cost20DC { get; set; }
        public Nullable<decimal> Cost40DC { get; set; }
        public Nullable<decimal> Cost40HC { get; set; }
        public Nullable<int> PODID { get; set; }
        public Nullable<int> POLID { get; set; }
        public string Remark { get; set; }
        public string TransportCostItemNM { get; set; }
        public Nullable<int> TransportCostTypeID { get; set; }
    
        public virtual TransportOfferMng_TransportOffer_View TransportOfferMng_TransportOffer_View { get; set; }
    }
}
