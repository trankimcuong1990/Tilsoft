using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AccountMng
{
    public class UserPermission
    {
        public int ModuleID { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public bool CanApprove { get; set; }
        public bool CanReset { get; set; }
        public string ModuleUD { get; set; }
        public string DisplayText { get; set; }
        public string DisplayImage { get; set; }
        public int DisplayOrder { get; set; }
        public string URLLink { get; set; }
        public string Description { get; set; }
        public string NavType { get; set; }
        public bool IsIncludeInNavigation { get; set; }
        public int ParentID { get; set; }
    }
}
