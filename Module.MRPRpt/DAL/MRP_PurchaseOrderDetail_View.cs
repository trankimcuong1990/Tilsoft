//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MRPRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MRP_PurchaseOrderDetail_View
    {
        public long KeyID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<System.DateTime> PlannedETA { get; set; }
        public Nullable<decimal> PlannedReceivingQnt { get; set; }
        public Nullable<int> ProductionItemGroupID { get; set; }
    
        public virtual MRP_ProductionItem_View MRP_ProductionItem_View { get; set; }
    }
}
