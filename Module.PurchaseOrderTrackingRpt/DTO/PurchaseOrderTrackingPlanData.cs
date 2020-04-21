using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt.DTO
{
    public class PurchaseOrderTrackingPlanData
    {
        public int? PurchaseOrderDetailID { get; set; }

        public int? WeekIndex { get; set; }

        public decimal? QntPlan { get; set; }

        public int? PurchaseOrderID { get; set; }

        public int? ProductionItemID { get; set; }
    }
}
