using System;

namespace Module.ComplianceMng.DTO
{
    public class AuditStatusDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> AuditStatusID { get; set; }
        public string AuditStatusNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
