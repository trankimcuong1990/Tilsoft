using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningOverviewRpt.DTO
{
    public class WeeklyOverviewDTO
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public int WeekInfoID { get; set; }
        public DateTime Weekstart { get; set; }
        public decimal Capacity { get; set; }
        public decimal TotalOrderQnt { get; set; }
        public decimal PreOrderQnt { get; set; }
        public decimal PreProducedQnt { get; set; }
    }
}
