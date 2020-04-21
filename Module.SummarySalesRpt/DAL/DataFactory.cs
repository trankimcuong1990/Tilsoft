using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.SummarySalesRpt.DTO;

namespace Module.SummarySalesRpt.DAL
{
    public class DataFactory : Library.Base.DataFactory<SearchForm, EditForm>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        private SummarySalesRptEntities CreateContext()
        {
            return new SummarySalesRptEntities(Library.Helper.CreateEntityConnectionString("DAL.SummarySalesRptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditForm GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.InitForm GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitForm data = new DTO.InitForm();
            data.SupportCustomer = new List<DTO.SupportCustomerData>();
            data.DeliveryNotes = new List<DeliveryNote>();

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    data.SupportCustomer = converter.DB2DTO_GetSubSupplier(context.SummarySalesRpt_SupportCustomer_view.ToList());
                    data.DeliveryNotes = converter.DB2DTO_GetDeliveryNotes(context.SummarySalesRpt_DeliveryNote_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.SearchForm GetDataWithFilters(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchForm data = new DTO.SearchForm();
            data.CustomerDatas = new List<DTO.CustomerData>();
            data.ProductionItemDatas = new List<DTO.ProductionItemData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string salesOrderNo = "";
                int? factoryRawMaterialID = null;
                string startDate = "";
                string endDate = "";

                if (filters.ContainsKey("salesOrderNo") && !string.IsNullOrEmpty(filters["salesOrderNo"].ToString()))
                {
                    salesOrderNo = filters["salesOrderNo"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null && !string.IsNullOrEmpty(filters["factoryRawMaterialID"].ToString()))
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }

                if (filters.ContainsKey("startDate") && !string.IsNullOrEmpty(filters["startDate"].ToString()))
                {
                    startDate = filters["startDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("endDate") && !string.IsNullOrEmpty(filters["endDate"].ToString()))
                {
                    endDate = filters["endDate"].ToString().Replace("'", "''");
                }
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    var results = context.SummarySalesRpt_function_CustomerReport(factoryRawMaterialID, salesOrderNo, companyID, startDate, endDate);
                    data.CustomerDatas = converter.DB2DTO_GetCustomerDatas(results.ToList());
                    data.ProductionItemDatas = converter.DB2DTO_GetProductionItemDatas(context.SummarySalesRpt_function_ProductionItemReport(factoryRawMaterialID, salesOrderNo, companyID, startDate, endDate).ToList());
                }
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return data;
            }
        }

        public string GetExcelReportData(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
            int? companyID = fwFactory.GetCompanyID(userId);
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("SummarySalesRpt_function_getExcelData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryRawMaterialID", filters["factoryRawMaterialID"]);
                adap.SelectCommand.Parameters.AddWithValue("@SalesOrderNo", filters["salesOrderNo"]);
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                adap.SelectCommand.Parameters.AddWithValue("@StartDate", filters["startDate"]);
                adap.SelectCommand.Parameters.AddWithValue("@EndDate", filters["endDate"]);
                adap.TableMappings.Add("Table", "CustomerReportDataView");
                adap.TableMappings.Add("Table1", "ProductionItemReportDataView");
                adap.TableMappings.Add("Table2", "DeliveryNoteView");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "SummarySalesRpt");
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
