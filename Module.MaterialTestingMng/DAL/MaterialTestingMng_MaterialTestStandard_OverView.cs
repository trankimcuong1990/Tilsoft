//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.MaterialTestingMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialTestingMng_MaterialTestStandard_OverView
    {
        public int MaterialTestUsingMaterialStandardID { get; set; }
        public Nullable<int> MaterialTestReportID { get; set; }
        public Nullable<int> MaterialTestStandardID { get; set; }
        public string TestStandardNM { get; set; }
    
        public virtual MaterialTestingMng_MaterialTestReport_OverView MaterialTestingMng_MaterialTestReport_OverView { get; set; }
    }
}
