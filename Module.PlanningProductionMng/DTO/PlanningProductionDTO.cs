using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningProductionMng.DTO
{
    public class PlanningProductionDTO
    {
        public int? PlanningProductionID { get; set; }
        public int? ParentPlanningProductionID { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public int? BOMID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionItemID { get; set; }
        public bool? IsDeleted { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public decimal? PlanQnt { get; set; }
        public decimal? ReceivedQnt { get; set; }
        public decimal? ProducedQnt { get; set; }
        public decimal? RemainQnt { get; set; }
        public string WorkCenterNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public int? PlanningTime { get; set; }
        public int? WorkingTime { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderStartDate { get; set; }
        public string WorkOrderFinishDate { get; set; }

        public List<PlanningProductionDTO> PlanningProductionDTOs { get; set; }
        public List<PlanningProductionTeamDTO> PlanningProductionTeamDTOs { get; set; }
        public List<ArisingProductionTeamDTO> ArisingProductionTeamDTOs { get; set; }
    }
}
