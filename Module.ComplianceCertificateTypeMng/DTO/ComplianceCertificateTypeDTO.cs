using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ComplianceCertificateTypeMng.DTO
{
    public class ComplianceCertificateTypeDTO
    {
        public int ComplianceCertificateTypeID { get; set; }
        public string ComplianceCertificateTypeUD { get; set; }
        public string ComplianceCertificateTypeNM { get; set; }
        public bool? IsRequired { get; set; }
        public int Total { get; set; }
    }
}
