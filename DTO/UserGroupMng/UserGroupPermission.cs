using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.UserGroupMng
{
    public class UserGroupPermission
    {
        public int UserGroupPermissionID { get; set; }

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
        public int TmpDisplayOrder { get; set; }
    }
}