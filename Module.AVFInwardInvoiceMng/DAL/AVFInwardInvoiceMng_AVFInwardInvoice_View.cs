//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AVFInwardInvoiceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AVFInwardInvoiceMng_AVFInwardInvoice_View
    {
        public int AVFPurchasingInvoiceID { get; set; }
        public string SerialNo { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string Season { get; set; }
        public string AVFSupplierNM { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public int TaxRate { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string DebitAccountNo { get; set; }
        public string CreditAccountNo { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
