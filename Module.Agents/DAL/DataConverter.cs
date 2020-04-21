using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.Agents.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "AgentsMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<Agents, DTO.AgentsDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgentMng_Agents_View, DTO.AgentsDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgentMng_ClientCity_View, DTO.ClientCitiesDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgentMng_ClientCountry_View, DTO.ClientCountryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AgentsMng_AmountByClientAndSeason_View, DTO.AmountByClientAndSeason>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //DTO2DB
                AutoMapper.Mapper.CreateMap<DTO.AgentsDTO, Agents>()
                    .IgnoreAllNonExisting()
                .ForMember(d => d.AgentID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.AgentsDTO> DB2DTO_AgentsSearch(List<AgentMng_Agents_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AgentMng_Agents_View>, List<DTO.AgentsDTO>>(dbItems);
        }

        public DTO.AgentsDTO DB2DTO_Agents(AgentMng_Agents_View dbItem)
        {
            DTO.AgentsDTO Agents = AutoMapper.Mapper.Map<AgentMng_Agents_View, DTO.AgentsDTO>(dbItem);
            return Agents;
        }

        public void DTO2DB_Agents(DTO.AgentsDTO dtoItem, ref Agents dbItem)
        {
            AutoMapper.Mapper.Map<DTO.AgentsDTO, Agents>(dtoItem, dbItem);
        }

        public List<DTO.ClientCitiesDTO> DB2DTO_Clientcity(List<AgentMng_ClientCity_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AgentMng_ClientCity_View>, List<DTO.ClientCitiesDTO>>(dbItems);
        }

        public List<DTO.ClientCountryDTO> DB2DTO_ClientCountry(List<AgentMng_ClientCountry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AgentMng_ClientCountry_View>, List<DTO.ClientCountryDTO>>(dbItems);
        }

        public List<DTO.AmountByClientAndSeason> DB2DTO_Amount(List<AgentsMng_AmountByClientAndSeason_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AgentsMng_AmountByClientAndSeason_View>, List<DTO.AmountByClientAndSeason>>(dbItems);
        }

    }
}
