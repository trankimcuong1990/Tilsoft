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
    
    public partial class NotificationGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NotificationGroup()
        {
            this.NotificationGroupMember = new HashSet<NotificationGroupMember>();
            this.NotificationGroupStatus = new HashSet<NotificationGroupStatus>();
        }
    
        public int NotificationGroupID { get; set; }
        public string NotificationGroupUD { get; set; }
        public string NotificationGroupNM { get; set; }
        public string Description { get; set; }
        public Nullable<int> ModuleID { get; set; }
        public string EmailTemplate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationGroupMember> NotificationGroupMember { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationGroupStatus> NotificationGroupStatus { get; set; }
    }
}
