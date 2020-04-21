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
    
    public partial class ReportQAQC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReportQAQC()
        {
            this.ReportCheckList = new HashSet<ReportCheckList>();
            this.ReportDefect = new HashSet<ReportDefect>();
        }
    
        public int ReportQAQCID { get; set; }
        public Nullable<int> QAQCID { get; set; }
        public Nullable<int> TypeOfInspectionID { get; set; }
        public Nullable<System.DateTime> FinishedDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ReadyQty { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string ManagementStringID { get; set; }
        public Nullable<bool> IsSynced { get; set; }
        public Nullable<System.DateTime> SyncedDate { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
        public Nullable<int> CheckedQty { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ApprovalBy { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public string Comment { get; set; }
        public string OverallImage { get; set; }
        public Nullable<double> LatitudeC { get; set; }
        public Nullable<double> LongitudeC { get; set; }
        public Nullable<double> LatitudeF { get; set; }
        public Nullable<double> LongitudeF { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportCheckList> ReportCheckList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportDefect> ReportDefect { get; set; }
    }
}