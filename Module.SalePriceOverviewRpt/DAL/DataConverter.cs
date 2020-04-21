using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.SalePriceOverviewRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");

        public DataConverter()
        {
            string mapName = "SalePriceOverviewRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<SalePriceOverviewRpt_SalePriceOverview_View, DTO.SalePriceOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.SalePriceOverview> DB2DTO_SalePriceOverviewSearchResultList(List<SalePriceOverviewRpt_SalePriceOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SalePriceOverviewRpt_SalePriceOverview_View>, List<DTO.SalePriceOverview>>(dbItems);
        }
    }
}
