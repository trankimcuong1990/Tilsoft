//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ModelMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModelMng_ModelIssueTrackImageFinish_View
    {
        public int ModelIssueTrackImageFinishID { get; set; }
        public Nullable<int> ModelIssueTrackID { get; set; }
        public string ImageFile { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
    
        public virtual ModelMng_ModelIssueTrack_View ModelMng_ModelIssueTrack_View { get; set; }
    }
}
