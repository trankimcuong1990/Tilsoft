//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LPOverview.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LabelingPackagingMng_LabelingPackagingRemark_View
    {
        public int LabelingPackagingRemarkID { get; set; }
        public Nullable<int> LabelingPackagingID { get; set; }
        public string Remark { get; set; }
        public string RemarkFile { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string RemarkFileUrl { get; set; }
        public string RemarkFriendlyName { get; set; }
        public string UpdatorName { get; set; }
    
        public virtual LabelingPackagingMng_LabelingPackaging_View LabelingPackagingMng_LabelingPackaging_View { get; set; }
    }
}
