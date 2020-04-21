using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PODMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PODMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PODMng_PODSearchResult_View, DTO.PODSearchResult>()
                        .IgnoreAllNonExisting()
                        .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PODMng_POD_View, DTO.POD>()
                        .IgnoreAllNonExisting()
                        .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.POD, POD>()
                        .IgnoreAllNonExisting()
                        .ForMember(d => d.PoDID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<LIst_ClientCountry_View, DTO.ListClientCountryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                FrameworkSetting.Setting.Maps.Add(mapName);

                AutoMapper.Mapper.CreateMap<DTO.ListClientCountryDTO, LIst_ClientCountry_View>()
                       .IgnoreAllNonExisting()
                       .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public List<DTO.PODSearchResult> DB2DTO_PODSearchResultList(List<PODMng_PODSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PODMng_PODSearchResult_View>, List<DTO.PODSearchResult>>(dbItems);
        }

        public DTO.POD DB2DTO_POD(PODMng_POD_View dbItem)
        {
            return AutoMapper.Mapper.Map<PODMng_POD_View, DTO.POD>(dbItem);
        }

        public void DB2DTO_POD(DTO.POD dtoItem, ref POD dbItem)
        {
            AutoMapper.Mapper.Map<DTO.POD, POD>(dtoItem, dbItem);
        }
        public List<DTO.ListClientCountryDTO> DB2DTO_ListClientCountry(List<LIst_ClientCountry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<LIst_ClientCountry_View>, List<DTO.ListClientCountryDTO>>(dbItems);
        }
    }
}
