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
    
    public partial class MaterialOptionMng_MaterialOptionTestReport_View
    {
        public int MaterialOptionTestReportID { get; set; }
        public Nullable<int> MaterialOptionID { get; set; }
        public string FileUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> HasUrlLink { get; set; }
    
        public virtual MaterialOptionMng_MaterialOption_View MaterialOptionMng_MaterialOption_View { get; set; }
        public virtual MaterialOptionMng_MaterialOptionSearchResult_View MaterialOptionMng_MaterialOptionSearchResult_View { get; set; }
    }
}
