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
    
    public partial class TaskMng_Task_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskMng_Task_View()
        {
            this.TaskMng_TaskFile_View = new HashSet<TaskMng_TaskFile_View>();
            this.TaskMng_TaskStep_View = new HashSet<TaskMng_TaskStep_View>();
        }
    
        public int TaskID { get; set; }
        public string TaskUD { get; set; }
        public string TaskNM { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EstEndDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> CompletedPercent { get; set; }
        public Nullable<int> TaskStatusID { get; set; }
        public Nullable<bool> NotificationEnabled { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string TaskStatusNM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskMng_TaskFile_View> TaskMng_TaskFile_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskMng_TaskStep_View> TaskMng_TaskStep_View { get; set; }
    }
}
