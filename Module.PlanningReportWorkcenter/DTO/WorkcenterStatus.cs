using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningReportWorkcenter.DTO
{
    public class WorkcenterStatus
    {
        public long KeyID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public decimal? TotalReceiving { get; set; }
        public string FinishDate { get; set; }
    }
}
