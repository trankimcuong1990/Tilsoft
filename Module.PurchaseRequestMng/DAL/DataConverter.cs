using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Library;
namespace Module.PurchaseRequestMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PurchaseRequestMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_PurchaseRequestSearch_View, DTO.PurchaseRequestSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseRequestDate, o => o.ResolveUsing(s => s.PurchaseRequestDate.HasValue ? s.PurchaseRequestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RequestedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_PurchaseRequest_View, DTO.PurchaseRequestDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseRequestDate, o => o.ResolveUsing(s => s.PurchaseRequestDate.HasValue ? s.PurchaseRequestDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.RequestedDate, o => o.ResolveUsing(s => s.RequestedDate.HasValue ? s.RequestedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.PurchaseRequestDetailDTOs, o => o.MapFrom(s => s.PurchaseRequestMng_PurchaseRequestDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_PurchaseRequestDetail_View, DTO.PurchaseRequestDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseRequestWorkOrderDetailDTOs, o => o.MapFrom(s => s.PurchaseRequestMng_PurchaseRequestWorkOrderDetail_View))
                    .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_PurchaseRequestWorkOrderDetail_View, DTO.PurchaseRequestWorkOrderDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PurchaseRequestDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchaseRequestWorkOrderDetailDTO, PurchaseRequestWorkOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseRequestWorkOrderDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchaseRequestDetailDTO, PurchaseRequestDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.PurchaseRequestWorkOrderDetail, o => o.Ignore())
                   .ForMember(d => d.ETA, o => o.Ignore())
                   .ForMember(d => d.PurchaseRequestDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.PurchaseRequestDTO, PurchaseRequest>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseRequestID, o => o.Ignore())
                    .ForMember(d => d.PurchaseRequestUD, o => o.Ignore())
                    .ForMember(d => d.PurchaseRequestDate, o => o.Ignore())
                    .ForMember(d => d.ETA, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ApprovedBy, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                    .ForMember(d => d.PurchaseRequestDetail, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_PurchaseRequestStatus_View, DTO.PurchaseRequestStatusDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_PurchaseQuotation_View, DTO.PurchaseQuotationDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ValidFrom, o => o.ResolveUsing(s => s.ValidFrom.HasValue ? s.ValidFrom.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ValidTo, o => o.ResolveUsing(s => s.ValidTo.HasValue ? s.ValidTo.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_PurchaseQuotationDetail_View, DTO.PurchaseQuotationDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestDetailApproval, DTO.PurchaseRequestDetailApprovalDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_ProductionItemGroup_View, DTO.ProductionItemGroupDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_ProductionItem_View, DTO.ProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PurchaseRequestMng_function_GetProductionItemByWorkOrder2_Result, DTO.ProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.PurchaseRequestSearchDTO> DB2DTO_PurchaseRequestSearch(List<PurchaseRequestMng_PurchaseRequestSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PurchaseRequestMng_PurchaseRequestSearch_View>, List<DTO.PurchaseRequestSearchDTO>>(dbItems);
        }

        public DTO.PurchaseRequestDTO DB2DTO_PurchaseRequest(PurchaseRequestMng_PurchaseRequest_View dbItem)
        {
            return AutoMapper.Mapper.Map<PurchaseRequestMng_PurchaseRequest_View, DTO.PurchaseRequestDTO>(dbItem);
        }

        public void DTO2DB_PurchaseRequest(DTO.PurchaseRequestDTO dtoItem, ref PurchaseRequest dbItem)
        {
            if (dtoItem.PurchaseRequestDetailDTOs != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.PurchaseRequestDetail.ToArray())
                {
                    if (!dtoItem.PurchaseRequestDetailDTOs.Select(s => s.PurchaseRequestDetailID).Contains(item.PurchaseRequestDetailID))
                    {
                        //delete purchase request workorder item
                        foreach (var wItem in item.PurchaseRequestWorkOrderDetail.ToArray())
                        {
                            item.PurchaseRequestWorkOrderDetail.Remove(wItem);
                        }

                        //delete purchase request detail
                        dbItem.PurchaseRequestDetail.Remove(item);
                    }
                    else
                    {
                        var x = dtoItem.PurchaseRequestDetailDTOs.Where(o => o.PurchaseRequestDetailID == item.PurchaseRequestDetailID).FirstOrDefault().PurchaseRequestWorkOrderDetailDTOs;

                        //delete purchase request detail
                        foreach (var wItem in item.PurchaseRequestWorkOrderDetail.ToArray())
                        {
                            if (!x.Select(s => s.PurchaseRequestWorkOrderDetailID).Contains(wItem.PurchaseRequestWorkOrderDetailID))
                            {
                                item.PurchaseRequestWorkOrderDetail.Remove(wItem);
                            }
                        }
                    }
                }
                //read from dto to db
                PurchaseRequestDetail dbPurchaseDetail;
                PurchaseRequestWorkOrderDetail dbPurchaseWorkOrderDetail;
                foreach (var item in dtoItem.PurchaseRequestDetailDTOs)
                {
                    if (item.PurchaseRequestDetailID > 0)
                    {
                        dbPurchaseDetail = dbItem.PurchaseRequestDetail.Where(o => o.PurchaseRequestDetailID == item.PurchaseRequestDetailID).FirstOrDefault();

                        //read from purchase request workorder detail dto to db
                        if (dbPurchaseDetail != null && item.PurchaseRequestWorkOrderDetailDTOs != null)
                        {
                            foreach (var wItem in item.PurchaseRequestWorkOrderDetailDTOs)
                            {
                                if (wItem.PurchaseRequestWorkOrderDetailID > 0)
                                {
                                    dbPurchaseWorkOrderDetail = dbPurchaseDetail.PurchaseRequestWorkOrderDetail.Where(o => o.PurchaseRequestWorkOrderDetailID == wItem.PurchaseRequestWorkOrderDetailID).FirstOrDefault();
                                }
                                else
                                {
                                    dbPurchaseWorkOrderDetail = new PurchaseRequestWorkOrderDetail();
                                    dbPurchaseDetail.PurchaseRequestWorkOrderDetail.Add(dbPurchaseWorkOrderDetail);
                                }
                                if (dbPurchaseWorkOrderDetail != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.PurchaseRequestWorkOrderDetailDTO, PurchaseRequestWorkOrderDetail>(wItem, dbPurchaseWorkOrderDetail);

                                }
                            }
                        }
                    }
                    else
                    {
                        if (item.ProductionItemID == null || item.RequestQnt == null)
                        {
                            continue;
                        }

                        dbPurchaseDetail = new PurchaseRequestDetail();
                        dbItem.PurchaseRequestDetail.Add(dbPurchaseDetail);

                        //read from purchase request workorder detail dto to db
                        if (item.PurchaseRequestWorkOrderDetailDTOs != null)
                        {
                            foreach (var mItem in item.PurchaseRequestWorkOrderDetailDTOs)
                            {
                                dbPurchaseWorkOrderDetail = new PurchaseRequestWorkOrderDetail();
                                dbPurchaseDetail.PurchaseRequestWorkOrderDetail.Add(dbPurchaseWorkOrderDetail);
                                AutoMapper.Mapper.Map<DTO.PurchaseRequestWorkOrderDetailDTO, PurchaseRequestWorkOrderDetail>(mItem, dbPurchaseWorkOrderDetail);
                            }
                        }
                    }

                    //read purchase request detail dto to db
                    if (dbPurchaseDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.PurchaseRequestDetailDTO, PurchaseRequestDetail>(item, dbPurchaseDetail);
                        dbPurchaseDetail.ETA = item.ETA.ConvertStringToDateTime();
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.PurchaseRequestDTO, PurchaseRequest>(dtoItem, dbItem);
            dbItem.PurchaseRequestDate = dtoItem.PurchaseRequestDate.ConvertStringToDateTime();
            dbItem.ETA = dtoItem.ETA.ConvertStringToDateTime();
        }

        public List<DTO.ProductionItemDTO> DB2DTO_ProductionItem(List<PurchaseRequestMng_ProductionItem_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseRequestMng_ProductionItem_View>, List<DTO.ProductionItemDTO>>(dbItem);
        }

        public List<DTO.ProductionItemDTO> DB2DTO_ProductionItem_2(List<PurchaseRequestMng_function_GetProductionItemByWorkOrder2_Result> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseRequestMng_function_GetProductionItemByWorkOrder2_Result>, List<DTO.ProductionItemDTO>>(dbItem);
        }
    }
}
