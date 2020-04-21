using Library;
using System.Collections.Generic;

namespace Module.PurchaseOrderTrackingRpt.DAL
{
    public class DataConverter
    {
        private readonly string mapName = "PurchaseOrderTrackingRpt";

        public DataConverter()
        {
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<PurchaseOrderTrackingRpt_SupportProductionItemGroup_View, DTO.SupportItemGroupData>()
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<PurchaseOrderTrackingRpt_SupportSupplier_View, DTO.SupportSupplierData>()
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<PurchaseOrderTrackingRpt_SupportClient_View, DTO.SupportClientData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.Id, o => o.ResolveUsing(s => s.ClientID))
                .ForMember(d => d.Value, o => o.ResolveUsing(s => s.ClientUD))
                .ForMember(d => d.Label, o => o.ResolveUsing(s => s.ClientNM));

            AutoMapper.Mapper.CreateMap<PurchaseOrderTrackingRpt_SupportWorkOrder_View, DTO.SupportWorkOrderData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.Id, o => o.ResolveUsing(s => s.WorkOrderID))
                .ForMember(d => d.Value, o => o.ResolveUsing(s => s.WorkOrderUD))
                .ForMember(d => d.Label, o => o.ResolveUsing(s => s.Description));

            AutoMapper.Mapper.CreateMap<PurchaseOrderTrackingRpt_PurchaseOrderTracking_View, DTO.PurchaseOrderTrackingData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.PurchaseOrderDate, o => o.ResolveUsing(s => s.PurchaseOrderDate.HasValue ? s.PurchaseOrderDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.ETA, o => o.ResolveUsing(s => s.ETA.HasValue ? s.ETA.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.PurchaseOrderTrackingDetail, o => o.MapFrom(s => s.PurchaseOrderTrackingRpt_PurchaseOrderTrackingDetail_View))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PurchaseOrderTrackingRpt_PurchaseOrderTrackingDetail_View, DTO.PurchaseOrderTrackingDetailData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.PurchaseOrderETA, o => o.ResolveUsing(s => s.PurchaseOrderETA.HasValue ? s.PurchaseOrderETA.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.PurchaseOrderDetailETA, o => o.ResolveUsing(s => s.PurchaseOrderDetailETA.HasValue ? s.PurchaseOrderDetailETA.Value.ToString("dd/MM/yyyy") : null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<PurchaseOrderTrackingRpt_WeekInfo_View, DTO.PurchaseOrderTrackingWeekData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
        }

        public List<DTO.SupportItemGroupData> DB2DTO_SupportItemGroup(List<PurchaseOrderTrackingRpt_SupportProductionItemGroup_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderTrackingRpt_SupportProductionItemGroup_View>, List<DTO.SupportItemGroupData>>(dbItem);
        }

        public List<DTO.SupportSupplierData> DB2DTO_SupportSupplier(List<PurchaseOrderTrackingRpt_SupportSupplier_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderTrackingRpt_SupportSupplier_View>, List<DTO.SupportSupplierData>>(dbItem);
        }

        public List<DTO.SupportClientData> DB2DTO_SupportClient(List<PurchaseOrderTrackingRpt_SupportClient_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderTrackingRpt_SupportClient_View>, List<DTO.SupportClientData>>(dbItem);
        }

        public List<DTO.SupportWorkOrderData> DB2DTO_SupportWorkOrder(List<PurchaseOrderTrackingRpt_SupportWorkOrder_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderTrackingRpt_SupportWorkOrder_View>, List<DTO.SupportWorkOrderData>>(dbItem);
        }

        public List<DTO.PurchaseOrderTrackingData> DB2DTO_PurchaseOrderTracking(List<PurchaseOrderTrackingRpt_PurchaseOrderTracking_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderTrackingRpt_PurchaseOrderTracking_View>, List<DTO.PurchaseOrderTrackingData>>(dbItem);
        }

        public List<DTO.PurchaseOrderTrackingWeekData> DB2DTO_WeekInfo(List<PurchaseOrderTrackingRpt_WeekInfo_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PurchaseOrderTrackingRpt_WeekInfo_View>, List<DTO.PurchaseOrderTrackingWeekData>>(dbItem);
        }
    }
}
