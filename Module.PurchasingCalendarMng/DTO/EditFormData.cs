using System.Collections.Generic;

namespace Module.PurchasingCalendarMng.DTO
{
    public class EditFormData
    {
        public PurchasingCalendarAppointmentDTO Data { get; set; }
        public List<EmployeeDepartmentDTO> EmployeeDepartmentDTOs { get; set; }
    }
}
