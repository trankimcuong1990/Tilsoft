//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.AnnualLeaveMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnnualLeaveMng_AnnualLeaveTrackingDetail_View
    {
        public int AnnualLeaveTrackingDetailID { get; set; }
        public Nullable<int> AnnualLeaveTrackingID { get; set; }
        public Nullable<int> LeaveTypeID { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> NumberOfDays { get; set; }
    
        public virtual AnnualLeaveMng_AnnualLeaveTracking_View AnnualLeaveMng_AnnualLeaveTracking_View { get; set; }
    }
}
