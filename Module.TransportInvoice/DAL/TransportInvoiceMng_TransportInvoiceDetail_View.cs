//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.TransportInvoice.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransportInvoiceMng_TransportInvoiceDetail_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransportInvoiceMng_TransportInvoiceDetail_View()
        {
            this.TransportInvoiceMng_TransportInvoiceContainerDetail_View = new HashSet<TransportInvoiceMng_TransportInvoiceContainerDetail_View>();
        }
    
        public int TransportInvoiceDetailID { get; set; }
        public Nullable<int> TransportInvoiceID { get; set; }
        public Nullable<int> TransportCostItemID { get; set; }
        public Nullable<decimal> SubTotalAmount { get; set; }
        public string Remark { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> OfferPrice { get; set; }
        public Nullable<decimal> AlreadyInvoiced { get; set; }
        public Nullable<int> TransportCostChargeTypeID { get; set; }
    
        public virtual TransportInvoiceMng_TransportInvoice_View TransportInvoiceMng_TransportInvoice_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransportInvoiceMng_TransportInvoiceContainerDetail_View> TransportInvoiceMng_TransportInvoiceContainerDetail_View { get; set; }
    }
}
