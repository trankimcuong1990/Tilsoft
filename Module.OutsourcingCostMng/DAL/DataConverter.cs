using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<OutsourcingCostMng_OutsourcingCost_SearchView, DTO.OutsourcingCostSearchDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingCostMng_OutsourcingCost_View, DTO.OutsourcingCostDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<OutsourcingCostMng_WorkCenter_View, DTO.WorkCenterDTO>()
                   .IgnoreAllNonExisting();

                AutoMapper.Mapper.CreateMap<DTO.OutsourcingCostDTO, OutsourcingCost>()
                     .IgnoreAllNonExisting();

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.OutsourcingCostSearchDTO> DB2DTO_OutsourcingCostSearch(List<OutsourcingCostMng_OutsourcingCost_SearchView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingCostMng_OutsourcingCost_SearchView>, List<DTO.OutsourcingCostSearchDTO>>(dbItems);
        }

        public DTO.OutsourcingCostDTO DB2DTO_OutSourcingCost(OutsourcingCostMng_OutsourcingCost_View dbItems)
        {
            return AutoMapper.Mapper.Map<OutsourcingCostMng_OutsourcingCost_View, DTO.OutsourcingCostDTO>(dbItems);
        }

        public void DTO2DB_OutSourcingCost(DTO.OutsourcingCostDTO dtoItem, ref OutsourcingCost dbItem)
        {
            AutoMapper.Mapper.Map<DTO.OutsourcingCostDTO, OutsourcingCost>(dtoItem, dbItem);
            dbItem.UpdatedDate = dtoItem.UpdatedDate.ConvertStringToDateTime();
        }

        public List<DTO.WorkCenterDTO> DB2DTO_GetWorkCenter(List<OutsourcingCostMng_WorkCenter_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<OutsourcingCostMng_WorkCenter_View>, List<DTO.WorkCenterDTO>>(dbItems);
        }
    }
}
