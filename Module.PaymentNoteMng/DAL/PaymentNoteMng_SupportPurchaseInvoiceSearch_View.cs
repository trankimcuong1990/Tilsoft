//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PaymentNoteMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentNoteMng_SupportPurchaseInvoiceSearch_View
    {
        public long KeyID { get; set; }
        public int PurchaseInvoiceID { get; set; }
        public string PurchaseInvoiceUD { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> PurchaseInvoiceDate { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> TotalPurchaseInvoice { get; set; }
        public Nullable<decimal> TotalPayment { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public Nullable<decimal> TotalRealDeposit { get; set; }
        public string Remark { get; set; }
    }
}
