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
    
    public partial class PlanningReportWorkCenter_SetDetail_View
    {
        public long KeyID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> WeekInfoID { get; set; }
        public Nullable<System.DateTime> ReceivingNoteDate { get; set; }
        public decimal TotalReceiving { get; set; }
        public string ProductionItemNM { get; set; }
    }
}
