using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
namespace Module.EurofarServiceMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "EurofarService";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<EurofarServiceMng_WarehousePhysicalStock_View, DTO.WarehousePhysicalStock>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<EurofarServiceMng_WarehouseReservationStockBy_NLBL_View, DTO.WarehouseReservationStockBy_NLBL>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WarehousePhysicalStock> DB2DTO_WarehousePhysicalStock(List<EurofarServiceMng_WarehousePhysicalStock_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarServiceMng_WarehousePhysicalStock_View>, List<DTO.WarehousePhysicalStock>>(dbItems);
        }

        public List<DTO.WarehouseReservationStockBy_NLBL> DB2DTO_WarehouseReservationStock(List<EurofarServiceMng_WarehouseReservationStockBy_NLBL_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<EurofarServiceMng_WarehouseReservationStockBy_NLBL_View>, List<DTO.WarehouseReservationStockBy_NLBL>>(dbItems);
        }
    }
}
