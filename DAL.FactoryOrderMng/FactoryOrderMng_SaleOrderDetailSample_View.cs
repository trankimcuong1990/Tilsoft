//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryOrderMng_SaleOrderDetailSample_View
    {
        public int SaleOrderDetailSampleID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> OfferLineSampleProductID { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<int> OfferSeasonTypeID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<decimal> OrderQntIn40HC { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public string PackagingMethodText { get; set; }
    
        public virtual FactoryOrderMng_SaleOrder_View FactoryOrderMng_SaleOrder_View { get; set; }
    }
}
