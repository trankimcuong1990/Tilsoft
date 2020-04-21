using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DTO
{
    public class TaskStepComment
    {
        public int TaskStepCommentID { get; set; }
        public int TaskStepID { get; set; }
        public object Comment { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string OfficeNM { get; set; }

        public List<DTO.TaskStepCommentFile> TaskStepCommentFiles { get; set; }
    }
}
