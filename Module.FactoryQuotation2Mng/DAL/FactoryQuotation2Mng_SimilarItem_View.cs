//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryQuotation2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryQuotation2Mng_SimilarItem_View
    {
        public int QuotationDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string FactoryUD { get; set; }
        public string PriceDifferenceCode { get; set; }
        public string Season { get; set; }
        public int OfferSeasonDetailID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> StatusID { get; set; }
    }
}