//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.QuotationMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuotationMng_FactoryOrderDetailSearchResult_View
    {
        public long PrimaryID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryOrderSparepartDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public string FactoryOrderUD { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string ClientUD { get; set; }
    }
}