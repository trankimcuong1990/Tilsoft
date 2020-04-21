using Library.Base;
using Module.SampleLocationMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using System.Collections;
using Library;
using Newtonsoft.Json.Linq;
using FrameworkSetting;

namespace Module.SampleLocationMng.DAL
{
    internal class DataFactory : DataFactory<SearchFormData, EditFormData>
    {
        protected DataConverter converter = new DataConverter();
        protected Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Notification();
            //notification.Type = NotificationType.Success;

            //SampleProductLocationData sampleProductLocationData = ((JObject)dtoItem).ToObject<SampleProductLocationData>();

            //return true;
            //try
            //{
            //    using (var context = CreateContext())
            //    {
            //        SampleProductLocation sampleProductLocation = new SampleProductLocation();

            //        if (sampleProductLocationData.SampleProductLocationID <= 0)
            //        {
            //            context.SampleProductLocation.Add(sampleProductLocation);
            //        }
            //        else
            //        {
            //            sampleProductLocation = context.SampleProductLocation.FirstOrDefault(o => o.SampleProductLocationID == sampleProductLocationData.SampleProductLocationID);
            //        }

            //        if (sampleProductLocation == null)
            //        {
            //            notification.Type = NotificationType.Error;
            //            notification.Message = "Data can not found!";

            //            return false;
            //        }

            //        converter.ConvertToDB_SampleProductLocation(sampleProductLocationData, ref sampleProductLocation);

            //        sampleProductLocation.UpdatedBy = userId;
            //        sampleProductLocation.UpdatedDate = DateTime.Now;

            //        context.SaveChanges();

            //        dtoItem = GetData(userId, id, out notification);
            //    }

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = NotificationType.Error;
            //    notification.Message = ex.Message;

            //    return false;
            //}
        }

        public SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            // Define.
            SearchFormData searchFormData = new SearchFormData
            {
                SearchData = new List<SampleProductLocationSearchResultData>(),
                Seasons = new List<Support.DTO.Season>(),
                Locations = new List<SupportFactoryLocationData>(),
                Factories = new List<Support.DTO.Factory>(),
                Clients = new List<Support.DTO.Client>(),
                Statuses = new List<Support.DTO.SampleProductStatus>()
            };

            notification = new Notification
            {
                Type = NotificationType.Success
            };

            totalRows = 0;

