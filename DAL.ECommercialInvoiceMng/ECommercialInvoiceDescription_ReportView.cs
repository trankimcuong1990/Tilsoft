//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ECommercialInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ECommercialInvoiceDescription_ReportView
    {
        public int ECommercialInvoiceDescriptionID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
    
        public virtual ECommercialInvoice_ReportView ECommercialInvoice_ReportView { get; set; }
    }
}
