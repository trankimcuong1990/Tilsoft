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
    
    public partial class ProductTestingMng_ProducTestingStandard_SearchView
    {
        public int ProductTestUsingTestStandardID { get; set; }
        public Nullable<int> ProductTestID { get; set; }
        public string TestStandardNM { get; set; }
    
        public virtual ProductTestingMng_ProductTesting_SearchView ProductTestingMng_ProductTesting_SearchView { get; set; }
    }
}