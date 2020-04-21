using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ComplianceMng.DTO
{
    public class CalendarSearchDTO
    {
        public long RowNumber { get; set; }
        public int ComplianceProcessID { get; set; }
        public string ComplianceProcessUD { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryOfficialName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string DIBDNumber { get; set; }
        public Nullable<int> ComplianceCertificateTypeID { get; set; }
        public string ComplianceCertificateTypeUD { get; set; }
        public string ComplianceCertificateTypeNM { get; set; }
        public string CertificateNumber { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public Nullable<int> AuditStatusID { get; set; }
        public string AuditStatusNM { get; set; }
        public string Rating { get; set; }
        public string Remark { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Type { get; set; }
    }
}
