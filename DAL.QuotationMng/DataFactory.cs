using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.QuotationMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.QuotationMng.SearchFormData, DTO.QuotationMng.EditFormData, DTO.QuotationMng.Quotation>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        private QuotationMngEntities CreateContext()
        {
            return new QuotationMngEntities(DALBase.Helper.CreateEntityConnectionString("QuotationMngModel"));
        }

        public override DTO.QuotationMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.QuotationMng.SearchFormData data = new DTO.QuotationMng.SearchFormData();
            data.Data = new List<DTO.QuotationMng.QuotationSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                string QuotationUD = null;
                string FactoryOrderUD = null;
                string Season = null;
                string FactoryUD = null;
                string ProformaInvoiceNo = null;
                string ClientUD = null;
                string ArticleCode = null;
                string Description = null;
                if (filters.ContainsKey("QuotationUD") && !string.IsNullOrEmpty(filters["QuotationUD"].ToString()))
                {
                    QuotationUD = filters["QuotationUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryOrderUD") && !string.IsNullOrEmpty(filters["FactoryOrderUD"].ToString()))
                {
                    FactoryOrderUD = filters["FactoryOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                {
                    Season = filters["Season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
                {
                    FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                {
                    ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                {
                    ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                {
                    Description = filters["Description"].ToString().Replace("'", "''");
                }

                int totalRowReturned = 0;
                Task task1 = Task.Factory.StartNew(() => {
                    using (QuotationMngEntities context1 = CreateContext())
                    {
                        totalRowReturned = context1.QuotationMng_function_SearchQuotation(QuotationUD, Season, FactoryUD, FactoryOrderUD, ProformaInvoiceNo, ClientUD, ArticleCode, Description, orderBy, orderDirection).Count();
                    }
                });
                Task task2 = Task.Factory.StartNew(() => {
                    using (QuotationMngEntities context2 = CreateContext())
                    {
                        var result = context2.QuotationMng_function_SearchQuotation(QuotationUD, Season, FactoryUD, FactoryOrderUD, ProformaInvoiceNo, ClientUD, ArticleCode, Description, orderBy, orderDirection);
                        data.Data = converter.DB2DTO_QuotationSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    }
                });
                Task.WaitAll(task1, task2);

                totalRows = totalRowReturned;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public Task<DTO.QuotationMng.SearchFormData> GetDataWithFilterAsync(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection)
        {
            var tcs = new TaskCompletionSource<DTO.QuotationMng.SearchFormData>();
            DTO.QuotationMng.SearchFormData data = new DTO.QuotationMng.SearchFormData();
            data.Data = new List<DTO.QuotationMng.QuotationSearchResult>();

            //try to get data
            try
            {
                string QuotationUD = null;
                string FactoryOrderUD = null;
                string Season = null;
                string FactoryUD = null;
                string ProformaInvoiceNo = null;
                string ClientUD = null;
                string ArticleCode = null;
                string Description = null;
                if (filters.ContainsKey("QuotationUD") && !string.IsNullOrEmpty(filters["QuotationUD"].ToString()))
                {
                    QuotationUD = filters["QuotationUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryOrderUD") && !string.IsNullOrEmpty(filters["FactoryOrderUD"].ToString()))
                {
                    FactoryOrderUD = filters["FactoryOrderUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                {
                    Season = filters["Season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
                {
                    FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                {
                    ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                {
                    ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                {
                    Description = filters["Description"].ToString().Replace("'", "''");
                }

                int totalRowReturned = 0;
                Task task1 = Task.Factory.StartNew(() =>
                {
                    using (QuotationMngEntities context1 = CreateContext())
                    {
                        totalRowReturned = context1.QuotationMng_function_SearchQuotation(QuotationUD, Season, FactoryUD, FactoryOrderUD, ProformaInvoiceNo, ClientUD, ArticleCode, Description, orderBy, orderDirection).Count();
                    }
                });
                Task task2 = Task.Factory.StartNew(() =>
                {
                    using (QuotationMngEntities context2 = CreateContext())
                    {
                        var result = context2.QuotationMng_function_SearchQuotation(QuotationUD, Season, FactoryUD, FactoryOrderUD, ProformaInvoiceNo, ClientUD, ArticleCode, Description, orderBy, orderDirection);
                        data.Data = converter.DB2DTO_QuotationSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    }
                });
                Task.WaitAll(task1, task2);
                data.TotalRows = totalRowReturned;
                tcs.SetResult(data);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task; 
        }

        public override DTO.QuotationMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int id, ref DTO.QuotationMng.Quotation dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (QuotationMngEntities context = CreateContext())
                {
                    Quotation dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Quotation();
                        context.Quotation.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Quotation.FirstOrDefault(o => o.QuotationID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Quotation not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB(dtoItem, ref dbItem);

                        // generate latest code - table locks required
                        if (id == 0)
                        {
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT TOP 1 * FROM Quotation WITH (TABLOCKX, HOLDLOCK)");
                                int factoryID = dtoItem.FactoryID.Value;
                                string season = dtoItem.Season;
                                try
                                {
                                    var quotations = context.Quotation.Where(o => o.FactoryID.HasValue && o.FactoryID.Value == factoryID && o.Season == season).ToList();
                                    int lastNumber = 1;
                                    if (quotations.Count > 0)
                                    {
                                        lastNumber = quotations.Max(o => Convert.ToInt32(o.QuotationUD.Substring(0, 3))) + 1;
                                    }
                                    dbItem.QuotationUD = Library.Common.Helper.formatIndex(lastNumber.ToString(), 3, "0") + "/" + dtoItem.FactoryUD.Substring(0, 3) + "/" + dtoItem.Season.Replace("/", "-");
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    scope.Commit();
                                }
                            }
                        }                        
                        
                        context.QuotationDetail.Local.Where(o => o.Quotation == null).ToList().ForEach(o => context.QuotationDetail.Remove(o));
                        context.QuotationOffer.Local.Where(o => o.Quotation == null).ToList().ForEach(o => context.QuotationOffer.Remove(o));
                        context.QuotationOfferDetail.Local.Where(o => o.QuotationOffer == null || o.QuotationDetail == null).ToList().ForEach(o => context.QuotationOfferDetail.Remove(o));
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.QuotationID, 0, string.Empty, new List<int>(), 0, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = Library.Helper.GetInnerException(ex).Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.QuotationMng.Quotation dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.QuotationMng.Quotation dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.QuotationMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.QuotationMng.SearchFilterData data = new DTO.QuotationMng.SearchFilterData();
            data.Factories = new List<DTO.Support.Factory>();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                // insert missing data for factory order
                using (QuotationMngEntities context = CreateContext())
                {
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT TOP 1 * FROM QuotationDetail WITH (TABLOCKX, HOLDLOCK)");
                        try
                        {
                            context.QuotationMng_function_InsertMissingFactoryOrderItem();
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            scope.Commit();
                        }
                    }
                }

                data.Factories = supportFactory.GetFactory().ToList();
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.QuotationMng.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.QuotationMng.InitFormData data = new DTO.QuotationMng.InitFormData();
            data.Factories = new List<DTO.Support.Factory>();
            data.Orders = new List<DTO.QuotationMng.FactoryOrderSearchResult>();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                Task task1 = Task.Factory.StartNew(() => { data.Seasons = supportFactory.GetSeason().ToList(); });
                Task task2 = Task.Factory.StartNew(() => {
                    data.Factories = supportFactory.GetAuthorizedFactories(userId).ToList();
                    using (QuotationMngEntities context = CreateContext())
                    {
                        List<int> factoryIds = new List<int>();
                        factoryIds.AddRange(data.Factories.Select(o => o.FactoryID).ToList());
                        data.Orders = converter.DB2DTO_FactoryOrder(context.QuotationMng_FactoryOrderSearchResult_View.Where(o => o.FactoryID.HasValue && factoryIds.Contains(o.FactoryID.Value)).OrderBy(o => o.ProductionStatus).ToList());
                    }
                });
                Task.WaitAll(task1, task2);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.QuotationMng.EditFormData GetData(int id, int factoryID, string season, List<int> orders, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.QuotationMng.EditFormData data = new DTO.QuotationMng.EditFormData();
            data.Data = new DTO.QuotationMng.Quotation();
            data.Data.QuotationDetails = new List<DTO.QuotationMng.QuotationDetail>();
            data.Data.QuotationOffers = new List<DTO.QuotationMng.QuotationOffer>();
            data.DeliveryTerms = new List<DTO.Support.DeliveryTerm>();
            data.PaymentTerms = new List<DTO.Support.PaymentTerm>();
            data.OfferDirections = new List<DTO.Support.OfferDirection>();
            data.PriceDifferences = new List<DTO.Support.PriceDifference>();
            data.QuotationStatuses = new List<DTO.Support.QuotationStatus>();

            //try to get data
            try
            {
                using (QuotationMngEntities context = CreateContext())
                {
                    // add new case
                    if (id == 0)
                    {
                        data.Data.Season = season;

                        DTO.Support.Factory dtoFactory = supportFactory.GetFactory().FirstOrDefault(o => o.FactoryID == factoryID);
                        data.Data.FactoryID = factoryID;
                        data.Data.FactoryUD = dtoFactory.FactoryUD;
                        data.Data.FactoryName = dtoFactory.FactoryName;
                        data.Data.QuotationUD = "---/" + dtoFactory.FactoryUD.Substring(0, 3) + "/" + season.Replace("/", "-");

                        int index = -1;
                        foreach (QuotationMng_FactoryOrderDetailSearchResult_View detail in context.QuotationMng_FactoryOrderDetailSearchResult_View.Where(o => o.FactoryOrderID.HasValue && orders.Contains(o.FactoryOrderID.Value)))
                        {
                            DTO.QuotationMng.QuotationDetail dtoDetail = new DTO.QuotationMng.QuotationDetail();
                            dtoDetail.QuotationDetailID = index;
                            dtoDetail.ArticleCode = detail.ArticleCode;
                            dtoDetail.Description = detail.Description;
                            dtoDetail.FactoryOrderDetailID = detail.FactoryOrderDetailID;
                            dtoDetail.FactoryOrderSparepartDetailID = detail.FactoryOrderSparepartDetailID;
                            dtoDetail.ClientUD = detail.ClientUD;
                            dtoDetail.FactoryOrderUD = detail.FactoryOrderUD;
                            if (detail.LDS.HasValue)
                            {
                                dtoDetail.LDS = detail.LDS.Value.ToString("dd/MM/yyyy");
                            }
                            dtoDetail.StatusID = 1; // hardcode for PENDING
                            dtoDetail.QuotationStatusNM = "PENDING";
                            dtoDetail.StatusUpdatedBy = userId;
                            dtoDetail.StatusUpdatedDate = DateTime.Now.ToString("dd/MM/yyyy");
                            dtoDetail.UpdatorName = supportFactory.GetUser().FirstOrDefault(o => o.UserID == userId).FullName;
                            dtoDetail.UpdatorName2 = supportFactory.GetUser().FirstOrDefault(o => o.UserID == userId).FullName;
                            data.Data.QuotationDetails.Add(dtoDetail);

                            index--;
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_Quotation(context.QuotationMng_Quotation_View.Include("QuotationMng_QuotationDetail_View").Include("QuotationMng_QuotationOffer_View").Include("QuotationMng_QuotationOffer_View.QuotationMng_QuotationOfferDetail_View").FirstOrDefault(o => o.QuotationID == id));
                        season = data.Data.Season;
                    }
                }

                Task task2 = Task.Factory.StartNew(() => { data.DeliveryTerms = supportFactory.GetDeliveryTerm().ToList(); });
                Task task3 = Task.Factory.StartNew(() => { data.PaymentTerms = supportFactory.GetPaymentTerm().ToList(); });
                Task task4 = Task.Factory.StartNew(() => { data.OfferDirections = supportFactory.GetOfferDirection().ToList(); });
                Task task5 = Task.Factory.StartNew(() => { data.PriceDifferences = supportFactory.GetPriceDifference(season).ToList(); });
                Task task6 = Task.Factory.StartNew(() => { data.QuotationStatuses = supportFactory.GetQuotationStatus().ToList(); });
                Task.WaitAll(task2, task3, task4, task5, task6);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        // 
        // REPORT FUNCTION
        //
        public string GetFactoryQuotationReportData(int QuotationID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("QuotationMng_function_GetFactoryQuotationReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@QuotationID", QuotationID);
                adap.TableMappings.Add("Table", "QuotationMng_ReportHeaderData_View");
                adap.TableMappings.Add("Table1", "QuotationMng_FactoryQuotationReportData_View");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "FactoryQuotation");
                //return string.Empty;

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "FactoryQuotation");
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

        public string GetEurofarQuotationReportData(int QuotationID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("QuotationMng_function_GetEurofarQuotationReportData", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@QuotationID", QuotationID);
                adap.TableMappings.Add("Table", "QuotationMng_ReportHeaderData_View");
                adap.TableMappings.Add("Table1", "QuotationMng_EurofarQuotationReportData_View");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "EurofarQuotation");
                //return string.Empty;

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "EurofarQuotation");
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
    }
}
