//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseRequestMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseRequestMng_PurchaseRequestWorkOrderDetail_View
    {
        public int PurchaseRequestWorkOrderDetailID { get; set; }
        public Nullable<int> PurchaseRequestDetailID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<decimal> RequestedQnt { get; set; }
        public string Remark { get; set; }
        public string WorkOrderUD { get; set; }
    
        public virtual PurchaseRequestMng_PurchaseRequestDetail_View PurchaseRequestMng_PurchaseRequestDetail_View { get; set; }
    }
}
