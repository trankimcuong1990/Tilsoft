using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestOverviewRpt.DTO
{
    public class PlaningByPerson
    {
        public int DevRequestID { get; set; }
        public string Title { get; set; }
        public int? Priority { get; set; }
        public int? DevRequestStatusID { get; set; }
        public int? EstWorkingHour { get; set; }

        public string start_string { get; set; }
        public string end_string { get; set; }
    }
}
