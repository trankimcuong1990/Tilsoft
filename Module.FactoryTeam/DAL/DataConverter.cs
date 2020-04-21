using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.FactoryTeam.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryTeamMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<FactoryTeam, DTO.FactoryTeamDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryTeamStepDTOs, o => o.MapFrom(s => s.FactoryTeamStep))
                    .ForMember(d => d.FactoryMaterialGroupTeamDTOs, o => o.MapFrom(s => s.FactoryMaterialGroupTeam))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.FactoryTeamDTO, FactoryTeam>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FactoryTeamID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<FactoryTeamStep, DTO.FactoryTeamStepDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryMaterialGroupTeam, DTO.FactoryMaterialGroupTeamDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryTeamDTO> DB2DTO_FactoryTeamSearch(List<FactoryTeam> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryTeam>, List<DTO.FactoryTeamDTO>>(dbItems);
        }

        public DTO.FactoryTeamDTO DB2DTO_FactoryTeam(FactoryTeam dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryTeam, DTO.FactoryTeamDTO>(dbItem);
        }

        public void DTO2DB_FactoryTeam(DTO.FactoryTeamDTO dtoItem, ref FactoryTeam dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactoryTeamDTO, FactoryTeam>(dtoItem, dbItem);
        }

    }
}
