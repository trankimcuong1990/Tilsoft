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
    
    public partial class InventoryRpt_function_InventoryFinsihProductDetail_Result
    {
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public Nullable<int> FactoryWarehouseID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public Nullable<decimal> BeginningQnt { get; set; }
        public Nullable<decimal> ReceivingQnt { get; set; }
        public Nullable<decimal> DeliveringQnt { get; set; }
        public long PrimaryID { get; set; }
    }
}
