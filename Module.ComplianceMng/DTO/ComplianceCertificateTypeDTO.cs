using System;

namespace Module.ComplianceMng.DTO
{
    public class ComplianceCertificateTypeDTO
    {
        public int ComplianceCertificateTypeID { get; set; }
        public string ComplianceCertificateTypeUD { get; set; }
        public string ComplianceCertificateTypeNM { get; set; }
        public Nullable<bool> IsRequired { get; set; }
    }
}
