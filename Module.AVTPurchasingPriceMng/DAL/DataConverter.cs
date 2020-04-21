using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.AVTPurchasingPriceMng.DAL
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
                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_FactoryQuotationSearchResult_View, DTO.PurchasingPriceHistoryDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_FactoryQuotationSearchResult_View, DTO.FactoryQuotationSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.PriceUpdatedDate, o => o.ResolveUsing(s => s.PriceUpdatedDate.HasValue ? s.PriceUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_OfferHistory_View, DTO.OfferHistoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_QuotationStatus_View, DTO.QuotationStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_RelatedQuotationDetail_View, DTO.PurchasingPriceHistoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_GeneralBreakDown_View, DTO.GeneralBreakDownDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_PALBreakDown_View, DTO.PALBreakDownDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_BreakdownCategoryOption_View, DTO.AVTBreakdownCategoryOptionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_BreakdownCategory_View, DTO.AVTSupportBreakdownCategoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_Breakdown_View, DTO.AVTSeasonSpecPercentDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AVTPurchasingPriceMng_BreakdownManagementFee_View, DTO.AVTBreakdownManagementFeeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.FactoryQuotationSearchResultDTO> DB2DTO_FactoryQuotationSearchResult(List<AVTPurchasingPriceMng_FactoryQuotationSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_FactoryQuotationSearchResult_View>, List<DTO.FactoryQuotationSearchResultDTO>>(dbItems);
        }

        public List<DTO.OfferHistoryDTO> DB2DTO_OfferHistory(List<AVTPurchasingPriceMng_OfferHistory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_OfferHistory_View>, List<DTO.OfferHistoryDTO>>(dbItems);
        }

        public List<DTO.QuotationStatusDTO> DB2DTO_QuotationStatus(List<SupportMng_QuotationStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_QuotationStatus_View>, List<DTO.QuotationStatusDTO>>(dbItems);
        }

        public List<DTO.PurchasingPriceHistoryDTO> DB2DTO_PurchasingPriceHistory(List<AVTPurchasingPriceMng_RelatedQuotationDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_RelatedQuotationDetail_View>, List<DTO.PurchasingPriceHistoryDTO>>(dbItems);
        }

        public List<DTO.GeneralBreakDownDTO> DB2DTO_GeneralBreakDown(List<AVTPurchasingPriceMng_GeneralBreakDown_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_GeneralBreakDown_View>, List<DTO.GeneralBreakDownDTO>>(dbItems);
        }

        public List<DTO.PALBreakDownDTO> DB2DTO_PALBreakDown(List<AVTPurchasingPriceMng_PALBreakDown_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_PALBreakDown_View>, List<DTO.PALBreakDownDTO>>(dbItems);
        }

        public List<DTO.AVTSupportBreakdownCategoryDTO> DB2DTO_AVTSupportBreakdownCategory(List<AVTPurchasingPriceMng_BreakdownCategory_View> dbitems)
        {
            return AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_BreakdownCategory_View>, List<DTO.AVTSupportBreakdownCategoryDTO>>(dbitems);
        }
    }
}
