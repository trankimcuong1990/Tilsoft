using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVTPurchasingPriceMng.DTO
{
    public class AVTBreakdownCategoryOptionDTO
    {
        public int? BreakdownCategoryOptionID { get; set; }
        public int? BreakdownID { get; set; }
        public int? ModelID { get; set; }
        public int? OfferLineID { get; set; }
        public int? BreakdownCategoryID { get; set; }
        public int? CompanyID { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public bool? IsDefaultOption { get; set; }
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public int? MaterialColorID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? FrameMaterialColorID { get; set; }
        public int? SubMaterialID { get; set; }
        public int? SubMaterialColorID { get; set; }
        public int? CushionTypeID { get; set; }
        public int? BackCushionID { get; set; }
        public int? SeatCushionID { get; set; }
        public int? CushionColorID { get; set; }
        public int? FSCTypeID { get; set; }
        public int? FSCPercentID { get; set; }
        public int? PackagingMethodID { get; set; }
        public string PackagingMethodNM { get; set; }
        public string Description { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? LoadAbilityAVF { get; set; }
        public int? LoadAbilityAVT { get; set; }
    }
}
