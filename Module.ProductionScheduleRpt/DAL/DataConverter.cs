using AutoMapper;
using FrameworkSetting;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionScheduleRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ProductionScheduleRpt";

            if (Setting.Maps.Contains(mapName))
            {
                return;
            }

            Mapper.CreateMap<ProductionScheduleRpt_ProductionSchedule_View, DTO.ProductionSchedule>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
          
            Setting.Maps.Add(mapName);
        }

        public List<DTO.ProductionSchedule> DB2DTO_ProductionSchedule(List<ProductionScheduleRpt_ProductionSchedule_View> dbItem)
        {
            return Mapper.Map<List<ProductionScheduleRpt_ProductionSchedule_View>, List<DTO.ProductionSchedule>>(dbItem);
        }
    }
}
