using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class FactoryRawMaterialSearch
    {
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string Phone { get; set; }
        public string WebAddress { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        //public string CertificateNM { get; set; }
        //public string ValidUntil { get; set; }
        //public string CertificateFileLocation { get; set; }
        //public string CertificateFriendlyName { get; set; }
        public decimal? Amount { get; set; }
        public string LocationNM { get; set; }
        public string KeyRawMaterialNM { get; set; }
        public string UpdatorName { get; set; }
        public string LogoFileLocation { get; set; }
        public string LogoThumbnailLocation { get; set; }
        public string ContactPerson { get; set; }
        public int? CompanyID { get; set; }
        public bool? IsPotential { get; set; }

        // Turnover
        public decimal? CurrentTurnover { get; set; }
        public decimal? PreviousTurnover { get; set; }

        // Child list
        public List<DTO.FactoryRawMaterialCertificate> FactoryRawMaterialCertificates { get; set; }
        public List<DTO.FactoryRawMaterialTest> FactoryRawMaterialTests { get; set; }
        public List<DTO.FactoryRawMaterialPricingPerson> FactoryRawMaterialPricingPersons { get; set; }
        public List<DTO.FactoryRawMaterialQualityPerson> FactoryRawMaterialQualityPersons { get; set; }
    }
}
