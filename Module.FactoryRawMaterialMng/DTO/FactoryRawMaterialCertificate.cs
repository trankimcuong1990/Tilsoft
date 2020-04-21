using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class FactoryRawMaterialCertificate
    {
        public int? FactoryRawMaterialCertificateID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string CertificateNM { get; set; }
        public string CertificateNo { get; set; }
        public string ValidUntil { get; set; }
        public int? TypeID { get; set; }
        public string CertificateFile { get; set; }
        public string CertificateFileUrl { get; set; }
        public string CertificateFileFriendlyName { get; set; }
        public bool CertificateFileHasChange { get; set; }
        public string NewCertificateFile { get; set; }
    }
}
