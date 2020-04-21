using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;

namespace Module.LPOverview.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private LPOverviewEntities CreateContext()
        {
            return new LPOverviewEntities(Library.Helper.CreateEntityConnectionString("DAL.LPOverviewModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }


        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            //Details
            editFormData.Data = new DTO.LabelingPackaging();
            editFormData.Data.LabelingPackagingDetails = new List<DTO.LabelingPackagingDetail>();
            editFormData.Data.LabelingPackagingSparepartDetails = new List<DTO.LabelingPackagingSparepartDetail>();
            editFormData.Data.LabelingPackagingRemarks = new List<DTO.LabelingPackagingRemark>();

            try
            {
                using (LPOverviewEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        LabelingPackagingMng_LabelingPackaging_View dbItem;
                        dbItem = context.LabelingPackagingMng_LabelingPackaging_View.FirstOrDefault(o => o.LabelingPackagingID == id);

                        //check permission on factory
                        int FactoryID = Convert.ToInt32(dbItem.FactoryID);
                        if (fwFactory.CheckFactoryPermission(userId, FactoryID) == 0)
                        {
                            throw new Exception("You do not have access permission on this factory");
                        }
                        editFormData.Data = converter.DB2DTO_LabelingPackaging(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.LabelingPackaging();
                        editFormData.Data.LabelingPackagingDetails = new List<DTO.LabelingPackagingDetail>();
                        editFormData.Data.LabelingPackagingSparepartDetails = new List<DTO.LabelingPackagingSparepartDetail>();
                        editFormData.Data.LabelingPackagingRemarks = new List<DTO.LabelingPackagingRemark>();
                        editFormData.Data.LabelingPackagingOtherFiles = new List<DTO.LabelingPackagingOtherFile>();
                    }
                    //editFormData.Factories = support_factory.GetFactory(userId);
                    return editFormData;
                }
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
                return editFormData;
            }
        }

        public DTO.SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            int? UserID = userID;
            string Season = null;
            int? SaleID = null;
            string ProformaInvoiceNo = null;
            string FactoryUD = null;
            string ClientUD = null;
            string ApprovedName = null;


            if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ApprovedName") && !string.IsNullOrEmpty(filters["ApprovedName"].ToString()))
            {
                ApprovedName = filters["ApprovedName"].ToString().Replace("'", "''");
            }

            try
            {
                using (LPOverviewEntities context = CreateContext())
                {
                    totalRows = context.LPOverview_function_SearchLabelingPackaging(orderBy, orderDirection, UserID, Season, SaleID, ProformaInvoiceNo, FactoryUD, ClientUD, ApprovedName).Count();
                    var result = context.LPOverview_function_SearchLabelingPackaging(orderBy, orderDirection, UserID, Season, SaleID, ProformaInvoiceNo, FactoryUD, ClientUD, ApprovedName);
                    searchFormData.Data = converter.DB2DTO_LPOverviewSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return searchFormData;
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
                return searchFormData;
            }
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();

            try
            {
                data.Seasons = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelData(int labelingPackagingID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject2 ds = new ReportDataObject2();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("LabelingPackagingMng_function_GetExcelData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@LabelingPackagingID", labelingPackagingID);

                adap.TableMappings.Add("Table", "LabelingPackaging");
                adap.TableMappings.Add("Table1", "LabelingPackagingDetail");
                adap.TableMappings.Add("Table2", "LabelingPackagingOtherFile");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "LabelingPackagingOverview");
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

        public string GetExcelDataNL(int labelingPackagingID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject2 ds = new ReportDataObject2();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("LabelingPackagingMng_function_GetExcelData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@LabelingPackagingID", labelingPackagingID);

                adap.TableMappings.Add("Table", "LabelingPackaging");
                adap.TableMappings.Add("Table1", "LabelingPackagingDetail");
                adap.TableMappings.Add("Table2", "LabelingPackagingOtherFile");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "LabelingPackagingOverviewNL");
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

        public string GetExcelDataV2(int labelingPackagingID, string listLPDetails, string listLPSparepartDetails, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject2 ds = new ReportDataObject2();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("LabelingPackagingMng_function_GetExcelDataProduct", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@LabelingPackagingID", labelingPackagingID);
                adap.SelectCommand.Parameters.AddWithValue("@ListLPDetails", listLPDetails);
                adap.SelectCommand.Parameters.AddWithValue("@ListLPSparepartDetails", listLPSparepartDetails);

                adap.TableMappings.Add("Table", "LabelingPackaging");
                adap.TableMappings.Add("Table1", "LabelingPackagingDetail");
                adap.TableMappings.Add("Table2", "LabelingPackagingOtherFile");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "LabelingPackagingOverviewV2");
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

        public bool ReviseData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (LPOverviewEntities context = CreateContext())
                {
                    LabelingPackaging dbItem = context.LabelingPackaging.FirstOrDefault(o => o.LabelingPackagingID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "LP not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.LPStatusID = 1;
                        context.SaveChanges();

                        return true;
                    }
                }
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
                return false;
            }
        }

        // Get overview excel report
        public string GetExcelReportData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                int? UserID = userID;
                string Season = null;
                int? SaleID = null;
                string ProformaInvoiceNo = null;
                string FactoryUD = null;
                string ClientUD = null;
                string ApprovedName = null;
                string SortedBy = null;
                string SortedDirection = null;


                if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                {
                    Season = filters["Season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("SaleID") && !string.IsNullOrEmpty(filters["SaleID"].ToString()))
                {
                    SaleID = Convert.ToInt32(filters["SaleID"].ToString());
                }
                if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                {
                    ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
                {
                    FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ApprovedName") && !string.IsNullOrEmpty(filters["ApprovedName"].ToString()))
                {
                    ApprovedName = filters["ApprovedName"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("SortedBy") && !string.IsNullOrEmpty(filters["SortedBy"].ToString()))
                {
                    SortedBy = filters["SortedBy"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("SortedDirection") && !string.IsNullOrEmpty(filters["SortedDirection"].ToString()))
                {
                    SortedDirection = filters["SortedDirection"].ToString().Replace("'", "''");
                }

                if (SortedBy == "LDS")
                {
                    SortedBy = "LDS1";
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("LPOverview_function_ReportExcel", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@SaleID", SaleID);
                adap.SelectCommand.Parameters.AddWithValue("@ProformaInvoiceNo", ProformaInvoiceNo);
                adap.SelectCommand.Parameters.AddWithValue("@ClientUD", ClientUD);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", FactoryUD);
                adap.SelectCommand.Parameters.AddWithValue("@ApprovedName", ApprovedName);
                adap.SelectCommand.Parameters.AddWithValue("@SortedBy", SortedBy);
                adap.SelectCommand.Parameters.AddWithValue("@SortedDirection", SortedDirection);
                adap.Fill(ds.LPOverview_function_ReportExcel);

                return Library.Helper.CreateReportFileWithEPPlus(ds, "LPOverview");
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
