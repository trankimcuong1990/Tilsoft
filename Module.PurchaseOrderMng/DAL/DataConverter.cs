using Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PurchaseOrderMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_SearchPurchaseOrder_View, DTO.PurchaseOrderSearchDto>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseOrderDate, o => o.ResolveUsing(s => s.PurchaseOrderDate.HasValue ? s.PurchaseOrderDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : " ")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseOrder_View, DTO.PurchaseOrderDto>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseOrderDate, o => o.ResolveUsing(s => s.PurchaseOrderDate.HasValue ? s.PurchaseOrderDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.PurchaseOrderDetailDTOs, o => o.MapFrom(s => s.PurchaseOrderMng_PurchaseOrderDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseOrderDetail_View, DTO.PurchaseOrderDetailDto>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileLocation) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.FileLocation)))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PurchaseOrderWorkOrderDetailDTOs, o => o.MapFrom(s => s.PurchaseOrderMng_PurchaseOrderWorkOrderDetail_View))
                    .ForMember(d => d.PurchaseOrderDetailReceivingPlanDtos, o => o.MapFrom(s => s.PurchaseOrderMng_PurchaseOrderDetailReceivingPlan_View))
                    .ForMember(d => d.PurchaseOrderDetailPriceHistoryDtos, o => o.MapFrom(s => s.PurchaseOrderMng_PurchaseOrderDetailPriceHistory_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseOrderWorkOrderDetail_View, DTO.PurchaseOrderWorkOrderDetailDto>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForMember(d => d.PurchaseOrderDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseOrderDetailReceivingPlan_View, DTO.PurchaseOrderDetailReceivingPlanDto>()
                    .ForMember(d => d.PlannedETA, o => o.ResolveUsing(s => s.PlannedETA.ToString("dd/MM/yyyy")))
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseOrderDetailPriceHistory_View, DTO.PurchaseOrderDetailPriceHistoryDto>()
                   .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.ToString("dd/MM/yyyy")))
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.PurchaseOrderWorkOrderDetailDto, PurchaseOrderWorkOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseOrderWorkOrderDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchaseOrderDetailReceivingPlanDto, PurchaseOrderDetailReceivingPlan>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PlannedETA, o => o.Ignore())
                    .ForMember(d => d.PurchaseOrderDetailReceivingPlanID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchaseOrderDetailPriceHistoryDto, PurchaseOrderDetailPriceHistory>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseOrderDetailPriceHistoryID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchaseOrderDetailDto, PurchaseOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PurchaseOrderWorkOrderDetail, o => o.Ignore())
                   .ForMember(d => d.PurchaseOrderDetailID, o => o.Ignore())
                   .ForMember(d => d.PurchaseOrderDetailReceivingPlan, o => o.Ignore())
                   .ForMember(d => d.PurchaseOrderDetailPriceHistory, o => o.Ignore())
                   .ForMember(d => d.ETA, o => o.Ignore())
                   .ForMember(d => d.PurchaseRequestDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchaseOrderDto, PurchaseOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseOrderID, o => o.Ignore())
                    .ForMember(d => d.PurchaseOrderUD, o => o.Ignore())
                    .ForMember(d => d.PurchaseOrderDate, o => o.Ignore())
                    .ForMember(d => d.ETA, o => o.Ignore())
                    .ForMember(d => d.IsApproved, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                    .ForMember(d => d.PurchaseOrderDetail, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_FactoryRawMaterial_View, DTO.FactoryRawMaterialDto>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                //AutoMapper.Mapper.CreateMap<PurchaseOrderMng_RequestingItem_View, DTO.PurchaseOrderRequestingItemDto>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseRequest_View, DTO.SupportPurchaseRequestDto>()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseRequestDetailApproval_View, DTO.PurchaseRequestDetailApproval>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => !s.ETA.HasValue ? string.Empty : s.ETA.Value.ToString("dd/MM/yyyy")))
                    .ForMember(d => d.PurchaseRequestWorkOrderDetails, o => o.MapFrom(s => s.PurchaseOrderMng_PurchaseRequestWorkOrder_View));

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseRequestWorkOrder_View, DTO.PurchaseRequestWorkOrderDetail>()
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => !s.FinishDate.HasValue ? string.Empty : s.FinishDate.Value.ToString("dd/MM/yyyy")))
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_PurchaseQuotationAndSupplier_View, DTO.PurchaseQuotationAndSupplierDto>()
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileLocation) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.FileLocation)))
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<PurchaseOrderMng_SupplierPaymentTerm_View, DTO.SupplierPaymentTermDto>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PurchaseOrderSearchDto> DB2DTO_PurchaseOrderSearch(List<PurchaseOrderMng_SearchPurchaseOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderMng_SearchPurchaseOrder_View>, List<DTO.PurchaseOrderSearchDto>>(dbItems);
        }

        public DTO.PurchaseOrderDto DB2DTO_PurchaseOrder(PurchaseOrderMng_PurchaseOrder_View dbItem)
        {
            return AutoMapper.Mapper.Map<PurchaseOrderMng_PurchaseOrder_View, DTO.PurchaseOrderDto>(dbItem);
        }

        public List<DTO.FactoryRawMaterialDto> DB2DTO_GetFactoryRawMaterial(List<PurchaseOrderMng_FactoryRawMaterial_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderMng_FactoryRawMaterial_View>, List<DTO.FactoryRawMaterialDto>>(dbItems);
        }

        //public List<DTO.PurchaseOrderRequestingItemDto> DB2DTO_GetPurchaseOrderMngRequestingItem(List<PurchaseOrderMng_RequestingItem_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<PurchaseOrderMng_RequestingItem_View>, List<DTO.PurchaseOrderRequestingItemDto>>(dbItems);
        //}

        public List<DTO.SupportPurchaseRequestDto> DB2DTO_GetPurchaseRequest(List<PurchaseOrderMng_PurchaseRequest_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderMng_PurchaseRequest_View>, List<DTO.SupportPurchaseRequestDto>>(dbItem);
        }

        public void DTO2DB_PurchaseOrder(DTO.PurchaseOrderDto dtoItem, ref PurchaseOrder dbItem)
        {
            if (dtoItem.PurchaseOrderDetailDTOs != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.PurchaseOrderDetail.ToArray())
                {
                    if (!dtoItem.PurchaseOrderDetailDTOs.Select(s => s.PurchaseOrderDetailID).Contains(item.PurchaseOrderDetailID))
                    {
                        //delete purchase order workorder item
                        foreach (var wItem in item.PurchaseOrderWorkOrderDetail.ToArray())
                        {
                            item.PurchaseOrderWorkOrderDetail.Remove(wItem);
                        }

                        //delete purchase order receiving plan item
                        foreach (var wItem in item.PurchaseOrderDetailReceivingPlan.ToArray())
                        {
                            item.PurchaseOrderDetailReceivingPlan.Remove(wItem);
                        }

                        //delete purchase order detail
                        dbItem.PurchaseOrderDetail.Remove(item);
                    }
                    else
                    {
                        var x = dtoItem.PurchaseOrderDetailDTOs.Where(o => o.PurchaseOrderDetailID == item.PurchaseOrderDetailID).FirstOrDefault().PurchaseOrderWorkOrderDetailDTOs;
                        var y = dtoItem.PurchaseOrderDetailDTOs.Where(o => o.PurchaseOrderDetailID == item.PurchaseOrderDetailID).FirstOrDefault().PurchaseOrderDetailReceivingPlanDtos;

                        //delete purchase order workorder detail
                        foreach (var wItem in item.PurchaseOrderWorkOrderDetail.ToArray())
                        {
                            if (!x.Select(s => s.PurchaseOrdertWorkOrderDetailID).Contains(wItem.PurchaseOrderWorkOrderDetailID))
                            {
                                item.PurchaseOrderWorkOrderDetail.Remove(wItem);
                            }
                        }

                        //delete purchase order receiving plan detail
                        foreach (var wItem in item.PurchaseOrderDetailReceivingPlan.ToArray())
                        {
                            if (!y.Select(s => s.PurchaseOrderDetailReceivingPlanID).Contains(wItem.PurchaseOrderDetailReceivingPlanID))
                            {
                                item.PurchaseOrderDetailReceivingPlan.Remove(wItem);
                            }
                        }
                    }
                }
                //read from dto to db
                PurchaseOrderDetail dbPurchaseDetail;
                PurchaseOrderWorkOrderDetail dbPurchaseWorkOrderDetail;
                PurchaseOrderDetailReceivingPlan dbPurchaseOrderDetailReceivingPlan;
                foreach (var item in dtoItem.PurchaseOrderDetailDTOs)
                {
                    if (item.PurchaseOrderDetailID > 0)
                    {
                        dbPurchaseDetail = dbItem.PurchaseOrderDetail.Where(o => o.PurchaseOrderDetailID == item.PurchaseOrderDetailID).FirstOrDefault();

                        //read from purchase order workorder detail dto to db
                        if (dbPurchaseDetail != null && item.PurchaseOrderWorkOrderDetailDTOs != null)
                        {
                            foreach (var wItem in item.PurchaseOrderWorkOrderDetailDTOs)
                            {
                                if (wItem.PurchaseOrdertWorkOrderDetailID > 0)
                                {
                                    dbPurchaseWorkOrderDetail = dbPurchaseDetail.PurchaseOrderWorkOrderDetail.Where(o => o.PurchaseOrderWorkOrderDetailID == wItem.PurchaseOrdertWorkOrderDetailID).FirstOrDefault();
                                }
                                else
                                {
                                    dbPurchaseWorkOrderDetail = new PurchaseOrderWorkOrderDetail();
                                    dbPurchaseDetail.PurchaseOrderWorkOrderDetail.Add(dbPurchaseWorkOrderDetail);
                                }
                                if (dbPurchaseWorkOrderDetail != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.PurchaseOrderWorkOrderDetailDto, PurchaseOrderWorkOrderDetail>(wItem, dbPurchaseWorkOrderDetail);
                                }
                            }
                        }

                        //read from purchase order detail receiving plan dto to db
                        if (dbPurchaseDetail != null && item.PurchaseOrderDetailReceivingPlanDtos != null)
                        {
                            foreach (var wItem in item.PurchaseOrderDetailReceivingPlanDtos)
                            {
                                if (wItem.PurchaseOrderDetailReceivingPlanID > 0)
                                {
                                    dbPurchaseOrderDetailReceivingPlan = dbPurchaseDetail.PurchaseOrderDetailReceivingPlan.Where(o => o.PurchaseOrderDetailReceivingPlanID == wItem.PurchaseOrderDetailReceivingPlanID).FirstOrDefault();
                                }
                                else
                                {
                                    dbPurchaseOrderDetailReceivingPlan = new PurchaseOrderDetailReceivingPlan();
                                    dbPurchaseDetail.PurchaseOrderDetailReceivingPlan.Add(dbPurchaseOrderDetailReceivingPlan);
                                }
                                if (dbPurchaseOrderDetailReceivingPlan != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.PurchaseOrderDetailReceivingPlanDto, PurchaseOrderDetailReceivingPlan>(wItem, dbPurchaseOrderDetailReceivingPlan);
                                    dbPurchaseOrderDetailReceivingPlan.PlannedETA = wItem.PlannedETA.ConvertStringToDateTime().Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        dbPurchaseDetail = new PurchaseOrderDetail();
                        dbItem.PurchaseOrderDetail.Add(dbPurchaseDetail);

                        //read from purchase order workorder detail dto to db
                        if (item.PurchaseOrderWorkOrderDetailDTOs != null)
                        {
                            foreach (var mItem in item.PurchaseOrderWorkOrderDetailDTOs)
                            {
                                dbPurchaseWorkOrderDetail = new PurchaseOrderWorkOrderDetail();
                                dbPurchaseDetail.PurchaseOrderWorkOrderDetail.Add(dbPurchaseWorkOrderDetail);
                                AutoMapper.Mapper.Map<DTO.PurchaseOrderWorkOrderDetailDto, PurchaseOrderWorkOrderDetail>(mItem, dbPurchaseWorkOrderDetail);
                            }
                        }

                        //read from purchase order receiving plan dto to db
                        if (item.PurchaseOrderDetailReceivingPlanDtos != null)
                        {
                            foreach (var mItem in item.PurchaseOrderDetailReceivingPlanDtos)
                            {
                                dbPurchaseOrderDetailReceivingPlan = new PurchaseOrderDetailReceivingPlan();
                                dbPurchaseDetail.PurchaseOrderDetailReceivingPlan.Add(dbPurchaseOrderDetailReceivingPlan);                                                               
                                AutoMapper.Mapper.Map<DTO.PurchaseOrderDetailReceivingPlanDto, PurchaseOrderDetailReceivingPlan>(mItem, dbPurchaseOrderDetailReceivingPlan);
                                dbPurchaseOrderDetailReceivingPlan.PlannedETA = mItem.PlannedETA.ConvertStringToDateTime().Value;
                            }                          
                        }
                    }
                    //read purchase request detail dto to db
                    if (dbPurchaseDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchaseOrderDetailDto, PurchaseOrderDetail>(item, dbPurchaseDetail);
                        dbPurchaseDetail.ETA = item.ETA.ConvertStringToDateTime();
                        dbPurchaseDetail.PurchaseRequestDetailID = item.PurchaseRequestDetailID;                      
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.PurchaseOrderDto, PurchaseOrder>(dtoItem, dbItem);
            dbItem.VAT = (decimal?)dtoItem.VAT;
            dbItem.PurchaseOrderDate = dtoItem.PurchaseOrderDate.ConvertStringToDateTime();
            dbItem.UpdatedDate = DateTime.Now;
            dbItem.ETA = dtoItem.ETA.ConvertStringToDateTime();
        }

        public List<DTO.PurchaseRequestDetailApproval> DB2DTO_PurchaseRequestDetailApproval(List<PurchaseOrderMng_PurchaseRequestDetailApproval_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderMng_PurchaseRequestDetailApproval_View>, List<DTO.PurchaseRequestDetailApproval>>(dbItems);
        }
       
        public List<DTO.SupplierPaymentTermDto> DB2DTO_SupplierPaymentTerm(List<PurchaseOrderMng_SupplierPaymentTerm_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderMng_SupplierPaymentTerm_View>, List<DTO.SupplierPaymentTermDto>>(dbItems);
        }
    }
}
