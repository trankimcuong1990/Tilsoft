//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PriceConfiguration.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PriceConfiguration_PackagingMethod_View
    {
        public int PriceConfigurationDetailID { get; set; }
        public Nullable<int> PriceConfigurationID { get; set; }
        public Nullable<int> OptionID { get; set; }
        public string OptionNM { get; set; }
        public Nullable<decimal> PercentValue { get; set; }
        public Nullable<decimal> USDAmount { get; set; }
        public Nullable<decimal> EURAmount { get; set; }
        public string Season { get; set; }
    }
}
