//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AgendaMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AgendaMng_Appointment_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AgendaMng_Appointment_View()
        {
            this.AgendaMng_AppointmentAttachedFile_View = new HashSet<AgendaMng_AppointmentAttachedFile_View>();
            this.AgendaMng_AgendaMngUser_View = new HashSet<AgendaMng_AgendaMngUser_View>();
        }
    
        public int AppointmentID { get; set; }
        public string AppointmentUD { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> UserID { get; set; }
        public int PersonInChargeID { get; set; }
        public string PersonInChargeNM { get; set; }
        public string Subject { get; set; }
        public Nullable<int> MeetingLocationID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> RemindTime { get; set; }
        public string Description { get; set; }
        public string MeetingMinute { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string OwnerNM { get; set; }
        public string MeetingLocationNM { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<bool> HasLink { get; set; }
        public string FlightDetail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaMng_AppointmentAttachedFile_View> AgendaMng_AppointmentAttachedFile_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaMng_AgendaMngUser_View> AgendaMng_AgendaMngUser_View { get; set; }
    }
}