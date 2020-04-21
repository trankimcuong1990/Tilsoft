using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FSCRpt.DAL
{
    internal class DataFactory
    {
        public DTO.InitForm GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitForm data = new DTO.InitForm();
            data.Seasons = new List<Support.DTO.Season>();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.Seasons = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public string ExportFSCReport(string startDate, string endDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            ReportObjectData ds = new ReportObjectData();
            string fileName = string.Empty;

            DateTime? valStartDate = startDate.ConvertStringToDateTime();
            DateTime? valEndDate = endDate.ConvertStringToDateTime();

            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("FSCRpt_function_FSCOverviewExcel", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@StartDate", valStartDate.Value.Date);
                adap.SelectCommand.Parameters.AddWithValue("@EndDate", valEndDate.Value.Date);
                adap.TableMappings.Add("Table", "FSCRpt_FSCOverview_View FSCOverview");
                adap.Fill(ds);

                ReportObjectData.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                hRow.StartDate = startDate;
                hRow.EndDate = endDate;
                ds.ReportHeader.AddReportHeaderRow(hRow);

                ds.AcceptChanges();

                fileName = Library.Helper.CreateReportFileWithEPPlus(ds, "FSCReport");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return fileName;
        }
    }
}
