using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MISaleProformaRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private MISaleProformaRptEntities CreateContext()
        {
            return new MISaleProformaRptEntities(DALBase.Helper.CreateEntityConnectionString("MISaleProformaRptModel"));
        }

        public DTO.MISaleProformaRpt.ReportData GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MISaleProformaRpt.ReportData data = new DTO.MISaleProformaRpt.ReportData();
            data.ConfirmedContainerPerMonths = new List<DTO.MISaleProformaRpt.ConfirmedContainerPerMonth>();
            data.ConfirmedContainerPerSeasons = new List<DTO.MISaleProformaRpt.ConfirmedContainerPerSeason>();
            data.AllProformaByMonths = new List<DTO.MISaleProformaRpt.AllProformaByMonth>();
            data.ConfirmedProformaByMonths = new List<DTO.MISaleProformaRpt.ConfirmedProformaByMonth>();
            data.AllCummulatives = new List<DTO.MISaleProformaRpt.AllCummulative>();
            data.ConfirmedCummulatives = new List<DTO.MISaleProformaRpt.ConfirmedCummulative>();
            data.EurofarInvoicedCummulatives = new List<DTO.MISaleProformaRpt.EurofarInvoicedCummulative>();
            data.EurofarInvoicedPerMonths = new List<DTO.MISaleProformaRpt.EurofarInvoicedPerMonth>();
            data.EURContainerValue = 0;
            data.USDContainerValue = 0;
            data.ExchangeRate = 0;

            //try to get data
            try
            {
                using (MISaleProformaRptEntities context = CreateContext())
                {
                    data.ConfirmedContainerPerMonths = converter.DB2DTO_ConfirmedContPerMonthList(context.MISaleProformaRpt_function_getConfirmedContPerMonth().ToList());
                    data.ConfirmedContainerPerSeasons = converter.DB2DTO_ConfirmedContPerSeasonList(context.MISaleProformaRpt_ConfirmedContPerSeason_View.ToList());
                    data.AllProformaByMonths = converter.DB2DTO_AllProformaByMonthList(context.MISaleProformaRpt_function_getAllProfomaByMonth().ToList());
                    data.ConfirmedProformaByMonths = converter.DB2DTO_ConfirmedProformaByMonthList(context.MISaleProformaRpt_function_getConfirmedProfomaByMonth().ToList());
                    data.AllCummulatives = converter.DB2DTO_AllCummulativeList(context.MISaleProformaRpt_function_getAllCumulativeProfomaByMonth().ToList());
                    data.ConfirmedCummulatives = converter.DB2DTO_ConfirmedCummulativeList(context.MISaleProformaRpt_function_getConfirmedCumulativeProfomaByMonth().ToList());
                    data.EurofarInvoicedCummulatives = converter.DB2DTO_EurofarInvoicedCummulativeList(context.MISaleProformaRpt_function_getCumulativeEurofarInvoicedByMonth().ToList());
                    data.EurofarInvoicedPerMonths = converter.DB2DTO_EurofarInvoicedPerMonth(context.MISaleProformaRpt_function_getEurofarInvoicedByMonth().ToList());

                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));
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
