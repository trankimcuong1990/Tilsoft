using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPerformanceRpt.DTO
{
    public class TotalCapacityAndOrderData
    {
        public int? FactoryID { get; set; }
        public decimal? TotalCapacity { get; set; }
        public decimal? TotalOrderQnt { get; set; }
        public decimal? TotalPreOrderQnt { get; set; }
        public decimal? TotalPreProducedQnt { get; set; }
        public decimal? TotalDeltaQnt
        {
            get
            {
                return TotalCapacity + TotalPreProducedQnt - (TotalOrderQnt + TotalPreOrderQnt);
            }
        }
        public string FactoryUD { get; set; }
    }
}
