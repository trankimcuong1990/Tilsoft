using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.MIEurofarPriceOverviewRpt.DAL
{
    internal class DataConverter
    {
        private System.Globalization.CultureInfo cInfo = new System.Globalization.CultureInfo("vi-VN");
        private DateTime tmpDate;

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<MIEurofarPriceOverviewRpt_MarginCustomer_HTML_View, DTO.CustomerData>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MIEurofarPriceOverviewRpt_Supplier_HTML_View, DTO.SupplierData>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MIEurofarPriceOverviewRpt_SalePerformance_View, DTO.SalePerformanceDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CustomerData> DB2DTO_CustomerData(List<MIEurofarPriceOverviewRpt_MarginCustomer_HTML_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIEurofarPriceOverviewRpt_MarginCustomer_HTML_View>, List<DTO.CustomerData>>(dbItems);
        }

        public List<DTO.SupplierData> DB2DTO_SupplierData(List<MIEurofarPriceOverviewRpt_Supplier_HTML_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIEurofarPriceOverviewRpt_Supplier_HTML_View>, List<DTO.SupplierData>>(dbItems);
        }

        public List<DTO.SalePerformanceDTO> DB2DTO_SalePerformanceList(List<MIEurofarPriceOverviewRpt_SalePerformance_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MIEurofarPriceOverviewRpt_SalePerformance_View>, List<DTO.SalePerformanceDTO>>(dbItems);
        }

    }
}
