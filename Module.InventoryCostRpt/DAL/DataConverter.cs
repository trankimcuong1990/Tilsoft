namespace Module.InventoryCostRpt.DAL
{
    using AutoMapper;
    using FrameworkSetting;
    using Library;
    using Module.InventoryCostRpt.DTO;
    using System.Collections.Generic;

    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            if (Setting.Maps.Contains(mapName))
            {
                return;
            }

            Mapper.CreateMap<InventoryCostRpt_InventoryCost_View, InventoryCostData>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.Unit, o => o.ResolveUsing(s => s.UnitNM))
                .ForMember(d => d.TotalBeginning, o => o.ResolveUsing(s => s.StockingQnt))
                .ForMember(d => d.TotalReceiving, o => o.ResolveUsing(s => s.ReceivingQnt))
                .ForMember(d => d.TotalDelivering, o => o.ResolveUsing(s => s.DeliveringQnt))
                .ForMember(d => d.TotalEndingInventory, o => o.ResolveUsing(s => s.EndingQnt))
                .ForMember(d => d.Price, o => o.ResolveUsing(s => s.CurrentPrice))
                .ForMember(d => d.Value, o => o.ResolveUsing(s => s.CurrentStockingValue))
                .ForMember(d => d.PrimaryID, o => o.ResolveUsing(s => s.KeyID))
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<InventoryCostRpt_FactoryWarehouse_View, FactoryWarehouseData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Mapper.CreateMap<InventoryCostRpt_ProductionItemType_View, ProductionItemTypeData>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            Setting.Maps.Add(mapName);
        }

        public List<InventoryCostData> DB2DTO_SearchResults(List<InventoryCostRpt_InventoryCost_View> dbItems)
        {
            return Mapper.Map<List<InventoryCostRpt_InventoryCost_View>, List<InventoryCostData>>(dbItems);
        }
    }
}
