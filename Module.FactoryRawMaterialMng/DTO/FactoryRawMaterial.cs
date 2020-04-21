using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class FactoryRawMaterial
    {
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string Address { get; set; }
        public string WebAddress { get; set; }
        public string GoogleMapURL { get; set; }
        public string Phone { get; set; }
        public int? LocationID { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool? IsProducer { get; set; }
        public bool? IsTrader { get; set; }
        public bool? IsPacking { get; set; }
        public bool? IsAssembler { get; set; }
        public bool? IsPrivate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPotential { get; set; }
        public bool? IsJointVenture { get; set; }
        public bool? IsState { get; set; }
        public bool? IsStockCompany { get; set; }
        public string ProductDescription { get; set; }
        public string ManufacturingProcessDescription { get; set; }
        public string TotalPlantSquareMeters { get; set; }
        public string NumberOfBuildings { get; set; }
        public string YearEstablished { get; set; }
        public string NumberOfWorkers { get; set; }
        public string OperatingHours { get; set; }
        public string OperatingDays { get; set; }
        public string ProductLeadtime { get; set; }
        public string WarehouseCapacity { get; set; }
        public string SampleLeadTime { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public object ConcurrencyFlag { get; set; }
        public string ContactPerson { get; set; }
        public int KeyRawMaterialID { get; set; }
        public string KeyRawMaterialNM { get; set; }
        public int? CompanyID { get; set; }

        // Image
        public string LogoFile { get; set; }
        public bool LogoFile_HasChange { get; set; }
        public string LogoFile_NewFile { get; set; }
        public string LogoFileLocation { get; set; }
        public string LogoThumbnailLocation { get; set; }

        // concurrency
        public string ConcurrencyFlag_String { get; set; }
        public string UpdatorNM { get; set; }

        // Child List
        public List<FactoryRawMaterialCertificate> FactoryRawMaterialCertificates { get; set; }
        public List<FactoryRawMaterialTest> FactoryRawMaterialTests { get; set; }
        public List<FactoryRawMaterialTurnover> FactoryRawMaterialTurnovers { get; set; }
        public List<FactoryRawMaterialPricingPerson> FactoryRawMaterialPricingPersons { get; set; }
        public List<FactoryRawMaterialQualityPerson> FactoryRawMaterialQualityPersons { get; set; }
        public List<FactoryRawMaterialImage> FactoryRawMaterialImages { get; set; }
        public List<FactoryRawPaymentTerm> FactoryRawPaymentTerms { get; set; }
        public List<SubSupplierContract> SubSupplierContracts { get; set; }

        public List<SupplierContactQuickInfo> SupplierContactQuickInfos { get; set; }
        public List<SupplierManager> supplierManagers { get; set; }
        public List<SupplierDirector> supplierDirectors { get; set; }
        public List<SupplierSampleTechnical> supplierSampleTechnicals { get; set; }

       
        public List<FactoryRawMaterialProductGroupDTO> FactoryRawMaterialProductGroupDTOs { get; set; }

        public List<MaterialsPrice> materialsPrices { get; set; }
        public List<StatusMaterialPrice> statusMaterials { get; set; }
        public List<FactoryRawMaterialBusinessCardDTO> FactoryRawMaterialBusinessCardDTO { get; set; }
        public List<FactoryRawMaterialGalleryDTO> FactoryRawMaterialGalleryDTO { get; set; }
        
    }
}
