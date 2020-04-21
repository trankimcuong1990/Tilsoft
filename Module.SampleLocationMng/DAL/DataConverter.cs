using AutoMapper;
using FrameworkSetting;
using Library;
using Module.SampleLocationMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleLocationMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SampleLocationMng";

            if (Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<SampleProductLocationMng_SampleProductLocationSearchResult_View, SampleProductLocationSearchResultData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SampleProductLocationMng_SampleProduct_View, DTO.SampleProductItemData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.LocationDate, o => o.ResolveUsing(s => s.LocationDate.HasValue ? s.LocationDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                .ForMember(d => d.SampleProductLocations, o => o.MapFrom(s => s.SampleProductLocationMng_SampleProductLocation_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SampleProductLocationMng_SampleProductLocation_View, DTO.SampleProductLocationData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.LocationDate, o => o.ResolveUsing(s => s.LocationDate.HasValue ? s.LocationDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : string.Empty));

            AutoMapper.Mapper.CreateMap<SupportMng_SampleOtherLocation_View, SupportSampleOtherLocationData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<SampleProductLocationMng_SupportFactoryLocation_View, SupportFactoryLocationData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public void ConvertToDB_SampleProductLocation(SampleProductLocationData sampleProductLocationData, ref SampleProductLocation sampleProductLocation)
        {
            Mapper.Map<SampleProductLocationData, SampleProductLocation>(sampleProductLocationData, sampleProductLocation);
        }

        public List<SupportSampleOtherLocationData> DB2DTO_SampleOtheLocation(List<SupportMng_SampleOtherLocation_View> supportSampleOtherLocations)
        {
            return Mapper.Map<List<SupportMng_SampleOtherLocation_View>, List<SupportSampleOtherLocationData>>(supportSampleOtherLocations);
        }

        public List<SupportFactoryLocationData> DB2DTO_SampleFactory(List<SampleProductLocationMng_SupportFactoryLocation_View> supportFactoryLocation)
        {
            return Mapper.Map<List<SampleProductLocationMng_SupportFactoryLocation_View>, List<SupportFactoryLocationData>>(supportFactoryLocation);
        }

        public List<SampleProductLocationSearchResultData> DB2DTO_SampleLocationSearch(List<SampleProductLocationMng_SampleProductLocationSearchResult_View> sampleProductLocationSearchResults)
        {
            return Mapper.Map<List<SampleProductLocationMng_SampleProductLocationSearchResult_View>, List<SampleProductLocationSearchResultData>>(sampleProductLocationSearchResults);
        }

        public List<DTO.SampleProductItemData> DB2DTO_SampleProduct(List<SampleProductLocationMng_SampleProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SampleProductLocationMng_SampleProduct_View>, List<DTO.SampleProductItemData>>(dbItems);
        }
    }
}
