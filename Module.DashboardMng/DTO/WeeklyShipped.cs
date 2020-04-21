using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class WeeklyShipped
    {
        public int FactoryID { get; set; }
        public int WeekIndex { get; set; }
        public decimal TotalShippedQnt40HC { get; set; }
        public decimal TotalShippedQnt40HCLastSeason { get; set; }
        public decimal? TotalProducedQnt40HC { get; set; }
        public decimal? TotalShippCont { get; set; }
    }
}
