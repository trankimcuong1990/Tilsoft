//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WeeklyProductionRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WeeklyProductionRpt_FrameOverview_View
    {
        public long KeyID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string Season { get; set; }
        public Nullable<int> WeekIndex { get; set; }
        public Nullable<System.DateTime> WeekStart { get; set; }
        public Nullable<System.DateTime> WeekEnd { get; set; }
        public Nullable<decimal> Capacity { get; set; }
        public Nullable<decimal> PlanCont { get; set; }
        public Nullable<decimal> RealCont { get; set; }
        public Nullable<decimal> DeltaCont { get; set; }
    }
}
