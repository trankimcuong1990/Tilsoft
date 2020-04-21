using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;

namespace Module.LabelingPackaging.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;
        public DataConverter()
        {
            string mapName = "LabelingPackagingMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackaging_SearchView, DTO.LabelingPackagingSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackagingDetail_View, DTO.LabelingPackagingDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.HangTagFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangTagFileUrl) ? s.HangTagFileUrl : "no-image.jpg")))
                   .ForMember(d => d.BoxShippingMarkFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BoxShippingMarkFileUrl) ? s.BoxShippingMarkFileUrl : "no-image.jpg")))
                   .ForMember(d => d.BrassLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BrassLabelFileUrl) ? s.BrassLabelFileUrl : "no-image.jpg")))
                   .ForMember(d => d.AIFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIFileUrl) ? s.AIFileUrl : "no-image.jpg")))
                   .ForMember(d => d.WashCushionLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.WashCushionLabelFileUrl) ? s.WashCushionLabelFileUrl : "no-image.jpg")))
                   .ForMember(d => d.FSCLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FSCLabelFileUrl) ? s.FSCLabelFileUrl : "no-image.jpg")))
                   .ForMember(d => d.HangtagStandardFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangtagStandardFileUrl) ? s.HangtagStandardFileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option1FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option1FileUrl) ? s.Option1FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option2FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option2FileUrl) ? s.Option2FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option3FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option3FileUrl) ? s.Option3FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option4FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option4FileUrl) ? s.Option4FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option5FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option5FileUrl) ? s.Option5FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option6FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option6FileUrl) ? s.Option6FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option7FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option7FileUrl) ? s.Option7FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option8FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option8FileUrl) ? s.Option8FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option9FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option9FileUrl) ? s.Option9FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option10FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option10FileUrl) ? s.Option10FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option11FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option11FileUrl) ? s.Option11FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option12FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option12FileUrl) ? s.Option12FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option13FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option13FileUrl) ? s.Option13FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option14FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option14FileUrl) ? s.Option14FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option15FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option15FileUrl) ? s.Option15FileUrl : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackagingFileMonitor_View, DTO.LabelingPackagingFileMonitor>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackagingSparepartDetail_View, DTO.LabelingPackagingSparepartDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.HangTagFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangTagFileUrl) ? s.HangTagFileUrl : "no-image.jpg")))
                   .ForMember(d => d.BoxShippingMarkFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BoxShippingMarkFileUrl) ? s.BoxShippingMarkFileUrl : "no-image.jpg")))
                   .ForMember(d => d.BrassLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BrassLabelFileUrl) ? s.BrassLabelFileUrl : "no-image.jpg")))
                   .ForMember(d => d.AIFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIFileUrl) ? s.AIFileUrl : "no-image.jpg")))
                   .ForMember(d => d.WashCushionLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.WashCushionLabelFileUrl) ? s.WashCushionLabelFileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option1FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option1FileUrl) ? s.Option1FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option2FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option2FileUrl) ? s.Option2FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option3FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option3FileUrl) ? s.Option3FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option4FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option4FileUrl) ? s.Option4FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option5FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option5FileUrl) ? s.Option5FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option6FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option6FileUrl) ? s.Option6FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option7FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option7FileUrl) ? s.Option7FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option8FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option8FileUrl) ? s.Option8FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option9FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option9FileUrl) ? s.Option9FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option10FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option10FileUrl) ? s.Option10FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option11FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option11FileUrl) ? s.Option11FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option12FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option12FileUrl) ? s.Option12FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option13FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option13FileUrl) ? s.Option13FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option14FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option14FileUrl) ? s.Option14FileUrl : "no-image.jpg")))
                   .ForMember(d => d.Option15FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.Option15FileUrl) ? s.Option15FileUrl : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackagingRemark_View, DTO.LabelingPackagingRemark>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.RemarkFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.RemarkFileUrl) ? s.RemarkFileUrl : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackagingOtherFile_View, DTO.LabelingPackagingOtherFile>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.OtherFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.OtherFileUrl) ? s.OtherFileUrl : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackaging_View, DTO.LabelingPackaging>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.OtherFileFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.OtherFileFileUrl) ? s.OtherFileFileUrl : "no-image.jpg")))
                    .ForMember(d => d.RejectCommentFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.RejectCommentFile_FileUrl) ? s.RejectCommentFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOHangTagFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOHangTagFile_FileUrl) ? s.AIOHangTagFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOBoxShippingMarkFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOBoxShippingMarkFile_FileUrl) ? s.AIOBoxShippingMarkFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOBrassLabelFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOBrassLabelFile_FileUrl) ? s.AIOBrassLabelFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOAIFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOAIFile_FileUrl) ? s.AIOAIFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOWashCushionLabelFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOWashCushionLabelFile_FileUrl) ? s.AIOWashCushionLabelFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOFSCLabelFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOFSCLabelFile_FileUrl) ? s.AIOFSCLabelFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOOption1File_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOOption1File_FileUrl) ? s.AIOOption1File_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOOption2File_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOOption2File_FileUrl) ? s.AIOOption2File_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOOption3File_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOOption3File_FileUrl) ? s.AIOOption3File_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.LabelingPackagingDetails, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingDetail_View))
                    .ForMember(d => d.LabelingPackagingFileMonitors, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingFileMonitor_View))
                    .ForMember(d => d.LabelingPackagingSparepartDetails, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingSparepartDetail_View))
                    .ForMember(d => d.LabelingPackagingRemarks, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingRemark_View))
                    .ForMember(d => d.LabelingPackagingOtherFiles, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingOtherFile_View))
                    .ForMember(d => d.LabelingPackagingRejections, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingRejection_View))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackaging_ApprovedOrderDetail, DTO.ApprovedDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.HangTagFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangTagFileUrl) ? s.HangTagFileUrl : "no-image.jpg")))
                   .ForMember(d => d.BoxShippingMarkFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BoxShippingMarkFileUrl) ? s.BoxShippingMarkFileUrl : "no-image.jpg")))
                   .ForMember(d => d.BrassLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BrassLabelFileUrl) ? s.BrassLabelFileUrl : "no-image.jpg")))
                   .ForMember(d => d.AIFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIFileUrl) ? s.AIFileUrl : "no-image.jpg")))
                   .ForMember(d => d.WashCushionLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.WashCushionLabelFileUrl) ? s.WashCushionLabelFileUrl : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackaging_ApprovedOrderSparepartDetail, DTO.ApprovedSparepartDetail>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForMember(d => d.HangTagFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.HangTagFileUrl) ? s.HangTagFileUrl : "no-image.jpg")))
                  .ForMember(d => d.BoxShippingMarkFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BoxShippingMarkFileUrl) ? s.BoxShippingMarkFileUrl : "no-image.jpg")))
                  .ForMember(d => d.BrassLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.BrassLabelFileUrl) ? s.BrassLabelFileUrl : "no-image.jpg")))
                  .ForMember(d => d.AIFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIFileUrl) ? s.AIFileUrl : "no-image.jpg")))
                  .ForMember(d => d.WashCushionLabelFileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.WashCushionLabelFileUrl) ? s.WashCushionLabelFileUrl : "no-image.jpg")))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<LabelingPackagingMng_LabelingPackagingRejection_View, DTO.LabelingPackagingRejection>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LabelingPackagingRejectionFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.LabelingPackagingRejectionFileLocation) ? s.LabelingPackagingRejectionFileLocation : "no-image.jpg")))
                    .ForMember(d => d.LabelingPackagingRejectionThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.LabelingPackagingRejectionThumbnailLocation) ? s.LabelingPackagingRejectionThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackagingDetail, LabelingPackagingDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LabelingPackagingDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackagingSparepartDetail, LabelingPackagingSparepartDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LabelingPackagingSparepartDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackagingFileMonitor, LabelingPackagingFileMonitor>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LPFileMonitorID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackagingRemark, LabelingPackagingRemark>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingRemarkID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackagingOtherFile, LabelingPackagingOtherFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingOtherFileID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackagingRejection, LabelingPackagingRejection>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LabelingPackagingRejectionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackaging, LabelingPackaging>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingID, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingDetail, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingRemark, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingOtherFile, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingRejection, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.LabelingPackagingTemp, LabelingPackaging>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingID, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingDetail, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingSparepartDetail, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingRemark, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingOtherFile, o => o.Ignore())
                    .ForMember(d => d.LabelingPackagingRejection, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.LabelingPackagingSearch> DB2DTO_LabelingPackagingSearch(List<LabelingPackagingMng_LabelingPackaging_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LabelingPackagingMng_LabelingPackaging_SearchView>, List<DTO.LabelingPackagingSearch>>(dbItems);
        }

        public DTO.LabelingPackaging DB2DTO_LabelingPackaging(LabelingPackagingMng_LabelingPackaging_View dbItem)
        {
            return AutoMapper.Mapper.Map<LabelingPackagingMng_LabelingPackaging_View, DTO.LabelingPackaging>(dbItem);
        }

        public List<DTO.ApprovedDetail> DB2DTO_ApprovedDetailList(List<LabelingPackagingMng_LabelingPackaging_ApprovedOrderDetail> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LabelingPackagingMng_LabelingPackaging_ApprovedOrderDetail>, List<DTO.ApprovedDetail>>(dbItems);
        }

        public DTO.ApprovedDetail DB2DTO_ApprovedDetail(LabelingPackagingMng_LabelingPackaging_ApprovedOrderDetail dbItem)
        {
            return AutoMapper.Mapper.Map<LabelingPackagingMng_LabelingPackaging_ApprovedOrderDetail, DTO.ApprovedDetail>(dbItem);
        }

        public List<DTO.ApprovedSparepartDetail> DB2DTO_ApprovedSparepartDetailList(List<LabelingPackagingMng_LabelingPackaging_ApprovedOrderSparepartDetail> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LabelingPackagingMng_LabelingPackaging_ApprovedOrderSparepartDetail>, List<DTO.ApprovedSparepartDetail>>(dbItems);
        }

        public DTO.ApprovedSparepartDetail DB2DTO_ApprovedSparepartDetail(LabelingPackagingMng_LabelingPackaging_ApprovedOrderSparepartDetail dbItems)
        {
            return AutoMapper.Mapper.Map<LabelingPackagingMng_LabelingPackaging_ApprovedOrderSparepartDetail, DTO.ApprovedSparepartDetail>(dbItems);
        }

        public void DTO2DB_LabelingPackaging(DTO.LabelingPackaging dtoItem, ref LabelingPackaging dbItem)
        {
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
            dbItem.ApprovedDate = dtoItem.ApprovedDate.ConvertStringToDateTime();
            AutoMapper.Mapper.Map<DTO.LabelingPackaging, LabelingPackaging>(dtoItem, dbItem);

            // Remove labeling detail
            //foreach (var item in dbItem.LabelingPackagingDetail.ToArray())
            //{
            //    if (!dtoItem.LabelingPackagingDetails.Select(s => s.LabelingPackagingDetailID).Contains(item.LabelingPackagingDetailID))
            //    {
            //        dbItem.LabelingPackagingDetail.Remove(item);
            //    }
            //}

            // Remove labeling sparepart detail
            //foreach (var item in dbItem.LabelingPackagingSparepartDetail.ToArray())
            //{
            //    if (!dtoItem.LabelingPackagingSparepartDetails.Select(s => s.LabelingPackagingSparepartDetailID).Contains(item.LabelingPackagingSparepartDetailID))
            //    {
            //        dbItem.LabelingPackagingSparepartDetail.Remove(item);
            //    }
            //}

            // Remove labeling remark
            //foreach (var item in dbItem.LabelingPackagingRemark.ToArray())
            //{
            //    if (!dtoItem.LabelingPackagingRemarks.Select(s => s.LabelingPackagingRemarkID).Contains(item.LabelingPackagingRemarkID))
            //    {
            //        dbItem.LabelingPackagingRemark.Remove(item);
            //    }
            //}

            // Update child detail
            foreach (var item in dtoItem.LabelingPackagingDetails)
            {
                LabelingPackagingDetail dbDetail;
                if (item.LabelingPackagingDetailID < 0)
                {
                    dbDetail = new LabelingPackagingDetail();
                    dbItem.LabelingPackagingDetail.Add(dbDetail);
                }
                else
                {
                    dbDetail = dbItem.LabelingPackagingDetail.Where(o => o.LabelingPackagingDetailID == item.LabelingPackagingDetailID).FirstOrDefault();
                }
                if (dbDetail != null)
                {
                    AutoMapper.Mapper.Map<DTO.LabelingPackagingDetail, LabelingPackagingDetail>(item, dbDetail);
                }
            }

            //update file monitor
            foreach(var item in dtoItem.LabelingPackagingFileMonitors)
            {
                LabelingPackagingFileMonitor dbDetail;
                if(item.LPFileMonitorID < 0)
                {
                    dbDetail = new LabelingPackagingFileMonitor();
                    dbItem.LabelingPackagingFileMonitor.Add(dbDetail);
                }
                else
                {
                    dbDetail = dbItem.LabelingPackagingFileMonitor.Where(o => o.LPFileMonitorID == item.LPFileMonitorID).FirstOrDefault();
                }
                if (dbDetail != null)
                {
                    AutoMapper.Mapper.Map<DTO.LabelingPackagingFileMonitor, LabelingPackagingFileMonitor>(item, dbDetail);
                }
            }

            // Update child sparepart detail
            foreach (var item in dtoItem.LabelingPackagingSparepartDetails)
            {
                LabelingPackagingSparepartDetail dbSparepartDetail;
                if (item.LabelingPackagingSparepartDetailID < 0)
                {
                    dbSparepartDetail = new LabelingPackagingSparepartDetail();
                    dbItem.LabelingPackagingSparepartDetail.Add(dbSparepartDetail);
                }
                else
                {
                    dbSparepartDetail = dbItem.LabelingPackagingSparepartDetail.Where(o => o.LabelingPackagingSparepartDetailID == item.LabelingPackagingSparepartDetailID).FirstOrDefault();
                }
                if (dbSparepartDetail != null)
                {
                    AutoMapper.Mapper.Map<DTO.LabelingPackagingSparepartDetail, LabelingPackagingSparepartDetail>(item, dbSparepartDetail);
                }
            }

            if (dtoItem.LabelingPackagingOtherFiles != null)
            {
                // check for child rows deleted
                foreach (LabelingPackagingOtherFile dbOther in dbItem.LabelingPackagingOtherFile.ToArray())
                {
                    if (!dtoItem.LabelingPackagingOtherFiles.Select(o => o.LabelingPackagingOtherFileID).Contains(dbOther.LabelingPackagingOtherFileID))
                    {
                        dbItem.LabelingPackagingOtherFile.Remove(dbOther);
                    }
                }

                // Update other file
                foreach (var item in dtoItem.LabelingPackagingOtherFiles)
                {
                    LabelingPackagingOtherFile dbDetailOther;
                    if (item.LabelingPackagingOtherFileID < 0)
                    {
                        dbDetailOther = new LabelingPackagingOtherFile();
                        dbItem.LabelingPackagingOtherFile.Add(dbDetailOther);
                    }
                    else
                    {
                        dbDetailOther = dbItem.LabelingPackagingOtherFile.Where(o => o.LabelingPackagingOtherFileID == item.LabelingPackagingOtherFileID).FirstOrDefault();
                    }
                    if (dbDetailOther != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LabelingPackagingOtherFile, LabelingPackagingOtherFile>(item, dbDetailOther);
                        if (item.OtherFileHasChange || item.RemarkTextHasChange)
                        {
                            dbDetailOther.UpdatedBy = dtoItem.UpdatedBy;
                            dbDetailOther.UpdatedDate = DateTime.Now;
                        }
                    }
                }
            }

            if (dtoItem.LabelingPackagingOtherFiles != null)
            {
                // check for child rows deleted
                foreach (LabelingPackagingRemark dbRemark in dbItem.LabelingPackagingRemark.ToArray())
                {
                    if (!dtoItem.LabelingPackagingRemarks.Select(o => o.LabelingPackagingRemarkID).Contains(dbRemark.LabelingPackagingRemarkID))
                    {
                        dbItem.LabelingPackagingRemark.Remove(dbRemark);
                    }
                }

                // Update remark
                foreach (var item in dtoItem.LabelingPackagingRemarks)
                {
                    LabelingPackagingRemark dbDetail;
                    if (item.LabelingPackagingRemarkID < 0)
                    {
                        dbDetail = new LabelingPackagingRemark();
                        dbItem.LabelingPackagingRemark.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.LabelingPackagingRemark.Where(o => o.LabelingPackagingRemarkID == item.LabelingPackagingRemarkID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LabelingPackagingRemark, LabelingPackagingRemark>(item, dbDetail);
                        if (item.RemarkFileHasChange || item.RemarkTextHasChange)
                        {
                            dbDetail.UpdatedBy = dtoItem.UpdatedBy;
                            dbDetail.UpdatedDate = DateTime.Now;
                        }
                    }
                }
            }

            if (dtoItem.LabelingPackagingRejections != null)
            {
                // check for child rows deleted
                foreach (LabelingPackagingRejection dbRejection in dbItem.LabelingPackagingRejection.ToArray())
                {
                    if (!dtoItem.LabelingPackagingRejections.Select(o => o.LabelingPackagingRejectionID).Contains(dbRejection.LabelingPackagingRejectionID))
                    {
                        dbItem.LabelingPackagingRejection.Remove(dbRejection);
                    }
                }

                // Update rejection
                foreach (var item in dtoItem.LabelingPackagingRejections)
                {
                    LabelingPackagingRejection dbRejection;
                    if (item.LabelingPackagingRejectionID < 0)
                    {
                        dbRejection = new LabelingPackagingRejection();
                        dbItem.LabelingPackagingRejection.Add(dbRejection);
                    }
                    else
                    {
                        dbRejection = dbItem.LabelingPackagingRejection.Where(o => o.LabelingPackagingRejectionID == item.LabelingPackagingRejectionID).FirstOrDefault();
                    }

                    if (dbRejection != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LabelingPackagingRejection, LabelingPackagingRejection>(item, dbRejection);
                        //if (item.RemarkFileHasChange || item.RemarkTextHasChange)
                        //{
                        //    dbRejection.UpdatedBy = dtoItem.UpdatedBy;
                        //    dbRejection.UpdatedDate = DateTime.Now;
                        //}
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.LabelingPackaging, LabelingPackaging>(dtoItem, dbItem);
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.UpdatedDate = DateTime.Now;
        }
        public List<DTO.LabelingPackaging> DB2DTO_LabelingPackagingList(List<LabelingPackagingMng_LabelingPackaging_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LabelingPackagingMng_LabelingPackaging_View>, List<DTO.LabelingPackaging>>(dbItems);
        }

        public void DTO2DB_LabelingPackagingTemp(DTO.LabelingPackagingTemp dtoItem, ref LabelingPackaging dbItem)
        {
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
            dbItem.ApprovedDate = dtoItem.ApprovedDate.ConvertStringToDateTime();
            AutoMapper.Mapper.Map<DTO.LabelingPackagingTemp, LabelingPackaging>(dtoItem, dbItem);

            // Update child sparepart detail
            foreach (var item in dtoItem.LabelingPackagingSparepartDetails)
            {
                LabelingPackagingSparepartDetail dbSparepartDetail;
                if (item.LabelingPackagingSparepartDetailID < 0)
                {
                    dbSparepartDetail = new LabelingPackagingSparepartDetail();
                    dbItem.LabelingPackagingSparepartDetail.Add(dbSparepartDetail);
                }
                else
                {
                    dbSparepartDetail = dbItem.LabelingPackagingSparepartDetail.Where(o => o.LabelingPackagingSparepartDetailID == item.LabelingPackagingSparepartDetailID).FirstOrDefault();
                }
                if (dbSparepartDetail != null)
                {
                    AutoMapper.Mapper.Map<DTO.LabelingPackagingSparepartDetail, LabelingPackagingSparepartDetail>(item, dbSparepartDetail);
                }
            }

            if (dtoItem.LabelingPackagingOtherFiles != null)
            {
                // check for child rows deleted
                foreach (LabelingPackagingOtherFile dbOther in dbItem.LabelingPackagingOtherFile.ToArray())
                {
                    if (!dtoItem.LabelingPackagingOtherFiles.Select(o => o.LabelingPackagingOtherFileID).Contains(dbOther.LabelingPackagingOtherFileID))
                    {
                        dbItem.LabelingPackagingOtherFile.Remove(dbOther);
                    }
                }

                // Update other file
                foreach (var item in dtoItem.LabelingPackagingOtherFiles)
                {
                    LabelingPackagingOtherFile dbDetailOther;
                    if (item.LabelingPackagingOtherFileID < 0)
                    {
                        dbDetailOther = new LabelingPackagingOtherFile();
                        dbItem.LabelingPackagingOtherFile.Add(dbDetailOther);
                    }
                    else
                    {
                        dbDetailOther = dbItem.LabelingPackagingOtherFile.Where(o => o.LabelingPackagingOtherFileID == item.LabelingPackagingOtherFileID).FirstOrDefault();
                    }
                    if (dbDetailOther != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LabelingPackagingOtherFile, LabelingPackagingOtherFile>(item, dbDetailOther);
                        if (item.OtherFileHasChange || item.RemarkTextHasChange)
                        {
                            dbDetailOther.UpdatedBy = dtoItem.UpdatedBy;
                            dbDetailOther.UpdatedDate = DateTime.Now;
                        }
                    }
                }
            }

            if (dtoItem.LabelingPackagingOtherFiles != null)
            {
                // check for child rows deleted
                foreach (LabelingPackagingRemark dbRemark in dbItem.LabelingPackagingRemark.ToArray())
                {
                    if (!dtoItem.LabelingPackagingRemarks.Select(o => o.LabelingPackagingRemarkID).Contains(dbRemark.LabelingPackagingRemarkID))
                    {
                        dbItem.LabelingPackagingRemark.Remove(dbRemark);
                    }
                }

                // Update remark
                foreach (var item in dtoItem.LabelingPackagingRemarks)
                {
                    LabelingPackagingRemark dbDetail;
                    if (item.LabelingPackagingRemarkID < 0)
                    {
                        dbDetail = new LabelingPackagingRemark();
                        dbItem.LabelingPackagingRemark.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.LabelingPackagingRemark.Where(o => o.LabelingPackagingRemarkID == item.LabelingPackagingRemarkID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LabelingPackagingRemark, LabelingPackagingRemark>(item, dbDetail);
                        if (item.RemarkFileHasChange || item.RemarkTextHasChange)
                        {
                            dbDetail.UpdatedBy = dtoItem.UpdatedBy;
                            dbDetail.UpdatedDate = DateTime.Now;
                        }
                    }
                }
            }

            if (dtoItem.LabelingPackagingRejections != null)
            {
                // check for child rows deleted
                foreach (LabelingPackagingRejection dbRejection in dbItem.LabelingPackagingRejection.ToArray())
                {
                    if (!dtoItem.LabelingPackagingRejections.Select(o => o.LabelingPackagingRejectionID).Contains(dbRejection.LabelingPackagingRejectionID))
                    {
                        dbItem.LabelingPackagingRejection.Remove(dbRejection);
                    }
                }

                // Update rejection
                foreach (var item in dtoItem.LabelingPackagingRejections)
                {
                    LabelingPackagingRejection dbRejection;
                    if (item.LabelingPackagingRejectionID < 0)
                    {
                        dbRejection = new LabelingPackagingRejection();
                        dbItem.LabelingPackagingRejection.Add(dbRejection);
                    }
                    else
                    {
                        dbRejection = dbItem.LabelingPackagingRejection.Where(o => o.LabelingPackagingRejectionID == item.LabelingPackagingRejectionID).FirstOrDefault();
                    }

                    if (dbRejection != null)
                    {
                        AutoMapper.Mapper.Map<DTO.LabelingPackagingRejection, LabelingPackagingRejection>(item, dbRejection);
                        //if (item.RemarkFileHasChange || item.RemarkTextHasChange)
                        //{
                        //    dbRejection.UpdatedBy = dtoItem.UpdatedBy;
                        //    dbRejection.UpdatedDate = DateTime.Now;
                        //}
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.LabelingPackagingTemp, LabelingPackaging>(dtoItem, dbItem);
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.UpdatedDate = DateTime.Now;
        }

    }
}
