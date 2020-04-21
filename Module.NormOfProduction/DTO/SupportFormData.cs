using System.Collections.Generic;

namespace Module.NormOfProduction.DTO
{
    public class SupportFormData
    {
        public List<Support.DTO.WorkCenter> SupportWorkCenterDTOs { get; set; }
        public List<Support.DTO.WorkOrderStatus> SupportWorkOrderStatusDTOs { get; set; }
    }
}
