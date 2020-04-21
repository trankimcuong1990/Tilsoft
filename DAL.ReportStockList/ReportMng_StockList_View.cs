//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ReportStockList
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportMng_StockList_View
    {
        public string KeyID { get; set; }
        public Nullable<int> GoodsID { get; set; }
        public string ProductType { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public string ProductStatusNM { get; set; }
        public string SelectedThumbnailImage { get; set; }
        public string SelectedFullImage { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<int> PhysicalQnt { get; set; }
        public Nullable<decimal> PhysicalQntIn40HC { get; set; }
        public Nullable<int> OnSeaQnt { get; set; }
        public Nullable<int> TobeShippedQnt { get; set; }
        public Nullable<int> ReservationQnt { get; set; }
        public Nullable<int> FTSQnt { get; set; }
        public Nullable<bool> IsActiveFreeToSale { get; set; }
        public Nullable<int> FreeToSaleCategoryID { get; set; }
        public bool IsMatchedImage { get; set; }
        public string FreeToSaleCategoryNM { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string FrameMaterialNM { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public string SubMaterialNM { get; set; }
        public string SubMaterialColorNM { get; set; }
        public string SeatCushionNM { get; set; }
        public string BackCushionNM { get; set; }
        public string CushionColorNM { get; set; }
        public string CushionImage { get; set; }
        public string MaterialImage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string ProductThumbnailImage { get; set; }
        public string ProductGardenThumbnailImage { get; set; }
        public string ModelThumbnailImage { get; set; }
        public string ProductFullImage { get; set; }
        public string ProductGardenFullImage { get; set; }
        public string ModelFullImage { get; set; }
        public Nullable<bool> IsHaveImage { get; set; }
    }
}