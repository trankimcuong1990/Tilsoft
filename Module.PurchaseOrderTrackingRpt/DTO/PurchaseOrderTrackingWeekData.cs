using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt.DTO
{
    public class PurchaseOrderTrackingWeekData
    {
        public int? WeekIndex { get; set; }

        public string WeekStart { get; set; }

        public string WeekEnd { get; set; }
    }
}
