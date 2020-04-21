using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample3Mng.DTO.SampleOrderOverview
{
    public class MonitorDTO
    {
        public int SampleMonitorID { get; set; }
        public int UserID { get; set; }
        public int SampleMonitorGroupID { get; set; }
        public string FullName { get; set; }
        public string InternalCompanyNM { get; set; }
    }
}
