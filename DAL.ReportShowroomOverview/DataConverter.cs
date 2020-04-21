using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ReportShowroomOverview
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ReportShowroomOverviewMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ReportMng_ShowroomOverview_ShowroomArea_View, DTO.ReportShowroomOverview.ShowroomArea>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                AutoMapper.Mapper.CreateMap<ReportMng_ShowroomOverview_View, DTO.ReportShowroomOverview.ShowroomOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ShowroomItemThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomItemThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ShowroomItemThumbnailImage)))
                    .ForMember(d => d.ShowroomItemFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomItemFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ShowroomItemFullImage)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReportMng_ShowroomOverview_ShowroomAreaImage_View, DTO.ReportShowroomOverview.ShowroomAreaImage>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ShowroomAreaThumbnailImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomAreaThumbnailImage) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ShowroomAreaThumbnailImage)))
                   .ForMember(d => d.ShowroomAreaFullImage, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ShowroomAreaFullImage) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ShowroomAreaFullImage)))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ReportShowroomOverview.ShowroomOverview, DTO.ReportShowroomOverview.ShowroomOverview>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ReportShowroomOverview.ShowroomAreaImage, DTO.ReportShowroomOverview.ShowroomAreaImage>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ReportShowroomOverview.ShowroomArea> DB2DTO_ShowroomArea(List<ReportMng_ShowroomOverview_ShowroomArea_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_ShowroomOverview_ShowroomArea_View>, List<DTO.ReportShowroomOverview.ShowroomArea>>(dbItems);
        }

        public List<DTO.ReportShowroomOverview.ShowroomOverview> DB2DTO_ShowroomOverview(List<ReportMng_ShowroomOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_ShowroomOverview_View>, List<DTO.ReportShowroomOverview.ShowroomOverview>>(dbItems);
        }

        public List<DTO.ReportShowroomOverview.ShowroomAreaImage> DB2DTO_ShowroomAreaImage(List<ReportMng_ShowroomOverview_ShowroomAreaImage_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReportMng_ShowroomOverview_ShowroomAreaImage_View>, List<DTO.ReportShowroomOverview.ShowroomAreaImage>>(dbItems);
        }

        public List<DTO.ReportShowroomOverview.ShowroomOverview> DTO2DTO_ShowroomOverview(List<DTO.ReportShowroomOverview.ShowroomOverview> dtoOverview)
        {
            return AutoMapper.Mapper.Map<List<DTO.ReportShowroomOverview.ShowroomOverview>, List<DTO.ReportShowroomOverview.ShowroomOverview>>(dtoOverview);
        }

        public List<DTO.ReportShowroomOverview.ShowroomAreaImage> DTO2DTO_ShowroomAreaImage(List<DTO.ReportShowroomOverview.ShowroomAreaImage> dtoOverview)
        {
            return AutoMapper.Mapper.Map<List<DTO.ReportShowroomOverview.ShowroomAreaImage>, List<DTO.ReportShowroomOverview.ShowroomAreaImage>>(dtoOverview);
        }


    }
}
