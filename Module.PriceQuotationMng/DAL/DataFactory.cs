using Library.Base;
using Module.PriceQuotationMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using System.Collections;
using Library;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Core.Objects;

namespace Module.PriceQuotationMng.DAL
{
    public class DataFactory : DataFactory<SearchFormData, EditFormData>
    {
        private DataConverter converter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.EditOfferQuotationData data = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.EditOfferQuotationData>();
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    int? offerDirectionID = CheckIsFactory(context, userId) ? 1 : 2;
                    int? quotationID = data.QuotationID;
                    int? quotationDetailID = data.QuotationDetailID;

                    var approve = context.PriceQuotationMng_ApprovePrice(offerDirectionID, quotationID, quotationDetailID, userId);
                    dtoItem = converter.DB2DTO_PriceQuotationSearchResult(approve.ToList());

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
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
            EditOfferQuotationData data = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<EditOfferQuotationData>();
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    //bool isCheckFactory = CheckIsFactory(context, userId);
                    bool isCheckFactory = true;

                    // Case change price difference at pop-up quotation in factory
                    var dbQuotation = context.Quotation.FirstOrDefault(o => o.QuotationID == data.QuotationID);
                    var priceDifference = context.PriceDifference.FirstOrDefault(o => o.PriceDifferenceUD == data.PriceDifferenceCode && o.Season == dbQuotation.Season);
                    if (priceDifference == null) throw new Exception("Quality code is missing!");
                    data.PriceDifferenceRate = priceDifference.Rate;

                    if (data.Price != null)
                    {
                        // QuotationOffer.
                        QuotationOffer quotationOffer = new QuotationOffer
                        {
                            QuotationID = data.QuotationID,
                            QuotationOfferDate = DateTime.Now,
                            QuotationOfferVersion = context.QuotationOffer.Where(o => o.QuotationID == data.QuotationID).ToList().Count + 1,
                            QuotationOfferDirectionID = (!isCheckFactory) ? 2 : 1,
                            UpdatedBy = userId,
                            UpdatedDate = DateTime.Now
                        };
                        context.QuotationOffer.Add(quotationOffer);

                        // QuotationOfferDetail
                        QuotationOfferDetail quotationOfferDetail = new QuotationOfferDetail()
                        {
                            QuotationOfferID = quotationOffer.QuotationOfferID,
                            QuotationDetailID = data.QuotationDetailID,
                            Price = data.Price,
                            Remark = data.Remark
                        };
                        context.QuotationOfferDetail.Add(quotationOfferDetail);
                    }

                    //QuotationDetail
                    QuotationDetail quotationDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == data.QuotationDetailID);
                    if (data.Price != null)
                    {
                        if (isCheckFactory)
                        {
                            quotationDetail.PurchasingPrice = data.Price;
                            quotationDetail.SalePrice = data.Price * (1 + data.PriceDifferenceRate);

                            if (data.Price == quotationDetail.TargetPrice) // Compare with target price
                            {
                                quotationDetail.StatusID = 3; // 3: Confirm
                                quotationDetail.StatusUpdatedBy = userId;
                                quotationDetail.StatusUpdatedDate = DateTime.Now;
                            }
                        }
                        else
                        {
                            quotationDetail.TargetPrice = data.Price /*/ (1 + data.PriceDifferenceRate)*/;

                            if (quotationDetail.SalePrice != data.SalePrice)
                            {
                                quotationDetail.SalePrice = data.SalePrice;

                                // Update Quotation Offer Detail Last
                                QuotationOfferDetail lastQuotationOfferDetail = context.QuotationOfferDetail.Where(o => o.QuotationDetailID == data.QuotationDetailID).OrderByDescending(o => o.QuotationOfferDetailID).FirstOrDefault();
                                lastQuotationOfferDetail.Price = data.SalePrice;
                            }

                            if (quotationDetail.TargetPrice == quotationDetail.SalePrice)  // Compare with sale price
                            {
                                quotationDetail.StatusID = 3; // 3: Confirm
                                quotationDetail.StatusUpdatedBy = userId;
                                quotationDetail.StatusUpdatedDate = DateTime.Now;
                            }
                        }

                        quotationDetail.PriceUpdatedBy = userId;
                        quotationDetail.PriceUpdatedDate = DateTime.Now;
                    }

                    quotationDetail.Remark = data.Remark;

