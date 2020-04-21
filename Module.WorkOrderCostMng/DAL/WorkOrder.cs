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
    
    public partial class WorkOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrder()
        {
            this.WorkOrder1 = new HashSet<WorkOrder>();
            this.WorkOrderCost = new HashSet<WorkOrderCost>();
        }
    
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> OPSequenceID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> WorkOrderTypeID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
        public Nullable<int> WorkOrderStatusID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<decimal> WastagePercent { get; set; }
        public Nullable<int> SetStatusBy { get; set; }
        public Nullable<System.DateTime> SetStatusDate { get; set; }
        public Nullable<bool> IsDefaultOfModel { get; set; }
        public Nullable<int> SampleOrderID { get; set; }
        public Nullable<int> PreOrderWorkOrderID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrder> WorkOrder1 { get; set; }
        public virtual WorkOrder WorkOrder2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrderCost> WorkOrderCost { get; set; }
    }
}
