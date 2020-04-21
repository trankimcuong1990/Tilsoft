using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestMng.DTO
{
    public class DevRequestAssignment
    {
        public int DevRequestAssignmentID { get; set; }
        public string AssignedPersonName { get; set; }
        public bool? IsPersonInCharge { get; set; }
        public int AssignedTo { get; set; }
        public int? EstWorkingHour { get; set; }
        public int? ActualWorkingHour { get; set; }
    }
}
