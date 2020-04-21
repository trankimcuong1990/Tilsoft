using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class MeetingLocation
    {
        public int MeetingLocationID { get; set; }
        public string MeetingLocationNM { get; set; }
        public bool IsSelected { get; set; }
    }
}
