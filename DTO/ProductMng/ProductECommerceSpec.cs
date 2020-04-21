using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class ProductECommerceSpec
    {
        public int ProductECommerceSpecID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string Warranty { get; set; }
        public Nullable<int> NumberOfSeat { get; set; }
        public Nullable<decimal> NetWeight { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> OverallDimL { get; set; }
        public Nullable<decimal> OverallDimW { get; set; }
        public Nullable<decimal> OverallDimH { get; set; }
        public Nullable<decimal> SeatDimL { get; set; }
        public Nullable<decimal> SeatDimW { get; set; }
        public Nullable<decimal> SeatDimH { get; set; }
        public Nullable<decimal> ArmDimL { get; set; }
        public Nullable<decimal> ArmDimW { get; set; }
        public Nullable<decimal> ArmDimH { get; set; }
        public Nullable<decimal> LongSideTableLegs { get; set; }
        public Nullable<decimal> ShortSideTableLegs { get; set; }
        public Nullable<decimal> SeatBackDimH { get; set; }
        public Nullable<decimal> SeatLegsDimH { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> TableLegsMaterialColorID { get; set; }
        public Nullable<bool> HasArmRest { get; set; }
        public Nullable<bool> IncludeCushion { get; set; }
        public Nullable<bool> CanAdjustDegree { get; set; }
        public Nullable<int> DegreeAdjust { get; set; }
        public Nullable<decimal> BackCushionThinkNess { get; set; }
        public Nullable<decimal> SeatCushionThinkNess { get; set; }
        public Nullable<int> NumberOfCushion { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public string FillingCushion { get; set; }
        public Nullable<int> CushionTypeID { get; set; }
        public Nullable<bool> AllSeasonCushion { get; set; }
        public decimal? LowestPointToGroundHeight { get; set; }
        public int? LegMaterialID { get; set; }
        public int? LegMaterialColorID { get; set; }
    }
}
