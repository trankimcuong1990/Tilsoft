//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BreakDownMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BreakdownCategoryOptionDetail
    {
        public int BreakdownCategoryOptionDetailID { get; set; }
        public Nullable<int> BreakdownCategoryOptionID { get; set; }
        public string Description { get; set; }
        public string DescriptionEN { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> QuantityPercent { get; set; }
        public Nullable<decimal> WastedRatePercent { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> BreakdownCategoryOptionGroupID { get; set; }
    
        public virtual BreakdownCategoryOption BreakdownCategoryOption { get; set; }
        public virtual BreakdownCategoryOptionGroup BreakdownCategoryOptionGroup { get; set; }
    }
}
