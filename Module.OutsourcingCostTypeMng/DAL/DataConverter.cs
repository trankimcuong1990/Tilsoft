using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostTypeMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<OutsourcingCostTypeMng_OutSourcingCostTypeMng_SearchView, DTO.OutsourcingCostTypeSearchDTO>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingCostTypeMng_OutSourcingCostTypeMng_View, DTO.OutsourcingCostTypeDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingCostTypeMng_ProductionItemGroup_View, DTO.ProductionItemGroup>()
                    .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.OutsourcingCostTypeDTO, OutsourcingCostType>()
                   .IgnoreAllNonExisting()
                   .ForMember(d => d.UpdatedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.OutsourcingCostTypeSearchDTO> DB2DTO_OutSourcingCostTypeSearchResultList(List<OutsourcingCostTypeMng_OutSourcingCostTypeMng_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingCostTypeMng_OutSourcingCostTypeMng_SearchView>, List<DTO.OutsourcingCostTypeSearchDTO>>(dbItems);
        }

        public DTO.OutsourcingCostTypeDTO DB2DTO_OutSourcingCostType(OutsourcingCostTypeMng_OutSourcingCostTypeMng_View dbItems)
        {
            return AutoMapper.Mapper.Map<OutsourcingCostTypeMng_OutSourcingCostTypeMng_View, DTO.OutsourcingCostTypeDTO>(dbItems);
        }

        public void DTO2DB_OutSourcingCostType(DTO.OutsourcingCostTypeDTO dtoItem, ref OutsourcingCostType dbItem)
        {
            AutoMapper.Mapper.Map<DTO.OutsourcingCostTypeDTO, OutsourcingCostType>(dtoItem, dbItem);
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }

        public List<DTO.ProductionItemGroup> DB2DTO_GetProductionItemGroup(List<OutsourcingCostTypeMng_ProductionItemGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingCostTypeMng_ProductionItemGroup_View>, List<DTO.ProductionItemGroup>>(dbItems);
        }
    }
}
