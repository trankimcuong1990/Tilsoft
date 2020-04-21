using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MISaleByClientRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private MISaleByClientRptEntities CreateContext()
        {
            return new MISaleByClientRptEntities(DALBase.Helper.CreateEntityConnectionString("MISaleByClientRptModel"));
        }

        public DTO.MISaleByClientRpt.ReportData GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MISaleByClientRpt.ReportData data = new DTO.MISaleByClientRpt.ReportData();
            
            data.Expectations = new List<DTO.MISaleByClientRpt.Expectation>();
            data.EurofarCommercialInvoices = new List<DTO.MISaleByClientRpt.EurofarCommercialInvoice>();
            data.EurofarCommercialInvoiceOfNewClients = new List<DTO.MISaleByClientRpt.EurofarCommercialInvoiceOfNewClient>();
            data.EurofarCommercialInvoiceOfLostedClients = new List<DTO.MISaleByClientRpt.EurofarCommercialInvoiceOfLostedClient>();

            //data.ConfirmedDetails = new List<DTO.MISaleByClientRpt.ConfirmedDetail>();
            //data.ConfirmedSummaries = new List<DTO.MISaleByClientRpt.ConfirmedSummary>();
            //data.Details = new List<DTO.MISaleByClientRpt.Detail>();
            //data.Summaries = new List<DTO.MISaleByClientRpt.Summary>();

            data.EURContainerValue = 0;
            data.USDContainerValue = 0;
            data.ExchangeRate = 0;

            string prevSeason = Library.Helper.GetPreviousSeason(season);
            //try to get data
            try
            {
                using (MISaleByClientRptEntities context = CreateContext())
                {
                    data.ProformaInvoiceConfirmeds = converter.DB2DTO_ProformaInvoiceConfirmed(context.MISaleByClientRpt_function_getProformaInvoiceConfirmed(season).ToList());
                    data.ProformaInvoices = converter.DB2DTO_ProformaInvoice(context.MISaleByClientRpt_function_getProformaInvoice(season).ToList());
                    data.Expectations = converter.DB2DTO_Expectation(context.MISaleByClientRpt_function_getExpectation(season).ToList());
                    data.EurofarCommercialInvoices = converter.DB2DTO_EurofarCommercialInvoice(context.MISaleByClientRpt_function_getEurofarCommercialInvoice(season).ToList());

                    //commercial invoice lost/new  clients last season
                    data.EurofarCommercialInvoiceOfNewClients = converter.DB2DTO_EurofarCommercialInvoice_NewClient(context.MISaleByClientRpt_function_getEurofarCommercialInvoiceOfNewClient(prevSeason).ToList());
                    data.EurofarCommercialInvoiceOfLostedClients = converter.DB2DTO_EurofarCommercialInvoice_LostedClient(context.MISaleByClientRpt_function_getEurofarCommercialInvoiceOfLostedClient(prevSeason).ToList());

                    //commercial invoice lost/new  clients current season
                    data.EurofarCommercialInvoiceOfNewClient2s = converter.DB2DTO_EurofarCommercialInvoice_NewClient(context.MISaleByClientRpt_function_getEurofarCommercialInvoiceOfNewClient(season).ToList());
                    data.EurofarCommercialInvoiceOfLostedClient2s = converter.DB2DTO_EurofarCommercialInvoice_LostedClient(context.MISaleByClientRpt_function_getEurofarCommercialInvoiceOfLostedClient(season).ToList());

                    //proforma lost/new  clients current season
                    data.ProformaInvoiceOfLostedClients = converter.DB2DTO_ProformaInvoice_LostedClient(context.MISaleByClientRpt_function_getProformaInvoiceOfLostedClient(season).ToList());
                    data.ProformaInvoiceOfNewClients = converter.DB2DTO_ProformaInvoice_NewClient(context.MISaleByClientRpt_function_getProformaInvoiceOfNewClient(season).ToList());

                    //param value
                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));

                    //Do not need anymore (Iron Request)
                    //data.ConfirmedDetails = converter.DB2DTO_ConfirmedDetailList(context.MISaleByClientRpt_function_getConfirmedDetail(season).ToList());
                    //data.ConfirmedSummaries = converter.DB2DTO_ConfirmedSummaryList(context.MISaleByClientRpt_function_getConfirmedSummary(season).ToList());
                    //data.Details = converter.DB2DTO_DetailList(context.MISaleByClientRpt_function_getDetail(season).ToList());
                    //data.Summaries = converter.DB2DTO_SummaryList(context.MISaleByClientRpt_function_getSummary(season).ToList());
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
