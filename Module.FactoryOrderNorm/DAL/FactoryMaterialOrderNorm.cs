//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryOrderNorm.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMaterialOrderNorm
    {
        public int FactoryMaterialOrderNormID { get; set; }
        public Nullable<int> FactoryMaterialID { get; set; }
        public Nullable<decimal> NormValue { get; set; }
        public Nullable<int> UnitID { get; set; }
    
        public virtual FactoryFinishedProductOrderNorm FactoryFinishedProductOrderNorm { get; set; }
    }
}
