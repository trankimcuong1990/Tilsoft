//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryMaterialOrderNorm.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View
    {
        public FactoryMaterialOrderNormMng_FactoryMaterialOrderNorm_View()
        {
            this.FactoryMaterialOrderNormMng_FactoryMaterialOrderNormDetail_View = new HashSet<FactoryMaterialOrderNormMng_FactoryMaterialOrderNormDetail_View>();
        }
    
        public int FactoryMaterialOrderNormID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<FactoryMaterialOrderNormMng_FactoryMaterialOrderNormDetail_View> FactoryMaterialOrderNormMng_FactoryMaterialOrderNormDetail_View { get; set; }
    }
}
