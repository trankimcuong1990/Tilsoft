//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductTestingMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductTestingMng_ProductTestFile_View
    {
        public int ProductTestFileID { get; set; }
        public Nullable<int> ProductTestID { get; set; }
        public string FileUD { get; set; }
        public string Remark { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
    
        public virtual ProductTestingMng_ProductTesting_View ProductTestingMng_ProductTesting_View { get; set; }
    }
}
