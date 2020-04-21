using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Newtonsoft.Json.Linq;
using Module.StandardCostPriceOverviewRpt.DTO;
using System.Collections;

namespace Module.StandardCostPriceOverviewRpt.DAL
{
    public class DataFactory : Library.Base.DataFactory<SearchFormData, EditFormData>
    {
        StandardCostPriceOverviewRptEntities CreatContext()
        {
            return new StandardCostPriceOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.StandardCostPriceOverviewRptModel"));
        }

        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        DataConverter converter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public EditFormData GetData(Hashtable filters, out Notification notification)
        {
            EditFormData editData = new EditFormData();
            editData.Data = new List<StandardCostPriceDetail>();

            notification = new Notification() { Type = NotificationType.Success };

            string articleCode = filters["articleCode"]?.ToString().Replace("'", "''");

            using (var context = CreatContext())
            {
                var dbItem = context.StandardCostPriceOverviewRpt_Detail_View.Where(o => o.ArticleCode == articleCode).ToList();
                editData.Data = converter.DB2DTO_StandardCostPrice(dbItem);
            }

            return editData;
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };

            SearchFormData data = new SearchFormData()
            {
                Data = new List<DTO.ProductSearchResultDto>(),
                //Sdata = new List<Support.DTO.Season>()  //Call Season
            };


            try
            {
                using (var context = CreatContext())
                {
                    string season = filters["Season"]?.ToString().Replace("'", "''");
                    string articleCode = filters["ArticleCode"]?.ToString().Replace("'", "''");
                    string description = filters["Description"]?.ToString().Replace("'", "''");
                    string factoryUD = filters["FactoryUD"]?.ToString().Replace("'", "''");

                    context.StandardCostPriceOverviewRpt_function_UpdateDefaultFactory();
                    totalRows = context.StandardCostPriceOverviewRpt_function_StandardCostPriceOverviewRptSearchResult(season, articleCode, description, factoryUD, orderBy, orderDirection).Count();
                    data.Data = this.converter.BD2DTO_StandardCostPriceOverviewRptSearch(context.StandardCostPriceOverviewRpt_function_StandardCostPriceOverviewRptSearchResult(season, articleCode, description, factoryUD, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    //data.Sdata = supportFactory.GetSeason().ToList(); //Call Season
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };

            }
            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public string ExportExcel(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            string ArticleCode = null;
            string Description = null;
            string Season = null;



            if (filters.ContainsKey("ArticleCode"))
            {
                ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description"))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }

            try
            {

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("StandardCostPriceOverviewRpt_function_ExportProduct", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ArticleCode", ArticleCode);
                adap.SelectCommand.Parameters.AddWithValue("@Description", Description);

                adap.TableMappings.Add("Table", "StandardCostPriceOverviewRpt_ProductSearchResult_View");
                adap.Fill(ds);

                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "StandardCostPriceOverviewRpt");
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

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public object SetDefaultFactory(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            DTO.ProductItem data = new DTO.ProductItem();
            notification = new Notification() { Type = NotificationType.Success };

            int factoryID = (filters.ContainsKey("factoryID") && filters["factoryID"] != null && !string.IsNullOrEmpty(filters["factoryID"].ToString())) ? Convert.ToInt32(filters["factoryID"].ToString()) : 0;
            int productID = (filters.ContainsKey("productID") && filters["productID"] != null && !string.IsNullOrEmpty(filters["productID"].ToString())) ? Convert.ToInt32(filters["productID"].ToString()) : 0;
            try
            {
                using (var context = CreatContext())
                {
                    if(productID != 0)
                    {
                        var dbItem = context.Product.FirstOrDefault(o => o.ProductID == productID);

                        if (factoryID != 0)
                        {
                            dbItem.DefaultFactoryID = factoryID;
                        }
                        context.SaveChanges();

                        data = converter.DB2DTO_Product(dbItem);
                    }
                    
                    //var dbItemReturn = context.StandardCostPriceOverviewRpt_Detail_View.FirstOrDefault(o => o.ProductID == productID);
                    //return AutoMapper.Mapper.Map<StandardCostPriceOverviewRpt_Detail_View, DTO.StandardCostPriceDetail>(dbItemReturn);
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
            }

            return data;
        }

        public bool UpdateProductPrice(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.ProductPrice> dtoPrice = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.ProductPrice>>();
            notification = new Notification() { Type = NotificationType.Success };                  
            try
            {
                using (var context = CreatContext())
                {
                    foreach (DTO.ProductPrice items in dtoPrice)
                    {
                        var dbItem = context.Product.FirstOrDefault(o => o.ProductID == items.ProductID);
                        if (dbItem != null)
                        {
                            dbItem.CostPrice = items.CostPrice;
                        }
                        else
                        {
                            Product itemPrice = new Product();
                            context.Product.Add(itemPrice);
                            itemPrice.CostPrice = items.CostPrice;
                            itemPrice.ProductID = items.ProductID;
                        }
                    }
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return true;
            }
        }
    }
}

