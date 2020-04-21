using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOM.DTO
{
    public class EditFormData
    {
        public DTO.BOMDTO Data { get; set; }
        //public List<Module.Support.DTO.OPSequenceDetail> OPSequenceDetails { get; set; }
        public List<Module.Support.DTO.ProductionItemType> ProductionItemTypes { get; set; }
        public List<Module.Support.DTO.ProductionTeam> ProductionTeams { get; set; }
        public List<DTO.DatePrices> DatePrices { get; set; }
        public List<SupportWorkOrderOPSequenceDTO> OPSequenceDetails { get; set; }
    }
}
