using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MISaleByCountryRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private MISaleByCountryRptEntities CreateContext()
        {
            return new MISaleByCountryRptEntities(DALBase.Helper.CreateEntityConnectionString("MISaleByCountryRptModel"));
        }

        public DTO.MISaleByCountryRpt.ReportData GetReportData(string season, int? saleId, out Library.DTO.Notification notification)
        {            
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MISaleByCountryRpt.ReportData data = new DTO.MISaleByCountryRpt.ReportData();
            data.Summaries = new List<DTO.MISaleByCountryRpt.Summary>();
            data.Details = new List<DTO.MISaleByCountryRpt.Detail>();
            data.ConfirmedSummaries = new List<DTO.MISaleByCountryRpt.ConfirmedSummary>();
            data.ConfirmedDetails = new List<DTO.MISaleByCountryRpt.ConfirmedDetail>();
            data.EURContainerValue = 0;
            data.USDContainerValue = 0;
            data.ExchangeRate = 0;

            string prevSeason = Library.Helper.GetPreviousSeason(season);
            //try to get data
            try
            {
                using (MISaleByCountryRptEntities context = CreateContext())
                {
                    data.ExpectedSummaries = converter.DB2DTO_ExpectedSummaryList(context.MISaleByCountryRpt_function_GetExpectedByCountry(season, saleId.GetValueOrDefault(-1)).ToList());
                    data.Summaries = converter.DB2DTO_SummaryList(context.MISaleByCountryRpt_function_GetSummaryByCountry(season, saleId.GetValueOrDefault(-1)).ToList());
                    data.ConfirmedSummaries = converter.DB2DTO_ConfirmedSummaryList(context.MISaleByCountryRpt_function_GetConfirmedSummaryByCountry(season, saleId.GetValueOrDefault(-1)).ToList());
                    data.CommercialInvoiceSummaries = converter.DB2DTO_CommercialInvoiceSummaryList(context.MISaleByCountryRpt_function_getCommercialInvoiceByCountry(season, saleId.GetValueOrDefault(-1)).ToList());

                    //data.Details = converter.DB2DTO_DetailList(context.MISaleByCountryRpt_Detail_View.Where(o => o.Season == season).ToList()); // Ko can sai nua
                    //data.ConfirmedDetails = converter.DB2DTO_ConfirmedDetailList(context.MISaleByCountryRpt_ConfirmedDetail_View.Where(o => o.Season == season).ToList()); // Ko can sai nua
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

        public DTO.MISaleByCountryRpt.ReportData GetReportForMiDelta(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MISaleByCountryRpt.ReportData data = new DTO.MISaleByCountryRpt.ReportData();
            data.Summaries = new List<DTO.MISaleByCountryRpt.Summary>();
            data.Details = new List<DTO.MISaleByCountryRpt.Detail>();
            data.ConfirmedSummaries = new List<DTO.MISaleByCountryRpt.ConfirmedSummary>();
            data.ConfirmedDetails = new List<DTO.MISaleByCountryRpt.ConfirmedDetail>();
            data.EURContainerValue = 0;
            data.USDContainerValue = 0;
            data.ExchangeRate = 0;

            string prevSeason = Library.Helper.GetPreviousSeason(season);
            //try to get data
            try
            {
                using (MISaleByCountryRptEntities context = CreateContext())
                {
                    data.CommercialInvoiceSummaries = converter.DB2DTO_CommercialInvoiceSummaryList(context.MISaleByCountryRpt_function_getCommercialInvoiceByCountry(season, -1).ToList());

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
