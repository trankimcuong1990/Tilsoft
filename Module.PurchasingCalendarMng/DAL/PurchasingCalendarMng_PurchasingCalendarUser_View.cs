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
    
    public partial class PurchasingCalendarMng_PurchasingCalendarUser_View
    {
        public int PurchasingCalendarUserID { get; set; }
        public Nullable<int> PurchasingCalendarAppointmentID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<bool> PIC { get; set; }
        public Nullable<bool> IsReceivePriceRequest { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual PurchasingCalendarMng_PurchasingCalendarAppointment_View PurchasingCalendarMng_PurchasingCalendarAppointment_View { get; set; }
    }
}
