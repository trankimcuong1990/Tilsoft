using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestOverviewRpt.DTO
{
    public class DetailByPerson
    {
        public int DevRequestID { get; set; }
        public string Title { get; set; }
        public int? Priority { get; set; }
        public string DevRequestPriorityNM { get; set; }
        public string UpdatedDate { get; set; }
        public int? DevRequestStatusID { get; set; }
        public string DevRequestStatusNM { get; set; }
        public int? EstWorkingHour { get; set; }
        public int? ActualWorkingHour { get; set; }
    }
}
