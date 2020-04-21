using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.ProductBreakDownMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private readonly DataConverter converter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            DTO.SearchFormData data = new DTO.SearchFormData();

            notification = new Notification() { Type = NotificationType.Success };
            totalRows = 0;

            try
            {
                int typeSearch = (filters.ContainsKey("typeSearch") && filters["typeSearch"] != null && !string.IsNullOrEmpty(filters["typeSearch"].ToString())) ? Convert.ToInt32(filters["typeSearch"].ToString()) : 0;

                switch (typeSearch)
                {
                    case 1: // Get data search ProductBreakDownDefaultCategory
                        data.DefaultData = new List<DTO.ProductBreakDownDefaultCategorySearchResultData>();

                        // Get value to filter
                        string name = (filters.ContainsKey("name") && filters["name"] != null && !string.IsNullOrEmpty(filters["name"].ToString())) ? filters["name"].ToString() : null;
                        string typeName = (filters.ContainsKey("typeName") && filters["typeName"] != null && !string.IsNullOrEmpty(filters["typeName"].ToString())) ? filters["typeName"].ToString() : null;
                        string unit = (filters.ContainsKey("unit") && filters["unit"] != null && !string.IsNullOrEmpty(filters["unit"].ToString())) ? filters["unit"].ToString() : null;

                        using (var context = CreateContext())
                        {
                            totalRows = context.ProductBreakDownDefaultCategoryMng_function_ProductBreakDownDefaultCategory(name, typeName, unit, orderBy, orderDirection).Count();
                            data.DefaultData = converter.DB2DTO_ProductBreakDownDefaultCategorySearchResult(context.ProductBreakDownDefaultCategoryMng_function_ProductBreakDownDefaultCategory(name, typeName, unit, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                        }

                        break;

                    case 2: // Get data search ProductBreakDown
                        data.MainData = new List<DTO.ProductBreakDownSearchResultData>();

                        string modelSearch = (filters.ContainsKey("modelSearch") && filters["modelSearch"] != null && !string.IsNullOrEmpty(filters["modelSearch"].ToString())) ? filters["modelSearch"].ToString() : null;
                        string clientSearch = (filters.ContainsKey("clientSearch") && filters["clientSearch"] != null && !string.IsNullOrEmpty(filters["clientSearch"].ToString())) ? filters["clientSearch"].ToString() : null;
                        string productSearch = (filters.ContainsKey("productSearch") && filters["productSearch"] != null && !string.IsNullOrEmpty(filters["productSearch"].ToString())) ? filters["productSearch"].ToString() : null;

                        using (var context = CreateContext())
                        {
                            totalRows = context.ProductBreakDownMng_function_ProductBreakDown(clientSearch, modelSearch, productSearch, orderBy, orderDirection).Count();
                            data.MainData = converter.DB2DTO_ProductBreakDownSearchResult(context.ProductBreakDownMng_function_ProductBreakDown(clientSearch, modelSearch, productSearch, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
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

        public DTO.EditFormData GetData(Hashtable filters, out Notification notification)
        {
            DTO.EditFormData data = new DTO.EditFormData();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                int typeGet = (filters.ContainsKey("typeGet") && filters["typeGet"] != null && !string.IsNullOrEmpty(filters["typeGet"].ToString())) ? Convert.ToInt32(filters["typeGet"].ToString()) : 0;

                switch (typeGet)
                {
                    case 1: // Get data ProductBreakDownDefaultCategory
                        int id_1 = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString())) ? Convert.ToInt32(filters["id"].ToString()) : 0;

                        data.DefaultData = new DTO.ProductBreakDownDefaultCategoryData();

                        using (var context = CreateContext())
                        {
                            if (id_1 > 0)
                            {
                                data.DefaultData = converter.DB2DTO_ProductBreakDownDefaultCategory(context.ProductBreakDownDefaultCategoryMng_ProductBreakDownDefaultCategory_View.FirstOrDefault(o => o.ProductBreakDownDefaultCategoryID == id_1));
                            }

                            data.CalculationType = converter.DB2DTO_ProductBreakDownCalculationType(context.SupportMng_ProductBreakDownCalculationType_View.ToList());
                            data.OptionalQuantity = converter.DB2DTO_OptionToGetQuantity(context.ProductBreakDownDefaultCategoryMng_OptionToGetQuantity_View.ToList());

                            // Issue 1360
                            data.OptionalPrice = converter.DB2DTO_OptionToGetPrice(context.ProductBreakDownDefaultCategoryMng_OptionToGetPrice_View.ToList());
                        }

                        break;

                    case 2: // Get data ProductBreakDown
                        int id_2 = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString())) ? Convert.ToInt32(filters["id"].ToString()) : 0;
                        int? modelID_2 = (filters.ContainsKey("model") && filters["model"] != null && !string.IsNullOrEmpty(filters["model"].ToString())) ? (int?)Convert.ToInt32(filters["model"].ToString()) : null;
                        int? sampleID_2 = (filters.ContainsKey("sample") && filters["sample"] != null && !string.IsNullOrEmpty(filters["sample"].ToString())) ? (int?)Convert.ToInt32(filters["sample"].ToString()) : null;

                        data.MainData = new DTO.ProductBreakDownData()
                        {
                            ProductBreakDownCategory = new List<DTO.ProductBreakDownCategoryData>(),
                            ProductBreakDownCategoryAmount = new List<DTO.ProductBreakDownCategoryAmountData>(),
                            ProductBreakDownCategoryPercent = new List<DTO.ProductBreakDownCategoryPercentData>(),
                        };

                        using (var context = CreateContext())
                        {
                            if (id_2 > 0)
                            {
                                data.MainData = converter.DB2DTO_ProductBreakDown(context.ProductBreakDownMng_ProductBreakDown_View.FirstOrDefault(o => o.ProductBreakDownID == id_2));
                            }
                            else
                            {
                                // 1. Move from Model
                                if (modelID_2.HasValue)
                                {
                                    var dbItem = context.ProductBreakDownMng_Model_View.FirstOrDefault(o => o.ModelID == modelID_2.Value);

                                    if (dbItem != null)
                                    {
                                        data.MainData.ModelID = dbItem.ModelID;
                                        data.MainData.ModelUD = dbItem.ModelUD;
                                        data.MainData.ModelNM = dbItem.ModelNM;
                                    }
                                }

                                // 2. Move from Sample
                                if (sampleID_2.HasValue)
                                {
                                    var dbItem = context.ProductBreakDownMng_SampleProduct_View.FirstOrDefault(o => o.SampleProductID == sampleID_2.Value);

                                    if (dbItem != null)
                                    {
                                        data.MainData.SampleProductID = dbItem.SampleProductID;
                                        data.MainData.SampleProductUD = dbItem.SampleProductUD;
                                        data.MainData.ArticleDescription = dbItem.ArticleDescription;

                                        data.MainData.ModelID = dbItem.ModelID;
                                        data.MainData.ModelUD = dbItem.ModelUD;
                                        data.MainData.ModelNM = dbItem.ModelNM;
                                    }
                                }
                            }
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object UpdateData(int userId, Hashtable filters, out Notification notification)
        {
            DTO.EditFormData data = new DTO.EditFormData();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                int typeUpdate = (filters.ContainsKey("typeGet") && filters["typeGet"] != null && !string.IsNullOrEmpty(filters["typeGet"].ToString())) ? Convert.ToInt32(filters["typeGet"].ToString()) : 0;
                int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString())) ? Convert.ToInt32(filters["id"].ToString()) : 0;

                switch (typeUpdate)
                {
                    case 1: // Update data ProductBreakDownDefaultCategory
                        DTO.ProductBreakDownDefaultCategoryData dtoItem_1 = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.ProductBreakDownDefaultCategoryData>();

                        using (var context = CreateContext())
                        {
                            ProductBreakDownDefaultCategory dbItem;

                            if (id == 0)
                            {
                                dbItem = new ProductBreakDownDefaultCategory();
                                context.ProductBreakDownDefaultCategory.Add(dbItem);
                            }
                            else
                            {
                                dbItem = context.ProductBreakDownDefaultCategory.FirstOrDefault(o => o.ProductBreakDownDefaultCategoryID == id);
                            }

                            // Check dbItem is null
                            if (dbItem == null)
                            {
                                notification.Type = NotificationType.Error;
                                notification.Message = "Can not find data";

                                return null;
                            }

                            // Mapping data
                            converter.DTO2DB_ProductBreakDownDefaultCategory(dtoItem_1, ref dbItem);

                            // Update createdBy, createdDate, updatedBy, updatedDate
                            if (id == 0)
                            {
                                dbItem.CreatedBy = userId;
                                dbItem.CreatedDate = DateTime.Now;
                            }
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;

                            // Update display order
                            //int displayOrder = context.ProductBreakDownDefaultCategory.Count();
                            //if (id == 0 && !dtoItem_1.DisplayOrder.HasValue)
                            //{
                            //    dbItem.DisplayOrder = displayOrder + 1;
                            //}
                            //else
                            //{
                            //    dbItem.DisplayOrder = displayOrder;
                            //}

                            context.SaveChanges();

                            filters["id"] = dbItem.ProductBreakDownDefaultCategoryID;
                        }

                        return GetData(filters, out notification).DefaultData;

                    case 2: // Update data ProductBreakDown
                        DTO.ProductBreakDownData dtoItem_2 = ((Newtonsoft.Json.Linq.JObject)filters["dataView"]).ToObject<DTO.ProductBreakDownData>();

                        using (var context = CreateContext())
                        {
                            ProductBreakDown dbItem;

                            if (id == 0)
                            {
                                dbItem = new ProductBreakDown();
                                context.ProductBreakDown.Add(dbItem);
                            }
                            else
                            {
                                dbItem = context.ProductBreakDown.FirstOrDefault(o => o.ProductBreakDownID == id);
                            }

                            // Check dbItem is null
                            if (dbItem == null)
                            {
                                notification.Type = NotificationType.Error;
                                notification.Message = "Can not find data";

                                return null;
                            }

                            converter.DTO2DB_ProductBreakDown(dtoItem_2, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;

                            context.ProductBreakDownDetail.Local.Where(o => o.ProductBreakDownCategoryType == null).ToList().ForEach(o => context.ProductBreakDownDetail.Remove(o));
                            context.ProductBreakDownCategoryType.Local.Where(o => o.ProductBreakDownCategory == null).ToList().ForEach(o => context.ProductBreakDownCategoryType.Remove(o));
                            context.ProductBreakDownCategoryImage.Local.Where(o => o.ProductBreakDownCategory == null).ToList().ForEach(o => context.ProductBreakDownCategoryImage.Remove(o));
                            context.ProductBreakDownCategory.Local.Where(o => o.ProductBreakDown == null).ToList().ForEach(o => context.ProductBreakDownCategory.Remove(o));

                            context.SaveChanges();

                            filters["id"] = dbItem.ProductBreakDownID;
                        }

                        return GetData(filters, out notification).MainData;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public bool DeleteData(int userId, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                int typeDelete = (filters.ContainsKey("typeGet") && filters["typeGet"] != null && !string.IsNullOrEmpty(filters["typeGet"].ToString())) ? Convert.ToInt32(filters["typeGet"].ToString()) : 0;
                int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString())) ? Convert.ToInt32(filters["id"].ToString()) : 0;

                using (var context = CreateContext())
                {
                    switch (typeDelete)
                    {
                        case 1:
                            var dbItem_1 = context.ProductBreakDownDefaultCategory.FirstOrDefault(o => o.ProductBreakDownDefaultCategoryID == id);

                            if (dbItem_1 == null)
                            {
                                notification.Type = NotificationType.Error;
                                notification.Message = "Can not find data";

                                return false;
                            }

                            context.ProductBreakDownDefaultCategory.Remove(dbItem_1);
                            context.SaveChanges();

                            return true;

                        case 2:
                            var dbItem_2 = context.ProductBreakDown.FirstOrDefault(o => o.ProductBreakDownID == id);

                            if (dbItem_2 == null)
                            {
                                notification.Type = NotificationType.Error;
                                notification.Message = "Can not find data";

                                return false;
                            }

                            context.ProductBreakDown.Remove(dbItem_2);
                            context.SaveChanges();

                            return true;

                        default:
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not find type to delete";

                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public object ApproveData(int userId, Hashtable filters, out Notification notification)
        {
            DTO.EditFormData data = new DTO.EditFormData();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                int typeApprove = (filters.ContainsKey("typeGet") && filters["typeGet"] != null && !string.IsNullOrEmpty(filters["typeGet"].ToString())) ? Convert.ToInt32(filters["typeGet"].ToString()) : 0;
                int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString())) ? Convert.ToInt32(filters["id"].ToString()) : 0;

                using (var context = CreateContext())
                {
                    switch (typeApprove)
                    {
                        case 1:
                            var dbItem_1 = context.ProductBreakDownDefaultCategory.FirstOrDefault(o => o.ProductBreakDownDefaultCategoryID == id);

                            if (dbItem_1 == null)
                            {
                                notification.Type = NotificationType.Error;
                                notification.Message = "Can not find data";

                                return false;
                            }

                            context.SaveChanges();

                            filters["id"] = dbItem_1.ProductBreakDownDefaultCategoryID;

                            return GetData(filters, out notification).DefaultData;

                        case 2:
                            var dbItem_2 = context.ProductBreakDown.FirstOrDefault(o => o.ProductBreakDownID == id);

                            if (dbItem_2 == null)
                            {
                                notification.Type = NotificationType.Error;
                                notification.Message = "Can not find data";

                                return false;
                            }

                            dbItem_2.IsConfirmed = true;
                            dbItem_2.ConfirmedBy = userId;
                            dbItem_2.ConfirmedDate = DateTime.Now;

                            context.SaveChanges();

                            filters["id"] = dbItem_2.ProductBreakDownID;

                            return GetData(filters, out notification).MainData;

                        default:
                            notification.Type = NotificationType.Error;
                            notification.Message = "Can not find type to approve";

                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public List<DTO.ModelSearchData> GetModelSearch(int userId, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            List<DTO.ModelSearchData> data = new List<DTO.ModelSearchData>();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_ModelSearch(context.ProductBreakDownMng_function_ModelQuickSearch(searchQuery, userId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.SampleProductSearchData> GetSampleProductSearch(int userId, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            List<DTO.SampleProductSearchData> data = new List<DTO.SampleProductSearchData>();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_SampleProductSearch(context.ProductBreakDownMng_function_SampleProductQuickSearch(searchQuery, userId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetSupportClient(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.SupportClientData> data = new List<DTO.SupportClientData>();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_SupportClient(context.ProductBreakDownMng_function_SupportClient(searchQuery).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.ProductBreakDownCategoryData> GetProductBreakDownCategory(int userId, Hashtable filters, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            List<DTO.ProductBreakDownCategoryData> data = new List<DTO.ProductBreakDownCategoryData>();

            try
            {
                int typeCategoryID = (filters.ContainsKey("typeCategoryID") && filters["typeCategoryID"] != null && !string.IsNullOrEmpty(filters["typeCategoryID"].ToString())) ? Convert.ToInt32(filters["typeCategoryID"].ToString()) : 0;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_ProductBreakDownCategory(context.ProductBreakDownMng_ProductBreakDownCategory_View.Where(o => o.ProductBreakDownCalculationTypeID == typeCategoryID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public ProductBreakDownMngEntities CreateContext()
        {
            return new ProductBreakDownMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductBreakDownMngModel"));
        }
    }
}
