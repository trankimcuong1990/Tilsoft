//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryMaterialNorm.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMaterialNormMng_FactoryMaterialNormDetail_View
    {
        public int FactoryMaterialNormDetailID { get; set; }
        public Nullable<int> FactoryMaterialNormID { get; set; }
        public Nullable<int> FactoryMaterialID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> NormValue { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public Nullable<int> FactoryGoodsProcedureID { get; set; }
    
        public virtual FactoryMaterialNormMng_FactoryMaterialNorm_View FactoryMaterialNormMng_FactoryMaterialNorm_View { get; set; }
    }
}
