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
    
    public partial class PurchasingCalendarMng_PurchasingCalendarAppointmentSearchResult_View
    {
        public int PurchasingCalendarAppointmentID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string PersonInChargeNM { get; set; }
        public string OwnerNM { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> RemindTime { get; set; }
        public int TotalAttachedFile { get; set; }
        public Nullable<int> MeetingLocationID { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}
