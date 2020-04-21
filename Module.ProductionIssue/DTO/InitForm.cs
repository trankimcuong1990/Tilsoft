using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionIssue.DTO
{
    public class InitForm
    {
        public List<Support.DTO.WorkCenter> SupportWorkCenter { get; set; }
        public List<Support.DTO.ProductionTeam> SupportProductionTeam { get; set; }
        public List<Support.DTO.FactoryWarehouseDto> SupportFactoryWarehouse { get; set; }
    }
}
