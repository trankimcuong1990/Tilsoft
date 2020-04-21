using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using System.Globalization;

namespace Module.FactoryRawMaterialMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialSearch_View, DTO.FactoryRawMaterialSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LogoFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.LogoFileLocation) ? s.LogoFileLocation : "no-image.jpg")))
                    .ForMember(d => d.LogoThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.LogoThumbnailLocation) ? s.LogoThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FactoryRawMaterialCertificates, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialCertificate_View))
                    .ForMember(d => d.FactoryRawMaterialQualityPersons, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialQualityPerson_View))
                    .ForMember(d => d.FactoryRawMaterialPricingPersons, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialPricingPerson_View))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_MaterialsPrice_View, DTO.MaterialsPrice>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                 .ForMember(d => d.AttachFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.AttachFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.AttachFileURL)))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_MaterialPriceHistory_View, DTO.MaterialPriceHistory>()
               .IgnoreAllNonExisting()
               .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
               .ForMember(d => d.AttachFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.AttachFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.AttachFileURL)))
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterial_View, DTO.FactoryRawMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.LogoFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.LogoFileLocation) ? s.LogoFileLocation : "no-image.jpg")))
                    .ForMember(d => d.LogoThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.LogoThumbnailLocation) ? s.LogoThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactoryRawMaterialTurnovers, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialTurnover_View))
                    .ForMember(d => d.FactoryRawMaterialCertificates, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialCertificate_View))
                    .ForMember(d => d.FactoryRawMaterialTests, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialTest_View))
                    .ForMember(d => d.FactoryRawMaterialQualityPersons, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialQualityPerson_View))
                    .ForMember(d => d.FactoryRawMaterialPricingPersons, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialPricingPerson_View))
                    .ForMember(d => d.FactoryRawPaymentTerms, o => o.MapFrom(s => s.FactoryRawMaterialMng_SupplierPaymentTerm_View))
                    .ForMember(d => d.FactoryRawMaterialImages, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialImage_View))
                    .ForMember(d => d.SubSupplierContracts, o => o.MapFrom(s => s.FactoryRawMaterialMng_SubSupplierContract_View))
                    .ForMember(d => d.SupplierContactQuickInfos, o => o.MapFrom(s => s.FactoryRawMaterialMng_SupplierContactQuickInfo_View))
                    .ForMember(d => d.supplierManagers, o => o.MapFrom(s => s.FactoryRawMaterialMng_SupplierManager_View))
                    .ForMember(d => d.supplierDirectors, o => o.MapFrom(s => s.FactoryRawMaterialMng_SupplierDirector_View))
                    .ForMember(d => d.supplierSampleTechnicals, o => o.MapFrom(s => s.FactoryRawMaterialMng_SupplierSampleTechnical_View))
                    .ForMember(d => d.FactoryRawMaterialProductGroupDTOs, o => o.MapFrom(s => s.FactoryRawMaterial_FactoryRawMaterialProductGroupDTO_View))
                    .ForMember(d => d.FactoryRawMaterialBusinessCardDTO, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialBusinessCard_View))
                    .ForMember(d => d.FactoryRawMaterialGalleryDTO, o => o.MapFrom(s => s.FactoryRawMaterialMng_FactoryRawMaterialGallery_View))
                    .ForMember(d => d.materialsPrices, o => o.MapFrom(s => s.FactoryRawMaterialMng_MaterialsPrice_View))

                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SearchProductionItem_View, DTO.MaterialPriceProductItemSeach>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.Units, o => o.MapFrom(s => s.FactoryRawMaterial_UnitPriceMaterials_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_MaterialsPrice_View, DTO.MaterialsPrice>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.materialPriceHistories, o => o.MapFrom(s => s.FactoryRawMaterialMng_MaterialPriceHistory_View))
                   .ForMember(d => d.Units, o => o.MapFrom(s => s.FactoryRawMaterial_UnitPriceMaterials_View))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialsPrice, MaterialsPrice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialPriceHistory, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterial, FactoryRawMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialCertificate, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialTest, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialPricingPerson, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialQualityPerson, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialImage, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialPaymentTerm, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialID, o => o.Ignore())
                    .ForMember(d => d.SubSupplierContract, o => o.Ignore())
                    .ForMember(d => d.SupplierContactQuickInfo, o => o.Ignore())
                    .ForMember(d => d.SupplierManager, o => o.Ignore())
                    .ForMember(d => d.SupplierDirector, o => o.Ignore())
                    .ForMember(d => d.SupplierSampleTechnical, o => o.Ignore())
                    .ForMember(d => d.MaterialsPrice, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialBusinessCard, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialProductGroupDTO, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialGallery, o => o.Ignore());


                AutoMapper.Mapper.CreateMap<DTO.MaterialsPrice, MaterialsPrice>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ValidFrom, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialPriceHistory, MaterialPriceHistory>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ValidFrom, o => o.Ignore())
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SubSupplierContract_View, DTO.SubSupplierContract>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ContractFileURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ContractFileURL) ? s.ContractFileURL : " ")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SubSupplierContract, SubSupplierContract>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.SubSupplierContractID, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<DTO.MaterialsPrice, MaterialsPrice>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.MaterialsPriceID, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_MaterialsPrice_View, DTO.MaterialsPrice>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.MaterialPriceHistory, MaterialPriceHistory>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.MaterialHistoryID, o => o.Ignore())
                     ;

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_MaterialPriceHistory_View, DTO.MaterialPriceHistory>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<FactoryRawMaterial_UnitPriceMaterials_View, DTO.Unit>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SearchProductionItem_View, DTO.MaterialPriceProductItemSeach>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_StatusMaterialsPrice_View, DTO.StatusMaterialPrice>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SupplierContactQuickInfo, SupplierContactQuickInfo>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.SupplierContactQuickInfoID, o => o.Ignore())
                   ;


                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialProductGroupDTO, FactoryRawMaterialProductGroupDTO>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.FactoryRawMaterialProductGroupID, o => o.Ignore())
                  ;


                AutoMapper.Mapper.CreateMap<DTO.SupplierManager, SupplierManager>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.SupplierManagerID, o => o.Ignore())
                  ;

                AutoMapper.Mapper.CreateMap<DTO.SupplierDirector, SupplierDirector>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SupplierDirectorID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SupplierSampleTechnical, SupplierSampleTechnical>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.SupplierSampleTechnicalID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_KeyRawMaterial_View, DTO.KeyRawMaterial>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));



                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialTurnover_View, DTO.FactoryRawMaterialTurnover>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialCertificate_View, DTO.FactoryRawMaterialCertificate>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.CertificateFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.CertificateFileUrl) ? s.CertificateFileUrl : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialTest_View, DTO.FactoryRawMaterialTest>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialPricingPerson_View, DTO.FactoryRawMaterialPricingPerson>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialQualityPerson_View, DTO.FactoryRawMaterialQualityPerson>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialImage_View, DTO.FactoryRawMaterialImage>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialCertificate, FactoryRawMaterialCertificate>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryRawMaterialCertificateID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialTest, FactoryRawMaterialTest>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryRawMaterialTestID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.KeyRawMaterial, KeyRawMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.KeyRawMaterialID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialPricingPerson, FactoryRawMaterialPricingPerson>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryRawMaterialPricingPersonID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialQualityPerson, FactoryRawMaterialQualityPerson>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryRawMaterialQualityPersonID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialImage, FactoryRawMaterialImage>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryRawMaterialImageID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawPaymentTerm, FactoryRawMaterialPaymentTerm>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.FactoryRawMaterialPaymentTermID, o => o.Ignore())
                  .ForMember(d => d.FactoryRawMaterialID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SupplierPaymentTerm_View, DTO.FactoryRawPaymentTerm>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SupportSupplierPaymentTerm_View, DTO.SupplierPaymentTerm>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<SupportMng_SubSupplierDocumentType_View, DTO.SubSupplierDocumentTypeDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SupplierContactQuickInfo_View, DTO.SupplierContactQuickInfo>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SupportMng_ProductGroup_View, DTO.ProductGroupDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SupplierManager_View, DTO.SupplierManager>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SupplierDirector_View, DTO.SupplierDirector>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_SupplierSampleTechnical_View, DTO.SupplierSampleTechnical>()
               .IgnoreAllNonExisting()
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterial_FactoryRawMaterialProductGroupDTO_View, DTO.FactoryRawMaterialProductGroupDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingCalendarMng_PurchasingCalendarAppointment_View, DTO.AppointmentDTO>()
               .IgnoreAllNonExisting()
               .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.RemindDate, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RemindTime, o => o.ResolveUsing(s => s.RemindTime.HasValue ? s.RemindTime.Value.ToString("HH:mm") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PurchasingCalendarAppointmentAttachedFileDTOs, o => o.MapFrom(s => s.PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View))
                    .ForMember(d => d.PurchasingCalendarUserDTOs, o => o.MapFrom(s => s.PurchasingCalendarMng_PurchasingCalendarUser_View))
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingCalendarMng_PurchasingCalendarAppointmentAttachedFile_View, DTO.PurchasingCalendarAppointmentAttachedFileDTO>()
               .IgnoreAllNonExisting()
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchasingCalendarMng_PurchasingCalendarUser_View, DTO.PurchasingCalendarUserDTO>()
               .IgnoreAllNonExisting()
               .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialBusinessCard_View, DTO.FactoryRawMaterialBusinessCardDTO>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FrontThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FrontThumbnailLocation) ? s.FrontThumbnailLocation : "no-image.jpg")))
                .ForMember(d => d.BehindThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.BehindThumbnailLocation) ? s.BehindThumbnailLocation : "no-image.jpg")))
                .ForMember(d => d.FrontFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FrontFileLocation) ? s.FrontFileLocation : "no-image.jpg")))
                .ForMember(d => d.BehindFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BehindFileLocation) ? s.BehindFileLocation : "no-image.jpg")))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

               AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialBusinessCardDTO, FactoryRawMaterialBusinessCard>()
               .IgnoreAllNonExisting()
               .ForMember(d => d.FactoryRawMaterialBusinessCardID, o => o.Ignore());

               AutoMapper.Mapper.CreateMap<SupportMng_User2_View, DTO.UsersDTO>()
              .IgnoreAllNonExisting()
              .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

               AutoMapper.Mapper.CreateMap<FactoryRawMaterialMng_FactoryRawMaterialGallery_View, DTO.FactoryRawMaterialGalleryDTO>()
               .IgnoreAllNonExisting()
               .ForMember(d => d.FactoryGalleryFile, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FactoryGalleryFile) ? s.FactoryGalleryFile : "no-image.jpg")))
               .ForMember(d => d.FactoryGalleryThumbnail, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FactoryGalleryThumbnail) ? s.FactoryGalleryThumbnail : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialGalleryDTO, FactoryRawMaterialGallery>()
                .IgnoreAllNonExisting()
                .ForMember(o => o.FactoryRawMaterialGalleryID, o => o.Ignore())
                .ForMember(o => o.FactoryRawMaterialID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.FactoryRawMaterialSearch> DB2DTO_FactoryRawMaterialSearchResultList(List<FactoryRawMaterialMng_FactoryRawMaterialSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_FactoryRawMaterialSearch_View>, List<DTO.FactoryRawMaterialSearch>>(dbItems);
        }

        public DTO.FactoryRawMaterial DB2DTO_FactoryRawMaterial(FactoryRawMaterialMng_FactoryRawMaterial_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryRawMaterialMng_FactoryRawMaterial_View, DTO.FactoryRawMaterial>(dbItem);
        }

        public List<DTO.KeyRawMaterial> DB2DTO_KeyRawMaterial(List<FactoryRawMaterialMng_KeyRawMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_KeyRawMaterial_View>, List<DTO.KeyRawMaterial>>(dbItems);
        }

        public List<DTO.KeyRawMaterial> DB2DTO_KeyRawMaterialList(List<FactoryRawMaterialMng_KeyRawMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_KeyRawMaterial_View>, List<DTO.KeyRawMaterial>>(dbItems);
        }


        public void DTO2BD(DTO.FactoryRawMaterial dtoItem, ref FactoryRawMaterial dbItem)
        {
            // FactoryRawMaterialCertificate
            if (dtoItem.FactoryRawMaterialCertificates != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialCertificate.ToArray())
                {
                    if (!dtoItem.FactoryRawMaterialCertificates.Select(o => o.FactoryRawMaterialCertificateID).Contains(item.FactoryRawMaterialCertificateID))
                    {
                        dbItem.FactoryRawMaterialCertificate.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawMaterialCertificates)
                {
                    FactoryRawMaterialCertificate dbFactoryRawMaterialCertificate;
                    if (item.FactoryRawMaterialCertificateID <= 0)
                    {
                        dbFactoryRawMaterialCertificate = new FactoryRawMaterialCertificate();
                        dbItem.FactoryRawMaterialCertificate.Add(dbFactoryRawMaterialCertificate);
                    }
                    else
                    {
                        dbFactoryRawMaterialCertificate = dbItem.FactoryRawMaterialCertificate.FirstOrDefault(o => o.FactoryRawMaterialCertificateID == item.FactoryRawMaterialCertificateID);
                    }
                    if (dbFactoryRawMaterialCertificate != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialCertificate, FactoryRawMaterialCertificate>(item, dbFactoryRawMaterialCertificate);
                    }
                }
            }

            // materialsPrices

            if (dtoItem.materialsPrices != null)
            {
                foreach (var item in dbItem.MaterialsPrice.ToArray())
                {
                    if (!dtoItem.materialsPrices.Select(o => o.MaterialsPriceID).Contains(item.MaterialsPriceID))
                    {
                        dbItem.MaterialsPrice.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.materialsPrices)
                {
                    MaterialsPrice dbmaterialPrice;
                    if (item.MaterialsPriceID <= 0)
                    {
                        dbmaterialPrice = new MaterialsPrice();
                        dbItem.MaterialsPrice.Add(dbmaterialPrice);
                        if (item.materialPriceHistories != null)
                        {
                            foreach (var itemhistorys in item.materialPriceHistories.ToArray())
                            {
                                if (!item.materialPriceHistories.Select(o => o.MaterialHistoryID).Contains(itemhistorys.MaterialHistoryID))
                                {
                                    item.materialPriceHistories.Remove(itemhistorys);
                                }
                            }
                         
                            //map child row
                            foreach (var itemhistorys in item.materialPriceHistories)
                            {
                                MaterialPriceHistory dbmaterialPricehistory;

                                if (itemhistorys.MaterialHistoryID <= 0)
                                {
                                    dbmaterialPricehistory = new MaterialPriceHistory();
                                    dbmaterialPrice.MaterialPriceHistory.Add(dbmaterialPricehistory);
                                    dbmaterialPricehistory.AttachFileHistory = item.AttachFile;
                                    dbmaterialPricehistory.RemarkHistory = item.Remark;
                                }
                                else
                                {
                                    dbmaterialPricehistory = dbmaterialPrice.MaterialPriceHistory.FirstOrDefault(o => o.MaterialHistoryID == itemhistorys.MaterialHistoryID);
                                }
                                if (dbmaterialPricehistory != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.MaterialPriceHistory, MaterialPriceHistory>(itemhistorys, dbmaterialPricehistory);
                                    dbmaterialPricehistory.ValidFrom = itemhistorys.ValidFrom.ConvertStringToDateTime();
                                }
                            }
                        }
                    }
                    else
                    {                        
                        dbmaterialPrice = dbItem.MaterialsPrice.FirstOrDefault(o => o.MaterialsPriceID == item.MaterialsPriceID);                        
                    }
                    if (dbmaterialPrice != null)
                    {
                        AutoMapper.Mapper.Map<DTO.MaterialsPrice, MaterialsPrice>(item, dbmaterialPrice);
                        dbmaterialPrice.ValidFrom = item.ValidFrom.ConvertStringToDateTime();
                    }
                }
            }


            // SupplierContactQuickInfo
            if (dtoItem.SupplierContactQuickInfos != null)
            {
                foreach (var item in dbItem.SupplierContactQuickInfo.ToArray())
                {
                    if (!dtoItem.SupplierContactQuickInfos.Select(o => o.FactoryRawMaterialID).Contains(item.FactoryRawMaterialID))
                    {
                        dbItem.SupplierContactQuickInfo.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.SupplierContactQuickInfos)
                {
                    SupplierContactQuickInfo dbSupplierContactQuickInfo;
                    if (item.SupplierContactQuickInfoId <= 0)
                    {
                        dbSupplierContactQuickInfo = new SupplierContactQuickInfo();
                        dbItem.SupplierContactQuickInfo.Add(dbSupplierContactQuickInfo);
                    }
                    else
                    {
                        dbSupplierContactQuickInfo = dbItem.SupplierContactQuickInfo.FirstOrDefault(o => o.FactoryRawMaterialID == item.FactoryRawMaterialID);
                    }
                    if (dbSupplierContactQuickInfo != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SupplierContactQuickInfo, SupplierContactQuickInfo>(item, dbSupplierContactQuickInfo);
                    }
                }
            }

            //Supplier Director
            if (dtoItem.supplierDirectors != null)
            {
                foreach (var item in dbItem.SupplierDirector.ToArray())
                {
                    if (!dtoItem.supplierDirectors.Select(o => o.FactoryRawMaterialID).Contains(item.FactoryRawMaterialID))
                    {
                        dbItem.SupplierDirector.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.supplierDirectors)
                {
                    SupplierDirector dbSupplierDirector;
                    if (item.SupplierDirectorID <= 0)
                    {
                        dbSupplierDirector = new SupplierDirector();
                        dbItem.SupplierDirector.Add(dbSupplierDirector);
                    }
                    else
                    {
                        dbSupplierDirector = dbItem.SupplierDirector.FirstOrDefault(o => o.FactoryRawMaterialID == item.FactoryRawMaterialID);
                    }
                    if (dbSupplierDirector != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SupplierDirector, SupplierDirector>(item, dbSupplierDirector);
                    }
                }
            }


            //Supplier Manager
            if (dtoItem.supplierManagers != null)
            {
                foreach (var item in dbItem.SupplierManager.ToArray())
                {
                    if (!dtoItem.supplierManagers.Select(o => o.FactoryRawMaterialID).Contains(item.FactoryRawMaterialID))
                    {
                        dbItem.SupplierManager.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.supplierManagers)
                {
                    SupplierManager dbSupplierManager;
                    if (item.SupplierManagerID <= 0)
                    {
                        dbSupplierManager = new SupplierManager();
                        dbItem.SupplierManager.Add(dbSupplierManager);
                    }
                    else
                    {
                        dbSupplierManager = dbItem.SupplierManager.FirstOrDefault(o => o.FactoryRawMaterialID == item.FactoryRawMaterialID);
                    }
                    if (dbSupplierManager != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SupplierManager, SupplierManager>(item, dbSupplierManager);
                    }
                }
            }

            //SupplierSampleTechnical
            if (dtoItem.supplierSampleTechnicals != null)
            {
                foreach (var item in dbItem.SupplierSampleTechnical.ToArray())
                {
                    if (!dtoItem.supplierSampleTechnicals.Select(o => o.FactoryRawMaterialID).Contains(item.FactoryRawMaterialID))
                    {
                        dbItem.SupplierSampleTechnical.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.supplierSampleTechnicals)
                {
                    SupplierSampleTechnical dbSupplierSampleTechnical;
                    if (item.SupplierSampleTechnicalID <= 0)
                    {
                        dbSupplierSampleTechnical = new SupplierSampleTechnical();
                        dbItem.SupplierSampleTechnical.Add(dbSupplierSampleTechnical);
                    }
                    else
                    {
                        dbSupplierSampleTechnical = dbItem.SupplierSampleTechnical.FirstOrDefault(o => o.FactoryRawMaterialID == item.FactoryRawMaterialID);
                    }
                    if (dbSupplierSampleTechnical != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SupplierSampleTechnical, SupplierSampleTechnical>(item, dbSupplierSampleTechnical);
                    }
                }
            }

            if (dtoItem.FactoryRawMaterialProductGroupDTOs != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialProductGroupDTO.ToArray())
                {
                    if (!dtoItem.FactoryRawMaterialProductGroupDTOs.Select(o => o.FactoryRawMaterialID).Contains(item.FactoryRawMaterialID))
                    {
                        dbItem.FactoryRawMaterialProductGroupDTO.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawMaterialProductGroupDTOs)
                {
                    FactoryRawMaterialProductGroupDTO dbFactoryRawMaterialProductGroupDTO;
                    if (item.FactoryRawMaterialProductGroupID <= 0)
                    {
                        dbFactoryRawMaterialProductGroupDTO = new FactoryRawMaterialProductGroupDTO();
                        dbItem.FactoryRawMaterialProductGroupDTO.Add(dbFactoryRawMaterialProductGroupDTO);
                    }
                    else
                    {
                        dbFactoryRawMaterialProductGroupDTO = dbItem.FactoryRawMaterialProductGroupDTO.FirstOrDefault(o => o.FactoryRawMaterialID == item.FactoryRawMaterialID);
                    }
                    if (dbFactoryRawMaterialProductGroupDTO != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialProductGroupDTO, FactoryRawMaterialProductGroupDTO>(item, dbFactoryRawMaterialProductGroupDTO);
                    }
                }
            }



            // FactoryRawMaterialTest
            if (dtoItem.FactoryRawMaterialTests != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialTest.ToArray())
                {
                    if (!dtoItem.FactoryRawMaterialTests.Select(o => o.FactoryRawMaterialTestID).Contains(item.FactoryRawMaterialTestID))
                    {
                        dbItem.FactoryRawMaterialTest.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawMaterialTests)
                {
                    FactoryRawMaterialTest dbFactoryRawMaterialTest;
                    if (item.FactoryRawMaterialTestID <= 0)
                    {
                        dbFactoryRawMaterialTest = new FactoryRawMaterialTest();
                        dbItem.FactoryRawMaterialTest.Add(dbFactoryRawMaterialTest);
                    }
                    else
                    {
                        dbFactoryRawMaterialTest = dbItem.FactoryRawMaterialTest.FirstOrDefault(o => o.FactoryRawMaterialTestID == item.FactoryRawMaterialTestID);
                    }
                    if (dbFactoryRawMaterialTest != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialTest, FactoryRawMaterialTest>(item, dbFactoryRawMaterialTest);
                    }
                }
            }

            // FactoryRawMaterialPricingPerson
            if (dtoItem.FactoryRawMaterialPricingPersons != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialPricingPerson.ToArray())
                {
                    if (!dtoItem.FactoryRawMaterialPricingPersons.Select(o => o.FactoryRawMaterialPricingPersonID).Contains(item.FactoryRawMaterialPricingPersonID))
                    {
                        dbItem.FactoryRawMaterialPricingPerson.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawMaterialPricingPersons)
                {
                    FactoryRawMaterialPricingPerson dbFactoryRawMaterialPricingPerson;
                    if (item.FactoryRawMaterialPricingPersonID <= 0)
                    {
                        dbFactoryRawMaterialPricingPerson = new FactoryRawMaterialPricingPerson();
                        dbItem.FactoryRawMaterialPricingPerson.Add(dbFactoryRawMaterialPricingPerson);
                    }
                    else
                    {
                        dbFactoryRawMaterialPricingPerson = dbItem.FactoryRawMaterialPricingPerson.FirstOrDefault(o => o.FactoryRawMaterialPricingPersonID == item.FactoryRawMaterialPricingPersonID);
                    }
                    if (dbFactoryRawMaterialPricingPerson != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialPricingPerson, FactoryRawMaterialPricingPerson>(item, dbFactoryRawMaterialPricingPerson);
                    }
                }
            }

            // FactoryRawMaterialQualityPerson
            if (dtoItem.FactoryRawMaterialQualityPersons != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialQualityPerson.ToArray())
                {
                    if (!dtoItem.FactoryRawMaterialQualityPersons.Select(o => o.FactoryRawMaterialQualityPersonID).Contains(item.FactoryRawMaterialQualityPersonID))
                    {
                        dbItem.FactoryRawMaterialQualityPerson.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawMaterialQualityPersons)
                {
                    FactoryRawMaterialQualityPerson dbFactoryRawMaterialQualityPerson;
                    if (item.FactoryRawMaterialQualityPersonID <= 0)
                    {
                        dbFactoryRawMaterialQualityPerson = new FactoryRawMaterialQualityPerson();
                        dbItem.FactoryRawMaterialQualityPerson.Add(dbFactoryRawMaterialQualityPerson);
                    }
                    else
                    {
                        dbFactoryRawMaterialQualityPerson = dbItem.FactoryRawMaterialQualityPerson.FirstOrDefault(o => o.FactoryRawMaterialQualityPersonID == item.FactoryRawMaterialQualityPersonID);
                    }
                    if (dbFactoryRawMaterialQualityPerson != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialQualityPerson, FactoryRawMaterialQualityPerson>(item, dbFactoryRawMaterialQualityPerson);
                    }
                }
            }

            // FactoryRawMaterialImage
            if (dtoItem.FactoryRawMaterialImages != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialImage.ToArray())
                {
                    if (!dtoItem.FactoryRawMaterialImages.Select(o => o.FactoryRawMaterialImageID).Contains(item.FactoryRawMaterialImageID))
                    {
                        dbItem.FactoryRawMaterialImage.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawMaterialImages)
                {
                    FactoryRawMaterialImage dbFactoryRawMaterialImage;
                    if (item.FactoryRawMaterialImageID <= 0)
                    {
                        dbFactoryRawMaterialImage = new FactoryRawMaterialImage();
                        dbItem.FactoryRawMaterialImage.Add(dbFactoryRawMaterialImage);
                    }
                    else
                    {
                        dbFactoryRawMaterialImage = dbItem.FactoryRawMaterialImage.FirstOrDefault(o => o.FactoryRawMaterialImageID == item.FactoryRawMaterialImageID);
                    }
                    if (dbFactoryRawMaterialImage != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialImage, FactoryRawMaterialImage>(item, dbFactoryRawMaterialImage);
                    }
                }
            }
            // Business Card
            if (dtoItem.FactoryRawMaterialBusinessCardDTO != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialBusinessCard.ToArray())
                {
                    if (!dtoItem.FactoryRawMaterialBusinessCardDTO.Select(o => o.FactoryRawMaterialBusinessCardID).Contains(item.FactoryRawMaterialBusinessCardID))
                    {
                        dbItem.FactoryRawMaterialBusinessCard.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawMaterialBusinessCardDTO)
                {
                    FactoryRawMaterialBusinessCard dbFactoryBusinessCard;
                    if (item.FactoryRawMaterialBusinessCardID <= 0)
                    {
                        dbFactoryBusinessCard = new FactoryRawMaterialBusinessCard();
                        dbItem.FactoryRawMaterialBusinessCard.Add(dbFactoryBusinessCard);
                    }
                    else
                    {
                        dbFactoryBusinessCard = dbItem.FactoryRawMaterialBusinessCard.FirstOrDefault(o => o.FactoryRawMaterialBusinessCardID == item.FactoryRawMaterialBusinessCardID);
                    }
                    if (dbFactoryBusinessCard != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialBusinessCardDTO, FactoryRawMaterialBusinessCard>(item, dbFactoryBusinessCard);
                    }
                }
            }
            // FactoryRawMaterialGallery for add video 360
            if (dtoItem.FactoryRawMaterialGalleryDTO != null)
            {
                foreach (FactoryRawMaterialGallery dbFactoryGallery in dbItem.FactoryRawMaterialGallery.ToList())
                {
                    if (!dtoItem.FactoryRawMaterialGalleryDTO.Select(s => s.FactoryRawMaterialGalleryID).Contains(dbFactoryGallery.FactoryRawMaterialGalleryID))
                    {
                        dbItem.FactoryRawMaterialGallery.Remove(dbFactoryGallery);
                    }
                }

                foreach (DTO.FactoryRawMaterialGalleryDTO dtoFactoryGallery in dtoItem.FactoryRawMaterialGalleryDTO.ToList())
                {
                    FactoryRawMaterialGallery dbFactoryGallery;

                    if (dtoFactoryGallery.FactoryRawMaterialGalleryID <= 0)
                    {
                        dbFactoryGallery = new FactoryRawMaterialGallery();
                        dbItem.FactoryRawMaterialGallery.Add(dbFactoryGallery);
                    }
                    else
                    {
                        dbFactoryGallery = dbItem.FactoryRawMaterialGallery.FirstOrDefault(o => o.FactoryRawMaterialGalleryID == dtoFactoryGallery.FactoryRawMaterialGalleryID);
                    }

                    if (dbFactoryGallery != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialGalleryDTO, FactoryRawMaterialGallery>(dtoFactoryGallery, dbFactoryGallery);
                    }
                }
            }
            /*
             * MAP & CHECK ClientContract
             */
            List<SubSupplierContract> contract_tobe_deleted = new List<SubSupplierContract>();
            if (dtoItem.SubSupplierContracts != null)
            {
                //CHECK
                foreach (var dbContract in dbItem.SubSupplierContract)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.SubSupplierContracts.Select(s => s.SubSupplierContractID).Contains(dbContract.SubSupplierContractID))
                    {
                        contract_tobe_deleted.Add(dbContract);
                    }
                    else //DB IS EXIST IN DB
                    {
                        //contractDetail_tobe_deleted = new List<ClientContractDetail>();
                        //foreach (var dbContractDetail in dbContract.ClientContractDetail)
                        //{
                        //    if (!dtoItem.ClientContracts.Where(o => o.ClientContractID == dbContract.ClientContractID).FirstOrDefault().ClientContractDetails.Select(x => x.ClientContractDetailID).Contains(dbContractDetail.ClientContractDetailID))
                        //    {
                        //        contractDetail_tobe_deleted.Add(dbContractDetail);
                        //    }
                        //}
                        //foreach (var dbDetailDescription in contractDetail_tobe_deleted)
                        //{
                        //    dbContract.ClientContractDetail.Remove(dbDetailDescription);
                        //}
                    }
                }

                foreach (var dbContract in contract_tobe_deleted)
                {
                    dbItem.SubSupplierContract.Remove(dbContract);
                }

                //MAP
                SubSupplierContract _dbContract;
                //ClientContractDetail _dbContractDetail;
                foreach (var dtoContract in dtoItem.SubSupplierContracts)
                {
                    if (dtoContract.SubSupplierContractID < 0)
                    {
                        _dbContract = new SubSupplierContract();
                        //if (dtoContract.ClientContractDetails != null)
                        //{
                        //    foreach (var dtoContractDetail in dtoContract.ClientContractDetails)
                        //    {
                        //        _dbContractDetail = new ClientContractDetail();
                        //        _dbContract.ClientContractDetail.Add(_dbContractDetail);
                        //        AutoMapper.Mapper.Map<DTO.ClientMng.ClientContractDetail, ClientContractDetail>(dtoContractDetail, _dbContractDetail);
                        //    }
                        //}
                        dbItem.SubSupplierContract.Add(_dbContract);
                    }
                    else
                    {
                        _dbContract = dbItem.SubSupplierContract.FirstOrDefault(o => o.SubSupplierContractID == dtoContract.SubSupplierContractID);
                        //if (_dbContract != null && dtoContract.ClientContractDetails != null)
                        //{
                        //    foreach (var dtoContractDetail in dtoContract.ClientContractDetails)
                        //    {
                        //        if (dtoContractDetail.ClientContractDetailID < 0)
                        //        {
                        //            _dbContractDetail = new ClientContractDetail();
                        //            _dbContract.ClientContractDetail.Add(_dbContractDetail);
                        //        }
                        //        else
                        //        {
                        //            _dbContractDetail = _dbContract.ClientContractDetail.FirstOrDefault(o => o.ClientContractDetailID == dtoContractDetail.ClientContractDetailID);
                        //        }
                        //        if (_dbContractDetail != null)
                        //        {
                        //            AutoMapper.Mapper.Map<DTO.ClientMng.ClientContractDetail, ClientContractDetail>(dtoContractDetail, _dbContractDetail);
                        //        }
                        //    }
                        //}
                    }
                    if (_dbContract != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SubSupplierContract, SubSupplierContract>(dtoContract, _dbContract);
                    }
                }
            }

            //FactoryRawMaterialPaymentTerm
            if (dtoItem.FactoryRawPaymentTerms != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialPaymentTerm.ToArray())
                {
                    if (!dtoItem.FactoryRawPaymentTerms.Select(o => o.FactoryRawMaterialPaymentTermID).Contains(item.FactoryRawMaterialPaymentTermID))
                    {
                        dbItem.FactoryRawMaterialPaymentTerm.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawPaymentTerms)
                {
                    FactoryRawMaterialPaymentTerm dbFactoryRawMaterialPaymentTerm;
                    if (item.FactoryRawMaterialPaymentTermID <= 0)
                    {
                        dbFactoryRawMaterialPaymentTerm = new FactoryRawMaterialPaymentTerm();
                        dbItem.FactoryRawMaterialPaymentTerm.Add(dbFactoryRawMaterialPaymentTerm);
                    }
                    else
                    {
                        dbFactoryRawMaterialPaymentTerm = dbItem.FactoryRawMaterialPaymentTerm.FirstOrDefault(o => o.FactoryRawMaterialPaymentTermID == item.FactoryRawMaterialPaymentTermID);
                    }
                    if (dbFactoryRawMaterialPaymentTerm != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawPaymentTerm, FactoryRawMaterialPaymentTerm>(item, dbFactoryRawMaterialPaymentTerm);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.FactoryRawMaterial, FactoryRawMaterial>(dtoItem, dbItem);
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }
        public List<DTO.SupplierPaymentTerm> DB2DTO_SupplierPaymentTermList(List<FactoryRawMaterialMng_SupportSupplierPaymentTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_SupportSupplierPaymentTerm_View>, List<DTO.SupplierPaymentTerm>>(dbItems);
        }

        public List<DTO.SubSupplierDocumentTypeDTO> DB2DTO_SubSupplierDocumentTypeDTO(List<SupportMng_SubSupplierDocumentType_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SubSupplierDocumentType_View>, List<DTO.SubSupplierDocumentTypeDTO>>(dbItem);
        }

        public List<DTO.AppointmentDTO> DB2DTO_SubSupplierAppointmentDTO(List<PurchasingCalendarMng_PurchasingCalendarAppointment_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchasingCalendarMng_PurchasingCalendarAppointment_View>, List<DTO.AppointmentDTO>>(dbItem);
        }

        public List<DTO.ProductGroupDTO> DB2DTO_ProductGroup(List<SupportMng_ProductGroup_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductGroup_View>, List<DTO.ProductGroupDTO>>(dbitems);
        }
        public List<DTO.MaterialsPrice> DB2DTO_MaterailPrice(List<FactoryRawMaterialMng_MaterialsPrice_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_MaterialsPrice_View>, List<DTO.MaterialsPrice>>(dbitems);
        }
        public List<DTO.StatusMaterialPrice> DB2DTO_StatusMaterailPrice(List<FactoryRawMaterialMng_StatusMaterialsPrice_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterialMng_StatusMaterialsPrice_View>, List<DTO.StatusMaterialPrice>>(dbitems);
        }
        public List<DTO.Unit> DB2DTO_Unit(List<FactoryRawMaterial_UnitPriceMaterials_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<FactoryRawMaterial_UnitPriceMaterials_View>, List<DTO.Unit>>(dbitems);
        }
        
        public List<DTO.UsersDTO> DB2DTO_UsersDTO(List<SupportMng_User2_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_User2_View>, List<DTO.UsersDTO>>(dbItem);
        }
    }
}
