using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.EurofarStockSyncData
{
    public class MainTask : NotificationBase.ITask
    {
        private DataConverter converter = new DataConverter();
        private string wexTokenURL = System.Configuration.ConfigurationManager.AppSettings["wex_api_url"].ToString() + "token/";
        private string wexAPIURL = System.Configuration.ConfigurationManager.AppSettings["wex_api_url"].ToString() + "eurofarstock/";
        private string wexAPIUserName = System.Configuration.ConfigurationManager.AppSettings["wex_api_username"].ToString();
        private string wexAPIPassword = System.Configuration.ConfigurationManager.AppSettings["wex_api_password"].ToString();
        private string eurofarstockURL = System.Configuration.ConfigurationManager.AppSettings["eurofarstock_api_url"].ToString();
        private string eurofarstockToken = System.Configuration.ConfigurationManager.AppSettings["eurofarstock_api_token"].ToString();
        private string eurofarstockDefaultStore = System.Configuration.ConfigurationManager.AppSettings["eurofarstock_api_store_code"].ToString();
        private List<DTO.EurofarStock.EAVItem> eurofarstockMaterial = new List<DTO.EurofarStock.EAVItem>();
        private string requestUD = string.Empty;

        public void ExecuteTask()
        {
            try
            {
                requestUD = System.Guid.NewGuid().ToString().Replace("-", "");
                eurofarstockMaterial = getEUMaterialList();
                List<DTO.ProductDTO> TSData;
                if (!getTilsoftData(out TSData))
                {
                    return;
                }
                List<DTO.EurofarStock.Item> EUData = getEUData();
                int index = 1;
                //Console.WriteLine("=== DISABLE ITEMS ==============================");
                //List<string> skus = TSData.Select(o => o.ArticleCode).ToList();
                //foreach (var euItem in EUData.Where(o => !skus.Contains(o.sku)))
                //{
                //    Console.Write("Disable item: " + euItem.sku + " ...");
                //    disableEUProduct(euItem);
                //    Console.WriteLine("Done!");
                //}
                //Console.WriteLine("=== /DISABLE ITEMS =============================");

                Console.WriteLine("=== UPDATING ITEMS =============================");
                index = 1;
                foreach (var tsItem in TSData)
                {
                    Console.Write("Updating item (" + index.ToString() + @"/" + TSData.Count().ToString() + "): " + tsItem.ArticleCode + " ... ");
                    if (EUData.FirstOrDefault(o => o.sku == tsItem.ArticleCode) != null)
                    {
                        // update product data
                        updateEUProduct(tsItem);
                    }
                    else
                    {
                        // insert product data
                        insertEUProduct(tsItem);
                    }

                    // update stock
                    //updateEUStock(tsItem.ArticleCode, tsItem.StockQnt);

                    index++;
                    Console.WriteLine("Done!");
                }
                Console.WriteLine("=== /UPDATING ITEMS ============================");
            }
            catch (Exception ex)
            {
                HandlingException(ex);
            }
        }

        public string GetDescription()
        {
            return "Eurofarstock sync data!";
        }
        
        // WEX
        private bool getWEXFreeToSaleData(out List<DTO.WEX.Article> data)
        {
            string jsonText = string.Empty;
            data = new List<DTO.WEX.Article>();
            try
            {
                using (WebClient client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["username"] = wexAPIUserName;
                    values["password"] = wexAPIPassword;
                    var response = client.UploadValues(wexTokenURL, values);
                    var responseString = Encoding.Default.GetString(response);

                    var definition = new { Token = "" };
                    var token = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(responseString, definition);
                    client.Headers.Add(HttpRequestHeader.Authorization, "JWT " + token.Token);
                    jsonText = client.DownloadString(wexAPIURL + "client/FREETOSALE");
                }

                if (!string.IsNullOrEmpty(jsonText))
                {
                    data = JArray.Parse(jsonText)[0].ToObject<List<DTO.WEX.Article>>();
                }
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception("Can not access WEX api! (Detail: " + Library.Helper.GetInnerException(ex).Message + ")");
                HandlingException(ex);
                return false;
            }
        }

        private bool getWEXData(out List<DTO.WEX.Article> data)
        {
            string jsonText = string.Empty;
            data = new List<DTO.WEX.Article>();
            try
            {
                using (WebClient client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["username"] = wexAPIUserName;
                    values["password"] = wexAPIPassword;
                    var response = client.UploadValues(wexTokenURL, values);
                    var responseString = Encoding.Default.GetString(response);

                    var definition = new { Token = "" };
                    var token = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(responseString, definition);
                    client.Headers.Add(HttpRequestHeader.Authorization, "JWT " + token.Token);
                    jsonText = client.DownloadString(wexAPIURL);
                }

                if (!string.IsNullOrEmpty(jsonText))
                {
                    data = JArray.Parse(jsonText)[0].ToObject<List<DTO.WEX.Article>>();
                }
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception("Can not access WEX api! (Detail: " + Library.Helper.GetInnerException(ex).Message + ")");
                HandlingException(ex);
                return false;
            }
        }

        // TILSOFT
        private bool getTilsoftData(out List<DTO.ProductDTO> data)
        {
            data = new List<DTO.ProductDTO>();
            try
            {
                //List<DTO.WEX.Article> jsonAllData;
                //List<DTO.WEX.Article> jsonData;
                //if (!getWEXData(out jsonAllData))
                //{
                //    return false;
                //}
                //if (getWEXFreeToSaleData(out jsonData))
                //{
                //    return false;
                //}

                using (EurofarStockEntities context = new EurofarStockEntities(Library.Helper.CreateEntityConnectionString("EurofarStockModel")))
                {
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        //context.Database.ExecuteSqlCommand("SELECT * FROM WexItem WITH (TABLOCKX, HOLDLOCK)");
                        //if (jsonAllData.Count() > 0)
                        //{
                        //    context.Database.ExecuteSqlCommand("DELETE FROM WexItem");
                        //    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('WexItem', RESEED, 1)");
                        //}

                        //try
                        //{
                        //    // delete wex item which is not in jsonAll
                        //    List<string> articleCodes = jsonAllData.Select(o => o.out_item_no).ToList();
                        //    context.WexItem.Where(o => !articleCodes.Contains(o.ArticleCode)).ToList().ForEach(o => context.WexItem.Remove(o));

                        //    foreach (DTO.WEX.Article wexItem in jsonAllData)
                        //    {
                        //        WexItem dbItem = context.WexItem.FirstOrDefault(o => o.ArticleCode == wexItem.out_item_no);
                        //        if (dbItem == null)
                        //        {
                        //            dbItem = new WexItem();
                        //            dbItem.ArticleCode = wexItem.out_item_no;
                        //            dbItem.SubEANCode = wexItem.out_barcode_base;
                        //            context.WexItem.Add(dbItem);
                        //        }
                        //        dbItem.StockQnt = 0;
                        //        dbItem.TotalStockQnt = Convert.ToInt32(wexItem.out_stock);
                        //        dbItem.IsEnabled = true;
                        //        if (jsonData.FirstOrDefault(o => o.out_item_no == wexItem.out_item_no) != null)
                        //        {
                        //            dbItem.StockQnt = Convert.ToInt32(jsonData.FirstOrDefault(o => o.out_item_no == wexItem.out_item_no).out_available_stock);
                        //        }

                        //        //if (wexItem.out_available_stock > 0)
                        //        //{
                        //        //    WexItem nItem = new WexItem()
                        //        //    {
                        //        //        ArticleCode = wexItem.out_item_no
                        //        //        , SubEANCode = wexItem.out_barcode_base
                        //        //        , StockQnt = 0
                        //        //        , TotalStockQnt = Convert.ToInt32(wexItem.out_stock)
                        //        //        , IsEnabled = true
                        //        //    };
                        //        //    if (jsonData.FirstOrDefault(o => o.out_item_no == wexItem.out_item_no) != null)
                        //        //    {
                        //        //        nItem.StockQnt = Convert.ToInt32(jsonData.FirstOrDefault(o => o.out_item_no == wexItem.out_item_no).out_available_stock);
                        //        //    }
                        //        //    context.WexItem.Add(nItem);
                        //        //}
                        //    }
                        //    context.SaveChanges();
                        //}
                        //catch (Exception ex)
                        //{
                        //    throw ex;
                        //}
                        //finally
                        //{
                        //    scope.Commit();
                        //}
                        // update price table
                        context.EurofarStockSync_function_UpdatePriceTable();

                        data = converter.DB2DTO_Product(context.WarehouseConnectMng_Product_View.ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                HandlingException(ex);
                data = new List<DTO.ProductDTO>();
                return false;
            }

            return true;
        }

        // EUROFARSTOCK
        private List<DTO.EurofarStock.Item> getEUData()
        {
            List<DTO.EurofarStock.Item> data = new List<DTO.EurofarStock.Item>();
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + eurofarstockToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    JObject obj;

                    // send request get all items
                    try
                    {
                        //obj = JObject.Parse(client.DownloadString(eurofarstockURL + "V1/products?searchCriteria="));
                        
                        // get only the simple product
                        obj = JObject.Parse(client.DownloadString(eurofarstockURL + "V1/products?searchCriteria[filter_groups][0][filters][0][field]=type_id&searchCriteria[filter_groups][0][filters][0][value]=simple"));
                    }
                    catch (WebException exception)
                    {
                        string responseText = string.Empty;
                        var responseStream = exception.Response?.GetResponseStream();
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseText = reader.ReadToEnd();
                            }
                        }
                        throw new Exception("getEUData: " + responseText);
                    }
                    data = ((JArray)obj.SelectToken("items")).ToObject<List<DTO.EurofarStock.Item>>();
                }
            }
            catch (Exception ex)
            {
                HandlingException(ex);
                data = new List<DTO.EurofarStock.Item>();
            }

            return data;
        }        

        private bool updateEUProduct(DTO.ProductDTO product)
        {
            DTO.EurofarStock.DataContract.Post.productContractForUpdate postedData = converter.Tilsoft2EurofarProductForUpdate(product);

            // assign material fs
            if (!string.IsNullOrEmpty(product.MaterialNM) && eurofarstockMaterial.Count(o => o.name.ToUpper() == product.MaterialNM.ToUpper()) > 0)
            {
                int materialID = eurofarstockMaterial.FirstOrDefault(o => o.name.ToUpper() == product.MaterialNM.ToUpper()).id;
                postedData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "material_fs", value = materialID.ToString() });
            }

            return updateEUProduct(postedData);
        }

        private bool insertEUProduct(DTO.ProductDTO product)
        {
            DTO.EurofarStock.DataContract.Post.productContract postedData = converter.Tilsoft2EurofarProduct(product);
            postedData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "tax_class_id", value = (int)DTO.EurofarStock.EUConstant.TaxClass });

            // assign category
            List<string> categories = new List<string>();
            if (!string.IsNullOrEmpty(product.ProductTypeNM))
            {
                switch (product.ProductTypeNM.ToUpper())
                {
                    case "SET":
                        categories.Add(((int)EUCategory.LoungeSets).ToString());
                        break;

                    case "CHAIR":
                        categories.Add(((int)EUCategory.Chairs).ToString());
                        break;

                    case "TABLE":
                        categories.Add(((int)EUCategory.Tables).ToString());
                        break;

                    case "BENCH":
                        categories.Add(((int)EUCategory.Bench).ToString());
                        break;

                    default:
                        categories.Add(((int)EUCategory.Accessories).ToString());
                        break;
                }
            }
            else
            {
                categories.Add(((int)EUCategory.Accessories).ToString());
            }
            postedData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "category_ids", value = categories });

            // assign material fs
            if (!string.IsNullOrEmpty(product.MaterialNM) && eurofarstockMaterial.Count(o=>o.name.ToUpper() == product.MaterialNM.ToUpper()) > 0)
            {
                int materialID = eurofarstockMaterial.FirstOrDefault(o => o.name.ToUpper() == product.MaterialNM.ToUpper()).id;
                postedData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "material_fs", value = materialID.ToString() });
            }
            
            return insertEUProduct(postedData);
        }       

        private List<DTO.EurofarStock.EAVItem> getEUMaterialList()
        {
            // hard coding to speed up the process - should get from api as well
            List<DTO.EurofarStock.EAVItem> data = new List<DTO.EurofarStock.EAVItem>();
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 61, name = "Wood " });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 62, name = "Metal" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 306, name = "Wicker" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 315, name = "Aluminium" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 316, name = "Magnesia" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 317, name = "Rope" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 318, name = "Sunbright" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 319, name = "Terrazzo" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 320, name = "Braided Outdoor" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 307, name = "Textilene" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 63, name = "Polyresin" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 321, name = "Bamboo" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 322, name = "Poly-Cement" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 323, name = "Cement/Pu Top" });
            //data.Add(new DTO.EurofarStock.EAVItem() { id = 324, name = "Polystone" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 5, name = "Wood" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 15, name = "Metal" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 4, name = "Wicker" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 12, name = "Aluminium" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 13, name = "Magnesia" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 16, name = "Rope" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 6, name = "Sunbright" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 8, name = "Terrazzo" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 11, name = "Braided Outdoor" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 7, name = "Textilene" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 9, name = "Polyresin" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 10, name = "Bamboo" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 14, name = "Poly-Cement" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 17, name = "Cement/Pu Top" });
            data.Add(new DTO.EurofarStock.EAVItem() { id = 18, name = "Polystone" });

            return data;
        }

        // CALL MAGENTO API 
        private bool updateEUProduct(DTO.EurofarStock.DataContract.Post.productContractForUpdate data)
        {
            // update 0 (default basic store) store
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + eurofarstockToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.UploadString(eurofarstockURL + "all/V1/products/"+data.product.sku, "PUT", Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }
            }
            catch (WebException exception)
            {
                string responseText = string.Empty;
                var responseStream = exception.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                HandlingException(new Exception("updateEUProduct[" + exception.Response.ResponseUri.ToString() + "]: (" + data.product.sku + ") - " + responseText));
                return false;
            }
            catch (Exception ex)
            {
                HandlingException(new Exception("updateEUProduct: (" + data.product.sku + ") - " + Library.Helper.GetInnerException(ex).Message));
                return false;
            }

            return true;
        }

        private bool insertEUProduct(DTO.EurofarStock.DataContract.Post.productContract data)
        {
            // update 0 (default basic store) store
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + eurofarstockToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.UploadString(eurofarstockURL + "V1/products", "POST", Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }
            }
            catch (WebException exception)
            {
                string responseText = string.Empty;
                var responseStream = exception.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                HandlingException(new Exception("insertEUProduct[" + exception.Response.ResponseUri.ToString() + "]: (" + data.product.sku + ") - " + responseText));
                return false;
            }
            catch (Exception ex)
            {
                HandlingException(new Exception("insertEUProduct: (" + data.product.sku + ") - " + Library.Helper.GetInnerException(ex).Message));
                return false;
            }

            return true;
        }

        private bool disableEUProduct(DTO.EurofarStock.Item item)
        {
            try
            {
                DTO.EurofarStock.DataContract.Post.productContractForUpdate postedData = new DTO.EurofarStock.DataContract.Post.productContractForUpdate();
                postedData.product = new DTO.EurofarStock.DataContract.Post.productContractDetailForUpdate()
                {
                    sku = item.sku
                    , attribute_set_id = item.attribute_set_id
                    , status = (int)DTO.EurofarStock.EUConstant.DisabledStatus
                    , visibility = (int)DTO.EurofarStock.EUConstant.Visibility
                    , type_id = item.type_id
                    , custom_attributes = new List<DTO.EurofarStock.DataContract.Post.productContractCustomAttribute>()
                };
                // update default store
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + eurofarstockToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    //client.UploadString(eurofarstockURL + "all/V1/products", "POST", Newtonsoft.Json.JsonConvert.SerializeObject(postedData));
                    client.UploadString(eurofarstockURL + "all/V1/products/" + postedData.product.sku, "PUT", Newtonsoft.Json.JsonConvert.SerializeObject(postedData));
                }
                return true;
            }
            catch (WebException exception)
            {
                string responseText = string.Empty;
                var responseStream = exception.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                HandlingException(new Exception("disableEUProduct: (" + item.sku + ") - " + responseText));
            }
            catch (Exception ex)
            {
                HandlingException(new Exception("disableEUProduct: (" + item.sku + ") - " + Library.Helper.GetInnerException(ex).Message));
                return false;
            }
            return false;
        }

        private bool updateEUStock(string sku, int stockQnt)
        {

            try
            {
                DTO.EurofarStock.DataContract.Put.stockContract postedData = new DTO.EurofarStock.DataContract.Put.stockContract();
                postedData.stockItem = new DTO.EurofarStock.DataContract.Put.stockContractDetail() { qty = stockQnt, is_in_stock = stockQnt > 0 ? 1 : 0 };
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + eurofarstockToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.UploadString(eurofarstockURL + "all/V1/products/" + sku + "/stockItems/1", "PUT", Newtonsoft.Json.JsonConvert.SerializeObject(postedData));                    
                }
                return true;
            }
            catch (WebException exception)
            {
                string responseText = string.Empty;
                var responseStream = exception.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                HandlingException(new Exception("updateEUStock: (" + sku + ") - " + responseText));
            }
            catch (Exception ex)
            {
                HandlingException(new Exception("updateEUStock: (" + sku + ") - " + Library.Helper.GetInnerException(ex).Message));
                return false;
            }
            return false;
        }

        private List<DTO.EurofarStock.Order> getEUOrder()
        {
            List<DTO.EurofarStock.Order> data = new List<DTO.EurofarStock.Order>();
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + eurofarstockToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    JObject obj = JObject.Parse(client.DownloadString(eurofarstockURL + "all/V1/orders?searchCriteria[filter_groups][0][filters][0][field]=status&searchCriteria[filter_groups][0][filters][0][value]=pending"));
                    data = ((JArray)obj.SelectToken("items")).ToObject<List<DTO.EurofarStock.Order>>();
                }
                //data
            }
            catch (WebException exception)
            {
                string responseText = string.Empty;
                var responseStream = exception.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                HandlingException(new Exception("getEUOrder: " + responseText));
            }
            catch (Exception ex)
            {
                HandlingException(new Exception("getEUOrder: " + Library.Helper.GetInnerException(ex).Message));
                data = new List<DTO.EurofarStock.Order>();
            }
            return data;
        }

        private DTO.EurofarStock.Customer getEUCustomer(int id)
        {
            DTO.EurofarStock.Customer data = new DTO.EurofarStock.Customer();
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + eurofarstockToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    JObject obj = JObject.Parse(client.DownloadString(eurofarstockURL + "all/V1/customers/" + id.ToString()));
                    data = obj.ToObject<DTO.EurofarStock.Customer>();
                }
                //data
            }
            catch (WebException exception)
            {
                string responseText = string.Empty;
                var responseStream = exception.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                HandlingException(new Exception("getEUCustomer: (" + id.ToString() + ") - " + responseText));
            }
            catch (Exception ex)
            {
                HandlingException(new Exception("getEUCustomer: (" + id.ToString() + ") - " + Library.Helper.GetInnerException(ex).Message));
                data = new DTO.EurofarStock.Customer();
            }
            return data;
        }

        private bool updateEUOrderStatusToProgress(int id)
        {
            try
            {
                DTO.EurofarStock.DataContract.Post.orderContract postedData = new DTO.EurofarStock.DataContract.Post.orderContract();
                postedData.entity = new DTO.EurofarStock.DataContract.Post.orderContractDetail() { entity_id = id };
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + eurofarstockToken);
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    client.UploadString(eurofarstockURL + "all/V1/order/" + id.ToString() + "/ship", "POST", Newtonsoft.Json.JsonConvert.SerializeObject(postedData));
                }
                return true;
            }
            catch (WebException exception)
            {
                string responseText = string.Empty;
                var responseStream = exception.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                HandlingException(new Exception("updateEUOrderStatusToProgress: (" + id.ToString() + ") - " + responseText));
            }
            catch (Exception ex)
            {
                HandlingException(new Exception("updateEUOrderStatusToProgress: (" + id.ToString() + ") - " + Library.Helper.GetInnerException(ex).Message));
                return false;
            }
            return false;
        }

        // HANDLING EXCEPTION
        private void HandlingException(Exception ex)
        {
            try
            {
                using (EurofarStockEntities context = new EurofarStockEntities(Library.Helper.CreateEntityConnectionString("EurofarStockModel")))
                {
                    SystemLogging logItem = new SystemLogging();
                    logItem.LogDate = System.DateTime.Now;
                    logItem.RequestUD = requestUD;
                    logItem.ProcessDescription = GetDescription();
                    logItem.DetailMessage = Library.Helper.GetInnerException(ex).Message;
                    context.SystemLogging.Add(logItem);
                    context.SaveChanges();
                }
            }
            catch { }
        }
    }

    //enum EUCategory
    //{
    //    LoungeSets = 327
    //    , Accessories = 325
    //    , Chairs = 324
    //    , Tables = 323
    //    , Bench = 328
    //}
    enum EUCategory
    {
        LoungeSets = 3
        , Accessories = 7
        , Chairs = 5
        , Tables = 4
        , Bench = 6
    }
}
