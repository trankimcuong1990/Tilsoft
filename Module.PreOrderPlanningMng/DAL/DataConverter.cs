using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PreOrderPlanningMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "PreOrderPlanningMng";

            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<PreOrderPlanningMng_FactoryPlanning_View, DTO.FactoryPlanningData>()
                .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<DTO.FactoryPlanningData, FactoryPlanning>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FactoryPlanningID, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore());

            FrameworkSetting.Setting.Maps.Add(mapName);
        }

        public List<DTO.FactoryPlanningData> DB2DTO_FactoryPlanningSearchResult(List<PreOrderPlanningMng_FactoryPlanning_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<PreOrderPlanningMng_FactoryPlanning_View>, List<DTO.FactoryPlanningData>>(dbItem);
        }

        public void DTO2DB_FactoryPlanning(DTO.FactoryPlanningData dtoItem, ref FactoryPlanning dbItem)
        {
            AutoMapper.Mapper.Map<DTO.FactoryPlanningData, FactoryPlanning>(dtoItem, dbItem);
        }
    }
}
