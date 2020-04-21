using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionCosting.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }
            AutoMapper.Mapper.CreateMap<ProductionCostingRpt_ProductionCosting_View, DTO.ProductionCostingPrintData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.ProductionCostingDTOs, o => o.MapFrom(s => s.ProductionCostingRpt_ProductionCosting_View1))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<ProductionCostingRpt_TotalQntReceivingNote_View, DTO.TotalReceivingNote>()
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<ProductionCostingMng_WorkOrder_View, DTO.WorkOrderDTO>()
                    .IgnoreAllNonExisting()                   
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.FinishDate, o => o.ResolveUsing(s => s.FinishDate.HasValue ? s.FinishDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.ProductionCostingPrintData> DB2DTO_SearchResults(List<ProductionCostingRpt_ProductionCosting_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionCostingRpt_ProductionCosting_View>, List<DTO.ProductionCostingPrintData>>(dbItems);
        }
        public DTO.ProductionCostingPrintData DB2DTO_ProductionCosting(ProductionCostingRpt_ProductionCosting_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionCostingRpt_ProductionCosting_View, DTO.ProductionCostingPrintData>(dbItem);
        }
        public List<DTO.TotalReceivingNote> DB2DTO_TotalReceivingNote(List<ProductionCostingRpt_TotalQntReceivingNote_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionCostingRpt_TotalQntReceivingNote_View>, List<DTO.TotalReceivingNote>>(dbItems);
        }
        public DTO.WorkOrderDTO DB2DTO_WorkOrder(ProductionCostingMng_WorkOrder_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionCostingMng_WorkOrder_View, DTO.WorkOrderDTO>(dbItem);
        }
    }
}
