using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.PaymentTermMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PaymentTermMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PaymentTermMng_PaymentTermSearchResult_View, DTO.PaymentTermMng.PaymentTermSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentTermMng_PaymentTerm_View, DTO.PaymentTermMng.PaymentTerm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PaymentTermMng.PaymentTerm, PaymentTerm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentTermID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.PaymentTermMng.PaymentTermSearchResult> DB2DTO_PaymentTermSearchResultList(List<PaymentTermMng_PaymentTermSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentTermMng_PaymentTermSearchResult_View>, List<DTO.PaymentTermMng.PaymentTermSearchResult>>(dbItems);
        }

        public DTO.PaymentTermMng.PaymentTerm DB2DTO_PaymentTerm(PaymentTermMng_PaymentTerm_View dbItem)
        {
            return AutoMapper.Mapper.Map<PaymentTermMng_PaymentTerm_View, DTO.PaymentTermMng.PaymentTerm>(dbItem);
        }

        public void DTO2BD_PaymentTerm(DTO.PaymentTermMng.PaymentTerm dtoItem, ref PaymentTerm dbItem)
        {
            AutoMapper.Mapper.Map<DTO.PaymentTermMng.PaymentTerm, PaymentTerm>(dtoItem, dbItem);
        }
    }
}
