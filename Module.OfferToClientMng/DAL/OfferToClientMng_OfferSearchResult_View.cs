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
    
    public partial class OfferToClientMng_OfferSearchResult_View
    {
        public int OfferID { get; set; }
        public string OfferUD { get; set; }
        public Nullable<System.DateTime> OfferDate { get; set; }
        public Nullable<int> OfferVersion { get; set; }
        public string Season { get; set; }
        public Nullable<System.DateTime> ValidUntil { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string PaymentTermNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string Currency { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string ForwarderNM { get; set; }
        public string PODNM { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string QntRemark { get; set; }
        public Nullable<int> V4ID { get; set; }
        public string TrackingStatusNM { get; set; }
        public Nullable<int> TrackingStatusID { get; set; }
        public Nullable<bool> IsPotential { get; set; }
        public Nullable<int> SaleID { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string ApproverName { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
    }
}