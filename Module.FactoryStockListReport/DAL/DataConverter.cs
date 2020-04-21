using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryStockListReport.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryStockListReportMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryStockListReportMng_FactoryStockList_View, DTO.FactoryStockList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ThumbnailLocation) ? "" : FrameworkSetting.Setting.MediaThumbnailUrl + s.ThumbnailLocation)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
               
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryStockList> DB2DTO_FactoryStockList(List<FactoryStockListReportMng_FactoryStockList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryStockListReportMng_FactoryStockList_View>, List<DTO.FactoryStockList>>(dbItems);
        }

        
    }
}
