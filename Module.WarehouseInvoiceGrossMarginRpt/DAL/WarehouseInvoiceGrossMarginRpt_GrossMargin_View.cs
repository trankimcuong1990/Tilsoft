//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WarehouseInvoiceGrossMarginRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehouseInvoiceGrossMarginRpt_GrossMargin_View
    {
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string Currency { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SaleNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string SupplierArt { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> PurchasingPrice { get; set; }
        public Nullable<decimal> PurchasingAmount { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> SaleAmount { get; set; }
        public Nullable<decimal> GrossMargin { get; set; }
        public long PrimaryID { get; set; }
        public string Season { get; set; }
        public string ECInvoiceDate { get; set; }
    }
}
