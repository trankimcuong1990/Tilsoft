using AutoMapper;
using DALBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.ModelMng
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");

        public DataConverter()
        {
            string mapName = "ModelMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DTO.ModelMng.Model, Model>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModelID, o => o.Ignore())
                    .ForMember(d => d.ModelPiece, o => o.Ignore())
                    .ForMember(d => d.ModelSparepart, o => o.Ignore())
                    .ForMember(d => d.ModelCushionOption, o => o.Ignore())
                    .ForMember(d => d.ModelSubMaterialOption, o => o.Ignore())
                    .ForMember(d => d.ModelPackagingMethodOption, o => o.Ignore())
                    .ForMember(d => d.ImageGallery, o => o.Ignore())
                    .ForMember(d => d.TestReport, o => o.Ignore())
                    .ForMember(d => d.TDFile, o => o.Ignore())
                    .ForMember(d => d.AIFile, o => o.Ignore())
                    .ForMember(d => d.ModelFixPriceConfiguration, o => o.Ignore())
                    .ForMember(d => d.ModelPriceConfiguration, o => o.Ignore())
                    .ForMember(d => d.ModelDefaultOption, o => o.Ignore())
                    // ignore below attributes for all user, map manually for the user with create rights
                    .ForMember(d => d.ModelNM, o => o.Ignore())
                    .ForMember(d => d.ProductTypeID, o => o.Ignore())
                    .ForMember(d => d.ManufacturerCountryID, o => o.Ignore())
                    .ForMember(d => d.RangeName, o => o.Ignore())
                    .ForMember(d => d.Collection, o => o.Ignore())
                    .ForMember(d => d.ModelStatusID, o => o.Ignore())
                    .ForMember(d => d.Season, o => o.Ignore())
                    .ForMember(d => d.FactoryID, o => o.Ignore())
                    .ForMember(d => d.ProductGroupID, o => o.Ignore())
                    .ForMember(d => d.ProductCategoryID, o => o.Ignore())
                    .ForMember(d => d.CataloguePageNo, o => o.Ignore())
                    .ForMember(d => d.DefaultFactoryID, o => o.Ignore())

                    .ForMember(d => d.ModelIssueTrack, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ModelNM, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore());



                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ImageGallery, ImageGallery>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.SampleImportDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    //.ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.ImageGalleryID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ImageGalleryVersion, ImageGalleryVersion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ImageGalleryVersionID, o => o.Ignore())
                    .ForMember(d => d.ImageGalleryID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.Piece, ModelPiece>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModelPieceID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.Sparepart, ModelSparepart>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PartUD, o => o.Ignore())
                    .ForMember(d => d.ModelSparepartID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.CushionOption, ModelCushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.SubMaterialOption, ModelSubMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.PackagingMethodOption, ModelPackagingMethodOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.TestReport, TestReport>()
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.TestReportID, o => o.Ignore())
                    .ForMember(d => d.ModelID, o => o.Ignore());

                //td file
                AutoMapper.Mapper.CreateMap<DTO.ModelMng.tdFile, TDFile>()
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.TDFileID, o => o.Ignore())
                    .ForMember(d => d.ModelID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ModelMng_TestReport_View, DTO.ModelMng.TestReport>()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ScanFileLocation) ? s.ScanFileLocation : "no-image.jpg")));
                //TD File
                AutoMapper.Mapper.CreateMap<ModelMng_TDFile_View, DTO.ModelMng.tdFile>()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ScanFileLocation) ? s.ScanFileLocation : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.AIFile, AIFile>()
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.AIFileID, o => o.Ignore())
                    .ForMember(d => d.ModelID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ModelMng_AIFile_View, DTO.ModelMng.AIFile>()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.ScanFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ScanFileLocation) ? s.ScanFileLocation : "no-image.jpg")));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelPiece_View, DTO.ModelMng.Piece>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelSparepart_View, DTO.ModelMng.Sparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelCushionOption_View, DTO.ModelMng.CushionOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelSubMaterialOption_View, DTO.ModelMng.SubMaterialOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelPackagingMethodOption_View, DTO.ModelMng.PackagingMethodOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<ModelMng_SampleProduct_View, DTO.ModelMng.SampleProductView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_Product2_view, DTO.ModelMng.productsView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ProductBreakDown_View, DTO.ModelMng.ProductBreakDown>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_Model_View, DTO.ModelMng.Model>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Pieces, o => o.MapFrom(s => s.ModelMng_ModelPiece_View))
                    .ForMember(d => d.Spareparts, o => o.MapFrom(s => s.ModelMng_ModelSparepart_View))
                    .ForMember(d => d.CushionOptions, o => o.MapFrom(s => s.ModelMng_ModelCushionOption_View))
                    .ForMember(d => d.SubMaterialOptions, o => o.MapFrom(s => s.ModelMng_ModelSubMaterialOption_View))
                    .ForMember(d => d.PackagingMethodOptions, o => o.MapFrom(s => s.ModelMng_ModelPackagingMethodOption_View))
                    .ForMember(d => d.ModelCheckListGroupDTO, o => o.MapFrom(s => s.ModelCheckListGroupMng_ModelCheckListGroup_View))
                    .ForMember(d => d.ImageGalleries, o => o.MapFrom(s => s.ModelMng_ImageGallery_View))
                    .ForMember(d => d.TestReports, o => o.MapFrom(s => s.ModelMng_TestReport_View))
                    .ForMember(d => d.Tdfiles, o => o.MapFrom(s => s.ModelMng_TDFile_View))
                    .ForMember(d => d.AIFiles, o => o.MapFrom(s => s.ModelMng_AIFile_View))
                    .ForMember(d => d.ModelIssueTracks, o => o.MapFrom(s => s.ModelMng_ModelIssueTrack_View))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ModelFixPriceConfigurations, o => o.MapFrom(s => s.ModelMng_ModelFixPriceConfiguration_View))
                    .ForMember(d => d.ModelPriceConfigurations, o => o.MapFrom(s => s.ModelMng_ModelPriceConfiguration_View))
                    .ForMember(d => d.ModelPurchasingFixPriceConfigurationDTOs, o => o.MapFrom(s => s.ModelMng_ModelPurchasingFixPriceConfiguration_View))
                    .ForMember(d => d.ModelPurchasingPriceConfigurationDTOs, o => o.MapFrom(s => s.ModelMng_ModelPurchasingPriceConfiguration_View))
                    .ForMember(d => d.ModelPriceStatusDTOs, o => o.MapFrom(s => s.ModelMng_ModelPriceStatus_View))
                    .ForMember(d => d.ModelPurchasingPriceStatusDTOs, o => o.MapFrom(s => s.ModelMng_ModelPurchasingPriceStatus_View))
                    .ForMember(d => d.MaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.MaterialThumbnailLocation))
                    .ForMember(d => d.FrameMaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.FrameMaterialThumbnailLocation))
                    .ForMember(d => d.SubMaterialColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SubMaterialColorThumbnailLocation))
                    .ForMember(d => d.CushionColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.CushionColorThumbnailLocation))
                    .ForMember(d => d.BackCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.BackCushionThumbnailLocation))
                    .ForMember(d => d.SeatCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SeatCushionThumbnailLocation))
                    .ForMember(d => d.ModelDefaultFactories, o => o.MapFrom(s => s.ModelMng_ModelDefaultFactory_View))
                    .ForMember(d => d.ModelDefaultOptionDTOs, o => o.MapFrom(s => s.ModelDefaultOptionMng_ModelDefaultOption_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelSearchResult_View, DTO.ModelMng.ModelSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ImageFile_FullSizeUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ImageFile_FullSizeUrl) ? s.ImageFile_FullSizeUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ImageGallery_View, DTO.ModelMng.ImageGallery>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SampleImportDate, o => o.ResolveUsing(s => s.SampleImportDate.HasValue ? s.SampleImportDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : s.GalleryItemTypeID == 3 ? "avi.png" : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.ImageGalleryVersions, o => o.MapFrom(s => s.ModelMng_ImageGalleryVersion_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ImageGalleryVersion_View, DTO.ModelMng.ImageGalleryVersion>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelIssueTrack, ModelIssueTrack>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelIssueTrack, ModelIssueTrack>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModelIssueTrackID, o => o.Ignore())
                    .ForMember(d => d.ModelIssueID, o => o.Ignore())
                    .ForMember(d => d.ModelID, o => o.Ignore());
                //.ForMember(d => d.ModelIssueTrackImageError, o => o.Ignore())
                //.ForMember(d => d.ModelIssueTrackImageFinish, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelIssueTrackImageError, ModelIssueTrackImageError>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModelIssueTrackImageErrorID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelIssueTrackImageFinish, ModelIssueTrackImageFinish>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModelIssueTrackImageFinishID, o => o.Ignore());

                //AutoMapper.Mapper.CreateMap<ModelMng_ModelIssue_View, DTO.ModelMng.ModelIssue>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelIssueTrack_View, DTO.ModelMng.ModelIssueTrack>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModelIssueTrackImagesError, o => o.MapFrom(s => s.ModelMng_ModelIssueTrackImageError_View))
                    .ForMember(d => d.ModelIssueTrackImagesFinish, o => o.MapFrom(s => s.ModelMng_ModelIssueTrackImageFinish_View))
                    .ForMember(d => d.IssueDateFormatted, o => o.ResolveUsing(s => s.IssueDate.HasValue ? s.IssueDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelIssueTrackImageError_View, DTO.ModelMng.ModelIssueTrackImageError>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelIssueTrackImageFinish_View, DTO.ModelMng.ModelIssueTrackImageFinish>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                // cuong.tran

                //
                // author: themy
                // description: model price
                //
                AutoMapper.Mapper.CreateMap<ModelMng_ProductElementOption_View, DTO.ModelMng.ProductElementOption>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ModelMng_ModelDefaultFactory_View, DTO.ModelMng.ModelDefaultFactory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<DTO.ModelMng.ModelDefaultFactory, ModelDefaultFactory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelFixPriceConfiguration_View, DTO.ModelMng.ModelFixPriceConfiguration>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelPriceConfiguration_View, DTO.ModelMng.ModelPriceConfiguration>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty))
                     .ForMember(d => d.ModelPriceConfigurationDetails, o => o.MapFrom(s => s.ModelMng_ModelPriceConfigurationDetail_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelPriceConfigurationDetail_View, DTO.ModelMng.ModelPriceConfigurationDetail>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelPriceConfigurationDefault_View, DTO.ModelMng.ModelPriceConfiguration>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ModelPriceConfigurationDetails, o => o.MapFrom(s => s.ModelMng_ModelPriceConfigurationDefaultDetail_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelPriceConfigurationDefaultDetail_View, DTO.ModelMng.ModelPriceConfigurationDetail>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelFixPriceConfiguration, ModelFixPriceConfiguration>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ModelFixPriceConfigurationID, o => o.Ignore())
                     .ForMember(d => d.ModelID, o => o.Ignore())
                     .ForMember(d => d.UpdatedBy, o => o.Ignore())
                     .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelPriceConfiguration, ModelPriceConfiguration>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ModelPriceConfigurationID, o => o.Ignore())
                     .ForMember(d => d.ModelID, o => o.Ignore())
                     .ForMember(d => d.UpdatedBy, o => o.Ignore())
                     .ForMember(d => d.UpdatedDate, o => o.Ignore())
                     .ForMember(d => d.ModelPriceConfigurationDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelPriceConfigurationDetail, ModelPriceConfigurationDetail>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ModelPriceConfigurationDetailID, o => o.Ignore())
                     .ForMember(d => d.ModelPriceConfigurationID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ModelMng_ModelDefaultFactory_View, DTO.ModelMng.ModelDefaultFactory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
              
                AutoMapper.Mapper.CreateMap<ModelMng_ModelDefaultFactoryDetail_View, DTO.ModelMng.ModelDefaultFactoryDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_Factory_View, DTO.ModelMng.FactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ClientSpecialPackagingMethod_View, DTO.ModelMng.ClientSpecialPackagingMethod>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ClientSaleData_View, DTO.ModelMng.ClientSaleData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelNoDefaultFactory_View, DTO.ModelMng.Model>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelCheckListGroupMng_ModelCheckListGroup_View, DTO.ModelMng.ModelCheckListGroupDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelCheckListGroupDTO, ModelCheckListGroupMng_ModelCheckListGroup_View>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CheckListMng_CheckListGroup_View, DTO.ModelMng.CheckListGroupDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelCheckListGroupDTO, ModelCheckListGroup>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                #region Purchasing Price  
                AutoMapper.Mapper.CreateMap<ModelMng_ModelPriceStatus_View, DTO.ModelMng.ModelPriceStatusDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ModelMng_ModelPurchasingPriceStatus_View, DTO.ModelMng.ModelPurchasingPriceStatusDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelPriceStatusDTO, ModelPriceStatus>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelPurchasingPriceStatusDTO, ModelPurchasingPriceStatus>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ModelMng_ModelPurchasingFixPriceConfiguration_View, DTO.ModelMng.ModelPurchasingFixPriceConfigurationDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ModelMng_ModelPurchasingPriceConfiguration_View, DTO.ModelMng.ModelPurchasingPriceConfigurationDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty))
                    .ForMember(d => d.ModelPurchasingPriceConfigurationDetailDTOs, o => o.MapFrom(s => s.ModelMng_ModelPurchasingPriceConfigurationDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<ModelMng_ModelPurchasingPriceConfigurationDetail_View, DTO.ModelMng.ModelPurchasingPriceConfigurationDetailDTO>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelPurchasingFixPriceConfigurationDTO, ModelPurchasingFixPriceConfiguration>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ModelPurchasingFixPriceConfigurationID, o => o.Ignore())
                  .ForMember(d => d.UpdatedBy, o => o.Ignore())
                  .ForMember(d => d.UpdatedDate, o => o.Ignore())
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelPurchasingPriceConfigurationDTO, ModelPurchasingPriceConfiguration>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ModelPurchasingPriceConfigurationID, o => o.Ignore())
                  .ForMember(d => d.UpdatedBy, o => o.Ignore())
                  .ForMember(d => d.UpdatedDate, o => o.Ignore())
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<DTO.ModelMng.ModelPurchasingPriceConfigurationDetailDTO, ModelPurchasingPriceConfigurationDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ModelPurchasingPriceConfigurationDetailID, o => o.Ignore())
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<DTO.ModelMng.ModelDefaultOptionDTO, ModelDefaultOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModelDefaultOptionID, o => o.Ignore());

                Mapper.CreateMap<ModelDefaultOptionMng_ModelDefaultOption_View, DTO.ModelMng.ModelDefaultOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.MaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.MaterialThumbnailLocation))
                    .ForMember(d => d.FrameMaterialThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.FrameMaterialThumbnailLocation))
                    .ForMember(d => d.SubMaterialColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SubMaterialColorThumbnailLocation))
                    .ForMember(d => d.CushionColorThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.CushionColorThumbnailLocation))
                    .ForMember(d => d.BackCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.BackCushionThumbnailLocation))
                    .ForMember(d => d.SeatCushionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + s.SeatCushionThumbnailLocation))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                #endregion

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ModelMng.ModelCheckListGroupDTO> DB2DTO_ModelCheckListGroupDTOs(List<ModelCheckListGroupMng_ModelCheckListGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ModelCheckListGroupMng_ModelCheckListGroup_View>, List<DTO.ModelMng.ModelCheckListGroupDTO>>(dbItems);
        }

        public List<DTO.ModelMng.CheckListGroupDTO> DB2DTO_ModelCheckListGroup(List<CheckListMng_CheckListGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CheckListMng_CheckListGroup_View>, List<DTO.ModelMng.CheckListGroupDTO>>(dbItems);
        }

        public List<DTO.ModelMng.ModelSearchResult> DB2DTO_ModelSearchResultList(List<ModelMng_ModelSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_ModelSearchResult_View>, List<DTO.ModelMng.ModelSearchResult>>(dbItems);
        }

        public List<DTO.ModelMng.ModelDefaultOptionDTO> DB2DTO_ModelDefaultOptionList(List<ModelDefaultOptionMng_ModelDefaultOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ModelDefaultOptionMng_ModelDefaultOption_View>, List<DTO.ModelMng.ModelDefaultOptionDTO>>(dbItems);
        }
        public DTO.ModelMng.Model DB2DTO_Model(ModelMng_Model_View dbItem)
        {
            DTO.ModelMng.Model dtoItem = AutoMapper.Mapper.Map<ModelMng_Model_View, DTO.ModelMng.Model>(dbItem);
          
            dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dtoItem.ConcurrencyFlag);

            return dtoItem;
        }

        public List<DTO.ModelMng.ProductElementOption> DB2DTO_ProductElementOptionList(List<ModelMng_ProductElementOption_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_ProductElementOption_View>, List<DTO.ModelMng.ProductElementOption>>(dbItems);
        }

        public List<DTO.ModelMng.ModelPriceConfiguration> DB2DTO_ModelPriceConfigurationList(List<ModelMng_ModelPriceConfigurationDefault_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_ModelPriceConfigurationDefault_View>, List<DTO.ModelMng.ModelPriceConfiguration>>(dbItems);
        }

        public void DTO2DB(int userId, DTO.ModelMng.Model dtoItem, DataFactory factory, ref Model dbItem)
        {
            // map parents
            AutoMapper.Mapper.Map(dtoItem, dbItem);
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.UpdatedDate = DateTime.Now;           

            //
            // author: The My
            // description: update model name only if current user have approve permission
            if (dtoItem.TotalFactoryOrderItem <= 0) // allow changing description if item is not in any factory order yet
            {
                dbItem.ModelNM = dtoItem.ModelNM;
            }
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (fwBll.CanPerformAction(dtoItem.UpdatedBy.Value, "ModelMng", Library.DTO.ModuleAction.CanApprove))
            {
                dbItem.ModelNM = dtoItem.ModelNM;
            }

            if (fwBll.CanPerformAction(dtoItem.UpdatedBy.Value, "ModelMng", Library.DTO.ModuleAction.CanReset))
            {
                dbItem.ModelNM = dtoItem.ModelNM;
            }
            //dbItem.CreatedDate = DateTime.Now;

            if (dtoItem.ModelID == 0)
            {
                dbItem.CreatedBy = dtoItem.CreatedBy;
                dbItem.CreatedDate = DateTime.Now;
            }

            // map gallery images
            List<ImageGallery> itemNeedDelete_Image = new List<ImageGallery>();
            List<ImageGalleryVersion> itemNeedDelete_imageGalleryVersions;
            if (dtoItem.ImageGalleries != null)
            {
                // check for child rows deleted
                foreach (ImageGallery dbImage in dbItem.ImageGallery)
                {

                    if (!dtoItem.ImageGalleries.Select(o => o.ImageGalleryID).Contains(dbImage.ImageGalleryID))
                    {
                        itemNeedDelete_Image.Add(dbImage);
                    }
                    else
                    {
                        itemNeedDelete_imageGalleryVersions = new List<ImageGalleryVersion>();
                        foreach (ImageGalleryVersion dbVersion in dbImage.ImageGalleryVersion)
                        {
                            if (!dtoItem.ImageGalleries.Where(o => o.ImageGalleryID == dbImage.ImageGalleryID).FirstOrDefault().ImageGalleryVersions.Select(x => x.ImageGalleryVersionID).Contains(dbVersion.ImageGalleryVersionID))
                            {
                                itemNeedDelete_imageGalleryVersions.Add(dbVersion);
                            }
                        }
                        foreach (ImageGalleryVersion dbVersion in itemNeedDelete_imageGalleryVersions)
                        {
                            dbImage.ImageGalleryVersion.Remove(dbVersion);
                        }
                        // dbItem.ImageGallery.Remove(dbImage);
                    }
                }
                foreach (ImageGallery dbImage in itemNeedDelete_Image)
                {
                    List<ImageGalleryVersion> item_deleteds = new List<ImageGalleryVersion>();
                    foreach (ImageGalleryVersion dbVersion in dbImage.ImageGalleryVersion)
                    {
                        item_deleteds.Add(dbVersion);
                    }
                    foreach (ImageGalleryVersion item in item_deleteds)
                    {
                        dbItem.ImageGallery.Where(o => o.ImageGalleryID == dbImage.ImageGalleryID).FirstOrDefault().ImageGalleryVersion.Remove(item);
                    }
                    dbItem.ImageGallery.Remove(dbImage);
                }

                //MAP
                ImageGallery _dbImage;
                ImageGalleryVersion _dbVersion;

                foreach (DTO.ModelMng.ImageGallery dtoImage in dtoItem.ImageGalleries)
                {
                    if (dtoImage.ImageGalleryID <= 0)
                    {
                        _dbImage = new ImageGallery();
                        _dbVersion = new ImageGalleryVersion();
                        _dbImage.ImageGalleryVersion.Add(_dbVersion);
                        _dbVersion.UpdatedBy = dtoItem.UpdatedBy;
                        _dbVersion.UpdatedDate = DateTime.Now;
                        _dbVersion.FileUD = dtoImage.FileUD;
                        _dbVersion.Version = 1;
                        _dbVersion.IsDefault = true;

                        dbItem.ImageGallery.Add(_dbImage);
                        _dbImage.UpdatedBy = dtoItem.UpdatedBy;
                        _dbImage.UpdatedDate = DateTime.Now;
                        //_dbImage.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        _dbImage = dbItem.ImageGallery.FirstOrDefault(o => o.ImageGalleryID == dtoImage.ImageGalleryID);
                        if (_dbImage != null && dtoImage.ImageGalleryVersions != null)
                        {
                            foreach (DTO.ModelMng.ImageGalleryVersion dtoVersion in dtoImage.ImageGalleryVersions)
                            {
                                if (dtoVersion.ImageGalleryVersionID < 0)
                                {
                                    _dbVersion = new ImageGalleryVersion();
                                    _dbImage.ImageGalleryVersion.Add(_dbVersion);
                                }
                                else
                                {
                                    _dbVersion = _dbImage.ImageGalleryVersion.FirstOrDefault(o => o.ImageGalleryVersionID == dtoVersion.ImageGalleryVersionID);
                                }
                                _dbVersion.UpdatedBy = dtoItem.UpdatedBy;
                                _dbVersion.UpdatedDate = DateTime.Now;
                                if (_dbVersion != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ModelMng.ImageGalleryVersion, ImageGalleryVersion>(dtoVersion, _dbVersion);
                                }
                            }
                        }
                    }
                    if (_dbImage != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ModelMng.ImageGallery, ImageGallery>(dtoImage, _dbImage);
                    }
                }
            }

            // map pieces
            if (dtoItem.Pieces != null)
            {
                // check for child rows deleted
                foreach (ModelPiece dbPiece in dbItem.ModelPiece.ToArray())
                {
                    if (!dtoItem.Pieces.Select(o => o.ModelPieceID).Contains(dbPiece.ModelPieceID))
                    {
                        dbItem.ModelPiece.Remove(dbPiece);
                    }
                }

                // map child rows
                foreach (DTO.ModelMng.Piece dtoPiece in dtoItem.Pieces)
                {
                    ModelPiece dbPiece;
                    if (dtoPiece.ModelPieceID <= 0)
                    {
                        dbPiece = new ModelPiece();
                        dbItem.ModelPiece.Add(dbPiece);
                    }
                    else
                    {
                        dbPiece = dbItem.ModelPiece.FirstOrDefault(o => o.ModelPieceID == dtoPiece.ModelPieceID);
                    }

                    if (dbPiece != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ModelMng.Piece, ModelPiece>(dtoPiece, dbPiece);
                    }
                }
            }

            // Map ModelDefaultfactory
            if (dtoItem.ModelDefaultFactories != null)
            {
                // check for child rows deleted
                foreach (ModelDefaultFactory dbModelDefaultFactory in dbItem.ModelDefaultFactory.ToArray())
                {
                    if (!dtoItem.ModelDefaultFactories.Select(o => o.ModelDefaultFactoryID).Contains(dbModelDefaultFactory.ModelDefaultFactoryID))
                    {
                        dbItem.ModelDefaultFactory.Remove(dbModelDefaultFactory);
                    }
                }

                // map child rows
                foreach (DTO.ModelMng.ModelDefaultFactory dtoModelDefaultFactory in dtoItem.ModelDefaultFactories)
                {
                    ModelDefaultFactory dbModelDefaultFactory;
                    if (dtoModelDefaultFactory.ModelDefaultFactoryID <= 0)
                    {
                        dbModelDefaultFactory = new ModelDefaultFactory();
                        dbItem.ModelDefaultFactory.Add(dbModelDefaultFactory);
                    }
                    else
                    {
                        dbModelDefaultFactory = dbItem.ModelDefaultFactory.FirstOrDefault(o => o.ModelDefaultFactoryID == dtoModelDefaultFactory.ModelDefaultFactoryID);
                    }

                    if (dbModelDefaultFactory != null)
                    {
                        Mapper.Map(dtoModelDefaultFactory, dbModelDefaultFactory);
                    }
                }
            }

            // Map ModelDefaultOption
            if (dtoItem.ModelDefaultOptionDTOs != null)
            {

                // check for child rows deleted
                foreach (ModelDefaultOption dbModelDefaultOption in dbItem.ModelDefaultOption.Where(db => db.Season.Equals(dtoItem.SeasonDefaultOption)).ToArray())
                {
                    if (!dtoItem.ModelDefaultOptionDTOs.Select(o => o.ModelDefaultOptionID).Contains(dbModelDefaultOption.ModelDefaultOptionID))
                    {
                        dbItem.ModelDefaultOption.Remove(dbModelDefaultOption);
                    }
                }

                foreach (DTO.ModelMng.ModelDefaultOptionDTO dtoModelDefaultOption in dtoItem.ModelDefaultOptionDTOs)
                {
                    ModelDefaultOption dbModelDefaultOption;
                    if (dtoModelDefaultOption.ModelDefaultOptionID <= 0)
                    {
                        dbModelDefaultOption = new ModelDefaultOption();
                        dbItem.ModelDefaultOption.Add(dbModelDefaultOption);
                    }
                    else
                    {
                        dbModelDefaultOption = dbItem.ModelDefaultOption.FirstOrDefault(o => o.ModelDefaultOptionID == dtoModelDefaultOption.ModelDefaultOptionID);
                    }

                    if (dbModelDefaultOption != null)
                    {
                        Mapper.Map(dtoModelDefaultOption, dbModelDefaultOption);
                        dbModelDefaultOption.UpdatedBy = userId;
                        dbModelDefaultOption.UpdatedDate = DateTime.Now;
                    }
                }
            }

            // map sparepart
            if (dtoItem.Spareparts != null)
            {
                // check for child rows deleted
                foreach (ModelSparepart dbSparepart in dbItem.ModelSparepart.ToArray())
                {
                    if (!dtoItem.Spareparts.Select(o => o.ModelSparepartID).Contains(dbSparepart.ModelSparepartID))
                    {
                        dbItem.ModelSparepart.Remove(dbSparepart);
                    }
                }

                // map child rows
                foreach (DTO.ModelMng.Sparepart dtoSparepart in dtoItem.Spareparts)
                {
                    ModelSparepart dbSparepart;
                    if (dtoSparepart.ModelSparepartID <= 0)
                    {
                        dbSparepart = new ModelSparepart();
                        dbItem.ModelSparepart.Add(dbSparepart);

                        // generate code
                        string uniqueCode = "01";
                        while (dbItem.ModelSparepart.Count(o => o.PartUD == uniqueCode) > 0)
                        {
                            uniqueCode = Library.Common.Helper.getNextCode(uniqueCode);
                        }
                        if (!string.IsNullOrEmpty(uniqueCode))
                        {
                            dbSparepart.PartUD = uniqueCode;
                        }
                    }
                    else
                    {
                        dbSparepart = dbItem.ModelSparepart.FirstOrDefault(o => o.ModelSparepartID == dtoSparepart.ModelSparepartID);
                    }

                    if (dbSparepart != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ModelMng.Sparepart, ModelSparepart>(dtoSparepart, dbSparepart);
                    }
                }
            }

            // Action work only with current user with approve permission
            if (fwBll.CanPerformAction(userId, "ModelMng", Library.DTO.ModuleAction.CanApprove))
            {
                // map packaging method
                if (dtoItem.PackagingMethodOptions != null)
                {
                    // check for child rows deleted
                    foreach (ModelPackagingMethodOption dbPackagingMethod in dbItem.ModelPackagingMethodOption.ToArray())
                    {
                        if (!dtoItem.PackagingMethodOptions.Select(o => o.ModelPackagingMethodOptionID).Contains(dbPackagingMethod.ModelPackagingMethodOptionID))
                        {
                            if (factory.IsOKToDeletePackagingMethod(dbPackagingMethod.PackagingMethodID.Value, dtoItem.ModelID))
                            {
                                dbItem.ModelPackagingMethodOption.Remove(dbPackagingMethod);
                            }
                            else
                            {
                                throw new Exception("Can not delete packaging method option which is already exist in product information");
                            }
                        }
                    }

                    // map child rows
                    int c = dtoItem.PackagingMethodOptions.Where(o => o.IsDefault.Equals(true)).Count();
                    foreach (DTO.ModelMng.PackagingMethodOption dtoPackagingMethod in dtoItem.PackagingMethodOptions)
                    {
                        if (c > 1)
                        {
                            throw new Exception("Each model only have one default packaging method");
                        }

                        ModelPackagingMethodOption dbPackagingMethod;
                        if (dtoPackagingMethod.ModelPackagingMethodOptionID <= 0)
                        {
                            dbPackagingMethod = new ModelPackagingMethodOption();
                            dbItem.ModelPackagingMethodOption.Add(dbPackagingMethod);
                        }
                        else
                        {
                            dbPackagingMethod = dbItem.ModelPackagingMethodOption.FirstOrDefault(o => o.ModelPackagingMethodOptionID == dtoPackagingMethod.ModelPackagingMethodOptionID);
                        }

                        if (dbPackagingMethod != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.PackagingMethodOption, ModelPackagingMethodOption>(dtoPackagingMethod, dbPackagingMethod);
                            dbPackagingMethod.ClientSpecialPackagingMethodID = dtoPackagingMethod.ClientSpecialPackagingMethodID;
                        }
                    }
                }
            }

            // map cushion option
            if (dtoItem.CushionOptions != null)
            {
                // check for child rows deleted
                foreach (ModelCushionOption dbCushionOption in dbItem.ModelCushionOption.ToArray())
                {
                    if (!dtoItem.CushionOptions.Select(o => o.ModelCushionOptionID).Contains(dbCushionOption.ModelCushionOptionID))
                    {
                        if (factory.IsOKToDeleteCushion(dbCushionOption.CushionID.Value, dtoItem.ModelID))
                        {
                            dbItem.ModelCushionOption.Remove(dbCushionOption);
                        }
                        else
                        {
                            throw new Exception("Can not delete cushion specification which is already exist in product information");
                        }
                    }
                }

                // map child rows
                foreach (DTO.ModelMng.CushionOption dtoCushionOption in dtoItem.CushionOptions)
                {
                    ModelCushionOption dbCushionOption;
                    if (dtoCushionOption.ModelCushionOptionID <= 0)
                    {
                        dbCushionOption = new ModelCushionOption();
                        dbItem.ModelCushionOption.Add(dbCushionOption);
                    }
                    else
                    {
                        dbCushionOption = dbItem.ModelCushionOption.FirstOrDefault(o => o.ModelCushionOptionID == dtoCushionOption.ModelCushionOptionID);
                    }

                    if (dbCushionOption != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ModelMng.CushionOption, ModelCushionOption>(dtoCushionOption, dbCushionOption);
                    }
                }
            }

            // map sub material option
            if (dtoItem.SubMaterialOptions != null)
            {
                // check for child rows deleted
                foreach (ModelSubMaterialOption dbSubMaterialOption in dbItem.ModelSubMaterialOption.ToArray())
                {
                    if (!dtoItem.SubMaterialOptions.Select(o => o.ModelSubMaterialOptionID).Contains(dbSubMaterialOption.ModelSubMaterialOptionID))
                    {
                        if (factory.IsOKToDeleteSubMaterial(dbSubMaterialOption.SubMaterialOptionID.Value, dtoItem.ModelID))
                        {
                            dbItem.ModelSubMaterialOption.Remove(dbSubMaterialOption);
                        }
                        else
                        {
                            throw new Exception("Can not delete sub material which is already exist in product information");
                        }
                    }
                }

                // map child rows
                foreach (DTO.ModelMng.SubMaterialOption dtoSubMaterialOption in dtoItem.SubMaterialOptions)
                {
                    ModelSubMaterialOption dbSubMaterialOption;
                    if (dtoSubMaterialOption.ModelSubMaterialOptionID <= 0)
                    {
                        dbSubMaterialOption = new ModelSubMaterialOption();
                        dbItem.ModelSubMaterialOption.Add(dbSubMaterialOption);
                    }
                    else
                    {
                        dbSubMaterialOption = dbItem.ModelSubMaterialOption.FirstOrDefault(o => o.ModelSubMaterialOptionID == dtoSubMaterialOption.ModelSubMaterialOptionID);
                    }

                    if (dbSubMaterialOption != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ModelMng.SubMaterialOption, ModelSubMaterialOption>(dtoSubMaterialOption, dbSubMaterialOption);
                    }
                }

                //map test report
                if (dtoItem.TestReports != null)
                {
                    // check for child rows deleted
                    foreach (TestReport dbTest in dbItem.TestReport.ToArray())
                    {
                        if (!dtoItem.TestReports.Select(o => o.TestReportID).Contains(dbTest.TestReportID))
                        {
                            dbItem.TestReport.Remove(dbTest);
                        }
                    }

                    // map child rows
                    foreach (DTO.ModelMng.TestReport dtoTest in dtoItem.TestReports)
                    {
                        TestReport dbTest;
                        if (dtoTest.TestReportID <= 0)
                        {
                            dbTest = new TestReport();
                            dbTest.UpdatedBy = dtoItem.UpdatedBy;
                            dbItem.TestReport.Add(dbTest);
                        }
                        else
                        {
                            dbTest = dbItem.TestReport.FirstOrDefault(o => o.TestReportID == dtoTest.TestReportID);
                            if (dtoTest.ScanHasChange)
                            {
                                dbTest.UpdatedBy = dtoItem.UpdatedBy;
                            }
                        }
                        dbTest.UpdatedDate = DateTime.Now;
                        if (dbTest != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.TestReport, TestReport>(dtoTest, dbTest);
                        }
                    }
                }



                if (dtoItem.ModelIssueTracks != null)
                {
                    foreach (ModelIssueTrack dbIssueTrack in dbItem.ModelIssueTrack.ToArray())
                    {
                        if (!dtoItem.ModelIssueTracks.Select(o => o.ModelIssueTrackID).Contains(dbIssueTrack.ModelIssueTrackID))
                        {
                            dbItem.ModelIssueTrack.Remove(dbIssueTrack);
                        }
                    }

                    foreach (DTO.ModelMng.ModelIssueTrack dtoIssueTrack in dtoItem.ModelIssueTracks)
                    {
                        ModelIssueTrack dbIssueTrack;

                        if (dtoIssueTrack.ModelIssueTrackID <= 0)
                        {
                            dbIssueTrack = new ModelIssueTrack()
                            {
                                //FollowUp = dtoItem.UpdatedBy,
                                QualityReport = dtoIssueTrack.QualityReport,
                                Status = dtoIssueTrack.StatusBy,
                                OtherContent = dtoIssueTrack.OtherContent
                            };

                            dbItem.ModelIssueTrack.Add(dbIssueTrack);
                        }
                        else
                        {
                            dbIssueTrack = dbItem.ModelIssueTrack.FirstOrDefault(o => o.ModelIssueTrackID == dtoIssueTrack.ModelIssueTrackID);
                        }

                        if (!string.IsNullOrEmpty(dtoIssueTrack.IssueDateFormatted))
                        {
                            if (DateTime.TryParse(dtoIssueTrack.IssueDateFormatted, nl, System.Globalization.DateTimeStyles.None, out DateTime tmpDate))
                            {
                                dbIssueTrack.IssueDate = tmpDate;
                            }
                        }

                        if (dbIssueTrack != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.ModelIssueTrack, ModelIssueTrack>(dtoIssueTrack, dbIssueTrack);
                        }

                        if (dtoIssueTrack.QualityReport_HasChange)
                        {
                            dbIssueTrack.FollowUp = dtoItem.UpdatedBy;
                        }
                    }
                }

                //if (dtoItem.ModelIssueID == 0 && dtoItem.ModelIssue != null)
                //{
                //    dtoItem.ModelIssues.Add(dtoItem.ModelIssue);
                //}


                //mapping model issue
                //if (dtoItem.ModelIssues != null)
                //{
                //    //check for child rows deleted
                //    foreach(ModelIssue dbTest in dbItem.ModelIssue.ToArray())
                //    {
                //        if (!dtoItem.ModelIssues.Select(o => o.ModelIssueID).Contains(dbTest.ModelIssueID))
                //            dbItem.ModelIssue.Remove(dbTest);
                //    }
                //    //mapping child rows
                //    foreach(DTO.ModelMng.ModelIssue dtoTest in dtoItem.ModelIssues)
                //    {
                //        ModelIssue dbTest;
                //        if (dtoTest.ModelIssueID <= 0)
                //        {
                //            dbTest = new ModelIssue();
                //            dbItem.ModelIssue.Add(dbTest);
                //        }
                //        else
                //        {
                //            dbTest = dbItem.ModelIssue.FirstOrDefault(o => o.ModelIssueID == dtoTest.ModelIssueID);
                //        }
                //        //auto mapping dto to db
                //        if (dbTest != null)
                //            AutoMapper.Mapper.Map<DTO.ModelMng.ModelIssue, ModelIssue>(dtoTest, dbTest);
                //    }
                //}

                //
                // author: themy
                // description: model price
                //

                // fix price
                if (dtoItem.ModelFixPriceConfigurations != null)
                {
                    //check for child rows deleted
                    foreach (ModelFixPriceConfiguration dbFixPrice in dbItem.ModelFixPriceConfiguration.ToArray())
                    {
                        if (!dtoItem.ModelFixPriceConfigurations.Select(o => o.ModelFixPriceConfigurationID).Contains(dbFixPrice.ModelFixPriceConfigurationID))
                        {
                            dbItem.ModelFixPriceConfiguration.Remove(dbFixPrice);
                        }
                    }

                    //mapping child rows
                    foreach (DTO.ModelMng.ModelFixPriceConfiguration dtoFixPrice in dtoItem.ModelFixPriceConfigurations)
                    {
                        ModelFixPriceConfiguration dbFixPrice;
                        if (dtoFixPrice.ModelFixPriceConfigurationID <= 0)
                        {
                            dbFixPrice = new ModelFixPriceConfiguration();
                            dbFixPrice.UpdatedBy = dtoItem.UpdatedBy.Value;
                            dbFixPrice.UpdatedDate = DateTime.Now;
                            dbItem.ModelFixPriceConfiguration.Add(dbFixPrice);
                        }
                        else
                        {
                            dbFixPrice = dbItem.ModelFixPriceConfiguration.FirstOrDefault(o => o.ModelFixPriceConfigurationID == dtoFixPrice.ModelFixPriceConfigurationID);
                        }
                        if (dbFixPrice != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.ModelFixPriceConfiguration, ModelFixPriceConfiguration>(dtoFixPrice, dbFixPrice);
                        }
                    }
                }

                // fix Purchasing price
                if (dtoItem.ModelPurchasingFixPriceConfigurationDTOs != null)
                {
                    //check for child rows deleted
                    foreach (ModelPurchasingFixPriceConfiguration dbPurchasingFixPrice in dbItem.ModelPurchasingFixPriceConfiguration.ToArray())
                    {
                        if (!dtoItem.ModelPurchasingFixPriceConfigurationDTOs.Select(o => o.ModelPurchasingFixPriceConfigurationID).Contains(dbPurchasingFixPrice.ModelPurchasingFixPriceConfigurationID))
                        {
                            dbItem.ModelPurchasingFixPriceConfiguration.Remove(dbPurchasingFixPrice);
                        }
                    }

                    //mapping child rows
                    foreach (DTO.ModelMng.ModelPurchasingFixPriceConfigurationDTO dtoPurchasingFixPrice in dtoItem.ModelPurchasingFixPriceConfigurationDTOs)
                    {
                        ModelPurchasingFixPriceConfiguration dbPurchasingFixPrice;
                        if (dtoPurchasingFixPrice.ModelPurchasingFixPriceConfigurationID <= 0)
                        {
                            dbPurchasingFixPrice = new ModelPurchasingFixPriceConfiguration();
                            dbPurchasingFixPrice.UpdatedBy = dtoItem.UpdatedBy.Value;
                            dbPurchasingFixPrice.UpdatedDate = DateTime.Now;
                            dbItem.ModelPurchasingFixPriceConfiguration.Add(dbPurchasingFixPrice);
                        }
                        else
                        {
                            dbPurchasingFixPrice = dbItem.ModelPurchasingFixPriceConfiguration.FirstOrDefault(o => o.ModelPurchasingFixPriceConfigurationID == dtoPurchasingFixPrice.ModelPurchasingFixPriceConfigurationID);
                        }
                        if (dbPurchasingFixPrice != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.ModelPurchasingFixPriceConfigurationDTO, ModelPurchasingFixPriceConfiguration>(dtoPurchasingFixPrice, dbPurchasingFixPrice);
                        }
                    }
                }

                // fix price status
                if (dtoItem.ModelPriceStatusDTOs != null)
                {
                    //check for child rows deleted
                    foreach (ModelPriceStatus dbPriceStatus in dbItem.ModelPriceStatus.ToArray())
                    {
                        if (!dtoItem.ModelPriceStatusDTOs.Select(o => o.ModelPriceStatusID).Contains(dbPriceStatus.ModelPriceStatusID))
                        {
                            dbItem.ModelPriceStatus.Remove(dbPriceStatus);
                        }
                    }

                    //mapping child rows
                    foreach (DTO.ModelMng.ModelPriceStatusDTO dtoPriceStatus in dtoItem.ModelPriceStatusDTOs)
                    {
                        ModelPriceStatus dbPriceStatus;
                        if (dtoPriceStatus.ModelPriceStatusID <= 0)
                        {
                            dbPriceStatus = new ModelPriceStatus();
                            dbPriceStatus.ConfirmedBy = dtoItem.UpdatedBy.Value;
                            dbPriceStatus.ConfirmedDate = DateTime.Now;
                            dbItem.ModelPriceStatus.Add(dbPriceStatus);
                        }
                        else
                        {
                            dbPriceStatus = dbItem.ModelPriceStatus.FirstOrDefault(o => o.ModelPriceStatusID == dtoPriceStatus.ModelPriceStatusID);
                        }
                        if (dbPriceStatus != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.ModelPriceStatusDTO, ModelPriceStatus>(dtoPriceStatus, dbPriceStatus);
                        }
                    }
                }

                // fix PurchasingPrice status
                if (dtoItem.ModelPurchasingPriceStatusDTOs != null)
                {
                    //check for child rows deleted
                    foreach (ModelPurchasingPriceStatus dbPurchasingPriceStatus in dbItem.ModelPurchasingPriceStatus.ToArray())
                    {
                        if (!dtoItem.ModelPurchasingPriceStatusDTOs.Select(o => o.ModelPurchasingPriceStatusID).Contains(dbPurchasingPriceStatus.ModelPurchasingPriceStatusID))
                        {
                            dbItem.ModelPurchasingPriceStatus.Remove(dbPurchasingPriceStatus);
                        }
                    }

                    //mapping child rows
                    foreach (DTO.ModelMng.ModelPurchasingPriceStatusDTO dtoPurchasingPriceStatus in dtoItem.ModelPurchasingPriceStatusDTOs)
                    {
                        ModelPurchasingPriceStatus dbPurchasingPriceStatus;
                        if (dtoPurchasingPriceStatus.ModelPurchasingPriceStatusID <= 0)
                        {
                            dbPurchasingPriceStatus = new ModelPurchasingPriceStatus();
                            dbPurchasingPriceStatus.ConfirmedBy = dtoItem.UpdatedBy.Value;
                            dbPurchasingPriceStatus.ConfirmedDate = DateTime.Now;
                            dbItem.ModelPurchasingPriceStatus.Add(dbPurchasingPriceStatus);
                        }
                        else
                        {
                            dbPurchasingPriceStatus = dbItem.ModelPurchasingPriceStatus.FirstOrDefault(o => o.ModelPurchasingPriceStatusID == dtoPurchasingPriceStatus.ModelPurchasingPriceStatusID);
                        }
                        if (dbPurchasingPriceStatus != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.ModelPurchasingPriceStatusDTO, ModelPurchasingPriceStatus>(dtoPurchasingPriceStatus, dbPurchasingPriceStatus);
                        }
                    }
                }
                // optional
                if (dtoItem.ModelPriceConfigurations != null)
                {
                    //check for child rows deleted
                    foreach (ModelPriceConfiguration dbPrice in dbItem.ModelPriceConfiguration.ToArray())
                    {
                        if (!dtoItem.ModelPriceConfigurations.Select(o => o.ModelPriceConfigurationID).Contains(dbPrice.ModelPriceConfigurationID))
                        {
                            dbItem.ModelPriceConfiguration.Remove(dbPrice);
                        }
                    }

                    //mapping child rows
                    foreach (DTO.ModelMng.ModelPriceConfiguration dtoPrice in dtoItem.ModelPriceConfigurations)
                    {
                        ModelPriceConfiguration dbPrice;
                        if (dtoPrice.ModelPriceConfigurationID <= 0)
                        {
                            dbPrice = new ModelPriceConfiguration();
                            dbPrice.UpdatedBy = dtoItem.UpdatedBy.Value;
                            dbPrice.UpdatedDate = DateTime.Now;
                            dbItem.ModelPriceConfiguration.Add(dbPrice);
                        }
                        else
                        {
                            dbPrice = dbItem.ModelPriceConfiguration.FirstOrDefault(o => o.ModelPriceConfigurationID == dtoPrice.ModelPriceConfigurationID);
                        }
                        if (dbPrice != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.ModelPriceConfiguration, ModelPriceConfiguration>(dtoPrice, dbPrice);

                            // optional detail
                            if (dtoPrice.ModelPriceConfigurationDetails != null)
                            {
                                //check for child rows deleted
                                foreach (ModelPriceConfigurationDetail dbPriceDetail in dbPrice.ModelPriceConfigurationDetail.ToArray())
                                {
                                    if (!dtoPrice.ModelPriceConfigurationDetails.Select(o => o.ModelPriceConfigurationDetailID).Contains(dbPriceDetail.ModelPriceConfigurationDetailID))
                                    {
                                        dbPrice.ModelPriceConfigurationDetail.Remove(dbPriceDetail);
                                    }
                                }

                                //mapping child rows
                                foreach (DTO.ModelMng.ModelPriceConfigurationDetail dtoPriceDetail in dtoPrice.ModelPriceConfigurationDetails)
                                {
                                    ModelPriceConfigurationDetail dbPriceDetail;
                                    if (dtoPriceDetail.ModelPriceConfigurationDetailID <= 0)
                                    {
                                        dbPriceDetail = new ModelPriceConfigurationDetail();
                                        dbPrice.ModelPriceConfigurationDetail.Add(dbPriceDetail);
                                    }
                                    else
                                    {
                                        dbPriceDetail = dbPrice.ModelPriceConfigurationDetail.FirstOrDefault(o => o.ModelPriceConfigurationDetailID == dtoPriceDetail.ModelPriceConfigurationDetailID);
                                    }
                                    if (dbPriceDetail != null)
                                    {
                                        AutoMapper.Mapper.Map<DTO.ModelMng.ModelPriceConfigurationDetail, ModelPriceConfigurationDetail>(dtoPriceDetail, dbPriceDetail);
                                    }
                                }
                            }
                        }
                    }
                }

                // optional Purchasing
                if (dtoItem.ModelPurchasingPriceConfigurationDTOs != null)
                {
                    //check for child rows deleted
                    foreach (ModelPurchasingPriceConfiguration dbPrice in dbItem.ModelPurchasingPriceConfiguration.ToArray())
                    {
                        if (!dtoItem.ModelPurchasingPriceConfigurationDTOs.Select(o => o.ModelPurchasingPriceConfigurationID).Contains(dbPrice.ModelPurchasingPriceConfigurationID))
                        {
                            dbItem.ModelPurchasingPriceConfiguration.Remove(dbPrice);
                        }
                    }

                    //mapping child rows
                    foreach (DTO.ModelMng.ModelPurchasingPriceConfigurationDTO dtoPrice in dtoItem.ModelPurchasingPriceConfigurationDTOs)
                    {
                        ModelPurchasingPriceConfiguration dbPrice;
                        if (dtoPrice.ModelPurchasingPriceConfigurationID <= 0)
                        {
                            dbPrice = new ModelPurchasingPriceConfiguration();
                            dbPrice.UpdatedBy = dtoItem.UpdatedBy.Value;
                            dbPrice.UpdatedDate = DateTime.Now;
                            dbItem.ModelPurchasingPriceConfiguration.Add(dbPrice);
                        }
                        else
                        {
                            dbPrice = dbItem.ModelPurchasingPriceConfiguration.FirstOrDefault(o => o.ModelPurchasingPriceConfigurationID == dtoPrice.ModelPurchasingPriceConfigurationID);
                        }
                        if (dbPrice != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.ModelPurchasingPriceConfigurationDTO, ModelPurchasingPriceConfiguration>(dtoPrice, dbPrice);

                            // optional detail
                            if (dtoPrice.ModelPurchasingPriceConfigurationDetailDTOs != null)
                            {
                                //check for child rows deleted
                                foreach (ModelPurchasingPriceConfigurationDetail dbPriceDetail in dbPrice.ModelPurchasingPriceConfigurationDetail.ToArray())
                                {
                                    if (!dtoPrice.ModelPurchasingPriceConfigurationDetailDTOs.Select(o => o.ModelPurchasingPriceConfigurationDetailID).Contains(dbPriceDetail.ModelPurchasingPriceConfigurationDetailID))
                                    {
                                        dbPrice.ModelPurchasingPriceConfigurationDetail.Remove(dbPriceDetail);
                                    }
                                }

                                //mapping child rows
                                foreach (DTO.ModelMng.ModelPurchasingPriceConfigurationDetailDTO dtoPriceDetail in dtoPrice.ModelPurchasingPriceConfigurationDetailDTOs)
                                {
                                    ModelPurchasingPriceConfigurationDetail dbPriceDetail;
                                    if (dtoPriceDetail.ModelPurchasingPriceConfigurationDetailID <= 0)
                                    {
                                        dbPriceDetail = new ModelPurchasingPriceConfigurationDetail();
                                        dbPrice.ModelPurchasingPriceConfigurationDetail.Add(dbPriceDetail);
                                    }
                                    else
                                    {
                                        dbPriceDetail = dbPrice.ModelPurchasingPriceConfigurationDetail.FirstOrDefault(o => o.ModelPurchasingPriceConfigurationDetailID == dtoPriceDetail.ModelPurchasingPriceConfigurationDetailID);
                                    }
                                    if (dbPriceDetail != null)
                                    {
                                        AutoMapper.Mapper.Map<DTO.ModelMng.ModelPurchasingPriceConfigurationDetailDTO, ModelPurchasingPriceConfigurationDetail>(dtoPriceDetail, dbPriceDetail);
                                    }
                                }
                            }
                        }
                    }
                }
                
                //
                // end model price
                //

                //map TD file
                if (dtoItem.Tdfiles != null)
                {
                    foreach (TDFile dbtdfile in dbItem.TDFile.ToArray())
                    {
                        if (!dtoItem.Tdfiles.Select(s => s.TDFileID).Contains(dbtdfile.TDFileID))
                        {
                            dbItem.TDFile.Remove(dbtdfile);
                        }
                    }

                    foreach (DTO.ModelMng.tdFile dtotdfile in dtoItem.Tdfiles)
                    {
                        TDFile dbtd;
                        if (dtotdfile.TDFileID <= 0)
                        {
                            dbtd = new TDFile();
                            dbtd.UpdatedBy = dtoItem.UpdatedBy;


                            dbItem.TDFile.Add(dbtd);
                        }
                        else
                        {
                            dbtd = dbItem.TDFile.FirstOrDefault(o => o.TDFileID == dtotdfile.TDFileID);
                            if (dtotdfile.ScanHasChange)
                            {
                                dbtd.UpdatedBy = dtoItem.UpdatedBy;
                            }
                        }
                        dbtd.UpdatedDate = DateTime.Now;
                        if (dbtd != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.tdFile, TDFile>(dtotdfile, dbtd);
                        }
                    }
                }

                //map AI file
                if (dtoItem.AIFiles != null)
                {
                    // check for child rows deleted
                    foreach (AIFile dbAI in dbItem.AIFile.ToArray())
                    {
                        if (!dtoItem.AIFiles.Select(o => o.AIFileID).Contains(dbAI.AIFileID))
                        {
                            dbItem.AIFile.Remove(dbAI);
                        }
                    }

                    // map child rows
                    foreach (DTO.ModelMng.AIFile dtoAI in dtoItem.AIFiles)
                    {
                        AIFile dbAI;
                        if (dtoAI.AIFileID <= 0)
                        {
                            dbAI = new AIFile();
                            dbAI.UpdatedBy = dtoItem.UpdatedBy;
                            dbAI.UpdatedDate = DateTime.Now;
                            dbItem.AIFile.Add(dbAI);
                        }
                        else
                        {
                            dbAI = dbItem.AIFile.FirstOrDefault(o => o.AIFileID == dtoAI.AIFileID);
                            if (dtoAI.ScanHasChange)
                            {
                                dbAI.UpdatedBy = dtoItem.UpdatedBy;
                                dbAI.UpdatedDate = DateTime.Now;
                            }
                        }
                        if (dbAI != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.AIFile, AIFile>(dtoAI, dbAI);
                        }
                    }
                }
                //map AI file
                if (dtoItem.ModelCheckListGroupDTO != null)
                {
                    // check for child rows deleted
                    foreach (ModelCheckListGroup dbmodelcheck in dbItem.ModelCheckListGroup.ToArray())
                    {
                        if (!dtoItem.ModelCheckListGroupDTO.Select(o => o.ModelCheckListGroupID).Contains(dbmodelcheck.ModelCheckListGroupID))
                        {
                            dbItem.ModelCheckListGroup.Remove(dbmodelcheck);
                        }
                    }

                    // map child rows
                    foreach (DTO.ModelMng.ModelCheckListGroupDTO dtomcl in dtoItem.ModelCheckListGroupDTO)
                    {
                        ModelCheckListGroup dbModelCheckListGroup;
                        if (dtomcl.ModelCheckListGroupID <= 0)
                        {
                            dbModelCheckListGroup = new ModelCheckListGroup();

                            dbItem.ModelCheckListGroup.Add(dbModelCheckListGroup);
                        }
                        else
                        {
                            dbModelCheckListGroup = dbItem.ModelCheckListGroup.FirstOrDefault(o => o.ModelCheckListGroupID == dtomcl.ModelCheckListGroupID);

                        }
                        if (dbModelCheckListGroup != null)
                        {
                            AutoMapper.Mapper.Map<DTO.ModelMng.ModelCheckListGroupDTO, ModelCheckListGroup>(dtomcl, dbModelCheckListGroup);
                        }
                    }
                }
            }
        }

        // cuong.tran
        public List<DTO.ModelMng.ModelIssueTrack> DB2DTO_ModelIssueTracks(List<ModelMng_ModelIssueTrack_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_ModelIssueTrack_View>, List<DTO.ModelMng.ModelIssueTrack>>(dbItems);
        }

        public DTO.ModelMng.ModelIssueTrack DB2DTO_ModelIssueTrack(ModelMng_ModelIssueTrack_View dbItem)
        {
            return AutoMapper.Mapper.Map<ModelMng_ModelIssueTrack_View, DTO.ModelMng.ModelIssueTrack>(dbItem);
        }

        public void DTO2DB_ModelIssueTrack(DTO.ModelMng.ModelIssueTrack dto, ref ModelIssueTrack db, string tempFolder)
        {
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

            if (dto.ModelIssueTrackImagesError != null)
            {
                foreach (var item in db.ModelIssueTrackImageError.ToArray())
                {
                    if (!dto.ModelIssueTrackImagesError.Select(s => s.ModelIssueTrackImageErrorID).Contains(item.ModelIssueTrackImageErrorID))
                        db.ModelIssueTrackImageError.Remove(item);
                }

                foreach (var item in dto.ModelIssueTrackImagesError)
                {
                    if (item.ImageFile_HasChange.HasValue && item.ImageFile_HasChange.Value)
                        item.ImageFile = fwFactory.CreateFilePointer(tempFolder, item.ImageFile_NewFile, item.ImageFile);

                    ModelIssueTrackImageError dbDetail;

                    if (item.ModelIssueTrackImageErrorID <= 0)
                    {
                        dbDetail = new ModelIssueTrackImageError();
                        db.ModelIssueTrackImageError.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = db.ModelIssueTrackImageError.Where(o => o.ModelIssueTrackImageErrorID == item.ModelIssueTrackImageErrorID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                    {
                        dbDetail.ModelIssueTrackID = dto.ModelIssueTrackID;
                        AutoMapper.Mapper.Map<DTO.ModelMng.ModelIssueTrackImageError, ModelIssueTrackImageError>(item, dbDetail);
                    }
                }
            }

            if (dto.ModelIssueTrackImagesFinish != null)
            {
                foreach (var item in db.ModelIssueTrackImageFinish.ToArray())
                {
                    if (!dto.ModelIssueTrackImagesFinish.Select(s => s.ModelIssueTrackImageFinishID).Contains(item.ModelIssueTrackImageFinishID))
                        db.ModelIssueTrackImageFinish.Remove(item);
                }

                foreach (var item in dto.ModelIssueTrackImagesFinish)
                {
                    if (item.ImageFile_HasChange.HasValue && item.ImageFile_HasChange.Value)
                        item.ImageFile = fwFactory.CreateFilePointer(tempFolder, item.ImageFile_NewFile, item.ImageFile);

                    ModelIssueTrackImageFinish dbDetail;

                    if (item.ModelIssueTrackImageFinishID <= 0)
                    {
                        dbDetail = new ModelIssueTrackImageFinish();
                        db.ModelIssueTrackImageFinish.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = db.ModelIssueTrackImageFinish.Where(o => o.ModelIssueTrackImageFinishID == item.ModelIssueTrackImageFinishID).FirstOrDefault();
                    }

                    if (dbDetail != null)
                    {
                        dbDetail.ModelIssueTrackID = dto.ModelIssueTrackID;
                        AutoMapper.Mapper.Map<DTO.ModelMng.ModelIssueTrackImageFinish, ModelIssueTrackImageFinish>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.ModelMng.ModelIssueTrack, ModelIssueTrack>(dto, db);
        }

        public List<DTO.ModelMng.ModelDefaultFactoryDetail> DB2DTO_ModelDefaultFactoryDetail(List<ModelMng_ModelDefaultFactoryDetail_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_ModelDefaultFactoryDetail_View>, List<DTO.ModelMng.ModelDefaultFactoryDetail>>(dbItem);
        }

        public List<DTO.ModelMng.SampleProductView> DB2DTO_SampleProduct(List<ModelMng_SampleProduct_View> dbItem)
        {
            return Mapper.Map<List<ModelMng_SampleProduct_View>, List<DTO.ModelMng.SampleProductView>>(dbItem);
        }

        public List<DTO.ModelMng.productsView> DB2DTO_Product(List<ModelMng_Product2_view> dbItem)
        {
            return Mapper.Map<List<ModelMng_Product2_view>, List<DTO.ModelMng.productsView>>(dbItem);
        }

        public List<DTO.ModelMng.ProductBreakDown> BD2DTO_ProductBreakDown(List<ModelMng_ProductBreakDown_View> dbItem)
        {
            return Mapper.Map<List<ModelMng_ProductBreakDown_View>, List<DTO.ModelMng.ProductBreakDown>>(dbItem);
        }
        public List<DTO.ModelMng.FactoryDTO> DB2DTO_Factory(List<ModelMng_Factory_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_Factory_View>, List<DTO.ModelMng.FactoryDTO>>(dbItem);
        }

        public List<DTO.ModelMng.ClientSpecialPackagingMethod> DB2DTO_ClientSpecialPackingMethod(List<ModelMng_ClientSpecialPackagingMethod_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_ClientSpecialPackagingMethod_View>, List<DTO.ModelMng.ClientSpecialPackagingMethod>>(dbitems);
        }

        public List<DTO.ModelMng.ClientSaleData> DB2DTO_ClientSale(List<ModelMng_ClientSaleData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_ClientSaleData_View>, List<DTO.ModelMng.ClientSaleData>>(dbItems);
        }

        public List<DTO.ModelMng.Model> DB2DTO_ModelNoFactoryList(List<ModelMng_ModelNoDefaultFactory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ModelMng_ModelNoDefaultFactory_View>, List<DTO.ModelMng.Model>>(dbItems);
        }

    }
}
