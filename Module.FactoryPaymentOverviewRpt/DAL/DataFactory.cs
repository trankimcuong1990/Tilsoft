using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPaymentOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public string GetExcelReportData(int userId, int supplierId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                // check permission
                if (fwFactory.CheckSupplierPermission(userId, supplierId) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected supplier data");
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryPaymentOverviewRpt_function_getDetailData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@SupplierID", supplierId);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.FactoryPaymentOverviewRpt_function_getDetailData);

                adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryPaymentOverviewRpt_function_getInvoiceData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@SupplierID", supplierId);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.FactoryPaymentOverviewRpt_function_getInvoiceData);

                adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryPaymentOverviewRpt_function_getTotalData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@SupplierID", supplierId);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.FactoryPaymentOverviewRpt_function_getTotalData);

                adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryPaymentOverviewRpt_function_getTotalExtraData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@SupplierID", supplierId);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.Fill(ds.FactoryPaymentOverviewRpt_function_getTotalExtraData);

                ReportDataObject.ReportHeaderRow row = ds.ReportHeader.NewReportHeaderRow();
                row.Season = season;
                Support.DTO.Supplier dtoSupplier = supportFactory.GetSupplier(userId).FirstOrDefault(o => o.SupplierID == supplierId);
                if (dtoSupplier != null)
                {
                    row.SupplierUD = dtoSupplier.SupplierUD;
                    row.SupplierNM = dtoSupplier.SupplierNM;
                }                
                ds.ReportHeader.AddReportHeaderRow(row);
                ds.AcceptChanges();

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "FactoryPaymentOverview");
                //return string.Empty;

                // generate xml file
                return Library.Helper.CreateCOMReportFile(ds, "FactoryPaymentOverview");
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

        public DTO.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Seasons = new List<Support.DTO.Season>();
            data.Suppliers = new List<Support.DTO.Supplier>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.Suppliers = supportFactory.GetSupplier(userId).ToList();
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
