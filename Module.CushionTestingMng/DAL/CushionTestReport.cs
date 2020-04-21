//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CushionTestingMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CushionTestReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CushionTestReport()
        {
            this.CushionTestReportFile = new HashSet<CushionTestReportFile>();
            this.CushionTestReportUsingCushionStandard = new HashSet<CushionTestReportUsingCushionStandard>();
        }
    
        public int CushionTestReportID { get; set; }
        public string CushionTestReportUD { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<System.DateTime> TestDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CushionTestReportFile> CushionTestReportFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CushionTestReportUsingCushionStandard> CushionTestReportUsingCushionStandard { get; set; }
    }
}