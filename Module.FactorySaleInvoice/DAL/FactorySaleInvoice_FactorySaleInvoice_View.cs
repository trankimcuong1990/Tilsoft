//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactorySaleInvoice.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactorySaleInvoice_FactorySaleInvoice_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FactorySaleInvoice_FactorySaleInvoice_View()
        {
            this.FactorySaleInvoice_FactorySaleInvoiceDetail_View = new HashSet<FactorySaleInvoice_FactorySaleInvoiceDetail_View>();
        }
    
        public int FactorySaleInvoiceID { get; set; }
        public string DocCode { get; set; }
        public Nullable<int> FactorySaleInvoiceStatusID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Currency { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatorNM { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorNM { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> VAT { get; set; }
        public string Remark { get; set; }
        public string FactorySaleInvoiceStatusNM { get; set; }
        public Nullable<int> SupplierPaymentTerm { get; set; }
        public string AttachedFile { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string Season { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactorySaleInvoice_FactorySaleInvoiceDetail_View> FactorySaleInvoice_FactorySaleInvoiceDetail_View { get; set; }
    }
}