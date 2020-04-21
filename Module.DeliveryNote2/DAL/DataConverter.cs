using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Module.DeliveryNote2.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DeliveryNote2Mng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_DeliveryNote_SearchView, DTO.DeliveryNoteSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.factoryWareHouseSearchDTOs, o => o.MapFrom(s => s.DeliveryNoteMng_FactoryWarehouseList_View))
                    .ForMember(d => d.DeliveryNoteDate, o => o.ResolveUsing(s => s.DeliveryNoteDate.HasValue ? s.DeliveryNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_DeliveryNote_View, DTO.DeliveryNoteDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DeliveryNoteDetailDTOs, o => o.MapFrom(s => s.DeliveryNoteMng_DeliveryNoteDetail_View))
                    .ForMember(d => d.DeliveryNoteDate, o => o.ResolveUsing(s => s.DeliveryNoteDate.HasValue ? s.DeliveryNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ResetDate, o => o.ResolveUsing(s => s.ResetDate.HasValue ? s.ResetDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_DeliveryNoteDetail_View, DTO.DeliveryNoteDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.DeliveryNoteDetailDTO, DeliveryNoteDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DeliveryNoteDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.DeliveryNoteDTO, DeliveryNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DeliveryNoteDetail, o => o.Ignore())
                    .ForMember(d => d.DeliveryNoteDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                    .ForMember(d => d.ResetDate, o => o.Ignore())
                    .ForMember(d => d.DeliveryNoteID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_BOM_View, DTO.BOMDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_WorkCenter_View, DTO.WorkCenterDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_WorkOrder_SearchView, DTO.WorkOrderSearchDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_FactoryWarehouseList_View, DTO.FactoryWareHouseSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_SubSupplierQuickSearch_View, DTO.SubSupplier>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_SupportFactoryWaseHouseList_View, DTO.SupportFactoryWareHouseList>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_DeliveryNote_FormView, DTO.DeliveryNoteFormViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.deliveryNoteDetailFormViewDTOs, o => o.MapFrom(s => s.DelivetyNoteMng_DeliveryNoteDetail_FormView))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.DeliveryNoteDate, o => o.ResolveUsing(s => s.DeliveryNoteDate.HasValue ? s.DeliveryNoteDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DelivetyNoteMng_DeliveryNoteDetail_FormView, DTO.DeliveryNoteDetailFormViewDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_SupportProductionItem_View, DTO.ProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_Function_ProductionItemUnitByValidDate_Result, DTO.ProductionItemUnit>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrder, DTO.WorkOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_QuickSearchFactorySaleOrder_View, DTO.FactorySaleOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_QuickSearchFactorySaleOrderDetail_View, DTO.FactorySaleOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_ListProductionItemUnit_View, DTO.ListProductionItemUnit>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_Function_ProductionItemUnitByValidDate_Result, DTO.ProductionItemUnitBOM>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNote2Mng_PurchaseOrder_View, DTO.PurchaseOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNote2Mng_PurchaseOrderDetail_View, DTO.PurchaseOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StockQnt, o => o.ResolveUsing(s => s.StockingQnt))
                    .ForMember(d => d.TotalDelivery, o => o.ResolveUsing(s => s.TotalDelivering))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_TransportationService_View, DTO.TransportationServiceDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_OutsourcingCost_View, DTO.OutsourcingCostDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DeliveryNoteMng_Function_SupportProductionItem_View_Result, DTO.ProductionItem>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.DeliveryNoteSearchDTO> DB2DTO_DeliveryNoteSearch(List<DeliveryNoteMng_DeliveryNote_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DeliveryNoteMng_DeliveryNote_SearchView>, List<DTO.DeliveryNoteSearchDTO>>(dbItems);
        }

        public DTO.DeliveryNoteDTO DB2DTO_DeliveryNote(DeliveryNoteMng_DeliveryNote_View dbItem)
        {
            return AutoMapper.Mapper.Map<DeliveryNoteMng_DeliveryNote_View, DTO.DeliveryNoteDTO>(dbItem);
        }

        public DTO.DeliveryNoteFormViewDTO DB2DTO_DeliveryNoteFormView(DeliveryNoteMng_DeliveryNote_FormView dbItem)
        {
            return AutoMapper.Mapper.Map<DeliveryNoteMng_DeliveryNote_FormView, DTO.DeliveryNoteFormViewDTO>(dbItem);
        }

        public List<DTO.SupportFactoryWareHouseList> DB2DTO_GetlistWareHouseList(List<DeliveryNoteMng_SupportFactoryWaseHouseList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DeliveryNoteMng_SupportFactoryWaseHouseList_View>, List<DTO.SupportFactoryWareHouseList>>(dbItems);
        }

        public void DTO2DB_DeliveryNote(DTO.DeliveryNoteDTO dtoItem, ref DeliveryNote dbItem)
        {
            if (dtoItem.DeliveryNoteDetailDTOs != null)
            {
                foreach (var item in dbItem.DeliveryNoteDetail.ToArray())
                {
                    if (!dtoItem.DeliveryNoteDetailDTOs.Where(o => o.Qty.HasValue/* && o.Qty>0*/).Select(s => s.DeliveryNoteDetailID).Contains(item.DeliveryNoteDetailID))
                    {
                        dbItem.DeliveryNoteDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.DeliveryNoteDetailDTOs)
                {
                    DeliveryNoteDetail dbDetail = new DeliveryNoteDetail();
                    if (item.DeliveryNoteDetailID < 0)
                    {
                        if (item.ProductionItemID.HasValue && item.Qty.HasValue && item.Qty.Value > 0)
                        {
                            dbItem.DeliveryNoteDetail.Add(dbDetail);
                        }
                    }
                    else
                    {
                        dbDetail = dbItem.DeliveryNoteDetail.Where(o => o.DeliveryNoteDetailID == item.DeliveryNoteDetailID).FirstOrDefault();

                        if (!item.Qty.HasValue || item.Qty.Value == 0)
                        {
                            throw new Exception("Please enter a number for the quantity column");
                        }
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.DeliveryNoteDetailDTO, DeliveryNoteDetail>(item, dbDetail);
                    }
                }
            }
            AutoMapper.Mapper.Map<DTO.DeliveryNoteDTO, DeliveryNote>(dtoItem, dbItem);

            dbItem.DeliveryNoteDate = dtoItem.DeliveryNoteDate.ConvertStringToDateTime();
        }

        public List<DTO.SubSupplier> DB2DTO_GetSubSuppliers(List<SupportMng_SubSupplierQuickSearch_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_SubSupplierQuickSearch_View>, List<DTO.SubSupplier>>(dbItem);
        }

        public List<DTO.ListProductionItemUnit> DB2DTO_GetProductionItemUnit(List<DeliveryNoteMng_ListProductionItemUnit_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<DeliveryNoteMng_ListProductionItemUnit_View>, List<DTO.ListProductionItemUnit>>(dbItem);
        }
    }
}
