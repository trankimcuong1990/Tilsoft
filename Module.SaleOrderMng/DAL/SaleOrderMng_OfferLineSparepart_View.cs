//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SaleOrderMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrderMng_OfferLineSparepart_View
    {
        public int OfferLineSparepartID { get; set; }
        public Nullable<int> OfferID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientEANCode { get; set; }
        public Nullable<int> ClientID { get; set; }
    
        public virtual SaleOrderMng_Offer_View SaleOrderMng_Offer_View { get; set; }
    }
}