            // Main.
            try
            {
                string season = null;
                string sampleProductItemUD = null;
                string articleDescription = null;
                string factoryUD = null;
                string locationUD = null;
                string clientUD = null;
                int? quantity = null;
                int? statusID = null;

                if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString().Replace("'", "''")))
                {
                    season = filters["Season"].ToString().Trim();
                }

                if (filters.ContainsKey("Code") && filters["Code"] != null && !string.IsNullOrEmpty(filters["Code"].ToString().Replace("'", "''")))
                {
                    sampleProductItemUD = filters["Code"].ToString().Trim();
                }

                if (filters.ContainsKey("ArticleDescription") && filters["ArticleDescription"] != null && !string.IsNullOrEmpty(filters["ArticleDescription"].ToString().Replace("'", "''")))
                {
                    articleDescription = filters["ArticleDescription"].ToString().Trim();
                }

                if (filters.ContainsKey("Factory") && filters["Factory"] != null && !string.IsNullOrEmpty(filters["Factory"].ToString().Replace("'", "''")))
                {
                    factoryUD = filters["Factory"].ToString().Trim();
                }

                if (filters.ContainsKey("Location") && filters["Location"] != null && !string.IsNullOrEmpty(filters["Location"].ToString().Replace("'", "''")))
                {
                    locationUD = filters["Location"].ToString().Trim();
                }

                if (filters.ContainsKey("Client") && filters["Client"] != null && !string.IsNullOrEmpty(filters["Client"].ToString().Replace("'", "''")))
                {
                    clientUD = filters["Client"].ToString().Trim();
                }

                if (filters.ContainsKey("Quantity") && filters["Quantity"] != null && !string.IsNullOrEmpty(filters["Quantity"].ToString().Replace("'", "''")))
                {
                    quantity = Convert.ToInt32(filters["Quantity"].ToString().Trim());
                }

                if (filters.ContainsKey("Status") && filters["Status"] != null && !string.IsNullOrEmpty(filters["Status"].ToString().Replace("'", "''")))
                {
                    statusID = Convert.ToInt32(filters["Status"].ToString().Trim());
                }

                using (var context = CreateContext())
                {
                    totalRows = context.SampleProductLocationMng_function_SampleProductLocationSearchResult(season, sampleProductItemUD, articleDescription, factoryUD, locationUD, clientUD, quantity, statusID, userID, orderBy, orderDirection).Count();

                    var results = context.SampleProductLocationMng_function_SampleProductLocationSearchResult(season, sampleProductItemUD, articleDescription, factoryUD, locationUD, clientUD, quantity, statusID, userID, orderBy, orderDirection);
                    searchFormData.SearchData = converter.DB2DTO_SampleLocationSearch(results.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    var factoryResult = context.SampleProductLocationMng_function_SupportFactoryLocation(userID.ToString());
                    searchFormData.Locations = converter.DB2DTO_SampleFactory(factoryResult.ToList());
                }

                searchFormData.Seasons = supportFactory.GetSeason().ToList();
                searchFormData.Clients = supportFactory.GetClient().ToList();
                searchFormData.Factories = supportFactory.GetFactory(userID).ToList();
                searchFormData.Statuses = supportFactory.GetSampleProductStatus().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                searchFormData = null;
            }

            // Result.
            return searchFormData;
        }

        public EditFormData GetData(int userID, int id, out Notification notification)
        {
            throw new NotImplementedException();
            //EditFormData editFormData = new EditFormData();
            //editFormData.EditData = new SampleProductLocationData();
            //editFormData.SampleProductLocationDatas = new List<SampleProductLocationData>();
            //editFormData.SupportSampleOtherLocations = new List<SupportSampleOtherLocationData>();
            //editFormData.SupportFactoryLocations = new List<Support.DTO.Factory>();

            //notification = new Notification();
            //notification.Type = NotificationType.Success;

            //try
            //{
            //    using (var context = CreateContext())
            //    {
            //        var sampleProductLocationDatas = context.SampleProductLocationMng_SampleProductLocation_View.Where(o => o.SampleProductID == id).OrderByDescending(o => o.UpdatedDate).ToList();

            //        if (sampleProductLocationDatas == null || sampleProductLocationDatas.Count == 0)
            //        {
            //            var sampleProduct = context.SampleProductLocationMng_SampleProduct_View.FirstOrDefault(o => o.SampleProductID == id);

            //            if (sampleProduct != null)
            //            {
            //                editFormData.EditData.SampleProductID = sampleProduct.SampleProductID;
            //                editFormData.EditData.SampleProductUD = sampleProduct.SampleProductUD;
            //                editFormData.EditData.ArticleDescription = sampleProduct.ArticleDescription;
            //                editFormData.EditData.ThumbnailLocation = Setting.MediaThumbnailUrl + (string.IsNullOrEmpty(sampleProduct.ThumbnailLocation) ? "no-image.jpg" : sampleProduct.ThumbnailLocation);
            //                editFormData.EditData.FileLocation = Setting.MediaFullSizeUrl + (string.IsNullOrEmpty(sampleProduct.FileLocation) ? "no-image.jpg" : sampleProduct.FileLocation);
            //            }
            //        }
            //        else
            //        {
            //            editFormData.EditData = converter.ConvertToObject_SampleProductLocation(sampleProductLocationDatas[0]);
            //            editFormData.SampleProductLocationDatas = converter.ConvertToObject_SampleProductLocations(sampleProductLocationDatas);
            //        }

            //        editFormData.SupportSampleOtherLocations = converter.ConvertToObject_SampleOtherLocations(context.SupportMng_SampleOtherLocation_View.ToList());
            //        editFormData.SupportFactoryLocations = supportFactory.GetFactory(userID).ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = NotificationType.Error;
            //    notification.Message = ex.Message;
            //    editFormData = null;
            //}

            //return editFormData;
        }

        public DTO.EditFormData GetData(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            EditFormData data = new EditFormData()
            {
                SampleProducts = new List<SampleProductItemData>(),
                FactoryLocations = new List<Support.DTO.Factory>(),
                OtherLocations = new List<SupportSampleOtherLocationData>()
            };

            try
            {
                string sampleProductIDs = null;
                if (filters.ContainsKey("SampleProductIDs") && filters["SampleProductIDs"] != null && !string.IsNullOrEmpty(filters["SampleProductIDs"].ToString().Replace("'", "''")))
                {
                    sampleProductIDs = filters["SampleProductIDs"].ToString().Trim();
                }

                using (var context = CreateContext())
                {
                    var sampleProducts = context.SampleProductLocationMng_function_SampleProduct(sampleProductIDs);
                    data.SampleProducts = converter.DB2DTO_SampleProduct(sampleProducts.ToList());

                    data.FactoryLocations = supportFactory.GetFactory(userID).ToList();
                    data.OtherLocations = converter.DB2DTO_SampleOtheLocation(context.SupportMng_SampleOtherLocation_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return null;
            }

            return data;
        }

        public List<DTO.SampleProductLocationSearchResultData> UpdateData(int userID, Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            List<DTO.SampleProductLocationSearchResultData> data = new List<SampleProductLocationSearchResultData>();

            try
            {
                string sampleProductIDs = null;
                string factoryLocationIDs = null;
                string otherLocationIDs = null;
                string locationDates = null;
                string remarks = null;

                if (filters.ContainsKey("sampleProductIDs") && filters["sampleProductIDs"] != null && !string.IsNullOrEmpty(filters["sampleProductIDs"].ToString().Replace("'", "''")))
                {
                    sampleProductIDs = filters["sampleProductIDs"].ToString().Trim();
                }

                if (filters.ContainsKey("factoryLocationIDs") && filters["factoryLocationIDs"] != null && !string.IsNullOrEmpty(filters["factoryLocationIDs"].ToString().Replace("'", "''")))
                {
                    factoryLocationIDs = filters["factoryLocationIDs"].ToString().Trim();
                }

                if (filters.ContainsKey("otherLocationIDs") && filters["otherLocationIDs"] != null && !string.IsNullOrEmpty(filters["otherLocationIDs"].ToString().Replace("'", "''")))
                {
                    otherLocationIDs = filters["otherLocationIDs"].ToString().Trim();
                }

                if (filters.ContainsKey("locationDates") && filters["locationDates"] != null && !string.IsNullOrEmpty(filters["locationDates"].ToString().Replace("'", "''")))
                {
                    locationDates = filters["locationDates"].ToString().Trim();
                }

                if (filters.ContainsKey("remarks") && filters["remarks"] != null && !string.IsNullOrEmpty(filters["remarks"].ToString().Replace("'", "''")))
                {
                    remarks = filters["remarks"].ToString().Trim();
                }

                using (var context = CreateContext())
                {
                    var result = context.SampleProductLocationMng_UpdateData(sampleProductIDs, factoryLocationIDs, otherLocationIDs, locationDates, remarks, userID);
                    data = converter.DB2DTO_SampleLocationSearch(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return null;
            }

            return data;
        }

        private SampleLocationMngEntities CreateContext()
        {
            return new SampleLocationMngEntities(Helper.CreateEntityConnectionString("DAL.SampleLocationMngModel"));
        }
    }
}
