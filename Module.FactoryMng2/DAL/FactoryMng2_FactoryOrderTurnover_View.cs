//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryMng2.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMng2_FactoryOrderTurnover_View
    {
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string ClientUD { get; set; }
        public string ProductionStatus { get; set; }
        public string TrackingStatusNM { get; set; }
        public Nullable<decimal> Turnover { get; set; }
        public string Season { get; set; }
    }
}