                    if (!isCheckFactory)
                    {
                        quotationDetail.PriceDifferenceCode = data.PriceDifferenceCode;
                        quotationDetail.PriceDifferenceRate = data.PriceDifferenceRate;
                        quotationDetail.OldPrice1 = data.OldPrice1;
                        quotationDetail.OldPrice2 = data.OldPrice2;
                        quotationDetail.OldPrice3 = data.OldPrice3;
                        quotationDetail.OldPriceRemark = data.OldPriceRemark;
                    }

                    context.SaveChanges();

                    dtoItem = AutoMapper.Mapper.Map<PriceQuotationMng_PriceQuotationSearchResult_View, DTO.PriceQuotationSearchResultData>(context.PriceQuotationMng_PriceQuotationSearchResult_View.FirstOrDefault(o => o.QuotationDetailID == data.QuotationDetailID));

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            SearchFormData data = new SearchFormData
            {
                Data = new List<PriceQuotationSearchResultData>()
            };

            notification = new Notification
            {
                Type = NotificationType.Success
            };

            totalRows = 0;

            try
            {
                string season = (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString().Replace("'", "''"))) ? filters["Season"].ToString().Trim() : null;
                string clientUD = (filters.ContainsKey("ClientUD") && filters["ClientUD"] != null && !string.IsNullOrEmpty(filters["ClientUD"].ToString().Replace("'", "''"))) ? filters["ClientUD"].ToString().Trim() : null;
                string factoryUD = (filters.ContainsKey("FactoryUD") && filters["FactoryUD"] != null && !string.IsNullOrEmpty(filters["FactoryUD"].ToString().Replace("'", "''"))) ? filters["FactoryUD"].ToString().Trim() : null;
                string articleCode = (filters.ContainsKey("ArticleCode") && filters["ArticleCode"] != null && !string.IsNullOrEmpty(filters["ArticleCode"].ToString().Replace("'", "''"))) ? filters["ArticleCode"].ToString().Trim() : null;
                string description = (filters.ContainsKey("Description") && filters["Description"] != null && !string.IsNullOrEmpty(filters["Description"].ToString().Replace("'", "''"))) ? filters["Description"].ToString().Trim() : null;
                string priceDifferenceCode = (filters.ContainsKey("PriceDifferenceCode") && filters["PriceDifferenceCode"] != null && !string.IsNullOrEmpty(filters["PriceDifferenceCode"].ToString().Replace("'", "''"))) ? filters["PriceDifferenceCode"].ToString().Trim() : null;
                int? quotationStatusID = (filters.ContainsKey("QuotationStatusID") && filters["QuotationStatusID"] != null && !string.IsNullOrEmpty(filters["QuotationStatusID"].ToString().Replace("'", "''"))) ? (int?)Convert.ToInt32(filters["QuotationStatusID"].ToString().Trim()) : null;
                string orderNo = (filters.ContainsKey("OrderNumber") && filters["OrderNumber"] != null && !string.IsNullOrEmpty(filters["OrderNumber"].ToString().Replace("'", "''"))) ? filters["OrderNumber"].ToString().Trim() : null;

                using (var context = CreateContext())
                {
                    if (!string.IsNullOrEmpty(season))
                    {
                        context.PriceQuotationMng_AddAllItemToQuotation(season);
                    }

                    Framework.DAL.DataFactory factory = new Framework.DAL.DataFactory();
                    data.IsCompany = context.SupportMng_InternalCompany2_View.Select(o => o.InternalCompanyID).Contains(factory.GetCompanyID(userID)) ? null : (int?)1;

                    totalRows = context.PriceQuotationMng_function_PriceQuotationSearchResult(clientUD, factoryUD, articleCode, description, priceDifferenceCode, quotationStatusID, season, orderBy, orderDirection, userID, data.IsCompany, orderNo).Count();
                    data.Data = converter.DB2DTO_PriceQuotationSearchResult(context.PriceQuotationMng_function_PriceQuotationSearchResult(clientUD, factoryUD, articleCode, description, priceDifferenceCode, quotationStatusID, season, orderBy, orderDirection, userID, data.IsCompany, orderNo).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return null;
            }
        }

