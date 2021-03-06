//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseOrderMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseOrderMng_PurchaseOrder_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrderMng_PurchaseOrder_View()
        {
            this.PurchaseOrderMng_PurchaseOrderDetail_View = new HashSet<PurchaseOrderMng_PurchaseOrderDetail_View>();
        }
    
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string Currency { get; set; }
        public Nullable<int> PurchaseRequestID { get; set; }
        public string PurchaseRequestUD { get; set; }
        public Nullable<System.DateTime> ETA { get; set; }
        public string AttachedFile { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string UpdaterName { get; set; }
        public string FullName { get; set; }
        public string ApproverName { get; set; }
        public string PaymentDocuments { get; set; }
        public string RequiredDocuments { get; set; }
        public Nullable<int> SupplierPaymentTermID { get; set; }
        public string SupplierPaymentTermNM { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string Season { get; set; }
        public Nullable<int> PurchaseOrderStatusID { get; set; }
        public string Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderMng_PurchaseOrderDetail_View> PurchaseOrderMng_PurchaseOrderDetail_View { get; set; }
    }
}
