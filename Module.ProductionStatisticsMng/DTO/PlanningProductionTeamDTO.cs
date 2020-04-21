using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionStatisticsMng.DTO
{
    public class PlanningProductionTeamDTO
    {        
        public int? PlanningProductionTeamID { get; set; }
        public object RowIndex { get; set; }
        public string ClientUD { get; set; }
        public string ProductUD { get; set; }
        public string ProductNM { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProductionTeamNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int ProductionStatisticsDetailID { get; set; }
    }
}
