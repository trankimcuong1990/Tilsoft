using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.SaleOrderMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "SaleOrderMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<SaleOrderMng_OfferSearch_View, DTO.SaleOrderMng.OfferSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderSearch_View, DTO.SaleOrderMng.SaleOrderSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailSearch_View, DTO.SaleOrderMng.SaleOrderDetailSearch>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailExtend_View, DTO.SaleOrderMng.SaleOrderDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetail_View, DTO.SaleOrderMng.SaleOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailExtends, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailExtend_View.OrderBy(x => x.RowIndex)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailSparepart_View, DTO.SaleOrderMng.SaleOrderDetailSparepart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailSample_View, DTO.SaleOrderMng.SaleOrderDetailSample>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderExtend_View, DTO.SaleOrderMng.SaleOrderExtend>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrder_View, DTO.SaleOrderMng.SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SignedPIFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SignedPIFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.SignedPIFileURL)))
                    .ForMember(d => d.ClientPOFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ClientPOFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ClientPOFileURL)))
                    .ForMember(d => d.LCFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.LCFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.LCFileURL)))
                    .ForMember(d => d.SaleOrderDetails, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetail_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderDetailSpareparts, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailSparepart_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderExtends, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderExtend_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderProductReturns, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderProductReturn_View.OrderBy(x => x.ReturnIndex)))
                    .ForMember(d => d.SaleOrderSparepartReturns, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderSparepartReturn_View.OrderBy(x => x.ReturnIndex)))
                    .ForMember(d => d.WarehouseImports, o => o.MapFrom(s => s.SaleOrderMng_WarehouseImport_View.OrderBy(x => x.ReceiptNo)))
                    .ForMember(d => d.SaleOrderDetailSamples, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailSample_View.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.LDSClients, o => o.MapFrom(s => s.SaleOrderMng_LDSClient_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrder, SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderDetail, o => o.Ignore())
                    .ForMember(d => d.SaleOrderDetailSparepart, o => o.Ignore())
                    .ForMember(d => d.SaleOrderExtend, o => o.Ignore())
                    .ForMember(d => d.SaleOrderHistory, o => o.Ignore())
                    .ForMember(d => d.PaymentUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.PaymentUpdatedDate, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderExtend, SaleOrderExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetail, SaleOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderDetailExtend, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetailSparepart, SaleOrderDetailSparepart>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.SaleOrderDetailSparepartID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetailSample, SaleOrderDetailSample>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.SaleOrderDetailSampleID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetailExtend, SaleOrderDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrder, SaleOrderHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderHistoryID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderHistoryDetail, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetail, SaleOrderHistoryDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderHistoryDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<SaleOrder_ReportView, DTO.SaleOrderMng.PIPrintout>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_PaymentStatus_View, DTO.SaleOrderMng.PaymentStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlanDetail_View, DTO.SaleOrderMng.LoadingPlanDetail>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlanSparepartDetail_View, DTO.SaleOrderMng.LoadingPlanSparepartDetail>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlan_View, DTO.SaleOrderMng.LoadingPlan>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.LoadingPlanDetails, o => o.MapFrom(s => s.SaleOrderMng_LoadingPlanDetail_View))
                     .ForMember(d => d.LoadingPlanSparepartDetails, o => o.MapFrom(s => s.SaleOrderMng_LoadingPlanSparepartDetail_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderProductReturn_View, DTO.SaleOrderMng.SaleOrderProductReturn>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderSparepartReturn_View, DTO.SaleOrderMng.SaleOrderSparepartReturn>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlanDetail2_View, DTO.SaleOrderMng.LoadingPlanDetail2>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlanSparepartDetail2_View, DTO.SaleOrderMng.LoadingPlanSparepartDetail2>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LoadingPlan2_View, DTO.SaleOrderMng.LoadingPlan2>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.LoadingPlanDetail2s, o => o.MapFrom(s => s.SaleOrderMng_LoadingPlanDetail2_View))
                     .ForMember(d => d.LoadingPlanSparepartDetail2s, o => o.MapFrom(s => s.SaleOrderMng_LoadingPlanSparepartDetail2_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_WarehouseImport_View, DTO.SaleOrderMng.WarehouseImport>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ImportedDate, o => o.ResolveUsing(s => s.ImportedDate.HasValue ? s.ImportedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //OverView Area
                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailExtend_OverView, DTO.SaleOrderMng.SaleOrderDetailExtendOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetail_OverView, DTO.SaleOrderMng.SaleOrderDetailOverView>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailExtends, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailExtend_OverView.OrderBy(x => x.RowIndex)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderDetailSparepart_OverView, DTO.SaleOrderMng.SaleOrderDetailSparepartOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrderExtend_OverView, DTO.SaleOrderMng.SaleOrderExtendOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_SaleOrder_OverView, DTO.SaleOrderMng.SaleOrderOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SignedPIFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.SignedPIFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.SignedPIFileURL)))
                    .ForMember(d => d.ClientPOFileURL, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ClientPOFileURL) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.ClientPOFileURL)))
                    .ForMember(d => d.SaleOrderDetails, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetail_OverView.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderDetailSpareparts, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderDetailSparepart_OverView.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.SaleOrderExtends, o => o.MapFrom(s => s.SaleOrderMng_SaleOrderExtend_OverView.OrderBy(x => x.RowIndex)))
                    .ForMember(d => d.WarehouseImports, o => o.MapFrom(s => s.SaleOrderMng_WarehouseImport_OverView.OrderBy(x => x.ReceiptNo)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderOverview, SaleOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull))
                ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderExtendOverView, SaleOrderExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetailOverView, SaleOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderDetailExtend, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetailSparepartOverView, SaleOrderDetailSparepart>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.SaleOrderDetailSparepartID, o => o.Ignore())
                   ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetailExtendOverView, SaleOrderDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderDetailExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderOverview, SaleOrderHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderHistoryID, o => o.Ignore())
                    .ForMember(d => d.SaleOrderHistoryDetail, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.SaleOrderMng.SaleOrderDetailOverView, SaleOrderHistoryDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SaleOrderHistoryDetailID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<SaleOrderMng_WarehouseImport_OverView, DTO.SaleOrderMng.WarehouseImportOverView>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ImportedDate, o => o.ResolveUsing(s => s.ImportedDate.HasValue ? s.ImportedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SaleOrderMng_OfferLine_View, DTO.SaleOrderMng.SaleOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForMember(d => d.PlaningPurchasingPriceSelectedDate, o => o.ResolveUsing(s => s.PlaningPurchasingPriceSelectedDate.HasValue ? s.PlaningPurchasingPriceSelectedDate.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<SaleOrderMng_OfferLineSparepart_View, DTO.SaleOrderMng.SaleOrderDetailSparepart>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_OfferLineSample_View, DTO.SaleOrderMng.SaleOrderDetailSample>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                   .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_GetClientIDByOfferID_View, DTO.SaleOrderMng.LinkInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<List_TrackingStatus, DTO.SaleOrderMng.SaleOrderStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_LDSClient_View, DTO.SaleOrderMng.LDSClient>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_FactoryOrderDetail_OverView, DTO.SaleOrderMng.FactoryOrderDetailOverView>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.factoryOrderDetailQCReportOverViews, o => o.MapFrom(s => s.SaleOrderMng_FactoryOrderDetailQCReport_OverView))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SaleOrderMng_FactoryOrderDetailQCReport_OverView, DTO.SaleOrderMng.FactoryOrderDetailQCReportOverView>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);

            }
        }

        public List<DTO.SaleOrderMng.SaleOrderDetail> DB2DTO_OfferLine(List<SaleOrderMng_OfferLine_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_OfferLine_View>, List<DTO.SaleOrderMng.SaleOrderDetail>>(dbItems);
        }

        public List<DTO.SaleOrderMng.SaleOrderDetailSparepart> DB2DTO_OfferLineSparepart(List<SaleOrderMng_OfferLineSparepart_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_OfferLineSparepart_View>, List<DTO.SaleOrderMng.SaleOrderDetailSparepart>>(dbItems);
        }

        public List<DTO.SaleOrderMng.SaleOrderDetailSample> DB2DTO_OfferLineSample(List<SaleOrderMng_OfferLineSample_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_OfferLineSample_View>, List<DTO.SaleOrderMng.SaleOrderDetailSample>>(dbItems);
        }

        public List<DTO.SaleOrderMng.OfferSearch> DB2DTO_OfferSearch(List<SaleOrderMng_OfferSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_OfferSearch_View>, List<DTO.SaleOrderMng.OfferSearch>>(dbItems);
        }

        public List<DTO.SaleOrderMng.SaleOrderSearch> DB2DTO_SaleOrderSearch(List<SaleOrderMng_SaleOrderSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_SaleOrderSearch_View>, List<DTO.SaleOrderMng.SaleOrderSearch>>(dbItems);
        }

        public List<DTO.SaleOrderMng.SaleOrderDetailSearch> DB2DTO_SaleOrderDetailSearch(List<SaleOrderMng_SaleOrderDetailSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_SaleOrderDetailSearch_View>, List<DTO.SaleOrderMng.SaleOrderDetailSearch>>(dbItems);
        }

        public DTO.SaleOrderMng.SaleOrder DB2DTO_SaleOrder(SaleOrderMng_SaleOrder_View dbItem)
        {
            DTO.SaleOrderMng.SaleOrder dtoItem = AutoMapper.Mapper.Map<SaleOrderMng_SaleOrder_View, DTO.SaleOrderMng.SaleOrder>(dbItem);

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

        public void DTO2DB_SaleOrder(DTO.SaleOrderMng.SaleOrder dtoItem, ref SaleOrder dbItem)
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
                foreach (DTO.SaleOrderMng.SaleOrderDetail dtoDetail in dtoItem.SaleOrderDetails)
                {
                    if (dtoDetail.SaleOrderDetailID < 0)
                    {
                        _dbDetail = new SaleOrderDetail();

                        if (dtoDetail.SaleOrderDetailExtends != null)
                        {
                            foreach (DTO.SaleOrderMng.SaleOrderDetailExtend dtoDetailExtend in dtoDetail.SaleOrderDetailExtends)
                            {
                                _dbDetailExtend = new SaleOrderDetailExtend();
                                _dbDetail.SaleOrderDetailExtend.Add(_dbDetailExtend);
                                AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrderDetailExtend, SaleOrderDetailExtend>(dtoDetailExtend, _dbDetailExtend);
                            }
                        }

                        dbItem.SaleOrderDetail.Add(_dbDetail);
                    }
                    else
                    {
                        _dbDetail = dbItem.SaleOrderDetail.FirstOrDefault(o => o.SaleOrderDetailID == dtoDetail.SaleOrderDetailID);
                        if (_dbDetail != null && dtoDetail.SaleOrderDetailExtends != null)
                        {
                            foreach (DTO.SaleOrderMng.SaleOrderDetailExtend dtoDetailExtend in dtoDetail.SaleOrderDetailExtends)
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
                                    AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrderDetailExtend, SaleOrderDetailExtend>(dtoDetailExtend, _dbDetailExtend);
                                }
                            }
                        }
                    }

                    if (_dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrderDetail, SaleOrderDetail>(dtoDetail, _dbDetail);
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
                foreach (DTO.SaleOrderMng.SaleOrderExtend dtoDescription in dtoItem.SaleOrderExtends)
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
                        AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrderExtend, SaleOrderExtend>(dtoDescription, dbExtend);
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
                foreach (DTO.SaleOrderMng.SaleOrderDetailSparepart item in dtoItem.SaleOrderDetailSpareparts)
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
                        AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrderDetailSparepart, SaleOrderDetailSparepart>(item, dbSparepart);
                    }
                }
            }

            /*
             * MAP & CHECK Sample
             */
            List<SaleOrderDetailSample> itemNeedDelete_Sample = new List<SaleOrderDetailSample>();
            if (dtoItem.SaleOrderDetailSamples != null)
            {
                //CHECK
                foreach (SaleOrderDetailSample item in dbItem.SaleOrderDetailSample.Where(o => !dtoItem.SaleOrderDetailSamples.Select(s => s.SaleOrderDetailSampleID).Contains(o.SaleOrderDetailSampleID)))
                {
                    itemNeedDelete_Sample.Add(item);
                }
                foreach (SaleOrderDetailSample item in itemNeedDelete_Sample)
                {
                    dbItem.SaleOrderDetailSample.Remove(item);
                }
                //MAP
                foreach (DTO.SaleOrderMng.SaleOrderDetailSample item in dtoItem.SaleOrderDetailSamples)
                {
                    SaleOrderDetailSample dbSample;
                    if (item.SaleOrderDetailSampleID < 0)
                    {
                        dbSample = new SaleOrderDetailSample();
                        dbItem.SaleOrderDetailSample.Add(dbSample);
                    }
                    else
                    {
                        dbSample = dbItem.SaleOrderDetailSample.FirstOrDefault(o => o.SaleOrderDetailSampleID == item.SaleOrderDetailSampleID);
                    }

                    if (dbSample != null)
                    {
                        AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrderDetailSample, SaleOrderDetailSample>(item, dbSample);
                    }
                }
            }

            /*
             * MAP SaleOrder History
             */
            SaleOrderHistory dbHistory = new SaleOrderHistory();
            SaleOrderHistoryDetail dbHistoryDetail;
            foreach (DTO.SaleOrderMng.SaleOrderDetail dtoDetail in dtoItem.SaleOrderDetails)
            {
                dbHistoryDetail = new SaleOrderHistoryDetail();
                AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrderDetail, SaleOrderHistoryDetail>(dtoDetail, dbHistoryDetail);
                dbHistory.SaleOrderHistoryDetail.Add(dbHistoryDetail);
            }
            AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrder, SaleOrderHistory>(dtoItem, dbHistory);
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

            AutoMapper.Mapper.Map<DTO.SaleOrderMng.SaleOrder, SaleOrder>(dtoItem, dbItem);
        }

        public DTO.SaleOrderMng.PIContainerPrintout DB2DTO_Printout(SaleOrder_ReportView dbItem)
        {
            DTO.SaleOrderMng.PIContainerPrintout dtoItem = new DTO.SaleOrderMng.PIContainerPrintout();
            dtoItem.PIPrintouts = new List<DTO.SaleOrderMng.PIPrintout>();
            dtoItem.PIDetailPrintouts = new List<DTO.SaleOrderMng.PIDetailPrintout>();



            DTO.SaleOrderMng.PIPrintout dtoPI = AutoMapper.Mapper.Map<SaleOrder_ReportView, DTO.SaleOrderMng.PIPrintout>(dbItem);
            dtoItem.PIPrintouts.Add(dtoPI);

            //COPY PI DETAIL
            DTO.SaleOrderMng.PIDetailPrintout dtoPIDetail;

            foreach (SaleOrderExtend_ReportView dbExtend in dbItem.SaleOrderExtend_ReportView.Where(o => o.Position == "TOP").OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                dtoPIDetail.Description = dbExtend.Description;
                dtoPIDetail.SaleAmount = dbExtend.TotalAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
            }

            foreach (SaleOrderDetail_ReportView dbDetail in dbItem.SaleOrderDetail_ReportView.OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                dtoPIDetail.RowIndex = dbDetail.RowIndex;
                dtoPIDetail.ArticleCode = dbDetail.ArticleCode;
                dtoPIDetail.Description = dbDetail.Description;
                dtoPIDetail.OrderQnt = dbDetail.OrderQnt;
                dtoPIDetail.UnitPrice = dbDetail.UnitPrice;
                dtoPIDetail.SaleAmount = dbDetail.SaleAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);

                if (!string.IsNullOrEmpty(dbDetail.ClientArticleCode))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetail.ClientArticleCode;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbDetail.ClientArticleName))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetail.ClientArticleName;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbDetail.ClientEANCode))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetail.ClientEANCode;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbItem.ClientOrderNumber))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbItem.ClientOrderNumber;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbDetail.Reference))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetail.Reference;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                foreach (SaleOrderDetailExtend_ReportView dbDetailExt in dbDetail.SaleOrderDetailExtend_ReportView.OrderBy(o => o.RowIndex))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbDetailExt.Description;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }
            }

            foreach (SaleOrderDetailSample_ReportView dbSample in dbItem.SaleOrderDetailSample_ReportView.OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                dtoPIDetail.RowIndex = dbSample.RowIndex;
                dtoPIDetail.ArticleCode = dbSample.ArticleCode;
                dtoPIDetail.Description = dbSample.Description;
                dtoPIDetail.OrderQnt = dbSample.Quantity;
                dtoPIDetail.UnitPrice = dbSample.SalePrice;
                dtoPIDetail.SaleAmount = dbSample.SaleAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
            }

            foreach (SaleOrderDetailSparepart_ReportView dbSparepart in dbItem.SaleOrderDetailSparepart_ReportView.OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                dtoPIDetail.RowIndex = dbSparepart.RowIndex;
                dtoPIDetail.ArticleCode = dbSparepart.ArticleCode;
                dtoPIDetail.Description = dbSparepart.Description;
                dtoPIDetail.OrderQnt = dbSparepart.OrderQnt;
                dtoPIDetail.UnitPrice = dbSparepart.UnitPrice;
                dtoPIDetail.SaleAmount = dbSparepart.SaleAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);

                if (!string.IsNullOrEmpty(dbSparepart.ClientArticleCode))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbSparepart.ClientArticleCode;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbSparepart.ClientArticleName))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbSparepart.ClientArticleName;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbSparepart.ClientEANCode))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbSparepart.ClientEANCode;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

                if (!string.IsNullOrEmpty(dbItem.ClientOrderNumber))
                {
                    dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                    dtoPIDetail.Description = dbItem.ClientOrderNumber;
                    dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
                }

            }

            foreach (SaleOrderExtend_ReportView dbExtend in dbItem.SaleOrderExtend_ReportView.Where(o => o.Position == "BOTTOM").OrderBy(o => o.RowIndex))
            {
                dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                dtoPIDetail.Description = dbExtend.Description;
                dtoPIDetail.SaleAmount = dbExtend.TotalAmount;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
            }

            if (dbItem.IsViewLDSOnPrintout.HasValue && dbItem.IsViewLDSOnPrintout.Value && dbItem.LDS != "")
            {
                dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
                dtoPIDetail.Description = "LDS: " + dbItem.LDS;
                dtoItem.PIDetailPrintouts.Add(dtoPIDetail);
            }

            if (dbItem.IsViewDeliveryDateOnPrintout.HasValue && dbItem.IsViewDeliveryDateOnPrintout.Value && dbItem.DeliveryDate != "")
            {
                dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
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

            dtoPIDetail = new DTO.SaleOrderMng.PIDetailPrintout();
            dtoPIDetail.Description = bottomString;
            dtoItem.PIDetailPrintouts.Add(dtoPIDetail);

            return dtoItem;
        }

        public List<DTO.SaleOrderMng.PaymentStatus> DB2DTO_PaymentStatus(List<List_PaymentStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<List_PaymentStatus_View>, List<DTO.SaleOrderMng.PaymentStatus>>(dbItems);
        }

        public List<DTO.SaleOrderMng.LoadingPlan> DB2DTO_LoadingPlan(List<SaleOrderMng_LoadingPlan_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_LoadingPlan_View>, List<DTO.SaleOrderMng.LoadingPlan>>(dbItems);
        }

        public List<DTO.SaleOrderMng.LoadingPlan2> DB2DTO_LoadingPlan2(List<SaleOrderMng_LoadingPlan2_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_LoadingPlan2_View>, List<DTO.SaleOrderMng.LoadingPlan2>>(dbItems);
        }

        public DTO.SaleOrderMng.SaleOrderOverview DB2DTO_SaleOrderOverView(SaleOrderMng_SaleOrder_OverView dbItem)
        {
            DTO.SaleOrderMng.SaleOrderOverview dtoItem = AutoMapper.Mapper.Map<SaleOrderMng_SaleOrder_OverView, DTO.SaleOrderMng.SaleOrderOverview>(dbItem);

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

        public DTO.SaleOrderMng.LinkInfo DB2DTO_LinkInfo(SaleOrderMng_GetClientIDByOfferID_View dbItem)
        {
            return AutoMapper.Mapper.Map<SaleOrderMng_GetClientIDByOfferID_View, DTO.SaleOrderMng.LinkInfo>(dbItem);
        }

        public List<DTO.SaleOrderMng.SaleOrderStatusDTO> DB2DTO_SaleOrderStatus(List<List_TrackingStatus> dbItem)
        {
            return AutoMapper.Mapper.Map<List<List_TrackingStatus>, List<DTO.SaleOrderMng.SaleOrderStatusDTO>>(dbItem);
        }

        public List<DTO.SaleOrderMng.FactoryOrderDetailOverView> DB2DTO_FactoryOrderDetailOverView(List<SaleOrderMng_FactoryOrderDetail_OverView> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SaleOrderMng_FactoryOrderDetail_OverView>, List<DTO.SaleOrderMng.FactoryOrderDetailOverView>>(dbItem);
        }
    }
}
