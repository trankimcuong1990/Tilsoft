//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactorySalesOrderMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactorySaleOrderDetail
    {
        public int FactorySaleOrderDetailID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<decimal> VATPercent { get; set; }
        public Nullable<int> FactoryWarehouseID { get; set; }
        public string Remark { get; set; }
    
        public virtual FactorySaleOrder FactorySaleOrder { get; set; }
    }
}
