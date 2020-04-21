using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MISaleByClientRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MISaleByClientRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ProformaInvoiceConfirmed_View, DTO.MISaleByClientRpt.ProformaInvoiceConfirmed>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ProformaInvoice_View, DTO.MISaleByClientRpt.ProformaInvoice>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_Expectation_View, DTO.MISaleByClientRpt.Expectation>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_EurofarCommercialInvoice_View, DTO.MISaleByClientRpt.EurofarCommercialInvoice>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_EurofarCommercialInvoiceOfNewClient_View, DTO.MISaleByClientRpt.EurofarCommercialInvoiceOfNewClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_EurofarCommercialInvoiceOfLostedClient_View, DTO.MISaleByClientRpt.EurofarCommercialInvoiceOfLostedClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ProformaInvoiceOfLostedClient_View, DTO.MISaleByClientRpt.ProformaInvoiceOfLostedClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ProformaInvoiceOfNewClient_View, DTO.MISaleByClientRpt.ProformaInvoiceOfNewClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ConfirmedDetail_View, DTO.MISaleByClientRpt.ConfirmedDetail>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ConfirmedSummary_View, DTO.MISaleByClientRpt.ConfirmedSummary>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<MISaleByClientRpt_Detail_View, DTO.MISaleByClientRpt.Detail>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //AutoMapper.Mapper.CreateMap<MISaleByClientRpt_Summary_View, DTO.MISaleByClientRpt.Summary>()
                //    .IgnoreAllNonExisting()
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));



                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }


        public List<DTO.MISaleByClientRpt.ProformaInvoiceConfirmed> DB2DTO_ProformaInvoiceConfirmed(List<MISaleByClientRpt_ProformaInvoiceConfirmed_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ProformaInvoiceConfirmed_View>, List<DTO.MISaleByClientRpt.ProformaInvoiceConfirmed>>(dbItems);
        }

        public List<DTO.MISaleByClientRpt.ProformaInvoice> DB2DTO_ProformaInvoice(List<MISaleByClientRpt_ProformaInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ProformaInvoice_View>, List<DTO.MISaleByClientRpt.ProformaInvoice>>(dbItems);
        }

        public List<DTO.MISaleByClientRpt.Expectation> DB2DTO_Expectation(List<MISaleByClientRpt_Expectation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_Expectation_View>, List<DTO.MISaleByClientRpt.Expectation>>(dbItems);
        }

        public List<DTO.MISaleByClientRpt.EurofarCommercialInvoice> DB2DTO_EurofarCommercialInvoice(List<MISaleByClientRpt_EurofarCommercialInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_EurofarCommercialInvoice_View>, List<DTO.MISaleByClientRpt.EurofarCommercialInvoice>>(dbItems);
        }

        public List<DTO.MISaleByClientRpt.EurofarCommercialInvoiceOfNewClient> DB2DTO_EurofarCommercialInvoice_NewClient(List<MISaleByClientRpt_EurofarCommercialInvoiceOfNewClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_EurofarCommercialInvoiceOfNewClient_View>, List<DTO.MISaleByClientRpt.EurofarCommercialInvoiceOfNewClient>>(dbItems);
        }

        public List<DTO.MISaleByClientRpt.EurofarCommercialInvoiceOfLostedClient> DB2DTO_EurofarCommercialInvoice_LostedClient(List<MISaleByClientRpt_EurofarCommercialInvoiceOfLostedClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_EurofarCommercialInvoiceOfLostedClient_View>, List<DTO.MISaleByClientRpt.EurofarCommercialInvoiceOfLostedClient>>(dbItems);
        }

        public List<DTO.MISaleByClientRpt.ProformaInvoiceOfLostedClient> DB2DTO_ProformaInvoice_LostedClient(List<MISaleByClientRpt_ProformaInvoiceOfLostedClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ProformaInvoiceOfLostedClient_View>, List<DTO.MISaleByClientRpt.ProformaInvoiceOfLostedClient>>(dbItems);
        }

        public List<DTO.MISaleByClientRpt.ProformaInvoiceOfNewClient> DB2DTO_ProformaInvoice_NewClient(List<MISaleByClientRpt_ProformaInvoiceOfNewClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ProformaInvoiceOfNewClient_View>, List<DTO.MISaleByClientRpt.ProformaInvoiceOfNewClient>>(dbItems);
        }


        //public List<DTO.MISaleByClientRpt.ConfirmedDetail> DB2DTO_ConfirmedDetailList(List<MISaleByClientRpt_ConfirmedDetail_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ConfirmedDetail_View>, List<DTO.MISaleByClientRpt.ConfirmedDetail>>(dbItems);
        //}

        //public List<DTO.MISaleByClientRpt.ConfirmedSummary> DB2DTO_ConfirmedSummaryList(List<MISaleByClientRpt_ConfirmedSummary_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ConfirmedSummary_View>, List<DTO.MISaleByClientRpt.ConfirmedSummary>>(dbItems);
        //}

        //public List<DTO.MISaleByClientRpt.Detail> DB2DTO_DetailList(List<MISaleByClientRpt_Detail_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<MISaleByClientRpt_Detail_View>, List<DTO.MISaleByClientRpt.Detail>>(dbItems);
        //}

        //public List<DTO.MISaleByClientRpt.Summary> DB2DTO_SummaryList(List<MISaleByClientRpt_Summary_View> dbItems)
        //{
        //    return AutoMapper.Mapper.Map<List<MISaleByClientRpt_Summary_View>, List<DTO.MISaleByClientRpt.Summary>>(dbItems);
        //}


    }
}
