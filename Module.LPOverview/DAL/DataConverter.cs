using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;

namespace Module.LPOverview.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;
        public DataConverter()
        {
            string mapName = "LPOverview";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<LPOverview_LabelingPackaging_SearchView, DTO.LPOverviewSearch>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
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
                    .ForMember(d => d.AIOHangTagFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOHangTagFile_FileUrl) ? s.AIOHangTagFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOBoxShippingMarkFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOBoxShippingMarkFile_FileUrl) ? s.AIOBoxShippingMarkFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOBrassLabelFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOBrassLabelFile_FileUrl) ? s.AIOBrassLabelFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOAIFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOAIFile_FileUrl) ? s.AIOAIFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOWashCushionLabelFile_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOWashCushionLabelFile_FileUrl) ? s.AIOWashCushionLabelFile_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOOption1File_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOOption1File_FileUrl) ? s.AIOOption1File_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOOption2File_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOOption2File_FileUrl) ? s.AIOOption2File_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.AIOOption3File_FileUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.AIOOption3File_FileUrl) ? s.AIOOption3File_FileUrl : "no-image.jpg")))
                    .ForMember(d => d.LabelingPackagingDetails, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingDetail_View))
                    .ForMember(d => d.LabelingPackagingSparepartDetails, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingSparepartDetail_View))
                    .ForMember(d => d.LabelingPackagingRemarks, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingRemark_View))
                    .ForMember(d => d.LabelingPackagingOtherFiles, o => o.MapFrom(s => s.LabelingPackagingMng_LabelingPackagingOtherFile_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.LPOverviewSearch> DB2DTO_LPOverviewSearch(List<LPOverview_LabelingPackaging_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LPOverview_LabelingPackaging_SearchView>, List<DTO.LPOverviewSearch>>(dbItems);
        }
        public DTO.LabelingPackaging DB2DTO_LabelingPackaging(LabelingPackagingMng_LabelingPackaging_View dbItem)
        {
            return AutoMapper.Mapper.Map<LabelingPackagingMng_LabelingPackaging_View, DTO.LabelingPackaging>(dbItem);
        }
    }
}
