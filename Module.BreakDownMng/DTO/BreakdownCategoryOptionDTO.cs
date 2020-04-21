using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownCategoryOptionDTO
    {
        public int BreakdownCategoryOptionID { get; set; }
        public int? BreakdownID { get; set; }
        public int? BreakdownCategoryID { get; set; }
        public bool IsDefaultOption { get; set; }
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
        public int? ClientSpecialPackagingMethodID { get; set; }
        public int? LoadAbility { get; set; }
        public string SpecialRequirement { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string MaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }
        public string FrameMaterialNM { get; set; }
        public string FrameMaterialColorNM { get; set; }
        public string SubMaterialNM { get; set; }
        public string SubMaterialColorNM { get; set; }
        public string CushionTypeNM { get; set; }
        public string BackCushionNM { get; set; }
        public string SeatCushionNM { get; set; }
        public string CushionColorNM { get; set; }
        public string FSCTypeNM { get; set; }
        public string FSCPercentNM { get; set; }
        public string PackagingMethodNM { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public bool IsSelected { get; set; }
        public int CompanyID { get; set; }
        public string ClientSpecialPackagingMethodNM { get; set; }

        public List<DTO.BreakdownCategoryOptionDetailDTO> BreakdownCategoryOptionDetailDTOs { get; set; }
        public List<DTO.BreakdownCategoryOptionImageDTO> BreakdownCategoryOptionImageDTOs { get; set; }
        public List<DTO.BreakdownCategoryOptionGroupDTO> BreakdownCategoryOptionGroupDTOs { get; set; }
    }
}
