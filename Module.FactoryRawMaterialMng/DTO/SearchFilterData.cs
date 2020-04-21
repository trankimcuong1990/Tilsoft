using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.FactoryLocation> Locations { get; set; }
        public List<DTO.KeyRawMaterial> KeyRawMaterials { get; set; }
    }
}