        public object GetSeasonWithFilter(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                string factoryOrderDetailID = (filters.ContainsKey("FactoryOrderDetailID") && filters["FactoryOrderDetailID"] != null && !string.IsNullOrEmpty(filters["FactoryOrderDetailID"].ToString())) ? filters["FactoryOrderDetailID"].ToString().Replace("'", "''") : null;
                string factoryOrderSparepartDetailID = (filters.ContainsKey("FactoryOrderSparepartDetailID") && filters["FactoryOrderSparepartDetailID"] != null && !string.IsNullOrEmpty(filters["FactoryOrderSparepartDetailID"].ToString())) ? filters["FactoryOrderSparepartDetailID"].ToString().Replace("'", "''") : null;
                string modelID = (filters.ContainsKey("ModelID") && filters["ModelID"] != null && !string.IsNullOrEmpty(filters["ModelID"].ToString())) ? filters["ModelID"].ToString().Replace("'", "''") : null;
                string frameMaterialID = (filters.ContainsKey("FrameMaterialID") && filters["FrameMaterialID"] != null && !string.IsNullOrEmpty(filters["FrameMaterialID"].ToString())) ? filters["FrameMaterialID"].ToString().Replace("'", "''") : null;
                string materialID = (filters.ContainsKey("MaterialID") && filters["MaterialID"] != null && !string.IsNullOrEmpty(filters["MaterialID"].ToString())) ? filters["MaterialID"].ToString().Replace("'", "''") : null;
                string materialTypeID = (filters.ContainsKey("MaterialTypeID") && filters["MaterialTypeID"] != null && !string.IsNullOrEmpty(filters["MaterialTypeID"].ToString())) ? filters["MaterialTypeID"].ToString().Replace("'", "''") : null;
                string season = (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString())) ? filters["Season"].ToString().Replace("'", "''") : null;

                using (var context = CreateContext())
                {
                    int? model = !string.IsNullOrEmpty(modelID) ? Convert.ToInt32(modelID) : (int?)null;
                    int? frameMaterial = !string.IsNullOrEmpty(frameMaterialID) ? Convert.ToInt32(frameMaterialID) : (int?)null;
                    int? material = !string.IsNullOrEmpty(materialID) ? Convert.ToInt32(materialID) : (int?)null;
                    int? materialType = !string.IsNullOrEmpty(materialTypeID) ? Convert.ToInt32(materialTypeID) : (int?)null;

                    if (!string.IsNullOrEmpty(factoryOrderDetailID))
                        return context.PriceQuotationMng_function_GetPriceSeasonWithFactoryOrderDetail(season, model, frameMaterial, material, materialType).ToList();

                    if (!string.IsNullOrEmpty(factoryOrderSparepartDetailID))
                        return context.PriceQuotationMng_function_GetPriceSeasonWithFactoryOrderSparepartDetail(season, model, frameMaterial, material, materialType).ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return null;
        }

