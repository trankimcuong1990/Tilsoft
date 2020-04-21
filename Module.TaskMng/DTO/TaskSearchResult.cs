using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DTO
{
    public class TaskSearchResult
    {
        public int TaskID { get; set; }
        public bool? NotificationEnabled { get; set; }
        public string TaskStatusNM { get; set; }
        public string TaskUD { get; set; }
        public string TaskNM { get; set; }
        public string StartDate { get; set; }
        public string EstEndDate { get; set; }
        public string EndDate { get; set; }
        public int? CompletedPercent { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? TaskStatusID { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
