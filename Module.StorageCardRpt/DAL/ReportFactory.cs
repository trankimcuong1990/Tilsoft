using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StorageCardRpt.DAL
{
    internal class ReportFactory
    {
        public DTO.InitForm GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitForm initForm = new DTO.InitForm();
            initForm.FactoryWarehouses = new List<Support.DTO.FactoryWarehouseDto>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(iRequesterID);

                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                initForm.FactoryWarehouses = supportFactory.GetFactoryWarehouse(companyID);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message.Trim()))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return initForm;
        }

        public string ExportStorageCard(int iRequesterID, int productionItemID, int factoryWarehouseID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            string fileName = "";

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            StorageCardData ds = new StorageCardData();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(iRequesterID);

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("StorageCardRpt_function_ExportExcel", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@productionItemID", productionItemID);
                adap.SelectCommand.Parameters.AddWithValue("@factoryWarehouseID", factoryWarehouseID);
                adap.SelectCommand.Parameters.AddWithValue("@startDate", startDate);
                adap.SelectCommand.Parameters.AddWithValue("@endDate", endDate);
                adap.SelectCommand.Parameters.AddWithValue("@companyID", companyID);

                adap.TableMappings.Add("Table", "Storage_Common");
                adap.TableMappings.Add("Table1", "Storage_Detail");
                adap.Fill(ds);

                ds.AcceptChanges();

                fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "StorageCardRpt");
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
