//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PlanningReportWorkcenter.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkCenter
    {
        public int WorkCenterID { get; set; }
        public string WorkCenterUD { get; set; }
        public string WorkCenterNM { get; set; }
        public Nullable<decimal> OperatingTime { get; set; }
        public Nullable<decimal> DefaultCost { get; set; }
        public Nullable<decimal> Capacity { get; set; }
        public Nullable<int> ResponsibleBy { get; set; }
        public Nullable<int> DefaultFactoryWarehouseID { get; set; }
        public Nullable<int> PlanningTime { get; set; }
        public Nullable<int> WorkingTime { get; set; }
        public Nullable<bool> IsVirtual { get; set; }
    }
}