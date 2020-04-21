using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DashboardMng
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
         private DashboardMngEntities CreateContext()
        {
            return new DashboardMngEntities(DALBase.Helper.CreateEntityConnectionString("DashboardMng"));
        }

         public DTO.DashboardMng.DashboardReportData GetReportData(string season, out Library.DTO.Notification notification)
         {
             notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
             DTO.DashboardMng.DashboardReportData data = new DTO.DashboardMng.DashboardReportData();
             // TAB 1
             data.DDCs = new List<DTO.DashboardMng.DDC>();
             data.Productions = new List<DTO.DashboardMng.Production>();
             data.Proformas = new List<DTO.DashboardMng.Proforma>();

             // TAB 2
             data.ConfirmedContainerPerMonths = new List<DTO.DashboardMng.ConfirmedContainerPerMonth>();
             data.ConfirmedContainerPerSeasons = new List<DTO.DashboardMng.ConfirmedContainerPerSeason>();
             data.AllProformaByMonths = new List<DTO.DashboardMng.AllProformaByMonth>();
             data.ConfirmedProformaByMonths = new List<DTO.DashboardMng.ConfirmedProformaByMonth>();

             // TAB 3
             data.AllCummulatives = new List<DTO.DashboardMng.AllCummulative>();
             data.ConfirmedCummulatives = new List<DTO.DashboardMng.ConfirmedCummulative>();

             // TAB 4
             data.Margins = new List<DTO.DashboardMng.Margin>();

             // TAB 5
             data.AllSaleProformaByMonths = new List<DTO.DashboardMng.AllSaleProformaByMonth>();
             data.ConfirmedSaleProformaByMonths = new List<DTO.DashboardMng.ConfirmedSaleProformaByMonth>();
             data.DDCProformaBySales = new List<DTO.DashboardMng.DDCProformaBySale>();

             // TAB 6
             data.SaleByClients = new List<DTO.DashboardMng.SaleByClient>();
             data.SaleByClientAndSales = new List<DTO.DashboardMng.SaleByClientAndSale>();
             data.SaleByCountrys = new List<DTO.DashboardMng.SaleByCountry>();
             data.SaleByCountryAndSales = new List<DTO.DashboardMng.SaleByCountryAndSale>();
             data.ShippedByCountrys = new List<DTO.DashboardMng.ShippedByCountry>();

             //try to get data
             try
             {
                 using (DashboardMngEntities context = CreateContext())
                 {
                     // TAB 1
                     data.DDCs = converter.DB2DTO_DDCList(context.DashboardMng_function_getDDC(season).ToList());
                     data.Proformas = converter.DB2DTO_ProformaList(context.DashboardMng_function_getProforma(season).ToList());
                     data.Productions = converter.DB2DTO_ProductionList(context.DashboardMng_Production_View.Where(o => o.Season == season).ToList());

                     // TAB 2
                     data.ConfirmedContainerPerMonths = converter.DB2DTO_ConfirmedContPerMonthList(context.DashboardMng_function_getConfirmedContPerMonth().ToList());
                     data.ConfirmedContainerPerSeasons = converter.DB2DTO_ConfirmedContPerSeasonList(context.DashboardMng_ConfirmedContPerSeason_View.ToList());
                     data.AllProformaByMonths = converter.DB2DTO_AllProformaByMonthList(context.DashboardMng_function_getAllProfomaByMonth().ToList());
                     data.ConfirmedProformaByMonths = converter.DB2DTO_ConfirmedProformaByMonthList(context.DashboardMng_function_getConfirmedProfomaByMonth().ToList());

                     // TAB 3
                     data.AllCummulatives = converter.DB2DTO_AllCummulativeList(context.DashboardMng_function_getAllCumulativeProfomaByMonth().ToList());
                     data.ConfirmedCummulatives = converter.DB2DTO_ConfirmedCummulativeList(context.DashboardMng_function_getConfirmedCumulativeProfomaByMonth().ToList());

                     // TAB 4
                     data.Margins = converter.DB2DTO_MarginList(context.DashboardMng_function_getMargin().ToList());

                     // TAB 5
                     data.AllSaleProformaByMonths = converter.DB2DTO_AllSaleProformaByMonthList(context.DashboardMng_function_getAllSaleProfomaByMonth(season).ToList());
                     data.ConfirmedSaleProformaByMonths = converter.DB2DTO_ConfirmedSaleProformaByMonthList(context.DashboardMng_function_getConfirmedSaleProfomaByMonth(season).ToList());
                     data.DDCProformaBySales = converter.DB2DTO_DDCProformaBySaleList(context.DashboardMng_function_getDDCProformaBySale(season).ToList());

                     // TAB 6
                     data.SaleByClients = converter.DB2DTO_SaleByClientList(context.DashboardMng_SaleByClient_View.Where(o=>o.Season == season).ToList());
                     data.SaleByClientAndSales = converter.DB2DTO_SaleByClientAndSaleList(context.DashboardMng_SaleByClientAndSale_View.Where(o=>o.Season == season).ToList());
                     data.SaleByCountrys = converter.DB2DTO_SaleByCountryList(context.DashboardMng_SaleByCountry_View.Where(o=>o.Season == season).ToList());
                     data.SaleByCountryAndSales = converter.DB2DTO_SaleByCountryAndSaleList(context.DashboardMng_SaleByCountryAndSale_View.Where(o => o.Season == season).ToList());
                     data.ShippedByCountrys = converter.DB2DTO_ShippedByCountryList(context.DashboardMng_ShippedByCountry_View.Where(o => o.Season == season).ToList());
                 }
             }
             catch (Exception ex)
             {
                 notification.Type = Library.DTO.NotificationType.Error;
                 notification.Message = ex.Message;
             }

             return data;
         }
    }
}
