using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StandardPurchasingPriceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private StandardPurchasingPriceMngEntities CreateContext()
        {
            return new StandardPurchasingPriceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.StandardPurchasingPriceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.StandardPurchasingPriceSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (StandardPurchasingPriceMngEntities context = CreateContext())
                {
                    string Season = null;
                    string ArticleCode = null;
                    string Description = null;
                    string FactoryUD = null;
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        Description = filters["Description"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
                    {
                        FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
                    }
                    totalRows = context.StandardPurchasingPriceMng_function_Search(Season, ArticleCode, Description, FactoryUD, orderBy, orderDirection).Count();
                    var result = context.StandardPurchasingPriceMng_function_Search(Season, ArticleCode, Description, FactoryUD, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_StandardPurchasingPriceSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
        // CUSTOM FUNCTIONS
        //
        public string GetExcelReportData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();
            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("StandardPurchasingPriceMng_function_GetReportData", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (string filterKey in filters.Keys)
                {
                    if (filters[filterKey] != null && !string.IsNullOrEmpty(filters[filterKey].ToString()))
                    {
                        adap.SelectCommand.Parameters.AddWithValue("@" + filterKey, filters[filterKey].ToString());
                    }
                }                
                adap.Fill(ds.StandardPurchasingPriceMng_ReportData_View);

                ds.AcceptChanges();
                return Library.Helper.CreateReportFileWithEPPlus(ds, "StandardPurchasingPrice", new List<string>() { "StandardPurchasingPriceMng_ReportData_View" });
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return string.Empty;
            }
        }
    }
}
