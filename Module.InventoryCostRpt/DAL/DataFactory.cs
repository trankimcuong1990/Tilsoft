namespace Module.InventoryCostRpt.DAL
{
    using AutoMapper;
    using Library;
    using Library.DTO;
    using Module.InventoryCostRpt.DTO;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class DataFactory
    {
        readonly DataConverter dataConverter = new DataConverter();

        private InventoryCostRptEntities CreateContext()
        {
            return new InventoryCostRptEntities(Library.Helper.CreateEntityConnectionString("DAL.InventoryCostRptModel"));
        }

        public InitFormData GetInitData(int userID, out Notification notification)
        {
            InitFormData data = new InitFormData();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    // Get data factory warehouse
                    var employee = context.SupportMng_BaseEmployee_View.FirstOrDefault(o => o.UserID == userID);
                    if (employee != null)
                    {
                        data.FactoryWarehouses = Mapper.Map<List<InventoryCostRpt_FactoryWarehouse_View>, List<FactoryWarehouseData>>(context.InventoryCostRpt_FactoryWarehouse_View.Where(o => o.CompanyID == employee.CompanyID).ToList());
                        if (employee.BranchID.HasValue)
                        {
                            data.FactoryWarehouses = data.FactoryWarehouses.Where(o => o.BranchID == employee.BranchID).ToList();
                        }
                    }

                    // Get data production type
                    data.ProductionItemTypes = Mapper.Map<List<InventoryCostRpt_ProductionItemType_View>, List<ProductionItemTypeData>>(context.InventoryCostRpt_ProductionItemType_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public SearchFormData GetDataWithFilters(int userID, int? factoryWarsehouseID, int? productionItemTypeID, string validDate, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            SearchFormData data = new SearchFormData();

            try
            {
                Framework.DAL.DataFactory frameFactory = new Framework.DAL.DataFactory();
                int? companyID = frameFactory.GetCompanyID(userID);

                DateTime? currentDate = validDate.ConvertStringToDateTime();
                if (!currentDate.HasValue)
                {
                    throw new Exception("Please input valid date!");
                }

                string startDate = currentDate.Value.ToString("dd/MM/yyyy");
                string endDate = DateTime.DaysInMonth(currentDate.Value.Year, currentDate.Value.Month).ToString("dd/MM/yyyy");

                int? currentMonth = currentDate.Value.Month;
                int? currentYear = currentDate.Value.Year;

                using (var context = CreateContext())
                {
                    data.Data = dataConverter.DB2DTO_SearchResults(context.InventoryCostRpt_function_InventoryCostSearchResult(factoryWarsehouseID, startDate, null, null, productionItemTypeID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public string GetExcelReportData(int userID, int? factoryWarsehouseID, int? productionItemTypeID, string validDate, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            ReportDataObject ds = new ReportDataObject();

            Framework.DAL.DataFactory frameFactory = new Framework.DAL.DataFactory();
            int? companyID = frameFactory.GetCompanyID(userID);

            DateTime? currentDate = validDate.ConvertStringToDateTime();
            if (!currentDate.HasValue)
            {
                throw new Exception("Please input valid date!");
            }

            string startDate = currentDate.Value.ToString("dd/MM/yyyy");
            string endDate = currentDate.Value.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");

            int? currentMonth = currentDate.Value.Month;
            int? currentYear = currentDate.Value.Year;

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("InventoryCostRpt_function_InventoryCostSearchResult", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryWarehouseID", factoryWarsehouseID);
                adap.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
                adap.SelectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value);
                adap.SelectCommand.Parameters.AddWithValue("@BranchID", DBNull.Value);
                adap.SelectCommand.Parameters.AddWithValue("@ProductionItemTypeID", productionItemTypeID);
                adap.TableMappings.Add("Table", "InventoryCostReportDataView");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "InventoryCostRpt");
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
