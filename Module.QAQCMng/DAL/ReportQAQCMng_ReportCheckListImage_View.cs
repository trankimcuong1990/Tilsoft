//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.QAQCMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportQAQCMng_ReportCheckListImage_View
    {
        public int ReportCheckListImageID { get; set; }
        public Nullable<int> ReportCheckListID { get; set; }
        public string CheckListImageStringID { get; set; }
        public string FileUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
    
        public virtual ReportQAQCMng_ReportCheckList_View ReportQAQCMng_ReportCheckList_View { get; set; }
    }
}
