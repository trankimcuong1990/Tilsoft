using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockOverviewRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }
            AutoMapper.Mapper.CreateMap<StockOverviewRpt_StockOverview_View, DTO.StockDTO>()
                .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<SupportMng_ProductType_View, DTO.ProductType>()
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);
        }
        public List<DTO.StockDTO> DB2DTO_GetStock(List<StockOverviewRpt_StockOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<StockOverviewRpt_StockOverview_View>, List<DTO.StockDTO>>(dbItems);
        }
        public List<DTO.ProductType> DB2DTO_GetProductType(List<SupportMng_ProductType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductType_View>, List<DTO.ProductType>>(dbItems);
        }
    }
}
