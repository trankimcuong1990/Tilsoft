//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotificationTask.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountMng_UserPermission_View
    {
        public int UserPermissionID { get; set; }
        public int UserId { get; set; }
        public Nullable<int> ModuleID { get; set; }
        public Nullable<bool> CanRead { get; set; }
        public Nullable<bool> CanCreate { get; set; }
        public Nullable<bool> CanUpdate { get; set; }
        public Nullable<bool> CanDelete { get; set; }
        public Nullable<bool> CanApprove { get; set; }
        public Nullable<bool> CanReset { get; set; }
        public Nullable<bool> CanPrint { get; set; }
        public string ModuleUD { get; set; }
        public string DisplayText { get; set; }
        public string DisplayImage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string URLLink { get; set; }
        public string Description { get; set; }
        public string NavType { get; set; }
        public Nullable<bool> IsIncludeInNavigation { get; set; }
        public int ParentID { get; set; }
    }
}
