using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.GrossMarginSummaryRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private GrossMarginSummaryRptEntities CreateContext()
        {
            return new GrossMarginSummaryRptEntities(DALBase.Helper.CreateEntityConnectionString("GrossMarginSummaryRptModel"));
        }

        private bool ValidatePeriod(string fromDate, string toDate, out string season, out DateTime begin, out DateTime end)
        {
            season = string.Empty;
            begin = DateTime.Now;
            end = DateTime.Now;

            DateTime tmpDate;
            System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
            if (DateTime.TryParse(fromDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            {
                begin = tmpDate;
            }
            else
            {
                return false;
            }
            if (DateTime.TryParse(toDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
            {
                end = tmpDate;
            }
            else
            {
                return false;
            }
            if (DateTime.Compare(begin, end) > 0)
            {
                return false;
            }
            season = Library.Helper.GetSeason(begin);

            return true;
        }

        public DTO.GrossMarginSummaryRpt.ReportData GetHtmlReportData(string fromDate, string toDate, out Library.DTO.Notification notification)
        {
            DateTime begin, end;
            string season;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.GrossMarginSummaryRpt.ReportData data = new DTO.GrossMarginSummaryRpt.ReportData();
            data.HtmlGrossMarginSummaries = new List<DTO.GrossMarginSummaryRpt.HtmlGrossMarginSummary>();

            //try to get data
            if (ValidatePeriod(fromDate, toDate, out season, out begin, out end))
            {
                try
                {
                    using (GrossMarginSummaryRptEntities context = CreateContext())
                    {
                        //decimal exrate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                        data.HtmlGrossMarginSummaries = converter.DB2DTO_HtmlGrossMarginSummaryList(context.GrossMarginSummaryRpt_function_getGrossMarginHtml(begin, end).ToList());
                        //foreach (DTO.GrossMarginSummaryRpt.HtmlGrossMarginSummary item in data.HtmlGrossMarginSummaries)
                        //{
                        //    item.ExchangeRate = exrate;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = ex.Message;
                }
            }
            else
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Report date range is invalid!";
            }
            return data;
        }

        public string GetExcelReportData(string fromDate, string toDate, out Library.DTO.Notification notification)
        {
            DateTime begin, end;
            string season;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            if (ValidatePeriod(fromDate, toDate, out season, out begin, out end))
            {
                try
                {
                    SqlDataAdapter adap = new SqlDataAdapter();
                    adap.SelectCommand = new SqlCommand("GrossMarginSummaryRpt_function_getGrossMarginExcel", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                    adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adap.SelectCommand.Parameters.AddWithValue("@Begin", begin);
                    adap.SelectCommand.Parameters.AddWithValue("@End", end);
                    adap.Fill(ds.GrossMarginSummaryRpt_GrossMarginSummaryExcel_View);

                    // add report param
                    ReportDataObject.ReportHeaderRow pRow = ds.ReportHeader.NewReportHeaderRow();
                    pRow.Season = season;
                    pRow.ExRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    ds.ReportHeader.AddReportHeaderRow(pRow);

                    // dev
                    //DALBase.Helper.DevCreateReportXMLSource(ds, "GrossMarginSummary");

                    // generate xml file
                    //return DALBase.Helper.CreateReportFiles(ds, "GrossMarginSummary");
                    return Library.Helper.CreateReportFileWithEPPlus(ds, "GrossMarginSummary", new List<string> { "GrossMarginSummaryRpt_GrossMarginSummaryExcel_View", "ReportHeader" });
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
            else
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Report date range is invalid!";
                return string.Empty;
            }
        }

        public string GetExcelForecastReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObjectForecast ds = new ReportDataObjectForecast();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("GrossMarginSummaryRpt_function_getGrossMarginForecastExcel", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.GrossMarginSummaryRpt_function_getGrossMarginForecastExcel);

                string result = Library.Helper.CreateReportFileWithEPPlus(ds, "GrossMarginForecast", new List<string> { "GrossMarginSummaryRpt_function_getGrossMarginForecastExcel" });
                if (string.IsNullOrEmpty(result))
                {
                    throw new Exception("Error when creating excel file for download!");
                }
                return result;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return string.Empty;
            }
        }
    }
}
