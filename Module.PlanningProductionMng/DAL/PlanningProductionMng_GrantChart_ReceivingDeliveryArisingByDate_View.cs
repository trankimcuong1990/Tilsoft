//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PlanningProductionMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlanningProductionMng_GrantChart_ReceivingDeliveryArisingByDate_View
    {
        public long KeyID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> ProductionTeamID { get; set; }
        public string ArisingDate { get; set; }
        public Nullable<decimal> ReceivedQnt { get; set; }
        public Nullable<decimal> ProducedQnt { get; set; }
        public string ProductionTeamNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
    }
}