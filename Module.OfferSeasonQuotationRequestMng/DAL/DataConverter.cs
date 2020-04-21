using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.OfferSeasonQuotationRequestMng.DAL
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
                AutoMapper.Mapper.CreateMap<OfferSeasonQuotatonRequestMng_OfferSeasonQuotationRequestSearchResult_View, DTO.OfferSeasonQuotationRequestSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonQuotatonRequestMng_FactoryQuotationSearchResult_View, DTO.FactoryQuotationSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonQuotatonRequestMng_PreferedFactory_View, DTO.PreferedFactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferSeasonQuotatonRequestMng_Factory_View, DTO.FactoryDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.OfferSeasonQuotationRequestSearchResultDTO> DB2DTO_OfferSeasonQuotationRequestSearchResult(List<OfferSeasonQuotatonRequestMng_OfferSeasonQuotationRequestSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonQuotatonRequestMng_OfferSeasonQuotationRequestSearchResult_View>, List<DTO.OfferSeasonQuotationRequestSearchResultDTO>>(dbItems);
        }

        public List<DTO.FactoryQuotationSearchResultDTO> DB2DTO_FactoryQuotationSearchResult(List<OfferSeasonQuotatonRequestMng_FactoryQuotationSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonQuotatonRequestMng_FactoryQuotationSearchResult_View>, List<DTO.FactoryQuotationSearchResultDTO>>(dbItems);
        }

        public List<DTO.PreferedFactoryDTO> DB2DTO_PreferedFactory(List<OfferSeasonQuotatonRequestMng_PreferedFactory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonQuotatonRequestMng_PreferedFactory_View>, List<DTO.PreferedFactoryDTO>>(dbItems);
        }

        //public List<DTO.OfferSeasonQuotationRequestDetailDTO> DB2DTO_OfferSeasonQuotationRequestDetail(List<OfferSeasonQuotatonRequestMng_OfferSeasonQuotationRequestDetail_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<OfferSeasonQuotatonRequestMng_OfferSeasonQuotationRequestDetail_View>, List<DTO.OfferSeasonQuotationRequestDetailDTO>>(dbItems);
        //}

        public List<DTO.FactoryDTO> DB2DTO_Factory(List<OfferSeasonQuotatonRequestMng_Factory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferSeasonQuotatonRequestMng_Factory_View>, List<DTO.FactoryDTO>>(dbItems);
        }

        public void DTO2DB(List<DTO.OfferSeasonQuotationRequestDetailDTO> dtoItems, ref OfferSeasonQuotationRequest dbItem, int userId)
        {
            throw new NotImplementedException();
            //string TmpFile = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";

            //foreach (OfferSeasonQuotationRequestDetail dbDetail in dbItem.OfferSeasonQuotationRequestDetail.ToArray())
            //{
            //    if (!dtoItems.Select(o => o.OfferSeasonQuotationRequestDetailID).Contains(dbDetail.OfferSeasonQuotationRequestDetailID))
            //    {
            //        dbItem.OfferSeasonQuotationRequestDetail.Remove(dbDetail);
            //    }
            //}
            //foreach (DTO.OfferSeasonQuotationRequestDetailDTO dtoDetail in dtoItems)
            //{
            //    OfferSeasonQuotationRequestDetail dbDetail;
            //    if (dtoDetail.OfferSeasonQuotationRequestDetailID <= 0)
            //    {
            //        dbDetail = new OfferSeasonQuotationRequestDetail();
            //        dbItem.OfferSeasonQuotationRequestDetail.Add(dbDetail);
            //    }
            //    else
            //    {
            //        dbDetail = dbItem.OfferSeasonQuotationRequestDetail.FirstOrDefault(o => o.OfferSeasonQuotationRequestDetailID == dtoDetail.OfferSeasonQuotationRequestDetailID);
            //    }

            //    if (dbDetail != null)
            //    {
            //        dbDetail.UpdatedDate = DateTime.Now;
            //        dbDetail.UpdatedBy = userId;
            //        AutoMapper.Mapper.Map<DTO.OfferSeasonQuotationRequestDetailDTO, OfferSeasonQuotationRequestDetail>(dtoDetail, dbDetail);
            //    }
            //}
        }
    }
}
