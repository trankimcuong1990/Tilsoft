using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SystemSetting2Mng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<SystemSetting2Mng_SystemSetting_View, DTO.SystemSetting2>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.SystemSetting2, SystemSetting>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.SystemSettingID, o => o.Ignore())
                .ForMember(d => d.SettingKey, o => o.Ignore())
                .ForMember(d => d.SettingValue, o => o.Ignore())
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.SystemSetting2> DB2DTO_SystemSettingSearchResult(List<SystemSetting2Mng_SystemSetting_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SystemSetting2Mng_SystemSetting_View>, List<DTO.SystemSetting2>>(dbItem);
        }

        public DTO.SystemSetting2 DB2DTO_SystemSetting(SystemSetting2Mng_SystemSetting_View dbItem)
        {
            return AutoMapper.Mapper.Map<SystemSetting2Mng_SystemSetting_View, DTO.SystemSetting2>(dbItem);
        }

        public void DTO2DB_UpdateData(DTO.SystemSetting2 dtoItem, ref SystemSetting dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SystemSetting2, SystemSetting>(dtoItem, dbItem);
        }
    }
}
