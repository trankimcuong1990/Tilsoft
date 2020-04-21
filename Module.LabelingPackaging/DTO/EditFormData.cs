using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    public class EditFormData
    {
        public LabelingPackaging Data { get; set; }
        public List<Module.Support.DTO.LabelingPackagingStatus> LPStatuses { get; set; }
        public List<DTO.ApprovedDetail> tmpDTODetails { get; set; }
        public List<DTO.ApprovedSparepartDetail> tmpDTOSparepartDetails { get; set; }
        public List<DTO.OptionsDTO> options { get; set; }
    }
}
