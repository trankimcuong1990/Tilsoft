using Library;
using System.Collections.Generic;

namespace Module.OrderedItemOverviewRpt.DAL
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
            AutoMapper.Mapper.CreateMap<OrderedItemOverview_ReportView, DTO.OrderedItemOverviewReportViewDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<OrderedOverviewRpt_Client_View, DTO.OrderedOverviewClientViewDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }
        public List<DTO.OrderedItemOverviewReportViewDTO> DB2DTO_OrderedItemOverviewReportView(List<OrderedItemOverview_ReportView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OrderedItemOverview_ReportView>, List<DTO.OrderedItemOverviewReportViewDTO>>(dbItems);
        }

        public List<DTO.OrderedOverviewClientViewDTO> DB2DTO_OrderedOverviewClientView(List<OrderedOverviewRpt_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OrderedOverviewRpt_Client_View>, List<DTO.OrderedOverviewClientViewDTO>>(dbItems);
        }
    }
}