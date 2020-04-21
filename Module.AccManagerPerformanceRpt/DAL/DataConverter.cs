using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.AccManagerPerformanceRpt.DAL
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
                AutoMapper.Mapper.CreateMap<AccManagerPerformanceRpt_function_GetReportData_Result, DTO.AccManagerPerformanceDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AccManagerPerformanceRpt_function_GetDeltaData_Result, DTO.AccManagerPerformanceDeltaDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_ActiveSales_View, DTO.SaleDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.AccManagerPerformanceDTO> DB2DTO_AccManagerPerformance(List<AccManagerPerformanceRpt_function_GetReportData_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccManagerPerformanceRpt_function_GetReportData_Result>, List<DTO.AccManagerPerformanceDTO>>(dbItems);
        }

        public List<DTO.AccManagerPerformanceDeltaDTO> DB2DTO_AccManagerPerformanceDelta(List<AccManagerPerformanceRpt_function_GetDeltaData_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AccManagerPerformanceRpt_function_GetDeltaData_Result>, List<DTO.AccManagerPerformanceDeltaDTO>>(dbItems);
        }

        public List<DTO.SaleDTO> DB2DTO_Sale(List<SupportMng_ActiveSales_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<SupportMng_ActiveSales_View>, List<DTO.SaleDTO>>(dbItems);
        }
    }
}
