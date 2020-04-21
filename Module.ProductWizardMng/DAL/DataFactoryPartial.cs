using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;

namespace Module.ProductWizardMng.DAL
{
    internal partial class DataFactory
    {
        public DTO.ProductWizardData GetProductOption(int? modelID, string season, int? clientID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProductWizardData data = new DTO.ProductWizardData();
            try
            {
                using (ProductWizardMngEntities context = CreateContext())
                {
                    ProductWizardMng_Model_View dbModel = context.ProductWizardMng_Model_View.FirstOrDefault(o => o.ModelID == modelID);
                    data.ModelID = dbModel.ModelID;
                    data.ModelUD = dbModel.ModelUD;
                    data.ModelNM = dbModel.ModelNM;
                    data.ManufacturerCountryUD = dbModel.ManufacturerCountryUD;
                    data.ManufacturerCountryID = dbModel.ManufacturerCountryID.Value;                    
                    data.HasFrameMaterial = dbModel.HasFrameMaterial;
                    data.HasSubMaterial = dbModel.HasSubMaterial;
                    data.HasCushion = dbModel.HasCushion;
                    if (!string.IsNullOrEmpty(dbModel.ImageFile_DisplayUrl))
                    {
                        data.ImageFile_DisplayUrl = FrameworkSetting.Setting.MediaThumbnailUrl + dbModel.ImageFile_DisplayUrl;
                    }
                    else
                    {
                        data.ImageFile_DisplayUrl = FrameworkSetting.Setting.MediaThumbnailUrl + "no-image.jpg";
                    }
                    if (!string.IsNullOrEmpty(dbModel.ImageFile_FullSizeUrl))
                    {
                        data.ImageFile_FullSizeUrl = FrameworkSetting.Setting.MediaFullSizeUrl + dbModel.ImageFile_FullSizeUrl;
                    }
                    else
                    {
                        data.ImageFile_FullSizeUrl = FrameworkSetting.Setting.MediaFullSizeUrl + "no-image.jpg";
                    }

                    int? productGroupID = dbModel.ProductGroupID;
                    var material = context.ProductWizardMng_function_GetMaterialOption(productGroupID).ToList();
                    data.MaterialOptions = Mapper.Map<List<ProductWizardMng_function_GetMaterialOption_Result>, List<DTO.MaterialOptionDTO>>(material);

                    var materialType = context.ProductWizardMng_function_GetMaterialTypeOption(productGroupID, season).ToList();
                    data.MaterialTypeOptions = Mapper.Map<List<ProductWizardMng_function_GetMaterialTypeOption_Result>, List<DTO.MaterialTypeOptionDTO>>(materialType);

                    var materialColor = context.ProductWizardMng_function_GetMaterialColorOption(productGroupID, season).ToList();
                    data.MaterialColorOptions = Mapper.Map<List<ProductWizardMng_function_GetMaterialColorOption_Result>, List<DTO.MaterialColorOptionDTO>>(materialColor);

                    var frameMaterial = context.ProductWizardMng_function_GetFrameMaterialOption(productGroupID, season, data.HasFrameMaterial).ToList();
                    data.FrameMaterialOptions = Mapper.Map<List<ProductWizardMng_function_GetFrameMaterialOption_Result>, List<DTO.FrameMaterialOptionDTO>>(frameMaterial);

                    var frameMaterialColor = context.ProductWizardMng_function_GetFrameMaterialColorOption(productGroupID, season, data.HasFrameMaterial).ToList();
                    data.FrameMaterialColorOptions = Mapper.Map<List<ProductWizardMng_function_GetFrameMaterialColorOption_Result>, List<DTO.FrameMaterialColorOptionDTO>>(frameMaterialColor);

                    var subMaterial = context.ProductWizardMng_function_GetSubMaterialOption(modelID, season, data.HasSubMaterial).ToList();
                    data.SubMaterialOptions = Mapper.Map<List<ProductWizardMng_function_GetSubMaterialOption_Result>, List<DTO.SubMaterialOptionDTO>>(subMaterial);

                    var subMaterialColor = context.ProductWizardMng_function_GetSubMaterialColorOption(modelID, season, data.HasSubMaterial).ToList();
                    data.SubMaterialColorOptions = Mapper.Map<List<ProductWizardMng_function_GetSubMaterialColorOption_Result>, List<DTO.SubMaterialColorOptionDTO>>(subMaterialColor);

                    var cushionType = context.ProductWizardMng_function_GetCushionTypeOption(productGroupID, season, data.HasCushion).ToList();
                    data.CushionTypeOptions = Mapper.Map<List<ProductWizardMng_function_GetCushionTypeOption_Result>, List<DTO.CushionTypeOptionDTO>>(cushionType);

                    var cushionColor = context.ProductWizardMng_function_GetCushionColorOption(productGroupID, season, data.HasCushion).ToList();
                    data.CushionColorOptions = Mapper.Map<List<ProductWizardMng_function_GetCushionColorOption_Result>, List<DTO.CushionColorOptionDTO>>(cushionColor);

                    var backCushion = context.ProductWizardMng_function_GetBackCushionOption(productGroupID, season, data.HasCushion).ToList();
                    data.BackCushionOptions = Mapper.Map<List<ProductWizardMng_function_GetBackCushionOption_Result>, List<DTO.BackCushionOptionDTO>>(backCushion);

                    var seatCushion = context.ProductWizardMng_function_GetSeatCushionOption(productGroupID, season, data.HasCushion).ToList();
                    data.SeatCushionOptions = Mapper.Map<List<ProductWizardMng_function_GetSeatCushionOption_Result>, List<DTO.SeatCushionOptionDTO>>(seatCushion);

                    var packagingMethod = context.ProductWizardMng_function_GetPackagingMethodOption(modelID, clientID).ToList();
                    data.PackagingMethods = Mapper.Map<List<ProductWizardMng_function_GetPackagingMethodOption_Result>, List<DTO.PackagingMethodDTO>>(packagingMethod);

                    var fscType = context.ProductWizardMng_FSCType_View.ToList(); ;
                    data.FSCTypes = Mapper.Map<List<ProductWizardMng_FSCType_View>, List<DTO.FSCTypeDTO>>(fscType);

                    var fscPercent = context.ProductWizardMng_FSCPercent_View.ToList(); ;
                    data.FSCPercents = Mapper.Map<List<ProductWizardMng_FSCPercent_View>, List<DTO.FSCPercentDTO>>(fscPercent);

                    //sale price confg
                    var modelFixPriceConfigurations = context.ProductWizardMng_function_GetModelFixPriceConfiguration(modelID, season).ToList();
                    data.ModelFixPriceConfigurations = Mapper.Map<List<ProductWizardMng_function_GetModelFixPriceConfiguration_Result>, List<DTO.ModelFixPriceConfigurationDTO>>(modelFixPriceConfigurations);

                    var modelPriceConfigurationDetails = context.ProductWizardMng_function_GetModelPriceConfigurationDetail(modelID, season).ToList();
                    data.ModelPriceConfigurationDetails = Mapper.Map<List<ProductWizardMng_function_GetModelPriceConfigurationDetail_Result>, List<DTO.ModelPriceConfigurationDetailDTO>>(modelPriceConfigurationDetails);

                    //purchase price config
                    var configuredPurchasingPriceModelConfirmedFixedPrices = context.ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedFixedPrice(modelID, season).ToList();
                    data.ConfiguredPurchasingPriceModelConfirmedFixedPrices = Mapper.Map<List<ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedFixedPrice_Result>, List<DTO.ConfiguredPurchasingPriceModelConfirmedFixedPriceDTO>>(configuredPurchasingPriceModelConfirmedFixedPrices);

                    var configuredPurchasingPriceModelConfirmedPriceConfigurations = context.ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedPriceConfiguration(modelID, season).ToList();
                    data.ConfiguredPurchasingPriceModelConfirmedPriceConfigurations = Mapper.Map<List<ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedPriceConfiguration_Result>, List<DTO.ConfiguredPurchasingPriceModelConfirmedPriceConfigurationDTO>>(configuredPurchasingPriceModelConfirmedPriceConfigurations);

                    //breakdown
                    var breakdownCategories = context.ProductWizardMng_function_GetBreakdownCategory().ToList(); ;
                    data.BreakdownCategories = Mapper.Map<List<ProductWizardMng_function_GetBreakdownCategory_Result>, List<DTO.BreakdownCategoryDTO>>(breakdownCategories);

                    var breakdownCategoryOptions = context.ProductWizardMng_function_GetBreakdownCategoryOption(modelID).ToList(); ;
                    data.BreakdownCategoryOptions = Mapper.Map<List<ProductWizardMng_function_GetBreakdownCategoryOption_Result>, List<DTO.BreakdownCategoryOptionDTO>>(breakdownCategoryOptions);

                    var breakdownManagementFees = context.ProductWizardMng_function_GetBreakdownManagementFee(modelID).ToList(); ;
                    data.BreakdownManagementFees = Mapper.Map<List<ProductWizardMng_function_GetBreakdownManagementFee_Result>, List<DTO.BreakdownManagementFeeDTO>>(breakdownManagementFees);

                    var clientSpecialPackagingMethods = context.ProductWizardMng_function_GetClientSpecialPackagingMethod(clientID).ToList(); ;
                    data.ClientSpecialPackagingMethods = Mapper.Map<List<ProductWizardMng_function_GetClientSpecialPackagingMethod_Result>, List<DTO.ClientSpecialPackagingMethodDTO>>(clientSpecialPackagingMethods);

                    var breakdown = context.ProductWizardMng_function_GetBreakdown(modelID).FirstOrDefault();
                    if (breakdown != null)
                    {
                        data.SeasonSpecPercent = breakdown.SeasonSpecPercent;
                    }
                    else
                    {
                        data.SeasonSpecPercent = 0;
                    }
                    data.VND_USD_ExchangeRate = context.ProductWizardMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault().ExchangeRate;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }
    }
}
