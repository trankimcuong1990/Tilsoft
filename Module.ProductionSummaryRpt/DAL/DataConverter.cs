using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionSummaryRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductionSummaryRpt_ProductionSummaryVirtual_View, DTO.ProductionSummaryDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Qnt40HC, o => o.ResolveUsing(s => s.Qnt40HC.HasValue ? s.Qnt40HC.Value : 0))
                    .ForMember(d => d.OrderQnt, o => o.ResolveUsing(s => s.OrderQnt.HasValue ? s.OrderQnt.Value : 0))
                    .ForMember(d => d.WorkOrderQnt, o => o.ResolveUsing(s => s.WorkOrderQnt.HasValue ? s.WorkOrderQnt.Value : 0))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionSummaryRpt_ProductionSummaryDetailVirtual_View, DTO.ProductionSummaryDetailDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.TotalImportQnt, o => o.ResolveUsing(s => s.TotalImportQnt.HasValue ? s.TotalImportQnt.Value : 0))
                    .ForMember(d => d.TotalFinishQnt, o => o.ResolveUsing(s => s.TotalFinishQnt.HasValue ? s.TotalFinishQnt.Value : 0))
                    .ForMember(d => d.TotalExportQnt, o => o.ResolveUsing(s => s.TotalExportQnt.HasValue ? s.TotalExportQnt.Value : 0))
                    .ForMember(d => d.TotalRemainQnt, o => o.ResolveUsing(s => s.TotalRemainQnt.HasValue ? s.TotalRemainQnt.Value : 0))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionSummaryRpt_WorkCenter_View, DTO.WorkCenterDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            }
        }

        public List<DTO.ProductionSummaryDTO> DB2DTO_ProductionSummary(List<ProductionSummaryRpt_ProductionSummaryVirtual_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductionSummaryRpt_ProductionSummaryVirtual_View>, List<DTO.ProductionSummaryDTO>>(dbItem);
        }

        public List<DTO.ProductionSummaryDetailDTO> DB2DTO_ProductionSummaryDetail(List<ProductionSummaryRpt_ProductionSummaryDetailVirtual_View> dbItemDetail)
        {
            return AutoMapper.Mapper.Map<List<ProductionSummaryRpt_ProductionSummaryDetailVirtual_View>, List<DTO.ProductionSummaryDetailDTO>>(dbItemDetail);
        }

        public List<DTO.WorkCenterDTO> DB2DTO_WorkCenter(List<ProductionSummaryRpt_WorkCenter_View> dbWorkCenter)
        {
            return AutoMapper.Mapper.Map<List<ProductionSummaryRpt_WorkCenter_View>, List<DTO.WorkCenterDTO>>(dbWorkCenter);
        }
    }
}
