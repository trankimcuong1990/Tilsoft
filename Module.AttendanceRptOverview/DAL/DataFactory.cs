using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Module.AttendanceRptOverview.DAL
{
    internal class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        public string GetExcelReportData(string month, string year, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("AttendanceRpt_function_GetReportOverview", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Month", month);
                adap.SelectCommand.Parameters.AddWithValue("@Year", year);
                adap.Fill(ds.AttendanceRpt_function_GetReportOverview);

                SqlDataAdapter adap2 = new SqlDataAdapter();
                adap2.SelectCommand = new SqlCommand("SELECT * FROM AttendanceRpt_EmployeeDataReport_View", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap2.SelectCommand.CommandType = System.Data.CommandType.Text;
                adap2.Fill(ds.AttendanceRpt_EmployeeDataReport_View);

                // add report param
                ReportDataObject.ReportHeaderRow pRow = ds.ReportHeader.NewReportHeaderRow();
                pRow.Month = month;
                pRow.Year = year;
                ds.ReportHeader.AddReportHeaderRow(pRow);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "AttendanceRptOverview");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "AttendanceRptOverview");
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
            return data;
        }
    }
}
