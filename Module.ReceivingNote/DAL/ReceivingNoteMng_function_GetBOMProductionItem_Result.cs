//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ReceivingNote.DAL
{
    using System;
    
    public partial class ReceivingNoteMng_function_GetBOMProductionItem_Result
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string UnitNM { get; set; }
        public Nullable<int> FactoryWarehouseID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> BOMID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public decimal BOMQnt { get; set; }
        public Nullable<decimal> BOMQntTotal { get; set; }
        public string WorkOrderUD { get; set; }
        public Nullable<int> ParentProductionItemID { get; set; }
        public string ParentProductionItemUD { get; set; }
        public string ParentProductionItemNM { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal FrameWeight { get; set; }
        public Nullable<decimal> StockQnt { get; set; }
        public Nullable<decimal> TotalReceive { get; set; }
    }
}