using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFSeparateBalanceSheetRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();
        public string GetExcelReportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("AVFSeparateBalanceSheetRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.Fill(ds.AVFSeparateBalanceSheetRpt_function_GetReportData);

                // dev
                Library.Helper.DevCreateReportXMLSource(ds, "AVFSeparateBalanceSheet");

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "AVFSeparateBalanceSheet");
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
    }
}
