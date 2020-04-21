//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DraftCommercialInvoiceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DraftCommercialInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DraftCommercialInvoice()
        {
            this.DraftCommercialInvoiceDescription = new HashSet<DraftCommercialInvoiceDescription>();
            this.DraftCommercialInvoiceDetail = new HashSet<DraftCommercialInvoiceDetail>();
        }
    
        public int DraftCommercialInvoiceID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientAddress { get; set; }
        public string InvoiceNo { get; set; }
        public string ClientInvoiceNo { get; set; }
        public string RefNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> AccountNo { get; set; }
        public string Conditions { get; set; }
        public string LCRefNo { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> VATRate { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string Remark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DraftCommercialInvoiceDescription> DraftCommercialInvoiceDescription { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DraftCommercialInvoiceDetail> DraftCommercialInvoiceDetail { get; set; }
    }
}
