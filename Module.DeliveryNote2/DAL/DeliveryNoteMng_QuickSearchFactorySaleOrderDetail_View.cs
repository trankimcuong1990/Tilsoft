//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DeliveryNote2.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeliveryNoteMng_QuickSearchFactorySaleOrderDetail_View
    {
        public int FactorySaleOrderDetailID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> FactoryWarehouseID { get; set; }
        public Nullable<decimal> StockQnt { get; set; }
        public Nullable<decimal> TotalDelivery { get; set; }
    }
}
