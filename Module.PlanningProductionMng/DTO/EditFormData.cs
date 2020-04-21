using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningProductionMng.DTO
{
    public class EditFormData
    {
        public PlanningProductionDTO Data { get; set; }
        public WorkOrderDTO WorkOrder { get; set; }
        public List<WorkCenterDTO> WorkCenters { get; set; }
    }
}
