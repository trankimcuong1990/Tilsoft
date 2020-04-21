using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.SystemSettingMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SystemSettingMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<SystemSettingMng_SystemSettingSearchResult_View, DTO.SystemSettingMng.SystemSettingSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FW_SystemSetting_View, DTO.SystemSettingMng.SystemSetting>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SystemSettingMng.SystemSetting, SystemSetting>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SystemSettingID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SystemSettingMng.SystemSettingSearchResult> DB2DTO_SystemSettingSearchResultList(List<SystemSettingMng_SystemSettingSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SystemSettingMng_SystemSettingSearchResult_View>, List<DTO.SystemSettingMng.SystemSettingSearchResult>>(dbItems);
        }

        public DTO.SystemSettingMng.SystemSetting DB2DTO_SystemSetting(FW_SystemSetting_View dbItem)
        {
            return AutoMapper.Mapper.Map<FW_SystemSetting_View, DTO.SystemSettingMng.SystemSetting>(dbItem);
        }

        public void DTO2BD_SystemSetting(DTO.SystemSettingMng.SystemSetting dtoItem, ref SystemSetting dbItem)
        {
            AutoMapper.Mapper.Map<DTO.SystemSettingMng.SystemSetting, SystemSetting>(dtoItem, dbItem);
        }
    }
}
