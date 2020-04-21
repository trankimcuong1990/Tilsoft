//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WEXStockOverviewRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WEXStockOverviewRpt_ProductSearchResult_View
    {
        public long PrimaryID { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string ProductTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string SubEANCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<int> StockQnt { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VVPPrice { get; set; }
        public Nullable<decimal> SalePriceTemp { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<int> Obsolete { get; set; }
        public Nullable<decimal> ValueObsolescence { get; set; }
        public string ItemSource { get; set; }
        public Nullable<int> ItemSourceID { get; set; }
        public Nullable<bool> NoImage { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ImageFile { get; set; }
        public string Warranty { get; set; }
        public Nullable<int> NumberOfSeat { get; set; }
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public string SeatDimL { get; set; }
        public string SeatDimW { get; set; }
        public string SeatDimH { get; set; }
        public string ArmDimL { get; set; }
        public string ArmDimW { get; set; }
        public string ArmDimH { get; set; }
        public Nullable<decimal> LongSideTableLegs { get; set; }
        public Nullable<decimal> ShortSideTableLegs { get; set; }
        public Nullable<decimal> SeatBackDimH { get; set; }
        public Nullable<decimal> SeatLegsDimH { get; set; }
        public string MaterialNM { get; set; }
        public string FrameMaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string FSCTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public Nullable<bool> HasArmRest { get; set; }
        public Nullable<bool> IncludeCushion { get; set; }
        public Nullable<bool> CanAdjustDegree { get; set; }
        public Nullable<int> DegreeAdjust { get; set; }
        public string BackCushionThinkNess { get; set; }
        public string SeatCushionThinkNess { get; set; }
        public Nullable<int> NumberOfCushion { get; set; }
        public string CushionColorNM { get; set; }
        public string FillingCushion { get; set; }
        public string CushionTypeNM { get; set; }
        public Nullable<bool> AllSeasonCushion { get; set; }
        public Nullable<decimal> LowestPointToGroundHeight { get; set; }
        public string LegMaterialNM { get; set; }
        public string LegMaterialColorNM { get; set; }
        public string BrandName { get; set; }
        public Nullable<int> CataloguePageNo { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<int> WexItemID { get; set; }
        public Nullable<int> OtherSystemItemID { get; set; }
        public Nullable<int> FreeToSaleQnt { get; set; }
    }
}