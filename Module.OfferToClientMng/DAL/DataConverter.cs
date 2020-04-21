using AutoMapper;
using Library;
using Module.OfferToClientMng.DTO;
using System;
using System.Collections.Generic;


using System.Linq;


namespace Module.OfferToClientMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<OfferToClientMng_ClientEstimatedAdditionalCost_View, ClientEstimatedAdditionalCostDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferToClientMng_OfferLine_View, OfferLineDTO>()
                  .IgnoreAllNonExisting()                
                  .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.ResolveUsing(s => s.PlaningPurchasingPriceSelectedDate.HasValue ? s.PlaningPurchasingPriceSelectedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                  .ForMember(d => d.ProductThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ProductThumbnailLocation) ? s.ProductThumbnailLocation : "no-image.jpg")))
                  .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductFileLocation) ? s.ProductFileLocation : "no-image.jpg")))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferToClientMng_OfferLineSparepart_View, OfferLineSparepartDTO> ()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferToClientMng_Client_View, ClientDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferToClientMng_function_getExchangeRate_Result, ExchangeRateDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<OfferToClientMng_PaymentTerm, PaymentTermDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<OfferToClientMng_InterestPercent_View, InterestPercentDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));                
                AutoMapper.Mapper.CreateMap<OfferToClientMng_DeliveryTerm, DeliveryTermDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<OfferToClientMng_Forwarder_View, ForwarderDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<OfferToClientMng_PutInProductionTerm_View, PutInProductionTermDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));            

                AutoMapper.Mapper.CreateMap<OfferToClientMng_OfferSeasonType_View, OfferSeasonTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                AutoMapper.Mapper.CreateMap<OfferToClientMng_OfferSeasonDetail_View, OfferSeasonDetailDTO>()
                    .ForMember(s => s.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ProductThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ProductThumbnailLocation) ? s.ProductThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.ProductFileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.ProductFileLocation) ? s.ProductFileLocation : "no-image.jpg")))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferToClientMng_Offer_View, OfferDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.OfferScanFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.OfferScanFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.OfferScanFileURL)))
                   .ForMember(d => d.OfferLineDTOs, o => o.MapFrom(s => s.OfferToClientMng_OfferLine_View.OrderBy(x => x.RowIndex)))
                   .ForMember(d => d.OfferLineSparepartDTOs, o => o.MapFrom(s => s.OfferToClientMng_OfferLineSparepart_View.OrderBy(x => x.RowIndex)))
                   .ForMember(d => d.OfferLineSampleProductDTOs, o => o.MapFrom(s => s.OfferToClientMng_OfferLineSampleProduct_View.OrderBy(x => x.RowIndex)))
                   .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferToClientMng_OfferLineSampleProduct_View, OfferLineSampleProductDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                   //.ForMember(d => d.StatusUpdatedDate, o => o.ResolveUsing(s => s.StatusUpdatedDate.HasValue ? s.StatusUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<OfferLineSampleProductDTO, OfferLineSampleProduct>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferLineSampleProductID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<OfferToClientMng_OfferSeason_View, OfferDTO>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.ClientContactEmail, o => o.ResolveUsing(s => s.Email))
                  .ForMember(d => d.ClientContactPhone, o => o.ResolveUsing(s => s.Telephone))                 
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OfferDTO, Offer>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferID, o => o.Ignore())
                    .ForMember(d => d.OfferScanFile, o => o.Ignore())
                    .ForMember(d => d.OfferLine, o => o.Ignore())
                    .ForMember(d => d.OfferLineSparepart, o => o.Ignore())
                    .ForMember(d => d.IsApproved, o => o.Ignore())
                    .ForMember(d => d.ApprovedBy, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<OfferLineDTO, OfferLine>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.OfferLineID, o => o.Ignore())
                     .ForMember(d => d.OfferLinePriceOption, o => o.Ignore())
                     .ForMember(d => d.ModelID, o => o.Ignore())
                     .ForMember(d => d.FrameMaterialID, o => o.Ignore())
                     .ForMember(d => d.FrameMaterialColorID, o => o.Ignore())
                     .ForMember(d => d.MaterialID, o => o.Ignore())
                     .ForMember(d => d.MaterialTypeID, o => o.Ignore())
                     .ForMember(d => d.MaterialColorID, o => o.Ignore())
                     .ForMember(d => d.SubMaterialID, o => o.Ignore())
                     .ForMember(d => d.SubMaterialColorID, o => o.Ignore())
                     .ForMember(d => d.SeatCushionID, o => o.Ignore())
                     .ForMember(d => d.BackCushionID, o => o.Ignore())
                     .ForMember(d => d.CushionColorID, o => o.Ignore())
                     .ForMember(d => d.FSCTypeID, o => o.Ignore())
                     .ForMember(d => d.FSCPercentID, o => o.Ignore())                  
                     .ForMember(d => d.EstimatedPurchasingPriceUpdatedByID, o => o.Ignore())
                     .ForMember(d => d.PlaningPurchasingPriceSelectedBy, o => o.Ignore())
                     .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.Ignore())
                     .ForMember(d => d.IsApproved, o => o.Ignore())
                    .ForMember(d => d.ApprovedBy, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<OfferLinePriceOptionDTO, OfferLinePriceOption>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferLinePriceOptionID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<OfferToClientMng_OfferSearchResult_View, OfferSearchResultDTO>()
                    .ForMember(s => s.OfferDate, o => o.ResolveUsing(s => s.OfferDate.HasValue ? s.OfferDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(s => s.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(s => s.ValidUntil, o => o.ResolveUsing(s => s.ValidUntil.HasValue ? s.ValidUntil.Value.ToString("dd/MM/yyyy") : ""))
                  .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<OfferLineSparepartDTO, OfferLineSparepart>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.OfferLineSparePartID, o => o.Ignore())
                    .ForMember(d => d.ModelID, o => o.Ignore())
                    .ForMember(d => d.PartID, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<OfferSearchResultDTO> DB2DTO_OfferSearchResultList(List<OfferToClientMng_OfferSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_OfferSearchResult_View>, List<OfferSearchResultDTO>>(dbItems);
        }

        public void DTO2DB_Offer(OfferDTO dtoItem, ref Offer dbItem, int userId)
        {
            //OfferStatus dbOfferStatus = dbItem.OfferStatus.Where(o => o.IsCurrentStatus.HasValue && o.IsCurrentStatus.Value).FirstOrDefault();
            /*
             * MAP & CHECK  OFFERLINE
             */
            List<OfferLine> ItemNeedDelete_Extends = new List<OfferLine>();
            if (dtoItem.OfferLineDTOs != null)
            {
                //CHECK
                foreach (OfferLine dbDetail in dbItem.OfferLine.Where(o => !dtoItem.OfferLineDTOs.Select(s => s.OfferLineID).Contains(o.OfferLineID)))
                {
                    ItemNeedDelete_Extends.Add(dbDetail);
                }
                foreach (OfferLine dbDetail in ItemNeedDelete_Extends)
                {
                    dbItem.OfferLine.Remove(dbDetail);
                }
                //MAP
                foreach (OfferLineDTO dtoDetail in dtoItem.OfferLineDTOs)
                {
                    OfferLine dbDetail;
                    OfferLinePriceOption dbPriceOption;
                    bool isAllowEditItem = true;
                    if (dtoDetail.OfferLineID < 0 ) // actionType { 0:New, 1:Edit,2: Copy, 3:New Version}
                    {
                        dbDetail = new OfferLine();
                        if (dtoDetail.OfferLinePriceOptionDTOs != null)
                        {
                            foreach (OfferLinePriceOptionDTO dtoPriceOption in dtoDetail.OfferLinePriceOptionDTOs)
                            {
                                dbPriceOption = new OfferLinePriceOption();
                                dbDetail.OfferLinePriceOption.Add(dbPriceOption);
                                AutoMapper.Mapper.Map<OfferLinePriceOptionDTO, OfferLinePriceOption>(dtoPriceOption, dbPriceOption);
                            }
                        }
                        dbItem.OfferLine.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.OfferLine.FirstOrDefault(o => o.OfferLineID == dtoDetail.OfferLineID);
                        if (dbDetail != null && dtoDetail.OfferLinePriceOptionDTOs != null)
                        {
                            foreach (OfferLinePriceOptionDTO dtoPriceOption in dtoDetail.OfferLinePriceOptionDTOs)
                            {
                                if (dtoPriceOption.OfferLinePriceOptionID < 0)
                                {
                                    dbPriceOption = new OfferLinePriceOption();
                                    dbDetail.OfferLinePriceOption.Add(dbPriceOption);
                                }
                                else
                                {
                                    dbPriceOption = dbDetail.OfferLinePriceOption.FirstOrDefault(o => o.OfferLinePriceOptionID == dtoPriceOption.OfferLinePriceOptionID);
                                }
                                if (dbPriceOption != null)
                                {
                                    Mapper.Map(dtoPriceOption, dbPriceOption);

                                }
                            }
                        }
                        ////check item exist in factory order
                        //var x = dbDetail.SaleOrderDetail.Where(o => o.FactoryOrderDetail != null && o.FactoryOrderDetail.Count() > 0);
                        //isAllowEditItem = !(x != null && x.Count() > 0);

                        // detect changes for approved items
                        if (dtoDetail.OfferItemTypeID == 1 && dtoDetail.IsApproved.HasValue && !dtoDetail.IsApproved.Value)
                        {
                            dbDetail.IsApproved = false;
                            dbDetail.ApprovedBy = null;
                            dbDetail.ApprovedDate = null;
                        }

                        //
                        // Author       : The My
                        // Description  : force update configuration changed even if item exists in factory order
                        //
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        if (fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_CHANGE_OFFER_ITEM_OPTION))
                        {
                            isAllowEditItem = true;
                        }
                    }

                    if (dbDetail != null)
                    {
                        dtoDetail.FinalPrice = (
                                                        (dtoItem.CommissionPercent == null ? 0 : dtoItem.CommissionPercent) +
                                                        (dtoItem.SurChargePercent == null ? 0 : dtoItem.SurChargePercent) +
                                                        (dtoDetail.IncreasePercent == null ? 0 : dtoDetail.IncreasePercent)
                                                    )
                                                    / 100 * (dtoDetail.UnitPrice == null ? 0 : dtoDetail.UnitPrice)
                                                    + (dtoDetail.UnitPrice == null ? 0 : dtoDetail.UnitPrice);

                        // set update by for est purchasing price
                        if (dtoDetail.EstimatedPurchasingPrice != dbDetail.EstimatedPurchasingPrice)
                        {
                            dbDetail.EstimatedPurchasingPriceUpdatedByID = userId;
                        }

                        // set selected by for planing purchasing price
                        if (dtoDetail.PlaningPurchasingPriceSelectedDate == "just now")
                        {
                            dbDetail.PlaningPurchasingPriceSelectedBy = userId;
                            dbDetail.PlaningPurchasingPriceSelectedDate = DateTime.Now;
                        }

                        Mapper.Map(dtoDetail, dbDetail);

                        // only allow edit item in case item does not put in factory order detail
                        if (isAllowEditItem)
                        {
                            dbDetail.ModelID = dtoDetail.ModelID;
                            dbDetail.FrameMaterialID = dtoDetail.FrameMaterialID;
                            dbDetail.FrameMaterialColorID = dtoDetail.FrameMaterialColorID;
                            dbDetail.MaterialID = dtoDetail.MaterialID;
                            dbDetail.MaterialTypeID = dtoDetail.MaterialTypeID;
                            dbDetail.MaterialColorID = dtoDetail.MaterialColorID;
                            dbDetail.SubMaterialID = dtoDetail.SubMaterialID;
                            dbDetail.SubMaterialColorID = dtoDetail.SubMaterialColorID;
                            dbDetail.SeatCushionID = dtoDetail.SeatCushionID;
                            dbDetail.BackCushionID = dtoDetail.BackCushionID;
                            dbDetail.CushionColorID = dtoDetail.CushionColorID;
                            dbDetail.FSCTypeID = dtoDetail.FSCTypeID;
                            dbDetail.FSCPercentID = dtoDetail.FSCPercentID;
                        }
                    }
                }
            }

            /*
            * MAP & CHECK  OFFERLINE SPAREPART
            */
            List<OfferLineSparepart> needItemDelete = new List<OfferLineSparepart>();
            if (dtoItem.OfferLineSparepartDTOs != null)
            {
                //CHECK
                foreach (OfferLineSparepart dbDetail in dbItem.OfferLineSparepart.Where(o => !dtoItem.OfferLineSparepartDTOs.Select(s => s.OfferLineSparePartID).Contains(o.OfferLineSparePartID)))
                {
                    needItemDelete.Add(dbDetail);
                }
                foreach (OfferLineSparepart dbDetail in needItemDelete)
                {
                    dbItem.OfferLineSparepart.Remove(dbDetail);
                }
                //MAP
                foreach (OfferLineSparepartDTO dtoDetail in dtoItem.OfferLineSparepartDTOs)
                {
                    OfferLineSparepart dbDetail;
                    bool isAllowEditItem = true;
                    if (dtoDetail.OfferLineSparePartID < 0) // actionType { 0:New, 1:Edit,2: Copy, 3:New Version}
                    {
                        dbDetail = new OfferLineSparepart();
                        dbItem.OfferLineSparepart.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.OfferLineSparepart.FirstOrDefault(o => o.OfferLineSparePartID == dtoDetail.OfferLineSparePartID);
                        //var x = dbDetail.SaleOrderDetailSparepart.Where(o => o.FactoryOrderSparepartDetail != null && o.FactoryOrderSparepartDetail.Count() > 0);
                        //isAllowEditItem = !(x != null && x.Count() > 0);
                    }

                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<OfferLineSparepartDTO, OfferLineSparepart>(dtoDetail, dbDetail);

                        // only allow edit item in case item does not put in factory order detail
                        if (isAllowEditItem)
                        {
                            dbDetail.ModelID = dtoDetail.ModelID;
                            dbDetail.PartID = dtoDetail.PartID;
                        }
                    }
                }
            }

            /*
            * MAP & CHECK  OFFERLINE SAMPLE PRODUCT
            */
            List<OfferLineSampleProduct> toBeDeletedItems = new List<OfferLineSampleProduct>();
            if (dtoItem.OfferLineSampleProductDTOs != null)
            {
                //CHECK
                foreach (OfferLineSampleProduct dbSample in dbItem.OfferLineSampleProduct.Where(o => !dtoItem.OfferLineSampleProductDTOs.Select(s => s.OfferLineSampleProductID).Contains(o.OfferLineSampleProductID)).ToArray())
                {
                    dbItem.OfferLineSampleProduct.Remove(dbSample);
                }
                //MAP
                foreach (OfferLineSampleProductDTO dtoSample in dtoItem.OfferLineSampleProductDTOs)
                {
                    OfferLineSampleProduct dbSample = null;
                    if (dtoSample.OfferLineSampleProductID <= 0)
                    {
                        dbSample = new OfferLineSampleProduct();
                        dbItem.OfferLineSampleProduct.Add(dbSample);
                    }
                    else
                    {
                        dbSample = dbItem.OfferLineSampleProduct.FirstOrDefault(o => o.OfferLineSampleProductID == dtoSample.OfferLineSampleProductID);
                        if (dbSample == null)
                        {
                            throw new Exception("Sample item not found!");
                        }
                    }
                    AutoMapper.Mapper.Map<OfferLineSampleProductDTO, OfferLineSampleProduct>(dtoSample, dbSample);
                }
            }

            /*
             * SETUP FORMATED FIELD
             */
            dbItem.OfferDate = Library.Helper.ConvertStringToDateTime(dtoItem.OfferDateFormated, new System.Globalization.CultureInfo("vi-VN"));
            dbItem.ValidUntil = Library.Helper.ConvertStringToDateTime(dtoItem.ValidUntilFormated, new System.Globalization.CultureInfo("vi-VN"));
            dbItem.LDS = Library.Helper.ConvertStringToDateTime(dtoItem.LDSFormated, new System.Globalization.CultureInfo("vi-VN"));
            dbItem.EstimatedDeliveryDate = Library.Helper.ConvertStringToDateTime(dtoItem.EstimatedDeliveryDateFormated, new System.Globalization.CultureInfo("vi-VN"));
            Mapper.Map(dtoItem, dbItem);         
        }
        public OfferDTO DB2DTO_Offer(OfferToClientMng_Offer_View dbItem)
        {
            OfferDTO dtoItem = AutoMapper.Mapper.Map<OfferToClientMng_Offer_View, OfferDTO>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.OfferDate.HasValue)
                dtoItem.OfferDateFormated = dbItem.OfferDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.ValidUntil.HasValue)
                dtoItem.ValidUntilFormated = dbItem.ValidUntil.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS.HasValue)
                dtoItem.LDSFormated = dbItem.LDS.Value.ToString("dd/MM/yyyy");

            if (dbItem.EstimatedDeliveryDate.HasValue)
                dtoItem.EstimatedDeliveryDateFormated = dbItem.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public OfferDTO DB2DTO_Offer(OfferToClientMng_OfferSeason_View dbItem)
        {
            OfferDTO dtoItem = AutoMapper.Mapper.Map<OfferToClientMng_OfferSeason_View, OfferDTO>(dbItem);          
            return dtoItem;
        }
        public List<ClientDTO> DB2DTO_Client(List<OfferToClientMng_Client_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_Client_View>, List<ClientDTO>>(dbItems);
        }
        public List<ClientEstimatedAdditionalCostDTO> DB2DTO_ClientEstimatedAdditionalCostDTO(List<OfferToClientMng_ClientEstimatedAdditionalCost_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_ClientEstimatedAdditionalCost_View>, List<ClientEstimatedAdditionalCostDTO>>(dbItems);
        }
        public List<ExchangeRateDTO> DB2DTO_ExchangeRate(List<OfferToClientMng_function_getExchangeRate_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_function_getExchangeRate_Result>, List<ExchangeRateDTO>>(dbItems);
        }
        public List<PaymentTermDTO> DB2DTO_PaymentTermDTO(List<OfferToClientMng_PaymentTerm> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_PaymentTerm>, List<PaymentTermDTO>>(dbItems);
        }
        public List<InterestPercentDTO> DB2DTO_InterestPercentDTO(List<OfferToClientMng_InterestPercent_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_InterestPercent_View>, List<InterestPercentDTO>>(dbItems);
        }
        public List<DeliveryTermDTO> DB2DTO_DeliveryTermDTO(List<OfferToClientMng_DeliveryTerm> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_DeliveryTerm>, List<DeliveryTermDTO>>(dbItems);
        }
        public List<ForwarderDTO> DB2DTO_ForwarderDTO(List<OfferToClientMng_Forwarder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_Forwarder_View>, List<ForwarderDTO>>(dbItems);
        }
        public List<PutInProductionTermDTO> DB2DTO_PutInProductionTermDTO(List<OfferToClientMng_PutInProductionTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_PutInProductionTerm_View>, List<PutInProductionTermDTO>>(dbItems);
        }
        public List<OfferSeasonTypeDTO> DB2DTO_OfferSeasonType(List<OfferToClientMng_OfferSeasonType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_OfferSeasonType_View>, List<OfferSeasonTypeDTO>>(dbItems);
        }
        public List<OfferSeasonDetailDTO> DB2DTO_OfferSeasonDetail(List<OfferToClientMng_OfferSeasonDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OfferToClientMng_OfferSeasonDetail_View>, List<OfferSeasonDetailDTO>>(dbItems);
        }
    }
}
