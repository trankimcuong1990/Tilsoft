//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ForwarderInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ForwarderInvoice
    {
        public ForwarderInvoice()
        {
            this.ForwarderInvoiceDetails = new HashSet<ForwarderInvoiceDetail>();
        }
    
        public int ForwarderInvoiceID { get; set; }
        public Nullable<int> BookingID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string AccountNo { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Remark { get; set; }
        public string PDFFileScan { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual ICollection<ForwarderInvoiceDetail> ForwarderInvoiceDetails { get; set; }
    }
}
