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
    
    public partial class InventoryOverviewDetailRpt_function_GetInventoryWithFactoryWarehouse_Result
    {
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> ReceivingNoteID { get; set; }
        public Nullable<int> DeliveryNoteID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<int> WarehouseTransferID { get; set; }
        public Nullable<int> FactoryWarehouseID { get; set; }
        public string DocumentNumber { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> ReceivingQnt { get; set; }
        public Nullable<decimal> DeliveringQnt { get; set; }
        public string ViewName { get; set; }
        public long PrimaryID { get; set; }
    }
}
