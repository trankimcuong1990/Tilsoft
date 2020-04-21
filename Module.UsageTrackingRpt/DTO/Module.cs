using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UsageTrackingRpt.DTO
{
    public class Module
    {
        public int ModuleID { get; set; }
        public string DisplayText { get; set; }
        public int ParentID { get; set; }
        public int DisplayOrder { get; set; }
    }
}
