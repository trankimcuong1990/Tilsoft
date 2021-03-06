//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseRequestMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseRequestDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseRequestDetail()
        {
            this.PurchaseRequestWorkOrderDetail = new HashSet<PurchaseRequestWorkOrderDetail>();
            this.PurchaseRequestDetailApproval = new HashSet<PurchaseRequestDetailApproval>();
        }
    
        public int PurchaseRequestDetailID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> SuggestedFactoryRawMaterialID { get; set; }
        public Nullable<decimal> RequestQnt { get; set; }
        public Nullable<System.DateTime> ETA { get; set; }
        public string Remark { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseRequestWorkOrderDetail> PurchaseRequestWorkOrderDetail { get; set; }
        public virtual PurchaseRequest PurchaseRequest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseRequestDetailApproval> PurchaseRequestDetailApproval { get; set; }
    }
}
