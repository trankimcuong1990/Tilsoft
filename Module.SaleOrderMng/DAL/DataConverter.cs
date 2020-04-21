using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "TransportationServiceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderMngSearch_View, DTO.SaleOrderMngSearchResult>()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<SaleOrderMng_OfferSearch_View, DTO.OfferSearch>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderSearch_View, DTO.SaleOrderSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailSearch_View, DTO.SaleOrderDetailSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailExtend_View, DTO.SaleOrderDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetail_View, DTO.SaleOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailExtends, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailExtend_View.OrderBy(x => x.RowIndex)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailSparepart_View, DTO.SaleOrderDetailSparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderExtend_View, DTO.SaleOrderExtend>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrder_View, DTO.SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SignedPIFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SignedPIFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.SignedPIFileURL)))
                    .ForMember(d => d.ClientPOFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ClientPOFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ClientPOFileURL)))
                    .ForMember(d => d.SaleOrderDetails, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetail_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderDetailSpareparts, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailSparepart_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderExtends, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderExtend_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderProductReturns, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderProductReturn_View.OrderBy(x => x.ReturnIndex)))
                    .ForMember(d => d.SaleOrderSparepartReturns, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderSparepartReturn_View.OrderBy(x => x.ReturnIndex)))
                    .ForMember(d => d.WarehouseImports, o => o.MapFrom(s => s.SaleOrderMng_WarehouseImport_View.OrderBy(x => x.ReceiptNo)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.SaleOrder, SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderDetail, o => o.Ignore())
                    .ForMember(d => d.SaleOrderDetailSparepart, o => o.Ignore())
                    .ForMember(d => d.SaleOrderExtend, o => o.Ignore())
                    .ForMember(d => d.SaleOrderHistory, o => o.Ignore())
                    .ForMember(d => d.PaymentUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.PaymentUpdatedDate, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderExtend, SaleOrderExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderDetail, SaleOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderDetailExtend, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderDetailSparepart, SaleOrderDetailSparepart>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.SaleOrderDetailSparepartID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderDetailExtend, SaleOrderDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrder, SaleOrderHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderHistoryID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderHistoryDetail, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderDetail, SaleOrderHistoryDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderHistoryDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrder_ReportView, DTO.PIPrintout>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_PaymentStatus_View, DTO.PaymentStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlanDetail_View, DTO.LoadingPlanDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlanSparepartDetail_View, DTO.LoadingPlanSparepartDetail>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlan_View, DTO.LoadingPlan>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.LoadingPlanDetails, o => o.MapFrom(s => s.SaleOrderMng_LoadingPlanDetail_View))
                     .ForMember(d => d.LoadingPlanSparepartDetails, o => o.MapFrom(s => s.SaleOrderMng_LoadingPlanSparepartDetail_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderProductReturn_View, DTO.SaleOrderProductReturn>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderSparepartReturn_View, DTO.SaleOrderSparepartReturn>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlanDetail2_View, DTO.LoadingPlanDetail2>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlanSparepartDetail2_View, DTO.LoadingPlanSparepartDetail2>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlan2_View, DTO.LoadingPlan2>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.LoadingPlanDetail2s, o => o.MapFrom(s => s.SaleOrderMng_LoadingPlanDetail2_View))
                     .ForMember(d => d.LoadingPlanSparepartDetail2s, o => o.MapFrom(s => s.SaleOrderMng_LoadingPlanSparepartDetail2_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_WarehouseImport_View, DTO.WarehouseImport>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ImportedDate, o => o.ResolveUsing(s => s.ImportedDate.HasValue ? s.ImportedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //OverView Area
                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailExtend_OverView, DTO.SaleOrderDetailExtendOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetail_OverView, DTO.SaleOrderDetailOverView>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailExtends, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailExtend_OverView.OrderBy(x => x.RowIndex)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailSparepart_OverView, DTO.SaleOrderDetailSparepartOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderExtend_OverView, DTO.SaleOrderExtendOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrder_OverView, DTO.SaleOrderOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SignedPIFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SignedPIFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.SignedPIFileURL)))
                    .ForMember(d => d.ClientPOFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ClientPOFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ClientPOFileURL)))
                    .ForMember(d => d.SaleOrderDetails, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetail_OverView.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderDetailSpareparts, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailSparepart_OverView.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderExtends, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderExtend_OverView.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.WarehouseImports, o => o.MapFrom(s => s.SaleOrderMng_WarehouseImport_OverView.OrderBy(x => x.ReceiptNo)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.SaleOrderOverview, SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull))
                ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderExtendOverView, SaleOrderExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderDetailOverView, SaleOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderDetailExtend, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderDetailSparepartOverView, SaleOrderDetailSparepart>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.SaleOrderDetailSparepartID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderDetailExtendOverView, SaleOrderDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderOverview, SaleOrderHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderHistoryID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderHistoryDetail, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderDetailOverView, SaleOrderHistoryDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderHistoryDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<SaleOrderMng_WarehouseImport_OverView, DTO.WarehouseImportOverView>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ImportedDate, o => o.ResolveUsing(s => s.ImportedDate.HasValue ? s.ImportedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SaleOrderMng_OfferLine_View, DTO.SaleOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.ResolveUsing(s => s.PlaningPurchasingPriceSelectedDate.HasValue ? s.PlaningPurchasingPriceSelectedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SaleOrderMng_OfferLineSparepart_View, DTO.SaleOrderDetailSparepart>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_OTC_View, DTO.SaleOrderDetailOTC>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.ResolveUsing(s => s.PlaningPurchasingPriceSelectedDate.HasValue ? s.PlaningPurchasingPriceSelectedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SaleOrderMng_OTCSparepart_View, DTO.SaleOrderDetailOTCSparepart>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // product status            
                AutoMapper.Mapper.CreateMap<SupportMng_ProductStatus_View, DTO.ProductStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.SaleOrderMngSearchResult> DB2DTO_SaleOrderResultList(List<SaleOrderMng_SaleOrderMngSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_SaleOrderMngSearch_View>, List<DTO.SaleOrderMngSearchResult>>(dbItems);
        }

        public List<DTO.SaleOrderDetail> DB2DTO_OfferLine(List<SaleOrderMng_OfferLine_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_OfferLine_View>, List<DTO.SaleOrderDetail>>(dbItems);
        }

        public List<DTO.SaleOrderDetailSparepart> DB2DTO_OfferLineSparepart(List<SaleOrderMng_OfferLineSparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_OfferLineSparepart_View>, List<DTO.SaleOrderDetailSparepart>>(dbItems);
        }

        public List<DTO.SaleOrderDetailOTC> DB2DTO_OfferLineOTC(List<SaleOrderMng_OTC_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_OTC_View>, List<DTO.SaleOrderDetailOTC>>(dbItems);
        }

        public List<DTO.SaleOrderDetailOTCSparepart> DB2DTO_OfferLineOTCSparepart(List<SaleOrderMng_OTCSparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_OTCSparepart_View>, List<DTO.SaleOrderDetailOTCSparepart>>(dbItems);
        }



        //public List<DTO.SaleOrderMng.OfferSearch> DB2DTO_OfferSearch(List<SaleOrderMng_OfferSearch_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<SaleOrderMng_OfferSearch_View>, List<DTO.SaleOrderMng.OfferSearch>>(dbItems);
        //}

        public List<DTO.SaleOrderSearch> DB2DTO_SaleOrderSearch(List<SaleOrderMng_SaleOrderSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_SaleOrderSearch_View>, List<DTO.SaleOrderSearch>>(dbItems);
        }

        public List<DTO.SaleOrderDetailSearch> DB2DTO_SaleOrderDetailSearch(List<SaleOrderMng_SaleOrderDetailSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_SaleOrderDetailSearch_View>, List<DTO.SaleOrderDetailSearch>>(dbItems);
        }

        public DTO.SaleOrder DB2DTO_SaleOrder(SaleOrderMng_SaleOrder_View dbItem)
        {
            DTO.SaleOrder dtoItem = AutoMapper.Mapper.Map<SaleOrderMng_SaleOrder_View, DTO.SaleOrder>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.InvoiceDate.HasValue)
                dtoItem.InvoiceDateFormated = dbItem.InvoiceDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.PIReceivedDate.HasValue)
                dtoItem.PIReceivedDateFormated = dbItem.PIReceivedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.DeliveryDate.HasValue)
                dtoItem.DeliveryDateFormated = dbItem.DeliveryDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.DeliveryDateInternal.HasValue)
                dtoItem.DeliveryDateInternalFormated = dbItem.DeliveryDateInternal.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS.HasValue)
                dtoItem.LDSFormated = dbItem.LDS.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.PaymentDate.HasValue)
                dtoItem.paymentDateFormated = dbItem.PaymentDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.IsDPReceived == true)
            {
                dbItem.IsDPReceived = dbItem.IsDPReceived.Value == true;
            }
            else
            {
                dbItem.IsDPReceived = dbItem.IsDPReceived.Value == false;
            }
            return dtoItem;
        }

        public void DTO2DB_SaleOrder(DTO.SaleOrder dtoItem, ref SaleOrder dbItem)
        {
            /*
             * MAP & CHECK SaleOrder
             */
            List<SaleOrderDetail> itemNeedDelete_Details = new List<SaleOrderDetail>();
            List<SaleOrderDetailExtend> itemNeedDelete_DetailExtends;
            if (dtoItem.SaleOrderDetails != null)
            {
                //CHECK
                foreach (SaleOrderDetail dbDetail in dbItem.SaleOrderDetail)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.SaleOrderDetails.Select(s => s.SaleOrderDetailID).Contains(dbDetail.SaleOrderDetailID))
                    {
                        itemNeedDelete_Details.Add(dbDetail);
                    }
                    else //DB IS EXIST IN DB
                    {
                        itemNeedDelete_DetailExtends = new List<SaleOrderDetailExtend>();
                        foreach (SaleOrderDetailExtend dbDetailExtend in dbDetail.SaleOrderDetailExtend)
                        {
                            if (!dtoItem.SaleOrderDetails.Where(o => o.SaleOrderDetailID == dbDetail.SaleOrderDetailID).FirstOrDefault().SaleOrderDetailExtends.Select(x => x.SaleOrderDetailExtendID).Contains(dbDetailExtend.SaleOrderDetailExtendID))
                            {
                                itemNeedDelete_DetailExtends.Add(dbDetailExtend);
                            }
                        }
                        foreach (SaleOrderDetailExtend dbDetailExtend in itemNeedDelete_DetailExtends)
                        {
                            dbDetail.SaleOrderDetailExtend.Remove(dbDetailExtend);
                        }
                    }
                }

                foreach (SaleOrderDetail dbDetail in itemNeedDelete_Details)
                {
                    List<SaleOrderDetailExtend> item_deleteds = new List<SaleOrderDetailExtend>();
                    foreach (SaleOrderDetailExtend dbDetailExtend in dbDetail.SaleOrderDetailExtend)
                    {
                        item_deleteds.Add(dbDetailExtend);
                    }
                    foreach (SaleOrderDetailExtend item in item_deleteds)
                    {
                        dbItem.SaleOrderDetail.Where(o => o.SaleOrderDetailID == dbDetail.SaleOrderDetailID).FirstOrDefault().SaleOrderDetailExtend.Remove(item);
                    }
                    dbItem.SaleOrderDetail.Remove(dbDetail);
                }

                //MAP
                SaleOrderDetail _dbDetail;
                SaleOrderDetailExtend _dbDetailExtend;
                foreach (DTO.SaleOrderDetail dtoDetail in dtoItem.SaleOrderDetails)
                {
                    if (dtoDetail.SaleOrderDetailID < 0)
                    {
                        _dbDetail = new SaleOrderDetail();

                        if (dtoDetail.SaleOrderDetailExtends != null)
                        {
                            foreach (DTO.SaleOrderDetailExtend dtoDetailExtend in dtoDetail.SaleOrderDetailExtends)
                            {
                                _dbDetailExtend = new SaleOrderDetailExtend();
                                _dbDetail.SaleOrderDetailExtend.Add(_dbDetailExtend);
                                AutoMapper.Mapper.Map<DTO.SaleOrderDetailExtend, SaleOrderDetailExtend>(dtoDetailExtend, _dbDetailExtend);
                            }
                        }

                        dbItem.SaleOrderDetail.Add(_dbDetail);
                    }
                    else
                    {
                        _dbDetail = dbItem.SaleOrderDetail.FirstOrDefault(o => o.SaleOrderDetailID == dtoDetail.SaleOrderDetailID);
                        if (_dbDetail != null && dtoDetail.SaleOrderDetailExtends != null)
                        {
                            foreach (DTO.SaleOrderDetailExtend dtoDetailExtend in dtoDetail.SaleOrderDetailExtends)
                            {
                                if (dtoDetailExtend.SaleOrderDetailExtendID < 0)
                                {
                                    _dbDetailExtend = new SaleOrderDetailExtend();
                                    _dbDetail.SaleOrderDetailExtend.Add(_dbDetailExtend);
                                }
                                else
                                {
                                    _dbDetailExtend = _dbDetail.SaleOrderDetailExtend.FirstOrDefault(o => o.SaleOrderDetailExtendID == dtoDetailExtend.SaleOrderDetailExtendID);
                                }
                                if (_dbDetailExtend != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.SaleOrderDetailExtend, SaleOrderDetailExtend>(dtoDetailExtend, _dbDetailExtend);
                                }
                            }
                        }
                    }

                    if (_dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SaleOrderDetail, SaleOrderDetail>(dtoDetail, _dbDetail);
                    }
                }
            }

            /*
             * MAP & CHECK SaleOrderExtend
             */
            List<SaleOrderExtend> ItemNeedDelete_Extends = new List<SaleOrderExtend>();
            if (dtoItem.SaleOrderExtends != null)
            {
                //CHECK
                foreach (SaleOrderExtend dbExtend in dbItem.SaleOrderExtend.Where(o => !dtoItem.SaleOrderExtends.Select(s => s.SaleOrderExtendID).Contains(o.SaleOrderExtendID)))
                {
                    ItemNeedDelete_Extends.Add(dbExtend);
                }
                foreach (SaleOrderExtend dbExtend in ItemNeedDelete_Extends)
                {
                    dbItem.SaleOrderExtend.Remove(dbExtend);
                }
                //MAP
                foreach (DTO.SaleOrderExtend dtoDescription in dtoItem.SaleOrderExtends)
                {
                    SaleOrderExtend dbExtend;
                    if (dtoDescription.SaleOrderExtendID < 0)
                    {
                        dbExtend = new SaleOrderExtend();
                        dbItem.SaleOrderExtend.Add(dbExtend);
                    }
                    else
                    {
                        dbExtend = dbItem.SaleOrderExtend.FirstOrDefault(o => o.SaleOrderExtendID == dtoDescription.SaleOrderExtendID);
                    }

                    if (dbExtend != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SaleOrderExtend, SaleOrderExtend>(dtoDescription, dbExtend);
                    }
                }
            }

            /*
            * MAP & CHECK SaleOrderDetailSparepart
            */
            List<SaleOrderDetailSparepart> itemNeedDelete = new List<SaleOrderDetailSparepart>();
            if (dtoItem.SaleOrderDetailSpareparts != null)
            {
                //CHECK
                foreach (SaleOrderDetailSparepart item in dbItem.SaleOrderDetailSparepart.Where(o => !dtoItem.SaleOrderDetailSpareparts.Select(s => s.SaleOrderDetailSparepartID).Contains(o.SaleOrderDetailSparepartID)))
                {
                    itemNeedDelete.Add(item);
                }
                foreach (SaleOrderDetailSparepart item in itemNeedDelete)
                {
                    dbItem.SaleOrderDetailSparepart.Remove(item);
                }
                //MAP
                foreach (DTO.SaleOrderDetailSparepart item in dtoItem.SaleOrderDetailSpareparts)
                {
                    SaleOrderDetailSparepart dbSparepart;
                    if (item.SaleOrderDetailSparepartID < 0)
                    {
                        dbSparepart = new SaleOrderDetailSparepart();
                        dbItem.SaleOrderDetailSparepart.Add(dbSparepart);
                    }
                    else
                    {
                        dbSparepart = dbItem.SaleOrderDetailSparepart.FirstOrDefault(o => o.SaleOrderDetailSparepartID == item.SaleOrderDetailSparepartID);
                    }

                    if (dbSparepart != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SaleOrderDetailSparepart, SaleOrderDetailSparepart>(item, dbSparepart);
                    }
                }
            }

            /*
             * MAP SaleOrder History
             */
            SaleOrderHistory dbHistory = new SaleOrderHistory();
            SaleOrderHistoryDetail dbHistoryDetail;
            foreach (DTO.SaleOrderDetail dtoDetail in dtoItem.SaleOrderDetails)
            {
                dbHistoryDetail = new SaleOrderHistoryDetail();
                AutoMapper.Mapper.Map<DTO.SaleOrderDetail, SaleOrderHistoryDetail>(dtoDetail, dbHistoryDetail);
                dbHistory.SaleOrderHistoryDetail.Add(dbHistoryDetail);
            }
            AutoMapper.Mapper.Map<DTO.SaleOrder, SaleOrderHistory>(dtoItem, dbHistory);
            dbItem.SaleOrderHistory.Add(dbHistory);
            /*
             * SETUP FORMATED FIELD
             */
            if (!string.IsNullOrEmpty(dtoItem.InvoiceDateFormated))
            {
                dtoItem.InvoiceDate = DateTime.ParseExact(dtoItem.InvoiceDateFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.InvoiceDate = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.LDSFormated))
            {
                dtoItem.LDS = DateTime.ParseExact(dtoItem.LDSFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.LDS = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.DeliveryDateFormated))
            {
                dtoItem.DeliveryDate = DateTime.ParseExact(dtoItem.DeliveryDateFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.DeliveryDate = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.DeliveryDateInternalFormated))
            {
                dtoItem.DeliveryDateInternal = DateTime.ParseExact(dtoItem.DeliveryDateInternalFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.DeliveryDateInternal = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.PIReceivedDateFormated))
            {
                dtoItem.PIReceivedDate = DateTime.ParseExact(dtoItem.PIReceivedDateFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.PIReceivedDate = null;
            }

            if (!string.IsNullOrEmpty(dtoItem.paymentDateFormated))
            {
                dtoItem.PaymentDate = DateTime.ParseExact(dtoItem.paymentDateFormated, "d", new System.Globalization.CultureInfo("vi-VN"));
            }
            else
            {
                dtoItem.PaymentDate = null;
            }

            AutoMapper.Mapper.Map<DTO.SaleOrder, SaleOrder>(dtoItem, dbItem);
        }

        public DTO.PIContainerPrintout DB2DTO_Printout(SaleOrderMng_SaleOrder_ReportView dbItem)
        {
            DTO.PIContainerPrintout dtoItem = new DTO.PIContainerPrintout();
            dtoItem.PIPrintouts = new List<DTO.PIPrintout>();
            dtoItem.PIDetailPrintouts = new List<DTO.PIDetailPrintout>();



            DTO.PIPrintout dtoPI = AutoMapper.Mapper.Map<SaleOrderMng_SaleOrder_ReportView, DTO.PIPrintout>(dbItem);
            dtoItem.PIPrintouts.Add(dtoPI);

            //COPY PI DETAIL
            DTO.PIDetailPrintout dtoPIDetail;

            foreach (SaleOrderMng_SaleOrderExtend_ReportView dbExtend in dbItem.SaleOrderMng_SaleOrderExtend_ReportView.Where(o => o.Position == "TOP").OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.PIDetailPrintout();
                dtoPIDetail.Description = dbExtend.Description;
                dtoPIDetail.SaleAmount = dbExtend.TotalAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
            }

            foreach (SaleOrderMng_SaleOrderDetail_ReportView dbDetail in dbItem.SaleOrderMng_SaleOrderDetail_ReportView.OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.PIDetailPrintout();
                dtoPIDetail.ArticleCode = dbDetail.ArticleCode;
                dtoPIDetail.Description = dbDetail.Description;
                dtoPIDetail.OrderQnt = dbDetail.OrderQnt;
                dtoPIDetail.UnitPrice = dbDetail.UnitPrice;
                dtoPIDetail.SaleAmount = dbDetail.SaleAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);

                if (!string.IsNullOrEmpty(dbDetail.ClientArticleCode))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetail.ClientArticleCode;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbDetail.ClientArticleName))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetail.ClientArticleName;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbDetail.ClientEANCode))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetail.ClientEANCode;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbItem.ClientOrderNumber))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbItem.ClientOrderNumber;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbDetail.Reference))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetail.Reference;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                foreach (SaleOrderMng_SaleOrderDetailExtend_ReportView dbDetailExt in dbDetail.SaleOrderMng_SaleOrderDetailExtend_ReportView.OrderBy(o => o.RowIndex))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetailExt.Description;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }
            }

            foreach (SaleOrderMng_SaleOrderDetailSparepart_ReportView dbSparepart in dbItem.SaleOrderMng_SaleOrderDetailSparepart_ReportView.OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.PIDetailPrintout();
                dtoPIDetail.ArticleCode = dbSparepart.ArticleCode;
                dtoPIDetail.Description = dbSparepart.Description;
                dtoPIDetail.OrderQnt = dbSparepart.OrderQnt;
                dtoPIDetail.UnitPrice = dbSparepart.UnitPrice;
                dtoPIDetail.SaleAmount = dbSparepart.SaleAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);

                if (!string.IsNullOrEmpty(dbSparepart.ClientArticleCode))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbSparepart.ClientArticleCode;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbSparepart.ClientArticleName))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbSparepart.ClientArticleName;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbSparepart.ClientEANCode))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbSparepart.ClientEANCode;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbItem.ClientOrderNumber))
                {
                    dtoPIDetail = new DTO.PIDetailPrintout();
                    dtoPIDetail.Description = dbItem.ClientOrderNumber;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

            }

            foreach (SaleOrderMng_SaleOrderExtend_ReportView dbExtend in dbItem.SaleOrderMng_SaleOrderExtend_ReportView.Where(o => o.Position == "BOTTOM").OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.PIDetailPrintout();
                dtoPIDetail.Description = dbExtend.Description;
                dtoPIDetail.SaleAmount = dbExtend.TotalAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
            }

            if (dbItem.IsViewLDSOnPrintout.HasValue && dbItem.IsViewLDSOnPrintout.Value && dbItem.LDS != "")
            {
                dtoPIDetail = new DTO.PIDetailPrintout();
                dtoPIDetail.Description = "LDS: " + dbItem.LDS;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
            }

            if (dbItem.IsViewDeliveryDateOnPrintout.HasValue && dbItem.IsViewDeliveryDateOnPrintout.Value && dbItem.DeliveryDate != "")
            {
                dtoPIDetail = new DTO.PIDetailPrintout();
                dtoPIDetail.Description = "DELIVERY DATE: " + dbItem.DeliveryDate;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
            }

            string bottomString = "";
            if (dbItem.IsViewQuantityContOnPrintout.HasValue && dbItem.IsViewQuantityContOnPrintout.Value)
            {
                if (dbItem.Quantity40HC.HasValue) bottomString += dbItem.Quantity40HC.ToString() + "x40'HC CONTAINER";
                if (dbItem.Quantity40DC.HasValue) bottomString += " " + dbItem.Quantity40DC.ToString() + "x40'DC CONTAINER";
                if (dbItem.Quantity20DC.HasValue) bottomString += " " + dbItem.Quantity20DC.ToString() + "x20'DC CONTAINER";
            }

            dtoPIDetail = new DTO.PIDetailPrintout();
            dtoPIDetail.Description = bottomString;
            dtoItem.PIDetailPrintouts.Add(dtoPIDetail);

            return dtoItem;
        }

        public List<DTO.PaymentStatus> DB2DTO_PaymentStatus(List<List_PaymentStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PaymentStatus_View>, List<DTO.PaymentStatus>>(dbItems);
        }

        public List<DTO.LoadingPlan> DB2DTO_LoadingPlan(List<SaleOrderMng_LoadingPlan_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_LoadingPlan_View>, List<DTO.LoadingPlan>>(dbItems);
        }

        public List<DTO.LoadingPlan2> DB2DTO_LoadingPlan2(List<SaleOrderMng_LoadingPlan2_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_LoadingPlan2_View>, List<DTO.LoadingPlan2>>(dbItems);
        }

        public DTO.SaleOrderOverview DB2DTO_SaleOrderOverView(SaleOrderMng_SaleOrder_OverView dbItem)
        {
            DTO.SaleOrderOverview dtoItem = AutoMapper.Mapper.Map<SaleOrderMng_SaleOrder_OverView, DTO.SaleOrderOverview>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.InvoiceDate.HasValue)
                dtoItem.InvoiceDateFormated = dbItem.InvoiceDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.PIReceivedDate.HasValue)
                dtoItem.PIReceivedDateFormated = dbItem.PIReceivedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.DeliveryDate.HasValue)
                dtoItem.DeliveryDateFormated = dbItem.DeliveryDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.DeliveryDateInternal.HasValue)
                dtoItem.DeliveryDateInternalFormated = dbItem.DeliveryDateInternal.Value.ToString("dd/MM/yyyy");

            if (dbItem.LDS.HasValue)
                dtoItem.LDSFormated = dbItem.LDS.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.PaymentDate.HasValue)
                dtoItem.paymentDateFormated = dbItem.PaymentDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.IsDPReceived == true)
            {
                dbItem.IsDPReceived = dbItem.IsDPReceived.Value == true;
            }
            else
            {
                dbItem.IsDPReceived = dbItem.IsDPReceived.Value == false;
            }
            return dtoItem;
        }

        public List<DTO.ProductStatus> DB2DTO_ProductStatus(List<SupportMng_ProductStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ProductStatus_View>, List<DTO.ProductStatus>>(dbItems);
        }

        //public List<DTO.LoadingPlan2> DB2DTO_LoadingPlan2(List<SaleOrderMng_LoadingPlan2_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<SaleOrderMng_LoadingPlan2_View>, List<DTO.LoadingPlan2>>(dbItems);
        //}

        //public List<DTO.LoadingPlan> DB2DTO_LoadingPlan(List<SaleOrderMng_LoadingPlan_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<SaleOrderMng_LoadingPlan_View>, List<DTO.LoadingPlan>>(dbItems);
        //}
    }
}
