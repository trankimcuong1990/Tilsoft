//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.MaterialOptionMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialOptionMng_MaterialTestingFile_View
    {
        public int MaterialTestReportFileID { get; set; }
        public Nullable<int> MaterialTestReportID { get; set; }
        public string Remark { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
    
        public virtual MaterialOptionMng_MaterialTesting_View MaterialOptionMng_MaterialTesting_View { get; set; }
    }
}
