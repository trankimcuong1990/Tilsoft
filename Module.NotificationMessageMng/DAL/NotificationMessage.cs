//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.NotificationMessageMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class NotificationMessage
    {
        public int NotificationMessageID { get; set; }
        public string NotificationMessageTag { get; set; }
        public string NotificationMessageTitle { get; set; }
        public string NotificationMessageContent { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> IsSynced { get; set; }
        public Nullable<System.DateTime> SyncedDate { get; set; }
    }
}
