//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AVTPurchasingPriceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AVTPurchasingPriceMng_RelatedQuotationDetail_View
    {
        public int FactoryPriceHistoryID { get; set; }
        public string Season { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<decimal> PurchasingPrice { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> QuotationDetailID { get; set; }
    }
}