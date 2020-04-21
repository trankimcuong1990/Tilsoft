using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryMng2.DTO
{
    public class Factory
    {
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string FactoryOfficialName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
        public string PIC { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public object Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public object ConcurrencyFlag { get; set; }
        public int? LocationID { get; set; }
        public bool? IsProducer { get; set; }
        public bool? IsTrader { get; set; }
        public bool? IsPacking { get; set; }
        public bool? IsAssembler { get; set; }
        public bool? IsPrivate { get; set; }
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
        public string SampleLeadtime { get; set; }
        public string WarehouseCapacity { get; set; }
        public string SpecialRemark { get; set; }
        public string LogoImage { get; set; }
        public string LogoImage_DisplayURL { get; set; }
        public string UpdatorName { get; set; }
        public bool IsActive { get; set; }
        public bool IsPotential { get; set; }
        public string GoogleMapURL { get; set; }
        public string Website { get; set; }
        public int? SupplierID { get; set; }
        public decimal? ExportCost20DC { get; set; }
        public decimal? ExportCost40DC { get; set; }
        public decimal? ExportCost40HC { get; set; }

        // TurnOver data
        public string CurrentSeason { get; set; }
        public decimal? CurrentTurnOver { get; set; }
        public string OnePreYrSeason { get; set; }
        public decimal? OnePreYrTurnover { get; set; }
        public string TwoPreYrSeason { get; set; }
        public decimal? TwoPreYrTurnover { get; set; }
        public string ThreePreYrSeason { get; set; }
        public decimal? ThreePreYrTurnover { get; set; }   

        // Image
        public bool LogoImage_HasChange { get; set; }
        public string LogoImage_NewFile { get; set; }
        //
        // concurrency
        //
        public string ConcurrencyFlag_String { get; set; }
        public string DIBDNumber { get; set; }

        // Child detail
        public List<FactoryImage> FactoryImages { get; set; }
        public List<FactoryBusinessCard> FactoryBusinessCard { get; set; }
        public List<FactoryDirector> FactoryDirectors { get; set; }
        public List<FactoryManager> FactoryManagers { get; set; }
        public List<FactoryPricing> FactoryPricings { get; set; }
        public List<FactorySampleTechnical> FactorySampleTechnicals { get; set; }
        public List<FactoryResponsiblePerson> FactoryResponsiblePersons { get; set; }
        public List<FactoryRawMaterialSupplier> FactoryRawMaterialSuppliers { get; set; }
        public List<FactoryInHouseTest> FactoryInHouseTests { get; set; }
        public List<FactoryCertificate> FactoryCertificates { get; set; }
        public List<FactoryMonthlyCapacity> MonthlyCapacityByCurrentSeasons { get; set; }
        public List<FactoryMonthlyCapacity> MonthlyCapacityByPreSeasons { get; set; }
        public List<FactoryTurnover> FactoryTurnovers { get; set; }
        public List<FactoryExpectedCapacity> FactoryExpectedCapacities { get; set; }
        public List<AppointmentDTO> AppointmentDTOs { get; set; }
        public List<FactoryCapacityByWeek> factoryCapacityByWeeks { get; set; }
        public List<WeekInforRangeDTO> weekInforRangeDTOs { get; set; }
        public List<FactorySupplier> FactorySuppliers { get; set; }
        public string FactoryAddress { get; set; }

        public List<FactoryGalleryDTO> FactoryGalleries { get; set; }

        public List<FactoryContactQuickInfo> FactoryContactQuickInfos { get; set; }

        public List<FactoryDocumentDTO> factoryDocuments { get; set; }
        public List<FactoryProductGroupDTO> FactoryProductGroupDTOs { get; set; }
    }
}
