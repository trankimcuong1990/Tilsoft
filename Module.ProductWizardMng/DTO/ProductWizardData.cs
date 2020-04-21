using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class ProductWizardData
    {        
        public List<DTO.MaterialOptionDTO> MaterialOptions { get; set; }
        public List<DTO.MaterialTypeOptionDTO> MaterialTypeOptions { get; set; }
        public List<DTO.MaterialColorOptionDTO> MaterialColorOptions { get; set; }

        public List<DTO.FrameMaterialOptionDTO> FrameMaterialOptions { get; set; }
        public List<DTO.FrameMaterialColorOptionDTO> FrameMaterialColorOptions { get; set; }

        public List<DTO.SubMaterialOptionDTO> SubMaterialOptions { get; set; }
        public List<DTO.SubMaterialColorOptionDTO> SubMaterialColorOptions { get; set; }

        public List<DTO.CushionTypeOptionDTO> CushionTypeOptions { get; set; }
        public List<DTO.CushionColorOptionDTO> CushionColorOptions { get; set; }
        public List<DTO.BackCushionOptionDTO> BackCushionOptions { get; set; }
        public List<DTO.SeatCushionOptionDTO> SeatCushionOptions { get; set; }
        public List<DTO.PackagingMethodDTO> PackagingMethods { get; set; }

        public List<DTO.FSCTypeDTO> FSCTypes { get; set; }
        public List<DTO.FSCPercentDTO> FSCPercents { get; set; }

        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int ManufacturerCountryID { get; set; }
        public string ManufacturerCountryUD { get; set; }
        public bool? HasCushion { get; set; }
        public bool? HasFrameMaterial { get; set; }
        public bool? HasSubMaterial { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }

        //sale price config
        public List<DTO.ModelFixPriceConfigurationDTO> ModelFixPriceConfigurations { get; set; }
        public List<DTO.ModelPriceConfigurationDetailDTO> ModelPriceConfigurationDetails { get; set; }

        //purchase price config
        public List<DTO.ConfiguredPurchasingPriceModelConfirmedFixedPriceDTO> ConfiguredPurchasingPriceModelConfirmedFixedPrices { get; set; }
        public List<DTO.ConfiguredPurchasingPriceModelConfirmedPriceConfigurationDTO> ConfiguredPurchasingPriceModelConfirmedPriceConfigurations { get; set; }

        //breakdown
        public List<DTO.BreakdownCategoryDTO> BreakdownCategories { get; set; }
        public List<DTO.BreakdownCategoryOptionDTO> BreakdownCategoryOptions { get; set; }
        public List<DTO.BreakdownManagementFeeDTO> BreakdownManagementFees { get; set; }
        public List<DTO.ClientSpecialPackagingMethodDTO> ClientSpecialPackagingMethods { get; set; }
        public decimal? VND_USD_ExchangeRate { get; set; }
        public decimal? SeasonSpecPercent { get; set; }

        public ProductWizardData() {
            MaterialOptions = new List<MaterialOptionDTO>();
            MaterialTypeOptions = new List<MaterialTypeOptionDTO>();
            MaterialColorOptions = new List<MaterialColorOptionDTO>();
            FrameMaterialOptions = new List<FrameMaterialOptionDTO>();
            FrameMaterialColorOptions = new List<FrameMaterialColorOptionDTO>();
            SubMaterialOptions = new List<SubMaterialOptionDTO>();
            SubMaterialColorOptions = new List<SubMaterialColorOptionDTO>();
            CushionTypeOptions = new List<CushionTypeOptionDTO>();
            CushionColorOptions = new List<CushionColorOptionDTO>();
            BackCushionOptions = new List<BackCushionOptionDTO>();
            SeatCushionOptions = new List<SeatCushionOptionDTO>();
            PackagingMethods = new List<PackagingMethodDTO>();
            FSCTypes = new List<FSCTypeDTO>();
            FSCPercents = new List<FSCPercentDTO>();

            ModelFixPriceConfigurations = new List<ModelFixPriceConfigurationDTO>();
            ModelPriceConfigurationDetails = new List<ModelPriceConfigurationDetailDTO>();
            ConfiguredPurchasingPriceModelConfirmedFixedPrices = new List<ConfiguredPurchasingPriceModelConfirmedFixedPriceDTO>();
            ConfiguredPurchasingPriceModelConfirmedPriceConfigurations = new List<ConfiguredPurchasingPriceModelConfirmedPriceConfigurationDTO>();
            BreakdownCategories = new List<BreakdownCategoryDTO>();
            BreakdownCategoryOptions = new List<BreakdownCategoryOptionDTO>();
            BreakdownManagementFees = new List<BreakdownManagementFeeDTO>();
            ClientSpecialPackagingMethods = new List<ClientSpecialPackagingMethodDTO>();

        }
    }


}
