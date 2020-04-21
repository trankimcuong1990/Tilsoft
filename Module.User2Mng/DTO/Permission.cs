using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User2Mng.DTO
{
    public class Permission
    {
        public int ModuleID { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public bool CanApprove { get; set; }
        public bool CanReset { get; set; }
        public string DisplayText { get; set; }
        public int DisplayOrder { get; set; }
        public int ParentID { get; set; }


        public int UserPermissionID { get; set; }
        public bool CanReadEditable { get; set; }
        public bool CanCreateEditable { get; set; }
        public bool CanUpdateEditable { get; set; }
        public bool CanDeleteEditable { get; set; }
        public bool CanPrintEditable { get; set; }
        public bool CanApproveEditable { get; set; }
        public bool CanResetEditable { get; set; }
    }
}
