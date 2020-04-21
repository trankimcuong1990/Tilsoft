using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DAL.ReportProformaInvoice
{
    public class DataFactory
    {
        public string GetReportProformaInvoice(string Season, int UserID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Report_function_GetProformaInvoice", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandTimeout = 500;
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);

                adap.TableMappings.Add("Table", "ParamTable");
                adap.TableMappings.Add("Table1", "Report_ProformaInvoice_View");
                adap.TableMappings.Add("Table2", "Report_ProformaInvoice_ByClient_View");                
                adap.TableMappings.Add("Table3", "SaleOrder");
                adap.Fill(ds);

                foreach (var item in ds.SaleOrder)
                {
                    int i = 1;
                    foreach (ReportDataObject.Report_ProformaInvoice_ViewRow rItem in ds.Report_ProformaInvoice_View.Where(o =>!o.IsProformaInvoiceNoNull() && o.ProformaInvoiceNo == item.ProformaInvoiceNo && !o.IsPaymentAmountEURNull() && o.PaymentAmountEUR!=0))
                    {
                        if (i > 1)
                        {
                            rItem["PaymentAmountInUSD"] = DBNull.Value;
                            rItem["PaymentAmountInEUR"] = DBNull.Value;
                            rItem["PaymentAmountEUR"] = DBNull.Value;
                        }
                        i++;
                    }
                }
                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "ProformaInvoiceOverview");
                //return string.Empty;
                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "ProformaInvoiceOverview");
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return string.Empty;
            }
        }
    }
}
