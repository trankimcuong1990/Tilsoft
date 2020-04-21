using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.NotificationMessageMng.DAL
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
                AutoMapper.Mapper.CreateMap<NotificationMessageMng_function_SearMessage_Result, DTO.NotificationMessageDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SyncedDate, o => o.ResolveUsing(s => s.SyncedDate.HasValue ? s.SyncedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.NotificationMessageDTO> DB2DTO_NotificationMessageDTO(List<NotificationMessageMng_function_SearMessage_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<NotificationMessageMng_function_SearMessage_Result>, List<DTO.NotificationMessageDTO>>(dbItems);
        }
    }
}
