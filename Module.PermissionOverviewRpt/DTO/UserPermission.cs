using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PermissionOverviewRpt.DTO
{
    public class UserPermission
    {
        public int UserId { get; set; }
        public int ModuleID { get; set; }
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string UserGroupNM { get; set; }
        public string Email { get; set; }
        public string OfficeNM { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public bool CanApprove { get; set; }
        public bool CanReset { get; set; }
        public int UserGroupID { get; set; }
        public int OfficeID { get; set; }
    }
}
