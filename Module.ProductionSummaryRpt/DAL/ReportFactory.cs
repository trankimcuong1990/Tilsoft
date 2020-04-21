using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionSummaryRpt.DAL
{
    internal class ReportFactory
    {
        public object ExportProductionSummary(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            ProductionSummaryObject ds = new ProductionSummaryObject();

            try
            {
                string season = (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString())) ? filters["Season"].ToString() : null;
                int? factoryID = (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString())) ? (int?) Convert.ToInt32(filters["FactoryID"].ToString()) : null;
                int? clientID = (filters.ContainsKey("ClientID") && filters["ClientID"] != null && !string.IsNullOrEmpty(filters["ClientID"].ToString())) ? (int?)Convert.ToInt32(filters["ClientID"].ToString()) : null;
                string proformaInvoiceNo = (filters.ContainsKey("ProformaInvoice") && filters["ProformaInvoice"] != null && !string.IsNullOrEmpty(filters["ProformaInvoice"].ToString())) ? filters["ProformaInvoice"].ToString() : null;

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ProductionSummaryRpt_function_ExportDataProductionSummary", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.TableMappings.Add("Table", "ProductionSummary");
                adap.TableMappings.Add("Table1", "ProductionSummaryDetail");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ProductionSummary");
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
