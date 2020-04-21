using System;
using System.Linq;

namespace Module.ProductBreakDownPALMng.DAL
{
    internal class DataFactory
    {
        private readonly DataConverter converter = new DataConverter();

        private ProductBreakDownPALEntities CreateContext()
        {
            return new ProductBreakDownPALEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductBreakDownPALModel"));
        }

        public object GetDataWithFilters(int userID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            totalRows = 0;
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int getTypeID = (filters.ContainsKey("getTypeSearch") && filters["getTypeSearch"] != null && !string.IsNullOrEmpty(filters["getTypeSearch"].ToString().Trim())) ? Convert.ToInt32(filters["getTypeSearch"].ToString().Trim()) : 0;

            DTO.SearchFormData data = new DTO.SearchFormData(getTypeID);

            try
            {
                using (var context = CreateContext())
                {
                    if (getTypeID == 1)
                    {
                        string defaultNameSearch = (filters.ContainsKey("defaultNameSearch") && filters["defaultNameSearch"] != null && !string.IsNullOrEmpty(filters["defaultNameSearch"].ToString().Trim())) ? filters["defaultNameSearch"].ToString().Trim() : null;
                        string defaultTypeSearch = (filters.ContainsKey("defaultTypeSearch") && filters["defaultTypeSearch"] != null && !string.IsNullOrEmpty(filters["defaultTypeSearch"].ToString().Trim())) ? filters["defaultTypeSearch"].ToString().Trim() : null;
                        string defaultUnitSearch = (filters.ContainsKey("defaultUnitSearch") && filters["defaultUnitSearch"] != null && !string.IsNullOrEmpty(filters["defaultUnitSearch"].ToString().Trim())) ? filters["defaultUnitSearch"].ToString().Trim() : null;

                        totalRows = context.ProductBreakDownPAL_function_ProductBreakDownDefaultCategoryPALSearchResult(defaultNameSearch, defaultTypeSearch, defaultUnitSearch, orderBy, orderDirection).Count();
                        data.ProductBreakDownDefaultCategoryPALSearchResult = converter.DB2DTO_DefaultPALSearchResult(context.ProductBreakDownPAL_function_ProductBreakDownDefaultCategoryPALSearchResult(defaultNameSearch, defaultTypeSearch, defaultUnitSearch, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    }

                    if (getTypeID == 2)
                    {
                        string modelSearch = (filters.ContainsKey("modelSearch") && filters["modelSearch"] != null && !string.IsNullOrEmpty(filters["modelSearch"].ToString())) ? filters["modelSearch"].ToString() : null;
                        string clientSearch = (filters.ContainsKey("clientSearch") && filters["clientSearch"] != null && !string.IsNullOrEmpty(filters["clientSearch"].ToString())) ? filters["clientSearch"].ToString() : null;
                        string productSearch = (filters.ContainsKey("productSearch") && filters["productSearch"] != null && !string.IsNullOrEmpty(filters["productSearch"].ToString())) ? filters["productSearch"].ToString() : null;
                        string sampleSearch = (filters.ContainsKey("sampleSearch") && filters["sampleSearch"] != null && !string.IsNullOrEmpty(filters["sampleSearch"].ToString())) ? filters["sampleSearch"].ToString() : null;
                        string currencySearch = (filters.ContainsKey("currencySearch") && filters["currencySearch"] != null && !string.IsNullOrEmpty(filters["currencySearch"].ToString())) ? filters["currencySearch"].ToString() : null;

                        totalRows = context.ProductBreakDownPAL_function_ProductBreakDownPALSearchResult(clientSearch, modelSearch, productSearch, sampleSearch, currencySearch, orderBy, orderDirection).Count();
                        data.ProductBreakDownPALSearchResult = converter.DB2DTO_ProductBreakDownPALSearchResult(context.ProductBreakDownPAL_function_ProductBreakDownPALSearchResult(clientSearch, modelSearch, productSearch, sampleSearch, currencySearch, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int getTypeID = (filters.ContainsKey("getTypeSearch") && filters["getTypeSearch"] != null && !string.IsNullOrEmpty(filters["getTypeSearch"].ToString().Trim())) ? Convert.ToInt32(filters["getTypeSearch"].ToString().Trim()) : 0;

            DTO.EditFormData data = new DTO.EditFormData(getTypeID);

            try
            {
                using (var context = CreateContext())
                {
                    int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? Convert.ToInt32(filters["id"].ToString().Trim()) : 0;

                    if (getTypeID == 1)
                    {
                        if (id > 0)
                        {
                            data.ProductBreakDownDefaultCategoryPAL = converter.DB2DTO_DefaultCategoryPAL(context.ProductBreakDownPAL_ProductBreakDownDefaultCategoryPAL_View.FirstOrDefault(o => o.ProductBreakDownDefaultCategoryID == id));
                        }
                    }

                    if (getTypeID == 2)
                    {
                        int? modelID = (filters.ContainsKey("model") && filters["model"] != null && !string.IsNullOrEmpty(filters["model"].ToString())) ? (int?)Convert.ToInt32(filters["model"].ToString()) : null;
                        int? sampleID = (filters.ContainsKey("sample") && filters["sample"] != null && !string.IsNullOrEmpty(filters["sample"].ToString())) ? (int?)Convert.ToInt32(filters["sample"].ToString()) : null;

                        if (id > 0)
                        {
                            data.ProductBreakDownPAL = converter.DB2DTO_ProductBreakDownPAL(context.ProductBreakDownPAL_ProductBreakDownPAL_View.FirstOrDefault(o => o.ProductBreakDownID == id));
                        }
                        else
                        {
                            if (modelID.HasValue)
                            {
                                var dbModel = context.ProductBreakDownPAL_ModelPAL_View.FirstOrDefault(o => o.ModelID == modelID.Value);

                                if (dbModel != null)
                                {
                                    data.ProductBreakDownPAL.ModelID = dbModel.ModelID;
                                    data.ProductBreakDownPAL.ModelUD = dbModel.ModelUD;
                                    data.ProductBreakDownPAL.ModelNM = dbModel.ModelNM;
                                }
                            }

                            if (sampleID.HasValue)
                            {
                                var dbSample = context.ProductBreakDownPAL_SampleProductPAL_View.FirstOrDefault(o => o.SampleProductID == sampleID.Value);

                                if (dbSample != null)
                                {
                                    data.ProductBreakDownPAL.ModelID = dbSample.ModelID;
                                    data.ProductBreakDownPAL.ModelUD = dbSample.ModelUD;
                                    data.ProductBreakDownPAL.ModelNM = dbSample.ModelNM;

                                    data.ProductBreakDownPAL.SampleProductID = dbSample.SampleProductID;
                                    data.ProductBreakDownPAL.SampleProductUD = dbSample.SampleProductUD;
                                    data.ProductBreakDownPAL.ArticleDescription = dbSample.ArticleDescription;
                                }
                            }

                            var masterExchangeRate = context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault();
                            if (masterExchangeRate != null)
                            {
                                data.ProductBreakDownPAL.ExchangeRate = masterExchangeRate.ExchangeRate;
                            }
                        }

                        if (string.IsNullOrEmpty(data.ProductBreakDownPAL.PriceDate))
                        {
                            data.ProductBreakDownPAL.PriceDate = DateTime.Now.ToString("dd/MM/yyyy");
                        }
                    }

                    if (getTypeID == 3)
                    {
                        int oldID = (filters.ContainsKey("oldID") && filters["oldID"] != null && !string.IsNullOrEmpty(filters["oldID"].ToString().Trim())) ? Convert.ToInt32(filters["oldID"].ToString().Trim()) : 0;

                        if (oldID == 0)
                        {
                            notification.Message = "Please select one Product BreakDown ready to create new data!";
                            notification.Type = Library.DTO.NotificationType.Error;

                            return data;
                        }

                        DTO.ProductBreakDownPALData oldItem = converter.DB2DTO_ProductBreakDownPAL(context.ProductBreakDownPAL_ProductBreakDownPAL_View.FirstOrDefault(o => o.ProductBreakDownID == oldID));
                        DTO.ProductBreakDownPALData newItem = new DTO.ProductBreakDownPALData();
                        converter.DTO2DTO_ProductBreakDownPAL(oldItem, ref newItem);

                        data.ProductBreakDownPAL = newItem;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetInitData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int getTypeID = (filters.ContainsKey("getTypeSearch") && filters["getTypeSearch"] != null && !string.IsNullOrEmpty(filters["getTypeSearch"].ToString().Trim())) ? Convert.ToInt32(filters["getTypeSearch"].ToString().Trim()) : 0;

            DTO.InitFormData data = new DTO.InitFormData(getTypeID);

            try
            {
                using (var context = CreateContext())
                {
                    if (getTypeID == 1)
                    {
                        data.SupportProductBreakDownCalculationTypePAL = converter.DB2DTO_CalculationTypePAL(context.ProductBreakDownPAL_ProductBreakDownCalculationTypePAL_View.ToList());
                        data.SupportProductBreakDownOptionPricePAL = converter.DB2DTO_OptionPricePAL(context.ProductBreakDownPAL_OptionToGetPricePAL_View.ToList());
                        data.SupportProductBreakDownOptionQuantityPAL = converter.DB2DTO_OptionQuantityPAL(context.ProductBreakDownPAL_OptionToGetQuantityPAL_View.ToList());
                    }

                    if (getTypeID == 2)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object UpdateData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int getTypeID = (filters.ContainsKey("getTypeSearch") && filters["getTypeSearch"] != null && !string.IsNullOrEmpty(filters["getTypeSearch"].ToString().Trim())) ? Convert.ToInt32(filters["getTypeSearch"].ToString().Trim()) : 0;

            DTO.EditFormData data = new DTO.EditFormData(getTypeID);

            try
            {
                using (var context = CreateContext())
                {
                    if (getTypeID == 1)
                    {
                        int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? Convert.ToInt32(filters["id"].ToString().Trim()) : 0;
                        DTO.ProductBreakDownDefaultCategoryPALData dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.ProductBreakDownDefaultCategoryPALData>();

                        ProductBreakDownDefaultCategoryPAL dbItem;

                        if (id == 0)
                        {
                            dbItem = new ProductBreakDownDefaultCategoryPAL();
                            context.ProductBreakDownDefaultCategoryPAL.Add(dbItem);
                        }
                        else
                        {
                            dbItem = context.ProductBreakDownDefaultCategoryPAL.FirstOrDefault(o => o.ProductBreakDownDefaultCategoryID == id);
                        }

                        if (dbItem == null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return data;
                        }

                        converter.DTO2DB_DefaultCategory(dtoItem, ref dbItem);

                        if (id == 0)
                        {
                            dbItem.CreatedBy = userID;
                            dbItem.CreatedDate = DateTime.Now;
                        }

                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = DateTime.Now;

                        context.SaveChanges();

                        filters["id"] = dbItem.ProductBreakDownDefaultCategoryID;

                        return GetData(userID, filters, out notification);
                    }

                    if (getTypeID == 2)
                    {
                        int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? Convert.ToInt32(filters["id"].ToString().Trim()) : 0;
                        DTO.ProductBreakDownPALData dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.ProductBreakDownPALData>();

                        ProductBreakDownPAL dbItem;

                        if (id == 0)
                        {
                            dbItem = new ProductBreakDownPAL();
                            context.ProductBreakDownPAL.Add(dbItem);
                        }
                        else
                        {
                            dbItem = context.ProductBreakDownPAL.FirstOrDefault(o => o.ProductBreakDownID == id);
                        }

                        if (dbItem == null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Can not find data";
                            return null;
                        }

                        //convert data
                        converter.DTO2DB_ProductBreakDownPAL2(dtoItem, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userID.ToString() + @"\", userID);

                        //create product for model default option
                        var modelDefaultOption = dtoItem.ModelDefaultOption;
                        if (modelDefaultOption != null)
                        {
                            if (!modelDefaultOption.ProductID.HasValue) //create product incase not exist 
                            {
                                if (!modelDefaultOption.ArticleCode.Contains("*"))
                                {
                                    Product dbProduct = new Product();
                                    context.Product.Add(dbProduct);
                                    dbItem.Product = dbProduct;

                                    dbProduct.ArticleCode = modelDefaultOption.ArticleCode;
                                    dbProduct.Description = modelDefaultOption.Description;
                                    dbProduct.ModelID = modelDefaultOption.ModelID;
                                    dbProduct.FrameMaterialID = modelDefaultOption.FrameMaterialID;
                                    dbProduct.FrameMaterialColorID = modelDefaultOption.FrameMaterialColorID;
                                    dbProduct.MaterialID = modelDefaultOption.MaterialID;
                                    dbProduct.MaterialTypeID = modelDefaultOption.MaterialTypeID;
                                    dbProduct.MaterialColorID = modelDefaultOption.MaterialColorID;
                                    dbProduct.SubMaterialID = modelDefaultOption.SubMaterialID;
                                    dbProduct.SubMaterialColorID = modelDefaultOption.SubMaterialColorID;
                                    dbProduct.SeatCushionID = modelDefaultOption.SeatCushionID;
                                    dbProduct.BackCushionID = modelDefaultOption.BackCushionID;
                                    dbProduct.CushionColorID = modelDefaultOption.CushionColorID;
                                    dbProduct.FSCTypeID = modelDefaultOption.FSCTypeID;
                                    dbProduct.FSCPercentID = modelDefaultOption.FSCPercentID;
                                }

                            }
                            else
                            {
                                dbItem.ProductID = modelDefaultOption.ProductID;
                            }
                        }

                        //tracking value
                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = DateTime.Now;
                        context.ProductBreakDownDetailPAL.Local.Where(o => o.ProductBreakDownCategoryTypePAL == null).ToList().ForEach(o => context.ProductBreakDownDetailPAL.Remove(o));
                        context.ProductBreakDownCategoryTypePAL.Local.Where(o => o.ProductBreakDownCategoryPAL == null).ToList().ForEach(o => context.ProductBreakDownCategoryTypePAL.Remove(o));
                        context.ProductBreakDownCategoryImagePAL.Local.Where(o => o.ProductBreakDownCategoryPAL == null).ToList().ForEach(o => context.ProductBreakDownCategoryImagePAL.Remove(o));
                        context.ProductBreakDownCategoryPAL.Local.Where(o => o.ProductBreakDownPAL == null).ToList().ForEach(o => context.ProductBreakDownCategoryPAL.Remove(o));

                        //save data
                        context.SaveChanges();

                        //reload data
                        filters["id"] = dbItem.ProductBreakDownID;
                        return GetData(userID, filters, out notification);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public bool DeleteData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int getTypeID = (filters.ContainsKey("getTypeSearch") && filters["getTypeSearch"] != null && !string.IsNullOrEmpty(filters["getTypeSearch"].ToString().Trim())) ? Convert.ToInt32(filters["getTypeSearch"].ToString().Trim()) : 0;

            bool isDeleted = false;

            try
            {
                int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? Convert.ToInt32(filters["id"].ToString().Trim()) : 0;

                using (var context = CreateContext())
                {
                    if (getTypeID == 1)
                    {
                        var dbItem = context.ProductBreakDownDefaultCategoryPAL.FirstOrDefault(o => o.ProductBreakDownDefaultCategoryID == id);

                        if (dbItem == null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Can not found data!";

                            return isDeleted;
                        }

                        context.ProductBreakDownDefaultCategoryPAL.Remove(dbItem);
                        context.SaveChanges();

                        isDeleted = true;
                    }

                    if (getTypeID == 2)
                    {
                        var dbItem = context.ProductBreakDownPAL.FirstOrDefault(o => o.ProductBreakDownID == id);

                        if (dbItem == null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Can not found data!";
                            return isDeleted;
                        }

                        context.ProductBreakDownPAL.Remove(dbItem);
                        context.SaveChanges();
                        isDeleted = true;
                    }
                }

                return isDeleted;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return isDeleted;
            }
        }

        public object GetModelWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            System.Collections.Generic.List<DTO.SupportProductBreakDownPALModelData> data = new System.Collections.Generic.List<DTO.SupportProductBreakDownPALModelData>();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_ModelPAL(context.ProductBreakDownPAL_function_SupportModelPAL(searchQuery, userID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetSampleWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SupportFormData data = new DTO.SupportFormData();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;
                string modelID = (filters.ContainsKey("modelID") && filters["modelID"] != null && !string.IsNullOrEmpty(filters["modelID"].ToString())) ? filters["modelID"].ToString() : null;

                using (var context = CreateContext())
                {
                    data.SupportSampleProduct = converter.DB2DTO_SamplePAL(context.ProductBreakDownPAL_function_SupportSampleProductPAL(searchQuery, modelID, userID).ToList());
                    return data.SupportSampleProduct;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetProductWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SupportFormData data = new DTO.SupportFormData();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;
                string modelID = (filters.ContainsKey("modelID") && filters["modelID"] != null && !string.IsNullOrEmpty(filters["modelID"].ToString())) ? filters["modelID"].ToString() : null;

                using (var context = CreateContext())
                {
                    data.SupportProduct = converter.DB2DTO_ProductPAL(context.ProductBreakDownPAL_function_SupportProductPAL(searchQuery, modelID, userID).ToList());
                    return data.SupportProduct;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetClientWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            System.Collections.Generic.List<DTO.SupportProductBreakDownPALClientData> data = new System.Collections.Generic.List<DTO.SupportProductBreakDownPALClientData>();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_ClientPAL(context.ProductBreakDownPAL_function_SupportClientPAL(searchQuery).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetProductionItemWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            System.Collections.Generic.List<DTO.SupportProductBreakDownPALProductionItemData> data = new System.Collections.Generic.List<DTO.SupportProductBreakDownPALProductionItemData>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;
                string priceDate = (filters.ContainsKey("priceDate") && filters["priceDate"] != null && !string.IsNullOrEmpty(filters["priceDate"].ToString())) ? filters["priceDate"].ToString() : null;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_ProductionItemPAL(context.ProductBreakDownPAL_function_SupportProductionItemPAL(searchQuery, priceDate, companyID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetProductBreakDownCategoryPAL(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            System.Collections.Generic.List<DTO.ProductBreakDownCategoryPALData> data = new System.Collections.Generic.List<DTO.ProductBreakDownCategoryPALData>();

            try
            {
                int typeCategoryID = (filters.ContainsKey("typeCategoryID") && filters["typeCategoryID"] != null && !string.IsNullOrEmpty(filters["typeCategoryID"].ToString())) ? Convert.ToInt32(filters["typeCategoryID"].ToString()) : 0;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_ProductBreakDownCategoryPAL(context.ProductBreakDownPAL_ProductBreakDownCategoryPAL_View.Where(o => o.ProductBreakDownCalculationTypeID == typeCategoryID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetCategoryPALWithFilters(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            System.Collections.Generic.List<DTO.SupportProductBreakDownPALCategoryData> data = new System.Collections.Generic.List<DTO.SupportProductBreakDownPALCategoryData>();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;
                int categoryTypeID = (filters.ContainsKey("categoryTypeID") && filters["categoryTypeID"] != null && !string.IsNullOrEmpty(filters["categoryTypeID"].ToString())) ? Convert.ToInt32(filters["categoryTypeID"].ToString()) : 0;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_CategoryPAL(context.ProductBreakDownPAL_SupportProductBreakDownCategoryPAL_View.Where(o => o.ProductBreakDownDefaultCategoryNM.Contains(searchQuery) && o.ProductBreakDownCalculationTypeID == categoryTypeID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object ApproveData(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int getTypeID = (filters.ContainsKey("getTypeSearch") && filters["getTypeSearch"] != null && !string.IsNullOrEmpty(filters["getTypeSearch"].ToString().Trim())) ? Convert.ToInt32(filters["getTypeSearch"].ToString().Trim()) : 0;

            DTO.EditFormData data = new DTO.EditFormData(getTypeID);

            try
            {
                int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? Convert.ToInt32(filters["id"].ToString().Trim()) : 0;

                using (var context = CreateContext())
                {
                    if (getTypeID == 1)
                    {
                    }

                    if (getTypeID == 2)
                    {
                        ProductBreakDownPAL dbItem = context.ProductBreakDownPAL.FirstOrDefault(o => o.ProductBreakDownID == id);

                        if (dbItem == null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Can not find data!";
                            return data;
                        }

                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = userID;
                        dbItem.ConfirmedDate = DateTime.Now;

                        context.SaveChanges();

                        filters["id"] = dbItem.ProductBreakDownID;
                        return GetData(userID, filters, out notification);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public DTO.ModelDefaultOption GetModelDefaultOption(int modelID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            DTO.ModelDefaultOption data = new DTO.ModelDefaultOption();
            try
            {
                using (var context = CreateContext())
                {
                    var modelDefaultOption = context.ProductBreakDownPAL_ModelDefaultOption_View.Where(o => o.ModelID == modelID).FirstOrDefault();
                    data = AutoMapper.Mapper.Map<ProductBreakDownPAL_ModelDefaultOption_View, DTO.ModelDefaultOption>(modelDefaultOption);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object ExportExcel(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                string client = (filters.ContainsKey("clientSearch") && filters["clientSearch"] != null && !string.IsNullOrEmpty(filters["clientSearch"].ToString().Trim())) ? filters["clientSearch"].ToString().Trim() : null;
                string model = (filters.ContainsKey("modelSearch") && filters["modelSearch"] != null && !string.IsNullOrEmpty(filters["modelSearch"].ToString().Trim())) ? filters["modelSearch"].ToString().Trim() : null;
                string product = (filters.ContainsKey("productSearch") && filters["productSearch"] != null && !string.IsNullOrEmpty(filters["productSearch"].ToString().Trim())) ? filters["productSearch"].ToString().Trim() : null;
                string sampleProduct = (filters.ContainsKey("sampleSearch") && filters["sampleSearch"] != null && !string.IsNullOrEmpty(filters["sampleSearch"].ToString().Trim())) ? filters["sampleSearch"].ToString().Trim() : null;

                ReportObjectData ds = new ReportObjectData();
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();

                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("ProductBreakDownPAL_function_ProductBreakDownPALSearchResult", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@clientSearch", client);
                adap.SelectCommand.Parameters.AddWithValue("@modelSearch", model);
                adap.SelectCommand.Parameters.AddWithValue("@productSearch", product);
                adap.SelectCommand.Parameters.AddWithValue("@sampleSearch", sampleProduct);

                adap.TableMappings.Add("Table", "ProductBreakDownView");
                adap.Fill(ds);

                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "Product_BreakDown_PAL");
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
