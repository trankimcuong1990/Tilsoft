using System.Collections.Generic;

namespace Module.ClientComplaint.DTO
{
    public class EditFormData
    {
        public ClientComplaint Data { get; set; }

        public List<Support.DTO.Employee> Employees { get; set; }

        public List<FactoryOrderDetailItem> FactoryOrderDetailItems { get; set; }

        public List<Support.DTO.ConstantEntry> ComplaintTypes { get; set; }

        public List<Support.DTO.ConstantEntry> ComplaintStatuses { get; set; }
    }
    
}
