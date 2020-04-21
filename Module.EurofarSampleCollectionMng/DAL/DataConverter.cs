using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarSampleCollectionMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "EurofarSampleCollectionMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<EurofarSampleCollectionMng_SampleProduct_View, DTO.EurofarSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.EurofarSampleCollectionDate, o => o.ResolveUsing(s => s.EurofarSampleCollectionDate.HasValue ? s.EurofarSampleCollectionDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishedImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FinishedImageThumbnailLocation) ? s.FinishedImageThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.EurofarSearchDTO> DB2DTO_EurofarSearchResultList(List<EurofarSampleCollectionMng_SampleProduct_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarSampleCollectionMng_SampleProduct_View>, List<DTO.EurofarSearchDTO>>(dbItems);
        }
    }
}
