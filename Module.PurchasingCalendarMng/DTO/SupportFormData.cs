using System.Collections.Generic;

namespace Module.PurchasingCalendarMng.DTO
{
    public class SupportFormData
    {      
        public List<MeetingLocationDTO> MeetingLocationDTOs { get; set; }
        public List<Module.Support.DTO.User2> Users { get; set; }
        public List<string> TimeRange { get; set; }
        public List<Module.Support.DTO.AppointmentAttachedFileType> AppointmentAttachedFileTypes { get; set; }
        public List<SaleDTO> Sales { get; set; }
        public List<EmployeeDepartmentDTO> EmployeeDepartmentDTOs { get; set; }
    }
}
