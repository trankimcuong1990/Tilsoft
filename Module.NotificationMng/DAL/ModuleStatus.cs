//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.NotificationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModuleStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModuleStatus()
        {
            this.NotificationGroupStatus = new HashSet<NotificationGroupStatus>();
        }
    
        public int ModuleStatusID { get; set; }
        public Nullable<int> ModuleID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusUD { get; set; }
        public string StatusNM { get; set; }
        public Nullable<bool> IsActived { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationGroupStatus> NotificationGroupStatus { get; set; }
    }
}
