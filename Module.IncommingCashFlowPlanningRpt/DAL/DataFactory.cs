using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.IncommingCashFlowPlanningRpt.DAL
{
    internal class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        public string GetExcelReportData(int userId, string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("IncommingCashFlowPlanningRpt_function_GetData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandTimeout = 180;
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.TableMappings.Add("Table", "IncommingCashFlowPlanningRpt_Planning_View");
                adap.TableMappings.Add("Table1", "IncommingCashFlowPlanningRpt_WeekInfo_View");
                adap.Fill(ds);

                ReportDataObject.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                hRow.Season = Season;
                hRow.ExRate = Convert.ToDecimal(supportFactory.GetSettingValue(Season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                ds.ReportHeader.AddReportHeaderRow(hRow);

                return Library.Helper.CreateReportFileWithEPPlus(ds, "IncommingCashFlowPlanning", new List<string>() { "IncommingCashFlowPlanningRpt_Planning_View", "IncommingCashFlowPlanningRpt_WeekInfo_View", "ReportHeader" });
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return string.Empty;
            }
        }
    }
}
