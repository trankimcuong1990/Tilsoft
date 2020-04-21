using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private EurofarPurchasingPriceMngEntities CreateContext()
        {
            EurofarPurchasingPriceMngEntities mContext = new EurofarPurchasingPriceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.EurofarPurchasingPriceMngModel"));
            mContext.Database.CommandTimeout = 300;
            return mContext;
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryQuotationSearchResultDTO>();
            data.eurofarBreakdownCategoryOptionDTOs = new List<DTO.EurofarBreakdownCategoryOptionDTO>();
            //data.WaitForFactoryConclusionDTOs = new List<DTO.WaitForFactoryConclusionDTO>();
            data.EmailAddressReceivePriceRequestDTO = new List<DTO.EmailAddressReceivePriceRequestDTO>();
            data.WaitForFactoryConclusionDTOs = new List<DTO.FactoryDTO>();
            data.WaitForFactoryProductionConclusionDTOs = new List<DTO.FactoryDTO>();
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
            int? OrderTypeID = null;
            int? StatusID = null;
            int? QuotationOfferDirectionID = null;
            int? BusinessDataStatusID = null;
            int? ShippedStatus = null;
            int? CataloguePageNo = null;
            int? PricingTeamMemberID = null;
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
            if (filters.ContainsKey("OrderTypeID") && filters["OrderTypeID"] != null && !string.IsNullOrEmpty(filters["OrderTypeID"].ToString()))
            {
                OrderTypeID = Convert.ToInt32(filters["OrderTypeID"].ToString());
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
            if (filters.ContainsKey("CataloguePageNo") && filters["CataloguePageNo"] != null && !string.IsNullOrEmpty(filters["CataloguePageNo"].ToString()))
            {
                CataloguePageNo = Convert.ToInt32(filters["CataloguePageNo"].ToString());
            }
            if (filters.ContainsKey("PricingTeamMemberID") && filters["PricingTeamMemberID"] != null && !string.IsNullOrEmpty(filters["PricingTeamMemberID"].ToString()))
            {
                PricingTeamMemberID = Convert.ToInt32(filters["PricingTeamMemberID"].ToString());
            }
            //try to get data
            try
            {
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    if (pageIndex == 1)
                    {// get current exchange rate
                        data.CurrentExchangeRate = 1;
                        var dbMasteExchangeRate = context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault();
                        if (dbMasteExchangeRate != null && dbMasteExchangeRate.ExchangeRate.HasValue)
                        {
                            data.CurrentExchangeRate = dbMasteExchangeRate.ExchangeRate.Value;
                        }

                        // get conclusion
                        var dbConclusion = context.EurofarPurchasingPriceMng_function_GetQuotaionConclusion(Season, ClientNM, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, OrderTypeID, ProformaInvoiceNo, BusinessDataStatusID, ShippedStatus, LDSFrom, LDSTo, CataloguePageNo).FirstOrDefault();
                        data.TotalItem = dbConclusion.TotalItem;
                        data.TotalConfirmedItem = dbConclusion.TotalConfirmedItem;
                        data.TotalWaitForEurofar = dbConclusion.TotalWaitEurofar;
                        data.TotalEstimated = dbConclusion.TotalEstimated;
                        data.TotalContainer = dbConclusion.TotalContainer;
                        data.TotalConfirmedContainer = dbConclusion.TotalConfirmedContainer;
                        data.TotalContainerWaitForEurofar = dbConclusion.TotalContainerWaitEurofar;
                        data.TotalContainerEstimated = dbConclusion.TotalContainerEstimated;

                        // wait for factory conclusion - offer item
                        var WaitForFactoryConclusionDTOs = converter.DB2DTO_WaitForFactoryConclusionDTO(context.EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion(Season).ToList());
                        WaitForFactoryConclusionDTOs.Select(o => new { o.FactoryID, o.FactoryUD }).Distinct().ToList().ForEach(o => data.WaitForFactoryConclusionDTOs.Add(new DTO.FactoryDTO { FactoryID = o.FactoryID, FactoryUD = o.FactoryUD }));
                        foreach (DTO.FactoryDTO dtoFactory in data.WaitForFactoryConclusionDTOs)
                        {
                            if (WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 1) != null)
                                dtoFactory.OverDue1Day = WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 1).TotalItem;

                            if (WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 2) != null)
                                dtoFactory.OverDue2Day = WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 2).TotalItem;

                            if (WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 3) != null)
                                dtoFactory.OverDue3Day = WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 3).TotalItem;

                            if (WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 4) != null)
                                dtoFactory.OverDue4DayOrMore = WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 4).TotalItem;

                            dtoFactory.TotalPendingItem = WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID).TotalPendingItem;
                            dtoFactory.PricingTeamMemberID = WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID).PricingTeamMemberID;
                            dtoFactory.PricingTeamMemberNM = WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID).PricingTeamMemberNM;
                        }

                        // wait for factory conclusion - production item
                        var WaitForFactoryProductionConclusionDTOs = converter.DB2DTO_WaitForFactoryConclusionDTO(context.EurofarPurchasingPriceMng_function_GetWaitForFactoryProductionConclusion(Season).ToList());
                        WaitForFactoryProductionConclusionDTOs.Select(o => new { o.FactoryID, o.FactoryUD }).Distinct().ToList().ForEach(o => data.WaitForFactoryProductionConclusionDTOs.Add(new DTO.FactoryDTO { FactoryID = o.FactoryID, FactoryUD = o.FactoryUD }));
                        foreach (DTO.FactoryDTO dtoFactory in data.WaitForFactoryProductionConclusionDTOs)
                        {
                            if (WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == -1) != null)
                                dtoFactory.OverLDS = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == -1).TotalItem;

                            if (WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 0) != null)
                                dtoFactory.LDS = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 0).TotalItem;

                            if (WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 1) != null)
                                dtoFactory.OneToTwoDays = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 1).TotalItem;

                            if (WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 2) != null)
                                dtoFactory.ThreeToFourDays = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 2).TotalItem;

                            if (WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 3) != null)
                                dtoFactory.FiveToSixDays = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 3).TotalItem;

                            if (WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 4) != null)
                                dtoFactory.MoreThanSixDays = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 4).TotalItem;

                            dtoFactory.TotalPendingItem = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID).TotalPendingItem;
                            dtoFactory.PricingTeamMemberID = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID).PricingTeamMemberID;
                            dtoFactory.PricingTeamMemberNM = WaitForFactoryProductionConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID).PricingTeamMemberNM;
                        }

                        data.EmailAddressReceivePriceRequestDTO = converter.DB2DTO_EmailAddressReceivePriceRequestDTO(context.EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest(Season).ToList());

                        totalRows = context.EurofarPurchasingPriceMng_function_SearchFactoryQuotation(Season, ClientNM, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, OrderTypeID, ProformaInvoiceNo, BusinessDataStatusID, ShippedStatus, LDSFrom, LDSTo, CataloguePageNo, PricingTeamMemberID, orderBy, orderDirection).Count();
                    }
                    else
                    {
                        totalRows = -1;
                    }                    
                    var result = context.EurofarPurchasingPriceMng_function_SearchFactoryQuotation(Season, ClientNM, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, OrderTypeID, ProformaInvoiceNo, BusinessDataStatusID, ShippedStatus, LDSFrom, LDSTo, CataloguePageNo, PricingTeamMemberID, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryQuotationSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public string Export(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            EurofarPurchasingPriceMngDataSet ds = new EurofarPurchasingPriceMngDataSet();
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
                int? OrderTypeID = null;
                int? StatusID = null;
                int? QuotationOfferDirectionID = null;
                int? BusinessDataStatusID = null;
                int? ShippedStatus = null;
                int? CataloguePageNo = null;
                int? PricingTeamMemberID = null;
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
                if (filters.ContainsKey("OrderTypeID") && filters["OrderTypeID"] != null && !string.IsNullOrEmpty(filters["OrderTypeID"].ToString()))
                {
                    OrderTypeID = Convert.ToInt32(filters["OrderTypeID"].ToString());
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
                if (filters.ContainsKey("CataloguePageNo") && filters["CataloguePageNo"] != null && !string.IsNullOrEmpty(filters["CataloguePageNo"].ToString()))
                {
                    CataloguePageNo = Convert.ToInt32(filters["CataloguePageNo"].ToString());
                }
                if (filters.ContainsKey("PricingTeamMemberID") && filters["PricingTeamMemberID"] != null && !string.IsNullOrEmpty(filters["PricingTeamMemberID"].ToString()))
                {
                    PricingTeamMemberID = Convert.ToInt32(filters["PricingTeamMemberID"].ToString());
                }
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("EurofarPurchasingPriceMng_function_ExportFactoryQuotation", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ClientNM", ClientNM);
                adap.SelectCommand.Parameters.AddWithValue("@Description", Description);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", FactoryUD);
                adap.SelectCommand.Parameters.AddWithValue("@ItemTypeID", ItemTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@StatusID", StatusID);
                adap.SelectCommand.Parameters.AddWithValue("@QuotationOfferDirectionID", QuotationOfferDirectionID);
                adap.SelectCommand.Parameters.AddWithValue("@OrderTypeID", OrderTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@ProformaInvoiceNo", ProformaInvoiceNo);
                adap.SelectCommand.Parameters.AddWithValue("@BusinessDataStatusID", BusinessDataStatusID);
                adap.SelectCommand.Parameters.AddWithValue("@ShippedStatus", ShippedStatus);
                adap.SelectCommand.Parameters.AddWithValue("@LDSFrom", LDSFrom);
                adap.SelectCommand.Parameters.AddWithValue("@LDSTo", LDSTo);
                adap.SelectCommand.Parameters.AddWithValue("@CataloguePageNo", CataloguePageNo);
                adap.SelectCommand.Parameters.AddWithValue("@PricingTeamMemberID", PricingTeamMemberID);

                adap.TableMappings.Add("Table", "QuotationDetail");
                adap.Fill(ds);

                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "EurofarPurchasingPriceMng");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return string.Empty;
            }
            
        }
        public bool ImportQuotationDetail(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            List<DTO.ImportQuotationDetailDTO> dtoImports = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.ImportQuotationDetailDTO>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try to get data
            try
            {
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    foreach (var item in dtoImports)
                    {
                        context.EurofarPurchasingPriceMng_action_AddQuotationOffer(item.QuotationDetailID, item.NewTargetComment, item.TargetPrice, userId);
                        //context.EurofarPurchasingPriceMng_function_RefreshCacheRow(item.QuotationDetailID, item.Season);
                        context.FW_function_RefreshPriceCacheRow(item.Season, item.QuotationDetailID, null);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
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

        public DTO.SupportFormData GetSearchFilter(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormData data = new DTO.SupportFormData();
            data.QuotationStatusDTOs = new List<DTO.QuotationStatusDTO>();

            //try to get data
            try
            {
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    data.QuotationStatusDTOs = converter.DB2DTO_QuotationStatus(context.SupportMng_QuotationStatus_View.ToList());
                    data.OrderTypeDTOs = converter.DB2DTO_OrderType(context.SupportMng_OrderType_View.ToList());
                    data.EurofarBreakdownCategoryDTOs = converter.DB2DTO_BreakdownCategory(context.EurofarPurchasingPriceMng_BreakdownCategory_View.ToList());
                    data.PricingTeamMemberInChargeDTOs = converter.DB2DTO_PricingTeamMemberInChargeDTO(context.EurofarPurchasingPriceMng_function_GetPricingTeamMemberInCharge().ToList());

                    //
                    // ADD CUSTOM STATUS AS REQUESTED - HARD CODE
                    //
                    List<int> toBeRemoveItems = new List<int>();
                    foreach (var item in data.QuotationStatusDTOs)
                    {
                        if (item.QuotationStatusID != 1 && item.QuotationStatusID != 3 && item.QuotationStatusID != 5)
                        {
                            toBeRemoveItems.Add(item.QuotationStatusID);
                        }
                    }
                    foreach(int id in toBeRemoveItems)
                    {
                        data.QuotationStatusDTOs.Remove(data.QuotationStatusDTOs.FirstOrDefault(o => o.QuotationStatusID == id));
                    }
                    data.QuotationStatusDTOs.Add(new DTO.QuotationStatusDTO { QuotationStatusID = -1, QuotationStatusNM = "ITEMS COSTPRICE" });
                    data.QuotationStatusDTOs.Add(new DTO.QuotationStatusDTO { QuotationStatusID = -2, QuotationStatusNM = "ITEMS NO COSTPRICE" });

                    // check special access code "SeeAllPurchasingData"
                    if (fwFactory.HasSpecialPermission(userID, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_SEE_ALL_PURCHASING_DATA))
                    {
                        data.isPermissionSeeAllPurchasingData = true;
                    }
                    else
                    {
                        data.isPermissionSeeAllPurchasingData = false;
                    }
                }
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
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
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    // get similar item: same offerlineid, same factoryid
                    // TO DO LATER
                    List<int> existingIDs = dtoOffers.Where(o => o.NewTargetPrice.HasValue && o.QuotationDetailID.HasValue).Select(o => o.QuotationDetailID.Value).ToList();
                    List<DTO.FactoryQuotationSearchResultDTO> additionalItems = new List<DTO.FactoryQuotationSearchResultDTO>();
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.NewTargetPrice.HasValue && o.OfferSeasonDetailID.HasValue && o.StatusID != 3).ToList())
                    {
                        additionalItems = converter.DB2DTO_SimilarItem(context.EurofarPurchasingPriceMng_SimilarItem_View.Where(o =>
                            !existingIDs.Contains(o.QuotationDetailID)
                            && o.OfferSeasonDetailID == dtoItem.OfferSeasonDetailID
                            && o.FactoryID == dtoItem.FactoryID
                            && o.StatusID != 3).ToList());

                        if (additionalItems.Count() > 0)
                        {
                            additionalItems.ForEach(o => { o.NewTargetPrice = dtoItem.NewTargetPrice.Value; o.NewTargetComment = dtoItem.NewTargetComment; });
                            dtoOffers.AddRange(additionalItems);
                        }
                    }
                    List<int> confirmedQuotationDetailIDs = new List<int>();
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o=>o.NewTargetPrice.HasValue))
                    {
                        QuotationDetail dbQuotationDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == dtoItem.QuotationDetailID && o.StatusID != 3);
                        if (dbQuotationDetail == null)
                        {
                            throw new Exception("Quotation item: [" + dtoItem.ArticleCode + "] [" + dtoItem.FactoryUD + "] not found or already confirmed!");
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
                            //confirmedQuotationDetailIDs.Add(dbQuotationDetail.QuotationDetailID);
                            if (dbQuotationDetail.OfferSeasonQuotationRequestDetailID.HasValue)
                            {
                                confirmedQuotationDetailIDs.Add(dbQuotationDetail.QuotationDetailID);
                            }
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

                    //ChangeOfferSeasonDetailStatusAfterConfirm(userId, context, confirmedOfferSeasonQuotationRequestDetailIDs);

                    //update offer season planing purchasing price
                    foreach (int quotationDetailID in confirmedQuotationDetailIDs)
                    {
                        context.FW_function_UpdateOfferSeasonDetailPurchasingPriceFromQuotationConfirmed(quotationDetailID, userId);
                    }

                    // refresh cached
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.ToList())
                    {
                        context.FW_function_RefreshPriceCacheRow(dtoItem.Season, dtoItem.QuotationDetailID, null);
                    }
                }

                return true;
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
            List<int> confirmedQuotationDetailIDs = new List<int>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.IsSelected && o.PurchasingPriceIncludeDiff.HasValue && o.QuotationDetailID.HasValue))
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
                            //confirmedQuotationDetailIDs.Add(dbQuotationDetail.QuotationDetailID);
                            if (dbQuotationDetail.OfferSeasonQuotationRequestDetailID.HasValue)
                            {
                                confirmedQuotationDetailIDs.Add(dbQuotationDetail.QuotationDetailID);
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
                            dbOfferDetail.Price = dbQuotationDetail.SalePrice;
                            if (!string.IsNullOrEmpty(dtoItem.NewTargetComment))
                            {
                                dbOfferDetail.Remark = dtoItem.NewTargetComment;
                            }
                        }
                    }
                    context.SaveChanges();
                    //ChangeOfferSeasonDetailStatusAfterConfirm(userId, context, confirmedOfferSeasonQuotationRequestDetailIDs);

                    // update offer season planing purchasing price
                    foreach (int quotationDetailID in confirmedQuotationDetailIDs)
                    {
                        context.FW_function_UpdateOfferSeasonDetailPurchasingPriceFromQuotationConfirmed(quotationDetailID, userId);
                    }

                    // refresh cached
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.ToList())
                    {
                        //context.EurofarPurchasingPriceMng_function_RefreshCacheRow(dtoItem.QuotationDetailID, dtoItem.Season);
                        context.FW_function_RefreshPriceCacheRow(dtoItem.Season, dtoItem.QuotationDetailID, null);
                    }
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
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.IsSelected && o.QuotationDetailID.HasValue && o.UnConfirmable))
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

                    // refresh cached
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.IsSelected && o.QuotationDetailID.HasValue && o.UnConfirmable).ToList())
                    {
                        //context.EurofarPurchasingPriceMng_function_RefreshCacheRow(dtoItem.QuotationDetailID, dtoItem.Season);
                        context.EurofarPurchasingPriceMng_function_AfterUnConfirm(dtoItem.QuotationDetailID);
                        context.FW_function_RefreshPriceCacheRow(dtoItem.Season, dtoItem.QuotationDetailID, null);                        
                    }

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
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_OfferHistory(context.EurofarPurchasingPriceMng_OfferHistory_View.Where(o => o.QuotationDetailID == id).OrderByDescending(o=>o.UpdatedDate).ToList());
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
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_PurchasingPriceHistory(context.EurofarPurchasingPriceMng_RelatedQuotationDetail_View.Where(o => o.QuotationDetailID == id).OrderByDescending(o => o.Season).ToList());
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
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_GeneralBreakDown(context.EurofarPurchasingPriceMng_GeneralBreakDown_View.Where(o => o.ModelID == id).ToList());
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
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_PALBreakDown(context.EurofarPurchasingPriceMng_PALBreakDown_View.Where(o => o.ModelID == id).ToList());
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

        public DTO.SearchFormData GetTotalRow(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryQuotationSearchResultDTO>();
            data.SubTotalRow = new DTO.SubTotalDTO();
            data.TotalRow = new DTO.SubTotalDTO();

            if (!fwFactory.HasSpecialPermission(userId, Module.Framework.ConstantIdentifier.SPECIAL_PERMISSION_SEE_ALL_PURCHASING_DATA))
            {
                return data;
            }

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
            int? OrderTypeID = null;
            int? StatusID = null;
            int? QuotationOfferDirectionID = null;
            int? BusinessDataStatusID = null;
            int? ShippedStatus = null;
            int? CataloguePageNo = null;
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
            if (filters.ContainsKey("OrderTypeID") && filters["OrderTypeID"] != null && !string.IsNullOrEmpty(filters["OrderTypeID"].ToString()))
            {
                OrderTypeID = Convert.ToInt32(filters["OrderTypeID"].ToString());
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
            if (filters.ContainsKey("CataloguePageNo") && filters["CataloguePageNo"] != null && !string.IsNullOrEmpty(filters["CataloguePageNo"].ToString()))
            {
                CataloguePageNo = Convert.ToInt32(filters["CataloguePageNo"].ToString());
            }
            //try to get data
            try
            {
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    // get search data
                    data.SubTotalRow = converter.DB2DTO_SubTotalDTO(context.EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation(Season, ClientNM, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, OrderTypeID, ProformaInvoiceNo, BusinessDataStatusID, ShippedStatus, LDSFrom, LDSTo, CataloguePageNo).FirstOrDefault());
                    data.TotalRow = converter.DB2DTO_SubTotalDTO(context.EurofarPurchasingPriceMng_function_GetSubTotalFactoryQuotation(Season, null, null, null, null, null, null, null, null, null, null, null, null, null).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetReportData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

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
            int? OrderTypeID = null;
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
            if (filters.ContainsKey("OrderTypeID") && filters["OrderTypeID"] != null && !string.IsNullOrEmpty(filters["OrderTypeID"].ToString()))
            {
                OrderTypeID = Convert.ToInt32(filters["OrderTypeID"].ToString());
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

            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("EurofarPurchasingPriceMng_function_ReportData", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ClientNM", ClientNM);
                adap.SelectCommand.Parameters.AddWithValue("@Description", Description);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", FactoryUD);
                adap.SelectCommand.Parameters.AddWithValue("@ItemTypeID", ItemTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@StatusID", StatusID);
                adap.SelectCommand.Parameters.AddWithValue("@QuotationOfferDirectionID", QuotationOfferDirectionID);
                adap.SelectCommand.Parameters.AddWithValue("@OrderTypeID", OrderTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@ProformaInvoiceNo", ProformaInvoiceNo);
                adap.SelectCommand.Parameters.AddWithValue("@BusinessDataStatusID", BusinessDataStatusID);
                adap.SelectCommand.Parameters.AddWithValue("@ShippedStatus", ShippedStatus);
                adap.SelectCommand.Parameters.AddWithValue("@LDSFrom", LDSFrom);
                adap.SelectCommand.Parameters.AddWithValue("@LDSTo", LDSTo);

                adap.TableMappings.Add("Table", "EurofarPurchasingPrice");
                adap.Fill(ds);
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "PurchasingPriceOfferMarginIncluded");
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
                using (EurofarPurchasingPriceMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryQuotationSearchResultAdditional(context.EurofarPurchasingPriceMng_function_GetAdditionalInfo(season, ids).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        //public DTO.SearchFormData GetConclusion(string season, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
        //    DTO.SearchFormData data = new DTO.SearchFormData();
        //    data.WaitForFactoryConclusionDTOs = new List<DTO.WaitForFactoryConclusionDTO>();
        //    data.EmailAddressReceivePriceRequestDTO = new List<DTO.EmailAddressReceivePriceRequestDTO>();
        //    data.FactoryDTOs = new List<DTO.FactoryDTO>();

        //    //try to get data
        //    try
        //    {
        //        using (EurofarPurchasingPriceMngEntities context = CreateContext())
        //        {
        //            // get conclusion by season
        //            var dbConclusion = context.EurofarPurchasingPriceMng_function_GetQuotaionConclusion(season).FirstOrDefault();
        //            data.TotalItem = dbConclusion.TotalItem.Value;
        //            data.TotalConfirmedItem = dbConclusion.TotalConfirmedItem.Value;
        //            data.TotalWaitForEurofar = dbConclusion.TotalWaitEurofar.Value;
        //            data.TotalContainer = dbConclusion.TotalContainer.Value;
        //            data.TotalConfirmedContainer = dbConclusion.TotalConfirmedContainer.Value;
        //            data.TotalContainerWaitForEurofar = dbConclusion.TotalContainerWaitEurofar.Value;

        //            data.WaitForFactoryConclusionDTOs = converter.DB2DTO_WaitForFactoryConclusionDTO(context.EurofarPurchasingPriceMng_function_GetWaitForFactoryConclusion(season).ToList());
        //            data.WaitForFactoryConclusionDTOs.Select(o => new { o.FactoryID, o.FactoryUD }).Distinct().ToList().ForEach(o => data.FactoryDTOs.Add(new DTO.FactoryDTO { FactoryID = o.FactoryID, FactoryUD = o.FactoryUD }));
        //            foreach (DTO.FactoryDTO dtoFactory in data.FactoryDTOs)
        //            {
        //                if(data.WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 1) != null)
        //                    dtoFactory.LessThan1Week = data.WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 1).TotalItem;

        //                if(data.WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 2) != null)
        //                    dtoFactory.From1To2Week = data.WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 2).TotalItem;

        //                if (data.WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 3) != null)
        //                    dtoFactory.From2To3Week = data.WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 3).TotalItem;

        //                if (data.WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 4) != null)
        //                    dtoFactory.Over3Week = data.WaitForFactoryConclusionDTOs.FirstOrDefault(o => o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 4).TotalItem;
        //            }
        //            data.EmailAddressReceivePriceRequestDTO = converter.DB2DTO_EmailAddressReceivePriceRequestDTO(context.EurofarPurchasingPriceMng_function_getEmailAddressReceivePriceRequest(season).ToList());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = ex.Message;
        //    }

        //    return data;
        //}

        public void ChangeOfferSeasonDetailStatusAfterConfirm(int userId, EurofarPurchasingPriceMngEntities context, List<int> IDs)
        {
            //string IDString = string.Empty;
            //foreach (int id in IDs)
            //{
            //    IDString += (string.IsNullOrEmpty(IDString) ? "" : ",") + id.ToString();
            //}
            //context.OfferSeasonQuotatonRequestMng_function_ChangeOfferSeasonDetailStatus(userId, IDString);
        }

        //public string ExportRawData(string season)
        //{
        //}
    }
}
