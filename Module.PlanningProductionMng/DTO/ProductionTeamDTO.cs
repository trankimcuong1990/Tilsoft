using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningProductionMng.DTO
{
    public class ProductionTeamDTO
    {
        public int ProductionTeamID { get; set; }
        public string ProductionTeamUD { get; set; }
        public string ProductionTeamNM { get; set; }
        public int? WorkCenterID { get; set; }
        public bool? IsDefault { get; set; }
    }
}
