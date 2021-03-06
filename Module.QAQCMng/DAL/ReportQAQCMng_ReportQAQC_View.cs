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
    
    public partial class ReportQAQCMng_ReportQAQC_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReportQAQCMng_ReportQAQC_View()
        {
            this.ReportQAQCMng_ReportDefect_View = new HashSet<ReportQAQCMng_ReportDefect_View>();
            this.ReportQAQCMng_ReportCheckList_View = new HashSet<ReportQAQCMng_ReportCheckList_View>();
        }
    
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
        public string Comment { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<double> LatitudeC { get; set; }
        public Nullable<double> LongitudeC { get; set; }
        public Nullable<double> LatitudeF { get; set; }
        public Nullable<double> LongitudeF { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportQAQCMng_ReportDefect_View> ReportQAQCMng_ReportDefect_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportQAQCMng_ReportCheckList_View> ReportQAQCMng_ReportCheckList_View { get; set; }
    }
}
