using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.POLMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "POLMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<POLMng_POLSearchResult_View, DTO.POLSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_POL_View, DTO.POL>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.POL, POL>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PoLID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.POLSearchResult> DB2DTO_POLSearchResultList(List<POLMng_POLSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<POLMng_POLSearchResult_View>, List<DTO.POLSearchResult>>(dbItems);
        }

        public DTO.POL DB2DTO_POL(List_POL_View dbItem)
        {
            return AutoMapper.Mapper.Map<List_POL_View, DTO.POL>(dbItem);
        }

        public void DTO2BD_POL(DTO.POL dtoItem, ref POL dbItem)
        {
            AutoMapper.Mapper.Map<DTO.POL, POL>(dtoItem, dbItem);
        }


    }
}
