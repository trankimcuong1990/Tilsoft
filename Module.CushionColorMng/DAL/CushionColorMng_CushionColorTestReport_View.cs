//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CushionColorMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CushionColorMng_CushionColorTestReport_View
    {
        public int CushionColorTestReportID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public string FileUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual CushionColorMng_CushionColor_View CushionColorMng_CushionColor_View { get; set; }
        public virtual CushionColorMng_CushionColorSearchResult_View CushionColorMng_CushionColorSearchResult_View { get; set; }
    }
}
