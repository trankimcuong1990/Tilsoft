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
    using System.Collections.Generic;
    
    public partial class ReceivingNoteMng_PurchaseOrderDetail_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReceivingNoteMng_PurchaseOrderDetail_View()
        {
            this.ReceivingNoteMng_PurchaseOrderWorkOrderDetail_View = new HashSet<ReceivingNoteMng_PurchaseOrderWorkOrderDetail_View>();
        }
    
        public int PurchaseOrderDetailID { get; set; }
        public Nullable<int> PurchaseOrderID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> FactoryWarehouseID { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> OrderQnt { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public decimal StockQnt { get; set; }
        public decimal TotalReceive { get; set; }
        public Nullable<int> BranchID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceivingNoteMng_PurchaseOrderWorkOrderDetail_View> ReceivingNoteMng_PurchaseOrderWorkOrderDetail_View { get; set; }
    }
}
