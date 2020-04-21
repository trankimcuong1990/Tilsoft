using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AppointmentMng.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public string AppointmentUD { get; set; }
        public int? ClientID { get; set; }
        public int? UserID { get; set; }
        public string Subject { get; set; }
        public int? MeetingLocationID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RemindTime { get; set; }
        public string Description { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string OwnerName { get; set; }
        public string MeetingLocationNM { get; set; }

        public string StartTime_Date { get; set; }
        public string StartTime_Time { get; set; }
        public string EndTime_Date { get; set; }
        public string EndTime_Time { get; set; }
        public string RemindTime_Date { get; set; }
        public string RemindTime_Time { get; set; }

        //
        // calendar attribute
        //
        public string start_string { get; set; }
        public string end_string { get; set; }
        public string title 
        {
            get 
            {
                return this.Subject;
            }
        }
        public string start { get; set; }
        public string end { get; set; }
        public string icon { 
            get 
            {
                if (!MeetingLocationID.HasValue)
                {
                    return "";
                }
                else
                {
                    if (MeetingLocationID.Value == 1 || MeetingLocationID.Value == 2)
                    {
                        return "fa-reply";
                    }
                    else
                    {
                        return "fa-share"; 
                    }
                }
                
            } 
        }
        public string className { get; set; }
    }
}
