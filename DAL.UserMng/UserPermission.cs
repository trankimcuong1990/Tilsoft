//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.UserMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPermission
    {
        public int UserPermissionID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ModuleID { get; set; }
        public Nullable<bool> CanCreate { get; set; }
        public Nullable<bool> CanRead { get; set; }
        public Nullable<bool> CanUpdate { get; set; }
        public Nullable<bool> CanDelete { get; set; }
        public Nullable<bool> CanPrint { get; set; }
        public Nullable<bool> CanApprove { get; set; }
        public Nullable<bool> CanReset { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
    }
}
