using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockMutationReport.DAL
{
    internal class DataFactory
    {
        public string GetReportData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                string season = null;
                if (filters.ContainsKey("season") && filters["season"] != null) season = filters["season"].ToString();
                
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ReportMng_function_GetStockMutation", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.ReportMng_StockMutation_View);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "StockMutation");
                //return string.Empty;

                // generate xml file
                return Library.Helper.CreateReportFiles(ds, "StockMutation");
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
