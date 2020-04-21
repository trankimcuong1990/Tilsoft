using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterial.DTO
{
    public class SearchFormData
    {
        public List<FactoryMaterialSearch> Data { get; set; }
        public List<Support.DTO.FactoryMaterialGroup> FactoryMaterialGroups { get; set; }
    }
}
