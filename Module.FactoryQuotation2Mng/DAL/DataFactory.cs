using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotation2Mng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        private FactoryQuotation2MngEntities CreateContext()
        {
            FactoryQuotation2MngEntities mContext = new FactoryQuotation2MngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryQuotation2MngModel"));
            mContext.Database.CommandTimeout = 300;
            return mContext;
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FactoryQuotationSearchResultDTO>();
            data.WaitForFactoryConclusionDTOs = new List<DTO.FactoryDTO>();
            data.WaitForFactoryProductionConclusionDTOs = new List<DTO.FactoryDTO>();
            totalRows = 0;

            string Season = null;
            string ClientUD = null;
            string Description = null;
            string FactoryUD = null;
            string ProformaInvoiceNo = null;
            int? ItemTypeID = null;
            int? StatusID = null;
            int? QuotationOfferDirectionID = null;
            int? BusinessDataStatusID = null;
            int UserID = 0;
            bool? IsDeadline = null;
            bool? IsRepeatItem = null;
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
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
            if (filters.ContainsKey("UserID") && filters["UserID"] != null && !string.IsNullOrEmpty(filters["UserID"].ToString()))
            {
                UserID = Convert.ToInt32(filters["UserID"].ToString());
            }
            if (filters.ContainsKey("IsDeadLine") && filters["IsDeadLine"] != null && !string.IsNullOrEmpty(filters["IsDeadLine"].ToString()))
            {
                IsDeadline = Convert.ToBoolean(int.Parse(filters["IsDeadLine"].ToString()));
            }
            if (filters.ContainsKey("IsRepeatItem") && filters["IsRepeatItem"] != null && !string.IsNullOrEmpty(filters["IsRepeatItem"].ToString()))
            {
                IsRepeatItem = Convert.ToBoolean(int.Parse(filters["IsRepeatItem"].ToString()));
            }
            //try to get data
            try
            {
                using (FactoryQuotation2MngEntities context = CreateContext())
                {
                    // get search data
                    if (pageIndex > 1)
                    {
                        totalRows = -1;
                    }
                    else
                    {
                        // get conclusion by season
                        var dbConclusion = context.FactoryQuotation2Mng_function_GetQuotaionConclusion(Season, ClientUD, Description, FactoryUD, 
                            ItemTypeID, StatusID, QuotationOfferDirectionID, ProformaInvoiceNo, BusinessDataStatusID).FirstOrDefault();
                        data.TotalItem = dbConclusion.TotalItem.Value;
                        data.TotalConfirmedItem = dbConclusion.TotalConfirmedItem.Value;
                        data.TotalWaitForEurofar = dbConclusion.TotalWaitEurofar.Value;
                        data.TotalContainer = dbConclusion.TotalContainer.Value;
                        data.TotalConfirmedContainer = dbConclusion.TotalConfirmedContainer.Value;
                        data.TotalContainerWaitForEurofar = dbConclusion.TotalContainerWaitEurofar.Value;

                        // wait for factory conclusion - offer item
                        var WaitForFactoryConclusionDTOs = converter.DB2DTO_WaitForFactoryConclusionDTO(context.FactoryQuotation2Mng_function_GetWaitForFactoryConclusion(Season, UserID).ToList());
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
                        var WaitForFactoryProductionConclusionDTOs = converter.DB2DTO_WaitForFactoryProductionConclusionDTO(context.FactoryQuotation2Mng_function_GetWaitForFactoryProductionConclusion(Season, UserID).ToList());
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

                        //totalRows = context.FactoryQuotation2Mng__function_GetTotalRowFound(Season, ClientUD, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, ProformaInvoiceNo, BusinessDataStatusID, UserID).FirstOrDefault().Value;
                        totalRows = context.FactoryQuotation2Mng_function_SearchFactoryQuotation(Season, ClientUD, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, ProformaInvoiceNo, BusinessDataStatusID, UserID, IsDeadline, IsRepeatItem, orderBy, orderDirection).Count();
                    }

                    var result = context.FactoryQuotation2Mng_function_SearchFactoryQuotation(Season, ClientUD, Description, FactoryUD, ItemTypeID, StatusID, QuotationOfferDirectionID, ProformaInvoiceNo, BusinessDataStatusID, UserID, IsDeadline, IsRepeatItem, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_FactoryQuotationSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return data;
        }
        public string Export(System.Collections.Hashtable filters, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            FactoryQuotation2MngDataSet ds = new FactoryQuotation2MngDataSet();
            try
            {
                string Season = null;
                string ClientUD = null;
                string Description = null;
                string FactoryUD = null;
                string ProformaInvoiceNo = null;
                int? ItemTypeID = null;
                int? StatusID = null;
                int? QuotationOfferDirectionID = null;
                int? BusinessDataStatusID = null;
                bool? IsDeadline = null;


                if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                {
                    Season = filters["Season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
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
                if (filters.ContainsKey("IsDeadLine") && filters["IsDeadLine"] != null && !string.IsNullOrEmpty(filters["IsDeadLine"].ToString()))
                {
                    IsDeadline = Convert.ToBoolean(int.Parse(filters["IsDeadLine"].ToString()));
                }

                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryQuotation2Mng_function_ExportFactoryQuotation", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.SelectCommand.Parameters.AddWithValue("@ClientUD", ClientUD);
                adap.SelectCommand.Parameters.AddWithValue("@Description", Description);
                adap.SelectCommand.Parameters.AddWithValue("@FactoryUD", FactoryUD);
                adap.SelectCommand.Parameters.AddWithValue("@ItemTypeID", ItemTypeID);
                adap.SelectCommand.Parameters.AddWithValue("@StatusID", StatusID);
                adap.SelectCommand.Parameters.AddWithValue("@QuotationOfferDirectionID", QuotationOfferDirectionID);
                adap.SelectCommand.Parameters.AddWithValue("@ProformaInvoiceNo", ProformaInvoiceNo);
                adap.SelectCommand.Parameters.AddWithValue("@BusinessDataStatusID", BusinessDataStatusID);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                adap.SelectCommand.Parameters.AddWithValue("@IsDeadline", IsDeadline);                

                adap.TableMappings.Add("Table", "QuotationDetail");            
                adap.Fill(ds);

                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "FactoryQuotation2Mng");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Exception iEx = Library.Helper.GetInnerException(ex);
                notification.Message = iEx.Message;
                return string.Empty;
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
                using (FactoryQuotation2MngEntities context = CreateContext())
                {
                    data.QuotationStatusDTOs = converter.DB2DTO_QuotationStatus(context.SupportMng_QuotationStatus_View.ToList());
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
                    foreach (int id in toBeRemoveItems)
                    {
                        data.QuotationStatusDTOs.Remove(data.QuotationStatusDTOs.FirstOrDefault(o => o.QuotationStatusID == id));
                    }
                    data.QuotationStatusDTOs.Add(new DTO.QuotationStatusDTO { QuotationStatusID = -1, QuotationStatusNM = "ITEMS COSTPRICE" });
                    data.QuotationStatusDTOs.Add(new DTO.QuotationStatusDTO { QuotationStatusID = -2, QuotationStatusNM = "ITEMS NO COSTPRICE" });
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
            List<int> confirmedQuotationDetailIDs = new List<int>();
            try
            {
                //
                // check factory access permission
                //
                foreach (DTO.FactoryQuotationSearchResultDTO dtoOffer in dtoOffers)
                {
                    if (fwFactory.CheckFactoryPermission(userId, dtoOffer.FactoryID.Value) <= 0)
                    {
                        throw new Exception("You dont have permission for accessing factory data for [" + dtoOffer.FactoryUD + "]!");
                    }
                }

                using (FactoryQuotation2MngEntities context = CreateContext())
                {
                    // get similar item: same offerlineid, same factoryid
                    int[] existingIDs = dtoOffers.Where(o => o.NewPurchasingPrice.HasValue).Select(o => o.QuotationDetailID.Value).ToArray();
                    List<DTO.FactoryQuotationSearchResultDTO> additionalItems = new List<DTO.FactoryQuotationSearchResultDTO>();
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.NewPurchasingPrice.HasValue && o.OfferSeasonDetailID.HasValue && o.StatusID != 3).ToList())
                    {
                        additionalItems = converter.DB2DTO_SimilarItems(context.FactoryQuotation2Mng_SimilarItem_View.Where(o =>
                            !existingIDs.Contains(o.QuotationDetailID)                          
                            && o.OfferSeasonDetailID == dtoItem.OfferSeasonDetailID
                            && o.FactoryID == dtoItem.FactoryID
                            && o.StatusID != 3).ToList());

                        if (additionalItems.Count() > 0)
                        {
                            additionalItems.ForEach(o => { o.NewPurchasingPrice = dtoItem.NewPurchasingPrice.Value; o.NewPurchasingComment = dtoItem.NewPurchasingComment; });
                            dtoOffers.AddRange(additionalItems);
                        }
                    }

                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.NewPurchasingPrice.HasValue && o.QuotationStatusNM != "WAITING REJECT"))
                    {
                        QuotationDetail dbQuotationDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == dtoItem.QuotationDetailID);
                        if (dbQuotationDetail == null)
                        {
                            throw new Exception("Quotation item: [" + dtoItem.ArticleCode + "] [" + dtoItem.FactoryUD + "] not found!");
                        }
                        var dbDiff = context.PriceDifference.FirstOrDefault(o => o.PriceDifferenceUD == dtoItem.PriceDifferenceCode && o.Season == dtoItem.Season);
                        if (dbDiff == null)
                        {
                            throw new Exception("Quality code: [" + dtoItem.PriceDifferenceCode + "] [" + dtoItem.Season + "] not found!");
                        }

                        dbQuotationDetail.PurchasingPrice = dtoItem.NewPurchasingPrice.Value;
                        dbQuotationDetail.PriceUpdatedDate = DateTime.Now;
                        dbQuotationDetail.SalePrice = dtoItem.NewPurchasingPrice.Value * (1 + Math.Round(dbDiff.Rate.Value / 100, 2, MidpointRounding.AwayFromZero));

                        // check if can confirm
                        if(dbQuotationDetail.TargetPrice == null)
                        {
                            dbQuotationDetail.TargetPrice = 0;
                        }
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
                        dbOffer.QuotationOfferDirectionID = 1;
                        dbOffer.UpdatedBy = userId;
                        dbOffer.UpdatedDate = DateTime.Now;

                        QuotationOfferDetail dbOfferDetail = new QuotationOfferDetail();
                        dbOffer.QuotationOfferDetail.Add(dbOfferDetail);
                        dbOfferDetail.QuotationDetailID = dtoItem.QuotationDetailID;
                        dbOfferDetail.Price = dtoItem.NewPurchasingPrice.Value;
                        if (!string.IsNullOrEmpty(dtoItem.NewPurchasingComment))
                        {
                            dbOfferDetail.Remark = dtoItem.NewPurchasingComment;
                        }
                    }

                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.Where(o => o.QuotationStatusNM == "WAITING REJECT"))
                    {
                        QuotationDetail dbQuotationDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == dtoItem.QuotationDetailID);
                        if (dbQuotationDetail == null)
                        {
                            throw new Exception("Quotation item: [" + dtoItem.ArticleCode + "] [" + dtoItem.FactoryUD + "] not found!");
                        }

                        dbQuotationDetail.StatusID = 5; //REJECTED
                        dbQuotationDetail.StatusUpdatedBy = userId;
                        dbQuotationDetail.StatusUpdatedDate = DateTime.Now;

                        //reset data OfferSeasonDetail
                        context.FactoryQuotation2Mng_function_AfterUnConfirm(dtoItem.QuotationDetailID);
                    }
                    context.SaveChanges();

                    // update offer season planing purchasing price
                    foreach (int quotationDetailID in confirmedQuotationDetailIDs)
                    {
                        context.FW_function_UpdateOfferSeasonDetailPurchasingPriceFromQuotationConfirmed(quotationDetailID, userId);
                    }

                    // refresh cached data
                    foreach (DTO.FactoryQuotationSearchResultDTO dtoItem in dtoOffers.ToList())
                    {
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
                using (FactoryQuotation2MngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_OfferHistory(context.FactoryQuotation2Mng_OfferHistory_View.Where(o => o.QuotationDetailID == id).OrderByDescending(o => o.UpdatedDate).ToList());
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
        //get data additional condition
        public List<DTO.ClientAdditionalConditionDTO> GetACData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientAdditionalConditionDTO> data = new List<DTO.ClientAdditionalConditionDTO>();

            //try to get data
            try
            {
                using (FactoryQuotation2MngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_ACData(context.FactoryQuotation2Mng_ClientAdditionalCondition_View.ToList());
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

        public bool ImportQuotationDetail(int userId, object dtoItems, out Library.DTO.Notification notification)
        {
            List<DTO.ImportQuotationDetailDTO> dtoImports = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<DTO.ImportQuotationDetailDTO>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };         
            //try to get data
            try
            {
                using (FactoryQuotation2MngEntities context = CreateContext())
                {
                    foreach (var item in dtoImports)
                    {
                        context.FactoryQuotation2Mng_action_AddQuotationOffer(item.QuotationDetailID, item.FactoryPrice, item.NewComment, userId);
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
    }
}
