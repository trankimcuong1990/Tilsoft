//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseQuotationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseQuotationMng_PurchaseQuotation_view
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseQuotationMng_PurchaseQuotation_view()
        {
            this.PurchaseQuotationMng_PurchaseQuotationDetail_View = new HashSet<PurchaseQuotationMng_PurchaseQuotationDetail_View>();
        }
    
        public int PurchaseQuotationID { get; set; }
        public string PurchaseQuotationUD { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string Currency { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public Nullable<int> PurchaseDeliveryTermID { get; set; }
        public string PurchaseDeliveryTermNM { get; set; }
        public Nullable<int> PurchasePaymentTermID { get; set; }
        public string PurchasePaymentTermNM { get; set; }
        public string AttachedFile { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public string ApproverName { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> CanceledBy { get; set; }
        public string CancellerName { get; set; }
        public Nullable<System.DateTime> CancelelDate { get; set; }
        public Nullable<bool> IsCancelled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseQuotationMng_PurchaseQuotationDetail_View> PurchaseQuotationMng_PurchaseQuotationDetail_View { get; set; }
    }
}