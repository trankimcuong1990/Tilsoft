//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.LabelingPackagingMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class LabelingPackagingDetail
    {
        public LabelingPackagingDetail()
        {
            this.LabelingPackagingAttachment = new HashSet<LabelingPackagingAttachment>();
        }
    
        public int LabelingPackagingDetailID { get; set; }
        public Nullable<int> LabelingPackagingID { get; set; }
        public Nullable<int> SaleOrderDetailID { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public Nullable<int> PackaingTypeID { get; set; }
        public Nullable<int> BoxLayoutApprovalStatusID { get; set; }
        public Nullable<int> HandTagApprovalStatusID { get; set; }
        public Nullable<int> MaintenanceInstructionApprovalStatusID { get; set; }
        public Nullable<int> AssemblyInstructionApprovalStatusID { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
    
        public virtual LabelingPackaging LabelingPackaging { get; set; }
        public virtual ICollection<LabelingPackagingAttachment> LabelingPackagingAttachment { get; set; }
        public virtual SaleOrderDetail SaleOrderDetail { get; set; }
    }
}