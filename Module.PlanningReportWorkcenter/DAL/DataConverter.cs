using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningReportWorkcenter.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<PlanningReport_WorkOrder_View, DTO.WorkOrderDTO>()
                        .IgnoreAllNonExisting()
                        .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                        .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                        .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                        .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                        .ForMember(d => d.WeekStart, o => o.ResolveUsing(s => s.WeekStart.HasValue ? s.WeekStart.Value.ToString("dd/MM/yyyy") : ""))
                        .ForMember(d => d.WeekEnd, o => o.ResolveUsing(s => s.WeekEnd.HasValue ? s.WeekEnd.Value.ToString("dd/MM/yyyy") : ""))
                        .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                        .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WeekInfo, DTO.WeekInfoDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.WeekStart, o => o.ResolveUsing(s => s.WeekStart.HasValue ? s.WeekStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WeekEnd, o => o.ResolveUsing(s => s.WeekEnd.HasValue ? s.WeekEnd.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkCenter, DTO.WorkCenterDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningReportWorkCenter_ReceivingDetail_View, DTO.ReceivingDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningReportWorkCenter_SetDetail_View, DTO.ReceivingSetDetailDTO>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<PlanningReportWorkCenter_GetWorkCenterStatus_View, DTO.WorkcenterStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<PlanningReportWorkCenter_GetMaterialStatus_View, DTO.MaterialStatus>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WorkOrderDTO> DB2DTO_WorkOrderList(List<PlanningReport_WorkOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PlanningReport_WorkOrder_View>, List<DTO.WorkOrderDTO>>(dbItems);
        }

        public List<DTO.WeekInfoDTO> DB2DTO_WeekInfoList(List<WeekInfo> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WeekInfo>, List<DTO.WeekInfoDTO>>(dbItems);
        }

        public List<DTO.ReceivingDetailDTO> DB2DTO_ReceivingDetail(List<PlanningReportWorkCenter_ReceivingDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PlanningReportWorkCenter_ReceivingDetail_View>, List<DTO.ReceivingDetailDTO>>(dbItems);
        }

        public List<DTO.WorkCenterDTO> DB2DTO_WorkCenterList(List<WorkCenter> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkCenter>, List<DTO.WorkCenterDTO>>(dbItems);
        }

        public List<DTO.ReceivingSetDetailDTO> DB2DTO_ReceivingSetDetail(List<PlanningReportWorkCenter_SetDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PlanningReportWorkCenter_SetDetail_View>, List<DTO.ReceivingSetDetailDTO>>(dbItems);
        }

        public List<DTO.WorkcenterStatus> DB2DTO_GetWorkCenterStatus(List<PlanningReportWorkCenter_GetWorkCenterStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PlanningReportWorkCenter_GetWorkCenterStatus_View>, List<DTO.WorkcenterStatus>>(dbItems);
        }

        public List<DTO.MaterialStatus> DB2DTO_GetMaterialStatus(List<PlanningReportWorkCenter_GetMaterialStatus_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<PlanningReportWorkCenter_GetMaterialStatus_View>, List<DTO.MaterialStatus>>(dbItems);
        }
    }
}
