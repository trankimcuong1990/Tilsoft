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
    
    public partial class OfferStatus
    {
        public int OfferStatusID { get; set; }
        public Nullable<int> OfferID { get; set; }
        public Nullable<int> TrackingStatusID { get; set; }
        public Nullable<System.DateTime> StatusDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<bool> IsCurrentStatus { get; set; }
    
        public virtual Offer Offer { get; set; }
    }
}
