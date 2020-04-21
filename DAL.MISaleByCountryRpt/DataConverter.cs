using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MISaleByCountryRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MISaleByCountryRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MISaleByCountryRpt_function_GetSummaryByCountry_Result, DTO.MISaleByCountryRpt.Summary>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByCountryRpt_function_GetConfirmedSummaryByCountry_Result, DTO.MISaleByCountryRpt.ConfirmedSummary>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByCountryRpt_function_GetExpectedByCountry_Result, DTO.MISaleByCountryRpt.ExpectedSummary>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByCountryRpt_function_getCommercialInvoiceByCountry_Result, DTO.MISaleByCountryRpt.CommercialInvoiceSummary>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByCountryRpt_Detail_View, DTO.MISaleByCountryRpt.Detail>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByCountryRpt_ConfirmedDetail_View, DTO.MISaleByCountryRpt.ConfirmedDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MISaleByCountryRpt.ConfirmedDetail> DB2DTO_ConfirmedDetailList(List<MISaleByCountryRpt_ConfirmedDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByCountryRpt_ConfirmedDetail_View>, List<DTO.MISaleByCountryRpt.ConfirmedDetail>>(dbItems);
        }

        public List<DTO.MISaleByCountryRpt.ConfirmedSummary> DB2DTO_ConfirmedSummaryList(List<MISaleByCountryRpt_function_GetConfirmedSummaryByCountry_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByCountryRpt_function_GetConfirmedSummaryByCountry_Result>, List<DTO.MISaleByCountryRpt.ConfirmedSummary>>(dbItems);
        }

        public List<DTO.MISaleByCountryRpt.Detail> DB2DTO_DetailList(List<MISaleByCountryRpt_Detail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByCountryRpt_Detail_View>, List<DTO.MISaleByCountryRpt.Detail>>(dbItems);
        }

        public List<DTO.MISaleByCountryRpt.Summary> DB2DTO_SummaryList(List<MISaleByCountryRpt_function_GetSummaryByCountry_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByCountryRpt_function_GetSummaryByCountry_Result>, List<DTO.MISaleByCountryRpt.Summary>>(dbItems);
        }

        public List<DTO.MISaleByCountryRpt.ExpectedSummary> DB2DTO_ExpectedSummaryList(List<MISaleByCountryRpt_function_GetExpectedByCountry_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByCountryRpt_function_GetExpectedByCountry_Result>, List<DTO.MISaleByCountryRpt.ExpectedSummary>>(dbItems);
        }

        public List<DTO.MISaleByCountryRpt.CommercialInvoiceSummary> DB2DTO_CommercialInvoiceSummaryList(List<MISaleByCountryRpt_function_getCommercialInvoiceByCountry_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByCountryRpt_function_getCommercialInvoiceByCountry_Result>, List<DTO.MISaleByCountryRpt.CommercialInvoiceSummary>>(dbItems);
        }
    }
}
