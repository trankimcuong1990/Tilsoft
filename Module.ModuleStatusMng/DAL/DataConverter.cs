using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using Module.ModuleStatusMng.DTO;

namespace Module.ModuleStatusMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                Mapper.CreateMap<ModuleStatusMng_ModuleStatusSearch_View, ModuleStatusSearchDTO>()
                    .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(o => !o.IsSourceValueNull));

                Mapper.CreateMap<ModuleStatusMng_ModuleStatus_View, ModuleStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ModuleStatusMng_Module_View, ModuleDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ModuleStatusDTO, ModuleStatus>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ModuleStatusID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<ModuleStatusSearchDTO> DB2DTO_Search(List<ModuleStatusMng_ModuleStatusSearch_View> dbItem)
        {
            return Mapper.Map<List<ModuleStatusMng_ModuleStatusSearch_View>, List<ModuleStatusSearchDTO>>(dbItem);
        }

        public DTO.ModuleStatusDTO DB2DTO_ModuleStatus(ModuleStatusMng_ModuleStatus_View dbItem)
        {
            return Mapper.Map<ModuleStatusMng_ModuleStatus_View, DTO.ModuleStatusDTO>(dbItem);
        }

        public void DTO2DB_Update(DTO.ModuleStatusDTO dataItem, ref ModuleStatus dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ModuleStatusDTO, ModuleStatus>(dataItem, dbItem);
        }

        public List<ModuleDTO> DB2DTO_Modules(List<ModuleStatusMng_Module_View> dbItem)
        {
            return Mapper.Map<List<ModuleStatusMng_Module_View>, List<ModuleDTO>>(dbItem);
        }
    }
}
