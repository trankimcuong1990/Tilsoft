using Library;
using System.Collections.Generic;

namespace Module.ProductionIssue.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductionIssueOverviewRpt_ProductionIssue_View, DTO.ProductionIssueData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionIssueDetail, o => o.MapFrom(s => s.ProductionIssueOverviewRpt_ProductionIssueDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionIssueOverviewRpt_ProductionIssueDetail_View, DTO.ProductionIssueDetailData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ProductionIssueDetailHistory, o => o.MapFrom(s => s.ProductionIssueOverviewRpt_ProductionIssueDetailHistory_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionIssueOverviewRpt_ProductionIssueDetailHistory_View, DTO.HistoryDeliveryNote>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionIssueMng_WorkOrder_View, DTO.QuickSearchWorkOrder>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionIssueData, DeliveryNote>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DeliveryNoteID, o => o.Ignore())
                    .ForMember(d => d.DeliveryNoteUD, o => o.Ignore())
                    .ForMember(d => d.DeliveryNoteDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ViewName, o => o.UseValue("Warehouse2Team"))
                    .ForMember(d => d.DeliveryNoteDetail, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.ProductionIssueDetailData, DeliveryNoteDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DeliveryNoteDetailID, o => o.Ignore())
                    .ForMember(d => d.DeliveryNote, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ProductionIssueData> DB2DTO_ProductionIssueOverview(List<ProductionIssueOverviewRpt_ProductionIssue_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductionIssueOverviewRpt_ProductionIssue_View>, List<DTO.ProductionIssueData>>(dbItem);
        }

        public List<DTO.QuickSearchWorkOrder> DB2DTO_WorkOrder(List<ProductionIssueMng_WorkOrder_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<ProductionIssueMng_WorkOrder_View>, List<DTO.QuickSearchWorkOrder>>(dbItem);
        }

        public void DTO2DB_DeliveryNote(DTO.ProductionIssueData dtoItem, ref DeliveryNote dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductionIssueData, DeliveryNote>(dtoItem, dbItem);

            foreach (var dtoDetail in dtoItem.ProductionIssueDetail)
            {
                if (dtoDetail.IssueQuantity != null)
                {
                    DeliveryNoteDetail dbItemDetail;
                    dbItemDetail = new DeliveryNoteDetail();

                    dbItem.DeliveryNoteDetail.Add(dbItemDetail);

                    dbItemDetail.FromFactoryWarehouseID = dtoDetail.FromFactoryWarehouseID;
                    dbItemDetail.Qty = dtoDetail.IssueQuantity;
                    dbItemDetail.WorkOrderID = dtoItem.WorkOrderID;
                    dbItemDetail.BOMID = dtoItem.BOMID;
                    dbItemDetail.ProductionItemID = dtoDetail.ProductionItemID;
                }
            }
        }
    }
}
