using Library;
using System.Collections.Generic;

namespace Module.StorageCardRpt.DAL
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

            AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItem_View, DTO.ProductionItem>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<StorageCardRpt_StorageCard_View, DTO.StorageCard>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            AutoMapper.Mapper.CreateMap<StorageCardRpt_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.StorageCard> DB2DTO_SearchResults(List<StorageCardRpt_StorageCard_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<StorageCardRpt_StorageCard_View>, List<DTO.StorageCard>>(dbItems);
        }

        public DTO.ProductionItem DB2DTO_Result(ProductionItemMng_ProductionItem_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionItemMng_ProductionItem_View, DTO.ProductionItem>(dbItem);
        }

        public List<DTO.FactoryWarehouseDTO> DB2DTO_FactoryWarehouses(List<StorageCardRpt_FactoryWarehouse_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<StorageCardRpt_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(dbItem);
        }
    }
}
