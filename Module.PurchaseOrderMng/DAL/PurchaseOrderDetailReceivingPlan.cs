//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseOrderMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseOrderDetailReceivingPlan
    {
        public int PurchaseOrderDetailReceivingPlanID { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public System.DateTime PlannedETA { get; set; }
        public decimal PlannedReceivingQnt { get; set; }
        public string Remark { get; set; }
    
        public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }
    }
}