using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.AccPIOverviewRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "AccPIOverviewRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<AccPIOverviewRpt_HtmlOverview_View, DTO.AccPIOverviewRpt.HtmlOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DeliveryDate, o => o.ResolveUsing(s => s.DeliveryDate.HasValue ? s.DeliveryDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PIReceivedDate, o => o.ResolveUsing(s => s.PIReceivedDate.HasValue ? s.PIReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ReceivedDate, o => o.ResolveUsing(s => s.ReceivedDate.HasValue ? s.ReceivedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.AccPIOverviewRpt.HtmlOverview> DB2DTO_HtmlOverviewList(List<AccPIOverviewRpt_HtmlOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccPIOverviewRpt_HtmlOverview_View>, List<DTO.AccPIOverviewRpt.HtmlOverview>>(dbItems);
        }
    }
}
