//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WorkOrder.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkOrderMng_BOM_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrderMng_BOM_View()
        {
            this.WorkOrderMng_BOM_View1 = new HashSet<WorkOrderMng_BOM_View>();
        }
    
        public int BOMID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> ParentBOMID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public decimal Quantity { get; set; }
        public int WorkOrderQnt { get; set; }
        public Nullable<decimal> TotalQnt { get; set; }
        public decimal TotalDeliveryQnt { get; set; }
        public decimal TotalReceivedQnt { get; set; }
        public Nullable<decimal> RemaintQnt { get; set; }
        public string Unit { get; set; }
        public string WorkCenterNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public string Description { get; set; }
        public decimal QtyByUnit { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string UnitNM { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public decimal StockQnt { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
        public Nullable<int> ProductionTeamID { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public Nullable<decimal> WastagePercent { get; set; }
        public Nullable<decimal> Delta { get; set; }
        public decimal StockQntPAL_1 { get; set; }
        public decimal StockQntPAL_2 { get; set; }
        public Nullable<int> BranchID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrderMng_BOM_View> WorkOrderMng_BOM_View1 { get; set; }
        public virtual WorkOrderMng_BOM_View WorkOrderMng_BOM_View2 { get; set; }
    }
}
