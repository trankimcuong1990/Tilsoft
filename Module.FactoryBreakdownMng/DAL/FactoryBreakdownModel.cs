//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryBreakdownMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryBreakdownModel
    {
        public int FactoryBreakdownModelID { get; set; }
        public Nullable<int> FactoryBreakdownID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Remark { get; set; }
    
        public virtual FactoryBreakdown FactoryBreakdown { get; set; }
    }
}
