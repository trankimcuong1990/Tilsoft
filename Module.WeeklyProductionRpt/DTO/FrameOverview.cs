using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WeeklyProductionRpt.DTO
{
    public class FrameOverview
    {
        public int? WeekIndex { get; set; }
        public string WeekStart { get; set; }
        public string WeekEnd { get; set; }
        public decimal? Capacity { get; set; }
        public decimal? PlanCont { get; set; }
        public decimal? RealCont { get; set; }
        public decimal? DeltaCont { get; set; }
    }
}
