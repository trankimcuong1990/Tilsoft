using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShippingPerformanceRpt.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<ShippingPerformanceRpt_GetReportData_View, DTO.ShippingPerformanceRptDto>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        public List<DTO.ShippingPerformanceRptDto> DB2DTO_ShippingPerformanceRptDTO(List<ShippingPerformanceRpt_GetReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ShippingPerformanceRpt_GetReportData_View>, List<DTO.ShippingPerformanceRptDto>>(dbItems);
        }
    }
}
