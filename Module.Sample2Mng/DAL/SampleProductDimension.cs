//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SampleProductDimension
    {
        public int SampleProductDimensionID { get; set; }
        public string SampleProductDimensionNM { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public string FrameWeight { get; set; }
        public string WickerWeight { get; set; }
        public Nullable<decimal> IndicatedPrice { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string Remark { get; set; }
        public string NetWeight { get; set; }
    
        public virtual SampleProduct SampleProduct { get; set; }
    }
}
