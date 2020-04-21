using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.ClientRelationshipTypeMng.DAL
{
   public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
                return;

            AutoMapper.Mapper.CreateMap<ClientRelationshipTypeMng_ClientRelationshipTypeSearch_View, DTO.ClientRelationshipTypeSearchDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<ClientRelationshipTypeMng_ClientRelationshipType_View, DTO.ClientRelationshipTypeDto>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<DTO.ClientRelationshipTypeDto, ClientRelationshipType>()
                .ForMember(d => d.ClientRelationshipTypeID, o => o.Ignore())
                .IgnoreAllNonExisting();

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.ClientRelationshipTypeSearchDto> DB2DTO_ClientRelationshipSearchResult(List<ClientRelationshipTypeMng_ClientRelationshipTypeSearch_View> items)
        {
            return AutoMapper.Mapper.Map<List<ClientRelationshipTypeMng_ClientRelationshipTypeSearch_View>, List<DTO.ClientRelationshipTypeSearchDto>>(items);
        }

        public DTO.ClientRelationshipTypeDto DB2DTO_ClientRelationshipType(ClientRelationshipTypeMng_ClientRelationshipType_View item)
        {
            return AutoMapper.Mapper.Map<ClientRelationshipTypeMng_ClientRelationshipType_View, DTO.ClientRelationshipTypeDto>(item);
        }

        public void DTO2DB_ClientRelationshipTypeMng(DTO.ClientRelationshipTypeDto dto, ref ClientRelationshipType db)
        {
            AutoMapper.Mapper.Map<DTO.ClientRelationshipTypeDto, ClientRelationshipType>(dto, db);
        }
    }
}
