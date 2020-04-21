using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Module.RevenueCostingRpt.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = GetType().Assembly.GetName().Name;

            // Check mapName exist in Maps.
            if (FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                return;
            }

            AutoMapper.Mapper.CreateMap<Supplier, DTO.SupplierDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

            FrameworkSetting.Setting.Maps.Add(mapName);
        }
        public List<DTO.SupplierDTO> DB2DTO_GetSubSupplier(List<Supplier> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Supplier>, List<DTO.SupplierDTO>>(dbItems);
        }
    }
}
