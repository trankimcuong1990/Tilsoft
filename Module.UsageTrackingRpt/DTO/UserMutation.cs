using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class UserMutation
    {
        public int WeekIndex { get; set; }
        public string WeekDisplayText { get; set; }
        public int TotalUser { get; set; }
    }
}
