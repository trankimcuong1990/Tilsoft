//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AVFPurchasingInvoiceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AVFPurchasingInvoiceDetail
    {
        public int AVFPurchasingInvoiceDetailID { get; set; }
        public Nullable<int> AVFPurchasingInvoiceID { get; set; }
        public Nullable<int> DetailIndex { get; set; }
        public string Description { get; set; }
        public decimal USD { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> DebitAccountID { get; set; }
        public Nullable<int> CreditAccountID { get; set; }
    
        public virtual AVFPurchasingInvoice AVFPurchasingInvoice { get; set; }
    }
}
