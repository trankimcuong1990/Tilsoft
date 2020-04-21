using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.ClientConditionRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientConditionRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ClientConditionRpt_CheckListHtml_View, DTO.CheckList>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CheckList> DB2DTO_CheckList(List<ClientConditionRpt_CheckListHtml_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientConditionRpt_CheckListHtml_View>, List<DTO.CheckList>>(dbItems);
        }
    }
}
