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
    
    public partial class FactoryNormMng_FactoryNorm_View
    {
        public FactoryNormMng_FactoryNorm_View()
        {
            this.FactoryNormMng_FactoryFinishedProductNorm_View = new HashSet<FactoryNormMng_FactoryFinishedProductNorm_View>();
        }
    
        public int FactoryNormID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
    
        public virtual ICollection<FactoryNormMng_FactoryFinishedProductNorm_View> FactoryNormMng_FactoryFinishedProductNorm_View { get; set; }
    }
}
