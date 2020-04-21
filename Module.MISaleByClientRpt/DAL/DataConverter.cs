using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.MISaleByClientRpt.DAL
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
                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ProformaInvoiceConfirmed_View, DTO.ProformaInvoiceConfirmed>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ProformaInvoice_View, DTO.ProformaInvoice>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_Expectation_View, DTO.Expectation>()
                 .IgnoreAllNonExisting()
                 .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_EurofarCommercialInvoice_View, DTO.EurofarCommercialInvoice>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_EurofarCommercialInvoiceOfNewClient_View, DTO.EurofarCommercialInvoiceOfNewClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_EurofarCommercialInvoiceOfLostedClient_View, DTO.EurofarCommercialInvoiceOfLostedClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ProformaInvoiceOfLostedClient_View, DTO.ProformaInvoiceOfLostedClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleByClientRpt_ProformaInvoiceOfNewClient_View, DTO.ProformaInvoiceOfNewClient>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ProformaInvoiceConfirmed> DB2DTO_ProformaInvoiceConfirmed(List<MISaleByClientRpt_ProformaInvoiceConfirmed_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ProformaInvoiceConfirmed_View>, List<DTO.ProformaInvoiceConfirmed>>(dbItems);
        }
        
        public List<DTO.ProformaInvoice> DB2DTO_ProformaInvoice(List<MISaleByClientRpt_ProformaInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ProformaInvoice_View>, List<DTO.ProformaInvoice>>(dbItems);
        }

        public List<DTO.Expectation> DB2DTO_Expectation(List<MISaleByClientRpt_Expectation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_Expectation_View>, List<DTO.Expectation>>(dbItems);
        }

        public List<DTO.EurofarCommercialInvoice> DB2DTO_EurofarCommercialInvoice(List<MISaleByClientRpt_EurofarCommercialInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_EurofarCommercialInvoice_View>, List<DTO.EurofarCommercialInvoice>>(dbItems);
        }

        public List<DTO.EurofarCommercialInvoiceOfNewClient> DB2DTO_EurofarCommercialInvoice_NewClient(List<MISaleByClientRpt_EurofarCommercialInvoiceOfNewClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_EurofarCommercialInvoiceOfNewClient_View>, List<DTO.EurofarCommercialInvoiceOfNewClient>>(dbItems);
        }

        public List<DTO.EurofarCommercialInvoiceOfLostedClient> DB2DTO_EurofarCommercialInvoice_LostedClient(List<MISaleByClientRpt_EurofarCommercialInvoiceOfLostedClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_EurofarCommercialInvoiceOfLostedClient_View>, List<DTO.EurofarCommercialInvoiceOfLostedClient>>(dbItems);
        }

        public List<DTO.ProformaInvoiceOfLostedClient> DB2DTO_ProformaInvoice_LostedClient(List<MISaleByClientRpt_ProformaInvoiceOfLostedClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ProformaInvoiceOfLostedClient_View>, List<DTO.ProformaInvoiceOfLostedClient>>(dbItems);
        }

        public List<DTO.ProformaInvoiceOfNewClient> DB2DTO_ProformaInvoice_NewClient(List<MISaleByClientRpt_ProformaInvoiceOfNewClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleByClientRpt_ProformaInvoiceOfNewClient_View>, List<DTO.ProformaInvoiceOfNewClient>>(dbItems);
        }
    }
}
