//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Booking2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasingInvoiceSampleDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchasingInvoiceSampleDetail()
        {
            this.PackingListSampleDetail = new HashSet<PackingListSampleDetail>();
        }
    
        public int PurchasingInvoiceSampleDetailID { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> LoadingPlanSampleDetailID { get; set; }
        public Nullable<decimal> FactoryPrice { get; set; }
    
        public virtual PurchasingInvoice PurchasingInvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackingListSampleDetail> PackingListSampleDetail { get; set; }
    }
}