using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.FactoryMng2.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryMng2_FactorySearchResult_View, DTO.FactorySearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LogoImage_DisplayURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.LogoImage_DisplayURL) ? s.LogoImage_DisplayURL : "no-image.jpg")))
                    .ForMember(d => d.FactoryDirectors, o => o.MapFrom(s => s.FactoryMng2_FactoryDirector_View))
                    .ForMember(d => d.FactoryManagers, o => o.MapFrom(s => s.FactoryMng2_FactoryManager_View))
                    .ForMember(d => d.FactorySampleTechnicals, o => o.MapFrom(s => s.FactoryMng2_FactorySampleTechnical_View))
                    .ForMember(d => d.FactoryResponsiblePersons, o => o.MapFrom(s => s.FactoryMng2_FactoryResponsiblePerson_View))
                    //.ForMember(d => d.FactorySuppliers, o => o.MapFrom(s => s.FactoryMng2_FactorySupplier_View))
                    .ForMember(d => d.FactoryRawMaterialSuppliers, o => o.MapFrom(s => s.FactoryMng2_FactoryRawMaterialSupplier_View))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_Factory_View, DTO.Factory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.LogoImage_DisplayURL, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.LogoImage_DisplayURL) ? s.LogoImage_DisplayURL : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FactoryImages, o => o.MapFrom(s => s.FactoryMng2_FactoryImage_View))
                    .ForMember(d => d.FactoryBusinessCard, o => o.MapFrom(s => s.FactoryMng2_FactoryBusinessCard_View))
                    .ForMember(d => d.FactoryDirectors, o => o.MapFrom(s => s.FactoryMng2_FactoryDirector_View))
                    .ForMember(d => d.FactoryManagers, o => o.MapFrom(s => s.FactoryMng2_FactoryManager_View))
                    .ForMember(d => d.FactoryPricings, o => o.MapFrom(s => s.FactoryMng2_FactoryPricing_View))
                    .ForMember(d => d.FactorySampleTechnicals, o => o.MapFrom(s => s.FactoryMng2_FactorySampleTechnical_View))
                    .ForMember(d => d.FactoryResponsiblePersons, o => o.MapFrom(s => s.FactoryMng2_FactoryResponsiblePerson_View))
                    .ForMember(d => d.FactoryRawMaterialSuppliers, o => o.MapFrom(s => s.FactoryMng2_FactoryRawMaterialSupplier_View))
                    //.ForMember(d => d.FactorySuppliers, o => o.MapFrom(s => s.FactoryMng2_FactorySupplier_View))
                    .ForMember(d => d.FactoryInHouseTests, o => o.MapFrom(s => s.FactoryMng2_FactoryInHouseTest_View))
                    .ForMember(d => d.FactoryCertificates, o => o.MapFrom(s => s.FactoryMng2_FactoryCertificate_View))
                    .ForMember(d => d.MonthlyCapacityByCurrentSeasons, o => o.MapFrom(s => s.FactoryMng2_MonthlyCapacityCurrentSeason_View))
                    .ForMember(d => d.MonthlyCapacityByPreSeasons, o => o.MapFrom(s => s.FactoryMng2_MonthlyCapacityPreviousSeason_View))
                    .ForMember(d => d.FactoryTurnovers, o => o.MapFrom(s => s.FactoryMng2_FactoryTurnover_View))
                    .ForMember(d => d.FactoryExpectedCapacities, o => o.MapFrom(s => s.FactoryMng2_FactoryExpectedCapacity_View))
                    .ForMember(d => d.AppointmentDTOs, o => o.MapFrom(s => s.FactoryMng2_Appointment_View))
                    //.ForMember(d => d.factoryCapacityByWeeks, o => o.MapFrom(s => s.FactoryMng2_CapacityByWeeks_View))
                    .ForMember(d => d.FactoryGalleries, o => o.MapFrom(s => s.FactoryMng2_FactoryGallery_View))
                    //.ForMember(d => d.factoryDocuments, o => o.MapFrom(s => s.FactoryMng2_FactoryDocument_View)
                    .ForMember(d => d.FactoryContactQuickInfos, o => o.MapFrom(s => s.FactoryMng2_FactoryContactQuickInfo_View))
                    .ForMember(d => d.factoryDocuments, o => o.MapFrom(s => s.FactoryMng2_FactoryDocument_View))
                    .ForMember(d => d.FactoryProductGroupDTOs, o => o.MapFrom(s => s.FactoryMng2_FactoryProductGroup_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.Factory, Factory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FactoryImage, o => o.Ignore())
                    .ForMember(d => d.FactoryBusinessCard, o => o.Ignore())
                    .ForMember(d => d.FactoryDirector, o => o.Ignore())
                    .ForMember(d => d.FactoryManager, o => o.Ignore())
                    .ForMember(d => d.FactoryPricing, o => o.Ignore())
                    .ForMember(d => d.FactorySampleTechnical, o => o.Ignore())
                    .ForMember(d => d.FactoryResponsiblePerson, o => o.Ignore())
                    .ForMember(d => d.FactoryRawMaterialSupplier, o => o.Ignore())
                    .ForMember(d => d.FactoryInHouseTest, o => o.Ignore())
                    .ForMember(d => d.FactoryCertificate, o => o.Ignore())
                    .ForMember(d => d.FactoryTurnover, o => o.Ignore())
                    .ForMember(d => d.FactoryExpectedCapacity, o => o.Ignore())
                    .ForMember(d => d.FactoryContactQuickInfo, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.FactoryID, o => o.Ignore())
                    .ForMember(d => d.FactoryGallery, o => o.Ignore())
                    .ForMember(d => d.FactoryDocument, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryImage_View, DTO.FactoryImage>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                   //.ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_Appointment_View, DTO.AppointmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartTime, o => o.ResolveUsing(s => s.StartTime.HasValue ? s.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.EndTime, o => o.ResolveUsing(s => s.EndTime.HasValue ? s.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryDirector_View, DTO.FactoryDirector>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryManager_View, DTO.FactoryManager>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryPricing_View, DTO.FactoryPricing>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactorySampleTechnical_View, DTO.FactorySampleTechnical>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryResponsiblePerson_View, DTO.FactoryResponsiblePerson>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryRawMaterialSupplier_View, DTO.FactoryRawMaterialSupplier>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryInHouseTest_View, DTO.FactoryInHouseTest>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryCertificate_View, DTO.FactoryCertificate>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.CertificateFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.CertificateFileUrl) ? s.CertificateFileUrl : "no-image.jpg")))
                   .ForMember(d => d.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : "")) // change ui calendar 21 FEB 2018.
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_MonthlyCapacityCurrentSeason_View, DTO.FactoryMonthlyCapacity>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_MonthlyCapacityPreviousSeason_View, DTO.FactoryMonthlyCapacity>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryTurnover_View, DTO.FactoryTurnover>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryExpectedCapacity_View, DTO.FactoryExpectedCapacity>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<FactoryMng2_CapacityByWeeks_View, DTO.FactoryCapacityByWeek>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryCapacityByWeek_View, DTO.FactoryCapacityByWeek>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryImage, FactoryImage>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.FactoryImageID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryBusinessCard, FactoryBusinessCard>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.FactoryBusinessCardID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryBusinessCard_View, DTO.FactoryBusinessCard>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.FrontThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FrontThumbnailLocation) ? s.FrontThumbnailLocation : "no-image.jpg")))
                  .ForMember(d => d.BehindThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.BehindThumbnailLocation) ? s.BehindThumbnailLocation : "no-image.jpg")))
                  .ForMember(d => d.FrontFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FrontFileLocation) ? s.FrontFileLocation : "no-image.jpg")))
                  .ForMember(d => d.BehindFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BehindFileLocation) ? s.BehindFileLocation : "no-image.jpg")))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryDirector, FactoryDirector>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryDirectorID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryManager, FactoryManager>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryManagerID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryPricing, FactoryPricing>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryPricingID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactorySampleTechnical, FactorySampleTechnical>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactorySampleTechnicalID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryResponsiblePerson, FactoryResponsiblePerson>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryResponsiblePersonID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryRawMaterialSupplier, FactoryRawMaterialSupplier>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryRawMaterialSupplierID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryInHouseTest, FactoryInHouseTest>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryInHouseTestID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryCertificate, FactoryCertificate>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryCertificateID, o => o.Ignore())
                   .ForMember(d => d.ValidUntil, o => o.Ignore()); //added

                AutoMapper.Mapper.CreateMap<DTO.FactoryTurnover, FactoryTurnover>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryTurnoverID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryExpectedCapacity, FactoryExpectedCapacity>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactoryExpectedCapacityID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryCapacityByWeek, FactoryCapacity>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryCapacityID, o => o.Ignore());

                // FactoryRawMaterialSupplier
                AutoMapper.Mapper.CreateMap<FactoryMng2_List_FactoryRawMaterialSupplier, DTO.FactoryRawMaterialSupplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_List_Supplier, DTO.Supplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_Employee_View, DTO.EmployeeDepartmentDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                

                AutoMapper.Mapper.CreateMap<SupportMng_ProductGroup_View, DTO.ProductGroupDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                

                AutoMapper.Mapper.CreateMap<FactoryMng2_WeekInforRange_View, DTO.WeekInforRangeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactorySupplier, FactorySupplier>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FactorySupplierID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactorySupplier_View, DTO.FactorySupplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryOrderTurnover_View, DTO.FactoryOrderTurnover>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryGallery_View, DTO.FactoryGalleryDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryGalleryFile, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FactoryGalleryFile) ? s.FactoryGalleryFile : "no-image.jpg")))
                    .ForMember(d => d.FactoryGalleryThumbnail, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FactoryGalleryThumbnail) ? s.FactoryGalleryThumbnail : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<DTO.FactoryGalleryDTO, FactoryGallery>()
                    .IgnoreAllNonExisting()
                    .ForMember(o => o.FactoryGalleryID, o => o.Ignore())
                    .ForMember(o => o.FactoryID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryDocument_View, DTO.FactoryDocumentDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<DTO.FactoryDocumentDTO, FactoryDocument>()
                    .IgnoreAllNonExisting()
                    .ForMember(o => o.FactoryDocmentID, o => o.Ignore())
                    .ForMember(o => o.FactoryID, o => o.Ignore())
                    .ForMember(o => o.FactoryDocumentFile, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
     
                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryContactQuickInfo_View, DTO.FactoryContactQuickInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryProductGroup_View, DTO.FactoryProductGroupDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));                

                AutoMapper.Mapper.CreateMap<FactoryMng2_FactoryExpectedCapacitySearch_View, DTO.FactoryExpectedCapacitySearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryContactQuickInfo, FactoryContactQuickInfo>()
                   .IgnoreAllNonExisting()
                   .ForMember(o => o.FactoryContactQuickInfoID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryProductGroupDTO, FactoryProductGroup>()
                   .IgnoreAllNonExisting()
                   .ForMember(o => o.FactoryProductGroupID, o => o.Ignore());
                AutoMapper.Mapper.CreateMap<Factory2Mng_PersonInCharge_View, DTO.PersonInChargeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_User2_View, DTO.UsersDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.FactorySearchResult> DB2DTO_FactorySearchResultList(List<FactoryMng2_FactorySearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMng2_FactorySearchResult_View>, List<DTO.FactorySearchResult>>(dbItems);
        }

        public DTO.Factory DB2DTO_Factory(FactoryMng2_Factory_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryMng2_Factory_View, DTO.Factory>(dbItem);
        }

        public List<DTO.FactoryCapacityByWeek> DB2DTO_GetSeasonByWeeks(List<FactoryMng2_FactoryCapacityByWeek_View> nlList)
        {
            return AutoMapper.Mapper.Map<List<FactoryMng2_FactoryCapacityByWeek_View>, List<DTO.FactoryCapacityByWeek>>(nlList);
        }

        public List<DTO.WeekInforRangeDTO> DB2DTO_WeeKInforRange(List<FactoryMng2_WeekInforRange_View> wList)
        {
            return AutoMapper.Mapper.Map<List<FactoryMng2_WeekInforRange_View>, List<DTO.WeekInforRangeDTO>>(wList);
        }

        public List<DTO.FactoryRawMaterialSupplier> DB2DTO_FactoryRawMaterialSupplier(List<FactoryMng2_List_FactoryRawMaterialSupplier> dbitems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMng2_List_FactoryRawMaterialSupplier>, List<DTO.FactoryRawMaterialSupplier>>(dbitems);
        }

        public List<DTO.Supplier> DB2DTO_Supplier(List<FactoryMng2_List_Supplier> dbitems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMng2_List_Supplier>, List<DTO.Supplier>>(dbitems);
        }
        public List<DTO.EmployeeDepartmentDTO> DB2DTO_EmployeeDepartment(List<FactoryMng2_Employee_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<FactoryMng2_Employee_View>, List<DTO.EmployeeDepartmentDTO>>(dbitems);
        }
        public List<DTO.ProductGroupDTO> DB2DTO_ProductGroup(List<SupportMng_ProductGroup_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductGroup_View>, List<DTO.ProductGroupDTO>>(dbitems);
        }
        public void DTO2BD(DTO.Factory dtoItem, ref Factory dbItem, int userId)
        {
            // FactoryImage
            if (dtoItem.FactoryImages != null)
            {
                foreach (var item in dbItem.FactoryImage.ToArray())
                {
                    if (!dtoItem.FactoryImages.Select(o => o.FactoryImageID).Contains(item.FactoryImageID))
                    {
                        dbItem.FactoryImage.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryImages)
                {
                    FactoryImage dbFactoryImage;
                    if (item.FactoryImageID <= 0)
                    {
                        dbFactoryImage = new FactoryImage();
                        dbItem.FactoryImage.Add(dbFactoryImage);
                    }
                    else
                    {
                        dbFactoryImage = dbItem.FactoryImage.FirstOrDefault(o => o.FactoryImageID == item.FactoryImageID);
                    }
                    if (dbFactoryImage != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryImage, FactoryImage>(item, dbFactoryImage);
                    }
                }
            }

            // Business Card
            if (dtoItem.FactoryBusinessCard != null)
            {
                foreach (var item in dbItem.FactoryBusinessCard.ToArray())
                {
                    if (!dtoItem.FactoryBusinessCard.Select(o => o.FactoryBusinessCardID).Contains(item.FactoryBusinessCardID))
                    {
                        dbItem.FactoryBusinessCard.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryBusinessCard)
                {
                    FactoryBusinessCard dbFactoryBusinessCard;
                    if (item.FactoryBusinessCardID <= 0)
                    {
                        dbFactoryBusinessCard = new FactoryBusinessCard();
                        dbItem.FactoryBusinessCard.Add(dbFactoryBusinessCard);
                    }
                    else
                    {
                        dbFactoryBusinessCard = dbItem.FactoryBusinessCard.FirstOrDefault(o => o.FactoryBusinessCardID == item.FactoryBusinessCardID);
                    }
                    if (dbFactoryBusinessCard != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryBusinessCard, FactoryBusinessCard>(item, dbFactoryBusinessCard);
                    }
                }
            }

            // FactoryDirector
            if (dtoItem.FactoryDirectors != null)
            {
                foreach (var item in dbItem.FactoryDirector.ToArray())
                {
                    if (!dtoItem.FactoryDirectors.Select(o => o.FactoryDirectorID).Contains(item.FactoryDirectorID))
                    {
                        dbItem.FactoryDirector.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryDirectors)
                {
                    FactoryDirector dbFactoryDirector;
                    if (item.FactoryDirectorID <= 0)
                    {
                        dbFactoryDirector = new FactoryDirector();
                        dbItem.FactoryDirector.Add(dbFactoryDirector);
                    }
                    else
                    {
                        dbFactoryDirector = dbItem.FactoryDirector.FirstOrDefault(o => o.FactoryDirectorID == item.FactoryDirectorID);
                    }
                    if (dbFactoryDirector != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryDirector, FactoryDirector>(item, dbFactoryDirector);
                    }
                }
            }

            // FactoryManager
            if (dtoItem.FactoryManagers != null)
            {
                foreach (var item in dbItem.FactoryManager.ToArray())
                {
                    if (!dtoItem.FactoryManagers.Select(o => o.FactoryManagerID).Contains(item.FactoryManagerID))
                    {
                        dbItem.FactoryManager.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryManagers)
                {
                    FactoryManager dbFactoryManager;
                    if (item.FactoryManagerID <= 0)
                    {
                        dbFactoryManager = new FactoryManager();
                        dbItem.FactoryManager.Add(dbFactoryManager);
                    }
                    else
                    {
                        dbFactoryManager = dbItem.FactoryManager.FirstOrDefault(o => o.FactoryManagerID == item.FactoryManagerID);
                    }
                    if (dbFactoryManager != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryManager, FactoryManager>(item, dbFactoryManager);
                    }
                }
            }

            // FactoryPricing
            if (dtoItem.FactoryPricings != null)
            {
                foreach (var item in dbItem.FactoryPricing.ToArray())
                {
                    if (!dtoItem.FactoryPricings.Select(o => o.FactoryPricingID).Contains(item.FactoryPricingID))
                    {
                        dbItem.FactoryPricing.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryPricings)
                {
                    FactoryPricing dbFactoryPricing;
                    if (item.FactoryPricingID <= 0)
                    {
                        dbFactoryPricing = new FactoryPricing();
                        dbItem.FactoryPricing.Add(dbFactoryPricing);
                    }
                    else
                    {
                        dbFactoryPricing = dbItem.FactoryPricing.FirstOrDefault(o => o.FactoryPricingID == item.FactoryPricingID);
                    }
                    if (dbFactoryPricing != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryPricing, FactoryPricing>(item, dbFactoryPricing);
                    }
                }
            }

            // FactorySampleTechnical
            if (dtoItem.FactorySampleTechnicals != null)
            {
                foreach (var item in dbItem.FactorySampleTechnical.ToArray())
                {
                    if (!dtoItem.FactorySampleTechnicals.Select(o => o.FactorySampleTechnicalID).Contains(item.FactorySampleTechnicalID))
                    {
                        dbItem.FactorySampleTechnical.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactorySampleTechnicals)
                {
                    FactorySampleTechnical dbFactorySampleTechnical;
                    if (item.FactorySampleTechnicalID <= 0)
                    {
                        dbFactorySampleTechnical = new FactorySampleTechnical();
                        dbItem.FactorySampleTechnical.Add(dbFactorySampleTechnical);
                    }
                    else
                    {
                        dbFactorySampleTechnical = dbItem.FactorySampleTechnical.FirstOrDefault(o => o.FactorySampleTechnicalID == item.FactorySampleTechnicalID);
                    }
                    if (dbFactorySampleTechnical != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactorySampleTechnical, FactorySampleTechnical>(item, dbFactorySampleTechnical);
                    }
                }
            }

            // FactoryResponsiblePerson
            if (dtoItem.FactoryResponsiblePersons != null)
            {
                foreach (var item in dbItem.FactoryResponsiblePerson.ToArray())
                {
                    if (!dtoItem.FactoryResponsiblePersons.Select(o => o.FactoryResponsiblePersonID).Contains(item.FactoryResponsiblePersonID))
                    {
                        dbItem.FactoryResponsiblePerson.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryResponsiblePersons)
                {
                    FactoryResponsiblePerson dbFactoryResponsiblePerson;
                    if (item.FactoryResponsiblePersonID <= 0)
                    {
                        dbFactoryResponsiblePerson = new FactoryResponsiblePerson();
                        dbItem.FactoryResponsiblePerson.Add(dbFactoryResponsiblePerson);
                    }
                    else
                    {
                        dbFactoryResponsiblePerson = dbItem.FactoryResponsiblePerson.FirstOrDefault(o => o.FactoryResponsiblePersonID == item.FactoryResponsiblePersonID);
                    }
                    if (dbFactoryResponsiblePerson != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryResponsiblePerson, FactoryResponsiblePerson>(item, dbFactoryResponsiblePerson);
                    }
                }
            }

            // FactoryRawMaterialSupplier
            if (dtoItem.FactoryRawMaterialSuppliers != null)
            {
                foreach (var item in dbItem.FactoryRawMaterialSupplier.ToArray())
                {
                    if (!dtoItem.FactoryRawMaterialSuppliers.Select(o => o.FactoryRawMaterialSupplierID).Contains(item.FactoryRawMaterialSupplierID))
                    {
                        dbItem.FactoryRawMaterialSupplier.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryRawMaterialSuppliers)
                {
                    FactoryRawMaterialSupplier dbFactoryRawMaterialSupplier;
                    if (item.FactoryRawMaterialSupplierID <= 0)
                    {
                        dbFactoryRawMaterialSupplier = new FactoryRawMaterialSupplier();
                        dbItem.FactoryRawMaterialSupplier.Add(dbFactoryRawMaterialSupplier);
                    }
                    else
                    {
                        dbFactoryRawMaterialSupplier = dbItem.FactoryRawMaterialSupplier.FirstOrDefault(o => o.FactoryRawMaterialSupplierID == item.FactoryRawMaterialSupplierID);
                    }
                    if (dbFactoryRawMaterialSupplier != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryRawMaterialSupplier, FactoryRawMaterialSupplier>(item, dbFactoryRawMaterialSupplier);
                    }
                }
            }

            // FactoryInHouseTest
            if (dtoItem.FactoryInHouseTests != null)
            {
                foreach (var item in dbItem.FactoryInHouseTest.ToArray())
                {
                    if (!dtoItem.FactoryInHouseTests.Select(o => o.FactoryInHouseTestID).Contains(item.FactoryInHouseTestID))
                    {
                        dbItem.FactoryInHouseTest.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryInHouseTests)
                {
                    FactoryInHouseTest dbFactoryInHouseTest;
                    if (item.FactoryInHouseTestID <= 0)
                    {
                        dbFactoryInHouseTest = new FactoryInHouseTest();
                        dbItem.FactoryInHouseTest.Add(dbFactoryInHouseTest);
                    }
                    else
                    {
                        dbFactoryInHouseTest = dbItem.FactoryInHouseTest.FirstOrDefault(o => o.FactoryInHouseTestID == item.FactoryInHouseTestID);
                    }
                    if (dbFactoryInHouseTest != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryInHouseTest, FactoryInHouseTest>(item, dbFactoryInHouseTest);
                    }
                }
            }

            // FactoryCertificate
            if (dtoItem.FactoryCertificates != null)
            {
                foreach (var item in dbItem.FactoryCertificate.ToArray())
                {
                    if (!dtoItem.FactoryCertificates.Select(o => o.FactoryCertificateID).Contains(item.FactoryCertificateID))
                    {
                        dbItem.FactoryCertificate.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryCertificates)
                {
                    FactoryCertificate dbFactoryCertificate;
                    if (item.FactoryCertificateID <= 0)
                    {
                        dbFactoryCertificate = new FactoryCertificate();
                        dbItem.FactoryCertificate.Add(dbFactoryCertificate);
                    }
                    else
                    {
                        dbFactoryCertificate = dbItem.FactoryCertificate.FirstOrDefault(o => o.FactoryCertificateID == item.FactoryCertificateID);
                    }
                    if (dbFactoryCertificate != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryCertificate, FactoryCertificate>(item, dbFactoryCertificate);
                        dbFactoryCertificate.ValidUntil = item.ValidUntil.ConvertStringToDateTime(); //convert datetime to string
                    }
                }
            }

            // Factory Turnover
            if (dtoItem.FactoryTurnovers != null)
            {
                //map child row
                foreach (var item in dtoItem.FactoryTurnovers)
                {
                    FactoryTurnover dbFactoryTurnover;
                    if (item.FactoryTurnoverID <= 0)
                    {
                        dbFactoryTurnover = new FactoryTurnover();
                        dbItem.FactoryTurnover.Add(dbFactoryTurnover);
                    }
                    else
                    {
                        dbFactoryTurnover = dbItem.FactoryTurnover.FirstOrDefault(o => o.FactoryTurnoverID == item.FactoryTurnoverID);
                    }
                    if (dbFactoryTurnover != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryTurnover, FactoryTurnover>(item, dbFactoryTurnover);
                    }
                }
            }

            // Factory Expected Capacity
            if (dtoItem.FactoryExpectedCapacities != null)
            {
                //map child row
                foreach (var item in dtoItem.FactoryExpectedCapacities)
                {
                    FactoryExpectedCapacity dbFactoryExpectedCapacity;
                    if (item.FactoryExpectedCapacityID <= 0)
                    {
                        dbFactoryExpectedCapacity = new FactoryExpectedCapacity();
                        dbItem.FactoryExpectedCapacity.Add(dbFactoryExpectedCapacity);
                    }
                    else
                    {
                        dbFactoryExpectedCapacity = dbItem.FactoryExpectedCapacity.FirstOrDefault(o => o.FactoryExpectedCapacityID == item.FactoryExpectedCapacityID);
                    }
                    if (dbFactoryExpectedCapacity != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryExpectedCapacity, FactoryExpectedCapacity>(item, dbFactoryExpectedCapacity);
                    }
                }
            }

            // Factory Capacity By Weeks
            if (dtoItem.factoryCapacityByWeeks != null)
            {
                //map child row
                foreach (var item in dtoItem.factoryCapacityByWeeks)
                {
                    FactoryCapacity dbFactoryCapacity;
                    if (item.FactoryCapacityID <= 0 && item.Capacity != null)
                    {
                        dbFactoryCapacity = new FactoryCapacity();
                        dbItem.FactoryCapacity.Add(dbFactoryCapacity);
                    }
                    else
                    {
                        dbFactoryCapacity = dbItem.FactoryCapacity.FirstOrDefault(o => o.FactoryCapacityID == item.FactoryCapacityID);
                    }
                    if (dbFactoryCapacity != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryCapacityByWeek, FactoryCapacity>(item, dbFactoryCapacity);
                    }
                }
            }

            // FactorySupplier
            //if (dtoItem.FactorySuppliers != null)
            //{
            //    foreach (var item in dbItem.FactorySupplier.ToArray())
            //    {
            //        if (!dtoItem.FactorySuppliers.Select(o => o.FactorySupplierID).Contains(item.FactorySupplierID))
            //        {
            //            dbItem.FactorySupplier.Remove(item);
            //        }
            //    }
            //    //map child row
            //    foreach (var item in dtoItem.FactorySuppliers)
            //    {
            //        FactorySupplier dbFactorySupplier;
            //        if (item.FactorySupplierID <= 0)
            //        {
            //            dbFactorySupplier = new FactorySupplier();
            //            dbItem.FactorySupplier.Add(dbFactorySupplier);
            //        }
            //        else
            //        {
            //            dbFactorySupplier = dbItem.FactorySupplier.FirstOrDefault(o => o.FactorySupplierID == item.FactorySupplierID);
            //        }
            //        if (dbFactorySupplier != null)
            //        {
            //            AutoMapper.Mapper.Map<DTO.FactorySupplier, FactorySupplier>(item, dbFactorySupplier);
            //        }
            //    }
            //}

            // FactoryGallery for add video 360
            if (dtoItem.FactoryGalleries != null)
            {
                foreach (FactoryGallery dbFactoryGallery in dbItem.FactoryGallery.ToList())
                {
                    if (!dtoItem.FactoryGalleries.Select(s => s.FactoryGalleryID).Contains(dbFactoryGallery.FactoryGalleryID))
                    {
                        dbItem.FactoryGallery.Remove(dbFactoryGallery);
                    }
                }

                foreach (DTO.FactoryGalleryDTO dtoFactoryGallery in dtoItem.FactoryGalleries.ToList())
                {
                    FactoryGallery dbFactoryGallery;

                    if (dtoFactoryGallery.FactoryGalleryID <= 0)
                    {
                        dbFactoryGallery = new FactoryGallery();
                        dbItem.FactoryGallery.Add(dbFactoryGallery);
                    }
                    else
                    {
                        dbFactoryGallery = dbItem.FactoryGallery.FirstOrDefault(o => o.FactoryGalleryID == dtoFactoryGallery.FactoryGalleryID);
                    }

                    if (dbFactoryGallery != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryGalleryDTO, FactoryGallery>(dtoFactoryGallery, dbFactoryGallery);
                    }
                }
            }

            // Factory Document
            if(dtoItem.factoryDocuments != null)
            {
                foreach (FactoryDocument dbFactoryDocument in dbItem.FactoryDocument.ToList())
                {
                    if (!dtoItem.factoryDocuments.Select(s => s.FactoryDocmentID).Contains(dbFactoryDocument.FactoryDocmentID))
                    {
                        dbItem.FactoryDocument.Remove(dbFactoryDocument);
                    }
                }

                foreach (DTO.FactoryDocumentDTO dtoFactoryDocument in dtoItem.factoryDocuments.ToList())
                {
                    FactoryDocument dbFactoryDocument;
                    if(dtoFactoryDocument.FactoryDocmentID <= 0)
                    {
                        dbFactoryDocument = new FactoryDocument();
                        dbItem.FactoryDocument.Add(dbFactoryDocument);
                    }
                    else
                    {
                        dbFactoryDocument = dbItem.FactoryDocument.FirstOrDefault(o => o.FactoryDocmentID == dtoFactoryDocument.FactoryDocmentID);
                    }

                    if(dbFactoryDocument != null)
                    {
                        if (dtoFactoryDocument.FactoryDocumentHasChange)
                        {
                            dbFactoryDocument.FactoryDocumentFile = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoFactoryDocument.FactoryDocumentNewFile, dtoFactoryDocument.FactoryDocumentFile, dtoFactoryDocument.FriendlyName);
                        }
                        AutoMapper.Mapper.Map<DTO.FactoryDocumentDTO, FactoryDocument>(dtoFactoryDocument, dbFactoryDocument);
                    }
                }
            }


            // FactoryDirector
            FactoryContactQuickInfo dbFactoryContactQuickInfo;
            if (dtoItem.FactoryContactQuickInfos != null)
            {
                foreach (var item in dbItem.FactoryContactQuickInfo.ToArray())
                {
                    if (!dtoItem.FactoryContactQuickInfos.Select(o => o.FactoryContactQuickInfoID).Contains(item.FactoryContactQuickInfoID))
                    {
                        dbItem.FactoryContactQuickInfo.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryContactQuickInfos)
                {                    
                    if (item.FactoryContactQuickInfoID <= 0)
                    {
                        dbFactoryContactQuickInfo = new FactoryContactQuickInfo();
                        dbItem.FactoryContactQuickInfo.Add(dbFactoryContactQuickInfo);
                    }
                    else
                    {
                        dbFactoryContactQuickInfo = dbItem.FactoryContactQuickInfo.FirstOrDefault(o => o.FactoryContactQuickInfoID == item.FactoryContactQuickInfoID);
                    }
                    if (dbFactoryContactQuickInfo != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryContactQuickInfo, FactoryContactQuickInfo>(item, dbFactoryContactQuickInfo);
                    }
                }
            }

            // FactoryProductGroup
            if (dtoItem.FactoryProductGroupDTOs != null)
            {
                foreach (var item in dbItem.FactoryProductGroup.ToArray())
                {
                    if (!dtoItem.FactoryProductGroupDTOs.Select(o => o.FactoryProductGroupID).Contains(item.FactoryProductGroupID))
                    {
                        dbItem.FactoryProductGroup.Remove(item);
                    }
                }
                //map child row
                foreach (var item in dtoItem.FactoryProductGroupDTOs)
                {
                    FactoryProductGroup dbFactoryProductGroup;
                    if (item.FactoryProductGroupID <= 0)
                    {
                        dbFactoryProductGroup = new FactoryProductGroup();
                        dbItem.FactoryProductGroup.Add(dbFactoryProductGroup);
                    }
                    else
                    {
                        dbFactoryProductGroup = dbItem.FactoryProductGroup.FirstOrDefault(o => o.FactoryProductGroupID == item.FactoryProductGroupID);
                    }
                    if (dbFactoryProductGroup != null)
                    {
                        AutoMapper.Mapper.Map<DTO.FactoryProductGroupDTO, FactoryProductGroup>(item, dbFactoryProductGroup);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.Factory, Factory>(dtoItem, dbItem);
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }

        // Employee
        //public List<DTO.FactoryRawMaterialSupplier> DB2DTO_FactoryRawMaterialSupplier(List<FactoryMng2_List_FactoryRawMaterialSupplier> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<FactoryMng2_List_FactoryRawMaterialSupplier>, List<DTO.FactoryRawMaterialSupplier>>(dbItems);
        //}


        public List<DTO.FactoryOrderTurnover> DB2DTO_FactoryOrderTurnover(List<FactoryMng2_FactoryOrderTurnover_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<FactoryMng2_FactoryOrderTurnover_View>, List<DTO.FactoryOrderTurnover>>(dbItem);
        }
        public List<DTO.PersonInChargeDTO> DB2DTO_PersonInCharge(List<Factory2Mng_PersonInCharge_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<Factory2Mng_PersonInCharge_View>, List<DTO.PersonInChargeDTO>>(dbItem);
        }
        public List<DTO.UsersDTO> DB2DTO_User2(List<SupportMng_User2_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_User2_View>, List<DTO.UsersDTO>>(dbitems);
        }

    }
}
