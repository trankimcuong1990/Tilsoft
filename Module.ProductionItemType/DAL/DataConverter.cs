using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.ProductionItemType.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemType_Search, DTO.ProductionItemTypeSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemType_View, DTO.ProductionItemTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemTypeDTO, ProductionItemType>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProductionItemTypeID, o => o.Ignore());


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ProductionItemTypeSearchDTO> DB2DTO_ProductionItemTypeSearchList(List<ProductionItemMng_ProductionItemType_Search> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemType_Search>, List<DTO.ProductionItemTypeSearchDTO>>(dbItems);
        }

        public DTO.ProductionItemTypeDTO DB2DTO_ProductionItemType(ProductionItemMng_ProductionItemType_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionItemMng_ProductionItemType_View, DTO.ProductionItemTypeDTO>(dbItem);
        }

        public void DTO2BD(DTO.ProductionItemTypeDTO dtoItem, ref ProductionItemType dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductionItemTypeDTO, ProductionItemType>(dtoItem, dbItem);
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }
    }
}
