//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample3Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sample3Mng_ProductInfo_ReferenceImage_View
    {
        public int SampleReferenceImageID { get; set; }
        public string BringInBy { get; set; }
        public Nullable<System.DateTime> BringInDate { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> SampleProductID { get; set; }
    
        public virtual Sample3Mng_ProductInfo_Product_View Sample3Mng_ProductInfo_Product_View { get; set; }
    }
}