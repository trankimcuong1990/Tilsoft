using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.EurofarPurchasingPriceMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_function_SearchFactoryQuotation_Result, DTO.FactoryQuotationSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yy HH:mm") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yy") : ""))
                    .ForMember(d => d.SoonestShippedDate, o => o.ResolveUsing(s => s.SoonestShippedDate.HasValue ? s.SoonestShippedDate.Value.ToString("dd/MM/yy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    //.ForMember(d => d.PurchasingPricePreviousSeason, o => o.MapFrom(s => s.PurchasingPriceLastSeason))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion_Result, DTO.WaitForFactoryConclusionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_function_GetWaitForFactoryProductionConclusion_Result, DTO.WaitForFactoryConclusionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest_Result, DTO.EmailAddressReceivePriceRequestDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_SimilarItem_View, DTO.FactoryQuotationSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_function_GetAdditionalInfo_Result, DTO.FactoryQuotationSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.IsAdditionalDataLoaded, o => o.UseValue(true))
                    .ForMember(d => d.SoonestShippedDate, o => o.ResolveUsing(s => s.SoonestShippedDate.HasValue ? s.SoonestShippedDate.Value.ToString("dd/MM/yy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_OfferHistory_View, DTO.OfferHistoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_QuotationStatus_View, DTO.QuotationStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_OrderType_View, DTO.OrderTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_RelatedQuotationDetail_View, DTO.PurchasingPriceHistoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_GeneralBreakDown_View, DTO.GeneralBreakDownDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_PALBreakDown_View, DTO.PALBreakDownDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation_Result, DTO.SubTotalDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.IsAdditionalDataLoaded, o => o.UseValue(true))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_BreakdownCategoryOption_View, DTO.EurofarBreakdownCategoryOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_BreakdownCategory_View, DTO.EurofarBreakdownCategoryDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_BreakdownManagementFee_View, DTO.EurofarBreakdownManagementFeeDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_Breakdown_View, DTO.EurofarSeasonSpecPercentDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarPurchasingPriceMng_function_GetPricingTeamMemberInCharge_Result, DTO.PricingTeamMemberInChargeDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        //public List<DTO.FactoryQuotationSearchResultDTO> DB2DTO_FactoryQuotationSearchResult(List<EurofarPurchasingPriceMng_function_SearchFactoryQuotation_Result> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_function_SearchFactoryQuotation_Result>, List<DTO.FactoryQuotationSearchResultDTO>>(dbItems);
        //}

        public List<DTO.PricingTeamMemberInChargeDTO> DB2DTO_PricingTeamMemberInChargeDTO(List<EurofarPurchasingPriceMng_function_GetPricingTeamMemberInCharge_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_function_GetPricingTeamMemberInCharge_Result>, List<DTO.PricingTeamMemberInChargeDTO>>(dbItems);
        }

        public List<DTO.FactoryQuotationSearchResultDTO> DB2DTO_FactoryQuotationSearchResult(List<EurofarPurchasingPriceMng_function_SearchFactoryQuotation_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_function_SearchFactoryQuotation_Result>, List<DTO.FactoryQuotationSearchResultDTO>>(dbItems);
        }

        public List<DTO.EmailAddressReceivePriceRequestDTO> DB2DTO_EmailAddressReceivePriceRequestDTO(List<EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest_Result>, List<DTO.EmailAddressReceivePriceRequestDTO>>(dbItems);
        }

        public List<DTO.WaitForFactoryConclusionDTO> DB2DTO_WaitForFactoryConclusionDTO(List<EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion_Result>, List<DTO.WaitForFactoryConclusionDTO>>(dbItems);
        }

        public List<DTO.WaitForFactoryConclusionDTO> DB2DTO_WaitForFactoryConclusionDTO(List<EurofarPurchasingPriceMng_function_GetWaitForFactoryProductionConclusion_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_function_GetWaitForFactoryProductionConclusion_Result>, List<DTO.WaitForFactoryConclusionDTO>>(dbItems);
        }

        public List<DTO.FactoryQuotationSearchResultDTO> DB2DTO_SimilarItem(List<EurofarPurchasingPriceMng_SimilarItem_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_SimilarItem_View>, List<DTO.FactoryQuotationSearchResultDTO>>(dbItems);
        }

        public List<DTO.FactoryQuotationSearchResultDTO> DB2DTO_FactoryQuotationSearchResultAdditional(List<EurofarPurchasingPriceMng_function_GetAdditionalInfo_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_function_GetAdditionalInfo_Result>, List<DTO.FactoryQuotationSearchResultDTO>>(dbItems);
        }

        public List<DTO.OfferHistoryDTO> DB2DTO_OfferHistory(List<EurofarPurchasingPriceMng_OfferHistory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_OfferHistory_View>, List<DTO.OfferHistoryDTO>>(dbItems);
        }

        public List<DTO.QuotationStatusDTO> DB2DTO_QuotationStatus(List<SupportMng_QuotationStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_QuotationStatus_View>, List<DTO.QuotationStatusDTO>>(dbItems);
        }

        public List<DTO.OrderTypeDTO> DB2DTO_OrderType(List<SupportMng_OrderType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_OrderType_View>, List<DTO.OrderTypeDTO>>(dbItems);
        }

        public List<DTO.PurchasingPriceHistoryDTO> DB2DTO_PurchasingPriceHistory(List<EurofarPurchasingPriceMng_RelatedQuotationDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_RelatedQuotationDetail_View>, List<DTO.PurchasingPriceHistoryDTO>>(dbItems);
        }

        public List<DTO.GeneralBreakDownDTO> DB2DTO_GeneralBreakDown(List<EurofarPurchasingPriceMng_GeneralBreakDown_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_GeneralBreakDown_View>, List<DTO.GeneralBreakDownDTO>>(dbItems);
        }

        public List<DTO.PALBreakDownDTO> DB2DTO_PALBreakDown(List<EurofarPurchasingPriceMng_PALBreakDown_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_PALBreakDown_View>, List<DTO.PALBreakDownDTO>>(dbItems);
        }

        public DTO.SubTotalDTO DB2DTO_SubTotalDTO(EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation_Result dbItem)
        {
            return AutoMapper.Mapper.Map<EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation_Result, DTO.SubTotalDTO>(dbItem);
        }

        public List<DTO.EurofarBreakdownCategoryDTO> DB2DTO_BreakdownCategory(List<EurofarPurchasingPriceMng_BreakdownCategory_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<EurofarPurchasingPriceMng_BreakdownCategory_View>, List<DTO.EurofarBreakdownCategoryDTO>>(dbItem);
        }
    }
}
