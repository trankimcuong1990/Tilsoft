//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryProductionStatus.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryProductionStatusMng_ProductionByWeek_View
    {
        public long KeyID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string Season { get; set; }
        public Nullable<int> WeekNo { get; set; }
        public Nullable<decimal> TotalProducedContainerQnt { get; set; }
    }
}