using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockStatusQntRpt.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<StockStatusQntRpt_StockStatusQnt_View, DTO.StockStatusQntDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StockQnt, o => o.ResolveUsing(s => s.StockQnt.HasValue ? s.StockQnt.Value : 0))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemGroup_View, DTO.ProductionItemGroup>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<StockStatusQntRpt_FactoryWarehouse_View, DTO.FactoryWarehouseDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            }
        }
        public List<DTO.ProductionItemGroup> DB2DTO_ProductionItemGroup(List<ProductionItemMng_ProductionItemGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemGroup_View>, List<DTO.ProductionItemGroup>>(dbItems);
        }

        public List<DTO.StockStatusQntDTO> DB2DTO_StockStatus(List<StockStatusQntRpt_StockStatusQnt_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<StockStatusQntRpt_StockStatusQnt_View>, List<DTO.StockStatusQntDTO>>(dbItem);
        }

        public List<DTO.FactoryWarehouseDTO> DB2DTO_FactoryWarehouses(List<StockStatusQntRpt_FactoryWarehouse_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<StockStatusQntRpt_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(dbItem);
        }
    }
}
