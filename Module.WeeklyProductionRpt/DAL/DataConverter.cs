using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.WeeklyProductionRpt.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<WeeklyProductionRpt_FrameOverview_View, DTO.FrameOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WeekStart, o => o.ResolveUsing(s => s.WeekStart.HasValue ? s.WeekStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WeekEnd, o => o.ResolveUsing(s => s.WeekEnd.HasValue ? s.WeekEnd.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FrameOverview> DB2DTO_Search(List<WeeklyProductionRpt_FrameOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WeeklyProductionRpt_FrameOverview_View>, List<DTO.FrameOverview>>(dbItems);
        }

        
    }
}
