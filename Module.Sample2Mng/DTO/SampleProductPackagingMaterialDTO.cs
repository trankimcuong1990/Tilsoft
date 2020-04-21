using System;

namespace Module.Sample2Mng.DTO
{
    public class SampleProductPackagingMaterialDTO
    {
        public int SampleProductPackagingMaterialID { get; set; }
        public Nullable<int> SampleProductPackagingMaterialTypeID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Remark { get; set; }
        public Nullable<int> SampleProductID { get; set; }
    }
}
