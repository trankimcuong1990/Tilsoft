using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.PermissionOverviewRpt.DAL
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
                AutoMapper.Mapper.CreateMap<PermissionOverviewRpt_Module_View, DTO.Module>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.Module> DB2DTO_Module(List<PermissionOverviewRpt_Module_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PermissionOverviewRpt_Module_View>, List<DTO.Module>>(dbItem);
        }
    }
}
