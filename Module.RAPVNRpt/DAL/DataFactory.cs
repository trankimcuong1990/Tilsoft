using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.RAPVNRpt.DTO;

namespace Module.RAPVNRpt.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private RAPVNRptEntities CreateContext()
        {
            return new RAPVNRptEntities(Library.Helper.CreateEntityConnectionString("DAL.RAPVNRptModel"));
        }
        public DTO.SearchForm GetDataWithFilters(Hashtable input, int userId, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchForm data = new DTO.SearchForm();
            totalRows = 0;
            try
            {
                //Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                //int? companyID = fwFactory.GetCompanyID(userId);
                bool isRAPEU = false;
                int? factoryID = (input.ContainsKey("factoryID") && input["factoryID"] != null && !string.IsNullOrEmpty(input["factoryID"].ToString()) ? (int?)Convert.ToInt32(input["factoryID"].ToString()) : null);
                string season = (input.ContainsKey("seasonText") && input["seasonText"] != null && !string.IsNullOrEmpty(input["seasonText"].ToString()) ? Convert.ToString(input["seasonText"]) : "");
                string proformaInvoiceNo = (input.ContainsKey("proformaInvoiceNo") && input["proformaInvoiceNo"] != null && !string.IsNullOrEmpty(input["proformaInvoiceNo"].ToString()) ? Convert.ToString(input["proformaInvoiceNo"]) : "");
                string clientUD = (input.ContainsKey("clientUD") && input["clientUD"] != null && !string.IsNullOrEmpty(input["clientUD"].ToString()) ? Convert.ToString(input["clientUD"]) : "");
                string articleCode = (input.ContainsKey("articleCode") && input["articleCode"] != null && !string.IsNullOrEmpty(input["articleCode"].ToString()) ? Convert.ToString(input["articleCode"]) : "");
                string description = (input.ContainsKey("description") && input["description"] != null && !string.IsNullOrEmpty(input["description"].ToString()) ? Convert.ToString(input["description"]) : "");
                int loadTab = Convert.ToInt32(input["loadTab"]);
                int pageSize = Convert.ToInt32(input["pageSize"]);
                int pageIndex = Convert.ToInt32(input["pageIndex"]);

                using (var context = CreateContext())
                {
                    data.WorkOrderDatas = converter.DTO2DB_SearchWorkOrder(context.RAPVNRpt_WorkOrder_View.ToList());
                    if (loadTab == 1)
                    {
                        //get total Order
                        var sumData = context.RAPVNRpt_function_GetOrder(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description).ToList();
                        data.TotalOrderQnt = sumData.Sum(o => o.OrderQnt);
                        data.TotalOrderInCont = sumData.Sum(o => o.OrderQntIn40HC.HasValue ? o.OrderQntIn40HC.Value : 0);
                        data.TotalOrderShippedQnt = sumData.Sum(o => o.TotalShippedQnt);
                        data.TotalOrderShippedQnt40HC = sumData.Sum(o => o.TotalShippedQntIn40HC.HasValue ? o.TotalShippedQntIn40HC.Value : 0);
                        data.TotalOrderToBeShippedQnt = sumData.Sum(o => o.ToBeShippedQnt.HasValue ? o.ToBeShippedQnt.Value : 0);
                        data.TotalOrderToBeShippedQnt40HC = sumData.Sum(o => o.ToBeShippedQntIn40HC.HasValue ? o.ToBeShippedQntIn40HC.Value : 0);

                        data.OrderDatas = new List<DTO.OrderData>();
                        totalRows = context.RAPVNRpt_function_GetOrder(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description).Count();
                        input["totalRows"] = totalRows;
                        var result = context.RAPVNRpt_function_GetOrder(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description);
                        data.OrderDatas = converter.DTO2DB_SearchOrder(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    }
                    if (loadTab == 2)
                    {
                        //get total Loaded
                        var sumData = context.RAPVNRpt_function_GetLoaded(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description).ToList();
                        data.TotalLoadedQnt = sumData.Sum(o => o.LoadedQnt);
                        data.TotalLoadedQnt40HC = sumData.Sum(o => o.LoadedQntIn40HC);

                        data.LoadedDatas = new List<LoadedData>();
                        totalRows = context.RAPVNRpt_function_GetLoaded(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description).Count();
                        input["totalRows"] = totalRows;
                        var result = context.RAPVNRpt_function_GetLoaded(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description);
                        data.LoadedDatas = converter.DTO2DB_SearchLoaded(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    }
                    if (loadTab == 3)
                    {
                        //get total Shipped
                        var sumData = context.RAPVNRpt_function_GetShipped(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description).ToList();
                        data.ShippedQnt = sumData.Sum(o => o.ShippedQnt);
                        data.ShippedQnt40HC = sumData.Sum(o => o.ShippedQntIn40HC);
                        data.TotalShippedLoadedQnt = sumData.Sum(o => o.LoadedQnt);
                        data.TotalShippedLoadedQnt40HC = sumData.Sum(o => o.LoadedQntIn40HC);

                        data.ShippedDatas = new List<ShippedData>();
                        totalRows = context.RAPVNRpt_function_GetShipped(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description).Count();
                        input["totalRows"] = totalRows;
                        var result = context.RAPVNRpt_function_GetShipped(season, factoryID, userId, isRAPEU, proformaInvoiceNo, clientUD, articleCode, description);
                        data.ShippedDatas = converter.DTO2DB_SearchShipped(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList().ToList());
                    }
                }
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

            return data;
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

        public object GetInitData(int userID, out Notification notification)
        {
            DTO.EditForm data = new EditForm() { Season = new List<Support.DTO.Season>(), Factory = new List<Support.DTO.Factory>() };
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

                data.Season = supportFactory.GetSeason().ToList();
                data.Factory = supportFactory.GetFactory(userID).ToList();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public string GetExcelReportData(bool isRAPEU, string season, int? factoryID, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            isRAPEU = false;
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("Report_function_GetRAPEU", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryID", factoryID);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                adap.SelectCommand.Parameters.AddWithValue("@IsRapEU", isRAPEU);

                adap.TableMappings.Add("Table", "Header");
                adap.TableMappings.Add("Table1", "Report_RAPEU_Order_View");
                adap.TableMappings.Add("Table2", "Report_RAPEU_Shipped_View");
                adap.TableMappings.Add("Table3", "Report_RAPEU_Loaded_View");
                adap.TableMappings.Add("Table4", "SupplierSCMResponsible");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "RAPEU");

                // Update value supplier chain responsible Order, Shipped, Loaded
                foreach (var dbOrderItem in ds.Report_RAPEU_Order_View)
                {
                    dbOrderItem.SupplyChainPersonNM = "";

                    var supplierChain = ds.SupplierSCMResponsible.Where(o => o.FactoryID == dbOrderItem.FactoryID).Select(s => s.EmployeeNM);
                    if (supplierChain != null && supplierChain.Count() > 0)
                    {
                        dbOrderItem.SupplyChainPersonNM = supplierChain.Aggregate((i, j) => i + "," + j);
                    }
                    //dbOrderItem.SupplyChainPersonNM = ds.SupplierSCMResponsible.Where(o => o.FactoryID == dbOrderItem.FactoryID).Select(s => s.EmployeeNM).Aggregate((i, j) => i + "," + j);
                }

                foreach (var dbShippedItem in ds.Report_RAPEU_Shipped_View)
                {
                    dbShippedItem.SupplyChainPersonNM = "";

                    var supplierChain = ds.SupplierSCMResponsible.Where(o => o.FactoryID == dbShippedItem.FactoryID).Select(s => s.EmployeeNM);
                    if (supplierChain != null && supplierChain.Count() > 0)
                    {
                        dbShippedItem.SupplyChainPersonNM = supplierChain.Aggregate((i, j) => i + "," + j);
                    }
                }

                foreach (var dbLoadedItem in ds.Report_RAPEU_Loaded_View)
                {
                    dbLoadedItem.SupplyChainPersonNM = "";

                    var supplierChain = ds.SupplierSCMResponsible.Where(o => o.FactoryID == dbLoadedItem.FactoryID).Select(s => s.EmployeeNM);
                    if (supplierChain != null && supplierChain.Count() > 0)
                    {
                        dbLoadedItem.SupplyChainPersonNM = supplierChain.Aggregate((i, j) => i + "," + j);
                    }
                }

                // generate xml file
                //return DALBase.Helper.CreateReportFiles(ds, IsRAPEU ? "RAPEU" : "RAPVN");
                return Library.Helper.CreateReportFileWithEPPlus2(ds, isRAPEU ? "RAPEU" : "RAPVN");
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
