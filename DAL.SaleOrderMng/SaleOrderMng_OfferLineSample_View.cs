//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SaleOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrderMng_OfferLineSample_View
    {
        public int OfferLineSampleProductID { get; set; }
        public Nullable<int> OfferID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<int> OfferSeasonTypeID { get; set; }
        public Nullable<int> OfferSeasonDetailQuantity { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> OrderQntIn40HC { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<int> OfferSeasonDetailID { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public string PackagingMethodText { get; set; }
    
        public virtual SaleOrderMng_Offer_View SaleOrderMng_Offer_View { get; set; }
    }
}