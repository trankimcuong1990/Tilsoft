using Library;
using System.Collections.Generic;

namespace Module.InventoryRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<InventoryRpt_Inventory_View, DTO.InventoryData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<InventoryRpt_InventoryDetail_View, DTO.InventoryDetailData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<InventoryRpt_InventoryOverviewWithClient_View, DTO.InventoryOverviewClientData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<InventoryRpt_InventoryOverviewFinishProduct_View, DTO.InventoryFinishProductData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<InventoryRpt_InventoryFinishProductDetail_View, DTO.InventoryFinishProductDetailData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<InventoryRpt_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            /// Custom function get data (new version)
            AutoMapper.Mapper.CreateMap<InventoryRpt_Inventory2_View, DTO.InventoryData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<InventoryRpt_function_InventorySearchResult2_Result, DTO.InventoryData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.StockedQnt, o => o.ResolveUsing(s => s.StockingQnt ?? null))
                .ForMember(d => d.ReceivedQnt, o => o.ResolveUsing(s => s.ReceivingQnt ?? null))
                .ForMember(d => d.DeliveredQnt, o => o.ResolveUsing(s => s.DeliveringQnt ?? null))
                .ForMember(d => d.EndedQnt, o => o.ResolveUsing(s => s.EndingQnt ?? null))
                .ForMember(d => d.StockedCont, o => o.ResolveUsing(s => s.StockedCont ?? null))
                .ForMember(d => d.ReceivedCont, o => o.ResolveUsing(s => s.ReceivedCont ?? null))
                .ForMember(d => d.DeliveredCont, o => o.ResolveUsing(s => s.DeliveredCont ?? null))
                .ForMember(d => d.EndedCont, o => o.ResolveUsing(s => s.EndedCont ?? null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            AutoMapper.Mapper.CreateMap<InventoryRpt_function_InventorySearchResult4_Result, DTO.InventoryDetailData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.DocumentNoteDate, o => o.ResolveUsing(s => s.DocumentNoteDate.HasValue ? s.DocumentNoteDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.ReceivedQnt, o => o.ResolveUsing(s => s.ReceivingQnt ?? null))
                .ForMember(d => d.DeliveredQnt, o => o.ResolveUsing(s => s.DeliveringQnt ?? null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<InventoryRpt_function_InventorySearchResult3_Result, DTO.InventoryData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.StockedQnt, o => o.ResolveUsing(s => s.StockingQnt ?? null))
                .ForMember(d => d.ReceivedQnt, o => o.ResolveUsing(s => s.ReceivingQnt ?? null))
                .ForMember(d => d.DeliveredQnt, o => o.ResolveUsing(s => s.DeliveringQnt ?? null))
                .ForMember(d => d.EndedQnt, o => o.ResolveUsing(s => s.EndingQnt ?? null))
                .ForMember(d => d.StockedCont, o => o.ResolveUsing(s => s.StockedCont ?? null))
                .ForMember(d => d.ReceivedCont, o => o.ResolveUsing(s => s.ReceivedCont ?? null))
                .ForMember(d => d.DeliveredCont, o => o.ResolveUsing(s => s.DeliveredCont ?? null))
                .ForMember(d => d.EndedCont, o => o.ResolveUsing(s => s.EndedCont ?? null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            
            AutoMapper.Mapper.CreateMap<InventoryRpt_function_InventorySearchResult5_Result, DTO.InventoryDetailData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.DocumentNoteDate, o => o.ResolveUsing(s => s.DocumentNoteDate.HasValue ? s.DocumentNoteDate.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(d => d.ReceivedQnt, o => o.ResolveUsing(s => s.ReceivingQnt ?? null))
                .ForMember(d => d.DeliveredQnt, o => o.ResolveUsing(s => s.DeliveringQnt ?? null))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.InventoryData> DB2DTO_SearchResults(List<InventoryRpt_Inventory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<InventoryRpt_Inventory_View>, List<DTO.InventoryData>>(dbItems);
        }

        public List<DTO.InventoryDetailData> DB2DTO_InventoryDetail(List<InventoryRpt_InventoryDetail_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<InventoryRpt_InventoryDetail_View>, List<DTO.InventoryDetailData>>(dbItem);
        }

        public List<DTO.InventoryOverviewClientData> DB2DTO_InventoryOverviewClient(List<InventoryRpt_InventoryOverviewWithClient_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<InventoryRpt_InventoryOverviewWithClient_View>, List<DTO.InventoryOverviewClientData>>(dbItem);
        }

        public List<DTO.InventoryFinishProductData> DB2DTO_InventoryFinishProducts(List<InventoryRpt_InventoryOverviewFinishProduct_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<InventoryRpt_InventoryOverviewFinishProduct_View>, List<DTO.InventoryFinishProductData>>(dbItem);
        }

        public List<DTO.InventoryFinishProductDetailData> DB2DTO_InventoryFinishProductDetail(List<InventoryRpt_InventoryFinishProductDetail_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<InventoryRpt_InventoryFinishProductDetail_View>, List<DTO.InventoryFinishProductDetailData>>(dbItem);
        }

        /// Custom function get data (new version)
        public List<DTO.InventoryData> DB2DTO_Inventory2SearchResult(List<InventoryRpt_Inventory2_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<InventoryRpt_Inventory2_View>, List<DTO.InventoryData>>(dbItem);
        }
    }
}
