//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.TaskMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskMng_TaskStepUser_View
    {
        public int TaskStepUserID { get; set; }
        public Nullable<int> TaskStepID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Description { get; set; }
        public Nullable<int> TaskUserRoleID { get; set; }
        public string FullName { get; set; }
        public string OfficeNM { get; set; }
        public string TaskRoleNM { get; set; }
    
        public virtual TaskMng_TaskStep_View TaskMng_TaskStep_View { get; set; }
    }
}
