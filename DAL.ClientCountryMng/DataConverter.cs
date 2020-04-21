using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ClientCountryMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ClientCountryMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ClientCountryMng_ClientCountrySearchResult_View, DTO.ClientCountryMng.ClientCountrySearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ClientCountryMng_ClientCountry_View, DTO.ClientCountryMng.ClientCountry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ClientCountryMng.ClientCountry, ClientCountry>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientCountryID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ClientCountryMng.ClientCountrySearchResult> DB2DTO_ClientCountrySearchResultList(List<ClientCountryMng_ClientCountrySearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ClientCountryMng_ClientCountrySearchResult_View>, List<DTO.ClientCountryMng.ClientCountrySearchResult>>(dbItems);
        }

        public DTO.ClientCountryMng.ClientCountry DB2DTO_ClientCountry(ClientCountryMng_ClientCountry_View dbItem)
        {
            DTO.ClientCountryMng.ClientCountry dtoItem = AutoMapper.Mapper.Map<ClientCountryMng_ClientCountry_View, DTO.ClientCountryMng.ClientCountry>(dbItem);

            if (dtoItem == null)
                return null;

            if (dtoItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dtoItem.CreatedDate.Value.ToString("dd/MM/yyyy");
            if (dtoItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dtoItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public void DTO2BD_ClientCountry(DTO.ClientCountryMng.ClientCountry dtoItem, ref ClientCountry dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ClientCountryMng.ClientCountry, ClientCountry>(dtoItem, dbItem);
        }
    }
}
