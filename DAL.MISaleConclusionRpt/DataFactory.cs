using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MISaleConclusionRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private MISaleConclusionRptEntities CreateContext()
        {
            return new MISaleConclusionRptEntities(DALBase.Helper.CreateEntityConnectionString("MISaleConclusionRptModel"));
        }

        public DTO.MISaleConclusionRpt.ReportData GetReportData(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MISaleConclusionRpt.ReportData data = new DTO.MISaleConclusionRpt.ReportData();

            //4 first list
            data.ProformaCountries = new List<DTO.MISaleConclusionRpt.ProformaCountry>();
            data.ConfirmedProformaCountries = new List<DTO.MISaleConclusionRpt.ConfirmedProformaCountry>();
            data.ExpectedCountries = new List<DTO.MISaleConclusionRpt.ExpectedCountry>();
            data.CommercialInvoiceCountries = new List<DTO.MISaleConclusionRpt.CommercialInvoiceCountry>();

            //expected
            data.ExpectedTopClients = new List<DTO.MISaleConclusionRpt.ExpectedTopClient>();
            data.RangeExpected = new List<DTO.MISaleConclusionRpt.RangeExpected>();
            data.Expected = new List<DTO.MISaleConclusionRpt.Expected>();

            //proforma invoice confirmed
            data.ConfirmedProformaTopClients = new List<DTO.MISaleConclusionRpt.ConfirmedProformaTopClient>();
            data.RangeConfirmedProformas = new List<DTO.MISaleConclusionRpt.RangeConfirmedProforma>();
            data.ConfirmedProformas = new List<DTO.MISaleConclusionRpt.ConfirmedProforma>();

            //proforma invoice
            data.ProformaInvoiceTopClients = new List<DTO.MISaleConclusionRpt.ProformaInvoiceTopClient>();
            data.RangeProformaInvoices = new List<DTO.MISaleConclusionRpt.RangeProformaInvoice>();
            data.ProformaCountries = new List<DTO.MISaleConclusionRpt.ProformaCountry>();

            data.EURContainerValue = 0;
            data.USDContainerValue = 0;
            data.ExchangeRate = 0;
            
            string prevSeason = Library.Helper.GetPreviousSeason(season);
            saleId = saleId.GetValueOrDefault(-1);
            //try to get data
            try
            {
                using (MISaleConclusionRptEntities context = CreateContext())
                {
                    data.ProformaCountries = converter.DB2DTO_ProformasCountry(context.MISaleConclusionRpt_function_getProformaCountry(season, saleId).ToList());
                    data.ConfirmedProformaCountries = converter.DB2DTO_ConfirmedProformaCountry(context.MISaleConclusionRpt_function_getConfirmedProformaCountry(season, saleId).ToList());
                    data.ExpectedCountries = converter.DB2DTO_ExpectedCountryList(context.MISaleConclusionRpt_function_getExpectedCountry(season, saleId).ToList());
                    data.CommercialInvoiceCountries = converter.DB2DTO_CommercialInvoiceCountry(context.MISaleConclusionRpt_function_getCommercialInvoiceCountry(season, saleId).ToList());

                    //EXPECTED: TOP 10 CLIENTS BY COUNTRY, CLIENT & SALES (EXPECTED - 2017/2018), DISTRIBUTION OF CLIENTS (EXPECTED - 2017/2018) 
                    data.ExpectedTopClients = converter.DB2DTO_ExpectedTopClientList(context.MISaleConclusionRpt_function_getExpectedTopClient(season, saleId).ToList());
                    data.RangeExpected = converter.DB2DTO_RangeExpectedList(context.MISaleConclusionRpt_function_getRangeExpected(season, saleId).ToList());
                    data.Expected = converter.DB2DTO_ExpectedList(context.MISaleConclusionRpt_function_getExpected(season, saleId).OrderByDescending(o => o.TotalAmount).Take(25).ToList());

                    //PROFORMA INVOICE CONFIRMED: TOP 10 CLIENTS BY COUNTRY, CLIENT & SALES(PI CONFIRMED - 2017 / 2018), DISTRIBUTION OF CLIENTS(PI CONFIRMED - 2017 / 2018)
                    data.ConfirmedProformaTopClients = converter.DB2DTO_ConfirmedProformaTopClientList(context.MISaleConclusionRpt_function_getConfirmedProformaTopClient(season, saleId).ToList());
                    data.RangeConfirmedProformas = converter.DB2DTO_RangeConfirmedProformaList(context.MISaleConclusionRpt_function_getRangeConfirmedProforma(season, saleId).ToList());
                    data.ConfirmedProformas = converter.DB2DTO_ConfirmedProforma(context.MISaleConclusionRpt_function_GetConfirmedProforma(season, saleId).OrderByDescending(o => o.TotalAmount).Take(25).ToList());

                    //PROFORMA INVOICE: TOP 10 CLIENTS BY COUNTRY, CLIENT & SALES(PI - 2017 / 2018), DISTRIBUTION OF CLIENTS(PI - 2017 / 2018)
                    data.ProformaInvoiceTopClients = converter.DB2DTO_ProformaInvoiceTopClients(context.MISaleConclusionRpt_function_getProformaInvoiceTopClient(season, saleId).ToList());
                    data.RangeProformaInvoices = converter.DB2DTO_RangeProformaInvoice(context.MISaleConclusionRpt_function_getRangeProformaInvoice(season, saleId).ToList());
                    data.ProformaInvoices = converter.DB2DTO_ProformaInvoice(context.MISaleConclusionRpt_function_GetProformaInvoice(season, saleId).OrderByDescending(o => o.TotalAmount).Take(25).ToList());

                    //DELTA
                    var delta = context.MISaleConclusionRpt_function_GetDelta(season, saleId).ToList();
                    data.Deltas = AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_GetDelta_Result>,List<DTO.MISaleConclusionRpt.Delta>>(delta);

                    //param value
                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));
                    data.Clients = converter.DB2DTO_Client(context.MISaleConclusionRpt_function_getClient(season, saleId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.MISaleConclusionRpt.ProformaCountry> GetPiByCountry(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MISaleConclusionRpt.ProformaCountry> data = new List<DTO.MISaleConclusionRpt.ProformaCountry>();

            saleId = saleId.GetValueOrDefault(-1);
            try
            {
                using (MISaleConclusionRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ProformasCountry(context.MISaleConclusionRpt_function_getProformaCountry(season, saleId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.MISaleConclusionRpt.ConfirmedProformaCountry> GetPiConfirmedByCountry(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MISaleConclusionRpt.ConfirmedProformaCountry> data = new List<DTO.MISaleConclusionRpt.ConfirmedProformaCountry>();

            saleId = saleId.GetValueOrDefault(-1);
            try
            {
                using (MISaleConclusionRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ConfirmedProformaCountry(context.MISaleConclusionRpt_function_getConfirmedProformaCountry(season, saleId).ToList());                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.MISaleConclusionRpt.ExpectedCountry> GetExpectedByCountry(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MISaleConclusionRpt.ExpectedCountry> data = new List<DTO.MISaleConclusionRpt.ExpectedCountry>();

            saleId = saleId.GetValueOrDefault(-1);
            try
            {
                using (MISaleConclusionRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ExpectedCountryList(context.MISaleConclusionRpt_function_getExpectedCountry(season, saleId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.MISaleConclusionRpt.CommercialInvoiceCountry> GetCiByCountry(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MISaleConclusionRpt.CommercialInvoiceCountry> data = new List<DTO.MISaleConclusionRpt.CommercialInvoiceCountry>();

            saleId = saleId.GetValueOrDefault(-1);
            try
            {
                using (MISaleConclusionRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_CommercialInvoiceCountry(context.MISaleConclusionRpt_function_getCommercialInvoiceCountry(season, saleId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.MISaleConclusionRpt.Delta> GetDeltaByClient(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MISaleConclusionRpt.Delta> data = new List<DTO.MISaleConclusionRpt.Delta>();

            saleId = saleId.GetValueOrDefault(-1);
            try
            {
                using (MISaleConclusionRptEntities context = CreateContext())
                {
                    var delta = context.MISaleConclusionRpt_function_GetDelta(season, saleId).ToList();
                    data = AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_GetDelta_Result>, List<DTO.MISaleConclusionRpt.Delta>>(delta);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.MISaleConclusionRpt.ExpectedByClient> GetExpectedByClient(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MISaleConclusionRpt.ExpectedByClient> data = new List<DTO.MISaleConclusionRpt.ExpectedByClient>();

            saleId = saleId.GetValueOrDefault(-1);
            try
            {
                using (MISaleConclusionRptEntities context = CreateContext())
                {
                    var dbData = context.MISaleConclusionRpt_function_getExpectedByClient(season, saleId).ToList();
                    data = AutoMapper.Mapper.Map<List<MISaleConclusionRpt_function_getExpectedByClient_Result>, List<DTO.MISaleConclusionRpt.ExpectedByClient>>(dbData);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.MISaleConclusionRpt.RangeExpected> GetRangeExpected(string season, int? saleId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.MISaleConclusionRpt.RangeExpected> data = new List<DTO.MISaleConclusionRpt.RangeExpected>();

            saleId = saleId.GetValueOrDefault(-1);
            try
            {
                using (MISaleConclusionRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_RangeExpectedList(context.MISaleConclusionRpt_function_getRangeExpected(season, saleId).ToList());
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
