//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionIssue.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionIssueMng_HistoryTransferProductionTeam_View
    {
        public long PrimaryID { get; set; }
        public int WorkOrderID { get; set; }
        public int WorkCenterID { get; set; }
        public int ProductionItemID { get; set; }
        public int ProductionTeamID { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string ProductionTeamNM { get; set; }
        public Nullable<int> BOMID { get; set; }
    }
}