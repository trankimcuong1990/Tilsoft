using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class TotalCapacityAndOrderByWeekData
    {
        public string Season { get; set; }
        public int? WeekIndex { get; set; }
        public string WeekStart { get; set; }
        public string WeekEnd { get; set; }
        public int? FactoryID { get; set; }
        public decimal? Capacity { get; set; }
        public decimal? OrderQnt { get; set; }
        public decimal? PreOrderQnt { get; set; }
        public decimal? PreProducedQnt { get; set; }
        public decimal? DeltaQnt
        {
            get
            {
                return Capacity + PreProducedQnt - (OrderQnt + PreProducedQnt);
            }
        }
    }
}
