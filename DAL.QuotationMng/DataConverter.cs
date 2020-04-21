using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.QuotationMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "QuotationMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<QuotationMng_QuotationSearchResult_View, DTO.QuotationMng.QuotationSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QuotationMng_Quotation_View, DTO.QuotationMng.Quotation>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.QuotationDetails, o => o.MapFrom(s => s.QuotationMng_QuotationDetail_View))
                    .ForMember(d => d.QuotationOffers, o => o.MapFrom(s => s.QuotationMng_QuotationOffer_View))
                    .ForMember(d => d.ConcurrencyFlag_String, o => o.MapFrom(s => Convert.ToBase64String(s.ConcurrencyFlag)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QuotationMng_QuotationDetail_View, DTO.QuotationMng.QuotationDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : string.Empty))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QuotationMng_QuotationOffer_View, DTO.QuotationMng.QuotationOffer>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QuotationOfferDate, o => o.ResolveUsing(s => s.QuotationOfferDate.HasValue ? s.QuotationOfferDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.QuotationOfferDetails, o => o.MapFrom(s => s.QuotationMng_QuotationOfferDetail_View))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QuotationMng_QuotationOfferDetail_View, DTO.QuotationMng.QuotationOfferDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QuotationMng_FactoryOrderSearchResult_View, DTO.QuotationMng.FactoryOrderSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<QuotationMng_FactoryOrderDetailSearchResult_View, DTO.QuotationMng.FactoryOrderDetailSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QuotationMng.Quotation, Quotation>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QuotationDetail, o => o.Ignore())
                    .ForMember(d => d.QuotationOffer, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.QuotationID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QuotationMng.QuotationDetail, QuotationDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QuotationID, o => o.Ignore())
                    .ForMember(d => d.StatusUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.QuotationDetailID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QuotationMng.QuotationOffer, QuotationOffer>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QuotationID, o => o.Ignore())
                    .ForMember(d => d.QuotationOfferID, o => o.Ignore())
                    .ForMember(d => d.QuotationOfferDate, o => o.Ignore())
                    .ForMember(d => d.QuotationOfferDetail, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.QuotationMng.QuotationOfferDetail, QuotationOfferDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.QuotationOfferID, o => o.Ignore())
                    .ForMember(d => d.QuotationOfferDetailID, o => o.Ignore())
                    .ForMember(d => d.QuotationDetailID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.QuotationMng.QuotationSearchResult> DB2DTO_QuotationSearchResultList(List<QuotationMng_QuotationSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QuotationMng_QuotationSearchResult_View>, List<DTO.QuotationMng.QuotationSearchResult>>(dbItems);
        }

        public DTO.QuotationMng.Quotation DB2DTO_Quotation(QuotationMng_Quotation_View dbItem)
        {
            return AutoMapper.Mapper.Map<QuotationMng_Quotation_View, DTO.QuotationMng.Quotation>(dbItem);
        }

        public List<DTO.QuotationMng.FactoryOrderSearchResult> DB2DTO_FactoryOrder(List<QuotationMng_FactoryOrderSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<QuotationMng_FactoryOrderSearchResult_View>, List<DTO.QuotationMng.FactoryOrderSearchResult>>(dbItems);
        }

        public void DTO2DB(DTO.QuotationMng.Quotation dtoItem, ref Quotation dbItem)
        {
            DateTime tmpDate;
            System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");

            // map fields
            AutoMapper.Mapper.Map<DTO.QuotationMng.Quotation, Quotation>(dtoItem, dbItem);
            dbItem.UpdatedBy = dtoItem.UpdatedBy;
            dbItem.UpdatedDate = DateTime.Now;

            // map load quotation detail
            if (dtoItem.QuotationDetails != null)
            {
                // check for child rows deleted
                foreach (QuotationDetail dbDetail in dbItem.QuotationDetail.ToArray())
                {
                    if (!dtoItem.QuotationDetails.Select(o => o.QuotationDetailID).Contains(dbDetail.QuotationDetailID))
                    {
                        dbItem.QuotationDetail.Remove(dbDetail);
                    }
                }

                // map child rows
                foreach (DTO.QuotationMng.QuotationDetail dtoDetail in dtoItem.QuotationDetails)
                {
                    QuotationDetail dbDetail;
                    if (dtoDetail.QuotationDetailID <= 0)
                    {
                        dbDetail = new QuotationDetail();
                        dbItem.QuotationDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == dtoDetail.QuotationDetailID);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.QuotationMng.QuotationDetail, QuotationDetail>(dtoDetail, dbDetail);
                        if (DateTime.TryParse(dtoDetail.StatusUpdatedDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbDetail.StatusUpdatedDate = tmpDate;
                        }

                        // work around for quotation offer detail
                        dtoDetail.DBObject = dbDetail;
                    }
                }
            }

            // map load quotation offer
            if (dtoItem.QuotationOffers != null)
            {
                // check for child rows deleted
                foreach (QuotationOffer dbOffer in dbItem.QuotationOffer.ToArray())
                {
                    if (!dtoItem.QuotationOffers.Select(o => o.QuotationOfferID).Contains(dbOffer.QuotationOfferID))
                    {
                        dbItem.QuotationOffer.Remove(dbOffer);
                    }
                }

                // map child rows
                foreach (DTO.QuotationMng.QuotationOffer dtoOffer in dtoItem.QuotationOffers)
                {
                    QuotationOffer dbOffer;
                    if (dtoOffer.QuotationOfferID <= 0)
                    {
                        dbOffer = new QuotationOffer();
                        dbItem.QuotationOffer.Add(dbOffer);
                    }
                    else
                    {
                        dbOffer = dbItem.QuotationOffer.FirstOrDefault(o => o.QuotationOfferID == dtoOffer.QuotationOfferID);
                    }

                    if (dbOffer != null)
                    {
                        AutoMapper.Mapper.Map<DTO.QuotationMng.QuotationOffer, QuotationOffer>(dtoOffer, dbOffer);
                        if (DateTime.TryParse(dtoOffer.QuotationOfferDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbOffer.QuotationOfferDate = tmpDate;
                        }
                        if (DateTime.TryParse(dtoOffer.UpdatedDate, cInfo, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbOffer.UpdatedDate = tmpDate;
                        }
                    }

                    // map quotation offer detail
                    if (dtoOffer.QuotationOfferDetails != null)
                    {
                        // check for child rows deleted
                        foreach (QuotationOfferDetail dbOfferDetail in dbOffer.QuotationOfferDetail.ToArray())
                        {
                            if (!dtoOffer.QuotationOfferDetails.Select(o => o.QuotationOfferDetailID).Contains(dbOfferDetail.QuotationOfferDetailID))
                            {
                                dbOffer.QuotationOfferDetail.Remove(dbOfferDetail);
                            }
                        }

                        // map child rows
                        QuotationDetail dbQuotationDetail = null;
                        foreach (DTO.QuotationMng.QuotationOfferDetail dtoOfferDetail in dtoOffer.QuotationOfferDetails)
                        {
                            QuotationOfferDetail dbOfferDetail;
                            if (dtoOfferDetail.QuotationOfferDetailID <= 0)
                            {
                                dbOfferDetail = new QuotationOfferDetail();
                                dbOffer.QuotationOfferDetail.Add(dbOfferDetail);
                                dbQuotationDetail = null;
                                if (dtoItem.QuotationDetails.FirstOrDefault(o => o.QuotationDetailID == dtoOfferDetail.QuotationDetailID) != null)
                                {
                                    dbQuotationDetail = (QuotationDetail)dtoItem.QuotationDetails.FirstOrDefault(o => o.QuotationDetailID == dtoOfferDetail.QuotationDetailID).DBObject;
                                    dbQuotationDetail.QuotationOfferDetail.Add(dbOfferDetail);
                                    //((QuotationDetail)dtoItem.QuotationDetails.FirstOrDefault(o => o.QuotationDetailID == dtoOfferDetail.QuotationDetailID).DBObject).QuotationOfferDetail.Add(dbOfferDetail);
                                }
                            }
                            else
                            {
                                dbOfferDetail = dbOffer.QuotationOfferDetail.FirstOrDefault(o => o.QuotationOfferDetailID == dtoOfferDetail.QuotationOfferDetailID);
                            }

                            if (dbOfferDetail != null)
                            {
                                AutoMapper.Mapper.Map<DTO.QuotationMng.QuotationOfferDetail, QuotationOfferDetail>(dtoOfferDetail, dbOfferDetail);
                                if (dbQuotationDetail != null)
                                {
                                    dbQuotationDetail.PriceUpdatedDate = dbOffer.UpdatedDate;
                                    dbQuotationDetail.PriceUpdatedBy = dbOffer.UpdatedBy;
                                    dbQuotationDetail.LastOfferDirectionID = dbOffer.QuotationOfferDirectionID;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
