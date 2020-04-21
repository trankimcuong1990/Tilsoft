using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.MISaleProformaRpt
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "MISaleProformaRpt";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<MISaleProformaRpt_ConfirmedContPerMonth_View, DTO.MISaleProformaRpt.ConfirmedContainerPerMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleProformaRpt_ConfirmedContPerSeason_View, DTO.MISaleProformaRpt.ConfirmedContainerPerSeason>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleProformaRpt_AllProfomaByMonth_View, DTO.MISaleProformaRpt.AllProformaByMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleProformaRpt_ConfirmedProfomaByMonth_View, DTO.MISaleProformaRpt.ConfirmedProformaByMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleProformaRpt_CummulativeAll_View, DTO.MISaleProformaRpt.AllCummulative>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleProformaRpt_CummulativeConfirmed_View, DTO.MISaleProformaRpt.ConfirmedCummulative>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleProformaRpt_CummulativeEurofarInvoiced_View, DTO.MISaleProformaRpt.EurofarInvoicedCummulative>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<MISaleProformaRpt_EurofarInvoicedByMonth_View, DTO.MISaleProformaRpt.EurofarInvoicedPerMonth>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.MISaleProformaRpt.ConfirmedContainerPerMonth> DB2DTO_ConfirmedContPerMonthList(List<MISaleProformaRpt_ConfirmedContPerMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleProformaRpt_ConfirmedContPerMonth_View>, List<DTO.MISaleProformaRpt.ConfirmedContainerPerMonth>>(dbItems);
        }
        public List<DTO.MISaleProformaRpt.ConfirmedContainerPerSeason> DB2DTO_ConfirmedContPerSeasonList(List<MISaleProformaRpt_ConfirmedContPerSeason_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleProformaRpt_ConfirmedContPerSeason_View>, List<DTO.MISaleProformaRpt.ConfirmedContainerPerSeason>>(dbItems);
        }
        public List<DTO.MISaleProformaRpt.AllProformaByMonth> DB2DTO_AllProformaByMonthList(List<MISaleProformaRpt_AllProfomaByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleProformaRpt_AllProfomaByMonth_View>, List<DTO.MISaleProformaRpt.AllProformaByMonth>>(dbItems);
        }
        public List<DTO.MISaleProformaRpt.ConfirmedProformaByMonth> DB2DTO_ConfirmedProformaByMonthList(List<MISaleProformaRpt_ConfirmedProfomaByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleProformaRpt_ConfirmedProfomaByMonth_View>, List<DTO.MISaleProformaRpt.ConfirmedProformaByMonth>>(dbItems);
        }
        public List<DTO.MISaleProformaRpt.AllCummulative> DB2DTO_AllCummulativeList(List<MISaleProformaRpt_CummulativeAll_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleProformaRpt_CummulativeAll_View>, List<DTO.MISaleProformaRpt.AllCummulative>>(dbItems);
        }
        public List<DTO.MISaleProformaRpt.ConfirmedCummulative> DB2DTO_ConfirmedCummulativeList(List<MISaleProformaRpt_CummulativeConfirmed_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleProformaRpt_CummulativeConfirmed_View>, List<DTO.MISaleProformaRpt.ConfirmedCummulative>>(dbItems);
        }

        public List<DTO.MISaleProformaRpt.EurofarInvoicedCummulative> DB2DTO_EurofarInvoicedCummulativeList(List<MISaleProformaRpt_CummulativeEurofarInvoiced_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleProformaRpt_CummulativeEurofarInvoiced_View>, List<DTO.MISaleProformaRpt.EurofarInvoicedCummulative>>(dbItems);
        }

        public List<DTO.MISaleProformaRpt.EurofarInvoicedPerMonth> DB2DTO_EurofarInvoicedPerMonth(List<MISaleProformaRpt_EurofarInvoicedByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<MISaleProformaRpt_EurofarInvoicedByMonth_View>, List<DTO.MISaleProformaRpt.EurofarInvoicedPerMonth>>(dbItems);
        }
    }
}
