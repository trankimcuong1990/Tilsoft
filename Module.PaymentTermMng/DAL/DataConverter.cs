using DTO.Support;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentTermMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PaymentTermMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PaymentTermMng_PaymentTermSearchResult_View, DTO.PaymentTermSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PaymentTermMng_PaymentTerm_View, DTO.PaymentTerm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.PaymentTerm, PaymentTerm>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PaymentTermID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<List_PaymentMethod, PaymentMethod>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<List_PaymentType, PaymentType>()

                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.PaymentTermSearchResult> DB2DTO_PaymentTermSearchResultList(List<PaymentTermMng_PaymentTermSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PaymentTermMng_PaymentTermSearchResult_View>, List<DTO.PaymentTermSearchResult>>(dbItems);
        }

        public DTO.PaymentTerm DB2DTO_PaymentTerm(PaymentTermMng_PaymentTerm_View dbItem)
        {
            return AutoMapper.Mapper.Map<PaymentTermMng_PaymentTerm_View, DTO.PaymentTerm>(dbItem);
        }

        public void DTO2BD_PaymentTerm(DTO.PaymentTerm dtoItem, ref PaymentTerm dbItem)
        {
            AutoMapper.Mapper.Map<DTO.PaymentTerm, PaymentTerm>(dtoItem, dbItem);
        }

        public List<PaymentMethod> DB2DTO_PaymentMethod(List<List_PaymentMethod> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PaymentMethod>, List<PaymentMethod>>(dbItems);
        }
        public List<PaymentType> DB2DTO_PaymentType(List<List_PaymentType> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PaymentType>, List<PaymentType>>(dbItems);
        }
    }
}
