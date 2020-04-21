using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardTotalHitPerWeekOfFactoryData
    {
        public int WeekIndex { get; set; }
        public int? TotalHitOfFactory { get; set; }
        public decimal? AVGOfFactory { get; set; }
    }
}
