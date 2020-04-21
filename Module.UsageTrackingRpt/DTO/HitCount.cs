using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class HitCount
    {
        public int WeekIndex { get; set; }
        public string WeekDisplayText { get; set; }
        public int TotalHit { get; set; }
    }
}
