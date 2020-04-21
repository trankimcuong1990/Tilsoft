using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class SCMAppointmentSearchResultDTO
    {
        public int SCMAppointmentID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string ClientNM { get; set; }
        public string PersonInChargeNM { get; set; }
        public string OwnerNM { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RemindTime { get; set; }
        public int TotalAttachedFile { get; set; }
        public string FactoryUD { get; set; }
        public int MeetingLocationID { get; set; }
        public int UserID { get; set; }
    }
}
