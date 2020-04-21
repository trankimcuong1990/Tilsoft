using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SubMaterialOptionMng
{
    public class EditFormData
    {
        public SubMaterialOption Data { get; set; }
        public List<Support.SubMaterial> SubMaterials { get; set; }
        public List<Support.SubMaterialColor> SubMaterialColors { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }

        public List<DTO.Support.ProductGroup> ProductGroups { get; set; }
    }
}
