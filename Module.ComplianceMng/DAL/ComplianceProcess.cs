//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ComplianceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ComplianceProcess
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ComplianceProcess()
        {
            this.ComplianceAttachedFile = new HashSet<ComplianceAttachedFile>();
            this.CompliancePIC = new HashSet<CompliancePIC>();
        }
    
        public int ComplianceProcessID { get; set; }
        public string ComplianceProcessUD { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> ComplianceCertificateTypeID { get; set; }
        public string CertificateNumber { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> AuditStatusID { get; set; }
        public string Rating { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public Nullable<System.DateTime> RenewDate { get; set; }
        public Nullable<System.DateTime> FictiveDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComplianceAttachedFile> ComplianceAttachedFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompliancePIC> CompliancePIC { get; set; }
    }
}