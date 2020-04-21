using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentOverviewRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        public string GetExcelReportData(int clientId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("ClientPaymentOverviewRpt_function_GetReport", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", clientId);
                adap.TableMappings.Add("Table", "ClientPaymentOverviewRpt_DownPayment_View");
                adap.TableMappings.Add("Table1", "ClientPaymentOverviewRpt_Invoice_View");
                adap.Fill(ds);

                // add report param
                ReportDataObject.ReportHeaderRow pRow = ds.ReportHeader.NewReportHeaderRow();
                pRow.Season = string.Empty;
                pRow.ClientNM = supportFactory.GetClient().FirstOrDefault(o => o.ClientID == clientId).ClientNM;
                pRow.ExRate = Convert.ToDecimal(supportFactory.GetSettingValue(Library.OMSHelper.Helper.GetCurrentSeason(), "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                ds.ReportHeader.AddReportHeaderRow(pRow);

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "ClientPaymentOverview");

                // generate xml file
                //return Library.Helper.CreateCOMReportFile(ds, "ClientPaymentOverview");
                return Library.Helper.CreateReportFiles(ds, "ClientPaymentOverview");
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
            data.Clients = new List<Support.DTO.Client>();

            //try to get data
            try
            {
                data.Clients = supportFactory.GetClient().ToList();
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
