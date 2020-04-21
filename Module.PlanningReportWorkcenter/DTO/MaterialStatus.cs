using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningReportWorkcenter.DTO
{
    public class MaterialStatus
    {
        public long KeyID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ProductionItemGroupID { get; set; }
        public decimal? needMaterial { get; set; }
        public decimal? storageMaterial { get; set; }
    }
}
