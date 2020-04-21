using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.PurchasingPriceOverviewRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = "PurchasingPriceOverviewRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PurchasingPriceOverviewRpt_PurchasingPriceOverview_View, DTO.PurchasingPriceOverview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.PurchasingPriceOverview> DB2DTO_PurchasingPriceOverviewSearchResultList(List<PurchasingPriceOverviewRpt_PurchasingPriceOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchasingPriceOverviewRpt_PurchasingPriceOverview_View>, List<DTO.PurchasingPriceOverview>>(dbItems);
        }
    }
}
