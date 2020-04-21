using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LeaveRequestMng.DTO
{
    public class EditFormData
    {
        public Module.LeaveRequestMng.DTO.LeaveRequest Data { get; set; }
        public List<DTO.Manager> Managers { get; set; }
        public List<DTO.Director> Directors { get; set; }
        public List<Module.Support.DTO.LeaveType> LeaveTypes { get; set; }
        public List<Module.Support.DTO.LeaveRequestTime> LeaveRequestTimes { get; set; }
    }
}
