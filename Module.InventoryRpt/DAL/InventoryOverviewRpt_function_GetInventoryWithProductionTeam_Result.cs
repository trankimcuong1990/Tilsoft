//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.InventoryRpt.DAL
{
    using System;
    
    public partial class InventoryOverviewRpt_function_GetInventoryWithProductionTeam_Result
    {
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> ProductionTeamID { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> TotalBeginning { get; set; }
        public Nullable<decimal> TotalReceiving { get; set; }
        public Nullable<decimal> TotalDelivering { get; set; }
        public Nullable<decimal> TotalEndingInventory { get; set; }
    }
}