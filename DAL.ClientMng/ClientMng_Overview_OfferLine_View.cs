//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientMng_Overview_OfferLine_View
    {
        public long KeyID { get; set; }
        public Nullable<int> OfferID { get; set; }
        public int GoodsID { get; set; }
        public string GoodsType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> SalePriceCalculated { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
        public Nullable<decimal> OrderQntIn40HC { get; set; }
        public Nullable<decimal> FinalPrice { get; set; }
        public Nullable<decimal> VATAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Currency { get; set; }
    }
}
