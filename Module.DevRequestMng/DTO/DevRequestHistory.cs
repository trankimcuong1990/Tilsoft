using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DevRequestMng.DTO
{
    public class DevRequestHistory
    {
        public int DevRequestHistoryID { get; set; }
        public int? DevRequestHistoryActionID { get; set; }
        public string ActionDescription { get; set; }
        public string Comment { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ActionUpdatorName { get; set; }
        public string DevRequestHistoryActionNM { get; set; }

        public List<DTO.DevRequestCommentAttachedFile> DevRequestCommentAttachedFiles { get; set; }
    }
}
