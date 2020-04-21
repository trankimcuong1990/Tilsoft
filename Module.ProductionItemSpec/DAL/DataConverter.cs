using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.ProductionItemSpec.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemSpec_Search, DTO.ProductionItemSpecSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductionItemMng_ProductionItemSpec_View, DTO.ProductionItemSpecDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductionItemSpecDTO, ProductionItemSpec>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProductionItemSpecID, o => o.Ignore());


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ProductionItemSpecSearchDTO> DB2DTO_ProductionItemSpecSearchList(List<ProductionItemMng_ProductionItemSpec_Search> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductionItemMng_ProductionItemSpec_Search>, List<DTO.ProductionItemSpecSearchDTO>>(dbItems);
        }

        public DTO.ProductionItemSpecDTO DB2DTO_ProductionItemSpec(ProductionItemMng_ProductionItemSpec_View dbItem)
        {
            return AutoMapper.Mapper.Map<ProductionItemMng_ProductionItemSpec_View, DTO.ProductionItemSpecDTO>(dbItem);
        }

        public void DTO2BD(DTO.ProductionItemSpecDTO dtoItem, ref ProductionItemSpec dbItem)
        {
            AutoMapper.Mapper.Map<DTO.ProductionItemSpecDTO, ProductionItemSpec>(dtoItem, dbItem);
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }
    }
}
