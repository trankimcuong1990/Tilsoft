using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.Sample3Mng.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                #region ORDER

                // DB 2 DTO
                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleOrderSearchResult_View, DTO.SampleOrderSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.HardDeadline, o => o.ResolveUsing(s => s.HardDeadline.HasValue ? s.HardDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Deadline, o => o.ResolveUsing(s => s.Deadline.HasValue ? s.Deadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_MonitorUser_View, DTO.MonitorUserDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleMonitor_View, DTO.SampleMonitorDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleOrder_View, DTO.SampleOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.HardDeadline, o => o.ResolveUsing(s => s.HardDeadline.HasValue ? s.HardDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Deadline, o => o.ResolveUsing(s => s.Deadline.HasValue ? s.Deadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SampleMonitorDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleMonitor_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleOrderOverview_Order_View, DTO.SampleOrderOverview.OrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.HardDeadline, o => o.ResolveUsing(s => s.HardDeadline.HasValue ? s.HardDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Deadline, o => o.ResolveUsing(s => s.Deadline.HasValue ? s.Deadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.MonitorDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleOrderOverview_Monitor_View))
                    .ForMember(d => d.ProductDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleOrderOverview_Product_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleOrderOverview_Product_View, DTO.SampleOrderOverview.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.QARemarkUpdatedDate, o => o.ResolveUsing(s => s.QARemarkUpdatedDate.HasValue ? s.QARemarkUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.FinishedImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FinishedImageThumbnailLocation) ? s.FinishedImageThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FinishedImageFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FinishedImageFileLocation) ? s.FinishedImageFileLocation : "no-image.jpg")))
                    .ForMember(d => d.SubFactoryDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleOrderOverview_ProductSubFactory_View))
                    .ForMember(d => d.ProductLocationDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleOrderOverview_ProductLocation_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleOrderOverview_Monitor_View, DTO.SampleOrderOverview.MonitorDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleOrderOverview_ProductSubFactory_View, DTO.SampleOrderOverview.SubFactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleOrderOverview_ProductLocation_View, DTO.SampleOrderOverview.ProductLocationDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.LocationDate, o => o.ResolveUsing(s => s.LocationDate.HasValue ? s.LocationDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // DTO 2 DB
                AutoMapper.Mapper.CreateMap<DTO.SampleMonitorDTO, SampleMonitor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleMonitorID, o => o.Ignore())
                    .ForMember(d => d.SampleOrderID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.SampleOrderDTO, SampleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleOrderID, o => o.Ignore())
                    .ForMember(d => d.ClientID, o => o.Ignore())
                    .ForMember(d => d.SampleOrderStatusID, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.Season, o => o.Ignore())
                    .ForMember(d => d.SampleOrderUD, o => o.Ignore())
                    .ForMember(d => d.Deadline, o => o.Ignore())
                    .ForMember(d => d.HardDeadline, o => o.Ignore())
                    .ForMember(d => d.SampleProduct, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                #endregion
                
                #region PRODUCT

                // DB 2 DTO
                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_InternalRemark_View, DTO.SampleProductOverview.InternalRemarkDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.InternalRemarkImageDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_InternalRemarkImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_InternalRemarkImage_View, DTO.SampleProductOverview.InternalRemarkImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_ItemLocation_View, DTO.SampleProductOverview.ItemLocationDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.LocationDate, o => o.ResolveUsing(s => s.LocationDate.HasValue ? s.LocationDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_Progress_View, DTO.SampleProductOverview.ProgressDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.ProgressImageDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_ProgressImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_ProgressImage_View, DTO.SampleProductOverview.ProgressImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_QARemark_View, DTO.SampleProductOverview.QARemarkDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.QARemarkImageDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_QARemarkImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_QARemarkImage_View, DTO.SampleProductOverview.QARemarkImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_ReferenceImage_View, DTO.SampleProductOverview.ReferenceImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BringInDate, o => o.ResolveUsing(s => s.BringInDate.HasValue ? s.BringInDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_SubFactory_View, DTO.SampleProductOverview.SubFactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_TechnicalDrawing_View, DTO.SampleProductOverview.TechnicalDrawingDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SourceFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.SourceFileLocation) ? s.SourceFileLocation : "no-image.jpg")))
                    .ForMember(d => d.PreviewFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.PreviewFileLocation) ? s.PreviewFileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_SampleProductOverview_Product_View, DTO.SampleProductOverview.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.FinishedImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FinishedImageThumbnailLocation) ? s.FinishedImageThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FinishedImageFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FinishedImageFileLocation) ? s.FinishedImageFileLocation : "no-image.jpg")))
                    .ForMember(d => d.SubFactoryDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_SubFactory_View))
                    .ForMember(d => d.InternalRemarkDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_InternalRemark_View))
                    .ForMember(d => d.QARemarkDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_QARemark_View))
                    .ForMember(d => d.ReferenceImageDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_ReferenceImage_View))
                    .ForMember(d => d.ItemLocationDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_ItemLocation_View))
                    .ForMember(d => d.ProgressDTOs, o => o.MapFrom(s => s.Sample3Mng_SampleProductOverview_Progress_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                #endregion
                
                #region FACTORY ASSIGNMENT

                // DTO 2 DB
                AutoMapper.Mapper.CreateMap<Sample3Mng_FactoryAssignment_Product_View, DTO.FactoryAssignment.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.SubFactoryDTOs, o => o.MapFrom(s => s.Sample3Mng_FactoryAssignment_SubFactory_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_FactoryAssignment_SubFactory_View, DTO.FactoryAssignment.SubFactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // DB 2 DTO
                AutoMapper.Mapper.CreateMap<DTO.FactoryAssignment.ProductDTO, SampleProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductID, o => o.Ignore())
                    .ForMember(d => d.SampleProductUD, o => o.Ignore())
                    .ForMember(d => d.ArticleDescription, o => o.Ignore())
                    .ForMember(d => d.Quantity, o => o.Ignore())
                    .ForMember(d => d.FactoryDeadline, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.FactoryAssignment.SubFactoryDTO, SampleProductSubFactory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductSubFactoryID, o => o.Ignore())
                    .ForMember(d => d.SampleProductID, o => o.Ignore());

                #endregion

                #region INTERNAL REMARK

                // DB 2 DTO
                AutoMapper.Mapper.CreateMap<Sample3Mng_InternalRemark_Product_View, DTO.InternalRemark.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.RemarkDTOs, o => o.MapFrom(s => s.Sample3Mng_InternalRemark_Remark_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_InternalRemark_Remark_View, DTO.InternalRemark.RemarkDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.RemarkImageDTOs, o => o.MapFrom(s => s.Sample3Mng_InternalRemark_RemarkImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_InternalRemark_RemarkImage_View, DTO.InternalRemark.RemarkImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // DTO 2 DB
                AutoMapper.Mapper.CreateMap<DTO.InternalRemark.RemarkDTO, SampleRemark>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleRemarkID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.InternalRemark.RemarkImageDTO, SampleRemarkImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleRemarkImageID, o => o.Ignore())
                    .ForMember(d => d.SampleRemarkID, o => o.Ignore());

                #endregion

                #region QA REMARK

                // DB 2 DTO
                AutoMapper.Mapper.CreateMap<Sample3Mng_QARemark_Product_View, DTO.QARemark.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.RemarkDTOs, o => o.MapFrom(s => s.Sample3Mng_QARemark_Remark_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_QARemark_Remark_View, DTO.QARemark.RemarkDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.RemarkImageDTOs, o => o.MapFrom(s => s.Sample3Mng_QARemark_RemarkImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_QARemark_RemarkImage_View, DTO.QARemark.RemarkImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // DTO 2 DB
                AutoMapper.Mapper.CreateMap<DTO.QARemark.RemarkDTO, SampleQARemark>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleQARemarkID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.QARemark.RemarkImageDTO, SampleQARemarkImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleQARemarkImageID, o => o.Ignore())
                    .ForMember(d => d.SampleQARemarkID, o => o.Ignore());

                #endregion

                #region BUILDING PROCESS

                // DB 2 DTO
                AutoMapper.Mapper.CreateMap<Sample3Mng_BuildingProcess_Product_View, DTO.BuildingProcess.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ProgressDTOs, o => o.MapFrom(s => s.Sample3Mng_BuildingProcess_Progress_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_BuildingProcess_Progress_View, DTO.BuildingProcess.ProgressDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ProgressImageDTOs, o => o.MapFrom(s => s.Sample3Mng_BuildingProcess_ProgressImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_BuildingProcess_ProgressImage_View, DTO.BuildingProcess.ProgressImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // DTO 2 DB
                AutoMapper.Mapper.CreateMap<DTO.BuildingProcess.ProgressDTO, SampleProgress>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProgressID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BuildingProcess.ProgressImageDTO, SampleProgressImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProgressImageID, o => o.Ignore())
                    .ForMember(d => d.SampleProgressID, o => o.Ignore());

                #endregion

                #region ITEM DATA

                // DB 2 DTO
                AutoMapper.Mapper.CreateMap<Sample3Mng_ItemData_Product_View, DTO.ItemData.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // DTO 2 DB
                AutoMapper.Mapper.CreateMap<DTO.ItemData.ProductDTO, SampleProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductID, o => o.Ignore())
                    .ForMember(d => d.SampleProductUD, o => o.Ignore())
                    .ForMember(d => d.ArticleDescription, o => o.Ignore())
                    .ForMember(d => d.Quantity, o => o.Ignore())
                    .ForMember(d => d.FactoryDeadline, o => o.Ignore());

                #endregion

                #region PRODUCT INFO

                // DB 2 DTO
                AutoMapper.Mapper.CreateMap<Sample3Mng_ProductInfo_Product_View, DTO.ProductInfo.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryDeadline, o => o.ResolveUsing(s => s.FactoryDeadline.HasValue ? s.FactoryDeadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishedImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FinishedImageThumbnailLocation) ? s.FinishedImageThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FinishedImageFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FinishedImageFileLocation) ? s.FinishedImageFileLocation : "no-image.jpg")))
                    .ForMember(d => d.ReferenceImageDTOs, o => o.MapFrom(s => s.Sample3Mng_ProductInfo_ReferenceImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Sample3Mng_ProductInfo_ReferenceImage_View, DTO.ProductInfo.ReferenceImageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BringInDate, o => o.ResolveUsing(s => s.BringInDate.HasValue ? s.BringInDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // DTO 2 DB
                AutoMapper.Mapper.CreateMap<DTO.ProductInfo.ProductDTO, SampleProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductID, o => o.Ignore())
                    .ForMember(d => d.SampleProductUD, o => o.Ignore())
                    .ForMember(d => d.FactoryDeadline, o => o.Ignore())
                    .ForMember(d => d.SampleOrderID, o => o.Ignore())
                    .ForMember(d => d.SampleReferenceImage, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductInfo.ReferenceImageDTO, SampleReferenceImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductID, o => o.Ignore())
                    .ForMember(d => d.SampleReferenceImageID, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.BringInDate, o => o.Ignore());

                #endregion

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        #region ORDER

        // DB 2 DTO
        public List<DTO.SampleOrderSearchResultDTO> DB2DTO_SampleOrderSearchResultList(List<Sample3Mng_SampleOrderSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Sample3Mng_SampleOrderSearchResult_View>, List<DTO.SampleOrderSearchResultDTO>>(dbItems);
        }

        public DTO.SampleOrderDTO DB2DTO_SampleOrder(Sample3Mng_SampleOrder_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_SampleOrder_View, DTO.SampleOrderDTO>(dbItem);
        }

        public DTO.SampleOrderOverview.OrderDTO DB2DTO_SampleOrderOverview(Sample3Mng_SampleOrderOverview_Order_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_SampleOrderOverview_Order_View, DTO.SampleOrderOverview.OrderDTO>(dbItem);
        }

        public List<DTO.MonitorUserDTO> DB2DTO_MonitorUserList(List<Sample3Mng_MonitorUser_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Sample3Mng_MonitorUser_View>, List<DTO.MonitorUserDTO>>(dbItems);
        }

        // DTO 2 DB
        public void DTO2DB_SampleOrder(DTO.SampleOrderDTO dtoItem, ref SampleOrder dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SampleOrderDTO, SampleOrder>(dtoItem, dbItem);
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
            if (dtoItem.SampleOrderID <= 0)
            {
                dbItem.ClientID = dtoItem.ClientID;
                dbItem.Season = dtoItem.Season;
            }

            // monitor
            foreach (SampleMonitor dbMonitor in dbItem.SampleMonitor.ToArray())
            {
                if (!dtoItem.SampleMonitorDTOs.Select(o => o.SampleMonitorID).Contains(dbMonitor.SampleMonitorID))
                {
                    dbItem.SampleMonitor.Remove(dbMonitor);
                }
            }
            foreach (DTO.SampleMonitorDTO dtoMonitor in dtoItem.SampleMonitorDTOs)
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
                    AutoMapper.Mapper.Map<DTO.SampleMonitorDTO, SampleMonitor>(dtoMonitor, dbMonitor);
                }
            }
        }

        #endregion

        #region PRODUCT

        // DB 2 DTO
        public DTO.SampleProductOverview.ProductDTO DB2DTO_SampleProductOverview(Sample3Mng_SampleProductOverview_Product_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_SampleProductOverview_Product_View, DTO.SampleProductOverview.ProductDTO>(dbItem);
        }

        // DTO 2 DB

        #endregion

        #region FACTORY ASSIGNMENT

        public DTO.FactoryAssignment.ProductDTO DB2DTO_FactoryAssignment_Product(Sample3Mng_FactoryAssignment_Product_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_FactoryAssignment_Product_View, DTO.FactoryAssignment.ProductDTO>(dbItem);
        }
        public void DTO2DB_FactoryAssignment(DTO.FactoryAssignment.ProductDTO dtoItem, ref SampleProduct dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactoryAssignment.ProductDTO, SampleProduct>(dtoItem, dbItem);
            if (!string.IsNullOrEmpty(dtoItem.FactoryDeadline))
            {
                if (DateTime.TryParse(dtoItem.FactoryDeadline, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                {
                    dbItem.FactoryDeadline = tmpDate;
                }
            }

            // monitor
            foreach (SampleProductSubFactory dbSubFactory in dbItem.SampleProductSubFactory.ToArray())
            {
                if (!dtoItem.SubFactoryDTOs.Select(o => o.SampleProductSubFactoryID).Contains(dbSubFactory.SampleProductSubFactoryID))
                {
                    dbItem.SampleProductSubFactory.Remove(dbSubFactory);
                }
            }
            foreach (DTO.FactoryAssignment.SubFactoryDTO dtoSubFactory in dtoItem.SubFactoryDTOs)
            {
                SampleProductSubFactory dbSubFactory;
                if (dtoSubFactory.SampleProductSubFactoryID <= 0)
                {
                    dbSubFactory = new SampleProductSubFactory();
                    dbItem.SampleProductSubFactory.Add(dbSubFactory);
                }
                else
                {
                    dbSubFactory = dbItem.SampleProductSubFactory.FirstOrDefault(o => o.SampleProductSubFactoryID == dtoSubFactory.SampleProductSubFactoryID);
                }

                if (dbSubFactory != null)
                {
                    AutoMapper.Mapper.Map<DTO.FactoryAssignment.SubFactoryDTO, SampleProductSubFactory>(dtoSubFactory, dbSubFactory);
                }
            }
        }

        #endregion

        #region INTERNAL REMARK

        public DTO.InternalRemark.ProductDTO DB2DTO_InternalRemark_Product(Sample3Mng_InternalRemark_Product_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_InternalRemark_Product_View, DTO.InternalRemark.ProductDTO>(dbItem);
        }
        public DTO.InternalRemark.RemarkDTO DB2DTO_InternalRemark_Remark(Sample3Mng_InternalRemark_Remark_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_InternalRemark_Remark_View, DTO.InternalRemark.RemarkDTO>(dbItem);
        }
        public void DTO2DB_InternalRemark(DTO.InternalRemark.RemarkDTO dtoItem, ref SampleRemark dbItem, string TmpFile)
        {
            AutoMapper.Mapper.Map<DTO.InternalRemark.RemarkDTO, SampleRemark>(dtoItem, dbItem);

            // remark image
            foreach (SampleRemarkImage dbImage in dbItem.SampleRemarkImage.ToArray())
            {
                if (!dtoItem.RemarkImageDTOs.Select(o => o.SampleRemarkImageID).Contains(dbImage.SampleRemarkImageID))
                {
                    // delete files
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleRemarkImage.Remove(dbImage);
                }
            }
            foreach (DTO.InternalRemark.RemarkImageDTO dtoImage in dtoItem.RemarkImageDTOs)
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

                if (dbItem != null)
                {
                    AutoMapper.Mapper.Map<DTO.InternalRemark.RemarkImageDTO, SampleRemarkImage>(dtoImage, dbImage);
                    if (dtoImage.HasChanged && !string.IsNullOrEmpty(dtoImage.NewFile))
                    {
                        dbImage.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoImage.NewFile, dbImage.FileUD, dtoImage.FriendlyName);
                    }
                }
            }
        }

        #endregion

        #region QA REMARK

        public DTO.QARemark.ProductDTO DB2DTO_QARemark_Product(Sample3Mng_QARemark_Product_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_QARemark_Product_View, DTO.QARemark.ProductDTO>(dbItem);
        }
        public DTO.QARemark.RemarkDTO DB2DTO_QARemark_Remark(Sample3Mng_QARemark_Remark_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_QARemark_Remark_View, DTO.QARemark.RemarkDTO>(dbItem);
        }
        public void DTO2DB_QARemark(DTO.QARemark.RemarkDTO dtoItem, ref SampleQARemark dbItem, string TmpFile)
        {
            AutoMapper.Mapper.Map<DTO.QARemark.RemarkDTO, SampleQARemark>(dtoItem, dbItem);

            // remark image
            foreach (SampleQARemarkImage dbImage in dbItem.SampleQARemarkImage.ToArray())
            {
                if (!dtoItem.RemarkImageDTOs.Select(o => o.SampleQARemarkImageID).Contains(dbImage.SampleQARemarkImageID))
                {
                    // delete files
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleQARemarkImage.Remove(dbImage);
                }
            }
            foreach (DTO.QARemark.RemarkImageDTO dtoImage in dtoItem.RemarkImageDTOs)
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

                if (dbItem != null)
                {
                    AutoMapper.Mapper.Map<DTO.QARemark.RemarkImageDTO, SampleQARemarkImage>(dtoImage, dbImage);
                    if (dtoImage.HasChanged && !string.IsNullOrEmpty(dtoImage.NewFile))
                    {
                        dbImage.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoImage.NewFile, dbImage.FileUD, dtoImage.FriendlyName);
                    }
                }
            }
        }

        #endregion

        #region BUILDING PROCESS

        public DTO.BuildingProcess.ProductDTO DB2DTO_BuildingProcess_Product(Sample3Mng_BuildingProcess_Product_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_BuildingProcess_Product_View, DTO.BuildingProcess.ProductDTO>(dbItem);
        }
        public DTO.BuildingProcess.ProgressDTO DB2DTO_BuildingProcess_Progress(Sample3Mng_BuildingProcess_Progress_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_BuildingProcess_Progress_View, DTO.BuildingProcess.ProgressDTO>(dbItem);
        }
        public void DTO2DB_BuildingProcess(DTO.BuildingProcess.ProgressDTO dtoItem, ref SampleProgress dbItem, string TmpFile)
        {
            AutoMapper.Mapper.Map<DTO.BuildingProcess.ProgressDTO, SampleProgress>(dtoItem, dbItem);

            // remark image
            foreach (SampleProgressImage dbImage in dbItem.SampleProgressImage.ToArray())
            {
                if (!dtoItem.ProgressImageDTOs.Select(o => o.SampleProgressImageID).Contains(dbImage.SampleProgressImageID))
                {
                    // delete files
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleProgressImage.Remove(dbImage);
                }
            }
            foreach (DTO.BuildingProcess.ProgressImageDTO dtoImage in dtoItem.ProgressImageDTOs)
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

                if (dbItem != null)
                {
                    AutoMapper.Mapper.Map<DTO.BuildingProcess.ProgressImageDTO, SampleProgressImage>(dtoImage, dbImage);
                    if (dtoImage.HasChanged && !string.IsNullOrEmpty(dtoImage.NewFile))
                    {
                        dbImage.FileUD = fwFactory.CreateNoneImageFilePointer(TmpFile, dtoImage.NewFile, dbImage.FileUD);
                    }
                }
            }
        }

        #endregion

        #region ITEM DATA

        public DTO.ItemData.ProductDTO DB2DTO_ItemData_Product(Sample3Mng_ItemData_Product_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_ItemData_Product_View, DTO.ItemData.ProductDTO>(dbItem);
        }
        public void DTO2DB_ItemData(DTO.ItemData.ProductDTO dtoItem, ref SampleProduct dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ItemData.ProductDTO, SampleProduct>(dtoItem, dbItem);
        }

        #endregion

        #region PRODUCT INFO

        public DTO.ProductInfo.ProductDTO DB2DTO_ProductInfo_Product(Sample3Mng_ProductInfo_Product_View dbItem)
        {
            return AutoMapper.Mapper.Map<Sample3Mng_ProductInfo_Product_View, DTO.ProductInfo.ProductDTO>(dbItem);
        }
        public void DTO2DB_ProductInfo(DTO.ProductInfo.ProductDTO dtoItem, ref SampleProduct dbItem, string TmpFile)
        {
            AutoMapper.Mapper.Map<DTO.ProductInfo.ProductDTO, SampleProduct>(dtoItem, dbItem);

            // remark image
            foreach (SampleReferenceImage dbImage in dbItem.SampleReferenceImage.ToArray())
            {
                if (!dtoItem.ReferenceImageDTOs.Select(o => o.SampleReferenceImageID).Contains(dbImage.SampleReferenceImageID))
                {
                    // delete files
                    if (!string.IsNullOrEmpty(dbImage.FileUD))
                    {
                        fwFactory.RemoveImageFile(dbImage.FileUD);
                    }
                    dbItem.SampleReferenceImage.Remove(dbImage);
                }
            }
            foreach (DTO.ProductInfo.ReferenceImageDTO dtoImage in dtoItem.ReferenceImageDTOs)
            {
                SampleReferenceImage dbImage;
                if (dtoImage.SampleReferenceImageID <= 0)
                {
                    dbImage = new SampleReferenceImage();
                    dbItem.SampleReferenceImage.Add(dbImage);
                }
                else
                {
                    dbImage = dbItem.SampleReferenceImage.FirstOrDefault(o => o.SampleReferenceImageID == dtoImage.SampleReferenceImageID);
                }

                if (dbItem != null)
                {
                    AutoMapper.Mapper.Map<DTO.ProductInfo.ReferenceImageDTO, SampleReferenceImage>(dtoImage, dbImage);
                    if (!string.IsNullOrEmpty(dtoImage.BringInDate))
                    {
                        if (DateTime.TryParse(dtoImage.BringInDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbImage.BringInDate = tmpDate;
                        }
                    }
                    if (dtoImage.HasChanged && !string.IsNullOrEmpty(dtoImage.NewFile))
                    {
                        dbImage.FileUD = fwFactory.CreateFilePointer(TmpFile, dtoImage.NewFile, dbImage.FileUD);
                    }
                }
            }
        }

        #endregion
    }
}
