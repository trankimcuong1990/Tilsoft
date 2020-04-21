using System;

namespace Module.ComplianceMng.DTO
{
    public class CompliancePICDTO
    {
        public int CompliancePICID { get; set; }
        public Nullable<int> ComplianceProcessID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string EmployeeNM { get; set; }
        public string PreparationTimeFrom { get; set; }
        public string PreparationTimeTo { get; set; }
        public string Remark { get; set; }
        public string FileUD { get; set; }
        public string FileUDUrl { get; set; }
        public string FileUDFriendlyName { get; set; }
        public bool FileUDHasChange { get; set; }
        public string NewFileUD { get; set; }
    }
}
