using AutoMapper;
using FrameworkSetting;
using Library;
using Module.PriceQuotationMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceQuotationMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PriceQuotationMng";

            if (Setting.Maps.Contains(mapName))
            {
                return;
            }

            Mapper.CreateMap<PriceQuotationMng_PriceQuotationSearchResult_View, PriceQuotationSearchResultData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.PriceUpdatedDate, o => o.ResolveUsing(s => s.PriceUpdatedDate.HasValue ? s.PriceUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<QuotationOfferData, QuotationOffer>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QuotationOfferID, o => o.Ignore())
                .ForMember(d => d.QuotationOfferVersion, o => o.Ignore())
                .ForMember(d => d.QuotationOfferDirectionID, o => o.Ignore())
                .ForMember(d => d.QuotationOfferDate, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.QuotationOfferDetail, o => o.Ignore());

            AutoMapper.Mapper.CreateMap<PriceQuotationMng_HistoryPriceQuotation_View, HistoryPriceQuotationData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QuotationOfferDate, o => o.ResolveUsing(s => s.QuotationOfferDate.HasValue ? s.QuotationOfferDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<QuotationOfferDetailData, QuotationOfferDetail>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.QuotationOfferDetailID, o => o.Ignore());

            //Mapper.CreateMap<PriceQuotationMng_PriceDifference_View, PriceDifferenceData>()
            //    .IgnoreAllNonExisting()
            //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            //Mapper.CreateMap<SupportMng_QuotationOfferDirection_View, QuotationOfferDirectionData>()
            //    .IgnoreAllNonExisting()
            //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Setting.Maps.Add(mapName);
        }

        public List<PriceQuotationSearchResultData> DB2DTO_PriceQuotationSearchResult(List<PriceQuotationMng_PriceQuotationSearchResult_View> dbItem)
        {
            return Mapper.Map<List<PriceQuotationMng_PriceQuotationSearchResult_View>, List<PriceQuotationSearchResultData>>(dbItem);
        }

        public List<HistoryPriceQuotationData> DB2DTO_HistoryPriceQuotation(List<PriceQuotationMng_HistoryPriceQuotation_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PriceQuotationMng_HistoryPriceQuotation_View>, List<HistoryPriceQuotationData>>(dbItem);
        }

        //public List<PriceDifferenceData> ConvertDBToDTO_PriceDifference(List<PriceQuotationMng_PriceDifference_View> priceDifferenceView)
        //{
        //    return Mapper.Map<List<PriceQuotationMng_PriceDifference_View>, List<PriceDifferenceData>>(priceDifferenceView);
        //}

        //public List<QuotationOfferDirectionData> ConvertDBToDTO_QuotationOfferDirection(List<SupportMng_QuotationOfferDirection_View> quotationOfferDirectionView)
        //{
        //    return Mapper.Map<List<SupportMng_QuotationOfferDirection_View>, List<QuotationOfferDirectionData>>(quotationOfferDirectionView);
        //}

        public void ConverterDTOToDB_QuotationOffer(QuotationOfferData dtoItem, ref QuotationOffer dbItem)
        {
            if (dtoItem.QuotationOfferDetail != null && dtoItem.QuotationOfferDetail.Count > 0)
            {
                foreach (var quotationOfferDetailData in dtoItem.QuotationOfferDetail)
                {
                    QuotationOfferDetail quotationOfferDetail = new QuotationOfferDetail();
                    dbItem.QuotationOfferDetail.Add(quotationOfferDetail);

                    if (quotationOfferDetail != null)
                    {
                        Mapper.Map<QuotationOfferDetailData, QuotationOfferDetail>(quotationOfferDetailData, quotationOfferDetail);
                    }
                }
            }

            Mapper.Map<QuotationOfferData, QuotationOffer>(dtoItem, dbItem);
        }
    }
}
