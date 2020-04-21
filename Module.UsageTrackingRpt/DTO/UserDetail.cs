using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class UserDetail
    {
        public int ModuleID { get; set; }
        public string DisplayText { get; set; }
        public int TotalHit { get; set; }
        public string ControllerName { get; set; }
    }
}
