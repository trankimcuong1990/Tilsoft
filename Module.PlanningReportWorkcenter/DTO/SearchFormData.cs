using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningReportWorkcenter.DTO
{
    public class SearchFormData
    {
        public List<WorkOrderDTO> Data { get; set; }
        public List<WeekInfoDTO> WeekInfoDTOs { get; set; }
        public List<ReceivingDetailDTO> ReceivingDetailDTOs { get; set; }
        public List<ReceivingSetDetailDTO> ReceivingSetDetailDTOs { get; set; }
        public List<WorkcenterStatus> WorkcenterStatus { get; set; }
        public List<MaterialStatus> MaterialStatus { get; set; }
    }
}
