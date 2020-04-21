using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DTO.Overview
{
    public class UserDetailDTO
    {
        public int? ModuleID { get; set; }
        public string DisplayText { get; set; }
        public int? TotalHit { get; set; }
        public string ControllerName { get; set; }
    }
}
