using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class HitCountDataRpt
    {
        public int? WeekIndex { get; set; }
        public int TotalHit { get; set; }
        public int TotalHitLast { get; set; }
        //public string WeekStart { get; set; }
        //public string WeekEnd { get; set; }
    }
}