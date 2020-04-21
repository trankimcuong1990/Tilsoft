using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostRpt.DTO
{
    public class InitDTO
    {
        public InitDTO()
        {
            productionTeams = new List<Support.DTO.ProductionTeam>();
            clients = new List<Support.DTO.Client>();
        }
        public List<Support.DTO.ProductionTeam> productionTeams { get; set; }
        public List<Support.DTO.Client> clients { get; set; }
    }
}
