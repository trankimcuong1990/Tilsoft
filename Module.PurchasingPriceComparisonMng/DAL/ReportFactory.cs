using System;
using System.Data.SqlClient;

namespace Module.PurchasingPriceComparisonMng.DAL
{
    internal class ReportFactory
    {
        public object GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchasingPriceComparisonMng_function_GetPurchasingPriceComparison", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.TableMappings.Add("Table", "PurchasingPriceComparisonView");
                adap.TableMappings.Add("Table1", "ReportHeader");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "PurchasingPriceComparison");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return "";
            }
        }
    }
}
