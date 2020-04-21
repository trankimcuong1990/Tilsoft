using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardWeeklyProduced
    {
        public int FactoryID { get; set; }
        public int WeekIndex { get; set; }
        public decimal? TotalProducedQnt40HC { get; set; }
        public decimal? TotalProducedQnt40HCLastSeason { get; set; }
    }
}
