using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DTO
{
    public class Task
    {
        public int TaskID { get; set; }
        public string TaskUD { get; set; }
        public string TaskNM { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EstEndDate { get; set; }
        public string EndDate { get; set; }
        public decimal? CompletedPercent { get; set; }
        public int? TaskStatusID { get; set; }
        public bool? NotificationEnabled { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string TaskStatusNM { get; set; }

        public List<TaskFile> TaskFiles { get; set; }
        public List<TaskStep> TaskSteps { get; set; }
    }
}
