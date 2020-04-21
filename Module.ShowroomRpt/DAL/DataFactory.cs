using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.ShowroomRpt.DTO;

namespace Module.ShowroomRpt.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchDataDto, DTO.EditDataDto>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        public ShowroomRptEntities CreateContext()
        {
            return new ShowroomRptEntities(Library.Helper.CreateEntityConnectionString("DAL.ShowroomRptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditDataDto GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchDataDto GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
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

        public object GetInitData(int userID, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            InitDto data = new InitDto();
            data.Data = new List<FactoryWarehouse>();
            data.ReceivingNotes = new List<ReceivingNote>();
            data.FactoryWarehousePallets = new List<FactoryWarehousePallet>();
            data.Seasons = new List<Support.DTO.Season>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    if (companyID.HasValue)
                    {
                        if (companyID.Value == 1)
                        {
                            data.Data = AutoMapper.Mapper.Map<List<ShowroomRpt_SupportFactoryWarehouseAVF_View>, List<FactoryWarehouse>>(context.ShowroomRpt_SupportFactoryWarehouseAVF_View.ToList());
                        }
                        else
                        {
                            data.Data = AutoMapper.Mapper.Map<List<ShowroomRpt_SupportFactoryWarehouseAVT_View>, List<FactoryWarehouse>>(context.ShowroomRpt_SupportFactoryWarehouseAVT_View.ToList());
                        }
                    }
                    data.ReceivingNotes = converter.DB2DTO_ReceivingNote(context.ShowroomRpt_ReceivingNote_View.ToList());
                    data.FactoryWarehousePallets = converter.DB2DTO_FactoryWarehousePallet(context.ShowroomRpt_FactoryWarehousePallet_View.ToList());
                }
                data.Seasons = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public string ExportExcel(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            string clientUD = (filters.ContainsKey("clientUD") && filters["clientUD"] != null) ? filters["clientUD"].ToString() : null;
            string productionItemUD = (filters.ContainsKey("productionItemUD") && filters["productionItemUD"] != null) ? filters["productionItemUD"].ToString() : null;
            string productionItemNM = (filters.ContainsKey("productionItemNM") && filters["productionItemNM"] != null) ? filters["productionItemNM"].ToString() : null;
            string factoryUD = (filters.ContainsKey("factoryUD") && filters["factoryUD"] != null) ? filters["factoryUD"].ToString() : null;
            string factoryWarehouses = (filters.ContainsKey("listFactoryWarehouse") && filters["listFactoryWarehouse"] != null) ? filters["listFactoryWarehouse"].ToString() : null;
            string season = (filters.ContainsKey("season") && filters["season"] != null) ? filters["season"].ToString() : null;
            string factoryWarehousePalletUD = (filters.ContainsKey("factoryWarehousePalletUD") && filters["factoryWarehousePalletUD"] != null) ? filters["factoryWarehousePalletUD"].ToString() : null;
            int? companyID = fwFactory.GetCompanyID(userID);

            try
            {
                if (companyID.HasValue)
                {
                    if (companyID.Value == 3 || companyID.Value == 4)
                    {
                        // Nothing
                    }

                    if (companyID.Value == 1)
                    {
                        if (string.IsNullOrEmpty(factoryWarehouses))
                        {
                            factoryWarehouses = "39,41"; // SPOGA WAREHOUSE
                        }
                    }

                    System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                    adap.SelectCommand = new System.Data.SqlClient.SqlCommand("ShowroomRpt_function_ExportShowroom", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                    adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    adap.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                    adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                    adap.SelectCommand.Parameters.AddWithValue("@ClientUD", clientUD);
                    adap.SelectCommand.Parameters.AddWithValue("@ProductionItemUD", productionItemUD);
                    adap.SelectCommand.Parameters.AddWithValue("@ProductionItemNM", productionItemNM);
                    adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", factoryUD);
                    adap.SelectCommand.Parameters.AddWithValue("@FactoryWarehouses", factoryWarehouses);
                    adap.SelectCommand.Parameters.AddWithValue("@FactoryWarehousePalletUD", factoryWarehousePalletUD);
                    adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                    adap.SelectCommand.Parameters.AddWithValue("@SortedBy", String.Empty);
                    adap.SelectCommand.Parameters.AddWithValue("@SortedDirection", String.Empty);

                    adap.TableMappings.Add("Table", "ShowroomRpt_Showroom2_View");
                    adap.TableMappings.Add("Table1", "ShowroomRpt_ReceivingNote_View");
                    adap.TableMappings.Add("Table2", "ShowroomRpt_FactoryWarehousePallet_View");
                    adap.Fill(ds);

                    ds.AcceptChanges();

                    return Library.Helper.CreateReportFileWithEPPlus(ds, "ShowroomRpt");
                }

                return "";
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

        public SearchDataDto GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification();
            notification.Type = NotificationType.Success;

            SearchDataDto data = new SearchDataDto();
            data.Data = new List<ShowroomDataDto>();
            data.ProductionItemWithDescriptions = new List<ProductionItemWithDescription>();

            try
            {
                string clientUD = (filters.ContainsKey("clientUD") && filters["clientUD"] != null) ? filters["clientUD"].ToString() : null;
                string productionItemUD = (filters.ContainsKey("productionItemUD") && filters["productionItemUD"] != null) ? filters["productionItemUD"].ToString() : null;
                string productionItemNM = (filters.ContainsKey("productionItemNM") && filters["productionItemNM"] != null) ? filters["productionItemNM"].ToString() : null;
                string factoryUD = (filters.ContainsKey("factoryUD") && filters["factoryUD"] != null) ? filters["factoryUD"].ToString() : null;
                string factoryWarehouses = (filters.ContainsKey("listFactoryWarehouse") && filters["listFactoryWarehouse"] != null) ? filters["listFactoryWarehouse"].ToString() : null;
                string factoryWarehousePalletUD = (filters.ContainsKey("factoryWarehousePalletUD") && filters["factoryWarehousePalletUD"] != null) ? filters["factoryWarehousePalletUD"].ToString() : null;
                string season = (filters.ContainsKey("season") && filters["season"] != null) ? filters["season"].ToString() : null;

                int? companyID = fwFactory.GetCompanyID(userID);

                using (var context = CreateContext())
                {
                    if (companyID.HasValue) // Only Eurofar, An Viet Thinh And OrangePine
                    {
                        if (companyID.Value == 3 || companyID.Value == 4 || companyID.Value == 13)
                        {
                            totalRows = context.ShowroomRpt_function_ShowroomSearchResult(userID, companyID, clientUD, productionItemUD, productionItemNM, factoryUD, factoryWarehouses, factoryWarehousePalletUD,season, orderBy, orderDirection).Count();
                            data.Data1 = converter.DB2DTO_Showroom2Rpt(context.ShowroomRpt_function_ShowroomSearchResult(userID, companyID, clientUD, productionItemUD, productionItemNM, factoryUD, factoryWarehouses, factoryWarehousePalletUD, season, orderBy, orderDirection).ToList());
                            data.Data = converter.DB2DTO_Showroom2Rpt(context.ShowroomRpt_function_ShowroomSearchResult(userID, companyID, clientUD, productionItemUD, productionItemNM, factoryUD, factoryWarehouses, factoryWarehousePalletUD, season, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                            data.ProductionItemWithDescriptions = converter.DB2DTO_ProductionItemWithDescription(context.ShowroomRpt_function_ProductionItemWithDescription(userID, companyID, clientUD, productionItemUD, productionItemNM, factoryUD, factoryWarehouses, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                            data.TotalQuantity = 0;
                            for (int i = 0; i < data.Data1.Count; i++)
                            {
                                if(data.Data1[i].WarehouseQuantity != null)
                                {
                                    data.TotalQuantity += data.Data1[i].WarehouseQuantity;
                                }
                            }
                        }

                        if (companyID.Value == 1)
                        {
                            if (string.IsNullOrEmpty(factoryWarehouses))
                            {
                                factoryWarehouses = "39,41"; // SPOGA WAREHOUSE AND SEAL SAMPLE WAREHOUSE
                            }

                            totalRows = context.ShowroomRpt_function_ShowroomSearchResult(userID, companyID, clientUD, productionItemUD, productionItemNM, factoryUD, factoryWarehouses, factoryWarehousePalletUD, season, orderBy, orderDirection).Count();
                            data.Data1 = converter.DB2DTO_Showroom2Rpt(context.ShowroomRpt_function_ShowroomSearchResult(userID, companyID, clientUD, productionItemUD, productionItemNM, factoryUD, factoryWarehouses, factoryWarehousePalletUD, season, orderBy, orderDirection).ToList());
                            data.Data = converter.DB2DTO_Showroom2Rpt(context.ShowroomRpt_function_ShowroomSearchResult(userID, companyID, clientUD, productionItemUD, productionItemNM, factoryUD, factoryWarehouses, factoryWarehousePalletUD, season, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                            data.ProductionItemWithDescriptions = converter.DB2DTO_ProductionItemWithDescription(context.ShowroomRpt_function_ProductionItemWithDescription(userID, companyID, clientUD, productionItemUD, productionItemNM, factoryUD, factoryWarehouses, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                            data.TotalQuantity = 0;
                            for (int i = 0; i < data.Data1.Count; i++)
                            {
                                if (data.Data1[i].WarehouseQuantity != null)
                                {
                                    data.TotalQuantity += data.Data1[i].WarehouseQuantity;
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

        public string ExportExcelWithoutImage(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            string clientUD = (filters.ContainsKey("clientUD") && filters["clientUD"] != null) ? filters["clientUD"].ToString() : null;
            string productionItemUD = (filters.ContainsKey("productionItemUD") && filters["productionItemUD"] != null) ? filters["productionItemUD"].ToString() : null;
            string productionItemNM = (filters.ContainsKey("productionItemNM") && filters["productionItemNM"] != null) ? filters["productionItemNM"].ToString() : null;
            string factoryUD = (filters.ContainsKey("factoryUD") && filters["factoryUD"] != null) ? filters["factoryUD"].ToString() : null;
            string factoryWarehouses = (filters.ContainsKey("listFactoryWarehouse") && filters["listFactoryWarehouse"] != null) ? filters["listFactoryWarehouse"].ToString() : null;
            string season = (filters.ContainsKey("season") && filters["season"] != null) ? filters["season"].ToString() : null;
            string factoryWarehousePalletUD = (filters.ContainsKey("factoryWarehousePalletUD") && filters["factoryWarehousePalletUD"] != null) ? filters["factoryWarehousePalletUD"].ToString() : null;

            int? companyID = fwFactory.GetCompanyID(userID);

            try
            {
                if (companyID.HasValue)
                {
                    if (companyID.Value == 3 || companyID.Value == 4)
                    {
                        // Nothing
                    }

                    if (companyID.Value == 1)
                    {
                        if (string.IsNullOrEmpty(factoryWarehouses))
                        {
                            factoryWarehouses = "39,41"; // SPOGA WAREHOUSE
                        }
                    }

                    System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                    adap.SelectCommand = new System.Data.SqlClient.SqlCommand("ShowroomRpt_function_ExportShowroom", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                    adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    adap.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                    adap.SelectCommand.Parameters.AddWithValue("@CompanyID", companyID);
                    adap.SelectCommand.Parameters.AddWithValue("@ClientUD", clientUD);
                    adap.SelectCommand.Parameters.AddWithValue("@ProductionItemUD", productionItemUD);
                    adap.SelectCommand.Parameters.AddWithValue("@ProductionItemNM", productionItemNM);
                    adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", factoryUD);
                    adap.SelectCommand.Parameters.AddWithValue("@FactoryWarehouses", factoryWarehouses);
                    adap.SelectCommand.Parameters.AddWithValue("@FactoryWarehousePalletUD", factoryWarehousePalletUD);
                    adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                    adap.SelectCommand.Parameters.AddWithValue("@SortedBy", String.Empty);
                    adap.SelectCommand.Parameters.AddWithValue("@SortedDirection", String.Empty);

                    adap.TableMappings.Add("Table", "ShowroomRpt_Showroom2_View");
                    adap.TableMappings.Add("Table1", "ShowroomRpt_ReceivingNote_View");
                    adap.TableMappings.Add("Table2", "ShowroomRpt_FactoryWarehousePallet_View");
                    adap.Fill(ds);

                    ds.AcceptChanges();

                    return Library.Helper.CreateReportFileWithEPPlus(ds, "ShowroomRpt_2");
                }

                return "";
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
