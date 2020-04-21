using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PermissionOverviewRpt.DTO
{
    public class UserPermissionStatus
    {
        public int ModuleID { get; set; }
        public int CreateCount { get; set; }
        public int ReadCount { get; set; }
        public int UpdateCount { get; set; }
        public int DeleteCount { get; set; }
        public int PrintCount { get; set; }
        public int ApproveCount { get; set; }
        public int ResetCount { get; set; }

        public List<DTO.UserPermission> Detail { get; set; }
    }
}
