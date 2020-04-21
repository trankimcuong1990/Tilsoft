using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningProductionMng.DTO
{
    public class ArisingProductionTeamDTO
    {
        public int? PlanningProductionID { get; set; }
        public int? ProductionTeamID { get; set; }
        public string ProductionTeamNM { get; set; }
        public decimal? ReceivedQnt { get; set; }
        public decimal? ProducedQnt { get; set; }
    }
}
