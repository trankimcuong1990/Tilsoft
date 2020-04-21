using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactorySearchResult
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string FactoryOfficialName { get; set; }
        public string SupplierNM { get; set; }
        public string City { get; set; }
        public int? LocationID { get; set; }
        public string LocationNM { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsPotential { get; set; }
        public string GoogleMapURL { get; set; }
        public string Website { get; set; }

        // Current Season data
        public string CurrentSeason { get; set; }
        public decimal? CurrentTurnover { get; set; }
        public decimal? CurrentTotalShipped40HC { get; set; }

        // Previous Season data
        public string PrevSeason { get; set; }
        public decimal? PrevTurnover { get; set; }
        public decimal? PrevTotalShipped40HC { get; set; }

        //Image
        public string LogoImage_DisplayURL { get; set; }
        public string ThumbnailLocation { get; set; }

        // Child detail
        public List<FactoryDirector> FactoryDirectors { get; set; }

        public List<FactoryManager> FactoryManagers { get; set; }
        public List<FactorySampleTechnical> FactorySampleTechnicals { get; set; }
        public List<FactoryResponsiblePerson> FactoryResponsiblePersons { get; set; }
        public List<FactoryRawMaterialSupplier> FactoryRawMaterialSuppliers { get; set; }
        public List<FactorySupplier> FactorySuppliers { get; set; }

        public List<FactoryExpectedCapacitySearch> FactoryExpectedCapacitySearches  { get; set; }

    }
}
