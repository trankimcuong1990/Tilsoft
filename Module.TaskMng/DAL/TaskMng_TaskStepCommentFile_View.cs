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
    
    public partial class TaskMng_TaskStepCommentFile_View
    {
        public int TaskStepCommentFileID { get; set; }
        public Nullable<int> TaskStepCommentID { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
    
        public virtual TaskMng_TaskStepComment_View TaskMng_TaskStepComment_View { get; set; }
    }
}
