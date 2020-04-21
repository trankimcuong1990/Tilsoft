using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.DevRequestOverviewRpt.DAL
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
                AutoMapper.Mapper.CreateMap<DevRequestOverviewRpt_DetailByPerson_View, DTO.DetailByPerson>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestOverviewRpt_Overview_View, DTO.Overview>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestOverviewRpt_PlaningByPerson_View, DTO.PlaningByPerson>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DevRequestOverviewRpt_ResolvedRequestByPerson_View, DTO.ResolvedRequestByPerson>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DetailByPerson> DB2DTO_DetailByPerson(List<DevRequestOverviewRpt_DetailByPerson_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevRequestOverviewRpt_DetailByPerson_View>, List<DTO.DetailByPerson>>(dbItems);
        }

        public List<DTO.Overview> DB2DTO_Overview(List<DevRequestOverviewRpt_Overview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevRequestOverviewRpt_Overview_View>, List<DTO.Overview>>(dbItems);
        }

        public List<DTO.PlaningByPerson> DB2DTO_PlaningByPerson(List<DevRequestOverviewRpt_PlaningByPerson_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevRequestOverviewRpt_PlaningByPerson_View>, List<DTO.PlaningByPerson>>(dbItems);
        }

        public List<DTO.ResolvedRequestByPerson> DB2DTO_ResolvedRequestByPerson(List<DevRequestOverviewRpt_ResolvedRequestByPerson_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DevRequestOverviewRpt_ResolvedRequestByPerson_View>, List<DTO.ResolvedRequestByPerson>>(dbItems);
        }
    }
}
