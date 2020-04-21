using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CommercialInvoiceOverviewRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        private CommercialInvoiceOverviewRptEntities CreateContext()
        {
            return new CommercialInvoiceOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.CommercialInvoiceOverviewRptModel"));
        }

        public DTO.ReportData GetHtmlReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ReportData data = new DTO.ReportData();
            data.CommercialInvoices = new List<DTO.CommercialInvoice>();
            data.ExchangeRate = 0;

            //try to get data
            try
            {
                using (CommercialInvoiceOverviewRptEntities context = CreateContext())
                {
                    data.CommercialInvoices = converter.DB2DTO_CommercialInvoiceList(context.CommercialInvoiceOverviewRpt_CommercialInvoice_View.Where(o => o.Season == season).ToList());
                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelReportData(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("CommercialInvoiceOverviewRpt_function_getCommercialInvoice", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.Fill(ds.CommercialInvoiceOverviewRpt_CommercialInvoiceExcel_View);

                // add report param
                ReportDataObject.ReportHeaderRow pRow = ds.ReportHeader.NewReportHeaderRow();
                pRow.Season = Season;
                pRow.ExRate = Convert.ToDecimal(supportFactory.GetSettingValue(Season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                ds.ReportHeader.AddReportHeaderRow(pRow);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "CommercialInvoiceOverview");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "CommercialInvoiceOverview");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }

        public DTO.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Seasons = new List<Support.DTO.Season>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
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
