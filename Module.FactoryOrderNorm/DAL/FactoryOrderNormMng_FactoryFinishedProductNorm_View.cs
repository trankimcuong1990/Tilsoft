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
    
    public partial class FactoryOrderNormMng_FactoryFinishedProductNorm_View
    {
        public FactoryOrderNormMng_FactoryFinishedProductNorm_View()
        {
            this.FactoryOrderNormMng_FactoryMaterialNorm_View = new HashSet<FactoryOrderNormMng_FactoryMaterialNorm_View>();
            this.FactoryOrderNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View = new HashSet<FactoryOrderNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View>();
        }
    
        public int FactoryFinishedProductNormID { get; set; }
        public Nullable<int> FactoryNormID { get; set; }
        public Nullable<int> FactoryFinishedProductID { get; set; }
        public Nullable<decimal> NormValue { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> ParentNormID { get; set; }
        public Nullable<int> MaterialGroupTypeID { get; set; }
    
        public virtual ICollection<FactoryOrderNormMng_FactoryMaterialNorm_View> FactoryOrderNormMng_FactoryMaterialNorm_View { get; set; }
        public virtual ICollection<FactoryOrderNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View> FactoryOrderNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View { get; set; }
    }
}
