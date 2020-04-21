using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.DashboardMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DashboardMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // TAB 1
                AutoMapper.Mapper.CreateMap<DashboardMng_DDC_View, DTO.DashboardMng.DDC>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_Profoma_View, DTO.DashboardMng.Proforma>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_Production_View, DTO.DashboardMng.Production>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // TAB 2
                AutoMapper.Mapper.CreateMap<DashboardMng_ConfirmedContPerMonth_View, DTO.DashboardMng.ConfirmedContainerPerMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_ConfirmedContPerSeason_View, DTO.DashboardMng.ConfirmedContainerPerSeason>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_AllProfomaByMonth_View, DTO.DashboardMng.AllProformaByMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_ConfirmedProfomaByMonth_View, DTO.DashboardMng.ConfirmedProformaByMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // TAB 3
                AutoMapper.Mapper.CreateMap<DashboardMng_CummulativeAll_View, DTO.DashboardMng.AllCummulative>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_CummulativeConfirmed_View, DTO.DashboardMng.ConfirmedCummulative>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // TAB 3
                AutoMapper.Mapper.CreateMap<DashboardMng_Margin_View, DTO.DashboardMng.Margin>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // TAB 5
                AutoMapper.Mapper.CreateMap<DashboardMng_AllSaleProformaByMonth_View, DTO.DashboardMng.AllSaleProformaByMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_ConfirmedSaleProformaByMonth_View, DTO.DashboardMng.ConfirmedSaleProformaByMonth>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_DDCProformaBySale_View, DTO.DashboardMng.DDCProformaBySale>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // TAB 6: sale statistic
                AutoMapper.Mapper.CreateMap<DashboardMng_SaleByClient_View, DTO.DashboardMng.SaleByClient>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_SaleByClientAndSale_View, DTO.DashboardMng.SaleByClientAndSale>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_SaleByCountry_View, DTO.DashboardMng.SaleByCountry>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_SaleByCountryAndSale_View, DTO.DashboardMng.SaleByCountryAndSale>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_ShippedByCountry_View, DTO.DashboardMng.ShippedByCountry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);              
            }
        }

        // TAB 1
        public List<DTO.DashboardMng.DDC> DB2DTO_DDCList(List<DashboardMng_DDC_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_DDC_View>, List<DTO.DashboardMng.DDC>>(dbItems);
        }

        public List<DTO.DashboardMng.Proforma> DB2DTO_ProformaList(List<DashboardMng_Profoma_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_Profoma_View>, List<DTO.DashboardMng.Proforma>>(dbItems);
        }

        public List<DTO.DashboardMng.Production> DB2DTO_ProductionList(List<DashboardMng_Production_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_Production_View>, List<DTO.DashboardMng.Production>>(dbItems);
        }

        // TAB 2
        public List<DTO.DashboardMng.ConfirmedContainerPerMonth> DB2DTO_ConfirmedContPerMonthList(List<DashboardMng_ConfirmedContPerMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_ConfirmedContPerMonth_View>, List<DTO.DashboardMng.ConfirmedContainerPerMonth>>(dbItems);
        }

        public List<DTO.DashboardMng.ConfirmedContainerPerSeason> DB2DTO_ConfirmedContPerSeasonList(List<DashboardMng_ConfirmedContPerSeason_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_ConfirmedContPerSeason_View>, List<DTO.DashboardMng.ConfirmedContainerPerSeason>>(dbItems);
        }

        public List<DTO.DashboardMng.AllProformaByMonth> DB2DTO_AllProformaByMonthList(List<DashboardMng_AllProfomaByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_AllProfomaByMonth_View>, List<DTO.DashboardMng.AllProformaByMonth>>(dbItems);
        }

        public List<DTO.DashboardMng.ConfirmedProformaByMonth> DB2DTO_ConfirmedProformaByMonthList(List<DashboardMng_ConfirmedProfomaByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_ConfirmedProfomaByMonth_View>, List<DTO.DashboardMng.ConfirmedProformaByMonth>>(dbItems);
        }

        // TAB 3
        public List<DTO.DashboardMng.AllCummulative> DB2DTO_AllCummulativeList(List<DashboardMng_CummulativeAll_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_CummulativeAll_View>, List<DTO.DashboardMng.AllCummulative>>(dbItems);
        }

        public List<DTO.DashboardMng.ConfirmedCummulative> DB2DTO_ConfirmedCummulativeList(List<DashboardMng_CummulativeConfirmed_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_CummulativeConfirmed_View>, List<DTO.DashboardMng.ConfirmedCummulative>>(dbItems);
        }

        // TAB 4
        public List<DTO.DashboardMng.Margin> DB2DTO_MarginList(List<DashboardMng_Margin_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_Margin_View>, List<DTO.DashboardMng.Margin>>(dbItems);
        }

        // TAB 5
        public List<DTO.DashboardMng.AllSaleProformaByMonth> DB2DTO_AllSaleProformaByMonthList(List<DashboardMng_AllSaleProformaByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_AllSaleProformaByMonth_View>, List<DTO.DashboardMng.AllSaleProformaByMonth>>(dbItems);
        }

        public List<DTO.DashboardMng.ConfirmedSaleProformaByMonth> DB2DTO_ConfirmedSaleProformaByMonthList(List<DashboardMng_ConfirmedSaleProformaByMonth_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_ConfirmedSaleProformaByMonth_View>, List<DTO.DashboardMng.ConfirmedSaleProformaByMonth>>(dbItems);
        }

        public List<DTO.DashboardMng.DDCProformaBySale> DB2DTO_DDCProformaBySaleList(List<DashboardMng_DDCProformaBySale_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_DDCProformaBySale_View>, List<DTO.DashboardMng.DDCProformaBySale>>(dbItems);
        }

        // TAB 6
        public List<DTO.DashboardMng.SaleByClient> DB2DTO_SaleByClientList(List<DashboardMng_SaleByClient_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_SaleByClient_View>, List<DTO.DashboardMng.SaleByClient>>(dbItems);
        }

        public List<DTO.DashboardMng.SaleByClientAndSale> DB2DTO_SaleByClientAndSaleList(List<DashboardMng_SaleByClientAndSale_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_SaleByClientAndSale_View>, List<DTO.DashboardMng.SaleByClientAndSale>>(dbItems);
        }

        public List<DTO.DashboardMng.SaleByCountry> DB2DTO_SaleByCountryList(List<DashboardMng_SaleByCountry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_SaleByCountry_View>, List<DTO.DashboardMng.SaleByCountry>>(dbItems);
        }

        public List<DTO.DashboardMng.SaleByCountryAndSale> DB2DTO_SaleByCountryAndSaleList(List<DashboardMng_SaleByCountryAndSale_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_SaleByCountryAndSale_View>, List<DTO.DashboardMng.SaleByCountryAndSale>>(dbItems);
        }

        public List<DTO.DashboardMng.ShippedByCountry> DB2DTO_ShippedByCountryList(List<DashboardMng_ShippedByCountry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_ShippedByCountry_View>, List<DTO.DashboardMng.ShippedByCountry>>(dbItems);
        }
    }
}
