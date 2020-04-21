using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class ProductOptionFormData
    {
        public List<DTO.MaterialOptionDTO> MaterialOptionDTOs { get; set; }
        public List<DTO.MaterialTypeOptionDTO> MaterialTypeOptionDTOs { get; set; }
        public List<DTO.MaterialColorOptionDTO> MaterialColorOptionDTOs { get; set; }

        public List<DTO.FrameMaterialOptionDTO> FrameMaterialOptionDTOs { get; set; }
        public List<DTO.FrameMaterialColorOptionDTO> FrameMaterialColorOptionDTOs { get; set; }

        public List<DTO.SubMaterialOptionDTO> SubMaterialOptionDTOs { get; set; }
        public List<DTO.SubMaterialColorOptionDTO> SubMaterialColorOptionDTOs { get; set; }

        public List<DTO.CushionTypeOptionDTO> CushionTypeOptionDTOs { get; set; }
        public List<DTO.BackCushionOptionDTO> BackCushionOptionDTOs { get; set; }
        public List<DTO.SeatCushionOptionDTO> SeatCushionOptionDTOs { get; set; }
        public List<DTO.CushionColorOptionDTO> CushionColorOptionDTOs { get; set; }

        public List<DTO.PackagingMethodOptionDTO> PackagingMethodOptionDTOs { get; set; }
        public List<DTO.ClientSpecialPackagingMethodDTO> ClientSpecialPackagingMethodDTOs { get; set; }

        public List<DTO.FSCTypeOptionDTO> FSCTypeOptionDTOs { get; set; }

        public string CurrentSeason { get; set; }
    }
}
