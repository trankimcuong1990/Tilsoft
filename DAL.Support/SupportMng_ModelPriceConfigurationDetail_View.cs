//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Support
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupportMng_ModelPriceConfigurationDetail_View
    {
        public int ModelPriceConfigurationDetailID { get; set; }
        public Nullable<int> OptionID { get; set; }
        public Nullable<decimal> PercentValue { get; set; }
        public Nullable<decimal> USDAmount { get; set; }
        public Nullable<decimal> EURAmount { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ProductElementID { get; set; }
    }
}