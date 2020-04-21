using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionScheduleRpt.DTO
{
    public class ProductionSchedule
    {
        public object KeyID { get; set; }
        public int? BOMID { get; set; }
        public int? ParentBOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionTeamID { get; set; }
        public int? ProductionItemID { get; set; }

        public int? OPSequenceDetailID { get; set; }
        public string WorkCenterNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public int? SequenceIndexNumber { get; set; }
        public int? NextOPSequenceDetailID { get; set; }
        public int? NextWorkCenterID { get; set; }
        public int? NextProductionTeamID { get; set; }

        public string WorkOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public int? WorkOrderQnt { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public decimal? ProductionQnt { get; set; }
        public decimal? ProducedQnt { get; set; }
        public decimal? RemainQnt { get; set; }

        //input fields
        public decimal? PutInProductQnt { get; set; }
        public int? ToProductionTeamID { get; set; }
        public int? ToFactoryWarehouseID { get; set; }
    }
}
