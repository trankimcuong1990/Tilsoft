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
    
    public partial class ReportQAQCMng_ReportQAQCSearch_View
    {
        public int ReportQAQCID { get; set; }
        public Nullable<int> QAQCID { get; set; }
        public Nullable<int> TypeOfInspectionID { get; set; }
        public string TypeOfInspecNM { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusNM { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ReadyQty { get; set; }
        public Nullable<int> CheckedQty { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedNM { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedNM { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> FinishedDate { get; set; }
        public Nullable<int> ApprovalBy { get; set; }
        public string ApprovalNM { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public Nullable<int> UserAccessID { get; set; }
        public Nullable<int> ApprovalID { get; set; }
    }
}
