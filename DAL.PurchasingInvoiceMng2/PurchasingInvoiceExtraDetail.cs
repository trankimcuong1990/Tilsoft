//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PurchasingInvoiceMng2
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasingInvoiceExtraDetail
    {
        public int PurchasingInvoiceExtraDetailID { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }
        public Nullable<int> PurchasingInvoiceDetailID { get; set; }
        public Nullable<int> PurchasingInvoiceSparepartDetailID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> FactoryPrice { get; set; }
        public string Remark { get; set; }
    
        public virtual PurchasingInvoice PurchasingInvoice { get; set; }
        public virtual PurchasingInvoiceDetail PurchasingInvoiceDetail { get; set; }
        public virtual PurchasingInvoiceSparepartDetail PurchasingInvoiceSparepartDetail { get; set; }
    }
}