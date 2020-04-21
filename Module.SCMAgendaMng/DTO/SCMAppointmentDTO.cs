using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class SCMAppointmentDTO
    {
        public int SCMAppointmentID { get; set; }
        public string SCMAppointmentUD { get; set; }
        public int? ClientID { get; set; }
        public int? UserID { get; set; }
        public int? PersonInChargeID { get; set; }
        public string PersonInChargeNM { get; set; }
        public string Subject { get; set; }
        public int? MeetingLocationID { get; set; }
        public int? FactoryID { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public string RemindDate { get; set; }
        public string RemindTime { get; set; }
        public string FlightDetail { get; set; }
        public string Description { get; set; }
        public string MeetingMinute { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string OwnerNM { get; set; }
        public string MeetingLocationNM { get; set; }
        public string FactoryUD { get; set; }

        // Updated By, UpdatedDate
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasLink { get; set; }

        public List<DTO.SCMAppointmentUserDTO> SCMAppointmentUserDTOs { get; set; }
        public List<DTO.SCMAppointmentAttachedFileDTO> SCMAppointmentAttachedFileDTOs { get; set; }
        public List<DTO.SCMInspectionDTO> SCMInspectionDTOs { get; set; }
    }
}
