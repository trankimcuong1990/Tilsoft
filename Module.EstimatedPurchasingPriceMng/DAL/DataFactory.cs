using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EstimatedPurchasingPriceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private EstimatedPurchasingPriceMngEntities CreateContext()
        {
            return new EstimatedPurchasingPriceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.EstimatedPurchasingPriceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.EstimatedPurchasingPriceSearchResultDTO>();
            totalRows = 0;

            string Season = null;
            string Remark = null;
            string OfferUD = null;
            string FactoryUD = null;
            string PackagingMethodNM = null;
            string Description = null;
            int UserID = 0;

            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("OfferUD") && !string.IsNullOrEmpty(filters["OfferUD"].ToString()))
            {
                OfferUD = filters["OfferUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Remark") && !string.IsNullOrEmpty(filters["Remark"].ToString()))
            {
                Remark = filters["Remark"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("PackagingMethodNM") && !string.IsNullOrEmpty(filters["PackagingMethodNM"].ToString()))
            {
                PackagingMethodNM = filters["PackagingMethodNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("UserID") && filters["UserID"] != null && !string.IsNullOrEmpty(filters["UserID"].ToString()))
            {
                UserID = Convert.ToInt32(filters["UserID"].ToString());
            }

            //try to get data
            try
            {
                using (EstimatedPurchasingPriceMngEntities context = CreateContext())
                {
                    var result = context.EstimatedPurchasingPriceMng_function_SearchEstimatedPurchasingPrice(Season, Remark, FactoryUD, Description, PackagingMethodNM, OfferUD, UserID, orderBy, orderDirection).ToList();
                    totalRows = result.Count();
                    data.Data = converter.DB2DTO_EstimatedPurchasingPriceSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.EditFormData data = new DTO.EditFormData();
            //data.Data = new DTO.SampleOrder();
            //data.Data.SampleOrderStatusID = 1;
            //data.Data.SampleOrderStatusNM = "PENDING";
            //data.Data.SampleProducts = new List<DTO.SampleProduct>();
            //data.Data.SampleMonitors = new List<DTO.SampleMonitor>();

            //data.Seasons = new List<Support.DTO.Season>();
            //data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            //data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
            //data.Users = new List<Support.DTO.User>();
            //data.Factories = new List<Support.DTO.Factory>();
            //data.SampleRequestTypes = new List<Support.DTO.SampleRequestType>();
            //data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
            //data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();


            ////try to get data
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        if (id > 0)
            //        {
            //            data.Data = converter.DB2DTO_SampleOrder(context.Sample2Mng_SampleOrder_View
            //                .Include("Sample2Mng_SampleProduct_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleItemLocation_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View.Sample2Mng_SampleQARemarkImage_View")
            //                .Include("Sample2Mng_SampleMonitor_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductSubFactory_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
            //                .FirstOrDefault(o => o.SampleOrderID == id));
            //        }

            //        data.Seasons = supportFactory.GetSeason().ToList();
            //        data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
            //        data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
            //        data.Users = supportFactory.GetUsers().ToList();
            //        data.Factories = supportFactory.GetFactory().ToList();
            //        data.SampleRequestTypes = supportFactory.GetSampleRequestType().ToList();
            //        data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
            //        data.SampleProductStatuses.Remove(data.SampleProductStatuses.FirstOrDefault(o => o.SampleProductStatusID == 4)); // remove finished status
            //        data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //DTO.SampleOrder dtoOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleOrder>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleOrder dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new SampleOrder();
            //            context.SampleOrder.Add(dbItem);
            //            dbItem.CreatedBy = userId;
            //            dbItem.CreatedDate = DateTime.Now;
            //        }
            //        else
            //        {
            //            dbItem = context.SampleOrder.FirstOrDefault(o => o.SampleOrderID == id);
            //        }

            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Order not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.UpdatedBy = userId;
            //            dbItem.UpdatedDate = DateTime.Now;
            //            converter.DTO2DB_SampleOrder(dtoOrder, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
            //            context.SampleProductMinuteImage.Local.Where(o => o.SampleProductMinute == null).ToList().ForEach(o => context.SampleProductMinuteImage.Remove(o));
            //            context.SampleProductMinute.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductMinute.Remove(o));
            //            context.SampleRemarkImage.Local.Where(o => o.SampleRemark == null).ToList().ForEach(o => context.SampleRemarkImage.Remove(o));
            //            context.SampleRemark.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleRemark.Remove(o));
            //            context.SampleTechnicalDrawingFile.Local.Where(o => o.SampleTechnicalDrawing == null).ToList().ForEach(o => context.SampleTechnicalDrawingFile.Remove(o));
            //            context.SampleTechnicalDrawing.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleTechnicalDrawing.Remove(o));
            //            context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
            //            context.SampleProgress.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProgress.Remove(o));
            //            context.SampleReferenceImage.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleReferenceImage.Remove(o));
            //            context.SampleProduct.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleProduct.Remove(o));
            //            context.SampleMonitor.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleMonitor.Remove(o));
            //            context.SaveChanges();

            //            // generate order number
            //            if (id <= 0)
            //            {
            //                using (DbContextTransaction scope = context.Database.BeginTransaction())
            //                {
            //                    context.Database.ExecuteSqlCommand("SELECT * FROM SampleOrder WITH (TABLOCKX, HOLDLOCK)");

            //                    try
            //                    {
            //                        context.SaveChanges();
            //                        dbItem.SampleOrderUD = Library.Common.Helper.formatIndex(dbItem.SampleOrderID.ToString(), 8, "0");
            //                        context.SaveChanges();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        throw ex;
            //                    }
            //                    finally
            //                    {
            //                        scope.Commit();
            //                    }
            //                }
            //            }
            //            dtoItem = GetData(dbItem.SampleOrderID, out notification).Data;
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            //DTO.SupportFormData data = new DTO.SupportFormData();
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //}
            //return data;
        }

        public bool UpdateData(int userId, object dtoInputItems, out Library.DTO.Notification notification)
        {
            List<DTO.EstimatedPurchasingPriceSearchResultDTO> dtoItems = ((Newtonsoft.Json.Linq.JArray)dtoInputItems).ToObject<List<DTO.EstimatedPurchasingPriceSearchResultDTO>>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (EstimatedPurchasingPriceMngEntities context = CreateContext())
                {
                    EstimatedPurchasingPrice dbNewItem;
                    EstimatedPurchasingPriceHistory dbNewHistory;
                    foreach (DTO.EstimatedPurchasingPriceSearchResultDTO dtoItem in dtoItems.Where(o => o.EstimatedPurchasingPriceID <= 0).ToList())
                    {
                        dbNewItem = new EstimatedPurchasingPrice();
                        context.EstimatedPurchasingPrice.Add(dbNewItem);
                        converter.DTO2DB(dtoItem, ref dbNewItem);
                        dbNewItem.EstimatedPrice = dtoItem.NewEstimatedPrice;
                        dbNewItem.Remark = dtoItem.NewRemark;
                        dbNewItem.UpdatedBy = userId;
                        dbNewItem.UpdatedDate = DateTime.Now;

                        // add history
                        dbNewHistory = new EstimatedPurchasingPriceHistory
                        {
                            EstimatedPrice = dtoItem.NewEstimatedPrice,
                            Remark = dtoItem.NewRemark,
                            UpdatedBy = userId,
                            UpdatedDate = DateTime.Now
                        };
                        dbNewItem.EstimatedPurchasingPriceHistory.Add(dbNewHistory);
                    }

                    var toBeUpdatedDTOs = dtoItems.Where(o => o.EstimatedPurchasingPriceID > 0 && (o.NewEstimatedPrice.HasValue || !string.IsNullOrEmpty(o.NewRemark))).ToList();
                    var toBeUpdatedDTOIDS = toBeUpdatedDTOs.Select(o => o.EstimatedPurchasingPriceID).ToList();
                    foreach (EstimatedPurchasingPrice dbItem in context.EstimatedPurchasingPrice.Where(o => toBeUpdatedDTOIDS.Contains(o.EstimatedPurchasingPriceID)).ToList())
                    {
                        dbItem.EstimatedPrice = toBeUpdatedDTOs.FirstOrDefault(o => o.EstimatedPurchasingPriceID == dbItem.EstimatedPurchasingPriceID).NewEstimatedPrice;
                        dbItem.Remark = toBeUpdatedDTOs.FirstOrDefault(o => o.EstimatedPurchasingPriceID == dbItem.EstimatedPurchasingPriceID).NewRemark;
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        // add history
                        dbNewHistory = new EstimatedPurchasingPriceHistory
                        {
                            EstimatedPrice = dbItem.EstimatedPrice,
                            Remark = dbItem.Remark,
                            UpdatedBy = userId,
                            UpdatedDate = DateTime.Now
                        };
                        dbItem.EstimatedPurchasingPriceHistory.Add(dbNewHistory);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return false;
        }

        public DTO.SupportFormData GetFactories(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormData data = new DTO.SupportFormData();
            data.FactoryDTOs = new List<DTO.FactoryDTO>();

            try
            {
                using (EstimatedPurchasingPriceMngEntities context = CreateContext())
                {
                    data.FactoryDTOs = converter.DB2DTO_Factory(context.EstimatedPurchasingPriceMng_function_GetAuthorizedFactory(userId).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public List<DTO.HistoryDTO> GetHistoryData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.HistoryDTO> data = new List<DTO.HistoryDTO>();
            try
            {
                using (EstimatedPurchasingPriceMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_HistoryDTO(context.EstimatedPurchasingPriceMng_History_View.Where(o => o.EstimatedPurchasingPriceID == id).OrderByDescending(o => o.UpdatedDate).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}
