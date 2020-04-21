using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryMng2.DTO
{
    public class FactoryCertificate
    {
        public int FactoryCertificateID { get; set; }
        public int? FactoryID { get; set; }
        public string CertificateNM { get; set; }
        public string CertificateNo { get; set; }
        public string ValidUntil { get; set; }
        public string CertificateFile { get; set; }
        public int? TypeID { get; set; }
        public string CertificateFileUrl { get; set; }
        public string CertificateFileFriendlyName { get; set; }
        public bool CertificateFileHasChange { get; set; }
        public string NewCertificateFile { get; set; }
    }
}
