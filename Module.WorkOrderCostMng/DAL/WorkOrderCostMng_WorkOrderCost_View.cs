//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WorkOrderCostMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkOrderCostMng_WorkOrderCost_View
    {
        public int WorkOrderCostID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public string UnitNM { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemTypeNM { get; set; }
    }
}