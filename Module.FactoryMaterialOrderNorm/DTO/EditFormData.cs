using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialOrderNorm.DTO
{
    public class EditFormData
    {
        public FactoryMaterialOrderNorm Data { get; set; }
        public List<Module.Support.DTO.Unit> Units { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
        public List<Module.Support.DTO.FactoryGoodsProcedure> FactoryGoodsProcedures { get; set; }
    }
}
