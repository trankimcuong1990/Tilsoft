using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.WorkOrderCostMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "WorkOrderCostMng";
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }
            AutoMapper.Mapper.CreateMap<SupportMng_ProductionItem_View, DTO.ProductionItem>()
                   .IgnoreAllNonExisting();

            AutoMapper.Mapper.CreateMap<WorkOrderCostMng_WorkOrderCost_View, DTO.WorkOrderCostDTO>()
               .IgnoreAllNonExisting();          
            FrameworkSetting.Setting.Maps.Add(mapName);


        }
        public List<DTO.WorkOrderCostDTO> DB2DTO_WorkOrderCost(List<WorkOrderCostMng_WorkOrderCost_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<WorkOrderCostMng_WorkOrderCost_View>, List<DTO.WorkOrderCostDTO>>(dbItem);
        }
    }
}
