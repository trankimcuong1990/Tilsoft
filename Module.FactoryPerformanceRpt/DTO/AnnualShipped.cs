using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPerformanceRpt.DTO
{
    public class AnnualShipped
    {
        public int FactoryID { get; set; }
        public decimal TotalShippedQnt40HC { get; set; }
        public decimal TotalShippedQnt40HCLastSeason { get; set; }
        public decimal? TotalProducedQnt40HC { get; set; }
        public int? TotalShippCont { get; set; }
    }
}
