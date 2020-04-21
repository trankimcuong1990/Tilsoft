using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryOrderNorm.DTO
{
    public class EditFormData
    {
        public FactoryOrderNorm Data { get; set; }
        public List<Module.Support.DTO.Unit> Units { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
        public List<Module.Support.DTO.FactoryGoodsProcedure> FactoryGoodsProcedures { get; set; }
        public List<Module.Support.DTO.MaterialGroupType> MaterialGroupTypes { get; set; }
    }
}
