using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MISaleConclusionRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MISaleConclusionRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getConfirmedProformaTopClient_Result, DTO.MISaleConclusionRpt.ConfirmedProformaTopClient>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getExpected_Result, DTO.MISaleConclusionRpt.Expected>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getExpectedCountry_Result, DTO.MISaleConclusionRpt.ExpectedCountry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getExpectedTopClient_Result, DTO.MISaleConclusionRpt.ExpectedTopClient>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getRangeConfirmedProforma_Result, DTO.MISaleConclusionRpt.RangeConfirmedProforma>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getRangeExpected_Result, DTO.MISaleConclusionRpt.RangeExpected>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getConfirmedProformaCountry_Result, DTO.MISaleConclusionRpt.ConfirmedProformaCountry>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getCommercialInvoiceCountry_Result, DTO.MISaleConclusionRpt.CommercialInvoiceCountry>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getProformaCountry_Result, DTO.MISaleConclusionRpt.ProformaCountry>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getProformaInvoiceTopClient_Result, DTO.MISaleConclusionRpt.ProformaInvoiceTopClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getRangeProformaInvoice_Result, DTO.MISaleConclusionRpt.RangeProformaInvoice>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_GetProformaInvoice_Result, DTO.MISaleConclusionRpt.ProformaInvoice>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_GetConfirmedProforma_Result, DTO.MISaleConclusionRpt.ConfirmedProforma>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getClient_Result, DTO.MISaleConclusionRpt.Client>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_GetDelta_Result, DTO.MISaleConclusionRpt.Delta>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleConclusionRpt_function_getExpectedByClient_Result, DTO.MISaleConclusionRpt.ExpectedByClient>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MISaleConclusionRpt.ConfirmedProformaTopClient> DB2DTO_ConfirmedProformaTopClientList(List<MISaleConclusionRpt_function_getConfirmedProformaTopClient_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getConfirmedProformaTopClient_Result>, List<DTO.MISaleConclusionRpt.ConfirmedProformaTopClient>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.Expected> DB2DTO_ExpectedList(List<MISaleConclusionRpt_function_getExpected_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getExpected_Result>, List<DTO.MISaleConclusionRpt.Expected>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.ExpectedCountry> DB2DTO_ExpectedCountryList(List<MISaleConclusionRpt_function_getExpectedCountry_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getExpectedCountry_Result>, List<DTO.MISaleConclusionRpt.ExpectedCountry>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.ExpectedTopClient> DB2DTO_ExpectedTopClientList(List<MISaleConclusionRpt_function_getExpectedTopClient_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getExpectedTopClient_Result>, List<DTO.MISaleConclusionRpt.ExpectedTopClient>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.RangeConfirmedProforma> DB2DTO_RangeConfirmedProformaList(List<MISaleConclusionRpt_function_getRangeConfirmedProforma_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getRangeConfirmedProforma_Result>, List<DTO.MISaleConclusionRpt.RangeConfirmedProforma>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.RangeExpected> DB2DTO_RangeExpectedList(List<MISaleConclusionRpt_function_getRangeExpected_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getRangeExpected_Result>, List<DTO.MISaleConclusionRpt.RangeExpected>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.ConfirmedProformaCountry> DB2DTO_ConfirmedProformaCountry(List<MISaleConclusionRpt_function_getConfirmedProformaCountry_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getConfirmedProformaCountry_Result>, List<DTO.MISaleConclusionRpt.ConfirmedProformaCountry>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.CommercialInvoiceCountry> DB2DTO_CommercialInvoiceCountry(List<MISaleConclusionRpt_function_getCommercialInvoiceCountry_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getCommercialInvoiceCountry_Result>, List<DTO.MISaleConclusionRpt.CommercialInvoiceCountry>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.ProformaCountry> DB2DTO_ProformasCountry(List<MISaleConclusionRpt_function_getProformaCountry_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getProformaCountry_Result>, List<DTO.MISaleConclusionRpt.ProformaCountry>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.ProformaInvoiceTopClient> DB2DTO_ProformaInvoiceTopClients(List<MISaleConclusionRpt_function_getProformaInvoiceTopClient_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getProformaInvoiceTopClient_Result>, List<DTO.MISaleConclusionRpt.ProformaInvoiceTopClient>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.RangeProformaInvoice> DB2DTO_RangeProformaInvoice(List<MISaleConclusionRpt_function_getRangeProformaInvoice_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getRangeProformaInvoice_Result>, List<DTO.MISaleConclusionRpt.RangeProformaInvoice>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.ProformaInvoice> DB2DTO_ProformaInvoice(List<MISaleConclusionRpt_function_GetProformaInvoice_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_GetProformaInvoice_Result>, List<DTO.MISaleConclusionRpt.ProformaInvoice>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.ConfirmedProforma> DB2DTO_ConfirmedProforma(List<MISaleConclusionRpt_function_GetConfirmedProforma_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_GetConfirmedProforma_Result>, List<DTO.MISaleConclusionRpt.ConfirmedProforma>>(dbItems);
        }

        public List<DTO.MISaleConclusionRpt.Client> DB2DTO_Client(List<MISaleConclusionRpt_function_getClient_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getClient_Result>, List<DTO.MISaleConclusionRpt.Client>>(dbItems);
        }



    }
}
