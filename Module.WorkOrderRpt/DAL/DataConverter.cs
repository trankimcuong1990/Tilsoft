using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.WorkOrderRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<WorkOrderRpt_WorkOrder_View, DTO.WorkOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderRpt_OPSequenceDetail_View, DTO.OPSequenceDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderRpt_ProductionTeamBySequenceDetail_View, DTO.ProductionTeam>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderRpt_FactoryWarehouseBySequenceDetail_View, DTO.FactoryWarehouse>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderRpt_InOutDetail_View, DTO.Detail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderRpt_InOutReceipt_View, DTO.Receipt>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderRpt_ReceiptOverview_View, DTO.ReceiptOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ReceiptDate, o => o.ResolveUsing(s => s.ReceiptDate.HasValue ? s.ReceiptDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WorkOrderRpt_ReceiptOverviewDetail_View, DTO.ReceiptOverviewDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public DTO.WorkOrder DB2DTO_WorkOrder(WorkOrderRpt_WorkOrder_View dbItem)
        {
            return AutoMapper.Mapper.Map<WorkOrderRpt_WorkOrder_View, DTO.WorkOrder>(dbItem);
        }

        public List<DTO.OPSequenceDetail> DB2DTO_OPSequenceDetail(List<WorkOrderRpt_OPSequenceDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderRpt_OPSequenceDetail_View>, List<DTO.OPSequenceDetail>>(dbItems);
        }

        public List<DTO.ProductionTeam> DB2DTO_ProductionTeam(List<WorkOrderRpt_ProductionTeamBySequenceDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderRpt_ProductionTeamBySequenceDetail_View>, List<DTO.ProductionTeam>>(dbItems);
        }

        public List<DTO.FactoryWarehouse> DB2DTO_FactoryWarehouse(List<WorkOrderRpt_FactoryWarehouseBySequenceDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderRpt_FactoryWarehouseBySequenceDetail_View>, List<DTO.FactoryWarehouse>>(dbItems);
        }

        public List<DTO.Detail> DB2DTO_Detail(List<WorkOrderRpt_InOutDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderRpt_InOutDetail_View>, List<DTO.Detail>>(dbItems);
        }

        public List<DTO.Receipt> DB2DTO_Receipt(List<WorkOrderRpt_InOutReceipt_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderRpt_InOutReceipt_View>, List<DTO.Receipt>>(dbItems);
        }

        public List<DTO.ReceiptOverview> DB2DTO_ReceiptOverview(List<WorkOrderRpt_ReceiptOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderRpt_ReceiptOverview_View>, List<DTO.ReceiptOverview>>(dbItems);
        }

        public List<DTO.ReceiptOverviewDetail> DB2DTO_ReceiptOverviewDetail(List<WorkOrderRpt_ReceiptOverviewDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderRpt_ReceiptOverviewDetail_View>, List<DTO.ReceiptOverviewDetail>>(dbItems);
        }
    }
}
