using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt.DTO
{
    public class PurchaseOrderTrackingActualData
    {
        public int? PurchaseOrderID { get; set; }

        public int? WorkOrderID { get; set; }

        public int? WeekIndex { get; set; }

        public decimal? QntActual { get; set; }
    }
}
