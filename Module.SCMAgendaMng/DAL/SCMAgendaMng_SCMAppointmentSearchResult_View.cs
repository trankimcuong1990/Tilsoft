//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SCMAgendaMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SCMAgendaMng_SCMAppointmentSearchResult_View
    {
        public int SCMAppointmentID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string ClientNM { get; set; }
        public string PersonInChargeNM { get; set; }
        public string OwnerNM { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> RemindTime { get; set; }
        public int TotalAttachedFile { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> MeetingLocationID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}