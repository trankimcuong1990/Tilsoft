using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningProductionMng.DTO
{
    public class PlanningProductionTeamDTO
    {
        public int PlanningProductionTeamID { get; set; }
        public int? PlanningProductionID { get; set; }
        public int? ProductionTeamID { get; set; }
        public decimal? PlanQnt { get; set; }
        public string ProductionTeamNM { get; set; }
    }
}
