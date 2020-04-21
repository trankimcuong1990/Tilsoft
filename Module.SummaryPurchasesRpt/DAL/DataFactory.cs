using Library.DTO;
using Module.SummaryPurchasesRpt.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Module.SummaryPurchasesRpt.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        private SummaryPurchasesRptEntities CreateContext()
        {
            return new SummaryPurchasesRptEntities(Library.Helper.CreateEntityConnectionString("DAL.SummaryPurchasesRptModel"));
        }

        public DTO.SearchForm GetDataWithFilters(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchForm data = new DTO.SearchForm();
            data.ReceivingNoteReportDatas = new List<DTO.ReceivingNoteReportData>();
            data.SupplierOfReceivingDatas = new List<SupplierOfReceivingData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string productionItemUD = "";
                int? factoryRawMaterialID = null;
                string factoryRawMaterialFullName = "";
                string startDate = "";
                string endDate = "";

                if (filters.ContainsKey("productionItemUD") && !string.IsNullOrEmpty(filters["productionItemUD"].ToString()))
                {
                    productionItemUD = filters["productionItemUD"].ToString().Replace("'", "''");
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
                if (filters.ContainsKey("factoryRawMaterialFullName") && !string.IsNullOrEmpty(filters["factoryRawMaterialFullName"].ToString()))
                {
                    factoryRawMaterialFullName = filters["factoryRawMaterialFullName"].ToString().Replace("'", "''");
                }
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    totalRows = context.SummaryPurchasesRpt_function_ReceivingNote(factoryRawMaterialID, productionItemUD, companyID, startDate, endDate, factoryRawMaterialFullName).Count();
                    var results = context.SummaryPurchasesRpt_function_ReceivingNote(factoryRawMaterialID, productionItemUD, companyID, startDate, endDate, factoryRawMaterialFullName);
                    data.ReceivingNoteReportDatas = converter.DB2DTO_GetReceivingNoteReportData(results.ToList());
                    data.SupplierOfReceivingDatas = converter.DB2DTO_GetSupplierOfReceivingNote(context.SummaryPurchasesRpt_function_SupplierOfReceivingNote(factoryRawMaterialID, productionItemUD, companyID, startDate, endDate, factoryRawMaterialFullName).ToList());
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

        public DTO.InitForm GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitForm data = new DTO.InitForm();
            data.SupportSubSuppliers = new List<DTO.SupportSubSupplierData>();

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    data.SupportSubSuppliers = converter.DB2DTO_GetSubSupplier(context.SummaryPurchasesRpt_SupportSubSupplier_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelReportData(int userId, int? factoryRawMaterialID, string productionItemUD, string startDate, string endDate, string factoryRawMaterialFullName, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
            int? companyID = fwFactory.GetCompanyID(userId);
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("SummaryPurchasesRpt_function_ReceivingNote", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryRawMaterialID", factoryRawMaterialID);
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItemUD", productionItemUD);
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                adap.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
                adap.SelectCommand.Parameters.AddWithValue("@EndDate", endDate);
                adap.SelectCommand.Parameters.AddWithValue("@DeliverName", factoryRawMaterialFullName);
                adap.TableMappings.Add("Table", "ReceivingNoteReportDataView");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "SummaryPurchasesRpt");
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
    }
}
