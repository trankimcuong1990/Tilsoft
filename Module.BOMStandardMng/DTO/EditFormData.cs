using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class EditFormData
    {
        public DTO.BOMStandardDTO Data { get; set; }
        public List<Module.Support.DTO.OPSequenceDetail> OPSequenceDetails { get; set; }
        public List<Module.Support.DTO.ProductionItemType> ProductionItemTypes { get; set; }
        public List<Module.Support.DTO.ProductionTeam> ProductionTeams { get; set; }
        public List<DTO.DateProductionPrice> DateProductionPrices { get; set; }

        public EditFormData()
        {
            Data = new BOMStandardDTO();

            OPSequenceDetails = new List<Support.DTO.OPSequenceDetail>();
            ProductionItemTypes = new List<Support.DTO.ProductionItemType>();
            ProductionTeams = new List<Support.DTO.ProductionTeam>();
            DateProductionPrices = new List<DateProductionPrice>();
        }
    }
}
