using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseOverviewRpt.DAL
{
    internal class ReportFactory
    {
        public string ExportWarehouseOverview(int iRequesterID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            string fileName = "";

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            WarehouseOverviewData ds = new WarehouseOverviewData();

            Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
            int? companyID = fwFactory.GetCompanyID(iRequesterID);

            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("WarehouseOverviewRpt_function_ExportExcel", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                adap.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
                adap.SelectCommand.Parameters.AddWithValue("@EndDate", endDate);

                adap.TableMappings.Add("Table", "COMMONDATA");
                adap.TableMappings.Add("Table1", "FACTORYWAREHOUSEDATA");
                adap.TableMappings.Add("Table2", "INVENTORYDETAILDATA");
                adap.Fill(ds);

                ds.AcceptChanges();

                fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "WarehouseOverview");
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
