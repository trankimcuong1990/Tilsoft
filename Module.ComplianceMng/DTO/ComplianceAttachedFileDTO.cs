using System;

namespace Module.ComplianceMng.DTO
{
    public class ComplianceAttachedFileDTO
    {
        public int ComplianceAttachedFileID { get; set; }
        public Nullable<int> ComplianceProcessID { get; set; }
        public string FileUD { get; set; }
        public string FileUDUrl { get; set; }
        public string FileUDFriendlyName { get; set; }
        public bool FileUDHasChange { get; set; }
        public string NewFileUD { get; set; }
        public Nullable<bool> IsCertificate { get; set; }
    }
}
