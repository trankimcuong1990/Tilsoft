//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OfferSeasonMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferSeasonMng_Client_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfferSeasonMng_Client_View()
        {
            this.OfferSeasonMng_ClientEstimatedAdditionalCost_View = new HashSet<OfferSeasonMng_ClientEstimatedAdditionalCost_View>();
        }
    
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string PaymentTermNM { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<int> InDays { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<decimal> DownValue { get; set; }
        public string DeliveryTermNM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfferSeasonMng_ClientEstimatedAdditionalCost_View> OfferSeasonMng_ClientEstimatedAdditionalCost_View { get; set; }
    }
}