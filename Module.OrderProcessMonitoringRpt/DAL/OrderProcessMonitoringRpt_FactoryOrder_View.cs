//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OrderProcessMonitoringRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderProcessMonitoringRpt_FactoryOrder_View
    {
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string ProductionStatus { get; set; }
        public string TrackingStatusNM { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
    
        public virtual OrderProcessMonitoringRpt_SaleOrder_View OrderProcessMonitoringRpt_SaleOrder_View { get; set; }
    }
}
