using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.FactoryQuotation2Mng.DAL
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
                AutoMapper.Mapper.CreateMap<FactoryQuotation2Mng_FactoryQuotationSearchResult_View, DTO.FactoryQuotationSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yy HH:mm") : ""))
                    .ForMember(d => d.RequestedDate, o => o.ResolveUsing(s => s.RequestedDate.HasValue ? s.RequestedDate.Value.ToString("dd/MM/yy HH:mm") : ""))
                    .ForMember(d => d.QuotedDeadline, o => o.ResolveUsing(s => s.QuotedDeadline.HasValue ? s.QuotedDeadline.Value.ToString("dd/MM/yy HH:mm") : ""))
                    //.ForMember(d => d.PriceUpdatedDate, o => o.ResolveUsing(s => s.PriceUpdatedDate.HasValue ? s.PriceUpdatedDate.Value.ToString("dd/MM/yy HH:mm") : ""))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yy HH:mm") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryQuotation2Mng_SimilarItem_View, DTO.FactoryQuotationSearchResultDTO>()
                    .IgnoreAllNonExisting()                    
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryQuotation2Mng_OfferHistory_View, DTO.OfferHistoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yy HH:mm") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_QuotationStatus_View, DTO.QuotationStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<FactoryQuotation2Mng_GetWaitForFactoryConclusion_View, DTO.WaitForFactoryConclusionDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View, DTO.WaitForFactoryConclusionDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryQuotation2Mng_ClientAdditionalCondition_View, DTO.ClientAdditionalConditionDTO>()
                 .IgnoreAllNonExisting()
                 .ForMember(d => d.AttachFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.AttachFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.AttachFileURL)))
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.WaitForFactoryConclusionDTO> DB2DTO_WaitForFactoryConclusionDTO(List<FactoryQuotation2Mng_GetWaitForFactoryConclusion_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryQuotation2Mng_GetWaitForFactoryConclusion_View>, List<DTO.WaitForFactoryConclusionDTO>>(dbItems);
        }

        public List<DTO.WaitForFactoryConclusionDTO> DB2DTO_WaitForFactoryProductionConclusionDTO(List<FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryQuotation2Mng_GetWaitForFactoryProductionConclusion_View>, List<DTO.WaitForFactoryConclusionDTO>>(dbItems);
        }
        public List<DTO.FactoryQuotationSearchResultDTO> DB2DTO_FactoryQuotationSearchResult(List<FactoryQuotation2Mng_FactoryQuotationSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryQuotation2Mng_FactoryQuotationSearchResult_View>, List<DTO.FactoryQuotationSearchResultDTO>>(dbItems);
        }
        public List<DTO.FactoryQuotationSearchResultDTO> DB2DTO_SimilarItems(List<FactoryQuotation2Mng_SimilarItem_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryQuotation2Mng_SimilarItem_View>, List<DTO.FactoryQuotationSearchResultDTO>>(dbItems);
        }
        public List<DTO.OfferHistoryDTO> DB2DTO_OfferHistory(List<FactoryQuotation2Mng_OfferHistory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryQuotation2Mng_OfferHistory_View>, List<DTO.OfferHistoryDTO>>(dbItems);
        }

        public List<DTO.ClientAdditionalConditionDTO> DB2DTO_ACData(List<FactoryQuotation2Mng_ClientAdditionalCondition_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryQuotation2Mng_ClientAdditionalCondition_View>, List<DTO.ClientAdditionalConditionDTO>>(dbItems);
        }

        public List<DTO.QuotationStatusDTO> DB2DTO_QuotationStatus(List<SupportMng_QuotationStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_QuotationStatus_View>, List<DTO.QuotationStatusDTO>>(dbItems);
        }
    }
}
