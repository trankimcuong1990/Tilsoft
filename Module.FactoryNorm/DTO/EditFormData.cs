using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryNorm.DTO
{
    public class EditFormData
    {
        public FactoryNorm Data { get; set; }
        public List<Module.Support.DTO.Unit> Units { get; set; }
        public List<Module.Support.DTO.FactoryGoodsProcedure> FactoryGoodsProcedures { get; set; }
        public List<Module.Support.DTO.MaterialGroupType> MaterialGroupTypes { get; set; }
    }
}
