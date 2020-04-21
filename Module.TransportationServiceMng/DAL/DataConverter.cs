using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportationServiceMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "TransportationServiceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<TransportationServiceMng_TransportationServiceSearchResult_View, DTO.TransportationServiceSearchResultData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportationServiceMng_TransportationService_View, DTO.TransportationServiceData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.TransportationServiceData, TransportationService>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TransportationServiceID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.TransportationServiceSearchResultData> DB2DTO_TransportationServiceResultList(List<TransportationServiceMng_TransportationServiceSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<TransportationServiceMng_TransportationServiceSearchResult_View>, List<DTO.TransportationServiceSearchResultData>>(dbItems);
        }

        public DTO.TransportationServiceData DB2DTO_TransportationService(TransportationServiceMng_TransportationService_View dbItem)
        {
            return AutoMapper.Mapper.Map<TransportationServiceMng_TransportationService_View, DTO.TransportationServiceData>(dbItem);
        }

        public void DTO2BD_TransportationService(DTO.TransportationServiceData dtoItem, ref TransportationService dbItem)
        {
            AutoMapper.Mapper.Map<DTO.TransportationServiceData, TransportationService>(dtoItem, dbItem);
        }
    }
}
