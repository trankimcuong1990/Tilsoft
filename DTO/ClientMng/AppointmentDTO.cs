using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public string Subject { get; set; }
        public string EmployeeNM { get; set; }
        public string MeetingLocationNM { get; set; }
        public string FactoryUD { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public int TotalAttachedFile { get; set; }
        public int UserID { get; set; }
    }
}
