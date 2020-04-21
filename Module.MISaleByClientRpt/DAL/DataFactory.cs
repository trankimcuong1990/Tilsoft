using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByClientRpt.DAL
{
    internal class DataFactory
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private MISaleByClientRptEntities CreateContext()
        {
            return new MISaleByClientRptEntities(Library.Helper.CreateEntityConnectionString("DAL.MISaleByClientRptModel"));
        }        

        public DTO.ReportData GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportData data = new DTO.ReportData();
            string prevSeason = Library.Helper.GetPreviousSeason(season);
            try
            {
                using (MISaleByClientRptEntities context = CreateContext())
                {
                    //data.ProformaInvoiceConfirmeds = converter.DB2DTO_ProformaInvoiceConfirmed(context.MISaleByClientRpt_function_getProformaInvoiceConfirmed(season).ToList());
                    //data.ProformaInvoices = converter.DB2DTO_ProformaInvoice(context.MISaleByClientRpt_function_getProformaInvoice(season).ToList());
                    //data.Expectations = converter.DB2DTO_Expectation(context.MISaleByClientRpt_function_getExpectation(season).ToList());
                    //data.EurofarCommercialInvoices = converter.DB2DTO_EurofarCommercialInvoice(context.MISaleByClientRpt_function_getEurofarCommercialInvoice(season).ToList());

                    //proforma lost/new  clients current season
                    data.ProformaInvoiceOfLostedClients = converter.DB2DTO_ProformaInvoice_LostedClient(context.MISaleByClientRpt_function_getProformaInvoiceOfLostedClient(season).ToList());
                    data.ProformaInvoiceOfNewClients = converter.DB2DTO_ProformaInvoice_NewClient(context.MISaleByClientRpt_function_getProformaInvoiceOfNewClient(season).ToList());

                    //commercial invoice lost/new  clients current season
                    data.EurofarCommercialInvoiceOfNewClientThisSeason = converter.DB2DTO_EurofarCommercialInvoice_NewClient(context.MISaleByClientRpt_function_getEurofarCommercialInvoiceOfNewClient(season).ToList());
                    data.EurofarCommercialInvoiceOfLostedClientThisSeason = converter.DB2DTO_EurofarCommercialInvoice_LostedClient(context.MISaleByClientRpt_function_getEurofarCommercialInvoiceOfLostedClient(season).ToList());

                    //commercial invoice lost/new  clients last season
                    data.EurofarCommercialInvoiceOfNewClientLastSeason = converter.DB2DTO_EurofarCommercialInvoice_NewClient(context.MISaleByClientRpt_function_getEurofarCommercialInvoiceOfNewClient(prevSeason).ToList());
                    data.EurofarCommercialInvoiceOfLostedClientLastSeason = converter.DB2DTO_EurofarCommercialInvoice_LostedClient(context.MISaleByClientRpt_function_getEurofarCommercialInvoiceOfLostedClient(prevSeason).ToList());

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
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return data;
        }

        public List<DTO.EurofarCommercialInvoice> GetEurofarCommercialInvoices(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.EurofarCommercialInvoice> data = new List<DTO.EurofarCommercialInvoice>();
            string prevSeason = Library.Helper.GetPreviousSeason(season);
            try
            {
                using (MISaleByClientRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_EurofarCommercialInvoice(context.MISaleByClientRpt_function_getEurofarCommercialInvoice(season).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.Expectation> GetExpectations(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.Expectation> data = new List<DTO.Expectation>();
            try
            {
                using (MISaleByClientRptEntities context = CreateContext())
                {
                    var expectation = context.MISaleByClientRpt_function_getExpectation(season).ToList();
                    data = converter.DB2DTO_Expectation(expectation);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return data;
        }

        public List<DTO.ProformaInvoiceConfirmed> GetProformaInvoiceConfirmed(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ProformaInvoiceConfirmed> data = new List<DTO.ProformaInvoiceConfirmed>();
            string prevSeason = Library.Helper.GetPreviousSeason(season);
            try
            {
                using (MISaleByClientRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ProformaInvoiceConfirmed(context.MISaleByClientRpt_function_getProformaInvoiceConfirmed(season).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.ProformaInvoice> GetProformaInvoice(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ProformaInvoice> data = new List<DTO.ProformaInvoice>();
            string prevSeason = Library.Helper.GetPreviousSeason(season);
            try
            {
                using (MISaleByClientRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ProformaInvoice(context.MISaleByClientRpt_function_getProformaInvoice(season).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }


    }
}
