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
    
    public partial class FactoryStepNormDetail
    {
        public int FactoryStepNormDetailID { get; set; }
        public Nullable<int> FactoryStepID { get; set; }
        public Nullable<int> StepIndex { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> TimeNorm { get; set; }
    
        public virtual FactoryStepNorm FactoryStepNorm { get; set; }
    }
}
