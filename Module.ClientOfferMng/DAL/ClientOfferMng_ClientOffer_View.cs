//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ClientOfferMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientOfferMng_ClientOffer_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientOfferMng_ClientOffer_View()
        {
            this.ClientOfferMng_ClientOfferConditionDetail_View = new HashSet<ClientOfferMng_ClientOfferConditionDetail_View>();
            this.ClientOfferMng_ClientOfferCostDetail_View = new HashSet<ClientOfferMng_ClientOfferCostDetail_View>();
            this.ClientOfferMng_ClientOfferAdditionalDetail_View = new HashSet<ClientOfferMng_ClientOfferAdditionalDetail_View>();
        }
    
        public int ClientOfferID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public string PaymentTermNM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientOfferMng_ClientOfferConditionDetail_View> ClientOfferMng_ClientOfferConditionDetail_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientOfferMng_ClientOfferCostDetail_View> ClientOfferMng_ClientOfferCostDetail_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientOfferMng_ClientOfferAdditionalDetail_View> ClientOfferMng_ClientOfferAdditionalDetail_View { get; set; }
    }
}