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
    
    public partial class CushionColorMng_CushionTestingFile_View
    {
        public int CushionTestReportFileID { get; set; }
        public Nullable<int> CushionTestReportID { get; set; }
        public string Remark { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
    
        public virtual CushionColorMng_CushionTesting_View CushionColorMng_CushionTesting_View { get; set; }
    }
}
