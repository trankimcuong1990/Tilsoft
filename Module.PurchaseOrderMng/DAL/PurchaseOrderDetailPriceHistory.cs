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
    
    public partial class PurchaseOrderDetailPriceHistory
    {
        public int PurchaseOrderDetailPriceHistoryID { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public decimal UnitPrice { get; set; }
        public int ApprovedBy { get; set; }
        public System.DateTime ApprovedDate { get; set; }
    
        public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }
    }
}