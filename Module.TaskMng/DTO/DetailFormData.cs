using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DTO
{
    public class DetailFormData
    {
        public DTO.TaskStep Data { get; set; }
        public List<DTO.TaskStepComment> TaskStepComments { get; set; }
        public List<Support.DTO.TaskStatus> TaskStatuses { get; set; }
    }
}
