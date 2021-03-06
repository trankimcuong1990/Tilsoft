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
    
    public partial class FactorySaleInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FactorySaleInvoice()
        {
            this.FactorySaleInvoiceDetail = new HashSet<FactorySaleInvoiceDetail>();
        }
    
        public int FactorySaleInvoiceID { get; set; }
        public string DocCode { get; set; }
        public Nullable<int> FactorySaleInvoiceStatusID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Currency { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> VAT { get; set; }
        public Nullable<int> SupplierPaymentTerm { get; set; }
        public string AttachedFile { get; set; }
        public string Season { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactorySaleInvoiceDetail> FactorySaleInvoiceDetail { get; set; }
    }
}
