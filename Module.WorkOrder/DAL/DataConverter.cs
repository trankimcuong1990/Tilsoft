namespace Module.WorkOrder.DAL
{
    using Library;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WorkOrderMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WorkOrder_SearchView, DTO.WorkOrderSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDateBOM, o => o.ResolveUsing(s => s.CreatedDateBOM.HasValue ? s.CreatedDateBOM.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SetStatusDate, o => o.ResolveUsing(s => s.SetStatusDate.HasValue ? s.SetStatusDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WorkOrder_View, DTO.WorkOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SetStatusDate, o => o.ResolveUsing(s => s.SetStatusDate.HasValue ? s.SetStatusDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WorkOrderDetailDTOs, o => o.MapFrom(s => s.WorkOrderMng_WorkOrderDetail_View))
                    .ForMember(d => d.WorkOrderOPSequences, o => o.MapFrom(s => s.WorkOrderMng_WorkOrderOPSequence_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.WorkOrderDetailDTO, WorkOrderDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WorkOrderDetailID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.WorkOrderDTO, WorkOrder>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.StartDate, o => o.Ignore())
                    .ForMember(d => d.FinishDate, o => o.Ignore())
                    .ForMember(d => d.SetStatusDate, o => o.Ignore())
                    .ForMember(d => d.WorkOrderDetail, o => o.Ignore())
                    .ForMember(d => d.WorkOrderID, o => o.Ignore())
                    .ForMember(d => d.WorkOrderOPSequence, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<WorkOrderMng_Product_View, DTO.ProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_SaleOrder_View, DTO.SaleOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_FactoryOrderDetail_View, DTO.FactoryOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WorkOrderDetail_View, DTO.WorkOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_SampleOrder_View, DTO.SampleOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.SampleProductDTOs, o => o.MapFrom(s => s.WorkOrderMng_SampleProduct_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_SampleProduct_View, DTO.SampleProductDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_BOM_View, DTO.BOMDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BOMDTOs, o => o.MapFrom(s => s.WorkOrderMng_BOM_View1))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_PreOrderWorkOrder_View, DTO.PreOrderWorkOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WeavingStatus_View, DTO.WeavingStatus>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemGroup_View, DTO.ProductionItemGroupDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<SupportMng_WorkCenter_View, DTO.WorkCenterDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WorkOrderListPreOrder_View, DTO.WorkOrderListPreOrderDTO>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WorkOrderOPSequence_View, DTO.WorkOrderOPSequenceDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_OPSequenceDetail_View, DTO.OPSequenceDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.WorkOrderOPSequenceDTO, WorkOrderOPSequence>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WorkOrderOPSequenceID, o => o.Ignore())
                    .ForMember(d => d.WorkOrderID, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<WorkOrderMng_PreOrderWorkOrderManagement_View, DTO.PreOrderWorkOrderManagement>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WorkOrderBaseOnManagement_View, DTO.WorkOrderBaseOnManagement>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_PreOrderWorkOrderBaseOnManagement_View, DTO.PreOrderWorkOrderBaseOnManagement>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_HistoryTransferPreOrderToWorkOrder_View, DTO.HistoryTransferPreOrderToWorkOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TransferWorkOrderDate, o => o.ResolveUsing(s => s.TransferWorkOrderDate.HasValue ? s.TransferWorkOrderDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_TransferWorkOrder_View, DTO.TransferWorkOrderDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TransferWorkOrderDate, o => o.ResolveUsing(s => s.TransferWorkOrderDate.HasValue ? s.TransferWorkOrderDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.TransferWorkOrderDetails, o => o.MapFrom(s => s.WorkOrderMng_TransferWorkOrderDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_TransferWorkOrderDetail_View, DTO.TransferWorkOrderDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WorkOrderChild_View, DTO.WorkOrderReportDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_ProductionItem_View, DTO.ProductionItemDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_WorkOrderReportHeader_View, DTO.WorkOrderReportHeaderDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderMng_ItemNotBOM_View, DTO.ItemNotBOMDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WorkOrderReportHeaderDTO> DB2DTO_WorkOrderReportHeaderDTO(List<WorkOrderMng_WorkOrderReportHeader_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_WorkOrderReportHeader_View>, List<DTO.WorkOrderReportHeaderDTO>>(dbItems);
        }
        public List<DTO.ProductionItemDTO> DB2DTO_ProductionItemDTO(List<WorkOrderMng_ProductionItem_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_ProductionItem_View>, List<DTO.ProductionItemDTO>>(dbItems);
        }
        public List<DTO.WorkOrderReportDTO> DB2DTO_WorkOrderReportDTOs(List<WorkOrderMng_WorkOrderChild_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_WorkOrderChild_View>, List<DTO.WorkOrderReportDTO>>(dbItems);
        }

        public List<DTO.WorkOrderSearchDTO> DB2DTO_WorkOrderSearch(List<WorkOrderMng_WorkOrder_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_WorkOrder_SearchView>, List<DTO.WorkOrderSearchDTO>>(dbItems);
        }

        public DTO.WorkOrderDTO DB2DTO_WorkOrder(WorkOrderMng_WorkOrder_View dbItem)
        {
            return AutoMapper.Mapper.Map<WorkOrderMng_WorkOrder_View, DTO.WorkOrderDTO>(dbItem);
        }

        //public DTO.ReceivingDeliveryDTO DB2DTO_ReceivingDelivery(WorkOrderMng_ReceivingDelivery_View delivery_View)
        //{
        //    return AutoMapper.Mapper.Map<WorkOrderMng_ReceivingDelivery_View, DTO.ReceivingDeliveryDTO>(delivery_View);
        //}

        //public List<DTO.ReceivingDeliveryDTO> DB2DTO_ReceivingDeliveryList(List<WorkOrderMng_ReceivingDelivery_View> delivery_View)
        //{
        //    return AutoMapper.Mapper.Map<List<WorkOrderMng_ReceivingDelivery_View>, List<DTO.ReceivingDeliveryDTO>>(delivery_View);
        //}

        //public DTO.WorkOrderSummaryDTO DB2DTO_Summary(WorkOrderMng_Summary_View workOrderMng_Summary_View)
        //{
        //    return AutoMapper.Mapper.Map<WorkOrderMng_Summary_View, DTO.WorkOrderSummaryDTO>(workOrderMng_Summary_View);
        //}

        public void DTO2DB_WorkOrder(DTO.WorkOrderDTO dtoItem, ref WorkOrder dbItem)
        {
            if (dtoItem.WorkOrderDetailDTOs != null)
            {
                foreach (var item in dbItem.WorkOrderDetail.ToArray())
                {
                    if (!dtoItem.WorkOrderDetailDTOs.Select(s => s.WorkOrderDetailID).Contains(item.WorkOrderDetailID))
                    {
                        dbItem.WorkOrderDetail.Remove(item);
                    }
                }
                foreach (var item in dtoItem.WorkOrderDetailDTOs)
                {
                    WorkOrderDetail dbDetail = new WorkOrderDetail();
                    if (item.WorkOrderDetailID < 0)
                    {
                        dbItem.WorkOrderDetail.Add(dbDetail);
                    }
                    else
                    {
                        dbDetail = dbItem.WorkOrderDetail.Where(o => o.WorkOrderDetailID == item.WorkOrderDetailID).FirstOrDefault();
                    }
                    if (dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WorkOrderDetailDTO, WorkOrderDetail>(item, dbDetail);
                    }
                }
            }

            if (dtoItem.WorkOrderOPSequences != null)
            {
                foreach (var item in dbItem.WorkOrderOPSequence.ToList())
                {
                    if (!dtoItem.WorkOrderOPSequences.Select(s => s.WorkOrderOPSequenceID).Contains(item.WorkOrderOPSequenceID))
                    {
                        dbItem.WorkOrderOPSequence.Remove(item);
                    }
                }

                foreach (var item in dtoItem.WorkOrderOPSequences.ToList())
                {
                    WorkOrderOPSequence workOrderOPSequence = new WorkOrderOPSequence();

                    if (item.WorkOrderOPSequenceID <= 0)
                    {
                        dbItem.WorkOrderOPSequence.Add(workOrderOPSequence);
                    }
                    else
                    {
                        workOrderOPSequence = dbItem.WorkOrderOPSequence.FirstOrDefault(o => o.WorkOrderOPSequenceID == item.WorkOrderOPSequenceID);
                    }

                    if (workOrderOPSequence != null)
                    {
                        AutoMapper.Mapper.Map<DTO.WorkOrderOPSequenceDTO, WorkOrderOPSequence>(item, workOrderOPSequence);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.WorkOrderDTO, WorkOrder>(dtoItem, dbItem);
        }

        public DTO.BOMDTO DB2DTO_BOM(WorkOrderMng_BOM_View dbItem)
        {
            return AutoMapper.Mapper.Map<WorkOrderMng_BOM_View, DTO.BOMDTO>(dbItem);
        }

        public List<DTO.WeavingStatus> DB2DTO_WeavingStatus(List<WorkOrderMng_WeavingStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_WeavingStatus_View>, List<DTO.WeavingStatus>>(dbItems);
        }

        public List<DTO.ProductionItemGroupDTO> DB2DTO_ProductionItemGroup(List<ProductionItemMng_ProductionItemGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemGroup_View>, List<DTO.ProductionItemGroupDTO>>(dbItems);
        }

        public List<DTO.WorkCenterDTO> DB2DTO_WorkCenter(List<SupportMng_WorkCenter_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_WorkCenter_View>, List<DTO.WorkCenterDTO>>(dbItems);
        }

        public List<DTO.WorkOrderListPreOrderDTO> DB2DTO_WorkOrderListPreOrder(List<WorkOrderMng_WorkOrderListPreOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_WorkOrderListPreOrder_View>, List<DTO.WorkOrderListPreOrderDTO>>(dbItems);
        }

        public List<DTO.OPSequenceDetailDTO> DB2DTO_OPSequenceDetail(List<WorkOrderMng_OPSequenceDetail_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_OPSequenceDetail_View>, List<DTO.OPSequenceDetailDTO>>(dbItem);
        }

        public List<DTO.PreOrderWorkOrderManagement> DB2DTO_PreOrderWorkOrderManagement(List<WorkOrderMng_PreOrderWorkOrderManagement_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_PreOrderWorkOrderManagement_View>, List<DTO.PreOrderWorkOrderManagement>>(dbItem);
        }

        public List<DTO.WorkOrderBaseOnManagement> DB2DTO_WorkOrderBaseOnManagement(List<WorkOrderMng_WorkOrderBaseOnManagement_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_WorkOrderBaseOnManagement_View>, List<DTO.WorkOrderBaseOnManagement>>(dbItem);
        }

        public List<DTO.PreOrderWorkOrderBaseOnManagement> DB2DTO_PreOrderWorkOrderBaseOnManagement(List<WorkOrderMng_PreOrderWorkOrderBaseOnManagement_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_PreOrderWorkOrderBaseOnManagement_View>, List<DTO.PreOrderWorkOrderBaseOnManagement>>(dbItem);
        }

        public List<DTO.HistoryTransferPreOrderToWorkOrderDTO> DB2DTO_HistoryTransferPreOrderToWorkOrder(List<WorkOrderMng_HistoryTransferPreOrderToWorkOrder_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderMng_HistoryTransferPreOrderToWorkOrder_View>, List<DTO.HistoryTransferPreOrderToWorkOrderDTO>>(dbItem);
        }
    }
}
