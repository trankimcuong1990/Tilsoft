//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CostInvoice2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostInvoice2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CostInvoice2()
        {
            this.CostInvoice2Client = new HashSet<CostInvoice2Client>();
            this.CostInvoice2Factory = new HashSet<CostInvoice2Factory>();
        }
    
        public int CostInvoice2ID { get; set; }
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> CostInvoice2CreditorID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Currency { get; set; }
        public Nullable<int> CostInvoice2TypeID { get; set; }
        public string CostInvoice2TypeNM { get; set; }
        public string RelatedDocumentNo { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
        public Nullable<bool> IsChargedToClient { get; set; }
        public Nullable<bool> IsChargedToFactory { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CostInvoice2UD { get; set; }
        public string RelatedDocumentFile { get; set; }
    
        public virtual CostInvoice2Creditor CostInvoice2Creditor { get; set; }
        public virtual CostInvoice2Type CostInvoice2Type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostInvoice2Client> CostInvoice2Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostInvoice2Factory> CostInvoice2Factory { get; set; }
    }
}
