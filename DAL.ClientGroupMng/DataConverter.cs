using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ClientGroupMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientGroupMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ClientGroupMng_ClientGroupSearchResult_View, DTO.ClientGroupMng.ClientGroupSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientGroupMng_ClientGroup_View, DTO.ClientGroupMng.ClientGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientGroupMng.ClientGroup, ClientGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientGroupID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ClientGroupMng.ClientGroupSearchResult> DB2DTO_ClientGroupSearchResultList(List<ClientGroupMng_ClientGroupSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientGroupMng_ClientGroupSearchResult_View>, List<DTO.ClientGroupMng.ClientGroupSearchResult>>(dbItems);
        }

        public DTO.ClientGroupMng.ClientGroup DB2DTO_ClientGroup(ClientGroupMng_ClientGroup_View dbItem)
        {
            return AutoMapper.Mapper.Map<ClientGroupMng_ClientGroup_View, DTO.ClientGroupMng.ClientGroup>(dbItem);
        }

        public void DTO2BD_ClientGroup(DTO.ClientGroupMng.ClientGroup dtoItem, ref ClientGroup dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ClientGroupMng.ClientGroup, ClientGroup>(dtoItem, dbItem);
        }
    }
}
