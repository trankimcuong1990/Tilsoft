using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.POLMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "POLMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<POLMng_POLSearchResult_View, DTO.POLMng.POLSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_POL_View, DTO.POLMng.POL>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.POLMng.POL, POL>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PoLID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.POLMng.POLSearchResult> DB2DTO_POLSearchResultList(List<POLMng_POLSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<POLMng_POLSearchResult_View>, List<DTO.POLMng.POLSearchResult>>(dbItems);
        }

        public DTO.POLMng.POL DB2DTO_POL(List_POL_View dbItem)
        {
            return AutoMapper.Mapper.Map<List_POL_View, DTO.POLMng.POL>(dbItem);
        }

        public void DTO2BD_POL(DTO.POLMng.POL dtoItem, ref POL dbItem)
        {
            AutoMapper.Mapper.Map<DTO.POLMng.POL, POL>(dtoItem, dbItem);
        }


    }
}
