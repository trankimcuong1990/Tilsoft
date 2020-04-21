using System;

namespace Module.ComplianceMng.DTO
{
    public class ComplianceSearchDTO
    {
        public int ComplianceProcessID { get; set; }
        public string ComplianceProcessUD { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryOfficialName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string DIBDNumber { get; set; }
        public int? ComplianceCertificateTypeID { get; set; }
        public string ComplianceCertificateTypeUD { get; set; }
        public string ComplianceCertificateTypeNM { get; set; }
        public string CertificateNumber { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public Nullable<int> AuditStatusID { get; set; }
        public string AuditStatusNM { get; set; }
        public string Rating { get; set; }
        public string ExpiredDate { get; set; }
        public string RenewDate { get; set; }
        public string FictiveDate { get; set; }
        public string Remark { get; set; }
    }
}
