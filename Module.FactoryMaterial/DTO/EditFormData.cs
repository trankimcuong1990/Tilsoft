using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterial.DTO
{
    public class EditFormData
    {
        public FactoryMaterial Data { get; set; }
        public List<Support.DTO.FactoryMaterialGroup> FactoryMaterialGroups { get; set; }
        public List<Support.DTO.FactoryMaterialType> FactoryMaterialTypes { get; set; }
        public List<Support.DTO.FactoryMaterialColor> FactoryMaterialColors { get; set; }
        public List<Support.DTO.Unit> Units { get; set; }
    }
}
