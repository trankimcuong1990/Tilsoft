using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterProductionScheduleRpt.DTO
{
    public class ReportData
    {
        public List<MasterProductionSchedule> MasterProductionSchedules { get; set; }
        public List<Module.Support.DTO.WorkCenter> WorkCenters { get; set; }
    }
}
