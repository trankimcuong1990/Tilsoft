//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CushionTestingMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CushionTestingMng_CushionTestStandard_OverView
    {
        public int CushionTestReportUsingCushionStandardID { get; set; }
        public Nullable<int> CushionTestReportID { get; set; }
        public Nullable<int> CushionTestStandardID { get; set; }
        public string TestStandardNM { get; set; }
    
        public virtual CushionTestingMng_CushionTestReport_OverView CushionTestingMng_CushionTestReport_OverView { get; set; }
    }
}
