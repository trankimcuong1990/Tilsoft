//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryMaterial.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMaterial
    {
        public FactoryMaterial()
        {
            this.FactoryMaterialImage = new HashSet<FactoryMaterialImage>();
        }
    
        public int FactoryMaterialID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> FactoryMaterialTypeID { get; set; }
        public Nullable<int> FactoryMaterialColorID { get; set; }
        public string FactoryMaterialEnglishName { get; set; }
        public Nullable<int> FactoryMaterialGroupID { get; set; }
    
        public virtual ICollection<FactoryMaterialImage> FactoryMaterialImage { get; set; }
    }
}
