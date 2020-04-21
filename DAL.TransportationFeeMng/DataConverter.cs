using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.TransportationFeeMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "TransportationFeeMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<TransportationFeeMng_TransportationFeeSearchResult_View, DTO.TransportationFeeMng.TransportationFeeSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<TransportationFeeMng_TransportationFee_View, DTO.TransportationFeeMng.TransportationFee>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.TransportationFeeMng.TransportationFee,SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.TransportationFeeMng.TransportationFeeSearchResult> DB2DTO_TransportationFeeSearchResultList(List<TransportationFeeMng_TransportationFeeSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<TransportationFeeMng_TransportationFeeSearchResult_View>, List<DTO.TransportationFeeMng.TransportationFeeSearchResult>>(dbItems);
        }

        public DTO.TransportationFeeMng.TransportationFee DB2DTO_TransportationFee(TransportationFeeMng_TransportationFee_View dbItem)
        {
            return AutoMapper.Mapper.Map<TransportationFeeMng_TransportationFee_View, DTO.TransportationFeeMng.TransportationFee>(dbItem);
        }

        public void DTO2BD_TransportationFee(DTO.TransportationFeeMng.TransportationFee dtoItem, ref SaleOrder dbItem)
        {
            AutoMapper.Mapper.Map<DTO.TransportationFeeMng.TransportationFee, SaleOrder>(dtoItem, dbItem);
        }
    }
}
