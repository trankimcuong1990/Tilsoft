//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryQuotationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryQuotationMng_QuotationDetailSearchResult_View
    {
        public long PrimaryID { get; set; }
        public string ClientUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string QuotationStatusNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int OrderQnt { get; set; }
        public Nullable<decimal> PurchasingPrice { get; set; }
        public Nullable<int> QuotationID { get; set; }
        public Nullable<int> StatusID { get; set; }
    }
}
