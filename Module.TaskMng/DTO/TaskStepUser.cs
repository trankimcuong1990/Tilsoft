using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DTO
{
    public class TaskStepUser
    {
        public int TaskStepUserID { get; set; }
        public int? UserID { get; set; }
        public string Description { get; set; }
        public int? TaskUserRoleID { get; set; }
        public string FullName { get; set; }
        public string TaskRoleNM { get; set; }
        public string OfficeNM { get; set; }
    }
}
