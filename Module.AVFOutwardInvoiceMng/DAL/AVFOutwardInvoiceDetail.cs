//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AVFOutwardInvoiceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AVFOutwardInvoiceDetail
    {
        public int AVFOutwardInvoiceDetailID { get; set; }
        public Nullable<int> AVFOutwardInvoiceID { get; set; }
        public Nullable<int> DetailIndex { get; set; }
        public string Description { get; set; }
        public Nullable<int> VAT { get; set; }
        public decimal USD { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
    
        public virtual AVFOutwardInvoice AVFOutwardInvoice { get; set; }
    }
}