using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptProductionRpt.DTO
{
    public class InitForm
    {
        public List<Support.DTO.WorkCenter> WorkCenters { get; set; }
        public List<Support.DTO.ProductionTeam> ProductionTeams { get; set; }
        public List<Support.DTO.FactoryWarehouseDto> FactoryWarehouses { get; set; }
    }
}
