using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningReportWorkcenter.DTO
{
    public class ReceivingSetDetailDTO
    {
        public long KeyID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? WeekInfoID { get; set; }
        public string ReceivingNoteDate { get; set; }
        public decimal TotalReceiving { get; set; }
        public string ProductionItemNM { get; set; }
    }
}
