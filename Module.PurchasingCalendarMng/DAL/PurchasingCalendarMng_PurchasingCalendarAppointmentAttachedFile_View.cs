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
    
    public partial class PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View
    {
        public int PurchasingCalendarAppointmentAttachedFileID { get; set; }
        public Nullable<int> PurchasingCalendarAppointmentID { get; set; }
        public string FileUD { get; set; }
        public Nullable<int> PurchasingCalendarAppointmentAttachedFileTypeID { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string AppointmentAttachedFileTypeNM { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<bool> HasLink { get; set; }
    
        public virtual PurchasingCalendarMng_PurchasingCalendarAppointment_View PurchasingCalendarMng_PurchasingCalendarAppointment_View { get; set; }
    }
}
