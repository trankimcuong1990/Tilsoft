using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WEXStockOverviewRpt.DTO
{
    public class ProductEcommerceSpecPieceDetailDTO
    {
        public int PieceID { get; set; }
        public string ProductTypeNM { get; set; }
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
    }
}
