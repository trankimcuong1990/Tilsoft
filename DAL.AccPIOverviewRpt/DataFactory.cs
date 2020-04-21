using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AccPIOverviewRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private AccPIOverviewRptEntities CreateContext()
        {
            return new AccPIOverviewRptEntities(DALBase.Helper.CreateEntityConnectionString("AccPIOverviewRptModel"));
        }

        public DTO.AccPIOverviewRpt.ReportData GetHtmlReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.AccPIOverviewRpt.ReportData data = new DTO.AccPIOverviewRpt.ReportData();
            data.HtmlOverviews = new List<DTO.AccPIOverviewRpt.HtmlOverview>();

            //try to get data
            try
            {
                using (AccPIOverviewRptEntities context = CreateContext())
                {
                    data.HtmlOverviews = converter.DB2DTO_HtmlOverviewList(context.AccPIOverviewRpt_HtmlOverview_View.Where(o=>o.Season == season).ToList());
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
                adap.SelectCommand = new SqlCommand("AccPIOverviewRpt_function_getExcelOverview", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.Fill(ds.AccPIOverviewRpt_ExcelOverview_View);

                // add report param
                ReportDataObject.ReportHeaderRow pRow = ds.ReportHeader.NewReportHeaderRow();
                pRow.Season = Season;
                pRow.ExRate = Convert.ToDecimal(supportFactory.GetSettingValue(Season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                ds.ReportHeader.AddReportHeaderRow(pRow);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "AccPIOverview");
                //return string.Empty;

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "AccPIOverview");
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

        public DTO.AccPIOverviewRpt.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.AccPIOverviewRpt.InitFormData data = new DTO.AccPIOverviewRpt.InitFormData();
            data.Seasons = new List<DTO.Support.Season>();

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
