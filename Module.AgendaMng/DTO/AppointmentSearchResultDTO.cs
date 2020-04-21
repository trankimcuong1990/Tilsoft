using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AgendaMng.DTO
{
    public class AppointmentSearchResultDTO
    {
        public int AppointmentID { get; set; }
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
