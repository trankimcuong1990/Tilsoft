//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.QualityInspectionRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QualityInspectionRpt_QualityInspectionCategoryImage_View
    {
        public int QualityInspectionCategoryImageID { get; set; }
        public Nullable<int> QualityInspectionCategoryID { get; set; }
        public string FileUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
    
        public virtual QualityInspectionRpt_QualityInspectionCategory_View QualityInspectionRpt_QualityInspectionCategory_View { get; set; }
    }
}
