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
    
    public partial class FactoryOrderNormMng_FactoryFinishedProductOrderNormFactoryGoodsProcedure_View
    {
        public int FactoryFinishedProductOrderNormFactoryGoodsProcedureID { get; set; }
        public Nullable<int> FactoryFinishedProductOrderNormID { get; set; }
        public Nullable<int> FactoryGoodsProcedureID { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    
        public virtual FactoryOrderNormMng_FactoryFinishedProductOrderNorm_View FactoryOrderNormMng_FactoryFinishedProductOrderNorm_View { get; set; }
    }
}
