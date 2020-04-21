using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShipmentToBeInvoicedRpt.DAL
{
    internal class DataFactory
    {
        public List<Support.DTO.Season> GetSupportSeasons(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                return supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message.Trim()))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }

                return new List<Support.DTO.Season>();
            }
        }

        public string ExportExcel(string season, out Library.DTO.Notification notification)
        {
            string fileName = "";

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            ReportObjectData ds = new ReportObjectData();

            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("ShipmentToBeInvoiceRpt_function_ExportExcel", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);

                adap.TableMappings.Add("Table", "ShipmentToBeInvoice_Common");
                adap.TableMappings.Add("Table1", "ShipmentToBeInvoice_Detail");
                adap.Fill(ds);

                ds.AcceptChanges();

                fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "ShipmentToBeInvoice");
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
