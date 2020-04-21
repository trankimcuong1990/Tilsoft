using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningOverviewRpt.DTO
{
    public class WeekInfoDTO
    {
        public int WeekInfoID { get; set; }
        public string Season { get; set; }
        public int WeekIndex { get; set; }
        public string WeekStart { get; set; }
        public string WeekEnd { get; set; }
    }
}
