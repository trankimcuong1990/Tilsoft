using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVTPurchasingPriceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private AVTPurchasingPriceMngEntities CreateContext()
        {
            //return new AVTPurchasingPriceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.AVTPurchasingPriceMngModel"));
            AVTPurchasingPriceMngEntities mContext = new AVTPurchasingPriceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.AVTPurchasingPriceMngModel"));
            mContext.Database.CommandTimeout = 300;
            return mContext;
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryQuotationSearchResultDTO>();
            data.Exchange = 1;
            data.avtBreakdownCategoryOptionDTOs = new List<DTO.AVTBreakdownCategoryOptionDTO>();

            totalRows = 0;

            DateTime tmpDate;
            CultureInfo cInfo = new CultureInfo("vi-VN");

            string Season = null;
            string ClientNM = null;
            string Description = null;
            string FactoryUD = null;
            string ProformaInvoiceNo = null;
            string LDSFrom = null;
            string LDSTo = null;
            int? ItemTypeID = null;
            int? StatusID = null;
            int? QuotationOfferDirectionID = null;
            int? BusinessDataStatusID = null;
            int? ShippedStatus = null;
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
            {
                ClientNM = filters["ClientNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("LDSFrom") && filters["LDSFrom"] != null && !string.IsNullOrEmpty(filters["LDSFrom"].ToString()))
            {
                if (DateTime.TryParse(filters["LDSFrom"].ToString(), cInfo, DateTimeStyles.None, out tmpDate))
                {
                    LDSFrom = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            if (filters.ContainsKey("LDSTo") && filters["LDSTo"] != null && !string.IsNullOrEmpty(filters["LDSTo"].ToString()))
            {
                if (DateTime.TryParse(filters["LDSTo"].ToString(), cInfo, DateTimeStyles.None, out tmpDate))
                {
                    LDSTo = tmpDate.ToString("yyyy-MM-dd");
                }
            }
            if (filters.ContainsKey("ItemTypeID") && filters["ItemTypeID"] != null && !string.IsNullOrEmpty(filters["ItemTypeID"].ToString()))
            {
                ItemTypeID = Convert.ToInt32(filters["ItemTypeID"].ToString());
            }
            if (filters.ContainsKey("StatusID") && filters["StatusID"] != null && !string.IsNullOrEmpty(filters["StatusID"].ToString()))
            {
                StatusID = Convert.ToInt32(filters["StatusID"].ToString());
            }
            if (filters.ContainsKey("QuotationOfferDirectionID") && filters["QuotationOfferDirectionID"] != null && !string.IsNullOrEmpty(filters["QuotationOfferDirectionID"].ToString()))
            {
                QuotationOfferDirectionID = Convert.ToInt32(filters["QuotationOfferDirectionID"].ToString());
            }
            if (filters.ContainsKey("BusinessDataStatusID") && filters["BusinessDataStatusID"] != null && !string.IsNullOrEmpty(filters["BusinessDataStatusID"].ToString()))
            {
                BusinessDataStatusID = Convert.ToInt32(filters["BusinessDataStatusID"].ToString());
            }
            if (filters.ContainsKey("ShippedStatus") && filters["ShippedStatus"] != null && !string.IsNullOrEmpty(filters["ShippedStatus"].ToString()))
            {
                ShippedStatus = Convert.ToInt32(filters["ShippedStatus"].ToString());
            }
            //try to get data
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    try
                    {
                        data.Exchange = context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault().ExchangeRate.Value;
                    }
                    catch { }

                    // get search data
                    if (pageIndex > 1)
                    {
                        totalRows = -1;
                    }
                    else
                    {
                        // get conclusion by season
                        var dbConclusion = context.AVTPurchasingPriceMng_function_GetQuotaionConclusion(Season).FirstOrDefault();
                        data.TotalItem = dbConclusion.TotalItem.Value;
                        data.TotalConfirmedItem = dbConclusion.TotalConfirmedItem.Value;
                        data.TotalWaitForEurofar = dbConclusion.TotalWaitEurofar.Value;
                        data.TotalContainer = dbConclusion.TotalContainer.Value;
                        data.TotalConfirmedContainer = dbConclusion.TotalConfirmedContainer.Value;
                        data.TotalContainerWaitForEurofar = dbConclusion.TotalContainerWaitEurofar.Value;

                        totalRows = context.AVTPurchasingPriceMng_function_GetTotalRowFound(Season, ClientNM, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, ProformaInvoiceNo, BusinessDataStatusID, ShippedStatus, LDSFrom, LDSTo).FirstOrDefault().Value;
                    }
                    var resultSet = context.AVTPurchasingPriceMng_function_SearchFactoryQuotation(Season, ClientNM, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, ProformaInvoiceNo, BusinessDataStatusID, ShippedStatus, LDSFrom, LDSTo, orderBy, orderDirection, pageSize, pageIndex).ToList();
                    data.Data = converter.DB2DTO_FactoryQuotationSearchResult(resultSet.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            //if(data.Data.Count() > 0)
            //{
            //    try
            //    {
            //        using (var context = CreateContext())
            //        {
            //            List<int?> modelIDs = data.Data.Select(o => o.ModelID).ToList();
            //            var avtBreakdownCategoryOptionDTOs = context.AVTPurchasingPriceMng_BreakdownCategoryOption_View.Where(o => modelIDs.Contains(o.ModelID)).ToList();
            //            data.avtBreakdownCategoryOptionDTOs = AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_BreakdownCategoryOption_View>, List<DTO.AVTBreakdownCategoryOptionDTO>>(avtBreakdownCategoryOptionDTOs);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        notification.Type = Library.DTO.NotificationType.Error;
            //        notification.Message = Library.Helper.GetInnerException(ex).Message;
            //    }
            //}
            //if (data.avtBreakdownCategoryOptionDTOs.Count() > 0)
            //{
            //    try
            //    {
            //        using (var contex = CreateContext())
            //        {
            //            List<int?> BreakdownIDs = data.avtBreakdownCategoryOptionDTOs.Select(o => o.BreakdownID).Distinct().ToList();
            //            var avtBreakdownSeasonSpecPercent = contex.AVTPurchasingPriceMng_Breakdown_View.Where(o => BreakdownIDs.Contains(o.BreakdownID)).ToList();
            //            data.avtSeasonSpecPercentDTOs = AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_Breakdown_View>, List<DTO.AVTSeasonSpecPercentDTO>>(avtBreakdownSeasonSpecPercent);

            //            var avtBreakdownManagementFee = contex.AVTPurchasingPriceMng_BreakdownManagementFee_View.Where(o => BreakdownIDs.Contains(o.BreakdownID)).ToList();
            //            data.avtBreakdownManagementFeeDTOs = AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_BreakdownManagementFee_View>, List<DTO.AVTBreakdownManagementFeeDTO>>(avtBreakdownManagementFee);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        notification.Type = Library.DTO.NotificationType.Error;
            //        notification.Message = Library.Helper.GetInnerException(ex).Message;
            //    }
            //}
            
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

        public DTO.SupportFormData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormData data = new DTO.SupportFormData();
            data.QuotationStatusDTOs = new List<DTO.QuotationStatusDTO>();

            //try to get data
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    data.QuotationStatusDTOs = converter.DB2DTO_QuotationStatus(context.SupportMng_QuotationStatus_View.ToList());
                    data.avtSupportBreakdownCategoryDTOs = converter.DB2DTO_AVTSupportBreakdownCategory(context.AVTPurchasingPriceMng_BreakdownCategory_View.ToList());

                    //
                    // ADD CUSTOM STATUS AS REQUESTED - HARD CODE
                    //
                    List<int> toBeRemoveItems = new List<int>();
                    foreach (var item in data.QuotationStatusDTOs)
                    {
                        if (item.QuotationStatusID != 1 && item.QuotationStatusID != 3)
                        {
                            toBeRemoveItems.Add(item.QuotationStatusID);
                        }
                    }
                    foreach (int id in toBeRemoveItems)
                    {
                        data.QuotationStatusDTOs.Remove(data.QuotationStatusDTOs.FirstOrDefault(o => o.QuotationStatusID == id));
                    }
                    data.QuotationStatusDTOs.Add(new DTO.QuotationStatusDTO { QuotationStatusID = -1, QuotationStatusNM = "ITEMS COSTPRICE" });
                    data.QuotationStatusDTOs.Add(new DTO.QuotationStatusDTO { QuotationStatusID = -2, QuotationStatusNM = "ITEMS NO COSTPRICE" });
                }

                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    string currency = "USD";
                    var masterExchangeRate = context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, currency).FirstOrDefault();
                    data.Exchange = masterExchangeRate.ExchangeRate;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool UpdateData(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            List<DTO.FactoryQuotationSearchResultDTO> dtoOffers = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.FactoryQuotationSearchResultDTO>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    // get similar item: same offerlineid, same factoryid
                    int[] existingIDs = dtoOffers.Where(o => o.NewTargetPrice.HasValue).Select(o => o.QuotationDetailID).ToArray();
                    List<DTO.FactoryQuotationSearchResultDTO> additionalItems = new List<DTO.FactoryQuotationSearchResultDTO>();
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.NewTargetPrice.HasValue && o.OfferLineID.HasValue && o.StatusID != 3).ToList())
                    {
                        additionalItems = converter.DB2DTO_FactoryQuotationSearchResult(context.AVTPurchasingPriceMng_FactoryQuotationSearchResult_View.Where(o =>
                            !existingIDs.Contains(o.QuotationDetailID)
                            && o.OfferLineID.HasValue
                            && o.OfferLineID == dtoItem.OfferLineID
                            && o.FactoryID == o.FactoryID
                            && o.StatusID != 3).ToList());

                        if (additionalItems.Count() > 0)
                        {
                            additionalItems.ForEach(o => { o.NewTargetPrice = dtoItem.NewTargetPrice.Value; o.NewTargetComment = dtoItem.NewTargetComment; });
                            dtoOffers.AddRange(additionalItems);
                        }
                    }

                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.NewTargetPrice.HasValue))
                    {
                        QuotationDetail dbQuotationDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == dtoItem.QuotationDetailID);
                        if (dbQuotationDetail == null)
                        {
                            throw new Exception("Quotation item: [" + dtoItem.ArticleCode + "] [" + dtoItem.FactoryUD + "] not found!");
                        }
                        var dbDiff = context.PriceDifference.FirstOrDefault(o => o.PriceDifferenceUD == dtoItem.PriceDifferenceCode && o.Season == dtoItem.Season);
                        if (dbDiff == null)
                        {
                            throw new Exception("Diff code: [" + dtoItem.PriceDifferenceCode + "] [" + dtoItem.Season + "] not found!");
                        }

                        dbQuotationDetail.TargetPrice = dtoItem.NewTargetPrice.Value;
                        dbQuotationDetail.PriceUpdatedDate = DateTime.Now;
                        // check if can confirm
                        if (dbQuotationDetail.SalePrice == Math.Round(dbQuotationDetail.TargetPrice.Value / (1 + dbDiff.Rate.Value), 2, MidpointRounding.AwayFromZero))
                        {
                            dbQuotationDetail.StatusID = 3; // confirm status;
                            dbQuotationDetail.StatusUpdatedBy = userId;
                            dbQuotationDetail.StatusUpdatedDate = DateTime.Now;
                        }

                        QuotationOffer dbOffer = new QuotationOffer();
                        context.QuotationOffer.Add(dbOffer);
                        dbOffer.QuotationOfferVersion = context.QuotationOffer.Count(o => o.QuotationID == dbQuotationDetail.QuotationID) + 1;
                        dbOffer.QuotationID = dbQuotationDetail.QuotationID;
                        dbOffer.QuotationOfferDate = DateTime.Now;
                        dbOffer.QuotationOfferDirectionID = 2;
                        dbOffer.UpdatedBy = userId;
                        dbOffer.UpdatedDate = DateTime.Now;

                        QuotationOfferDetail dbOfferDetail = new QuotationOfferDetail();
                        dbOffer.QuotationOfferDetail.Add(dbOfferDetail);
                        dbOfferDetail.QuotationDetailID = dtoItem.QuotationDetailID;
                        dbOfferDetail.Price = dtoItem.NewTargetPrice.Value;
                        if (!string.IsNullOrEmpty(dtoItem.NewTargetComment))
                        {
                            dbOfferDetail.Remark = dtoItem.NewTargetComment;
                        }
                    }
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool ConfirmPrice(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            List<DTO.FactoryQuotationSearchResultDTO> dtoOffers = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.FactoryQuotationSearchResultDTO>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.IsSelected && o.PriceIncludeDiff.HasValue))
                    {
                        QuotationDetail dbQuotationDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == dtoItem.QuotationDetailID);
                        if (dbQuotationDetail == null)
                        {
                            throw new Exception("Quotation item: [" + dtoItem.ArticleCode + "] [" + dtoItem.FactoryUD + "] not found!");
                        }
                        if (dbQuotationDetail.StatusID.HasValue && dbQuotationDetail.StatusID.Value != 3)
                        {
                            dbQuotationDetail.TargetPrice = dbQuotationDetail.SalePrice;
                            dbQuotationDetail.PriceUpdatedDate = DateTime.Now;
                            dbQuotationDetail.StatusID = 3; // confirm status;
                            dbQuotationDetail.StatusUpdatedBy = userId;
                            dbQuotationDetail.StatusUpdatedDate = DateTime.Now;

                            QuotationOffer dbOffer = new QuotationOffer();
                            context.QuotationOffer.Add(dbOffer);
                            dbOffer.QuotationOfferVersion = context.QuotationOffer.Count(o => o.QuotationID == dbQuotationDetail.QuotationID) + 1;
                            dbOffer.QuotationID = dbQuotationDetail.QuotationID;
                            dbOffer.QuotationOfferDate = DateTime.Now;
                            dbOffer.QuotationOfferDirectionID = 2;
                            dbOffer.UpdatedBy = userId;
                            dbOffer.UpdatedDate = DateTime.Now;

                            QuotationOfferDetail dbOfferDetail = new QuotationOfferDetail();
                            dbOffer.QuotationOfferDetail.Add(dbOfferDetail);
                            dbOfferDetail.QuotationDetailID = dtoItem.QuotationDetailID;
                            dbOfferDetail.Price = dbQuotationDetail.SalePrice;
                            if (!string.IsNullOrEmpty(dtoItem.NewTargetComment))
                            {
                                dbOfferDetail.Remark = dtoItem.NewTargetComment;
                            }
                        }
                    }
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool UnConfirmPrice(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            List<DTO.FactoryQuotationSearchResultDTO> dtoOffers = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.FactoryQuotationSearchResultDTO>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.IsSelected))
                    {
                        QuotationDetail dbQuotationDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == dtoItem.QuotationDetailID);
                        if (dbQuotationDetail == null)
                        {
                            throw new Exception("Quotation item: [" + dtoItem.ArticleCode + "] [" + dtoItem.FactoryUD + "] not found!");
                        }
                        if (dbQuotationDetail.StatusID == 3)
                        {
                            dbQuotationDetail.PriceUpdatedDate = DateTime.Now;
                            dbQuotationDetail.StatusID = 1; // pending;
                            dbQuotationDetail.StatusUpdatedBy = userId;
                            dbQuotationDetail.StatusUpdatedDate = DateTime.Now;
                        }
                    }
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public List<DTO.OfferHistoryDTO> GetHistoryData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.OfferHistoryDTO> data = new List<DTO.OfferHistoryDTO>();

            //try to get data
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_OfferHistory(context.AVTPurchasingPriceMng_OfferHistory_View.Where(o => o.QuotationDetailID == id).OrderByDescending(o => o.UpdatedDate).ToList());
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

        public List<DTO.PurchasingPriceHistoryDTO> GetPurchasingPriceHistoryData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.PurchasingPriceHistoryDTO> data = new List<DTO.PurchasingPriceHistoryDTO>();

            //try to get data
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_PurchasingPriceHistory(context.AVTPurchasingPriceMng_RelatedQuotationDetail_View.Where(o => o.QuotationDetailID == id).OrderByDescending(o => o.Season).ToList());
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

        public List<DTO.GeneralBreakDownDTO> GetGeneralBreakDownData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.GeneralBreakDownDTO> data = new List<DTO.GeneralBreakDownDTO>();

            //try to get data
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_GeneralBreakDown(context.AVTPurchasingPriceMng_GeneralBreakDown_View.Where(o => o.ModelID == id).ToList());
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

        public List<DTO.PALBreakDownDTO> GetPALBreakDownData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.PALBreakDownDTO> data = new List<DTO.PALBreakDownDTO>();

            //try to get data
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_PALBreakDown(context.AVTPurchasingPriceMng_PALBreakDown_View.Where(o => o.ModelID == id).ToList());
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

        public List<DTO.AVTBreakdownCategoryOptionDTO> GetBreakdownCategoryOption(List<DTO.FactoryQuotationSearchResultDTO> datas , out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.avtBreakdownCategoryOptionDTOs = new List<DTO.AVTBreakdownCategoryOptionDTO>();
            try
            {
                using(var context = CreateContext())
                {
                    List<int?> OfferLineIDs = datas.Select(o => o.OfferLineID).ToList();
                    var avtBreakdownCategoryOptionDTOs = context.AVTPurchasingPriceMng_BreakdownCategoryOption_View.Where(o => OfferLineIDs.Contains(o.OfferLineID)).ToList();
                    data.avtBreakdownCategoryOptionDTOs = AutoMapper.Mapper.Map<List<AVTPurchasingPriceMng_BreakdownCategoryOption_View>, List<DTO.AVTBreakdownCategoryOptionDTO>>(avtBreakdownCategoryOptionDTOs);
                    //data.avtBreakdownCategoryOptionDTOs = avtBreakdownCategoryOptionDTOs;
                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data.avtBreakdownCategoryOptionDTOs;
        }

        public string GetExcelReport(Hashtable filters, out Library.DTO.Notification notification)
        {

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            AVTPurchasingPriceMngObjectData ds = new AVTPurchasingPriceMngObjectData();

            try
            {
                DateTime tmpDate;
                CultureInfo cInfo = new CultureInfo("vi-VN");
                string Season = null;
                string ClientNM = null;
                string Description = null;
                string FactoryUD = null;
                string ProformaInvoiceNo = null;
                string LDSFrom = null;
                string LDSTo = null;
                int? ItemTypeID = null;
                int? StatusID = null;
                int? QuotationOfferDirectionID = null;
                int? BusinessDataStatusID = null;
                int? ShippedStatus = null;
                if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                {
                    Season = filters["Season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
                {
                    ClientNM = filters["ClientNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                {
                    Description = filters["Description"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
                {
                    FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                {
                    ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("LDSFrom") && filters["LDSFrom"] != null && !string.IsNullOrEmpty(filters["LDSFrom"].ToString()))
                {
                    if (DateTime.TryParse(filters["LDSFrom"].ToString(), cInfo, DateTimeStyles.None, out tmpDate))
                    {
                        LDSFrom = tmpDate.ToString("yyyy-MM-dd");
                    }
                }
                if (filters.ContainsKey("LDSTo") && filters["LDSTo"] != null && !string.IsNullOrEmpty(filters["LDSTo"].ToString()))
                {
                    if (DateTime.TryParse(filters["LDSTo"].ToString(), cInfo, DateTimeStyles.None, out tmpDate))
                    {
                        LDSTo = tmpDate.ToString("yyyy-MM-dd");
                    }
                }
                if (filters.ContainsKey("ItemTypeID") && filters["ItemTypeID"] != null && !string.IsNullOrEmpty(filters["ItemTypeID"].ToString()))
                {
                    ItemTypeID = Convert.ToInt32(filters["ItemTypeID"].ToString());
                }
                if (filters.ContainsKey("StatusID") && filters["StatusID"] != null && !string.IsNullOrEmpty(filters["StatusID"].ToString()))
                {
                    StatusID = Convert.ToInt32(filters["StatusID"].ToString());
                }
                if (filters.ContainsKey("QuotationOfferDirectionID") && filters["QuotationOfferDirectionID"] != null && !string.IsNullOrEmpty(filters["QuotationOfferDirectionID"].ToString()))
                {
                    QuotationOfferDirectionID = Convert.ToInt32(filters["QuotationOfferDirectionID"].ToString());
                }
                if (filters.ContainsKey("BusinessDataStatusID") && filters["BusinessDataStatusID"] != null && !string.IsNullOrEmpty(filters["BusinessDataStatusID"].ToString()))
                {
                    BusinessDataStatusID = Convert.ToInt32(filters["BusinessDataStatusID"].ToString());
                }
                if (filters.ContainsKey("ShippedStatus") && filters["ShippedStatus"] != null && !string.IsNullOrEmpty(filters["ShippedStatus"].ToString()))
                {
                    ShippedStatus = Convert.ToInt32(filters["ShippedStatus"].ToString());
                }

                //if(ShippedStatus == null)
                //{
                //    ShippedStatus = 0;
                //}
                AVTPurchasingPriceMngObjectData.ReportHeaderRow hRow = ds.ReportHeader.NewReportHeaderRow();
                hRow.Season = Season;
                ds.ReportHeader.AddReportHeaderRow(hRow);

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("AVTPurchasingPriceMng_function_ExportToExcelFactoryQuotation", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ClientNM", ClientNM);
                adap.SelectCommand.Parameters.AddWithValue("@Description", Description);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", FactoryUD);
                adap.SelectCommand.Parameters.AddWithValue("@ItemTypeID", ItemTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@StatusID", StatusID);
                adap.SelectCommand.Parameters.AddWithValue("@QuotationOfferDirectionID", QuotationOfferDirectionID);
                adap.SelectCommand.Parameters.AddWithValue("@ProformaInvoiceNo", ProformaInvoiceNo);
                adap.SelectCommand.Parameters.AddWithValue("@BusinessDataStatusID", BusinessDataStatusID);
                adap.SelectCommand.Parameters.AddWithValue("@ShippedStatus", ShippedStatus);

                adap.SelectCommand.Parameters.AddWithValue("@LDSFrom", LDSFrom);
                adap.SelectCommand.Parameters.AddWithValue("@LDSTo", LDSTo);
                adap.TableMappings.Add("Table", "FactoryQuotationExcel");
                adap.TableMappings.Add("Table1", "ReportHeader");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "AVTPurchasingPrice");

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

        public List<DTO.FactoryQuotationSearchResultDTO> GetDataWithFilterAdditional(string season, string ids, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryQuotationSearchResultDTO> data = new List<DTO.FactoryQuotationSearchResultDTO>();
           
            //try to get data
            try
            {
                using (AVTPurchasingPriceMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryQuotationSearchResult(context.AVTPurchasingPriceMng_function_GetAdditionalInfo(season, ids).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