        public bool UpdateAllDifferenceCode(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                string ids = (filters.ContainsKey("quotationDetailIDs") && filters["quotationDetailIDs"] != null && !string.IsNullOrEmpty(filters["quotationDetailIDs"].ToString().Replace("'", "''"))) ? filters["quotationDetailIDs"].ToString().Trim() : null;
                int? id = (filters.ContainsKey("priceDifferenceID") && filters["priceDifferenceID"] != null && !string.IsNullOrEmpty(filters["priceDifferenceID"].ToString().Replace("'", "''"))) ? (int?)Convert.ToInt32(filters["priceDifferenceID"].ToString().Trim()) : null;

                using (var context = CreateContext())
                {
                    ObjectParameter output = new ObjectParameter("ReturnData", typeof(int));
                    context.PriceQuotationMng_function_ApplyAllDiffCode(ids, id, output);

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public EditFormData GetData(Hashtable filters, out Notification notification)
        {
            EditFormData editData = new EditFormData()
            {
                Data = new EditOfferQuotationData(),
                HistoryQuotationPrices = new List<HistoryPriceQuotationData>(),
                PriceDifferences = new List<Support.DTO.PriceDifference>()
            };
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            int quotationID = 0;
            int quotationDetailID = 0;

            if (filters.ContainsKey("QuotationID") && filters["QuotationID"] != null && !string.IsNullOrEmpty(filters["QuotationID"].ToString()))
            {
                quotationID = Convert.ToInt32(filters["QuotationID"].ToString());
            }

            if (filters.ContainsKey("QuotationDetailID") && filters["QuotationDetailID"] != null && !string.IsNullOrEmpty(filters["QuotationDetailID"].ToString()))
            {
                quotationDetailID = Convert.ToInt32(filters["QuotationDetailID"].ToString());
            }

            using (var context = CreateContext())
            {
                var rsQuotationDetail = context.PriceQuotationMng_PriceQuotationSearchResult_View
                    .FirstOrDefault(o => o.QuotationID == quotationID && o.QuotationDetailID == quotationDetailID);

                if (rsQuotationDetail != null)
                {
                    editData.Data.ArticleCode = rsQuotationDetail.ArticleCode;
                    editData.Data.Description = rsQuotationDetail.Description;

                    editData.Data.OldPrice1 = rsQuotationDetail.OldPrice1;
                    editData.Data.OldPrice2 = rsQuotationDetail.OldPrice2;
                    editData.Data.OldPrice3 = rsQuotationDetail.OldPrice3;

                    editData.Data.PriceDifferenceCode = rsQuotationDetail.PriceDifferenceCode;
                    editData.Data.PriceDifferenceRate = rsQuotationDetail.PriceDifferenceRate;
                    editData.Data.Remark = rsQuotationDetail.Remark;

                    editData.Data.SalePrice = rsQuotationDetail.SalePrice;
                    editData.Data.OldPriceRemark = rsQuotationDetail.OldPriceRemark;
                }

                var rsHistoryQuotationPrices = context.PriceQuotationMng_HistoryPriceQuotation_View
                    .Where(o => o.QuotationID == quotationID && o.QuotationDetailID == quotationDetailID)
                    .OrderByDescending(o => o.UpdatedDate)
                    .ToList();

                editData.HistoryQuotationPrices = converter.DB2DTO_HistoryPriceQuotation(rsHistoryQuotationPrices);

                Support.DAL.DataFactory factory = new Support.DAL.DataFactory();
                editData.PriceDifferences = factory.GetPriceDifference(Library.OMSHelper.Helper.GetCurrentSeason());
            }

            return editData;
        }

        public List<PriceQuotationSearchResultData> GetPricePreviousSeason(Hashtable filters, out Notification notification)
        {
            List<PriceQuotationSearchResultData> data = null;
            notification = new Notification()
            {
                Type = NotificationType.Error
            };

            string quotationDetailIDs = null;
            string season = null;

            if (filters.ContainsKey("QuotationDetailIDs") && filters["QuotationDetailIDs"] != null && !string.IsNullOrEmpty(filters["QuotationDetailIDs"].ToString().Replace("'", "''")))
            {
                quotationDetailIDs = filters["QuotationDetailIDs"].ToString().Trim();
            }

            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString().Replace("'", "''")))
            {
                season = filters["Season"].ToString().Trim();
            }

            if (string.IsNullOrEmpty(quotationDetailIDs))
            {
                notification.Type = NotificationType.Error;
                notification.Message = "Data don't have to update";
                return null;
            }

            if (string.IsNullOrEmpty(season))
            {
                notification.Type = NotificationType.Error;
                notification.Message = "Season is invalid to update";
                return null;
            }

            try
            {
                using (var context = CreateContext())
                {
                    var result = context.PriceQuotationMng_function_UpdatePricePrevious(quotationDetailIDs, season);
                    data = converter.DB2DTO_PriceQuotationSearchResult(result.ToList());
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

        public bool UnConfirmData(int userID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            int quotationDetailID = id;

            try
            {
                using (var context = CreateContext())
                {
                    QuotationDetail dbItem = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == quotationDetailID);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Data is not found!";
                        return false;
                    }

                    dbItem.StatusID = 1; // Pending
                    dbItem.StatusUpdatedBy = userID;
                    dbItem.StatusUpdatedDate = DateTime.Now;

                    context.SaveChanges();

                    dtoItem = AutoMapper.Mapper.Map<PriceQuotationMng_PriceQuotationSearchResult_View, DTO.PriceQuotationSearchResultData>(context.PriceQuotationMng_PriceQuotationSearchResult_View.FirstOrDefault(o => o.QuotationDetailID == quotationDetailID));

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        private PriceQuotationMngEntities CreateContext()
        {
            PriceQuotationMngEntities context = new PriceQuotationMngEntities(Helper.CreateEntityConnectionString("DAL.PriceQuotationMngModel"));
            context.Database.CommandTimeout= 180;
            return context;
        }

        private bool CheckIsFactory(PriceQuotationMngEntities context, int userId)
        {
            Framework.DAL.DataFactory factory = new Framework.DAL.DataFactory();
            int? companyLogin = factory.GetCompanyID(userId);
            List<int?> companyInternals = context.SupportMng_InternalCompany2_View.Select(o => o.InternalCompanyID).ToList();

            return (!companyInternals.Contains(companyLogin));
        }

        public InitFormData GetInitData(Hashtable filters, out Notification notification)
        {
            InitFormData data = new InitFormData()
            {
                Season = new List<Support.DTO.Season>(),
                PriceDifference = new List<Support.DTO.PriceDifference>(),
                QuotationStatus = new List<Support.DTO.QuotationStatus>()
            };

            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            string season = (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString().Replace("'", "''"))) ? filters["Season"].ToString().Trim() : Helper.GetCurrentSeason();

            try
            {
                Support.DAL.DataFactory factory = new Support.DAL.DataFactory();

                data.Season = factory.GetSeason();
                data.PriceDifference = factory.GetPriceDifference(season);
                data.QuotationStatus = factory.GetQuotationStatus();

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return null;
            }
        }

        public List<PriceQuotationSearchResultData> UpdateData(int userId, object dtoItems, out Notification notification)
        {
            string keyIds = "";

            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            List<PriceQuotationSearchResultData> dataItems = ((Newtonsoft.Json.Linq.JArray)dtoItems).ToObject<List<PriceQuotationSearchResultData>>();

            using (var context = CreateContext())
            {
                //bool isFactory = CheckIsFactory(context, userId);
                bool isFactory = true;

                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        for (int i = 0; i < dataItems.Count; i++)
                        {
                            var dataItem = dataItems[i];
                            if (dataItem.QuotationStatusID.Value == 3)
                            {
                                // skip processing if price already confirmed
                                continue;
                            }

                            if (i > 0)
                            {
                                keyIds = keyIds + ",";
                            }

                            // Quotation Offer
                            QuotationOffer offer = new QuotationOffer()
                            {
                                QuotationID = dataItem.QuotationID,
                                QuotationOfferDate = DateTime.Now,
                                QuotationOfferVersion = context.QuotationOffer.Where(o => o.QuotationID == dataItem.QuotationID).ToList().Count + 1,
                                QuotationOfferDirectionID = (!isFactory) ? 2 : 1,
                                UpdatedBy = userId,
                                UpdatedDate = DateTime.Now

                            };

                            context.QuotationOffer.Add(offer);

                            // Quotation Offer Detail
                            QuotationOfferDetail offerDetail = new QuotationOfferDetail()
                            {
                                QuotationOfferID = offer.QuotationOfferID,
                                QuotationDetailID = dataItem.QuotationDetailID,
                                Price = dataItem.Price
                            };

                            context.QuotationOfferDetail.Add(offerDetail);

                            // Quotation Detail
                            QuotationDetail quotationDetail = context.QuotationDetail.FirstOrDefault(o => o.QuotationDetailID == dataItem.QuotationDetailID);

                            if (isFactory)
                            {
                                quotationDetail.PurchasingPrice = dataItem.Price;
                                quotationDetail.SalePrice = dataItem.Price * (1 + quotationDetail.PriceDifferenceRate);

                                if (dataItem.Price == quotationDetail.TargetPrice)
                                {
                                    quotationDetail.StatusID = 3;
                                    quotationDetail.StatusUpdatedBy = userId;
                                    quotationDetail.StatusUpdatedDate = DateTime.Now;
                                }
                            }
                            else
                            {
                                quotationDetail.TargetPrice = dataItem.Price;

                                if (quotationDetail.SalePrice != dataItem.SalePrice)
                                {
                                    quotationDetail.SalePrice = dataItem.SalePrice;
                                }

                                if (quotationDetail.TargetPrice == quotationDetail.SalePrice)
                                {
                                    quotationDetail.StatusID = 3;
                                    quotationDetail.StatusUpdatedBy = userId;
                                    quotationDetail.StatusUpdatedDate = DateTime.Now;
                                }
                            }

                            quotationDetail.PriceUpdatedBy = userId;
                            quotationDetail.PriceUpdatedDate = DateTime.Now;

                            keyIds = keyIds + dataItem.QuotationDetailID.ToString();

                            context.SaveChanges();
                        }

                        trans.Commit();

                        return converter.DB2DTO_PriceQuotationSearchResult(context.PriceQuotationMng_PriceQuotationSearchResult_View.Where(o => keyIds.Contains(o.QuotationDetailID.ToString())).ToList());
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();

                        notification.Type = NotificationType.Error;
                        notification.Message = ex.Message;

                        return new List<PriceQuotationSearchResultData>();
                    }
                }
            }
        }
    }
}
