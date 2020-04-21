using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.BreakdownPriceListMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DB 2 DTO
                //
                AutoMapper.Mapper.CreateMap<BreakdownPriceListMng_BreakdownPriceListSearch_View, DTO.BreakdownPriceListSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ThumbnailLocation) ? "" : FrameworkSetting.Setting.MediaThumbnailUrl + s.ThumbnailLocation)))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileLocation) ? "" : FrameworkSetting.Setting.MediaFullSizeUrl + s.FileLocation)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.BreakdownPriceListSearchDTO> DB2DTO_Search(List<BreakdownPriceListMng_BreakdownPriceListSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<BreakdownPriceListMng_BreakdownPriceListSearch_View>, List<DTO.BreakdownPriceListSearchDTO>>(dbItems);
        }

    }
}
