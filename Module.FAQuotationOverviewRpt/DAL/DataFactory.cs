using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FAQuotationOverviewRpt.DAL
{
    internal class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        public string GetReportData(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FAQuotationOverviewRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.TableMappings.Add("Table", "GetReportData");
                adap.Fill(ds);

                ReportDataObject.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                hRow.Season = Season;
                ds.ReportHeader.AddReportHeaderRow(hRow);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "QuotationOverview");
                //return string.Empty;

                // generate xml file
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "QuotationOverview");
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
            notification = new Library.DTO.Notification(){ Type = Library.DTO.NotificationType.Success };
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
