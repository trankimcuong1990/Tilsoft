using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Module.ReceivingNote.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ReceivingNoteMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_ReceivingNote_SearchView, DTO.ReceivingNoteSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceivingNoteDate, o => o.ResolveUsing(s => s.ReceivingNoteDate.HasValue ? s.ReceivingNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_ReceivingNote_View, DTO.ReceivingNoteDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceivingNoteDetailDTOs, o => o.MapFrom(s => s.ReceivingNoteMng_ReceivingNoteDetail_View))
                    .ForMember(d => d.ReceivingNoteDate, o => o.ResolveUsing(s => s.ReceivingNoteDate.HasValue ? s.ReceivingNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ResetDate, o => o.ResolveUsing(s => s.ResetDate.HasValue ? s.ResetDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmDate, o => o.ResolveUsing(s => s.ConfirmDate.HasValue ? s.ConfirmDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_ReceivingNoteDetail_View, DTO.ReceivingNoteDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceivingNoteWorkOrderDetailDTOs, o => o.MapFrom(s => s.ReceivingNoteMng_ReceivingNoteWorkOrderDetail_View))
                    .ForMember(d => d.ReceivingNoteDetailSubUnitDTOs, o => o.MapFrom(s => s.ReceivingNoteMng_ReceivingNoteDetailSubUnit_View))
                    //.ForMember(d => d.ProductionItemUnits, o => o.MapFrom(s => s.ReceivingNoteMng_ReceivingNoteDetail_ProductionItemUnit_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_ReceivingNoteWorkOrderDetail_View, DTO.ReceivingNoteWorkOrderDetailDTO>()
                  .IgnoreAllNonExisting()
                  .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_ReceivingNoteDetailSubUnit_View, DTO.ReceivingNoteDetailSubUnitDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ReceivingNoteWorkOrderDetailDTO, ReceivingNoteWorkOrderDetail>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.ReceivingNoteWorkOrderDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ReceivingNoteDetailSubUnitDTO, ReceivingNoteDetailSubUnit>()
                  .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.ReceivingNoteDetailDTO, ReceivingNoteDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceivingNoteWorkOrderDetail, o => o.Ignore())
                    .ForMember(d => d.ReceivingNoteDetailSubUnit, o => o.Ignore())
                    .ForMember(d => d.ReceivingNoteDetailID, o => o.Ignore())
                    .ForMember(d => d.Weight, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ReceivingNoteDTO, ReceivingNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceivingNoteDetail, o => o.Ignore())
                    .ForMember(d => d.ReceivingNoteDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmDate, o => o.Ignore())
                    .ForMember(d => d.ResetDate, o => o.Ignore())
                    .ForMember(d => d.ReceivingNoteID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_function_GetProductionItem_Result, DTO.ProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_BOM_View, DTO.BOMDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_WorkCenter_View, DTO.WorkCenter>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_WorkOrder_SearchView, DTO.WorkOrderSearchDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SubSupplierQuickSearch_View, DTO.SubSupplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_PurchaseOrder_View, DTO.PurchaseOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_PurchaseOrderDetail_View, DTO.PurchaseOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.PurchaseOrderWorkOrderDetailDTOs, o => o.MapFrom(s => s.ReceivingNoteMng_PurchaseOrderWorkOrderDetail_View.OrderBy(x => x.FinishDate)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_PurchaseOrderWorkOrderDetail_View, DTO.PurchaseOrderWorkOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_Function_ProductionItemUnitByValidDate_Result, DTO.ProductionItemUnit>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_WorkOrderItem_View, DTO.WorkOrderItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.FinishedDate, o => o.ResolveUsing(s => s.FinishedDate.HasValue ? s.FinishedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkCenter, DTO.WorkCenter>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_SetContentDetail_View, DTO.SetContentDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_BOMProduction_View, DTO.BOMProductionDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_TransportationService_View, DTO.TransportationServiceDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_FactorySaleOrder_View, DTO.FactorySaleOrderDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ReceivingNoteMng_FactorySaleOrderDetail_View, DTO.FactorySaleOrderDetailDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ReceivingNoteSearchDTO> DB2DTO_ReceivingNoteSearch(List<ReceivingNoteMng_ReceivingNote_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceivingNoteMng_ReceivingNote_SearchView>, List<DTO.ReceivingNoteSearchDTO>>(dbItems);
        }

        public DTO.ReceivingNoteDTO DB2DTO_ReceivingNote(ReceivingNoteMng_ReceivingNote_View dbItem)
        {
            return AutoMapper.Mapper.Map<ReceivingNoteMng_ReceivingNote_View, DTO.ReceivingNoteDTO>(dbItem);
        }

        public void DTO2DB_ReceivingNote(DTO.ReceivingNoteDTO dtoItem, ref ReceivingNote dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ReceivingNoteDTO, ReceivingNote>(dtoItem, dbItem);
            dbItem.ReceivingNoteDate = dtoItem.ReceivingNoteDate.ConvertStringToDateTime();

            if (dtoItem.ReceivingNoteDetailDTOs != null)
            {
                //delete item in db that exist in dto but not exist in db
                foreach (var item in dbItem.ReceivingNoteDetail.ToArray())
                {
                    if (!dtoItem.ReceivingNoteDetailDTOs.Where(o => o.Quantity.HasValue /*&& o.Quantity.Value > 0*/).Select(s => s.ReceivingNoteDetailID).Contains(item.ReceivingNoteDetailID))
                    {
                        foreach (var wItem in item.ReceivingNoteWorkOrderDetail.ToArray())
                        {
                            item.ReceivingNoteWorkOrderDetail.Remove(wItem);
                        }

                        foreach (var subUnitItem in item.ReceivingNoteDetailSubUnit.ToArray())
                        {
                            item.ReceivingNoteDetailSubUnit.Remove(subUnitItem);
                        }

                        dbItem.ReceivingNoteDetail.Remove(item);
                    }
                    else
                    {
                        var x = dtoItem.ReceivingNoteDetailDTOs.Where(o => o.ReceivingNoteDetailID == item.ReceivingNoteDetailID).FirstOrDefault().ReceivingNoteWorkOrderDetailDTOs;

                        foreach (var wItem in item.ReceivingNoteWorkOrderDetail.ToArray())
                        {
                            if (!x.Select(s => s.ReceivingNoteWorkOrderDetailID).Contains(wItem.ReceivingNoteWorkOrderDetailID))
                            {
                                item.ReceivingNoteWorkOrderDetail.Remove(wItem);
                            }
                        }

                        var y = dtoItem.ReceivingNoteDetailDTOs.Where(o => o.ReceivingNoteDetailID == item.ReceivingNoteDetailID).FirstOrDefault().ReceivingNoteDetailSubUnitDTOs;
                        foreach (var subUnitItem in item.ReceivingNoteDetailSubUnit.ToArray())
                        {
                            if (!y.Select(s => s.ReceivingNoteDetailSubUnitID).Contains(subUnitItem.ReceivingNoteDetailSubUnitID))
                            {
                                item.ReceivingNoteDetailSubUnit.Remove(subUnitItem);
                            }
                        }
                    }
                }

                //read from dto to db
                ReceivingNoteDetail dbReceivingNoteDetail;
                ReceivingNoteWorkOrderDetail dbReceivingNoteWorkOrderDetail;
                ReceivingNoteDetailSubUnit dbReceivingNoteDetailSubUnit;
                foreach (var item in dtoItem.ReceivingNoteDetailDTOs)
                {
                    if (item.ReceivingNoteDetailID > 0)
                    {
                        if (!item.Quantity.HasValue || item.Quantity.Value == 0)
                        {
                            throw new Exception("Please enter a number for the quantity column");
                        }

                        dbReceivingNoteDetail = dbItem.ReceivingNoteDetail.Where(o => o.ReceivingNoteDetailID == item.ReceivingNoteDetailID).FirstOrDefault();

                        // ReceivingNoteWorkOrderDetail
                        if (dbReceivingNoteDetail != null && item.ReceivingNoteWorkOrderDetailDTOs != null)
                        {
                            foreach (var wItem in item.ReceivingNoteWorkOrderDetailDTOs)
                            {
                                if (wItem.ReceivingNoteWorkOrderDetailID > 0)
                                {
                                    dbReceivingNoteWorkOrderDetail = dbReceivingNoteDetail.ReceivingNoteWorkOrderDetail.Where(o => o.ReceivingNoteWorkOrderDetailID == wItem.ReceivingNoteWorkOrderDetailID).FirstOrDefault();
                                }
                                else
                                {
                                    dbReceivingNoteWorkOrderDetail = new ReceivingNoteWorkOrderDetail();
                                    dbReceivingNoteDetail.ReceivingNoteWorkOrderDetail.Add(dbReceivingNoteWorkOrderDetail);
                                }
                                if (dbReceivingNoteWorkOrderDetail != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ReceivingNoteWorkOrderDetailDTO, ReceivingNoteWorkOrderDetail>(wItem, dbReceivingNoteWorkOrderDetail);
                                }
                            }
                        }

                        //ReceivingNoteDetailSubUnit
                        if (dbReceivingNoteDetail != null && item.ReceivingNoteDetailSubUnitDTOs != null)
                        {
                            foreach (var wItem in item.ReceivingNoteDetailSubUnitDTOs)
                            {
                                if (wItem.ReceivingNoteDetailSubUnitID > 0)
                                {
                                    dbReceivingNoteDetailSubUnit = dbReceivingNoteDetail.ReceivingNoteDetailSubUnit.Where(o => o.ReceivingNoteDetailSubUnitID == wItem.ReceivingNoteDetailSubUnitID).FirstOrDefault();
                                }
                                else
                                {
                                    dbReceivingNoteDetailSubUnit = new ReceivingNoteDetailSubUnit();
                                    dbReceivingNoteDetail.ReceivingNoteDetailSubUnit.Add(dbReceivingNoteDetailSubUnit);
                                }
                                if (dbReceivingNoteDetailSubUnit != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ReceivingNoteDetailSubUnitDTO, ReceivingNoteDetailSubUnit>(wItem, dbReceivingNoteDetailSubUnit);
                                }
                            }
                        }
                    }
                    else
                    {
                        dbReceivingNoteDetail = new ReceivingNoteDetail();
                        if (item.ProductionItemID.HasValue && item.Quantity.HasValue && item.Quantity.Value > 0)
                        {
                            dbItem.ReceivingNoteDetail.Add(dbReceivingNoteDetail);

                            // ReceivingNoteWorkOrderDetail
                            if (item.ReceivingNoteWorkOrderDetailDTOs != null)
                            {
                                foreach (var mItem in item.ReceivingNoteWorkOrderDetailDTOs)
                                {
                                    dbReceivingNoteWorkOrderDetail = new ReceivingNoteWorkOrderDetail();
                                    dbReceivingNoteDetail.ReceivingNoteWorkOrderDetail.Add(dbReceivingNoteWorkOrderDetail);
                                    AutoMapper.Mapper.Map<DTO.ReceivingNoteWorkOrderDetailDTO, ReceivingNoteWorkOrderDetail>(mItem, dbReceivingNoteWorkOrderDetail);
                                }
                            }

                            //ReceivingNoteDetailSubUnit
                            if (item.ReceivingNoteDetailSubUnitDTOs != null)
                            {
                                foreach (var mItem in item.ReceivingNoteDetailSubUnitDTOs)
                                {
                                    dbReceivingNoteDetailSubUnit = new ReceivingNoteDetailSubUnit();
                                    dbReceivingNoteDetail.ReceivingNoteDetailSubUnit.Add(dbReceivingNoteDetailSubUnit);
                                    AutoMapper.Mapper.Map<DTO.ReceivingNoteDetailSubUnitDTO, ReceivingNoteDetailSubUnit>(mItem, dbReceivingNoteDetailSubUnit);
                                }
                            }
                        }
                    }
                    //read detail dto to db
                    if (dbReceivingNoteDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ReceivingNoteDetailDTO, ReceivingNoteDetail>(item, dbReceivingNoteDetail);
                        dbReceivingNoteDetail.Weight = item.FrameWeight;
                    }
                }
            }
        }

        public List<DTO.ProductionItem> DB2DTO_ProductionItem(List<ReceivingNoteMng_function_GetProductionItem_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceivingNoteMng_function_GetProductionItem_Result>, List<DTO.ProductionItem>>(dbItems);
        }

        public List<DTO.WorkCenter> DB2DTO_WorkCenter(List<ReceivingNoteMng_WorkCenter_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceivingNoteMng_WorkCenter_View>, List<DTO.WorkCenter>>(dbItems);
        }

        public List<DTO.BOMDTO> DB2DTO_BOM(List<ReceivingNoteMng_BOM_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ReceivingNoteMng_BOM_View>, List<DTO.BOMDTO>>(dbItems);
        }

        public List<DTO.SubSupplier> DB2DTO_SubSupplier(List<SupportMng_SubSupplierQuickSearch_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SubSupplierQuickSearch_View>, List<DTO.SubSupplier>>(dbItems);
        }

        public List<DTO.WorkOrderItemDTO> DB2DTO_WorkOrderItem(List<ReceivingNoteMng_WorkOrderItem_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ReceivingNoteMng_WorkOrderItem_View>, List<DTO.WorkOrderItemDTO>>(dbItem);
        }
    }
}
