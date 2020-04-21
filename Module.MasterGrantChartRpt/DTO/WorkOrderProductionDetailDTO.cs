using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterGrantChartRpt.DTO
{
    public class WorkOrderProductionDetailDTO
    {
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ProductionTeamID { get; set; }

        public string WorkCenterNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public int? SequenceIndexNumber { get; set; }
        public decimal? NormQnt { get; set; }
        public decimal? ReceivedQnt { get; set; }
        public decimal? ProducedQnt { get; set; }
        public decimal? RemainQnt { get; set; }
    }
}
