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
    
    public partial class FactoryOrderNormMng_FactoryFinishedProductNormFactoryGoodsProcedure_View
    {
        public int FactoryFinishedProductNormFactoryGoodsProcedureID { get; set; }
        public Nullable<int> FactoryFinishedProductNormID { get; set; }
        public Nullable<int> FactoryGoodsProcedureID { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    
        public virtual FactoryOrderNormMng_FactoryFinishedProductNorm_View FactoryOrderNormMng_FactoryFinishedProductNorm_View { get; set; }
    }
}
