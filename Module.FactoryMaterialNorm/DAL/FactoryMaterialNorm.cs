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
    
    public partial class FactoryMaterialNorm
    {
        public FactoryMaterialNorm()
        {
            this.FactoryMaterialNormDetail = new HashSet<FactoryMaterialNormDetail>();
        }
    
        public int FactoryMaterialNormID { get; set; }
        public string FactoryMaterialNormUD { get; set; }
        public Nullable<int> ModelID { get; set; }
    
        public virtual ICollection<FactoryMaterialNormDetail> FactoryMaterialNormDetail { get; set; }
    }
}