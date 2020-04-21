using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CheckListMng.DTO
{
    public class SupportFormData
    {
        public List<DTO.CheckListGroup> CheckListGroups { get; set; }
        public List<DTO.TypeOfInspectionDTO> TypeOfInspectionDTO { get; set; }
    }
}
