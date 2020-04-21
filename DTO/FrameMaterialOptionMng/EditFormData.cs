using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FrameMaterialOptionMng
{
    public class EditFormData
    {
        public DTO.FrameMaterialOptionMng.FrameMaterialOption Data { get; set; }
        public List<DTO.Support.FrameMaterial> FrameMaterials { get; set; }
        public List<DTO.Support.FrameMaterialColor> FrameMaterialColors { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
    }
}
