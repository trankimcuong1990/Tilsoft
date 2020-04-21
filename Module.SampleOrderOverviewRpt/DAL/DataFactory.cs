using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleOrderOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private SampleOrderOverviewRptEntities CreateContext()
        {
            return new SampleOrderOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.SampleOrderOverviewRptModel"));
        }

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
        public string GetExcelReportData(string Season, string clientId, int? vnFactoryId, int? sampleProductStatusID, int? sampleOrderStatusID, int? sampleOrderID, bool canEdit, bool canRead, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("SampleOrderOverviewRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", clientId);

                // Add filter to export data
                // Can edit to see all items.
                // Can read only to see item in VN suggested factory of end-user manage
                adap.SelectCommand.Parameters.AddWithValue("@CanEdit", canEdit);
                adap.SelectCommand.Parameters.AddWithValue("@CanRead", canRead);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", userId);

                if (vnFactoryId.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@VNSuggestedFactoryID", vnFactoryId.Value);
                }
                if (sampleProductStatusID.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SampleProductStatusID", sampleProductStatusID.Value);
                }
                if (sampleOrderStatusID.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SampleOrderStatusID", sampleOrderStatusID.Value);
                }
                if (sampleOrderID.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SampleOrderID", sampleOrderID.Value);
                }
                adap.TableMappings.Add("Table", "SampleOrderOverviewRpt_ReportData_View");
                adap.TableMappings.Add("Table1", "SampleOrderOverviewRpt_ReportMinuteData_View");
                adap.TableMappings.Add("Table2", "SampleOrderOverviewRpt_ReportRemarkData_View");
                adap.TableMappings.Add("Table3", "SampleOrderOverviewRpt_ReportSampleOrder_View");
                adap.TableMappings.Add("Table4", "SampleOrderOverviewRpt_ReportQARemarkData_View");
                adap.Fill(ds);

                ReportDataObject.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                hRow.Season = Season;
                ds.ReportHeader.AddReportHeaderRow(hRow);

                // processing data and cleanup
                int sampleProductID = -1;
                string bigRemark = string.Empty;
                foreach (DAL.ReportDataObject.SampleOrderOverviewRpt_ReportData_ViewRow row in ds.SampleOrderOverviewRpt_ReportData_View)
                {
                    sampleProductID = row.SampleProductID;
                    bigRemark = string.Empty;
                    foreach (DAL.ReportDataObject.SampleOrderOverviewRpt_ReportQARemarkData_ViewRow mRow in ds.SampleOrderOverviewRpt_ReportQARemarkData_View.Where(o => o.SampleProductID == sampleProductID))
                    {
                        bigRemark += mRow.IndexNumber.ToString() + ". " + mRow.UpdatorName + "(" + mRow.DisplayUpdatedDate + ")";
                        bigRemark += Environment.NewLine + mRow.Remark + Environment.NewLine + Environment.NewLine;
                    }
                    row.LastMinuteTableRemark = bigRemark;

                    bigRemark = string.Empty;
                    foreach (DAL.ReportDataObject.SampleOrderOverviewRpt_ReportRemarkData_ViewRow rRow in ds.SampleOrderOverviewRpt_ReportRemarkData_View.Where(o => o.SampleProductID == sampleProductID))
                    {
                        bigRemark += rRow.IndexNumber.ToString() + ". " + rRow.UpdatorName + "(" + rRow.DisplayUpdatedDate + ")";
                        bigRemark += Environment.NewLine + rRow.Remark + Environment.NewLine + Environment.NewLine;
                    }
                    row.InternRemark = bigRemark;

                    row.Barcode = Library.Helper.QRCodeImageFile(row.SampleProductUD);
                }
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportMinuteData_View");
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportRemarkData_View");
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportQARemarkData_View");
                ds.AcceptChanges();

                // generate xml file
                //return Library.Helper.CreateReportFileWithEPPlus(ds, "SampleOrderOverview");
                return Library.Helper.CreateReportFileWithEPPlus(ds, "SampleOrderOverview", new List<string>() { "SampleOrderOverviewRpt_ReportData_View", "SampleOrderOverviewRpt_ReportSampleOrder_View", "ReportHeader" });
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

        public string GetExcelReportData(int SampleOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("SampleOrderOverviewRpt_function_GetPrintData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@SampleOrderID", SampleOrderID);
                adap.TableMappings.Add("Table", "SampleOrderOverviewRpt_ReportData_View");
                adap.TableMappings.Add("Table1", "SampleOrderOverviewRpt_ReportMinuteData_View");
                adap.TableMappings.Add("Table2", "SampleOrderOverviewRpt_ReportRemarkData_View");
                adap.TableMappings.Add("Table3", "SampleOrderOverviewRpt_ReportSampleOrder_View");
                adap.TableMappings.Add("Table4", "SampleOrderOverviewRpt_ReportQARemarkData_View");
                adap.Fill(ds);

                ReportDataObject.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                hRow.Season = "";
                if (ds.SampleOrderOverviewRpt_ReportData_View.FirstOrDefault() != null)
                {
                    hRow.Season = ds.SampleOrderOverviewRpt_ReportData_View.FirstOrDefault().Season;
                }
                ds.ReportHeader.AddReportHeaderRow(hRow);

                // processing data and cleanup
                int sampleProductID = -1;
                string bigRemark = string.Empty;
                foreach (DAL.ReportDataObject.SampleOrderOverviewRpt_ReportData_ViewRow row in ds.SampleOrderOverviewRpt_ReportData_View)
                {
                    sampleProductID = row.SampleProductID;
                    bigRemark = string.Empty;
                    foreach (DAL.ReportDataObject.SampleOrderOverviewRpt_ReportQARemarkData_ViewRow mRow in ds.SampleOrderOverviewRpt_ReportQARemarkData_View.Where(o => o.SampleProductID == sampleProductID))
                    {
                        bigRemark += mRow.IndexNumber.ToString() + ". " + mRow.UpdatorName + "(" + mRow.DisplayUpdatedDate + ")";
                        bigRemark += Environment.NewLine + mRow.Remark + Environment.NewLine + Environment.NewLine;
                    }
                    row.LastMinuteTableRemark = bigRemark;

                    bigRemark = string.Empty;
                    foreach (DAL.ReportDataObject.SampleOrderOverviewRpt_ReportRemarkData_ViewRow rRow in ds.SampleOrderOverviewRpt_ReportRemarkData_View.Where(o => o.SampleProductID == sampleProductID))
                    {
                        bigRemark += rRow.IndexNumber.ToString() + ". " + rRow.UpdatorName + "(" + rRow.DisplayUpdatedDate + ")";
                        bigRemark += Environment.NewLine + rRow.Remark + Environment.NewLine + Environment.NewLine;
                    }
                    row.InternRemark = bigRemark;
                }
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportMinuteData_View");
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportRemarkData_View");
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportQARemarkData_View");
                ds.AcceptChanges();

                // generate xml file
                return Library.Helper.CreateReportFileWithEPPlus(ds, "SampleOrderOverview", new List<string>() { "SampleOrderOverviewRpt_ReportData_View", "SampleOrderOverviewRpt_ReportSampleOrder_View", "ReportHeader" });
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

        public DTO.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Seasons = new List<Support.DTO.Season>();
            data.Factories = new List<Support.DTO.Factory>();
            data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
            data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
                data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();

                using (SampleOrderOverviewRptEntities context = CreateContext())
                {
                    data.Factories = converter.DB2DTO_Factory(context.SampleOrderOverviewRpt_Factory_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.SampleOrder> GetSampleOrder(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.SampleOrder> data = new List<DTO.SampleOrder>();

            string season = null;
            int? factoryID = null;
            int? sampleOrderStatusID = null;
            int? sampleProductStatusID = null;
            string sampleOrderUD = null;
            int? clientID = null;
            bool canEdit = false;
            bool canRead = false;

            try
            {
                season = (filters.ContainsKey("season")) && (filters["season"] != null) && (!string.IsNullOrEmpty(filters["season"].ToString())) ? filters["season"].ToString().Trim() : null;
                factoryID = (filters.ContainsKey("vnFactoryId")) && (filters["vnFactoryId"] != null) && (!string.IsNullOrEmpty(filters["vnFactoryId"].ToString())) ? (int?)Convert.ToInt32(filters["vnFactoryId"].ToString()) : null;
                sampleOrderStatusID = (filters.ContainsKey("sampleOrderStatusID")) && (filters["sampleOrderStatusID"] != null) && (!string.IsNullOrEmpty(filters["sampleOrderStatusID"].ToString())) ? (int?)Convert.ToInt32(filters["sampleOrderStatusID"].ToString()) : null;
                sampleProductStatusID = (filters.ContainsKey("sampleProductStatusID")) && (filters["sampleProductStatusID"] != null) && (!string.IsNullOrEmpty(filters["sampleProductStatusID"].ToString())) ? (int?)Convert.ToInt32(filters["sampleProductStatusID"].ToString()) : null;
                sampleOrderUD = (filters.ContainsKey("sampleOrderUD")) && (filters["sampleOrderUD"] != null) && (!string.IsNullOrEmpty(filters["sampleOrderUD"].ToString())) ? filters["sampleOrderUD"].ToString().Trim() : null;
                clientID = (filters.ContainsKey("clientID")) && (filters["clientID"] != null) && (!string.IsNullOrEmpty(filters["clientID"].ToString())) ? (int?)Convert.ToInt32(filters["clientID"].ToString()) : null;
                canEdit = (bool)filters["canEdit"];
                canRead = (bool)filters["canRead"];

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_SampleOrder(context.SupportMng_function_SampleOrder(sampleOrderUD, factoryID, sampleOrderStatusID, sampleProductStatusID, season, clientID, userId/*, canEdit, canRead*/).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExportBarcode(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            string fileName = string.Empty;

            try
            {
                ReportDataObject2 ds2 = new ReportDataObject2();
                SqlDataAdapter adap = new SqlDataAdapter();

                adap.SelectCommand = new SqlCommand("ReportMng_SampleOrder_function_PrintHangTag", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@sampleProductIDs", filters["keyIDs"].ToString().Trim());
                adap.SelectCommand.Parameters.AddWithValue("@qntBarcodes", filters["qntBarcode"].ToString().Trim());

                adap.TableMappings.Add("Table", "ReportMng_SampleOrder_HangTag_View");
                adap.Fill(ds2);

                ds2.AcceptChanges();

                foreach (var item in ds2.ReportMng_SampleOrder_HangTag_View.Where(o => !o.IsSampleProductUDNull()))
                {
                    item.BarCode = Library.Helper.QRCodeImageFile(item.SampleProductUD);
                }

                fileName = Library.Helper.CreateReportFileWithEPPlus(ds2, "SampleOrder_HangTag");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return fileName;
        }

        public object GetInitDataCustomFunction(int userID, bool canEdit, bool canRead, out Library.DTO.Notification notification)
        {
            // Refered coding
            // Define notification
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            // Define init data
            DTO.InitFormData data = new DTO.InitFormData()
            {
                Factories = new List<Support.DTO.Factory>(),
                SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>(),
                SampleProductStatuses = new List<Support.DTO.SampleProductStatus>(),
                Seasons = new List<Support.DTO.Season>()
            };

            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
                data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();

                using (var context = CreateContext())
                {
                    data.Factories = converter.DB2DTO_Factory(context.SampleOrderOverviewRpt_function_GetFactory(userID, canEdit, canRead).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public string GetExcelReportDataSampleProcess(string Season, string clientId, int? vnFactoryId, int? sampleProductStatusID, int? sampleOrderStatusID, int? sampleOrderID, bool canEdit, bool canRead, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataSampleProcess ds = new ReportDataSampleProcess();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("SampleOrderOverviewRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ClientID", clientId);

                // Add filter to export data
                // Can edit to see all items.
                // Can read only to see item in VN suggested factory of end-user manage
                adap.SelectCommand.Parameters.AddWithValue("@CanEdit", canEdit);
                adap.SelectCommand.Parameters.AddWithValue("@CanRead", canRead);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", userId);

                if (vnFactoryId.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@VNSuggestedFactoryID", vnFactoryId.Value);
                }
                if (sampleProductStatusID.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SampleProductStatusID", sampleProductStatusID.Value);
                }
                if (sampleOrderStatusID.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SampleOrderStatusID", sampleOrderStatusID.Value);
                }
                if (sampleOrderID.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SampleOrderID", sampleOrderID.Value);
                }
                adap.TableMappings.Add("Table", "SampleOrderOverviewRpt_ReportData_View");
                adap.TableMappings.Add("Table1", "SampleOrderOverviewRpt_ReportMinuteData_View");
                adap.TableMappings.Add("Table2", "SampleOrderOverviewRpt_ReportRemarkData_View");
                adap.TableMappings.Add("Table3", "SampleOrderOverviewRpt_ReportSampleOrder_View");
                adap.TableMappings.Add("Table4", "SampleOrderOverviewRpt_ReportQARemarkData_View");
                adap.Fill(ds);                
                ReportDataSampleProcess.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                hRow.Season = "";
                if (ds.SampleOrderOverviewRpt_ReportData_View.FirstOrDefault() != null)
                {
                    hRow.Season = ds.SampleOrderOverviewRpt_ReportData_View.FirstOrDefault().Season;
                }
                ds.ReportHeader.AddReportHeaderRow(hRow);

                // processing data and cleanup
                int sampleProductID = -1;
                string bigRemark = string.Empty;
                foreach (DAL.ReportDataSampleProcess.SampleOrderOverviewRpt_ReportData_ViewRow row in ds.SampleOrderOverviewRpt_ReportData_View)
                {
                    sampleProductID = row.SampleProductID;
                    bigRemark = string.Empty;
                    foreach (DAL.ReportDataSampleProcess.SampleOrderOverviewRpt_ReportQARemarkData_ViewRow mRow in ds.SampleOrderOverviewRpt_ReportQARemarkData_View.Where(o => o.SampleProductID == sampleProductID))
                    {
                        bigRemark += mRow.IndexNumber.ToString() + ". " + mRow.UpdatorName + "(" + mRow.DisplayUpdatedDate + ")";
                        bigRemark += Environment.NewLine + mRow.Remark + Environment.NewLine + Environment.NewLine;
                    }
                    row.LastMinuteTableRemark = bigRemark;

                    bigRemark = string.Empty;
                    foreach (DAL.ReportDataSampleProcess.SampleOrderOverviewRpt_ReportRemarkData_ViewRow rRow in ds.SampleOrderOverviewRpt_ReportRemarkData_View.Where(o => o.SampleProductID == sampleProductID))
                    {
                        bigRemark += rRow.IndexNumber.ToString() + ". " + rRow.UpdatorName + "(" + rRow.DisplayUpdatedDate + ")";
                        bigRemark += Environment.NewLine + rRow.Remark + Environment.NewLine + Environment.NewLine;
                    }
                    row.InternRemark = bigRemark;
                }
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportMinuteData_View");
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportRemarkData_View");
                ds.Tables.Remove("SampleOrderOverviewRpt_ReportQARemarkData_View");
                ds.AcceptChanges();

                // generate xml file
                return Library.Helper.CreateReportFileWithEPPlus(ds, "SampleProcessOverview", new List<string>() { "SampleOrderOverviewRpt_ReportData_View", "SampleOrderOverviewRpt_ReportSampleOrder_View", "ReportHeader" });
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex, ds);
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }
    }
}
