using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.FactoryPerformanceRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("nl-NL");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<FactoryPerformanceRpt_WeekInfoReportData_View, DTO.WeekInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPerformanceRpt_YearlyShippedReportData_View, DTO.AnnualShipped>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPerformanceRpt_WeeklyShippedReportData_View, DTO.WeeklyShipped>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
            
                AutoMapper.Mapper.CreateMap<FactoryPerformanceRpt_FactoryInfoReportData_View, DTO.FactoryInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPerformanceRpt_YearlyProducedReportData_View, DTO.AnnualProduced>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPerformanceRpt_WeeklyProducedReportData_View, DTO.WeeklyProduced>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Factory Capacity
                AutoMapper.Mapper.CreateMap<FactoryPerformanceRpt_TotalCapacityAndOrder_View, DTO.TotalCapacityAndOrderData>()
                   .IgnoreAllNonExisting();
                //.ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<FactoryPerformanceRpt_TotalCapacityAndOrderByWeekOfFactory_View, DTO.TotalCapacityAndOrderByWeekData>()
                    .ForMember(d => d.WeekStart, o => o.ResolveUsing(s => s.WeekStart.HasValue ? s.WeekStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WeekEnd, o => o.ResolveUsing(s => s.WeekEnd.HasValue ? s.WeekEnd.Value.ToString("dd/MM/yyyy") : ""))
                    .IgnoreAllNonExisting();
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.WeekInfo> DB2DTO_WeekInfo(List<FactoryPerformanceRpt_WeekInfoReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPerformanceRpt_WeekInfoReportData_View>, List<DTO.WeekInfo>>(dbItems);
        }

        public List<DTO.AnnualShipped> DB2DTO_AnnualShipped(List<FactoryPerformanceRpt_YearlyShippedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPerformanceRpt_YearlyShippedReportData_View>, List<DTO.AnnualShipped>>(dbItems);
        }

        public List<DTO.WeeklyShipped> DB2DTO_WeeklyShipped(List<FactoryPerformanceRpt_WeeklyShippedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPerformanceRpt_WeeklyShippedReportData_View>, List<DTO.WeeklyShipped>>(dbItems);
        }

        public List<DTO.FactoryInfo> DB2DTO_FactoryInfo(List<FactoryPerformanceRpt_FactoryInfoReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPerformanceRpt_FactoryInfoReportData_View>, List<DTO.FactoryInfo>>(dbItems);
        }

        public List<DTO.AnnualProduced> DB2DTO_AnnualProduced(List<FactoryPerformanceRpt_YearlyProducedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPerformanceRpt_YearlyProducedReportData_View>, List<DTO.AnnualProduced>>(dbItems);
        }

        public List<DTO.WeeklyProduced> DB2DTO_WeeklyProduced(List<FactoryPerformanceRpt_WeeklyProducedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPerformanceRpt_WeeklyProducedReportData_View>, List<DTO.WeeklyProduced>>(dbItems);
        }

        //FactoryCapacity
        public List<DTO.TotalCapacityAndOrderData> DB2DTO_TotalCapacityAndOrder(List<FactoryPerformanceRpt_TotalCapacityAndOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPerformanceRpt_TotalCapacityAndOrder_View>, List<DTO.TotalCapacityAndOrderData>>(dbItems);
        }

        public List<DTO.TotalCapacityAndOrderByWeekData> DB2DTO_TotalCapacityAndOrderByWeek(List<FactoryPerformanceRpt_TotalCapacityAndOrderByWeekOfFactory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<FactoryPerformanceRpt_TotalCapacityAndOrderByWeekOfFactory_View>, List<DTO.TotalCapacityAndOrderByWeekData>>(dbItems);
        }
    }
}
