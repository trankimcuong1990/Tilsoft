using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotationMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryQuotationMngEntities CreateContext()
        {
            return new FactoryQuotationMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryQuotationMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.QuotationSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryQuotationMngEntities context = CreateContext())
                {
                    string QuotationUD = null;
                    string Season = null;
                    int? FactoryID = null;
                    int UserID = -1;
                    string FactoryOrderUD = null;
                    int? StatusID = null;
                    string ArticleCode = null;
                    string Description = null;
                    if (filters.ContainsKey("QuotationUD") && !string.IsNullOrEmpty(filters["QuotationUD"].ToString()))
                    {
                        QuotationUD = filters["QuotationUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    if (filters.ContainsKey("FactoryOrderUD") && !string.IsNullOrEmpty(filters["FactoryOrderUD"].ToString()))
                    {
                        FactoryOrderUD = filters["FactoryOrderUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("StatusID") && !string.IsNullOrEmpty(filters["StatusID"].ToString()))
                    {
                        StatusID = Convert.ToInt32(filters["StatusID"].ToString());
                    }
                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        Description = filters["Description"].ToString().Replace("'", "''");
                    }
                    totalRows = context.FactoryQuotationMng_function_SearchQuotation(QuotationUD, Season, FactoryID, UserID, FactoryOrderUD, StatusID, ArticleCode, Description, orderBy, orderDirection).Count();
                    var result = context.FactoryQuotationMng_function_SearchQuotation(QuotationUD, Season, FactoryID, UserID, FactoryOrderUD, StatusID, ArticleCode, Description, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_QuotationSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                DTO.Quotation dtoQuotation = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Quotation>();

                // check permission
                if (id > 0 && fwFactory.CheckQuotationPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected quotation data");
                }

                using (FactoryQuotationMngEntities context = CreateContext())
                {
                    Quotation dbItem = context.Quotation.FirstOrDefault(o => o.QuotationID == id);

                    if (dbItem == null)
                    {
                        throw new Exception("Quotation does not exists!");
                    }

                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM QuotationOffer WITH (TABLOCKX, HOLDLOCK); SELECT * FROM QuotationDetail WITH (TABLOCKX, HOLDLOCK);");

                        try
                        {
                            int? maxVersion = context.QuotationOffer.Where(o => o.QuotationID == id).Max(o => o.QuotationOfferVersion);

                            QuotationOffer dbOfferItem = new QuotationOffer();
                            dbOfferItem.QuotationID = id;
                            dbOfferItem.QuotationOfferDirectionID = 1; // factory -> furnindo
                            dbOfferItem.UpdatedBy = userId;
                            dbOfferItem.UpdatedDate = DateTime.Now;
                            dbOfferItem.QuotationOfferDate = DateTime.Now;
                            if (maxVersion.HasValue)
                            {
                                dbOfferItem.QuotationOfferVersion = maxVersion.Value + 1;
                            }
                            else
                            {
                                dbOfferItem.QuotationOfferVersion = 1;
                            }

                            bool isOK = false;
                            foreach (DTO.QuotationDetail detail in dtoQuotation.QuotationDetails.Where(o => o.NewOfferPrice.HasValue))
                            {
                                if (detail.FactoryOrderDetailID.HasValue)
                                {
                                    if (fwFactory.CheckFactoryOrderDetailPermission(userId, detail.FactoryOrderDetailID.Value) > 0)
                                    {
                                        isOK = true;
                                    }
                                }
                                if (detail.FactoryOrderSparepartDetailID.HasValue)
                                {
                                    if (fwFactory.CheckFactoryOrderSparepartDetailPermission(userId, detail.FactoryOrderSparepartDetailID.Value) > 0)
                                    {
                                        isOK = true;
                                    }
                                }
                                if (!isOK)
                                {
                                    throw new Exception("Invalid permission!");
                                }

                                // update quotation detail purchasing price
                                QuotationDetail dbDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == detail.QuotationDetailID);
                                if (dbDetail != null && Decimal.TryParse(detail.NewOfferPrice.Value.ToString(), out decimal tmpDecimal))
                                {
                                    QuotationOfferDetail dbOfferDetailItem = new QuotationOfferDetail();
                                    dbOfferDetailItem.QuotationOffer = dbOfferItem;
                                    dbOfferDetailItem.QuotationDetailID = detail.QuotationDetailID;
                                    dbOfferDetailItem.Price = tmpDecimal;
                                    dbOfferItem.QuotationOfferDetail.Add(dbOfferDetailItem);

                                    dbDetail.StatusID = 1;
                                    dbDetail.PurchasingPrice = detail.NewOfferPrice.Value;
                                    dbDetail.SalePrice = detail.NewOfferPrice.Value + (detail.NewOfferPrice.Value * dbDetail.PriceDifferenceRate);
                                    dbDetail.PriceUpdatedDate = DateTime.Now;
                                    dbDetail.PriceUpdatedBy = userId;
                                }
                            }
                            context.QuotationOffer.Add(dbOfferItem);
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

                    dtoItem = GetData(userId, id, out notification).Data;
                    return true;
                }
            }
            catch (Newtonsoft.Json.JsonReaderException jex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Data input validation failed !";
                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTIONS
        //
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.QuotationStatuses = new List<Support.DTO.QuotationStatus>();
            data.Seasons = new List<Support.DTO.Season>();
            data.Factories = new List<Support.DTO.Factory>();

            try
            {
                data.QuotationStatuses = supportFactory.GetQuotationStatus();
                data.Seasons = supportFactory.GetSeason();
                data.Factories = supportFactory.GetFactory(userId);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.PreviewFormData GetPreviewData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PreviewFormData data = new DTO.PreviewFormData();
            data.Data = new List<DTO.QuotationDetailSearchResult>();

            //try to get data
            try
            {
                using (FactoryQuotationMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_QuotationDetailSearchResultList(context.FactoryQuotationMng_QuotationDetailSearchResult_View.Where(o => o.QuotationID == id).OrderBy(o=>o.Description).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool CheckAuthentication(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            bool result = false;
            try
            {
                using (FactoryQuotationMngEntities context = CreateContext())
                {
                    Quotation dbItem = context.Quotation.FirstOrDefault(o => o.QuotationID == id);
                    if (dbItem != null && dbItem.FactoryID != null)
                    {
                        foreach (Support.DTO.Factory factory in supportFactory.GetFactory(userId))
                        {
                            if (factory.FactoryID == dbItem.FactoryID.Value)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Quotation does not exists");
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return result;
        }

        public DTO.EditFormData GetData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.Quotation();

            //try to get data
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckQuotationPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected quotation data");
                }

                using (FactoryQuotationMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_Quotation(context.FactoryQuotationMng_Quotation_View
                           .Include("FactoryQuotationMng_QuotationDetail_View")
                           .Include("FactoryQuotationMng_QuotationDetail_View.FactoryQuotationMng_QuotationOfferDetail_View")
                           .FirstOrDefault(o => o.QuotationID == id));
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
