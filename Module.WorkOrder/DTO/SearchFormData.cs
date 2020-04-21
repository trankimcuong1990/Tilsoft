using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class SearchFormData
    {
        public List<WorkOrderSearchDTO> Data { get; set; }
        public List<Module.Support.DTO.WorkOrderStatus> WorkOrderStatuses { get; set; }
        public List<DTO.WeavingStatus> WeavingStatus { get; set; }
        public decimal? TotalWorkOrderQnt { get; set; }
        public List<DTO.ProductionItemGroupDTO> productionItemGroupDTOs { get; set; }
    }
}
