//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WAYNMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WAYNMng_EmployeeList_View
    {
        public int WAYNID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string EmployeeUD { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<System.DateTime> WorkingDate { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public Nullable<bool> IsOutOfOffice { get; set; }
        public string Description { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> LeaveTypeID { get; set; }
        public Nullable<bool> IsHaftDayOff { get; set; }
    }
}
