//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.CushionColorMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class CushionColorMng_CushionTesting_View
    {
        public CushionColorMng_CushionTesting_View()
        {
            this.CushionColorMng_CushionTestingFile_View = new HashSet<CushionColorMng_CushionTestingFile_View>();
            this.CushionColorMng_CushionTestingStandard_View = new HashSet<CushionColorMng_CushionTestingStandard_View>();
        }
    
        public int CushionTestReportID { get; set; }
        public string CushionTestReportUD { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<System.DateTime> TestDate { get; set; }
        public string Remark { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> CushionTestStandardID { get; set; }
        public string TestStandardNM { get; set; }
    
        public virtual ICollection<CushionColorMng_CushionTestingFile_View> CushionColorMng_CushionTestingFile_View { get; set; }
        public virtual ICollection<CushionColorMng_CushionTestingStandard_View> CushionColorMng_CushionTestingStandard_View { get; set; }
        public virtual CushionColorMng_CushionColor_View CushionColorMng_CushionColor_View { get; set; }
    }
}
