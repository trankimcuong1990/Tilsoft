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
    
    public partial class ModelMng_ModelIssueTrack_View
    {
        public ModelMng_ModelIssueTrack_View()
        {
            this.ModelMng_ModelIssueTrackImageError_View = new HashSet<ModelMng_ModelIssueTrackImageError_View>();
            this.ModelMng_ModelIssueTrackImageFinish_View = new HashSet<ModelMng_ModelIssueTrackImageFinish_View>();
        }
    
        public int ModelIssueTrackID { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<int> StatusBy { get; set; }
        public string StatusName { get; set; }
        public Nullable<int> FollowUp { get; set; }
        public string EmployeeName { get; set; }
        public string FullName { get; set; }
        public Nullable<int> ModelIssueID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> TypeID { get; set; }
        public string TypeName { get; set; }
        public string Comment { get; set; }
        public string OtherContent { get; set; }
        public string QualityReport { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
    
        public virtual ICollection<ModelMng_ModelIssueTrackImageError_View> ModelMng_ModelIssueTrackImageError_View { get; set; }
        public virtual ICollection<ModelMng_ModelIssueTrackImageFinish_View> ModelMng_ModelIssueTrackImageFinish_View { get; set; }
        public virtual ModelMng_Model_View ModelMng_Model_View { get; set; }
    }
}
