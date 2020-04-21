using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionStatisticsMng.DTO
{
    public class ProductionStatisticsDetailDTO
    {
        public int ProductionStatisticsDetailID { get; set; }
        public int? ProductionStatisticsID { get; set; }
        public int? PlanningProductionTeamID { get; set; }
        public string StartingTime { get; set; }
        public string FinishingTime { get; set; }
        public decimal? ProducedQnt { get; set; }
        public decimal? Weight { get; set; }
        public string Remark { get; set; }

        public object RowIndex { get; set; }
        public string ClientUD { get; set; }
        public string ProductUD { get; set; }
        public string ProductNM { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProductionTeamNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
    }
}
