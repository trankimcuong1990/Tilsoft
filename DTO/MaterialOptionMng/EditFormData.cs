using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MaterialOptionMng
{
    public class EditFormData
    {
        public DTO.MaterialOptionMng.MaterialOption Data { get; set; }
        public List<DTO.Support.Material> Materials { get; set; }
        public List<DTO.Support.MaterialType> MaterialTypes { get; set; }
        public List<DTO.Support.MaterialColor> MaterialColors { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.ProductGroup> ProductGroups { get; set; }
    }
}
