//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ReportFreeToSale
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportMng_FreeToSaleOverview_View
    {
        public string KeyID { get; set; }
        public Nullable<int> GoodsID { get; set; }
        public string ProductType { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public string ArticleCode { get; set; }
        public string SelectedThumbnailImage { get; set; }
        public string SelectedFullImage { get; set; }
        public string Description { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string CushionNM { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public Nullable<int> FTSQnt { get; set; }
        public Nullable<decimal> FTSQntIn40HC { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<bool> IsActiveFreeToSale { get; set; }
        public bool IsMatchedImage { get; set; }
        public string FreeToSaleCategoryNM { get; set; }
        public string CushionImage { get; set; }
        public string MaterialImage { get; set; }
        public Nullable<int> Qnt20DC { get; set; }
        public Nullable<int> Qnt40DC { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string ProductThumbnailImage { get; set; }
        public string ProductGardenThumbnailImage { get; set; }
        public string ModelThumbnailImage { get; set; }
        public string ProductFullImage { get; set; }
        public string ProductGardenFullImage { get; set; }
        public string ModelFullImage { get; set; }
    }
}
