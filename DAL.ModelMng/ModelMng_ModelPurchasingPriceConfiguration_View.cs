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
    
    public partial class ModelMng_ModelPurchasingPriceConfiguration_View
    {
        public ModelMng_ModelPurchasingPriceConfiguration_View()
        {
            this.ModelMng_ModelPurchasingPriceConfigurationDetail_View = new HashSet<ModelMng_ModelPurchasingPriceConfigurationDetail_View>();
        }
    
        public int ModelPurchasingPriceConfigurationID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ProductElementID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public string ProductElementNM { get; set; }
    
        public virtual ModelMng_Model_View ModelMng_Model_View { get; set; }
        public virtual ICollection<ModelMng_ModelPurchasingPriceConfigurationDetail_View> ModelMng_ModelPurchasingPriceConfigurationDetail_View { get; set; }
    }
}
