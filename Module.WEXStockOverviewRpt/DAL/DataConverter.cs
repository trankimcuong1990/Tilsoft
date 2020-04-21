using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.WEXStockOverviewRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<WEXStockOverviewRpt_ProductSearchResult_View, DTO.ProductSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public List<DTO.ProductSearchResultDTO> DB2DTO_ProductSearchResultList(List<WEXStockOverviewRpt_ProductSearchResult_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WEXStockOverviewRpt_ProductSearchResult_View>, List<DTO.ProductSearchResultDTO>>(dbItem);
        }
    }
}
