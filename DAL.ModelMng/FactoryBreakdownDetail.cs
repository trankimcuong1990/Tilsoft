//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ModelMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryBreakdownDetail
    {
        public int FactoryBreakdownDetailID { get; set; }
        public Nullable<int> FactoryBreakdownID { get; set; }
        public Nullable<int> FactoryBreakdownCategoryID { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Remark { get; set; }
    }
}
