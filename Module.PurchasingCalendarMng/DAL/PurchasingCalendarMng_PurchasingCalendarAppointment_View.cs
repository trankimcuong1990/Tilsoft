//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchasingCalendarMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasingCalendarMng_PurchasingCalendarAppointment_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchasingCalendarMng_PurchasingCalendarAppointment_View()
        {
            this.PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View = new HashSet<PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View>();
            this.PurchasingCalendarMng_PurchasingCalendarUser_View = new HashSet<PurchasingCalendarMng_PurchasingCalendarUser_View>();
        }
    
        public int PurchasingCalendarAppointmentID { get; set; }
        public string PurchasingCalendarAppointmentUD { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> UserID { get; set; }
        public int PersonInChargeID { get; set; }
        public string PersonInChargeNM { get; set; }
        public string Subject { get; set; }
        public Nullable<int> MeetingLocationID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> RemindTime { get; set; }
        public string FlightDetail { get; set; }
        public string Description { get; set; }
        public string MeetingMinute { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string OwnerNM { get; set; }
        public string MeetingLocationNM { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<bool> HasLink { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View> PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchasingCalendarMng_PurchasingCalendarUser_View> PurchasingCalendarMng_PurchasingCalendarUser_View { get; set; }
    }
}
