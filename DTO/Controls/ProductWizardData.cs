using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Controls
{
    public class ProductWizardData
    {
        public List<DTO.Support.ModelMaterialOption> MaterialOptions { get; set; }
        public List<DTO.Support.ModelMaterialTypeOption> MaterialTypeOptions { get; set; }
        public List<DTO.Support.ModelMaterialColorOption> MaterialColorOptions { get; set; }
        public List<DTO.Support.ModelFrameMaterialOption> FrameMaterialOptions { get; set; }
        public List<DTO.Support.ModelFrameMaterialColorOption> FrameMaterialColorOptions { get; set; }
        public List<DTO.Support.ModelSubMaterialOption> SubMaterialOptions { get; set; }
        public List<DTO.Support.ModelSubMaterialColorOption> SubMaterialColorOptions { get; set; }
        public List<DTO.Support.ModelCushionOption> CushionOptions { get; set; }
        public List<DTO.Support.ModelCushionTypeOption> CushionTypeOptions { get; set; }
        public List<DTO.Support.ModelCushionColorOption> CushionColorOptions { get; set; }
        public List<DTO.Support.ModelPackagingMethodOption> PackagingMethods { get; set; }
        public List<DTO.Support.ModelBackCushionOption> BackCushionOptions { get; set; }
        public List<DTO.Support.ModelSeatCushionOption> SeatCushionOptions { get; set; }
        public List<DTO.Support.FSCPercent> FSCPercents { get; set; }
        public List<DTO.Support.FSCType> FSCTypes { get; set; }

        //model sale price config
        public List<DTO.Support.ModelFixPriceConfiguration> ModelFixPriceConfigurations { get; set; }
        public List<DTO.Support.ModelPriceConfigurationDetail> ModelPriceConfigurationDetails { get; set; }

        //breakdown
        public List<DTO.Support.BreakdownCategory> BreakdownCategories { get; set; }
        public List<DTO.Support.BreakdownCategoryOption> BreakdownCategoryOptions { get; set; }
        public List<DTO.Support.BreakdownManagementFee> BreakdownManagementFees { get; set; }
        public decimal? VND_USD_ExchangeRate { get; set; }
        public decimal? SeasonSpecPercent { get; set; }
        public List<DTO.Support.ClientSpecialPackagingMethod> ClientSpecialPackagingMethods { get; set; }

        //purchasing config price
        public List<DTO.Support.ConfiguredPurchasingPriceModelConfirmedFixedPrice> ConfiguredPurchasingPriceModelConfirmedFixedPrices { get; set; }
        public List<DTO.Support.ConfiguredPurchasingPriceModelConfirmedPriceConfiguration> ConfiguredPurchasingPriceModelConfirmedPriceConfigurations { get; set; }


        //mode
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int ManufacturerCountryID { get; set; }
        public string ManufacturerCountryUD { get; set; }
        public bool HasCushion { get; set; }
        public bool HasFrameMaterial { get; set; }
        public bool HasSubMaterial { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
    }
}
