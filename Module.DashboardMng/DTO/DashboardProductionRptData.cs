using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardProductionRptData
    {
        public List<DTO.DashboardWeeklyShipped> WeeklyShippedData { get; set; }
        public List<DTO.DashboardWeeklyProduced> WeeklyProducedData { get; set; }
        public List<WeekInfo> WeekInfoData { get; set; }
    }
}
