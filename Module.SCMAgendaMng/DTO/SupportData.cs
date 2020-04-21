using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class SupportData
    {
        public List<Module.Support.DTO.Factory> Factories { get; set; }
        public List<DTO.MeetingLocation> MeetingLocations { get; set; }
        public List<Module.Support.DTO.User2> Users { get; set; }
        public List<string> TimeRange { get; set; }
        public List<Module.Support.DTO.AppointmentAttachedFileType> SCMAppointmentAttachedFileTypes { get; set; }
        public List<DTO.Sale> Sales { get; set; }
        public List<Module.Support.DTO.EmployeeDepartmentDTO> employeeDepartmentDTOs { get; set; }
    }
}
