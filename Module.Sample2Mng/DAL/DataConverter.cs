using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.Sample2Mng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate;        

        public DataConverter()
        {
            string mapName = "Sample2Mng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleOrderSearchResult_View, DTO.SampleOrderSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.HardDeadline, o => o.ResolveUsing(s => s.HardDeadline.HasValue ? s.HardDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.Deadline, o => o.ResolveUsing(s => s.Deadline.HasValue ? s.Deadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleOrder_View, DTO.SampleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.HardDeadline, o => o.ResolveUsing(s => s.HardDeadline.HasValue ? s.HardDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Deadline, o => o.ResolveUsing(s => s.Deadline.HasValue ? s.Deadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SampleProducts, o => o.MapFrom(s => s.Sample2Mng_SampleProduct_View))
                    .ForMember(d => d.SampleMonitors, o => o.MapFrom(s => s.Sample2Mng_SampleMonitor_View))
                    .ForMember(d => d.ConcurrencyFlag, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProduct_View, DTO.SampleProduct>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.SampleProductMinutes, o => o.MapFrom(s => s.Sample2Mng_SampleProductMinute_View))
                     .ForMember(d => d.SampleRemarks, o => o.MapFrom(s => s.Sample2Mng_SampleRemark_View))
                     .ForMember(d => d.SampleQARemarks, o => o.MapFrom(s => s.Sample2Mng_SampleQARemark_View))
                     .ForMember(d => d.SampleReferenceImages, o => o.MapFrom(s => s.Sample2Mng_SampleReferenceImage_View))
                     .ForMember(d => d.SampleProgresses, o => o.MapFrom(s => s.Sample2Mng_SampleProgress_View))
                     .ForMember(d => d.SampleTechnicalDrawings, o => o.MapFrom(s => s.Sample2Mng_SampleTechnicalDrawing_View))
                     .ForMember(d => d.SampleProductSubFactories, o => o.MapFrom(s => s.Sample2Mng_SampleProductSubFactory_View))
                     .ForMember(d => d.SampleProductLocationDTOs, o => o.MapFrom(s => s.Sample2Mng_SampleItemLocation_View))
                     .ForMember(d => d.samplePackagings, o => o.MapFrom(s => s.Sample2Mng_SamplePackaging_View))
                     .ForMember(d => d.SampleProductDimensionDTOs, o => o.MapFrom(s => s.Sample2Mng_SampleProductDimension_View))
                     .ForMember(d => d.SampleProductPackagingMaterialDTOs, o => o.MapFrom(s => s.Sample2Mng_SampleProductPackagingMaterial_View))
                     .ForMember(d => d.SampleProductCartonBoxDTOs, o => o.MapFrom(s => s.Sample2Mng_SampleProductCartonBox_View))
                     .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                     .ForMember(d => d.ModelThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ModelThumbnailLocation) ? s.ModelThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.ModelFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ModelFileLocation) ? s.ModelFileLocation : "no-image.jpg")))
                     .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                     .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                     .ForMember(d => d.ETADestination, o => o.ResolveUsing(s => s.ETADestination.HasValue ? s.ETADestination.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.ItemInfoUpdatedDate, o => o.ResolveUsing(s => s.ItemInfoUpdatedDate.HasValue ? s.ItemInfoUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                     .ForMember(d => d.FinishedImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FinishedImageThumbnailLocation) ? s.FinishedImageThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.FinishedImageFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FinishedImageFileLocation) ? s.FinishedImageFileLocation : "no-image.jpg")))
                     .ForMember(d => d.ProgressImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ProgressImageThumbnailLocation) ? s.ProgressImageThumbnailLocation : "no-image.jpg")))
                     .ForMember(d => d.ProgressImageFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProgressImageFileLocation) ? s.ProgressImageFileLocation : "no-image.jpg")))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SamplePackaging_View, DTO.SamplePackaging>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProductDimension_View, DTO.SampleProductDimensionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProductPackagingMaterial_View, DTO.SampleProductPackagingMaterialDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleproductPackagingMaterialType_View, DTO.SampleProductPackagingMaterialTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Sample2Mng_Unit_View, DTO.UnitDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleItemLocation_View, DTO.SampleProductLocationDTO>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.LocationDate, o => o.ResolveUsing(s => s.LocationDate.HasValue ? s.LocationDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleMonitor_View, DTO.SampleMonitor>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProductMinute_View, DTO.SampleProductMinute>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.SampleProductMinuteImages, o => o.MapFrom(s => s.Sample2Mng_SampleProductMinuteImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProductMinuteImage_View, DTO.SampleProductMinuteImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleRemark_View, DTO.SampleRemark>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.SampleRemarkImages, o => o.MapFrom(s => s.Sample2Mng_SampleRemarkImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleRemarkImage_View, DTO.SampleRemarkImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleQARemark_View, DTO.SampleQARemark>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.SampleQARemarkImages, o => o.MapFrom(s => s.Sample2Mng_SampleQARemarkImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleQARemarkImage_View, DTO.SampleQARemarkImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleReferenceImage_View, DTO.SampleReferenceImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BringInDate, o => o.ResolveUsing(s => s.BringInDate.HasValue ? s.BringInDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProgress_View, DTO.SampleProgress>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.SampleProgressImages, o => o.MapFrom(s => s.Sample2Mng_SampleProgressImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProgressImage_View, DTO.SampleProgressImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleTechnicalDrawing_View, DTO.SampleTechnicalDrawing>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.SampleTechnicalDrawingFiles, o => o.MapFrom(s => s.Sample2Mng_SampleTechnicalDrawingFile_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleTechnicalDrawingFile_View, DTO.SampleTechnicalDrawingFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.PreviewFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.PreviewFileLocation) ? s.PreviewFileLocation : "no-image.jpg")))
                    .ForMember(d => d.SourceFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.SourceFileLocation) ? s.SourceFileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProductSubFactory_View, DTO.SampleProductSubFactory>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_PackagingMethod_View, DTO.PackagingMethodDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_AccountManager_View, DTO.AccountManagerDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SampleOrder, SampleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleOrderID, o => o.Ignore())
                    .ForMember(d => d.SampleOrderUD, o => o.Ignore())
                    .ForMember(d => d.Deadline, o => o.Ignore())
                    .ForMember(d => d.HardDeadline, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.SampleProduct, o => o.Ignore())
                    .ForMember(d => d.SampleMonitor, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleMonitor, SampleMonitor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleMonitorID, o => o.Ignore())
                    .ForMember(d => d.SampleOrderID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleProduct, SampleProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductID, o => o.Ignore())
                    .ForMember(d => d.SampleOrderID, o => o.Ignore())
                    .ForMember(d => d.SampleProductMinute, o => o.Ignore())
                    .ForMember(d => d.SampleRemark, o => o.Ignore())
                    .ForMember(d => d.SampleReferenceImage, o => o.Ignore())
                    .ForMember(d => d.SampleTechnicalDrawing, o => o.Ignore())
                    .ForMember(d => d.SampleProgress, o => o.Ignore())
                    .ForMember(d => d.SampleProductCartonBox, o => o.Ignore())
                    .ForMember(d => d.SampleProductDimension, o => o.Ignore())
                    .ForMember(d => d.SampleProductStatusID, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ETADestination, o => o.Ignore())
                    .ForMember(d => d.ItemInfoUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.IsEurofarSampleCollection, o => o.Ignore())
                    .ForMember(d => d.FactoryDeadline, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleProductMinute, SampleProductMinute>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductID, o => o.Ignore())
                    .ForMember(d => d.SampleProductMinuteID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.SampleProductMinuteImage, o => o.Ignore());                    

                AutoMapper.Mapper.CreateMap<DTO.SampleProductMinuteImage, SampleProductMinuteImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductMinuteImageID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.SampleProductMinuteID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleRemark, SampleRemark>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleRemarkID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.SampleRemarkImage, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleRemarkImage, SampleRemarkImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleRemarkImageID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.SampleRemarkID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleQARemark, SampleQARemark>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleQARemarkID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.SampleQARemarkImage, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleQARemarkImage, SampleQARemarkImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleQARemarkImageID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleReferenceImage, SampleReferenceImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductID, o => o.Ignore())
                    .ForMember(d => d.SampleReferenceImageID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.BringInDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleProgress, SampleProgress>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProgressID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.SampleProgressImage, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleProgressImage, SampleProgressImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProgressImageID, o => o.Ignore())
                    .ForMember(d => d.SampleProgressID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleTechnicalDrawing, SampleTechnicalDrawing>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleTechnicalDrawingID, o => o.Ignore())
                    .ForMember(d => d.SampleTechnicalDrawingFile, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleTechnicalDrawingFile, SampleTechnicalDrawingFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleTechnicalDrawingFileID, o => o.Ignore())
                    .ForMember(d => d.SampleTechnicalDrawingID, o => o.Ignore())
                    .ForMember(d => d.PreviewFileUD, o => o.Ignore())
                    .ForMember(d => d.SourceFileUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleProductSubFactory, SampleProductSubFactory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductSubFactoryID, o => o.Ignore())
                    .ForMember(d => d.SampleProductID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SamplePackaging, SamplePackaging>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SamplePackagingID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SampleProductDimensionDTO, SampleProductDimension>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductDimensionID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SampleProductPackagingMaterialDTO, SampleProductPackagingMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductPackagingMaterialID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<Sample2Mng_Model_View, DTO.ModelList>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProductDimension_View, DTO.SampleProductDimensionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProductPackagingMaterial_View, DTO.SampleProductPackagingMaterialDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleproductPackagingMaterialType_View, DTO.SampleProductPackagingMaterialTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Sample2Mng_Unit_View, DTO.UnitDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<DTO.SampleProductDimensionDTO, SampleProductDimension>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductDimensionID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<DTO.SampleProductPackagingMaterialDTO, SampleProductPackagingMaterial>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductPackagingMaterialID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<DTO.SampleProductCartonBoxDTO, SampleProductCartonBox>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductCartonBoxID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<Sample2Mng_SampleProductCartonBox_View, DTO.SampleProductCartonBoxDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample2Mng_DevelopmentType_View, DTO.DevelopmentTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SampleOrderSearchResult> DB2DTO_SampleOrderSearchResult(List<Sample2Mng_SampleOrderSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Sample2Mng_SampleOrderSearchResult_View>, List<DTO.SampleOrderSearchResult>>(dbItems);
        }

        public DTO.SampleOrder DB2DTO_SampleOrder(Sample2Mng_SampleOrder_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample2Mng_SampleOrder_View, DTO.SampleOrder>(dbItem);
        }

        public void DTO2DB_SampleOrder(DTO.SampleOrder dtoItem, ref SampleOrder dbItem, string TmpFile, int userId)
        {
            // orders
            AutoMapper.Mapper.Map<DTO.SampleOrder, SampleOrder>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.Deadline))
            {
                if (DateTime.TryParse(dtoItem.Deadline, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.Deadline = tmpDate;
                }
            }
            if (!string.IsNullOrEmpty(dtoItem.HardDeadline))
            {
                if (DateTime.TryParse(dtoItem.HardDeadline, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.HardDeadline = tmpDate;
                }
            }

            // monitor
            foreach (SampleMonitor dbMonitor in dbItem.SampleMonitor.ToArray())
            {
                if (!dtoItem.SampleMonitors.Select(o => o.SampleMonitorID).Contains(dbMonitor.SampleMonitorID))
                {
                    dbItem.SampleMonitor.Remove(dbMonitor);
                }
            }
            foreach (DTO.SampleMonitor dtoMonitor in dtoItem.SampleMonitors)
            {
                SampleMonitor dbMonitor;
                if (dtoMonitor.SampleMonitorID <= 0)
                {
                    dbMonitor = new SampleMonitor();
                    dbItem.SampleMonitor.Add(dbMonitor);
                }
                else
                {
                    dbMonitor = dbItem.SampleMonitor.FirstOrDefault(o => o.SampleMonitorID == dtoMonitor.SampleMonitorID);
                }

                if (dbMonitor != null)
                {
                    AutoMapper.Mapper.Map<DTO.SampleMonitor, SampleMonitor>(dtoMonitor, dbMonitor);
                }
            }


            // product 
            foreach (SampleProduct dbProduct in dbItem.SampleProduct.ToArray())
            {
                if (!dtoItem.SampleProducts.Select(o => o.SampleProductID).Contains(dbProduct.SampleProductID))
                {                    
                    // remove minute image
                    foreach (SampleProductMinute dbMinute in dbProduct.SampleProductMinute.ToArray())
                    {
                        foreach (SampleProductMinuteImage dbMinuteImage in dbMinute.SampleProductMinuteImage.ToArray())
                        {
                            if (!string.IsNullOrEmpty(dbMinuteImage.FileUD))
                            {
                                // remove image file
                                fwFactory.RemoveImageFile(dbMinuteImage.FileUD);
                            }
                            dbMinute.SampleProductMinuteImage.Remove(dbMinuteImage);
                        }
                        dbProduct.SampleProductMinute.Remove(dbMinute);
                    }

                    // remove reference image
                    foreach (SampleReferenceImage dbReference in dbProduct.SampleReferenceImage.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbReference.FileUD))
                        {
                            // remove image file
                            fwFactory.RemoveImageFile(dbReference.FileUD);
                        }
                        dbProduct.SampleReferenceImage.Remove(dbReference);
                    }

                    // remove technical drawing
                    foreach (SampleTechnicalDrawing dbDrawing in dbProduct.SampleTechnicalDrawing.ToArray())
                    {
                        foreach (SampleTechnicalDrawingFile dbDrawingFile in dbDrawing.SampleTechnicalDrawingFile.ToArray())
                        {
                            if (!string.IsNullOrEmpty(dbDrawingFile.PreviewFileUD))
                            {
                                // remove image file
                                fwFactory.RemoveImageFile(dbDrawingFile.PreviewFileUD);
                            }
                            if (!string.IsNullOrEmpty(dbDrawingFile.SourceFileUD))
                            {
                                // remove image file
                                fwFactory.RemoveImageFile(dbDrawingFile.SourceFileUD);
                            }
                            dbDrawing.SampleTechnicalDrawingFile.Remove(dbDrawingFile);
                        }
                        dbProduct.SampleTechnicalDrawing.Remove(dbDrawing);
                    }

                    // remove remark
                    foreach (SampleRemark dbRemark in dbProduct.SampleRemark.ToArray())
                    {
                        foreach (SampleRemarkImage dbRemarkImage in dbRemark.SampleRemarkImage.ToArray())
                        {
                            if (!string.IsNullOrEmpty(dbRemarkImage.FileUD))
                            {
                                // remove image file
                                fwFactory.RemoveImageFile(dbRemarkImage.FileUD);
                            }
                            dbRemark.SampleRemarkImage.Remove(dbRemarkImage);
                        }
                        dbProduct.SampleRemark.Remove(dbRemark);
                    }

                    // remove progress image
                    foreach (SampleProgress dbProgress in dbProduct.SampleProgress.ToArray())
                    {
                        foreach (SampleProgressImage dbProgressImage in dbProgress.SampleProgressImage.ToArray())
                        {
                            if (!string.IsNullOrEmpty(dbProgressImage.FileUD))
                            {
                                // remove image file
                                fwFactory.RemoveImageFile(dbProgressImage.FileUD);
                            }
                            dbProgress.SampleProgressImage.Remove(dbProgressImage);
                        }
                        dbProduct.SampleProgress.Remove(dbProgress);
                    }

                    // remove sample product dimension
                    foreach (SampleProductDimension dbSampleProductDimension in dbProduct.SampleProductDimension.ToArray())
                    {
                        dbProduct.SampleProductDimension.Remove(dbSampleProductDimension);
                    }

                    // remove sample product carton box
                    foreach (SampleProductCartonBox dbSampleProductCartonBox in dbProduct.SampleProductCartonBox.ToArray())
                    {
                        dbProduct.SampleProductCartonBox.Remove(dbSampleProductCartonBox);
                    }

                    // remove sample product carton box
                    foreach (SampleProductPackagingMaterial dbSampleProductPackagingMaterial in dbProduct.SampleProductPackagingMaterial.ToArray())
                    {
                        dbProduct.SampleProductPackagingMaterial.Remove(dbSampleProductPackagingMaterial);
                    }

                    dbItem.SampleProduct.Remove(dbProduct);
                }
            }
            foreach (DTO.SampleProduct dtoProduct in dtoItem.SampleProducts)
            {
                SampleProduct dbProduct;
                if (dtoProduct.SampleProductID <= 0)
                {
                    dbProduct = new SampleProduct();
                    dbProduct.CreatedBy = userId;
                    dbProduct.CreatedDate = DateTime.Now;
                    dbItem.SampleProduct.Add(dbProduct);                    
                }
                else
                {
                    dbProduct = dbItem.SampleProduct.FirstOrDefault(o => o.SampleProductID == dtoProduct.SampleProductID);
                }

                if (dbProduct != null)
                {
                    AutoMapper.Mapper.Map<DTO.SampleProduct, SampleProduct>(dtoProduct, dbProduct);

                    if (!dbProduct.SampleProductStatusID.HasValue || dbProduct.SampleProductStatusID.Value != dtoProduct.SampleProductStatusID.Value)
                    {
                        dbProduct.SampleProductStatusID = dtoProduct.SampleProductStatusID;
                        dbProduct.StatusUpdatedBy = userId;
                        dbProduct.StatusUpdatedDate = DateTime.Now;
                    }

                    // delete finished image if status is: 1,2,3,10 ~ CREATE,CONFIRMED,REVISED,REJECTED
                    if (dbProduct.SampleProductStatusID.HasValue)
                    {
                        int[] statuses = { 1, 2, 3, 10 };
                        if (statuses.Contains(dbProduct.SampleProductStatusID.Value))
                        {
                            if (!string.IsNullOrEmpty(dbProduct.FinishedImage))
                            {
                                fwFactory.RemoveImageFile(dbProduct.FinishedImage);
                                dbProduct.FinishedImage = string.Empty;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(dtoProduct.FactoryDeadline))
                    {
                        if (DateTime.TryParse(dtoProduct.FactoryDeadline, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbProduct.FactoryDeadline = tmpDate;
                        }
                    }
                    if (!string.IsNullOrEmpty(dtoProduct.ETADestination))
                    {
                        if (DateTime.TryParse(dtoProduct.ETADestination, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbProduct.ETADestination = tmpDate;
                        }
                    }
                    if (!string.IsNullOrEmpty(dtoProduct.StatusUpdatedDate))
                    {
                        if (DateTime.TryParse(dtoProduct.StatusUpdatedDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbProduct.StatusUpdatedDate = tmpDate;
                        }
                    }

                    if (dtoProduct.IsEurofarSampleCollection == true)
                    {
                        dbProduct.IsEurofarSampleCollection = true;
                        dbProduct.EurofarSampleCollectionBy = userId;
                        dbProduct.EurofarSampleCollectionDate = DateTime.Now;
                    }

                    // product sub factory
                    foreach (SampleProductSubFactory dbSubFactory in dbProduct.SampleProductSubFactory.ToArray())
                    {
                        if (!dtoProduct.SampleProductSubFactories.Select(o => o.SampleProductSubFactoryID).Contains(dbSubFactory.SampleProductSubFactoryID))
                        {
                            dbProduct.SampleProductSubFactory.Remove(dbSubFactory);
                        }
                    }
                    foreach (DTO.SampleProductSubFactory dtoSubFactory in dtoProduct.SampleProductSubFactories)
                    {
                        SampleProductSubFactory dbSubFactory;
                        if (dtoSubFactory.SampleProductSubFactoryID <= 0)
                        {
                            dbSubFactory = new SampleProductSubFactory();
                            dbProduct.SampleProductSubFactory.Add(dbSubFactory);
                        }
                        else
                        {
                            dbSubFactory = dbProduct.SampleProductSubFactory.FirstOrDefault(o => o.SampleProductSubFactoryID == dtoSubFactory.SampleProductSubFactoryID);
                        }

                        if (dbSubFactory != null)
                        {
                            AutoMapper.Mapper.Map<DTO.SampleProductSubFactory, SampleProductSubFactory>(dtoSubFactory, dbSubFactory);
                        }
                    } 

                    // product minute
                    foreach (SampleProductMinute dbMinute in dbProduct.SampleProductMinute.ToArray())
                    {
                        if (!dtoProduct.SampleProductMinutes.Select(o => o.SampleProductMinuteID).Contains(dbMinute.SampleProductMinuteID))
                        {
                            // remove minute image
                            foreach (SampleProductMinuteImage dbMinuteImage in dbMinute.SampleProductMinuteImage.ToArray())
                            {
                                if (!string.IsNullOrEmpty(dbMinuteImage.FileUD))
                                {
                                    // remove image file
                                    fwFactory.RemoveImageFile(dbMinuteImage.FileUD);
                                }
                                dbMinute.SampleProductMinuteImage.Remove(dbMinuteImage);
                            }
                            dbProduct.SampleProductMinute.Remove(dbMinute);
                        }
                    }
                    foreach (DTO.SampleProductMinute dtoMinute in dtoProduct.SampleProductMinutes)
                    {
                        SampleProductMinute dbMinute;
                        if (dtoMinute.SampleProductMinuteID <= 0)
                        {
                            dbMinute = new SampleProductMinute();
                            dbProduct.SampleProductMinute.Add(dbMinute);
                            dbMinute.UpdatedDate = DateTime.Now;
                            dbMinute.UpdatedBy = userId;
                        }
                        else
                        {
                            dbMinute = dbProduct.SampleProductMinute.FirstOrDefault(o => o.SampleProductMinuteID == dtoMinute.SampleProductMinuteID);
                        }

                        if (dbMinute != null)
                        {
                            AutoMapper.Mapper.Map<DTO.SampleProductMinute, SampleProductMinute>(dtoMinute, dbMinute);

                            // map minute image
                            foreach (SampleProductMinuteImage dbMinuteImage in dbMinute.SampleProductMinuteImage.ToArray())
                            {
                                if (!dtoMinute.SampleProductMinuteImages.Select(o => o.SampleProductMinuteImageID).Contains(dbMinuteImage.SampleProductMinuteImageID))
                                {
                                    if (!string.IsNullOrEmpty(dbMinuteImage.FileUD))
                                    {
                                        // remove image file
                                        fwFactory.RemoveImageFile(dbMinuteImage.FileUD);
                                    }
                                    dbMinute.SampleProductMinuteImage.Remove(dbMinuteImage);
                                }
                            }
                            foreach (DTO.SampleProductMinuteImage dtoMinuteImage in dtoMinute.SampleProductMinuteImages)
                            {
                                SampleProductMinuteImage dbMinuteImage;
                                if (dtoMinuteImage.SampleProductMinuteImageID <= 0)
                                {
                                    dbMinuteImage = new SampleProductMinuteImage();
                                    dbMinute.SampleProductMinuteImage.Add(dbMinuteImage);
                                }
                                else
                                {
                                    dbMinuteImage = dbMinute.SampleProductMinuteImage.FirstOrDefault(o => o.SampleProductMinuteImageID == dtoMinuteImage.SampleProductMinuteImageID);
                                }

                                if (dbMinuteImage != null)
                                {
                                    // processing images
                                    if (dtoMinuteImage.HasChanged)
                                    {
                                        dbMinuteImage.FileUD = fwFactory.CreateFilePointer(TmpFile, dtoMinuteImage.NewFile, dtoMinuteImage.FileUD, dtoMinuteImage.FriendlyName);
                                    }
                                    AutoMapper.Mapper.Map<DTO.SampleProductMinuteImage, SampleProductMinuteImage>(dtoMinuteImage, dbMinuteImage);
                                }
                            }
                        }
                    }

                    // product remark
                    foreach (SampleRemark dbRemark in dbProduct.SampleRemark.ToArray())
                    {
                        if (!dtoProduct.SampleRemarks.Select(o => o.SampleRemarkID).Contains(dbRemark.SampleRemarkID))
                        {
                            // remove remark image
                            foreach (SampleRemarkImage dbRemarkImage in dbRemark.SampleRemarkImage.ToArray())
                            {
                                if (!string.IsNullOrEmpty(dbRemarkImage.FileUD))
                                {
                                    // remove image file
                                    fwFactory.RemoveImageFile(dbRemarkImage.FileUD);
                                }
                                dbRemark.SampleRemarkImage.Remove(dbRemarkImage);
                            }
                            dbProduct.SampleRemark.Remove(dbRemark);
                        }
                    }
                    foreach (DTO.SampleRemark dtoRemark in dtoProduct.SampleRemarks)
                    {
                        SampleRemark dbRemark;
                        if (dtoRemark.SampleRemarkID <= 0)
                        {
                            dbRemark = new SampleRemark();
                            dbProduct.SampleRemark.Add(dbRemark);
                            dbRemark.UpdatedDate = DateTime.Now;
                            dbRemark.UpdatedBy = userId;
                        }
                        else
                        {
                            dbRemark = dbProduct.SampleRemark.FirstOrDefault(o => o.SampleRemarkID == dtoRemark.SampleRemarkID);
                        }

                        if (dbRemark != null)
                        {
                            AutoMapper.Mapper.Map<DTO.SampleRemark, SampleRemark>(dtoRemark, dbRemark);

                            // map remark image
                            foreach (SampleRemarkImage dbRemarkImage in dbRemark.SampleRemarkImage.ToArray())
                            {
                                if (!dtoRemark.SampleRemarkImages.Select(o => o.SampleRemarkImageID).Contains(dbRemarkImage.SampleRemarkImageID))
                                {
                                    if (!string.IsNullOrEmpty(dbRemarkImage.FileUD))
                                    {
                                        // remove image file
                                        fwFactory.RemoveImageFile(dbRemarkImage.FileUD);
                                    }
                                    dbRemark.SampleRemarkImage.Remove(dbRemarkImage);
                                }
                            }
                            foreach (DTO.SampleRemarkImage dtoRemarkImage in dtoRemark.SampleRemarkImages)
                            {
                                SampleRemarkImage dbRemarkImage;
                                if (dtoRemarkImage.SampleRemarkImageID <= 0)
                                {
                                    dbRemarkImage = new SampleRemarkImage();
                                    dbRemark.SampleRemarkImage.Add(dbRemarkImage);
                                }
                                else
                                {
                                    dbRemarkImage = dbRemark.SampleRemarkImage.FirstOrDefault(o => o.SampleRemarkImageID == dtoRemarkImage.SampleRemarkImageID);
                                }

                                if (dbRemarkImage != null)
                                {
                                    // processing images
                                    if (dtoRemarkImage.HasChanged)
                                    {
                                        dbRemarkImage.FileUD = fwFactory.CreateFilePointer(TmpFile, dtoRemarkImage.NewFile, dtoRemarkImage.FileUD, dtoRemarkImage.FriendlyName);
                                    }
                                    AutoMapper.Mapper.Map<DTO.SampleRemarkImage, SampleRemarkImage>(dtoRemarkImage, dbRemarkImage);
                                }
                            }
                        }
                    } 

                    // reference 
                    foreach (SampleReferenceImage dbReference in dbProduct.SampleReferenceImage.ToArray())
                    {
                        if (!dtoProduct.SampleReferenceImages.Select(o => o.SampleReferenceImageID).Contains(dbReference.SampleReferenceImageID))
                        {
                            // remove reference image
                            if (!string.IsNullOrEmpty(dbReference.FileUD))
                            {
                                // remove image file
                                fwFactory.RemoveImageFile(dbReference.FileUD);
                            }
                            dbProduct.SampleReferenceImage.Remove(dbReference);
                        }
                    }
                    foreach (DTO.SampleReferenceImage dtoReference in dtoProduct.SampleReferenceImages)
                    {
                        SampleReferenceImage dbReference;
                        if (dtoReference.SampleReferenceImageID <= 0)
                        {
                            dbReference = new SampleReferenceImage();
                            dbProduct.SampleReferenceImage.Add(dbReference);
                        }
                        else
                        {
                            dbReference = dbProduct.SampleReferenceImage.FirstOrDefault(o => o.SampleReferenceImageID == dtoReference.SampleReferenceImageID);
                        }

                        if (dbReference != null)
                        {
                            AutoMapper.Mapper.Map<DTO.SampleReferenceImage, SampleReferenceImage>(dtoReference, dbReference);
                            if (dtoReference.HasChanged)
                            {
                                dbReference.FileUD = fwFactory.CreateFilePointer(TmpFile, dtoReference.NewFile, dtoReference.FileUD);
                            }
                            if (!string.IsNullOrEmpty(dtoReference.BringInDate))
                            {
                                if (DateTime.TryParse(dtoReference.BringInDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                                {
                                    dbReference.BringInDate = tmpDate;
                                }
                            }
                        }
                    }

                    //Map Packaging
                    foreach(DTO.SamplePackaging dtoPackaging in dtoProduct.samplePackagings)
                    {
                        SamplePackaging dbPackaging;

                        if(dtoPackaging.SamplePackagingID <= 0)
                        {
                            dbPackaging = new SamplePackaging();
                            dbProduct.SamplePackaging.Add(dbPackaging);
                        }
                        else
                        {
                            dbPackaging = dbProduct.SamplePackaging.FirstOrDefault(o => o.SamplePackagingID == dtoPackaging.SamplePackagingID);
                        }
                        if(dbPackaging != null)
                        {
                            AutoMapper.Mapper.Map<DTO.SamplePackaging, SamplePackaging>(dtoPackaging, dbPackaging);
                        }
                    }

                    //Map SampleProductDimension
                    foreach (SampleProductDimension dbSampleProductDimension in dbProduct.SampleProductDimension.ToArray())
                    {
                        if (!dtoProduct.SampleProductDimensionDTOs.Select(o => o.SampleProductDimensionID).Contains(dbSampleProductDimension.SampleProductDimensionID))
                        {
                            dbProduct.SampleProductDimension.Remove(dbSampleProductDimension);
                        }
                    }
                    foreach (DTO.SampleProductDimensionDTO sampleProductDimensionDTO in dtoProduct.SampleProductDimensionDTOs)
                    {
                        SampleProductDimension sampleProductDimension;

                        if (sampleProductDimensionDTO.SampleProductDimensionID <= 0)
                        {
                            sampleProductDimension = new SampleProductDimension();
                            dbProduct.SampleProductDimension.Add(sampleProductDimension);
                        }
                        else
                        {
                            sampleProductDimension = dbProduct.SampleProductDimension.FirstOrDefault(o => o.SampleProductDimensionID == sampleProductDimensionDTO.SampleProductDimensionID);
                        }
                        if (sampleProductDimension != null)
                        {
                            AutoMapper.Mapper.Map<DTO.SampleProductDimensionDTO, SampleProductDimension>(sampleProductDimensionDTO, sampleProductDimension);
                        }
                    }

                    //Map SampleProductPackagingMaterial
                    foreach (DTO.SampleProductPackagingMaterialDTO sampleProductPackagingMaterialDTO in dtoProduct.SampleProductPackagingMaterialDTOs)
                    {
                        SampleProductPackagingMaterial sampleProductPackagingMaterial;

                        if (sampleProductPackagingMaterialDTO.SampleProductPackagingMaterialID <= 0)
                        {
                            sampleProductPackagingMaterial = new SampleProductPackagingMaterial();
                            dbProduct.SampleProductPackagingMaterial.Add(sampleProductPackagingMaterial);
                        }
                        else
                        {
                            sampleProductPackagingMaterial = dbProduct.SampleProductPackagingMaterial.FirstOrDefault(o => o.SampleProductPackagingMaterialID == sampleProductPackagingMaterialDTO.SampleProductPackagingMaterialID);
                        }
                        if (sampleProductPackagingMaterial != null)
                        {
                            AutoMapper.Mapper.Map<DTO.SampleProductPackagingMaterialDTO, SampleProductPackagingMaterial>(sampleProductPackagingMaterialDTO, sampleProductPackagingMaterial);
                        }
                    }

                    //Map SampleProductCartonBox
                    foreach (SampleProductCartonBox dbSampleProductCartonBox in dbProduct.SampleProductCartonBox.ToArray())
                    {
                        if (!dtoProduct.SampleProductCartonBoxDTOs.Select(o => o.SampleProductCartonBoxID).Contains(dbSampleProductCartonBox.SampleProductCartonBoxID))
                        {
                            dbProduct.SampleProductCartonBox.Remove(dbSampleProductCartonBox);
                        }
                    }
                    foreach (DTO.SampleProductCartonBoxDTO sampleProductCartonBoxDTO in dtoProduct.SampleProductCartonBoxDTOs)
                    {
                        SampleProductCartonBox sampleProductCartonBox;

                        if (sampleProductCartonBoxDTO.SampleProductCartonBoxID <= 0)
                        {
                            sampleProductCartonBox = new SampleProductCartonBox();
                            dbProduct.SampleProductCartonBox.Add(sampleProductCartonBox);
                        }
                        else
                        {
                            sampleProductCartonBox = dbProduct.SampleProductCartonBox.FirstOrDefault(o => o.SampleProductCartonBoxID == sampleProductCartonBoxDTO.SampleProductCartonBoxID);
                        }
                        if (sampleProductCartonBox != null)
                        {
                            AutoMapper.Mapper.Map<DTO.SampleProductCartonBoxDTO, SampleProductCartonBox>(sampleProductCartonBoxDTO, sampleProductCartonBox);
                        }
                    }
                }
            }          
        }

        public DTO.SampleProgress DB2DTO_SampleProgress(Sample2Mng_SampleProgress_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample2Mng_SampleProgress_View, DTO.SampleProgress>(dbItem);
        }

        public void DTO2DB_SampleProgress(DTO.SampleProgress dtoItem, ref SampleProgress dbItem, string TmpFile, int userId)
        {
            // progress
            AutoMapper.Mapper.Map<DTO.SampleProgress, SampleProgress>(dtoItem, dbItem);

            // progress image
            foreach (SampleProgressImage dbImage in dbItem.SampleProgressImage.ToArray())
            {
                if (!dtoItem.SampleProgressImages.Select(o => o.SampleProgressImageID).Contains(dbImage.SampleProgressImageID))
                {
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleProgressImage.Remove(dbImage);
                }
            }
            foreach (DTO.SampleProgressImage dtoImage in dtoItem.SampleProgressImages)
            {
                SampleProgressImage dbImage;
                if (dtoImage.SampleProgressImageID <= 0)
                {
                    dbImage = new SampleProgressImage();
                    dbItem.SampleProgressImage.Add(dbImage);
                }
                else
                {
                    dbImage = dbItem.SampleProgressImage.FirstOrDefault(o => o.SampleProgressImageID == dtoImage.SampleProgressImageID);
                }

                if (dbImage != null)
                {
                    // change or add images
                    if (dtoImage.HasChanged)
                    {
                        dbImage.FileUD = fwFactory.CreateFilePointer(TmpFile, dtoImage.NewFile, dtoImage.FileUD);
                    }
                    AutoMapper.Mapper.Map<DTO.SampleProgressImage, SampleProgressImage>(dtoImage, dbImage);
                }
            }
        }

        public DTO.SampleTechnicalDrawing DB2DTO_SampleTechnicalDrawing(Sample2Mng_SampleTechnicalDrawing_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample2Mng_SampleTechnicalDrawing_View, DTO.SampleTechnicalDrawing>(dbItem);
        }

        public void DTO2DB_SampleTechnicalDrawing(DTO.SampleTechnicalDrawing dtoItem, ref SampleTechnicalDrawing dbItem, string TmpFile, int userId)
        {
            // technical drawing
            AutoMapper.Mapper.Map<DTO.SampleTechnicalDrawing, SampleTechnicalDrawing>(dtoItem, dbItem);

            // technical drawing file
            foreach (SampleTechnicalDrawingFile dbImage in dbItem.SampleTechnicalDrawingFile.ToArray())
            {
                if (!dtoItem.SampleTechnicalDrawingFiles.Select(o => o.SampleTechnicalDrawingFileID).Contains(dbImage.SampleTechnicalDrawingFileID))
                {
                    if (!string.IsNullOrEmpty(dbImage.PreviewFileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbImage.PreviewFileUD);
                    }
                    if (!string.IsNullOrEmpty(dbImage.SourceFileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbImage.SourceFileUD);
                    }
                    dbItem.SampleTechnicalDrawingFile.Remove(dbImage);
                }
            }
            foreach (DTO.SampleTechnicalDrawingFile dtoImage in dtoItem.SampleTechnicalDrawingFiles)
            {
                SampleTechnicalDrawingFile dbImage;
                if (dtoImage.SampleTechnicalDrawingFileID <= 0)
                {
                    dbImage = new SampleTechnicalDrawingFile();
                    dbItem.SampleTechnicalDrawingFile.Add(dbImage);
                }
                else
                {
                    dbImage = dbItem.SampleTechnicalDrawingFile.FirstOrDefault(o => o.SampleTechnicalDrawingFileID == dtoImage.SampleTechnicalDrawingFileID);
                }

                if (dbImage != null)
                {
                    AutoMapper.Mapper.Map<DTO.SampleTechnicalDrawingFile, SampleTechnicalDrawingFile>(dtoImage, dbImage);
                    if (dtoImage.SampleTechnicalDrawingFileID <= 0)
                    {
                        dbImage.UpdatedDate = DateTime.Now;
                        dbImage.UpdatedBy = userId;
                    }                    

                    // change or add images
                    if (dtoImage.PreviewFileHasChanged)
                    {
                        dbImage.PreviewFileUD = fwFactory.CreateFilePointer(TmpFile, dtoImage.PreviewFileNewFile, dtoImage.PreviewFileUD, dtoImage.PreviewFileFriendlyName);
                    }
                    if (dtoImage.SourceFileHasChanged)
                    {
                        dbImage.SourceFileUD = fwFactory.CreateFilePointer(TmpFile, dtoImage.SourceFileNewFile, dtoImage.SourceFileUD, dtoImage.SourceFileFriendlyName);
                    }
                }
            }
        }

        public DTO.SampleRemark DB2DTO_SampleRemark(Sample2Mng_SampleRemark_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample2Mng_SampleRemark_View, DTO.SampleRemark>(dbItem);
        }

        public void DTO2DB_SampleRemark(DTO.SampleRemark dtoItem, ref SampleRemark dbItem, string TmpFile, int userId)
        {
            // remark
            AutoMapper.Mapper.Map<DTO.SampleRemark, SampleRemark>(dtoItem, dbItem);

            // remark image
            foreach (SampleRemarkImage dbImage in dbItem.SampleRemarkImage.ToArray())
            {
                if (!dtoItem.SampleRemarkImages.Select(o => o.SampleRemarkImageID).Contains(dbImage.SampleRemarkImageID))
                {
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleRemarkImage.Remove(dbImage);
                }
            }
            foreach (DTO.SampleRemarkImage dtoImage in dtoItem.SampleRemarkImages)
            {
                SampleRemarkImage dbImage;
                if (dtoImage.SampleRemarkImageID <= 0)
                {
                    dbImage = new SampleRemarkImage();
                    dbItem.SampleRemarkImage.Add(dbImage);
                }
                else
                {
                    dbImage = dbItem.SampleRemarkImage.FirstOrDefault(o => o.SampleRemarkImageID == dtoImage.SampleRemarkImageID);
                }

                if (dbImage != null)
                {
                    // change or add images
                    if (dtoImage.HasChanged)
                    {
                        dbImage.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoImage.NewFile, dtoImage.FileUD, dtoImage.FriendlyName);
                    }
                    AutoMapper.Mapper.Map<DTO.SampleRemarkImage, SampleRemarkImage>(dtoImage, dbImage);
                }
            }
        }

        public DTO.SampleQARemark DB2DTO_SampleQARemark(Sample2Mng_SampleQARemark_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample2Mng_SampleQARemark_View, DTO.SampleQARemark>(dbItem);
        }

        public void DTO2DB_SampleQARemark(DTO.SampleQARemark dtoItem, ref SampleQARemark dbItem, string TmpFile, int userId)
        {
            // remark
            AutoMapper.Mapper.Map<DTO.SampleQARemark, SampleQARemark>(dtoItem, dbItem);

            // remark image
            foreach (SampleQARemarkImage dbImage in dbItem.SampleQARemarkImage.ToArray())
            {
                if (!dtoItem.SampleQARemarkImages.Select(o => o.SampleQARemarkImageID).Contains(dbImage.SampleQARemarkImageID))
                {
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        // remove image file
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleQARemarkImage.Remove(dbImage);
                }
            }
            foreach (DTO.SampleQARemarkImage dtoImage in dtoItem.SampleQARemarkImages)
            {
                SampleQARemarkImage dbImage;
                if (dtoImage.SampleQARemarkImageID <= 0)
                {
                    dbImage = new SampleQARemarkImage();
                    dbItem.SampleQARemarkImage.Add(dbImage);
                }
                else
                {
                    dbImage = dbItem.SampleQARemarkImage.FirstOrDefault(o => o.SampleQARemarkImageID == dtoImage.SampleQARemarkImageID);
                }

                if (dbImage != null)
                {
                    // change or add images
                    if (dtoImage.HasChanged)
                    {
                        dbImage.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoImage.NewFile, dtoImage.FileUD, dtoImage.FriendlyName);
                    }
                    AutoMapper.Mapper.Map<DTO.SampleQARemarkImage, SampleQARemarkImage>(dtoImage, dbImage);
                }
            }
        }

        public void DTO2DB_SampleItemInfo(DTO.SampleProduct dtoItem, ref SampleProduct dbItem, int userId)
        {
            // product
            AutoMapper.Mapper.Map<DTO.SampleProduct, SampleProduct>(dtoItem, dbItem);
        }

        public List<DTO.ModelList> DB2DTO_ModelList(List<Sample2Mng_Model_View> dbItem)
        {
            return Mapper.Map<List<Sample2Mng_Model_View>, List<DTO.ModelList>>(dbItem);
        }
        public List<DTO.DevelopmentTypeDTO> DB2DTO_DevelopmentList(List<Sample2Mng_DevelopmentType_View> dbItem)
        {
            return Mapper.Map<List<Sample2Mng_DevelopmentType_View>, List<DTO.DevelopmentTypeDTO>>(dbItem);
        }
    }
}
