using AutoMapper;
using FrameworkSetting;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterProductionScheduleRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MasterProductionScheduleRpt";

            if (Setting.Maps.Contains(mapName))
            {
                return;
            }

            Mapper.CreateMap<MasterProductionScheduleRpt_MasterProductionSchedule_View, DTO.MasterProductionSchedule>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
          
            Setting.Maps.Add(mapName);
        }

        public List<DTO.MasterProductionSchedule> DB2DTO_MasterProductionSchedule(List<MasterProductionScheduleRpt_MasterProductionSchedule_View> dbItem)
        {
            return Mapper.Map<List<MasterProductionScheduleRpt_MasterProductionSchedule_View>, List<DTO.MasterProductionSchedule>>(dbItem);
        }
    }
}
