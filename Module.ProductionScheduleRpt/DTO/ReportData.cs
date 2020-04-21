using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionScheduleRpt.DTO
{
    public class ReportData
    {
        public List<DTO.ProductionSchedule> ProductionSchedules { get; set; }
        public List<Module.Support.DTO.WorkCenter> WorkCenters { get; set; }
        public List<Module.Support.DTO.ProductionTeam> ProductionTeams { get; set; }
        public List<Module.Support.DTO.FactoryWarehouseDto> FactoryWarehouseDtos { get; set; }


    }
}
