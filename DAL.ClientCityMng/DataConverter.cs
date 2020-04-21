using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;
using DAL.ClientCountryMng;

namespace DAL.ClientCityMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientCityMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ClientCityMng_ClientCitySearchResult_View, DTO.ClientCityMng.ClientCitySearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientCityMng_ClientCity_View, DTO.ClientCityMng.ClientCity>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientCityMng.ClientCity, ClientCity>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientCityID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ClientCityMng.ClientCitySearchResult> DB2DTO_ClientCitySearchResultList(List<ClientCityMng_ClientCitySearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientCityMng_ClientCitySearchResult_View>, List<DTO.ClientCityMng.ClientCitySearchResult>>(dbItems);
        }

        //public List<DTO.ClientCountryMng.ClientCountrySearchResult> DB2DTO_ClientSitySearchCountryList(List<DTO.ClientCountryMng.ClientCountrySearchResult> dbItems)
        //{ 
        //return AutoMapper.Mapper.Map<List<CL ClientCountryMng_ClientCountrySearchResult>, List(DTO.ClientCountryMng.ClientCountrySearchResult>>(dbItems); 
        //}

        public DTO.ClientCityMng.ClientCity DB2DTO_ClientCity(ClientCityMng_ClientCity_View dbItem)
        {
            DTO.ClientCityMng.ClientCity dtoItem = AutoMapper.Mapper.Map<ClientCityMng_ClientCity_View, DTO.ClientCityMng.ClientCity>(dbItem);

            if (dtoItem == null)
                return dtoItem;

            if (dtoItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dtoItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dtoItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dtoItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public void DTO2BD_ClientCity(DTO.ClientCityMng.ClientCity dtoItem, ref ClientCity dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ClientCityMng.ClientCity, ClientCity>(dtoItem, dbItem);
        }
    }
}
