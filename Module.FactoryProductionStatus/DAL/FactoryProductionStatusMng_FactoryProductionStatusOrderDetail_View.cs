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
    
    public partial class FactoryProductionStatusMng_FactoryProductionStatusOrderDetail_View
    {
        public int FactoryProductionStatusOrderDetailID { get; set; }
        public Nullable<int> FactoryProductionStatusDetailID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> ProducedQnt { get; set; }
        public Nullable<decimal> ProducedCont { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<decimal> OrderInCont { get; set; }
        public Nullable<int> TotalProducedLastWeek { get; set; }
        public Nullable<int> OutputProducedQnt { get; set; }
        public Nullable<int> TotalOutputProducedLastWeek { get; set; }
    
        public virtual FactoryProductionStatusMng_FactoryProductionStatusDetail_View FactoryProductionStatusMng_FactoryProductionStatusDetail_View { get; set; }
    }
}
