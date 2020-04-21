using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardTotalHitPerWeekOfUserData
    {
        public int WeekIndex { get; set; }
        public decimal? TotalHitOfUser { get; set; }
        public decimal? TargetHitOfAllUser { get; set; }
    }
}
