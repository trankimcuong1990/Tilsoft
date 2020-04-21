using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class EditFormData
    {
        public DTO.ModelMng.Model Data { get; set; }
        public List<DTO.Support.ProductType> ProductTypes { get; set; }
        public List<DTO.Support.PackagingMethod> PackagingMethods { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.Support.ProductGroup> ProductGroups { get; set; }
        public List<DTO.Support.ManufacturerCountry> ManufacturerCountries { get; set; }
        public List<DTO.Support.ModelStatus> ModelStatuses { get; set; }
        public List<DTO.Support.ProductCategory> ProductCategories { get; set; }
        public List<DTO.Support.Factory> Factories { get; set; }
        public List<Support.GalleryItemType> GalleryItemTypes { get; set; }
        public List<Support.ProductElement> ProductElements { get; set; }
        public List<DTO.ModelMng.ProductElementOption> ProductElementOptions { get; set; }
        public List<Support.MaterialType> MaterialTypes { get; set; }
        public List<DTO.ModelMng.ModelPriceConfiguration> ModelPriceConfigurationDefault { get; set; }
        public List<DTO.ModelMng.FactoryDTO> FactoryDTOs { get; set; }
        public bool IsPriceEnabled { get; set; }
        public bool CanApprove { get; set; }

        public List<QCTrackingStatus> SupportQCTrackingStatus { get; set; }
        public List<QCTrackingType> SupportQCTrackingType { get; set; }
        public List<DTO.ModelMng.ClientSpecialPackagingMethod> ClientSpecialPackagingMethods { get; set; }
        public List<ModelDefaultOptionDTO> ModelDefaultOptionDTOs { get; set; }

        public List<Support.Client> Clients { get; set; }
        public List<ConfigPriceDTO> ConfigPriceDTOs { get; set; }
        public List<DTO.ModelMng.CheckListGroupDTO> CheckListGroupDTO { get; set; }
        
    }
}
