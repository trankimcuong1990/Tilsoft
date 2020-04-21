using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.Framework.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<FW_SystemSetting_View, DTO.SystemSettingDTO>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.SystemSettingDTO, SystemSetting>()
                    .IgnoreAllNonExisting()
                    //.ForMember(d => d.UpdatedBy, o => o.Ignore())
                    //.ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.SystemSettingID, o => o.Ignore())
                    .ForMember(d => d.SettingKey, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SystemSettingDTO> DB2DTO_SystemSettingDTO(List<FW_SystemSetting_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FW_SystemSetting_View>, List<DTO.SystemSettingDTO>>(dbItems);
        }

        public void DTO2DB_SystemSettingDTO(DTO.SystemSettingDTO dtoItem, ref SystemSetting dbItem, int userId)
        {
            // map systemsetting
            AutoMapper.Mapper.Map<DTO.SystemSettingDTO, SystemSetting>(dtoItem, dbItem);
            //dbItem.UpdatedBy = userId;
            //dbItem.UpdatedDate = System.DateTime.Now;
        }
    }
}
