using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestOverviewRpt.DTO
{
    public class ResolvedRequestByPerson
    {
        public int DevRequestID { get; set; }
        public string Title { get; set; }
        public int? EstWorkingHour { get; set; }
        public int? ActualWorkingHour { get; set; }
    }
}
