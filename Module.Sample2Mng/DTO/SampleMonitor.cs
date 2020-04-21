using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class SampleMonitor
    {
        public int SampleMonitorID { get; set; }
        public int? UserID { get; set; }
        public int? SampleMonitorGroupID { get; set; }
        public string FullName { get; set; }
        public string OfficeNM { get; set; }
    }
}
