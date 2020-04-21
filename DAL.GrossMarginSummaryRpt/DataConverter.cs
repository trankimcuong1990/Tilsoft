using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.GrossMarginSummaryRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "GrossMarginSummaryRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<GrossMarginSummaryRpt_GrossMarginSummaryHtml_View, DTO.GrossMarginSummaryRpt.HtmlGrossMarginSummary>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.GrossMarginSummaryRpt.HtmlGrossMarginSummary> DB2DTO_HtmlGrossMarginSummaryList(List<GrossMarginSummaryRpt_GrossMarginSummaryHtml_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<GrossMarginSummaryRpt_GrossMarginSummaryHtml_View>, List<DTO.GrossMarginSummaryRpt.HtmlGrossMarginSummary>>(dbItems);
        }
    }
}
