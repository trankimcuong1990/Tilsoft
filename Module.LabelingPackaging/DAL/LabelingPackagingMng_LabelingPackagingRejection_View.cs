//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LabelingPackaging.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LabelingPackagingMng_LabelingPackagingRejection_View
    {
        public int LabelingPackagingRejectionID { get; set; }
        public Nullable<int> LabelingPackagingID { get; set; }
        public string LabelingPackagingRejectionComment { get; set; }
        public string LabelingPackagingRejectionCommentFile { get; set; }
        public string LabelingPackagingRejectionFriendlyName { get; set; }
        public string LabelingPackagingRejectionFileLocation { get; set; }
        public string LabelingPackagingRejectionThumbnailLocation { get; set; }
    
        public virtual LabelingPackagingMng_LabelingPackaging_View LabelingPackagingMng_LabelingPackaging_View { get; set; }
    }
}
