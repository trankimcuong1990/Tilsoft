//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryStepNorm.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryStepNorm
    {
        public FactoryStepNorm()
        {
            this.FactoryStepNormDetail = new HashSet<FactoryStepNormDetail>();
        }
    
        public int FactoryStepNormID { get; set; }
        public Nullable<int> ProductID { get; set; }
    
        public virtual ICollection<FactoryStepNormDetail> FactoryStepNormDetail { get; set; }
    }
}
