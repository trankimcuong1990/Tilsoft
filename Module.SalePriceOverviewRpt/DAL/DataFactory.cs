using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Net;

namespace Module.SalePriceOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private SalePriceOverviewRptEntities CreateContext()
        {
            return new SalePriceOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.SalePriceOverviewRptModel"));
        }

        public string GetExcelReportData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                string Client = string.Empty;
                //string Offer = string.Empty;
                string Season = string.Empty;
                //string ArticleCode = string.Empty;
                string ModelNM = string.Empty;

                //if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                //{
                //    FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                //}
                if (filters.ContainsKey("Client")) Client = filters["Client"].ToString();
                //if (filters.ContainsKey("Offer")) Offer = filters["Offer"].ToString();
                if (filters.ContainsKey("Season")) Season = filters["Season"].ToString();
                //if (filters.ContainsKey("ArticleCode")) ArticleCode = filters["ArticleCode"].ToString();
                if (filters.ContainsKey("ModelNM")) ModelNM = filters["ModelNM"].ToString();

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("SalePriceOverviewRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ClientUD", Client);
                //adap.SelectCommand.Parameters.AddWithValue("@Offer", Offer);
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                //adap.SelectCommand.Parameters.AddWithValue("@ArticleCode", ArticleCode);
                adap.SelectCommand.Parameters.AddWithValue("@ModelNM", ModelNM);
                adap.Fill(ds.SalePriceOverviewRpt_function_GetReportData);

                return Library.Helper.CreateReportFileWithEPPlus(ds, "SalePriceOverview");
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

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.SalePriceOverview>();
            totalRows = 0;

            //try to get data
            try
            {
                using (SalePriceOverviewRptEntities context = CreateContext())
                {
                    string Client = string.Empty;
                    //string Offer = string.Empty;
                    string Season = string.Empty;
                    //string ArticleCode = string.Empty;
                    string ModelNM = string.Empty;

                    if (filters.ContainsKey("Client")) Client = filters["Client"].ToString();
                    //if (filters.ContainsKey("Offer")) Offer = filters["Offer"].ToString();
                    if (filters.ContainsKey("Season")) Season = filters["Season"].ToString();
                    //if (filters.ContainsKey("ArticleCode")) ArticleCode = filters["ArticleCode"].ToString();
                    if (filters.ContainsKey("ModelNM")) ModelNM = filters["ModelNM"].ToString();

                    totalRows = context.SalePriceOverviewRpt_function_GetReportData(Client, Season, ModelNM, orderBy, orderDirection).Count();
                    var result = context.SalePriceOverviewRpt_function_GetReportData(Client, Season, ModelNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SalePriceOverviewSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
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
    }
}
