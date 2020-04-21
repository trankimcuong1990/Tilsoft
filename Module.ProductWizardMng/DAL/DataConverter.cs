using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.ProductWizardMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DB 2 DTO
                //
                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetMaterialOption_Result, DTO.MaterialOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetMaterialTypeOption_Result, DTO.MaterialTypeOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetMaterialColorOption_Result, DTO.MaterialColorOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetFrameMaterialOption_Result, DTO.FrameMaterialOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetFrameMaterialColorOption_Result, DTO.FrameMaterialColorOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetSubMaterialOption_Result, DTO.SubMaterialOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetSubMaterialColorOption_Result, DTO.SubMaterialColorOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetCushionTypeOption_Result, DTO.CushionTypeOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetCushionColorOption_Result, DTO.CushionColorOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetBackCushionOption_Result, DTO.BackCushionOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetSeatCushionOption_Result, DTO.SeatCushionOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetPackagingMethodOption_Result, DTO.PackagingMethodDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_FSCType_View, DTO.FSCTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_FSCPercent_View, DTO.FSCPercentDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //sale price config
                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetModelFixPriceConfiguration_Result, DTO.ModelFixPriceConfigurationDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetModelPriceConfigurationDetail_Result, DTO.ModelPriceConfigurationDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //purchase price config
                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedFixedPrice_Result, DTO.ConfiguredPurchasingPriceModelConfirmedFixedPriceDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetConfiguredPurchasingPriceModelConfirmedPriceConfiguration_Result, DTO.ConfiguredPurchasingPriceModelConfirmedPriceConfigurationDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //breakdown
                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetBreakdownCategory_Result, DTO.BreakdownCategoryDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetBreakdownCategoryOption_Result, DTO.BreakdownCategoryOptionDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetBreakdownManagementFee_Result, DTO.BreakdownManagementFeeDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductWizardMng_function_GetClientSpecialPackagingMethod_Result, DTO.ClientSpecialPackagingMethodDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));



                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

    }
}
