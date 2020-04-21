using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DefectsMng.DTO
{
    public class SupportFormData
    {
        public List<DTO.DefectsGroup> DefectsGroups { get; set; }
        public List<DTO.TypeOfDefectDTO> TypeOfDefectDTO { get; set; }
    }
}
