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
    
    public partial class Sample3Mng_SampleOrderOverview_ProductLocation_View
    {
        public int SampleProductItemID { get; set; }
        public string SampleProductItemUD { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CurrentLocation { get; set; }
        public Nullable<System.DateTime> LocationDate { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
    
        public virtual Sample3Mng_SampleOrderOverview_Product_View Sample3Mng_SampleOrderOverview_Product_View { get; set; }
    }
}
