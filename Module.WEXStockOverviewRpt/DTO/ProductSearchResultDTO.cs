using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WEXStockOverviewRpt.DTO
{
    public class ProductSearchResultDTO
    {
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string ProductTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string SubEANCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? StockQnt { get; set; }
        public int? FreeToSaleQnt { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? VVPPrice { get; set; }
        public decimal? SalePriceTemp { get; set; }
        public decimal? SalePrice { get; set; }
        public object CostPrice { get; set; }
        public int? Obsolete { get; set; }
        public decimal? ValueObsolescence { get; set; }
        public string ItemSource { get; set; }
        public int? ItemSourceID { get; set; }
        public bool? NoImage { get; set; }
        public int? ProductID { get; set; }
        public string ImageFile { get; set; }
        public string Warranty { get; set; }
        public int? NumberOfSeat { get; set; }
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
        public decimal? LongSideTableLegs { get; set; }
        public decimal? ShortSideTableLegs { get; set; }
        public decimal? SeatBackDimH { get; set; }
        public decimal? SeatLegsDimH { get; set; }
        public string MaterialNM { get; set; }
        public string FrameMaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string FSCTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public bool? HasArmRest { get; set; }
        public bool? IncludeCushion { get; set; }
        public bool? CanAdjustDegree { get; set; }
        public int? DegreeAdjust { get; set; }
        public string BackCushionThinkNess { get; set; }
        public string SeatCushionThinkNess { get; set; }
        public int? NumberOfCushion { get; set; }
        public string CushionColorNM { get; set; }
        public string FillingCushion { get; set; }
        public string CushionTypeNM { get; set; }
        public bool? AllSeasonCushion { get; set; }
        public decimal? LowestPointToGroundHeight { get; set; }
        public string LegMaterialNM { get; set; }
        public string LegMaterialColorNM { get; set; }


        public decimal? ValueIncludeObsolesence { get; set; }
        public bool IsSellingPriceChanged { get; set; }
        public bool IsBuyingPriceChanged { get; set; }

        public bool IsEnabled { get; set; }
        public int WexItemID { get; set; }
        public int OtherSystemItemID { get; set; }
        public bool IsSelected { get; set; }
    }
}
