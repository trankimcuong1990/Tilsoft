using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterProductionScheduleRpt.DTO
{
    public class MasterProductionSchedule
    {
        public object KeyID { get; set; }
        public int? BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionTeamID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? NormQnt { get; set; }
        public int? WorkOrderQnt { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public decimal? PercentComplete { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public decimal? ProductionQnt { get; set; }
        public decimal? ProducedQnt { get; set; }
        public decimal? RemainQnt { get; set; }
        public string WorkCenterNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public int? SequenceIndexNumber { get; set; }
    }
}
