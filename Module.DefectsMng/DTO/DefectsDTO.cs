using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DefectsMng.DTO
{
    public class DefectsDTO
    {
        public DefectsDTO()
        {
            defectsImageDTOs = new List<DefectsImageDTO>();
        }
        public int DefectID { get; set; }
        public string DefectUD { get; set; }
        public string DefectNM { get; set; }
        public int? DefectA { get; set; }       
        public int? DefectGroupID { get; set; }
        public string DefectGroupNM { get; set; }
        public string TypeOfDefectANM { get; set; }
        public string TypeOfDefectBNM { get; set; }
        public string TypeOfDefectCNM { get; set; }
        public int? DefectB { get; set; }
        public int? DefectC { get; set; }
        public string DefectViNM { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }

        public List<DTO.DefectsImageDTO> defectsImageDTOs { get; set; }
    }
}
