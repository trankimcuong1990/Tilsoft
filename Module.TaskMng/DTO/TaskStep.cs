using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DTO
{
    public class TaskStep
    {
        public int TaskStepID { get; set; }
        public int? StepIndex { get; set; }
        public string Description { get; set; }
        public int? StepStatusID { get; set; }
        public string TaskStatusNM { get; set; }

        public List<TaskStepUser> TaskStepUsers { get; set; }
        public string TaskDescription { get; set; }
    }
}
