//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryNorm.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryFinishedProductNorm
    {
        public FactoryFinishedProductNorm()
        {
            this.FactoryMaterialNorm = new HashSet<FactoryMaterialNorm>();
            this.FactoryFinishedProductNormFactoryGoodsProcedure = new HashSet<FactoryFinishedProductNormFactoryGoodsProcedure>();
        }
    
        public int FactoryFinishedProductNormID { get; set; }
        public Nullable<int> FactoryNormID { get; set; }
        public Nullable<int> FactoryFinishedProductID { get; set; }
        public Nullable<decimal> NormValue { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> ParentNormID { get; set; }
        public Nullable<int> MaterialGroupTypeID { get; set; }
    
        public virtual FactoryFinishedProduct FactoryFinishedProduct { get; set; }
        public virtual FactoryNorm FactoryNorm { get; set; }
        public virtual ICollection<FactoryMaterialNorm> FactoryMaterialNorm { get; set; }
        public virtual ICollection<FactoryFinishedProductNormFactoryGoodsProcedure> FactoryFinishedProductNormFactoryGoodsProcedure { get; set; }
    }
}
