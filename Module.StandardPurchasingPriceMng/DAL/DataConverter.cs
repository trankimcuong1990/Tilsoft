using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.StandardPurchasingPriceMng.DAL
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
                AutoMapper.Mapper.CreateMap<StandardPurchasingPriceMng_SearchResult_View, DTO.StandardPurchasingPriceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.StandardPurchasingPriceSearchResult> DB2DTO_StandardPurchasingPriceSearchResult(List<StandardPurchasingPriceMng_SearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<StandardPurchasingPriceMng_SearchResult_View>, List<DTO.StandardPurchasingPriceSearchResult>>(dbItems);
        }
    }
}
