//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryProformaInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryProformaInvoice
    {
        public FactoryProformaInvoice()
        {
            this.FactoryProformaInvoiceDetail = new HashSet<FactoryProformaInvoiceDetail>();
        }
    
        public int FactoryProformaInvoiceID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string AttachedFile { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
    
        public virtual ICollection<FactoryProformaInvoiceDetail> FactoryProformaInvoiceDetail { get; set; }
    }
}