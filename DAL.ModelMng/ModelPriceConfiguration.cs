//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ModelMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModelPriceConfiguration
    {
        public ModelPriceConfiguration()
        {
            this.ModelPriceConfigurationDetail = new HashSet<ModelPriceConfigurationDetail>();
        }
    
        public int ModelPriceConfigurationID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ProductElementID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Model Model { get; set; }
        public virtual ICollection<ModelPriceConfigurationDetail> ModelPriceConfigurationDetail { get; set; }
    }
}
