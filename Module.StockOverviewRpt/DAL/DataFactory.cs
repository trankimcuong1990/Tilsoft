using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.StockOverviewRpt.DAL
{
    public class DataFactory 
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        private StockOverViewRptEntities CreateContext()
        {
            return new StockOverViewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.StockOverViewRptModel"));
        }
        public DTO.SearchForm GetDataWithFilters(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchForm data = new DTO.SearchForm();
            data.StockDTOs = new List<DTO.StockDTO>();          
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string itemSource = "";
                int? productTypeID = null;
                string articleCode = "";
                string subEANCode = "";
                string description = "";

                if (filters.ContainsKey("itemSource") && !string.IsNullOrEmpty(filters["itemSource"].ToString()))
                {
                    itemSource = filters["itemSource"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("productTypeID") && filters["productTypeID"] != null && !string.IsNullOrEmpty(filters["productTypeID"].ToString()))
                {
                    productTypeID = Convert.ToInt32(filters["productTypeID"]);
                }

                if (filters.ContainsKey("articleCode") && !string.IsNullOrEmpty(filters["articleCode"].ToString()))
                {
                    articleCode = filters["articleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("subEANCode") && !string.IsNullOrEmpty(filters["subEANCode"].ToString()))
                {
                    subEANCode = filters["subEANCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                {
                    description = filters["description"].ToString().Replace("'", "''");
                }
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    totalRows = context.StockOverviewRpt_function_StockOverview(itemSource, productTypeID, articleCode, subEANCode, description).Count();
                    var results = context.StockOverviewRpt_function_StockOverview(itemSource, productTypeID, articleCode, subEANCode, description);
                    data.StockDTOs = converter.DB2DTO_GetStock(results.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
        public object GetInitData(int userID, out Notification notification)
        {
            DTO.SearchForm data = new DTO.SearchForm();
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    data.ProductTypes = converter.DB2DTO_GetProductType(context.SupportMng_ProductType_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public string GetExcel(Hashtable filters, out Library.DTO.Notification notification)
        {
            ReportDataObject ds = new ReportDataObject();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                string itemSource = "";
                int? productTypeID = null;
                string articleCode = "";
                string subEANCode = "";
                string description = "";

                if (filters.ContainsKey("itemSource") && !string.IsNullOrEmpty(filters["itemSource"].ToString()))
                {
                    itemSource = filters["itemSource"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("productTypeID") && filters["productTypeID"] != null && !string.IsNullOrEmpty(filters["productTypeID"].ToString()))
                {
                    productTypeID = Convert.ToInt32(filters["productTypeID"]);
                }

                if (filters.ContainsKey("articleCode") && !string.IsNullOrEmpty(filters["articleCode"].ToString()))
                {
                    articleCode = filters["articleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("subEANCode") && !string.IsNullOrEmpty(filters["subEANCode"].ToString()))
                {
                    subEANCode = filters["subEANCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                {
                    description = filters["description"].ToString().Replace("'", "''");
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("StockOverviewRpt_function_StockOverview", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ItemSource", itemSource);
                adap.SelectCommand.Parameters.AddWithValue("@ProductTypeID", productTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@ArticleCode", articleCode);
                adap.SelectCommand.Parameters.AddWithValue("@SubEANCode", subEANCode);
                adap.SelectCommand.Parameters.AddWithValue("@Description", description);

                adap.TableMappings.Add("Table", "StockOverViewRpt");
                adap.Fill(ds);

                // generate xml file
                return Library.Helper.CreateReportFileWithEPPlus2(ds, "StockOverView");
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
                return string.Empty;
            }
        }
    }
}
