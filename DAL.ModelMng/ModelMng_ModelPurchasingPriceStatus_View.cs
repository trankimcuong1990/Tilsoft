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
    
    public partial class ModelMng_ModelPurchasingPriceStatus_View
    {
        public int ModelPurchasingPriceStatusID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
    
        public virtual ModelMng_Model_View ModelMng_Model_View { get; set; }
    }
}
