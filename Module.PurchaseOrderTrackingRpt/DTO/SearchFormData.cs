using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt.DTO
{
    public class SearchFormData
    {
        public SearchFormData()
        {
            PurchaseOrderTracking = new List<PurchaseOrderTrackingData>();
            PurchaseOrderTrackingPlan = new List<PurchaseOrderTrackingPlanData>();
            PurchaseOrderTrackingActual = new List<PurchaseOrderTrackingActualData>();
            PurchaseOrderTrackingWeek = new List<PurchaseOrderTrackingWeekData>();
        }

        public List<PurchaseOrderTrackingData> PurchaseOrderTracking { get; set; }

        public List<PurchaseOrderTrackingPlanData> PurchaseOrderTrackingPlan { get; set; }

        public List<PurchaseOrderTrackingActualData> PurchaseOrderTrackingActual { get; set; }

        public List<PurchaseOrderTrackingWeekData> PurchaseOrderTrackingWeek { get; set; }
    }
}
