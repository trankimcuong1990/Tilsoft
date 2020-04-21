using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DAL.ReportClientMng
{
    public class DataFactory
    {
        public string GetReportClients( int UserID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObjects ds = new ReportDataObjects();

           // int demoPLCID = 4;

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportClientMng_function_ReportClient", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.TableMappings.Add("Table", "ReportClient_View"); 
       

                adap.Fill(ds);

                return DALBase.Helper.CreateReportFiles(ds, "ReportClient");
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
