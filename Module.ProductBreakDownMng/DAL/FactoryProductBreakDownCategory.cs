//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductBreakDownMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryProductBreakDownCategory
    {
        public int FactoryProductBreakDownCategoryID { get; set; }
        public Nullable<int> ProductBreakDownCategoryID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual ProductBreakDownCategory ProductBreakDownCategory { get; set; }
    }
}
