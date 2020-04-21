using System;

namespace Module.Sample2Mng.DTO
{
    public class SampleProductDimensionDTO
    {
        public int SampleProductDimensionID { get; set; }
        public string SampleProductDimensionNM { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public string FrameWeight { get; set; }
        public string WickerWeight { get; set; }
        public Nullable<decimal> IndicatedPrice { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string NetWeight { get; set; }
    }
}
