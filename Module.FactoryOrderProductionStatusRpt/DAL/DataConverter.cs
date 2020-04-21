using AutoMapper;
using FrameworkSetting;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryOrderProductionStatusRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryOrderProductionStatusRpt";

            if (Setting.Maps.Contains(mapName))
            {
                return;
            }

            Mapper.CreateMap<FactoryOrderProductionStatusRpt_FactoryOrderProductionStatus_View, DTO.FactoryOrderProductionStatus>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
          
            Setting.Maps.Add(mapName);
        }

        public List<DTO.FactoryOrderProductionStatus> DB2DTO_FactoryOrderProductionStatus(List<FactoryOrderProductionStatusRpt_FactoryOrderProductionStatus_View> dbItem)
        {
            return Mapper.Map<List<FactoryOrderProductionStatusRpt_FactoryOrderProductionStatus_View>, List<DTO.FactoryOrderProductionStatus>>(dbItem);
        }
    }
}
