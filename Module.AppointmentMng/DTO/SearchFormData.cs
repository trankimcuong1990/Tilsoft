using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AppointmentMng.DTO
{
    public class SearchFormData
    {
        public List<AppointmentDTO> Data { get; set; }
        public List<string> TimeRange { get; set; }
        public List<Support.DTO.MeetingLocation> MeetingLocations { get; set; }
    }
}
