using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Module.AVFVATRpt.DAL
{
    internal class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        public string GetExcelReportData(string From, string To, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            DateTime fromDt = DateTime.ParseExact(From, "d/M/yyyy", CultureInfo.InvariantCulture);
            string fromDate = fromDt.ToString("MM/dd/yyyy");

            DateTime toDt = DateTime.ParseExact(To, "d/M/yyyy", CultureInfo.InvariantCulture);
            string toDate = toDt.ToString("MM/dd/yyyy");

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("AVFVATRpt_function_getExcelOverview", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@From", fromDate);
                adap.SelectCommand.Parameters.AddWithValue("@To", toDate);
                adap.Fill(ds);

                // add report param
                ReportDataObject.ReportHeaderRow pRow = ds.ReportHeader.NewReportHeaderRow();
                pRow.From = From;
                pRow.To = To;
                ds.ReportHeader.AddReportHeaderRow(pRow);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "AVFVATRptOverview");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "AVFVATRptOverview");
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
