using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Module.InventoryRpt.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();

        public DTO.InitFormData GetInitData(int userID, int? branchID, out Notification notification)
        {
            DTO.InitFormData initData = new DTO.InitFormData();
            initData.FactoryWarehouse = new List<DTO.FactoryWarehouseDTO>();
            initData.ProductionTeam = new List<Support.DTO.ProductionTeam>();

            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                // Declare data factory of support to get data.
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

                // Get company of end-user.
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);

                //initData.FactoryWarehouse = supportFactory.GetFactoryWarehouse(companyID);
                initData.ProductionTeam = supportFactory.GetProductionTeam(companyID);

                using (InventoryRptEntities context = CreateContext())
                {
                    initData.FactoryWarehouse = AutoMapper.Mapper.Map<List<InventoryRpt_FactoryWarehouse_View>, List<DTO.FactoryWarehouseDTO>>(context.InventoryRpt_FactoryWarehouse_View.Where(o => o.CompanyID == companyID).ToList());

                    if (branchID.HasValue) // Has branch with end-user
                    {
                        initData.FactoryWarehouse = initData.FactoryWarehouse.Where(o => o.BranchID == branchID).ToList();
                    }
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

            return initData;
        }

        public string ExportInventoryReport(int userID, int factoryWarehouseID, int? productionTeamID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            string fileName = "";

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyID = frameworkFactory.GetCompanyID(userID);

                int? factoryWID = (factoryWarehouseID <= 0) ? (int?)null : factoryWarehouseID;
                int? productionTID = (productionTeamID <= 0) ? (int?)null : productionTeamID;

                if (!factoryWID.HasValue && !productionTID.HasValue)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Please select one Factory Warehouse or one Production Team to search";

                    return fileName;
                }

                InventoryDataObject ds = new InventoryDataObject();
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();

                if (factoryWID.HasValue)
                {
                    adap.SelectCommand = new System.Data.SqlClient.SqlCommand("InventoryOverviewRpt_function_ExportInventoryWithFactoryWarehouse", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                    adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adap.SelectCommand.Parameters.AddWithValue("@FactoryWarehouseID", factoryWID);
                }

                if (productionTID.HasValue)
                {
                    adap.SelectCommand = new System.Data.SqlClient.SqlCommand("InventoryOverviewRpt_function_ExportInventoryWithProductionTeam", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                    adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    adap.SelectCommand.Parameters.AddWithValue("@ProductionTeamID", productionTID);
                }

                adap.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
                adap.SelectCommand.Parameters.AddWithValue("@EndDate", endDate);
                adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);

                adap.TableMappings.Add("Table", "CommonData");
                adap.TableMappings.Add("Table1", "InventoryData");
                adap.TableMappings.Add("Table2", "ClientData");

                adap.Fill(ds);
                //ds.AcceptChanges();
                foreach (var inventoryItem in ds.InventoryData.ToList())
                {
                    var clientUDs = ds.ClientData.Where(o => o.ProductionItemUD == inventoryItem.ProductionItemUD).Select(s => s.ClientUD);
                    if (clientUDs != null && clientUDs.Count() > 0)
                    {
                        inventoryItem.ClientUD = clientUDs.Aggregate((i, j) => i + "," + j);
                    }
                }

                fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "Inventory");
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return fileName;
        }

        public DTO.SearchFormData GetDataWithFilters(int userID, int? factoryWarsehouseID, int? productionTeamID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.SearchFormData data = new DTO.SearchFormData(factoryWarsehouseID.Value);

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                int? factoryWID = (factoryWarsehouseID <= 0) ? (int?)null : factoryWarsehouseID;
                int? productionTID = (productionTeamID <= 0) ? (int?)null : productionTeamID;

                if (!factoryWID.HasValue && !productionTID.HasValue)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Please select one Factory Warehouse or one Production Team to search";

                    return data;
                }

                using (var context = CreateContext())
                {
                    var ListFrameMaterialColor = context.FrameMaterialColorMng_FrameMaterialColor_View.ToList();

                    if (factoryWID.HasValue && factoryWID.Value == 46)
                    {
                        data.InventoryFinishProduct = converter.DB2DTO_InventoryFinishProducts(context.InventoryRpt_function_InventoryFinishProduct(companyID, factoryWID, productionTID, startDate, endDate).ToList());
                        data.InventoryFinishProductDetail = converter.DB2DTO_InventoryFinishProductDetail(context.InventoryRpt_function_InventoryFinishProductDetail(companyID, factoryWID, productionTID, startDate, endDate).ToList());
                    }
                    else
                    {
                        if (factoryWID.HasValue)
                        {
                            data.InventoryOverview = converter.DB2DTO_SearchResults(context.InventoryOverviewRpt_function_GetInventoryWithFactoryWarehouse(factoryWID.Value, startDate, endDate).ToList());
                            data.InventoryOverviewDetail = converter.DB2DTO_InventoryDetail(context.InventoryOverviewDetailRpt_function_GetInventoryWithFactoryWarehouse(factoryWID.Value, startDate, endDate).ToList());
                        }

                        if (productionTID.HasValue)
                        {
                            data.InventoryOverview = converter.DB2DTO_SearchResults(context.InventoryOverviewRpt_function_GetInventoryWithProductionTeam(productionTID.Value, startDate, endDate, companyID).ToList());
                            data.InventoryOverviewDetail = converter.DB2DTO_InventoryDetail(context.InventoryOverviewDetailRpt_function_GetInventoryWithProductionTeam(productionTID.Value, startDate, endDate, companyID).ToList());
                        }

                        data.InventoryOverviewClient = converter.DB2DTO_InventoryOverviewClient(context.InventoryRpt_InventoryOverviewWithClient_View.ToList());
                    }

                    if (factoryWID.HasValue && (factoryWID.Value == 40 || factoryWID.Value == 42 || factoryWID.Value == 66 || factoryWID.Value == 67))
                    {
                        if (data.InventoryOverview != null)
                        {
                            foreach (var item in data.InventoryOverview)
                            {
                                if (item.ProductionItemTypeID == 1 && !string.IsNullOrEmpty(item.ProductionItemUD))
                                {
                                    var frameColorCode = "";
                                    if (item.ProductionItemUD.LastIndexOf('-') > 0)
                                    {
                                        frameColorCode = item.ProductionItemUD.Substring(0, item.ProductionItemUD.LastIndexOf('-'));
                                    }
                                    if (frameColorCode.Length >= 8)
                                    {
                                        frameColorCode = frameColorCode.Substring(6, 2);
                                        foreach (var color in ListFrameMaterialColor)
                                        {
                                            if (frameColorCode == color.FrameMaterialColorUD)
                                            {
                                                item.FrameMaterialColorNM = color.FrameMaterialColorNM;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        private InventoryRptEntities CreateContext()
        {
            return new InventoryRptEntities(Library.Helper.CreateEntityConnectionString("DAL.InventoryRptModel"));
        }

        public string ExportInventoryReportDetail(int userID, int factoryWarehouseID, int? productionTeamID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            string fileName = string.Empty;

            InventoryDataObject ds = new InventoryDataObject();

            try
            {
                //// Get company.
                //Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                //int? companyID = fwFactory.GetCompanyID(userID);

                //// Get factory warehouse.
                //int? factoryWID = (factoryWarehouseID <= 0) ? (int?)null : factoryWarehouseID;
                //int? productionTID = (productionTeamID <= 0) ? (int?)null : productionTeamID;

                //System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                //adap.SelectCommand = new System.Data.SqlClient.SqlCommand("InventoryRpt_function_ExportExcel", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                //adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //adap.SelectCommand.Parameters.AddWithValue("@companyID", companyID);
                //adap.SelectCommand.Parameters.AddWithValue("@factoryWarehouseID", factoryWID);
                //adap.SelectCommand.Parameters.AddWithValue("@productionTeamID", productionTID);
                //adap.SelectCommand.Parameters.AddWithValue("@startDate", startDate);
                //adap.SelectCommand.Parameters.AddWithValue("@endDate", endDate);
                //adap.TableMappings.Add("Table", "WarehouseData");
                //adap.TableMappings.Add("Table1", "InventoryData");
                //adap.TableMappings.Add("Table2", "InventoryDetailData");
                //adap.Fill(ds);

                //InventoryDataObject.CommonDataRow hRow = ds.CommonData.NewCommonDataRow();
                //hRow.ReportDate = DateTime.Now.ToString("dd/MM/yyyy");
                //hRow.StartDate = startDate;
                //hRow.EndDate = endDate;
                //hRow.FactoryWarehouseID = factoryWarehouseID;
                //ds.CommonData.AddCommonDataRow(hRow);

                //ds.Tables.Remove("InventoryRpt_Inventory_View");
                //ds.Tables.Remove("InventoryRpt_InventoryDetail_View");

                //ds.AcceptChanges();

                //fileName = Library.Helper.CreateReportFileWithEPPlus2(ds, "InventoryDetail");
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

        public DTO.SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.SearchFormData data = new DTO.SearchFormData(0);

            try
            {
                int? factoryWarehouseID = filters.ContainsKey("factoryWarhouseID") && filters["factoryWarhouseID"] != null && !string.IsNullOrEmpty(filters["factoryWarhouseID"].ToString().Trim()) ? (int?)Convert.ToInt32(filters["factoryWarhouseID"].ToString()) : null;
                int? productionTeamID = filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null && !string.IsNullOrEmpty(filters["productionTeamID"].ToString().Trim()) ? (int?)Convert.ToInt32(filters["productionTeamID"].ToString()) : null;
                string startDate = filters.ContainsKey("startDate") && filters["startDate"] != null ? filters["startDate"].ToString() : null;
                string endDate = filters.ContainsKey("endDate") && filters["endDate"] != null ? filters["endDate"].ToString() : null;

                int? companyID = null;

                using (InventoryRptEntities context = CreateContext())
                {
                    if (factoryWarehouseID.HasValue)
                    {
                        if (factoryWarehouseID.Value == 46)
                        {
                            totalRows = context.InventoryRpt_function_InventoryFinishProduct(companyID, factoryWarehouseID, productionTeamID, startDate, endDate).Count();
                        }
                        else
                        {
                            totalRows = 0;
                        }
                    }
                    else
                    {
                        if (productionTeamID.HasValue)
                        {

                        }
                    }

                    data.InventoryOverviewClient = converter.DB2DTO_InventoryOverviewClient(context.InventoryRpt_InventoryOverviewWithClient_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        /// Custom function get data (new version)
        /// FactoryWarehouses: list selected from front-end
        /// ProductionTeamID
        /// StartDate, EndDate
        public object CustomGetDataWithFilter(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.InventoryDTO data = new DTO.InventoryDTO();

            try
            {
                string factoryWarehouses = null;
                int? productionTeamID = null;
                string startDate = null;
                string endDate = null;

                if (filters.ContainsKey("factoryWarehouses") && filters["factoryWarehouses"] != null && !string.IsNullOrEmpty(filters["factoryWarehouses"].ToString()))
                {
                    factoryWarehouses = filters["factoryWarehouses"].ToString();
                }

                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null && !string.IsNullOrEmpty(filters["productionTeamID"].ToString()))
                {
                    productionTeamID = (int?)Convert.ToInt32(filters["productionTeamID"].ToString());
                }

                if (filters.ContainsKey("startDate") && filters["startDate"] != null && !string.IsNullOrEmpty(filters["startDate"].ToString()))
                {
                    startDate = filters["startDate"].ToString();
                }

                if (filters.ContainsKey("endDate") && filters["endDate"] != null && !string.IsNullOrEmpty(filters["endDate"].ToString()))
                {
                    endDate = filters["endDate"].ToString();
                }

                if (factoryWarehouses == null && productionTeamID == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Please select factory warehouse or production team!";
                    return data;
                }

                if (startDate == null || endDate == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Please fill start date and end date!";
                    return data;
                }

                using (var context = CreateContext())
                {
                    if (!string.IsNullOrEmpty(factoryWarehouses))
                    {
                        List<InventoryRpt_function_InventorySearchResult2_Result> dbItem = context.InventoryRpt_function_InventorySearchResult2(factoryWarehouses, startDate, endDate).ToList();
                        List<InventoryRpt_function_InventorySearchResult4_Result> dbItemDetail = context.InventoryRpt_function_InventorySearchResult4(factoryWarehouses, startDate, endDate).ToList();

                        data.Inventory = AutoMapper.Mapper.Map<List<InventoryRpt_function_InventorySearchResult2_Result>, List<DTO.InventoryData>>(dbItem);
                        data.InventoryDetail = AutoMapper.Mapper.Map<List<InventoryRpt_function_InventorySearchResult4_Result>, List<DTO.InventoryDetailData>>(dbItemDetail);
                    }

                    if (productionTeamID.HasValue)
                    {
                        List<InventoryRpt_function_InventorySearchResult3_Result> dbItem = context.InventoryRpt_function_InventorySearchResult3(productionTeamID, startDate, endDate).ToList();
                        List<InventoryRpt_function_InventorySearchResult5_Result> dbItemDetail = context.InventoryRpt_function_InventorySearchResult5(productionTeamID, startDate, endDate).ToList();

                        data.Inventory = AutoMapper.Mapper.Map<List<InventoryRpt_function_InventorySearchResult3_Result>, List<DTO.InventoryData>>(dbItem);
                        data.InventoryDetail = AutoMapper.Mapper.Map<List<InventoryRpt_function_InventorySearchResult5_Result>, List<DTO.InventoryDetailData>>(dbItemDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }


        /// Custom function export data (new version)
        /// FactoryWarehouses: list selected from front-end
        /// ProductionTeamID
        /// StartDate, EndDate
        public object CustomExportDataWithFilter(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            string path = null;

            try
            {
                string factoryWarehouses = null;
                int? productionTeamID = null;
                string startDate = null;
                string endDate = null;
                int? companyID = null;

                if (filters.ContainsKey("factoryWarehouses") && filters["factoryWarehouses"] != null && !string.IsNullOrEmpty(filters["factoryWarehouses"].ToString()))
                {
                    factoryWarehouses = filters["factoryWarehouses"].ToString();
                }

                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null && !string.IsNullOrEmpty(filters["productionTeamID"].ToString()))
                {
                    productionTeamID = (int?)Convert.ToInt32(filters["productionTeamID"].ToString());
                }

                if (filters.ContainsKey("startDate") && filters["startDate"] != null && !string.IsNullOrEmpty(filters["startDate"].ToString()))
                {
                    startDate = filters["startDate"].ToString();
                }

                if (filters.ContainsKey("endDate") && filters["endDate"] != null && !string.IsNullOrEmpty(filters["endDate"].ToString()))
                {
                    endDate = filters["endDate"].ToString();
                }

                if (factoryWarehouses == null && productionTeamID == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Please select factory warehouse or production team!";
                    return path;
                }

                if (startDate == null || endDate == null)
                {
                    notification.Type = NotificationType.Error;
                    notification.Message = "Please fill start date and end date!";
                    return path;
                }

                using (var context = CreateContext())
                {
                    InventoryDataObject ds = new InventoryDataObject();
                    System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();

                    if (!string.IsNullOrEmpty(factoryWarehouses))
                    {
                        adap.SelectCommand = new System.Data.SqlClient.SqlCommand("InventoryOverviewRpt_function_ExportInventoryWithFactoryWarehouse", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                        adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        adap.SelectCommand.Parameters.AddWithValue("@factoryWarehouses", factoryWarehouses);
                    }

                    if (productionTeamID.HasValue)
                    {
                        Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                        companyID = fwFactory.GetCompanyID(userID);

                        adap.SelectCommand = new System.Data.SqlClient.SqlCommand("InventoryOverviewRpt_function_ExportInventoryWithProductionTeam", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                        adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        adap.SelectCommand.Parameters.AddWithValue("@productionTeamID", productionTeamID);
                        adap.SelectCommand.Parameters.AddWithValue("@companyID", companyID);
                    }

                    adap.SelectCommand.Parameters.AddWithValue("@startDate", startDate);
                    adap.SelectCommand.Parameters.AddWithValue("@endDate", endDate);

                    adap.TableMappings.Add("Table", "CommonData");
                    adap.TableMappings.Add("Table1", "InventoryData");
                    adap.TableMappings.Add("Table2", "ClientData");

                    adap.Fill(ds);

                    foreach (var inventoryItem in ds.InventoryData.ToList())
                    {
                        var clientUDs = ds.ClientData.Where(o => o.ProductionItemUD == inventoryItem.ProductionItemUD).Select(s => s.ClientUD);
                        if (clientUDs != null && clientUDs.Count() > 0)
                        {
                            inventoryItem.ClientUD = clientUDs.Aggregate((i, j) => i + "," + j);
                        }
                    }

                    path = Library.Helper.CreateReportFileWithEPPlus2(ds, "Inventory");
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return path;
        }
    }
}
