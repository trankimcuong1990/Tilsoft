//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ShowroomTransferMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceivingNoteDetail
    {
        public int ReceivingNoteDetailID { get; set; }
        public Nullable<int> ReceivingNoteID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> FactoryWarehousePalletID { get; set; }
        public string Description { get; set; }
        public Nullable<int> ToFactoryWarehouseID { get; set; }
        public Nullable<int> BOMID { get; set; }
        public Nullable<int> QNTBarCode { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> QtyByUnit { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public Nullable<int> PurchaseOrderDetailID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<decimal> Weight { get; set; }
    
        public virtual ReceivingNote ReceivingNote { get; set; }
        public virtual FactoryWarehouse FactoryWarehouse { get; set; }
    }
}
