using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Module.TransportRpt.DAL
{
    public class DataFactory
    {
        public string GetReportData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message="Get transport overview report success !!!"};
            ReportDataObject ds = new ReportDataObject();
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("TransportRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.TableMappings.Add("Table", "Container");
                adap.TableMappings.Add("Table1", "TransportInvoice");
                adap.TableMappings.Add("Table2", "ClientTransportInvoice");
                adap.TableMappings.Add("Table3", "TransportCostItem");
                adap.TableMappings.Add("Table4", "ClientCostType");

                adap.TableMappings.Add("Table5", "ContainerSubInfo");
                adap.TableMappings.Add("Table6", "TransportInvoiceSubInfo");
                adap.TableMappings.Add("Table7", "ClientTransportInvoiceSubInfo");
                adap.Fill(ds);
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "TransportOverview");
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
