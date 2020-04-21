using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningProductionMng.DTO
{
    public class WorkCenterDTO
    {
        public int WorkCenterID { get; set; }
        public string WorkCenterNM { get; set; }
        public int? PlanningTime { get; set; }
        public int? WorkingTime { get; set; }
    }
}
