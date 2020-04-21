using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingPriceOverview2Rpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private PurchasingPriceOverview2RptEntities CreateContext()
        {
            return new PurchasingPriceOverview2RptEntities(Library.Helper.CreateEntityConnectionString("DAL.PurchasingPriceOverview2RptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.PurchasingPriceOverview>();
            totalRows = 0;

            //try to get data
            try
            {
                using (PurchasingPriceOverview2RptEntities context = CreateContext())
                {
                    string Season = null;
                    string ClientUD = null;
                    int? FactoryID = null;
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryID") && filters["FactoryID"] != null && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    totalRows = context.PurchasingPriceOverview2Rpt_function_GetHTMLReportData(FactoryID, ClientUD, Season, orderBy, orderDirection).Count();
                    data.Data = converter.DB2DTO_PurchasingPriceOverview(context.PurchasingPriceOverview2Rpt_function_GetHTMLReportData(FactoryID, ClientUD, Season, orderBy, orderDirection).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
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

        public DTO.SearchFilterData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();
            data.Factories = new List<Support.DTO.Factory>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.Factories = supportFactory.GetFactory().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetExcelReportData(int? factoryId, string clientUd, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PurchasingPriceOverview2Rpt_function_GetExcelReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                if (factoryId.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@FactoryID", factoryId);
                }
                if (!string.IsNullOrEmpty(clientUd))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@ClientUD", clientUd);
                }
                if (!string.IsNullOrEmpty(season))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                }
                adap.Fill(ds.PurchasingPriceOverview2Rpt_ExcelReportData_View);

                ReportDataObject.ReportHeaderRow row = ds.ReportHeader.NewReportHeaderRow();
                row.Season = season;
                ds.ReportHeader.AddReportHeaderRow(row);

                ds.AcceptChanges();

                // dev
                //Library.Helper.DevCreateReportXMLSource(ds, "MissingShippingInfo");

                // generate xml file
                return Library.Helper.CreateReportFileWithEPPlus(ds, "PurchasingPriceOverview2");
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
