//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ReceiptNoteMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceiptNoteMng_SupportFactorySaleInvoiceSearch_View
    {
        public int FactorySaleInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TotalReceipt { get; set; }
        public Nullable<decimal> RemainQnt { get; set; }
        public string DocCode { get; set; }
    }
}