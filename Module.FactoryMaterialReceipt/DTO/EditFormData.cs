using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialReceipt.DTO
{
    public class EditFormData
    {
        public FactoryMaterialReceipt Data { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
        public List<Module.Support.DTO.FactoryTeam> FactoryTeams { get; set; }
        public List<Module.Support.DTO.FactoryArea> FactoryAreas { get; set; }
        public List<Module.Support.DTO.FactoryGoodsProcedureDetail> FactoryGoodsProcedureDetails { get; set; }
    }
}
