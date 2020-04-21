using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WEXStockOverviewRpt.DAL
{
    internal class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        private WEXStockOverviewRptEntities CreateContext()
        {
            var context = new WEXStockOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.WEXStockOverviewRptModel"));
            context.Database.CommandTimeout = 180;
            return context;
        }

        public DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.ProductSearchResultDTO>();
            totalRows = 0;

            //try to get data
            try
            {
                using (WEXStockOverviewRptEntities context = CreateContext())
                {
                    string ArticleCode = null;
                    string SubEANCode = null;
                    string Description = null;
                    string ProductTypeNM = null;
                    bool? NoImage = false;
                    int? ItemSourceID = null;

                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("SubEANCode") && !string.IsNullOrEmpty(filters["SubEANCode"].ToString()))
                    {
                        SubEANCode = filters["SubEANCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        Description = filters["Description"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ProductTypeNM") && !string.IsNullOrEmpty(filters["ProductTypeNM"].ToString()))
                    {
                        ProductTypeNM = filters["ProductTypeNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("NoImage") && filters["NoImage"] != null && !string.IsNullOrEmpty(filters["NoImage"].ToString()))
                    {
                        NoImage = (filters["NoImage"].ToString() == "true") ? true : false;
                    }
                    if (filters.ContainsKey("ItemSourceID") && filters["ItemSourceID"] != null && !string.IsNullOrEmpty(filters["ItemSourceID"].ToString()))
                    {
                        ItemSourceID = Convert.ToInt32(filters["ItemSourceID"]);
                    }
                    totalRows = context.WEXStockOverviewRpt_function_SearchProduct(ArticleCode, SubEANCode, Description, ProductTypeNM, NoImage, ItemSourceID, orderBy, orderDirection).Count();
                    var result = context.WEXStockOverviewRpt_function_SearchProduct(ArticleCode, SubEANCode, Description, ProductTypeNM, NoImage, ItemSourceID, orderBy, orderDirection);
                    //data.Data = converter.DB2DTO_ProductSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.Data = converter.DB2DTO_ProductSearchResultList(result.ToList());
                    if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_WEX_VVP))
                    {
                        data.Data.ForEach(o => o.VVPPrice = null);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string ExportExcel(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("WEXStockOverviewRpt_function_ExportProduct", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@ArticleCode", filters["ArticleCode"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("SubEANCode") && !string.IsNullOrEmpty(filters["SubEANCode"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@SubEANCode", filters["SubEANCode"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@Description", filters["Description"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("ProductTypeNM") && !string.IsNullOrEmpty(filters["ProductTypeNM"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@ProductTypeNM", filters["ProductTypeNM"].ToString().Replace("'", "''"));
                }
                if (filters.ContainsKey("NoImage") && filters["NoImage"] != null && !string.IsNullOrEmpty(filters["NoImage"].ToString()))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@NoImage", filters["NoImage"].ToString().Replace("'", "''"));
                }
                adap.Fill(ds.WEXStockOverviewRpt_ExcelData_View);
                if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_WEX_VVP))
                {
                    foreach (ReportDataObject.WEXStockOverviewRpt_ExcelData_ViewRow mRow in ds.WEXStockOverviewRpt_ExcelData_View)
                    {
                        mRow.SetVVPPriceNull();
                    }
                }
                ds.AcceptChanges();

                //// prepare cache image
                //foreach (ReportDataObject.WEXStockOverviewRpt_ProductSearchResult_ViewRow mRow in ds.WEXStockOverviewRpt_ProductSearchResult_View)
                //{
                //    try
                //    {
                //        fwFactory.CreateImageCache(mRow.ImageFile, 120, 120, false);
                //    }
                //    catch { }
                //}

                return Library.Helper.CreateReportFileWithEPPlus(ds, "WEXStockOverview", new List<string>() { "WEXStockOverviewRpt_ExcelData_View" });
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

        public bool UpdateSellingPrice(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PriceDTO> dataItems = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.PriceDTO>>();
            try
            {
                using (var context = CreateContext())
                {
                    foreach (DTO.PriceDTO pItem in dataItems)
                    {
                        ProductSellingPrice price = context.ProductSellingPrice.FirstOrDefault(o => o.ProductID == pItem.ProductID);
                        if (price != null)
                        {
                            price.SellingPrice = pItem.Price;
                        }

                    }
                    context.SaveChanges();

                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }


        public bool UpdatePurchasingPrice(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_WEX_VVP))
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "You dont have access to this VVP data!";
                return false;
            }


            List<DTO.PriceDTO> dataItems = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.PriceDTO>>();

            try
            {
                using (var context = CreateContext())
                {
                    foreach (DTO.PriceDTO pItem in dataItems)
                    {
                        ProductPurchasingPrice pPrice = context.ProductPurchasingPrice.FirstOrDefault(o => o.ProductID == pItem.ProductID);
                        if (pPrice != null)
                        {
                            pPrice.PurchasingPrice = pItem.Price;
                        }
                        //else
                        //{
                        //    pPrice = new ProductPurchasingPrice();
                        //    context.ProductPurchasingPrice.Add(pPrice);

                        //    pPrice.PurchasingPrice = pItem.Price;
                        //}
                    }
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public bool UpdateObsolete(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ObsoleteDTO> dataItems = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.ObsoleteDTO>>();

            try
            {
                using (var context = CreateContext())
                {
                    foreach (DTO.ObsoleteDTO items in dataItems)
                    {
                        ProductPurchasingPrice dbItem = context.ProductPurchasingPrice.FirstOrDefault(o => o.ProductID == items.ProductID);
                        if (dbItem != null)
                        {
                            dbItem.Obsolete = items.Obsolete;
                        }
                        else
                        {
                            ProductPurchasingPrice itemPrice = new ProductPurchasingPrice();
                            context.ProductPurchasingPrice.Add(itemPrice);
                            itemPrice.Obsolete = items.Obsolete;
                            itemPrice.ProductID = items.ProductID;
                        }
                    }
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public bool UpdateValueObsolescence(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            List<DTO.ValueObsolescenceDTO> dataItems = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.ValueObsolescenceDTO>>();
            try
            {
                using (var context = CreateContext())
                {
                    foreach (DTO.ValueObsolescenceDTO dtoItem in dataItems)
                    {
                        ProductPurchasingPrice dbItem = context.ProductPurchasingPrice.FirstOrDefault(o => o.ProductID == dtoItem.ProductID);
                        if (dbItem != null)
                        {
                            dbItem.ValueObsolescence = dtoItem.ValueObsolescence;
                        }
                        else
                        {
                            ProductPurchasingPrice pItem = new ProductPurchasingPrice();
                            context.ProductPurchasingPrice.Add(pItem);
                            pItem.ProductID = dtoItem.ProductID;
                            pItem.ValueObsolescence = dtoItem.ValueObsolescence;
                        }
                    }
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
        }

        public bool UpdateProductPrice(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.ProductPrice> dtoPrice = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.ProductPrice>>();
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
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

        public bool UpdateEnableStatus(int userId, object dtoJson, out Library.DTO.Notification notification)
        {
            List<DTO.ProductSearchResultDTO> dtoItems = ((Newtonsoft.Json.Linq.JArray)dtoJson).ToObject<List<DTO.ProductSearchResultDTO>>();
            notification = new Notification { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    foreach (DTO.ProductSearchResultDTO dtoItem in dtoItems)
                    {
                        if (dtoItem.WexItemID > 0)
                        {
                            if (context.WexItem.FirstOrDefault(o => o.WexItemID == dtoItem.WexItemID) != null)
                            {
                                context.WexItem.FirstOrDefault(o => o.WexItemID == dtoItem.WexItemID).IsEnabled = dtoItem.IsEnabled;
                            }
                        }
                        else
                        {
                            if (context.OtherSystemItem.FirstOrDefault(o => o.OtherSystemItemID == dtoItem.OtherSystemItemID) != null)
                            {
                                context.OtherSystemItem.FirstOrDefault(o => o.OtherSystemItemID == dtoItem.OtherSystemItemID).IsEnabled = dtoItem.IsEnabled;
                            }
                        }
                    }
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return false;
        }
    }
}
