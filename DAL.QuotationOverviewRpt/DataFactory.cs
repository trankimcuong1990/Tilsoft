using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.QuotationOverviewRpt
{
    public class DataFactory
    {
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        public string GetReportData(int userId, string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();                
                adap.SelectCommand = new SqlCommand("QuotationOverviewRpt_function_GetReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandTimeout = 500;
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                adap.Fill(ds.QuotationOverviewRpt_function_GetReportData);

                ReportDataObject.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                hRow.Season = Season;
                ds.ReportHeader.AddReportHeaderRow(hRow);

                // generate xml file
                //return Library.Helper.CreateReportFileWithEPPlus(ds, "QuotationOverview");
                return Library.Helper.CreateReportFileWithEPPlus(ds, "QuotationOverview", new List<string>() { "QuotationOverviewRpt_function_GetReportData", "ReportHeader" });
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

        public DTO.QuotationOverviewRpt.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.QuotationOverviewRpt.InitFormData data = new DTO.QuotationOverviewRpt.InitFormData();
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
