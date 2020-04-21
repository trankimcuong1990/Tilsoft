using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;

namespace Module.FactoryQuotationMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "FactoryQuotationMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<FactoryQuotationMng_QuotationSearchResult_View, DTO.QuotationSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryQuotationMng_QuotationDetailSearchResult_View, DTO.QuotationDetailSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryQuotationMng_Quotation_View, DTO.Quotation>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(d => d.QuotationDetails, o => o.MapFrom(s => s.FactoryQuotationMng_QuotationDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryQuotationMng_QuotationDetail_View, DTO.QuotationDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.QuotationOfferDetails, o => o.MapFrom(s => s.FactoryQuotationMng_QuotationOfferDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryQuotationMng_QuotationOfferDetail_View, DTO.QuotationOfferDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.QuotationSearchResult> DB2DTO_QuotationSearchResultList(List<FactoryQuotationMng_QuotationSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryQuotationMng_QuotationSearchResult_View>, List<DTO.QuotationSearchResult>>(dbItems);
        }

        public List<DTO.QuotationDetailSearchResult> DB2DTO_QuotationDetailSearchResultList(List<FactoryQuotationMng_QuotationDetailSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryQuotationMng_QuotationDetailSearchResult_View>, List<DTO.QuotationDetailSearchResult>>(dbItems);
        }

        public DTO.Quotation DB2DTO_Quotation(FactoryQuotationMng_Quotation_View dbItem)
        {
            return AutoMapper.Mapper.Map<FactoryQuotationMng_Quotation_View, DTO.Quotation>(dbItem);
        }
    }
}
