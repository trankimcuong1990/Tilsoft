using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.DevelopmentItemsRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if(!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DevelopmentItemsRpt_DevelopmentItems_SearchView, DTO.DevelopmentItemSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FinishedImageThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.FinishedImageThumbnailLocation) ? s.FinishedImageThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FinishedImageFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FinishedImageFileLocation) ? s.FinishedImageFileLocation : "no-image.jpg")))
                    .ForAllMembers(o => o.Condition(s => !s.IsSourceValueNull));

            }
        }

        public List<DTO.DevelopmentItemSearchResult> DB2DTO_SearchResult(List<DevelopmentItemsRpt_DevelopmentItems_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevelopmentItemsRpt_DevelopmentItems_SearchView>, List<DTO.DevelopmentItemSearchResult>>(dbItems);
        }
    }
}
