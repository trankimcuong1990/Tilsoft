//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Support.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupportMng_PurchaseOrderDetailApproveProductionItemUnit_View
    {
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> ConversionFactor { get; set; }
        public long PrimaryID { get; set; }
    
        public virtual SupportMng_PurchaseOrderDetailApprove_View SupportMng_PurchaseOrderDetailApprove_View { get; set; }
    }
}
