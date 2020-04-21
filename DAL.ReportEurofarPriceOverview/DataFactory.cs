using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ReportEurofarPriceOverview
{
    public class DataFactory
    {
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        public string GetReportData(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("EurofarPriceOverviewRpt_function_GetReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.Fill(ds.EurofarPriceOverviewRpt_function_GetReportData);

                SqlDataAdapter adap2 = new SqlDataAdapter();
                adap2.SelectCommand = new SqlCommand("EurofarPriceOverviewRpt_function_GetReportHeaderData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap2.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap2.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap2.Fill(ds.ReportHeader);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "EurofarPriceOverview");
                //return string.Empty;

                // generate xml file
                //return Library.Helper.CreateCOMReportFile(ds, "EurofarPriceOverview");
                return Library.Helper.CreateReportFileWithEPPlus(ds, "EurofarPriceOverview");
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

        public DTO.EurofarPriceOverViewRpt.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EurofarPriceOverViewRpt.InitFormData data = new DTO.EurofarPriceOverViewRpt.InitFormData();
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
