using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();

        private PurchaseOrderTrackingRptEntities CreateContext()
        {
            return new PurchaseOrderTrackingRptEntities(Library.Helper.CreateEntityConnectionString("DAL.PurchaseOrderTrackingRptModel"));
        }

        public object GetInitData(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitFormData data = new DTO.InitFormData();

            try
            {
                using (var context = CreateContext())
                {
                    data.SupportItemGroup = converter.DB2DTO_SupportItemGroup(context.PurchaseOrderTrackingRpt_SupportProductionItemGroup_View.ToList());
                    data.SupportSupplier = converter.DB2DTO_SupportSupplier(context.PurchaseOrderTrackingRpt_SupportSupplier_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetClientWithFilter(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.SupportClientData> data = new List<DTO.SupportClientData>();

            try
            {
                string searchString = filters.ContainsKey("searchString") && filters["searchString"] != null && !string.IsNullOrEmpty(filters["searchString"].ToString().Trim()) ? filters["searchString"].ToString().Trim() : null;

                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseOrderTrackingRpt_function_SupportClient(searchString);
                    data = converter.DB2DTO_SupportClient(dbItem.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetWorkOrderWithFilter(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.SupportWorkOrderData> data = new List<DTO.SupportWorkOrderData>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                string searchString = filters.ContainsKey("searchString") && filters["searchString"] != null && !string.IsNullOrEmpty(filters["searchString"].ToString().Trim()) ? filters["searchString"].ToString().Trim() : null;
                int? clientID = filters.ContainsKey("clientID") && filters["clientID"] != null && !string.IsNullOrEmpty(filters["clientID"].ToString().Trim()) ? (int?)Convert.ToInt32(filters["clientID"].ToString().Trim()) : null;

                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseOrderTrackingRpt_function_SupportWorkOrder(searchString, clientID, companyID);
                    data = converter.DB2DTO_SupportWorkOrder(dbItem.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetDataWithFilter(int userID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            totalRows = 0;

            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchFormData data = new DTO.SearchFormData();

            try
            {
                string fromDate = filters.ContainsKey("fromDate") && filters["fromDate"] != null && !string.IsNullOrEmpty(filters["fromDate"].ToString().Trim()) ? filters["fromDate"].ToString().Trim() : null;
                string toDate = filters.ContainsKey("toDate") && filters["toDate"] != null && !string.IsNullOrEmpty(filters["toDate"].ToString().Trim()) ? filters["toDate"].ToString().Trim() : null;
                int? supplierID = filters.ContainsKey("supplierID") && filters["supplierID"] != null && !string.IsNullOrEmpty(filters["supplierID"].ToString().Trim()) ? (int?)Convert.ToInt32(filters["supplierID"].ToString().Trim()) : null;
                string status = filters.ContainsKey("status") && filters["status"] != null && !string.IsNullOrEmpty(filters["status"].ToString().Trim()) ? filters["status"].ToString().Trim() : null;

                using (var context = CreateContext())
                {
                    totalRows = context.PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingSearchResult(fromDate, toDate, supplierID, status, orderBy, orderDirection).Count();

                    data.PurchaseOrderTracking = converter.DB2DTO_PurchaseOrderTracking(context.PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingSearchResult(fromDate, toDate, supplierID, status, orderBy, orderDirection).ToList());
                    data.PurchaseOrderTrackingWeek = converter.DB2DTO_WeekInfo(context.PurchaseOrderTrackingRpt_function_WeekInfo(fromDate, toDate).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public string GetExcelReportData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                string fromDate = filters.ContainsKey("fromDate") && filters["fromDate"] != null && !string.IsNullOrEmpty(filters["fromDate"].ToString().Trim()) ? filters["fromDate"].ToString().Trim() : null;
                string toDate = filters.ContainsKey("toDate") && filters["toDate"] != null && !string.IsNullOrEmpty(filters["toDate"].ToString().Trim()) ? filters["toDate"].ToString().Trim() : null;
                int? supplierID = filters.ContainsKey("supplierID") && filters["supplierID"] != null && !string.IsNullOrEmpty(filters["supplierID"].ToString().Trim()) ? (int?)Convert.ToInt32(filters["supplierID"].ToString().Trim()) : null;
                string status = filters.ContainsKey("status") && filters["status"] != null && !string.IsNullOrEmpty(filters["status"].ToString().Trim()) ? filters["status"].ToString().Trim() : null;

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingSearchResult", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@fromDate", fromDate);
                adap.SelectCommand.Parameters.AddWithValue("@toDate", toDate);
                adap.SelectCommand.Parameters.AddWithValue("@supplierID", supplierID);
                adap.SelectCommand.Parameters.AddWithValue("@status", status);
                adap.TableMappings.Add("Table", "POTracking");
                adap.Fill(ds.PurchaseOrderTracking);

                SqlDataAdapter adap2 = new SqlDataAdapter();
                adap2.SelectCommand = new SqlCommand("PurchaseOrderTrackingRpt_function_PurchaseOrderTrackingDetail", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap2.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap2.SelectCommand.Parameters.AddWithValue("@fromDate", fromDate);
                adap2.SelectCommand.Parameters.AddWithValue("@toDate", toDate);
                adap2.SelectCommand.Parameters.AddWithValue("@supplierID", supplierID);
                adap2.SelectCommand.Parameters.AddWithValue("@status", status);
                adap.TableMappings.Add("Table1", "POTrackingDetail");
                adap2.Fill(ds.PurchaseOrderTrackingDetail);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "PurchaseOrderTrackingRpt");
            }
            catch(Exception ex)
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
