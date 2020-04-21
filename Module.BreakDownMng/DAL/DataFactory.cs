using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Module.BreakDownMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();       

        private BreakDownMngEntities CreateContext()
        {
            return new BreakDownMngEntities(Library.Helper.CreateEntityConnectionString("DAL.BreakDownMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            searchFormData.Exchange = 1;

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                //int? companyID = null;
                string breakDownUD = null;
                int? type = null;
                string clientUD = null;
                string description = null;
                string season = null;
                int? modelStatusID = null;
                string defaultFactory = null;

                Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
                //companyID = fw_factory.GetCompanyID(userId);

                if (filters.ContainsKey("breakDownUD") && !string.IsNullOrEmpty(filters["breakDownUD"].ToString()))
                {
                    breakDownUD = filters["breakDownUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("type") && !string.IsNullOrEmpty(filters["type"].ToString()))
                {
                    type = Convert.ToInt32(filters["type"]);
                }
                if (filters.ContainsKey("clientUD") && !string.IsNullOrEmpty(filters["clientUD"].ToString()))
                {
                    clientUD = filters["clientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
                {
                    description = filters["description"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("season") && filters["season"]!= null && !string.IsNullOrEmpty(filters["season"].ToString()))
                {
                    season = filters["season"].ToString().Replace("'", "''");
                }
                if(filters.ContainsKey("modelStatusID") && filters["modelStatusID"]!= null && !string.IsNullOrEmpty(filters["modelStatusID"].ToString()))
                {
                    modelStatusID = Convert.ToInt32(filters["modelStatusID"]);
                }
                if (filters.ContainsKey("defaultFactory") && !string.IsNullOrEmpty(filters["defaultFactory"].ToString()))
                {
                    defaultFactory = filters["defaultFactory"].ToString().Replace("'", "''");
                }

                using (BreakDownMngEntities context = CreateContext())
                {
                    try
                    {
                        searchFormData.Exchange = context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault().ExchangeRate.Value;
                    }
                    catch { }

                    var resultSet = context.BreakDownMng_function_SearchBreakDown(breakDownUD, type, clientUD, description, season, orderBy, orderDirection, modelStatusID, defaultFactory).ToList();
                    totalRows = resultSet.Count();
                    searchFormData.TotalAVFConfirmedItem = resultSet.Count(o => o.AVFBreakdownAmountVND.HasValue);
                    searchFormData.TotalAVTConfirmedItem = resultSet.Count(o => o.AVTBreakdownAmountVND.HasValue);
                    searchFormData.Data = converter.DB2DTO_BreakDownSearch(resultSet.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return searchFormData;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return searchFormData;
            }
        }
        public DTO.SearchFormData GetCaculation(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.CaculatePriceUSDs = new List<DTO.CaculatePriceUSD>();
            data.CalculationPriceMangementCosts = new List<DTO.CalculationPriceMangementCost>();
            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    data.CaculatePriceUSDs = converter.DB2DTO_ListCaculate(context.BreakDownMng_LoadDetailCalculationPriceUSD_View.ToList());
                }
                using (BreakDownMngEntities context = CreateContext())
                {
                    data.CalculationPriceMangementCosts = converter.DB2DTO_ListCalculationPriceMangementCost(context.BreakdownMng_LoadDetailCalculationPriceMangementCost_View.ToList());
                }
                using (BreakDownMngEntities context = CreateContext())
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
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTIONS
        //
        public DTO.EditFormData GetData(int userId, int id, int modelId, int sampleProductId, int offerSeasonDetailID, int factoryId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.ExchangeRate = 1;
            data.FactoryDTOs = new List<DTO.FactoryDTO>();
            data.Data = new DTO.BreakdownDTO();
            data.Data.BreakdownCategoryOptionDTOs = new List<DTO.BreakdownCategoryOptionDTO>();
            data.BreakdownCategoryDTOs = new List<DTO.BreakdownCategoryDTO>();
            int companyId = fwFactory.GetCompanyID(userId).Value;
            if (companyId != 1)
            {
                companyId = 3;
            }
            int[] supportCompanyIDs = { 1, 3 };

            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    data.BreakdownCategoryDTOs = converter.DB2DTO_BreakdownCategoryDTOList(context.BreakdownMng_BreakdownCategory_View.OrderBy(o => o.DisplayOrder).ToList());
                    data.ExchangeRate = context.MasterExchangeRateMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault().ExchangeRate.Value;
                    data.FactoryDTOs = converter.DB2DTO_FactoryDTOList(context.BreakdownMng_Factory_View.OrderBy(o => o.FactoryUD).ToList());
                    data.FactoryID = factoryId;

                    // check if breakdown is exists
                    Breakdown dbBreakdown = null;
                    BreakDownMng_Model_View dbModel = null;
                    BreakdownMng_OfferSeasonDetail_View dbOfferSeasonDetail = null;
                    DTO.ProductOptionParamDTO options = new DTO.ProductOptionParamDTO();

                    // get options
                    if (offerSeasonDetailID > 0)
                    {
                        dbOfferSeasonDetail = context.BreakdownMng_OfferSeasonDetail_View.FirstOrDefault(o => o.OfferSeasonDetailID == offerSeasonDetailID);
                        if (dbOfferSeasonDetail != null)
                        {
                            // get option
                            options.ModelID = dbOfferSeasonDetail.ModelID;
                            options.MaterialID = dbOfferSeasonDetail.MaterialID;
                            options.MaterialTypeID = dbOfferSeasonDetail.MaterialTypeID;
                            options.MaterialColorID = dbOfferSeasonDetail.MaterialColorID;
                            options.FrameMaterialID = dbOfferSeasonDetail.FrameMaterialID;
                            options.FrameMaterialColorID = dbOfferSeasonDetail.FrameMaterialColorID;
                            options.SubMaterialID = dbOfferSeasonDetail.SubMaterialID;
                            options.SubMaterialColorID = dbOfferSeasonDetail.SubMaterialColorID;
                            options.CushionTypeID = dbOfferSeasonDetail.CushionTypeID;
                            options.BackCushionID = dbOfferSeasonDetail.BackCushionID;
                            options.SeatCushionID = dbOfferSeasonDetail.SeatCushionID;
                            options.CushionColorID = dbOfferSeasonDetail.CushionColorID;
                            options.PackagingMethodID = dbOfferSeasonDetail.PackagingMethodID;
                            options.ClientSpecialPackagingMethodID = dbOfferSeasonDetail.ClientSpecialPackagingMethodID;
                            options.FSCTypeID = dbOfferSeasonDetail.FSCTypeID;
                            options.FSCPercentID = dbOfferSeasonDetail.FSCPercentID;
                            options.IsDefault = false;
                        }
                        else
                        {
                            throw new Exception("Offer item not found!");
                        }
                        dbBreakdown = context.Breakdown.FirstOrDefault(o => o.ModelID == options.ModelID);
                    }
                    else
                    {
                        if (modelId > 0)
                        {
                            options.ModelID = modelId;
                            dbModel = context.BreakDownMng_Model_View.FirstOrDefault(o => o.ModelID == modelId && o.MaterialID.HasValue);
                            if (dbModel != null)
                            {
                                // get options                                
                                options.MaterialID = dbModel.MaterialID;
                                options.MaterialTypeID = dbModel.MaterialTypeID;
                                options.MaterialColorID = dbModel.MaterialColorID;
                                options.FrameMaterialID = dbModel.FrameMaterialID;
                                options.FrameMaterialColorID = dbModel.FrameMaterialColorID;
                                options.SubMaterialID = dbModel.SubMaterialID;
                                options.SubMaterialColorID = dbModel.SubMaterialColorID;
                                options.CushionTypeID = dbModel.CushionTypeID;
                                options.BackCushionID = dbModel.BackCushionID;
                                options.SeatCushionID = dbModel.SeatCushionID;
                                options.CushionColorID = dbModel.CushionColorID;
                                options.PackagingMethodID = dbModel.PackagingMethodID;
                                options.FSCTypeID = dbModel.FSCTypeID;
                                options.FSCPercentID = dbModel.FSCPercentID;
                                options.IsDefault = true;
                            }
                            dbBreakdown = context.Breakdown.FirstOrDefault(o => o.ModelID == options.ModelID.Value);
                        }
                        else
                        {
                            if (sampleProductId > 0)
                            {
                                options.SampleProductID = sampleProductId;
                                dbBreakdown = context.Breakdown.FirstOrDefault(o => o.SampleProductID == options.SampleProductID.Value);
                            }
                        }
                    }
                    // validate input param
                    if (!options.ModelID.HasValue && !options.SampleProductID.HasValue)
                    {
                        throw new Exception("Invalid param!");
                    }

                    // create new breakdown if needed
                    if (dbBreakdown == null)
                    {
                        if (options.ModelID.HasValue && options.MaterialID == null)
                        {
                            throw new Exception("Default option not defined!");
                        }

                        dbBreakdown = new Breakdown();
                        context.Breakdown.Add(dbBreakdown);
                        dbBreakdown.CreatedBy = userId;
                        dbBreakdown.CreatedDate = System.DateTime.Now;
                        dbBreakdown.UpdatedBy = userId;
                        dbBreakdown.UpdatedDate = System.DateTime.Now;
                        if (options.ModelID.HasValue)
                        {
                            dbBreakdown.ModelID = options.ModelID;
                            // add option
                            PrepareCategoryOption(userId, companyId, options, ref dbBreakdown, context);
                        }
                        else if (options.SampleProductID.HasValue)
                        {
                            dbBreakdown.SampleProductID = options.SampleProductID;
                            // add option
                            PrepareCategoryOption(userId, companyId, null, ref dbBreakdown, context);
                        }
                        context.SaveChanges();
                        dbBreakdown.BreakdownUD = dbBreakdown.BreakdownID.ToString("D10");
                        context.SaveChanges();
                    }
                    else
                    {
                        if (options.ModelID.HasValue)
                        {
                            dbBreakdown.ModelID = options.ModelID;
                            // add option
                            PrepareCategoryOption(userId, companyId, options, ref dbBreakdown, context);
                        }
                        context.SaveChanges();
                    }

                    id = dbBreakdown.BreakdownID;
                    data.Data = converter.DB2DTO_BreakdownDTO(
                        context.BreakdownMng_Breakdown_View
                        .Include("BreakdownMng_BreakdownCategoryOption_View")
                        .Include("BreakdownMng_BreakdownCategoryOption_View.BreakdownMng_BreakdownCategoryOptionGroup_View")
                        .Include("BreakdownMng_BreakdownCategoryOption_View.BreakdownMng_BreakdownCategoryOptionDetail_View")
                        .Include("BreakdownMng_BreakdownCategoryOption_View.BreakdownMng_BreakdownCategoryOptionImage_View")
                        .FirstOrDefault(o => o.BreakdownID == id)
                        );
                    //data.Data = converter.DB2DTO_BreakdownDTO(
                    //   context.BreakdownMng_Breakdown_View
                    //   .Include("BreakdownMng_BreakdownCategoryOption_View")
                    //   .Include("BreakdownMng_BreakdownCategoryOption_View.BreakdownMng_BreakdownCategoryOptionGroup_View")
                    //   .Include("BreakdownMng_BreakdownCategoryOption_View.BreakdownMng_BreakdownCategoryOptionDetail_View")
                    //   .Include("BreakdownMng_BreakdownCategoryOption_View.BreakdownMng_BreakdownCategoryOptionImage_View")
                    //   .FirstOrDefault(o => o.BreakdownID == id)
                    //   );

                    // restrict PAL to see AVT data
                    if (companyId == 1)
                    {
                        data.Data.BreakdownCategoryOptionDTOs.Where(o => o.CompanyID == 3).ToList().ForEach(o => data.Data.BreakdownCategoryOptionDTOs.Remove(o));
                    }

                    // check and set selected
                    if (options.ModelID.HasValue)
                    {
                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 1 && o.MaterialID == options.MaterialID && o.MaterialTypeID == options.MaterialTypeID && o.MaterialColorID == options.MaterialColorID && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 1 && o.MaterialID == options.MaterialID && o.MaterialTypeID == options.MaterialTypeID && o.MaterialColorID == options.MaterialColorID && o.CompanyID == companyId).IsSelected = true;
                        }
                        else
                        {
                            if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 1 && o.CompanyID == companyId) != null)
                            {
                                data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 1 && o.CompanyID == companyId).IsSelected = true;
                            }
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 2 && o.FrameMaterialID == options.FrameMaterialID && o.FrameMaterialColorID == options.FrameMaterialColorID && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 2 && o.FrameMaterialID == options.FrameMaterialID && o.FrameMaterialColorID == options.FrameMaterialColorID && o.CompanyID == companyId).IsSelected = true;
                        }
                        else
                        {
                            if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 2 && o.CompanyID == companyId) != null)
                            {
                                data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 2 && o.CompanyID == companyId).IsSelected = true;
                            }
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 3 && o.SubMaterialID == options.SubMaterialID && o.SubMaterialColorID == options.SubMaterialColorID && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 3 && o.SubMaterialID == options.SubMaterialID && o.SubMaterialColorID == options.SubMaterialColorID && o.CompanyID == companyId).IsSelected = true;
                        }
                        else
                        {
                            if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 3 && o.CompanyID == companyId) != null)
                            {
                                data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 3 && o.CompanyID == companyId).IsSelected = true;
                            }
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 4 && o.CushionTypeID == options.CushionTypeID && o.CushionColorID == options.CushionColorID && o.BackCushionID == options.BackCushionID && o.SeatCushionID == options.SeatCushionID && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 4 && o.CushionTypeID == options.CushionTypeID && o.CushionColorID == options.CushionColorID && o.BackCushionID == options.BackCushionID && o.SeatCushionID == options.SeatCushionID && o.CompanyID == companyId).IsSelected = true;
                        }
                        else
                        {
                            if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 4 && o.CompanyID == companyId) != null)
                            {
                                data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 4 && o.CompanyID == companyId).IsSelected = true;
                            }
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 5 && o.PackagingMethodID == options.PackagingMethodID && o.ClientSpecialPackagingMethodID == options.ClientSpecialPackagingMethodID && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 5 && o.PackagingMethodID == options.PackagingMethodID && o.ClientSpecialPackagingMethodID == options.ClientSpecialPackagingMethodID && o.CompanyID == companyId).IsSelected = true;
                        }
                        else
                        {
                            if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 5 && o.CompanyID == companyId) != null)
                            {
                                data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 5 && o.CompanyID == companyId).IsSelected = true;
                            }
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 6 && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 6 && o.CompanyID == companyId).IsSelected = true;
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 7 && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 7 && o.CompanyID == companyId).IsSelected = true;
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 8 && o.FSCTypeID == options.FSCTypeID && o.FSCPercentID == options.FSCPercentID && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 8 && o.FSCTypeID == options.FSCTypeID && o.FSCPercentID == options.FSCPercentID && o.CompanyID == companyId).IsSelected = true;
                        }
                        else
                        {
                            if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 8 && o.CompanyID == companyId) != null)
                            {
                                data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 8 && o.CompanyID == companyId).IsSelected = true;
                            }
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 9 && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 9 && o.CompanyID == companyId).IsSelected = true;
                        }

                        if (data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 10 && o.CompanyID == companyId) != null)
                        {
                            data.Data.BreakdownCategoryOptionDTOs.FirstOrDefault(o => o.BreakdownCategoryID == 10 && o.CompanyID == companyId).IsSelected = true;
                        }
                    }
                    else
                    {
                        foreach (var dbOption in data.Data.BreakdownCategoryOptionDTOs.Where(o => o.CompanyID == companyId))
                        {
                            dbOption.IsSelected = true;
                        }
                    }

                    // get list of available options: art.code + description
                    if (options.ModelID.HasValue)
                    {
                        data.AvailableOptionByArticleCodeDTOs = converter.DB2DTO_AvailableOptionByArticleCodeList(context.BreakdownMng_AvailableOptionByArticleCode_View.Where(o => o.ModelID == options.ModelID.Value).ToList());
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

        private void PrepareCategoryOption(int userId, int companyId, DTO.ProductOptionParamDTO options, ref Breakdown dbBreakdown, BreakDownMngEntities context)
        {
            int[] supportCompanyIDs = { 1, 3 };

            // add option
            BreakdownCategoryOption dbCategoryOption = null;
            if (options != null) // model or offerline
            {
                foreach (var dtoCategory in context.BreakdownCategory.ToList())
                {
                    dbCategoryOption = null;
                    switch (dtoCategory.BreakdownCategoryID)
                    {
                        case 1: // MATERIAL COST
                            if (options.MaterialID.HasValue && options.MaterialTypeID.HasValue && options.MaterialColorID.HasValue)
                            {
                                if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null || dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID && o.MaterialID == options.MaterialID && o.MaterialTypeID == options.MaterialTypeID && o.MaterialColorID == options.MaterialColorID) == null)
                                {
                                    foreach (int i in supportCompanyIDs)
                                    {
                                        dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                        dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                        dbCategoryOption.MaterialID = options.MaterialID;
                                        dbCategoryOption.MaterialTypeID = options.MaterialTypeID;
                                        dbCategoryOption.MaterialColorID = options.MaterialColorID;
                                        dbCategoryOption.IsDefaultOption = options.IsDefault;
                                        if (companyId == i)
                                        {
                                            dbCategoryOption.UpdatedBy = userId;
                                            dbCategoryOption.UpdatedDate = DateTime.Now;
                                        }
                                    }
                                }
                            }
                            break;

                        case 2: // FRAME COST
                            if (options.FrameMaterialID.HasValue && options.FrameMaterialColorID.HasValue)
                            {
                                if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null || dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID && o.FrameMaterialID == options.FrameMaterialID && o.FrameMaterialColorID == options.FrameMaterialColorID) == null)
                                {
                                    foreach (int i in supportCompanyIDs)
                                    {
                                        dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                        dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                        dbCategoryOption.FrameMaterialID = options.FrameMaterialID;
                                        dbCategoryOption.FrameMaterialColorID = options.FrameMaterialColorID;
                                        dbCategoryOption.IsDefaultOption = options.IsDefault;
                                        if (companyId == i)
                                        {
                                            dbCategoryOption.UpdatedBy = userId;
                                            dbCategoryOption.UpdatedDate = DateTime.Now;
                                        }
                                    }
                                }
                            }
                            break;

                        case 3: // SUB MATERIAL COST
                            if (options.SubMaterialID.HasValue && options.SubMaterialColorID.HasValue)
                            {
                                if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null || dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID && o.SubMaterialID == options.SubMaterialID && o.SubMaterialColorID == options.SubMaterialColorID) == null)
                                {
                                    foreach (int i in supportCompanyIDs)
                                    {
                                        dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                        dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                        dbCategoryOption.SubMaterialID = options.SubMaterialID;
                                        dbCategoryOption.SubMaterialColorID = options.SubMaterialColorID;
                                        dbCategoryOption.IsDefaultOption = options.IsDefault;
                                        if (companyId == i)
                                        {
                                            dbCategoryOption.UpdatedBy = userId;
                                            dbCategoryOption.UpdatedDate = DateTime.Now;
                                        }
                                    }
                                }
                            }
                            break;

                        case 4: // CUSHION COST
                            if (options.CushionTypeID.HasValue && options.BackCushionID.HasValue && options.SeatCushionID.HasValue && options.CushionColorID.HasValue)
                            {
                                if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null || dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID && o.CushionTypeID == options.CushionTypeID && o.CushionColorID == options.CushionColorID && o.BackCushionID == options.BackCushionID && o.SeatCushionID == options.SeatCushionID) == null)
                                {
                                    foreach (int i in supportCompanyIDs)
                                    {
                                        dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                        dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                        dbCategoryOption.CushionTypeID = options.CushionTypeID;
                                        dbCategoryOption.BackCushionID = options.BackCushionID;
                                        dbCategoryOption.SeatCushionID = options.SeatCushionID;
                                        dbCategoryOption.CushionColorID = options.CushionColorID;
                                        dbCategoryOption.IsDefaultOption = options.IsDefault;
                                        if (companyId == i)
                                        {
                                            dbCategoryOption.UpdatedBy = userId;
                                            dbCategoryOption.UpdatedDate = DateTime.Now;
                                        }
                                    }
                                }
                            }
                            break;

                        case 5: // PACKING COST
                            if (options.PackagingMethodID.HasValue && options.PackagingMethodID.Value > 0)
                            {
                                if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null || dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID && o.PackagingMethodID == options.PackagingMethodID) == null)
                                {
                                    foreach (int i in supportCompanyIDs)
                                    {
                                        dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                        dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                        dbCategoryOption.PackagingMethodID = options.PackagingMethodID;
                                        dbCategoryOption.IsDefaultOption = options.IsDefault;
                                        if (companyId == i)
                                        {
                                            dbCategoryOption.UpdatedBy = userId;
                                            dbCategoryOption.UpdatedDate = DateTime.Now;
                                        }
                                    }
                                }
                            }
                            else if (options.PackagingMethodID.HasValue && options.PackagingMethodID.Value < 0)
                            {
                                if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null || dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID && o.PackagingMethodID == options.PackagingMethodID) == null)
                                {
                                    foreach (int i in supportCompanyIDs)
                                    {
                                        dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                        dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                        dbCategoryOption.PackagingMethodID = options.PackagingMethodID;
                                        dbCategoryOption.Description = "TBA";
                                        dbCategoryOption.IsDefaultOption = options.IsDefault;
                                        if (companyId == i)
                                        {
                                            dbCategoryOption.UpdatedBy = userId;
                                            dbCategoryOption.UpdatedDate = DateTime.Now;
                                        }
                                    }
                                }
                            }
                            break;

                        case 6: // HARDWARE COST
                            if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null)
                            {
                                foreach (int i in supportCompanyIDs)
                                {
                                    dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                    dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                    dbCategoryOption.IsDefaultOption = options.IsDefault;
                                    if (companyId == i)
                                    {
                                        dbCategoryOption.UpdatedBy = userId;
                                        dbCategoryOption.UpdatedDate = DateTime.Now;
                                    }
                                }
                            }
                            break;

                        case 7: // OTHER MATERIAL COST
                            if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null)
                            {
                                foreach (int i in supportCompanyIDs)
                                {
                                    dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                    dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                    dbCategoryOption.IsDefaultOption = options.IsDefault;
                                    if (companyId == i)
                                    {
                                        dbCategoryOption.UpdatedBy = userId;
                                        dbCategoryOption.UpdatedDate = DateTime.Now;
                                    }
                                }
                            }
                            break;

                        case 8: // FSC COST
                            if (options.FSCTypeID.HasValue && options.FSCPercentID.HasValue)
                            {
                                if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null || dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID && o.FSCTypeID == options.FSCTypeID && o.FSCPercentID == options.FSCPercentID) == null)
                                {
                                    foreach (int i in supportCompanyIDs)
                                    {
                                        dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                        dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                        dbCategoryOption.FSCTypeID = options.FSCTypeID;
                                        dbCategoryOption.FSCPercentID = options.FSCPercentID;
                                        dbCategoryOption.IsDefaultOption = options.IsDefault;
                                        if (companyId == i)
                                        {
                                            dbCategoryOption.UpdatedBy = userId;
                                            dbCategoryOption.UpdatedDate = DateTime.Now;
                                        }
                                    }
                                }
                            }
                            break;

                        case 9: // SPECIAL REQUIREMENT COST
                            if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null)
                            {
                                foreach (int i in supportCompanyIDs)
                                {
                                    dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                    dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                    dbCategoryOption.Description = "NONE";
                                    dbCategoryOption.IsDefaultOption = options.IsDefault;
                                    if (companyId == i)
                                    {
                                        dbCategoryOption.UpdatedBy = userId;
                                        dbCategoryOption.UpdatedDate = DateTime.Now;
                                    }
                                }
                            }
                            break;

                        case 10: // MANAGEMENT FEE
                            if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null)
                            {
                                foreach (int i in supportCompanyIDs)
                                {
                                    dbCategoryOption = new BreakdownCategoryOption { CompanyID = i, BreakdownCategoryID = dtoCategory.BreakdownCategoryID };
                                    dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                                    dbCategoryOption.IsDefaultOption = options.IsDefault;
                                    if (companyId == i)
                                    {
                                        dbCategoryOption.UpdatedBy = userId;
                                        dbCategoryOption.UpdatedDate = DateTime.Now;
                                    }
                                    //foreach (var dbDefaulDetail in context.BreakdownMng_ProductionItemCostType_View.Where(o => o.IsAutoAdd.HasValue && o.IsAutoAdd.Value && o.ValuationMethod == "2"))
                                    //{
                                    //    // add detail automatically
                                    //    BreakdownCategoryOptionDetail dbDetail = new BreakdownCategoryOptionDetail();
                                    //    dbCategoryOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                    //    dbDetail.ProductionItemID = dbDefaulDetail.ProductionItemID;
                                    //    dbDetail.QuantityPercent = dbDefaulDetail.ItemCost;
                                    //    dbDetail.UnitID = dbDefaulDetail.UnitID;
                                    //}
                                }
                            }
                            break;
                    }
                    if (dbCategoryOption != null)
                    {
                        // add default item
                        foreach (BreakdownMng_ProductionItemMaterialType_View dbDefaultItem in context.BreakdownMng_ProductionItemMaterialType_View.Where(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID).ToList())
                        {
                            BreakdownCategoryOptionDetail dbDetail = new BreakdownCategoryOptionDetail();
                            dbCategoryOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            dbDetail.ProductionItemID = dbDefaultItem.ProductionItemID;
                            dbDetail.UnitID = dbDefaultItem.UnitID;
                            dbDetail.WastedRatePercent = 0;
                            dbDetail.Quantity = 0;
                            if (dtoCategory.BreakdownCategoryID == 10 && dbDefaultItem.ItemCost.HasValue)
                            {
                                dbDetail.QuantityPercent = dbDefaultItem.ItemCost;
                            }
                        }
                    }
                }
            }
            else  // sample
            {
                foreach (var dtoCategory in context.BreakdownCategory.ToList())
                {
                    if (dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID) == null)
                    {
                        foreach (int i in supportCompanyIDs)
                        {
                            dbCategoryOption = new BreakdownCategoryOption();
                            dbBreakdown.BreakdownCategoryOption.Add(dbCategoryOption);
                            dbCategoryOption.IsDefaultOption = true;
                            dbCategoryOption.BreakdownCategoryID = dtoCategory.BreakdownCategoryID;
                            dbCategoryOption.CompanyID = i;

                            if (companyId == i)
                            {
                                dbCategoryOption.UpdatedBy = userId;
                                dbCategoryOption.UpdatedDate = DateTime.Now;
                            }

                            // add default item
                            if (dtoCategory.BreakdownCategoryID != 10)
                            {
                                foreach (BreakdownMng_ProductionItemMaterialType_View dbDefaultItem in context.BreakdownMng_ProductionItemMaterialType_View.Where(o => o.BreakdownCategoryID == dtoCategory.BreakdownCategoryID).ToList())
                                {
                                    BreakdownCategoryOptionDetail dbDetail = new BreakdownCategoryOptionDetail();
                                    dbCategoryOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                    dbDetail.ProductionItemID = dbDefaultItem.ProductionItemID;
                                    dbDetail.UnitID = dbDefaultItem.UnitID;
                                    dbDetail.WastedRatePercent = 0;
                                    dbDetail.Quantity = 0;
                                }
                            }
                            else
                            {
                                foreach (var dbDefaulDetail in context.BreakdownMng_ProductionItemCostType_View.Where(o => o.IsAutoAdd.HasValue && o.IsAutoAdd.Value && o.ValuationMethod == "2"))
                                {
                                    // add detail automatically
                                    BreakdownCategoryOptionDetail dbDetail = new BreakdownCategoryOptionDetail();
                                    dbCategoryOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                    dbDetail.ProductionItemID = dbDefaulDetail.ProductionItemID;
                                    dbDetail.QuantityPercent = dbDefaulDetail.ItemCost;
                                    dbDetail.UnitID = dbDefaulDetail.UnitID;
                                }
                            }
                        }
                    }
                }
            }
        }

        public DTO.ProductOptionFormData GetProductOption(int userId, int categoryId, int modelId, int productGroupId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.ProductOptionFormData data = new DTO.ProductOptionFormData();
            data.MaterialOptionDTOs = new List<DTO.MaterialOptionDTO>();
            data.MaterialTypeOptionDTOs = new List<DTO.MaterialTypeOptionDTO>();
            data.MaterialColorOptionDTOs = new List<DTO.MaterialColorOptionDTO>();
            data.FrameMaterialOptionDTOs = new List<DTO.FrameMaterialOptionDTO>();
            data.FrameMaterialColorOptionDTOs = new List<DTO.FrameMaterialColorOptionDTO>();
            data.SubMaterialOptionDTOs = new List<DTO.SubMaterialOptionDTO>();
            data.SubMaterialColorOptionDTOs = new List<DTO.SubMaterialColorOptionDTO>();
            data.CushionTypeOptionDTOs = new List<DTO.CushionTypeOptionDTO>();
            data.BackCushionOptionDTOs = new List<DTO.BackCushionOptionDTO>();
            data.SeatCushionOptionDTOs = new List<DTO.SeatCushionOptionDTO>();
            data.CushionColorOptionDTOs = new List<DTO.CushionColorOptionDTO>();
            data.PackagingMethodOptionDTOs = new List<DTO.PackagingMethodOptionDTO>();
            data.FSCTypeOptionDTOs = new List<DTO.FSCTypeOptionDTO>();

            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    switch (categoryId)
                    {
                        case 1: //material
                            data.MaterialOptionDTOs = converter.DB2DTO_MaterialOptionDTOList(context.SupportMng_ModelMaterial_View.Where(o => o.ProductGroupID == productGroupId).ToList());
                            //foreach (var item in context.SupportMng_ModelMaterial_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { MaterialID = o.MaterialID, MaterialUD = o.MaterialUD, MaterialNM = o.MaterialNM }).Distinct().ToList())
                            //{
                            //    data.MaterialOptionDTOs.Add(new DTO.MaterialOptionDTO { MaterialID = item.MaterialID, MaterialUD = item.MaterialUD, MaterialNM = item.MaterialNM});
                            //}

                            data.MaterialTypeOptionDTOs = converter.DB2DTO_MaterialTypeOptionDTOList(context.SupportMng_ModelMaterialType_View.Where(o => o.ProductGroupID == productGroupId).ToList());
                            //data.MaterialTypeOptionDTOs.Where(o => o.Season != Library.Helper.GetCurrentSeason()).ToList().ForEach(o => o.IsStandard = false);
                            //foreach (var item in context.SupportMng_ModelMaterialType_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { MaterialTypeID = o.MaterialTypeID, MaterialTypeUD = o.MaterialTypeUD, MaterialTypeNM = o.MaterialTypeNM }).Distinct().ToList())
                            //{
                            //    data.MaterialTypeOptionDTOs.Add(new DTO.MaterialTypeOptionDTO { MaterialTypeID = item.MaterialTypeID, MaterialTypeUD = item.MaterialTypeUD, MaterialTypeNM = item.MaterialTypeNM });
                            //}

                            data.MaterialColorOptionDTOs = converter.DB2DTO_MaterialColorOptionDTOList(context.SupportMng_ModelMaterialColor_View.Where(o => o.ProductGroupID == productGroupId).ToList());
                            //foreach (var item in context.SupportMng_ModelMaterialColor_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { MaterialColorID = o.MaterialColorID, MaterialColorUD = o.MaterialColorUD, MaterialColorNM = o.MaterialColorNM }).Distinct().ToList())
                            //{
                            //    data.MaterialColorOptionDTOs.Add(new DTO.MaterialColorOptionDTO { MaterialColorID = item.MaterialColorID, MaterialColorUD = item.MaterialColorUD, MaterialColorNM = item.MaterialColorNM });
                            //}
                            break;

                        case 2: // frame
                            data.FrameMaterialOptionDTOs = converter.DB2DTO_FrameMaterialOptionDTOList(context.SupportMng_ModelFrameMaterial_View.Where(o => o.ProductGroupID == productGroupId).Distinct().ToList());
                            //foreach (var item in context.SupportMng_ModelFrameMaterial_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { FrameMaterialID = o.FrameMaterialID, FrameMaterialUD = o.FrameMaterialUD, FrameMaterialNM = o.FrameMaterialNM }).Distinct().ToList())
                            //{
                            //    data.FrameMaterialOptionDTOs.Add(new DTO.FrameMaterialOptionDTO { FrameMaterialID = item.FrameMaterialID, FrameMaterialNM = item.FrameMaterialNM });
                            //}

                            data.FrameMaterialColorOptionDTOs = converter.DB2DTO_FrameMaterialColorOptionDTOList(context.SupportMng_ModelFrameMaterialColor_View.Where(o => o.ProductGroupID == productGroupId).Distinct().ToList());
                            //foreach (var item in context.SupportMng_ModelFrameMaterialColor_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { FrameMaterialColorID = o.FrameMaterialColorID, FrameMaterialColorUD = o.FrameMaterialColorUD, FrameMaterialColorNM = o.FrameMaterialColorNM }).Distinct().ToList())
                            //{
                            //    data.FrameMaterialColorOptionDTOs.Add(new DTO.FrameMaterialColorOptionDTO { FrameMaterialColorID = item.FrameMaterialColorID, FrameMaterialColorNM = item.FrameMaterialColorNM });
                            //}
                            break;

                        case 3: // sub
                            data.SubMaterialOptionDTOs = converter.DB2DTO_SubMaterialOptionDTOList(context.SupportMng_ModelSubMaterial_View.Where(o => o.ModelID == modelId).Distinct().ToList());
                            //foreach (var item in context.SupportMng_ModelSubMaterial_View.Where(o => o.ModelID == modelId).Select(o => new { SubMaterialID = o.SubMaterialID, SubMaterialUD = o.SubMaterialUD, SubMaterialNM = o.SubMaterialNM }).Distinct().ToList())
                            //{
                            //    data.SubMaterialOptionDTOs.Add(new DTO.SubMaterialOptionDTO { SubMaterialID = item.SubMaterialID.Value, SubMaterialNM = item.SubMaterialNM });
                            //}

                            data.SubMaterialColorOptionDTOs = converter.DB2DTO_SubMaterialColorOptionDTOList(context.SupportMng_ModelSubMaterialColor_View.Where(o => o.ModelID == modelId).Distinct().ToList());
                            //foreach (var item in context.SupportMng_ModelSubMaterialColor_View.Where(o => o.ModelID == modelId).Select(o => new { SubMaterialColorID = o.SubMaterialColorID, SubMaterialColorUD = o.SubMaterialColorUD, SubMaterialColorNM = o.SubMaterialColorNM }).Distinct().ToList())
                            //{
                            //    data.SubMaterialColorOptionDTOs.Add(new DTO.SubMaterialColorOptionDTO { SubMaterialColorID = item.SubMaterialColorID.Value, SubMaterialColorNM = item.SubMaterialColorNM });
                            //}
                            break;

                        case 4: // cushion
                            data.CushionTypeOptionDTOs = converter.DB2DTO_CushionTypeOptionDTOList(context.SupportMng_ModelCushionType_View.Where(o => o.ProductGroupID == productGroupId).Distinct().ToList());
                            //foreach (var item in context.SupportMng_ModelCushionType_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { CushionTypeID = o.CushionTypeID, CushionTypeNM = o.CushionTypeNM }).Distinct().ToList())
                            //{
                            //    data.CushionTypeOptionDTOs.Add(new DTO.CushionTypeOptionDTO { CushionTypeID = item.CushionTypeID.Value, CushionTypeNM = item.CushionTypeNM });
                            //}

                            data.BackCushionOptionDTOs = converter.DB2DTO_BackCushionOptionDTOList(context.SupportMng_ModelBackCushion_View.Where(o => o.ProductGroupID == productGroupId).Distinct().ToList());
                            //foreach (var item in context.SupportMng_ModelBackCushion_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { BackCushionID = o.BackCushionID, BackCushionNM = o.BackCushionNM }).Distinct().ToList())
                            //{
                            //    data.BackCushionOptionDTOs.Add(new DTO.BackCushionOptionDTO { BackCushionID = item.BackCushionID, BackCushionNM = item.BackCushionNM });
                            //}

                            data.SeatCushionOptionDTOs = converter.DB2DTO_SeatCushionOptionDTOList(context.SupportMng_ModelSeatCushion_View.Where(o => o.ProductGroupID == productGroupId).Distinct().ToList());
                            //foreach (var item in context.SupportMng_ModelSeatCushion_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { SeatCushionID = o.SeatCushionID, SeatCushionNM = o.SeatCushionNM }).Distinct().ToList())
                            //{
                            //    data.SeatCushionOptionDTOs.Add(new DTO.SeatCushionOptionDTO { SeatCushionID = item.SeatCushionID, SeatCushionNM = item.SeatCushionNM });
                            //}

                            data.CushionColorOptionDTOs = converter.DB2DTO_CushionColorOptionDTOList(context.SupportMng_ModelCushionColor_View.Where(o => o.ProductGroupID == productGroupId).Distinct().ToList());
                            //foreach (var item in context.SupportMng_ModelCushionColor_View.Where(o => o.ProductGroupID == productGroupId).Select(o => new { CushionColorID = o.CushionColorID, CushionColorNM = o.CushionColorNM }).Distinct().ToList())
                            //{
                            //    data.CushionColorOptionDTOs.Add(new DTO.CushionColorOptionDTO { CushionColorID = item.CushionColorID, CushionColorNM = item.CushionColorNM });
                            //}
                            break;

                        case 5: // packing
                            data.PackagingMethodOptionDTOs = converter.DB2DTO_PackagingMethodOptionDTOList(context.BreakdownMng_PackagingMethod_View.OrderBy(o => o.PackagingMethodNM).ToList());
                            data.ClientSpecialPackagingMethodDTOs = converter.DB2DTO_ClientSpecialPackagingMethodDTOList(context.BreakdownMng_ClientSpecialPackagingMethod_View.OrderBy(o => o.ClientUD).OrderBy(o => o.ClientSpecialPackagingMethodNM).ToList());
                            break;

                        case 8: // fsc
                            data.FSCTypeOptionDTOs = converter.DB2DTO_FSCTypeOptionDTOList(context.BreakdownMng_FSCType_View.Distinct().ToList());
                            break;
                    }
                }

                data.CurrentSeason = Library.Helper.GetCurrentSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public bool UpdateCategoryOption(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.BreakdownCategoryOptionDTO dtoOption = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.BreakdownCategoryOptionDTO>();
            int companyId = fwFactory.GetCompanyID(userId).Value;
            if (companyId != 1)
            {
                companyId = 3;
            }
            int[] supportCompanyIDs = { 1, 3 };

            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    BreakdownCategoryOption dbOption = null;
                    if (id > 0)
                    {
                        if (dtoOption.CompanyID != companyId)
                        {
                            throw new Exception("Can not update breakdown data from other company!");
                        }

                        dbOption = context.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryOptionID == id && o.CompanyID == companyId);
                        converter.DTO2DB_BreakdownCategoryOption(dtoOption, ref dbOption, string.Empty, userId);
                    }
                    else
                    {
                        foreach (int i in supportCompanyIDs)
                        {
                            BreakdownCategoryOption dbNewOption = new BreakdownCategoryOption { CompanyID = i };
                            context.BreakdownCategoryOption.Add(dbNewOption);
                            // add detail automatically
                            foreach (var dbMaterial in context.BreakdownMng_ProductionItemMaterialType_View.Where(o => o.BreakdownCategoryID.HasValue && o.BreakdownCategoryID == dtoOption.BreakdownCategoryID))
                            {
                                BreakdownCategoryOptionDetail dbDetail = new BreakdownCategoryOptionDetail { ProductionItemID = dbMaterial.ProductionItemID, UnitID = dbMaterial.UnitID };
                                dbNewOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            }
                            if (i == companyId)
                            {
                                dbOption = dbNewOption;
                                converter.DTO2DB_BreakdownCategoryOption(dtoOption, ref dbNewOption, string.Empty, userId);
                            }
                            else
                            {
                                converter.DTO2DB_BreakdownCategoryOption(dtoOption, ref dbNewOption, string.Empty, 0);
                            }
                        }
                    }

                    // remove orphan
                    context.BreakdownCategoryOptionDetail.Local.Where(o => o.BreakdownCategoryOption == null).ToList().ForEach(o => context.BreakdownCategoryOptionDetail.Remove(o));
                    context.BreakdownCategoryOptionImage.Local.Where(o => o.BreakdownCategoryOption == null).ToList().ForEach(o => context.BreakdownCategoryOptionImage.Remove(o));
                    context.SaveChanges();

                    // return new dto
                    Breakdown dbBreakdown = context.Breakdown.FirstOrDefault(o => o.BreakdownID == dtoOption.BreakdownID);
                    if (dbBreakdown != null && dbBreakdown.ModelID.HasValue)
                    {
                        dtoItem = new DTO.UpdateOptionResultDTO
                        {
                            Data = converter.DB2DTO_BreakdownCategoryOptionDTO(context.BreakdownMng_BreakdownCategoryOption_View.FirstOrDefault(o => o.BreakdownCategoryOptionID == dbOption.BreakdownCategoryOptionID)),
                            AvailableOptionByArticleCodeDTOs = converter.DB2DTO_AvailableOptionByArticleCodeList(context.BreakdownMng_AvailableOptionByArticleCode_View.Where(o => o.ModelID == dbBreakdown.ModelID.Value).ToList())
                        };
                    }
                    else
                    {
                        dtoItem = new DTO.UpdateOptionResultDTO
                        {
                            Data = converter.DB2DTO_BreakdownCategoryOptionDTO(context.BreakdownMng_BreakdownCategoryOption_View.FirstOrDefault(o => o.BreakdownCategoryOptionID == dbOption.BreakdownCategoryOptionID)),
                            AvailableOptionByArticleCodeDTOs = new List<DTO.AvailableOptionByArticleCodeDTO>()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
            return true;
        }

        public bool UpdateSeasonSpecPercent(int userId, int id, decimal percent, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    Breakdown dbItem = context.Breakdown.FirstOrDefault(o => o.BreakdownID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Breakdown not found!");
                    }
                    dbItem.SeasonSpecPercent = percent;
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return true;
        }

        public DTO.GetPriceDetailDTO GetPriceData(int userId, object dtoItem, int id, string articleCode, 
            out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.BreakdownCategoryOptionIDDTO breakdownCategoryOptionIDs = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.BreakdownCategoryOptionIDDTO>();

            DTO.GetPriceDetailDTO data = new DTO.GetPriceDetailDTO();
            data.BreakDownBreakdownCategoryDTOs = new List<DTO.BreakDownBreakdownCategoryDTO>();
            data.BreakdownPriceHistoryDTOs = new List<DTO.BreakdownPriceHistoryDTO>();
           

            int companyId = fwFactory.GetCompanyID(userId).Value;
            if (companyId != 1)
            {
                companyId = 3;
            }

            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    if(breakdownCategoryOptionIDs.MaterialCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.MaterialCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.FrameCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.FrameCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.SubMaterialCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.SubMaterialCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.CushionCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.CushionCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.PackingCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.PackingCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.HardwareCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.HardwareCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.OtherMaterialCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.OtherMaterialCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.FscCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.FscCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.SpecialRequirementCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.SpecialRequirementCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }
                    if (breakdownCategoryOptionIDs.ManagementCostOptionID > 0)
                    {
                        List<DTO.BreakDownBreakdownCategoryDTO> temp = converter.DB2DTO_BreakDownBreakdownCategoryDTOs(context.BreakDownMng_function_BreakdownCategoryOption(breakdownCategoryOptionIDs.ManagementCostOptionID).ToList());
                        data.BreakDownBreakdownCategoryDTOs.AddRange(temp);
                    }

                    foreach (var item in data.BreakDownBreakdownCategoryDTOs)
                    {
                        item.BreakDownPriceDefaultDTOs = new List<DTO.BreakDownPriceDefaultDTO>();
                        item.BreakDownPriceDefaultDTOs = converter.DB2DTO_BreakDownPriceDefaultDTOs(context.BreakDownMng_function_PriceDetail(companyId, item.BreakdownCategoryOptionID).ToList());

                    }

                    //get price AVF
                    //nếu có lưu giá của article code thì lấy, ko thì lấy giá từ BreakDownPriceList
                    if (companyId == 3)
                    {
                        foreach (var itemBreakDownBreakdownCategoryDTO in data.BreakDownBreakdownCategoryDTOs)
                        {
                            foreach (var item in itemBreakDownBreakdownCategoryDTO.BreakDownPriceDefaultDTOs)
                            {

                                List<DTO.BreakdownPriceHistoryDTO> temp = converter.DB2DTO_BreakdownPriceHistorys(context.BreakDownMng_BreakdownPriceHistory_View.Where(s => s.ArticleCode == articleCode && s.CompanyID == 1).ToList());
                                if (temp.Count > 0)
                                {
                                    foreach (var itemBreakdownPriceHistoryDetailDTOs in temp[0].BreakdownPriceHistoryDetailDTOs)
                                    {
                                        if (item.ProductionItemID == itemBreakdownPriceHistoryDetailDTOs.ProductionItemID
                                            && item.BreakdownCategoryID == itemBreakdownPriceHistoryDetailDTOs.BreakdownCategoryID)
                                        {
                                            item.QuantityAVF = itemBreakdownPriceHistoryDetailDTOs.Quantity;
                                            item.UnitPriceAVF = itemBreakdownPriceHistoryDetailDTOs.UnitPrice;
                                        }
                                    }
                                }
                                else
                                {
                                    List<DTO.BreakDownPriceDefaultDTO> priceDefaultAVF = converter.DB2DTO_BreakDownPriceDefaultDTOs(context.BreakDownMng_function_PriceDetail(1, itemBreakDownBreakdownCategoryDTO.BreakdownCategoryOptionID).ToList());

                                    foreach (var itemBreakDownPriceDefaultDTO in priceDefaultAVF)
                                    {
                                        if (item.ProductionItemID == itemBreakDownPriceDefaultDTO.ProductionItemID
                                            && item.BreakdownCategoryID == itemBreakDownPriceDefaultDTO.BreakdownCategoryID)
                                        {
                                            item.QuantityAVF = itemBreakDownPriceDefaultDTO.Quantity;
                                            item.UnitPriceAVF = itemBreakDownPriceDefaultDTO.UnitPrice;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    data.BreakdownPriceHistoryDTOs = converter.DB2DTO_BreakdownPriceHistorys(context.BreakDownMng_BreakdownPriceHistory_View.Where(s => s.ArticleCode == articleCode && s.CompanyID == companyId).ToList());
                    // lấy lich sử lưu giá theo articleCode
                    if (data.BreakdownPriceHistoryDTOs.Count > 0)
                    {
                        foreach (var itemBreakDownBreakdownCategoryDTO in data.BreakDownBreakdownCategoryDTOs)
                        {
                            foreach (var item in itemBreakDownBreakdownCategoryDTO.BreakDownPriceDefaultDTOs)
                            {

                                for (int i = 0; i < data.BreakdownPriceHistoryDTOs.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        foreach (var itemBreakdownPriceHistoryDetailDTOs in data.BreakdownPriceHistoryDTOs[i].BreakdownPriceHistoryDetailDTOs)
                                        {
                                            if (item.ProductionItemID == itemBreakdownPriceHistoryDetailDTOs.ProductionItemID
                                                && item.BreakdownCategoryID == itemBreakdownPriceHistoryDetailDTOs.BreakdownCategoryID)
                                            {
                                                item.Quantity1 = itemBreakdownPriceHistoryDetailDTOs.Quantity;
                                                item.UnitPrice1 = itemBreakdownPriceHistoryDetailDTOs.UnitPrice;
                                            }
                                        }
                                    }

                                    if (i == 1)
                                    {
                                        foreach (var itemBreakdownPriceHistoryDetailDTOs in data.BreakdownPriceHistoryDTOs[i].BreakdownPriceHistoryDetailDTOs)
                                        {
                                            if (item.ProductionItemID == itemBreakdownPriceHistoryDetailDTOs.ProductionItemID
                                                && item.BreakdownCategoryID == itemBreakdownPriceHistoryDetailDTOs.BreakdownCategoryID)
                                            {
                                                item.Quantity2 = itemBreakdownPriceHistoryDetailDTOs.Quantity;
                                                item.UnitPrice2 = itemBreakdownPriceHistoryDetailDTOs.UnitPrice;
                                            }
                                        }
                                    }

                                    if (i == 2)
                                    {
                                        foreach (var itemBreakdownPriceHistoryDetailDTOs in data.BreakdownPriceHistoryDTOs[i].BreakdownPriceHistoryDetailDTOs)
                                        {
                                            if (item.ProductionItemID == itemBreakdownPriceHistoryDetailDTOs.ProductionItemID
                                                && item.BreakdownCategoryID == itemBreakdownPriceHistoryDetailDTOs.BreakdownCategoryID)
                                            {
                                                item.Quantity3 = itemBreakdownPriceHistoryDetailDTOs.Quantity;
                                                item.UnitPrice3 = itemBreakdownPriceHistoryDetailDTOs.UnitPrice;
                                            }
                                        }
                                    }
                                }

                            }
                        }
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
        public bool UpdatePrice(int userId, object dtoItem, int id, string articleCode, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.GetPriceDetailDTO dtoPrices = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.GetPriceDetailDTO>();
            try
            {
                int companyId = fwFactory.GetCompanyID(userId).Value;
                if (companyId != 1) companyId = 3;
                using (BreakDownMngEntities context = CreateContext())
                {       
                    BreakdownPriceHistory breakdownPriceHistory = new BreakdownPriceHistory();                 
                    breakdownPriceHistory.BreakdownPriceHistoryUD = DateTime.Now.ToString("yy-MM-dd");
                    breakdownPriceHistory.BreakdownPriceHistoryNM = DateTime.Now.ToString("yy-MM-dd");
                    breakdownPriceHistory.ArticleCode = articleCode;
                    breakdownPriceHistory.CompanyID = companyId;
                    breakdownPriceHistory.BreakdownID = id;
                    breakdownPriceHistory.CreatedBy = userId;
                    breakdownPriceHistory.CreatedDate = DateTime.Now;

                    breakdownPriceHistory.BreakdownPriceHistoryDetail = new List<BreakdownPriceHistoryDetail>();

                    foreach (var itemBreakDownBreakdownCategoryDTO in dtoPrices.BreakDownBreakdownCategoryDTOs)
                    {
                        foreach (var item in itemBreakDownBreakdownCategoryDTO.BreakDownPriceDefaultDTOs)
                        {
                            BreakdownPriceHistoryDetail detail = new BreakdownPriceHistoryDetail();
                            detail.ProductionItemID = item.ProductionItemID;
                            detail.UnitID = item.UnitID;
                            detail.Quantity = item.Quantity;
                            detail.BreakdownCategoryID = item.BreakdownCategoryID;
                            detail.UnitPrice = item.UnitPrice;
                            detail.CreatedBy = userId;
                            detail.CreatedDate = DateTime.Now;

                            breakdownPriceHistory.BreakdownPriceHistoryDetail.Add(detail);
                        }
                    }

                    // update price list            
                    foreach (var itemBreakDownBreakdownCategoryDTO in dtoPrices.BreakDownBreakdownCategoryDTOs)
                    {
                        foreach (var dtoPrice in itemBreakDownBreakdownCategoryDTO.BreakDownPriceDefaultDTOs)
                        {
                            BreakdownPriceList dbPrice = context.BreakdownPriceList.FirstOrDefault(o => o.ProductionItemID == dtoPrice.ProductionItemID && o.CompanyID == companyId);
                            if (dbPrice != null)
                            {
                                dbPrice.UpdatedBy = userId;
                                dbPrice.UpdatedDate = DateTime.Now;
                                dbPrice.UnitPrice = dtoPrice.UnitPrice;
                            }
                            else
                            {
                                dbPrice = new BreakdownPriceList();
                                dbPrice.ProductionItemID = dtoPrice.ProductionItemID;
                                dbPrice.UnitID = dtoPrice.UnitID;
                                dbPrice.UnitPrice = dtoPrice.UnitPrice;
                                dbPrice.CompanyID = companyId;
                                dbPrice.UpdatedBy = userId;
                                dbPrice.UpdatedDate = DateTime.Now;

                                context.BreakdownPriceList.Add(dbPrice);
                            }
                        }
                    }

                    context.BreakdownPriceHistory.Add(breakdownPriceHistory);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
            return true;
        }
        //public bool UpdatePrice(int userId, object dtoItem, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
        //    List<DTO.BreakdownPriceDTO> dtoPrices = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.BreakdownPriceDTO>>();
        //    try
        //    {
        //        int companyId = fwFactory.GetCompanyID(userId).Value;
        //        if (companyId != 1) companyId = 3;
        //        using (BreakDownMngEntities context = CreateContext())
        //        {
        //            // update price list                    
        //            foreach (var dtoPrice in dtoPrices)
        //            {
        //                BreakdownPriceList dbPrice = context.BreakdownPriceList.FirstOrDefault(o => o.ProductionItemID == dtoPrice.ProductionItemID && o.CompanyID == companyId);
        //                if (dbPrice == null)
        //                {
        //                    throw new Exception("Price not found!");
        //                }
        //                dbPrice.UpdatedBy = userId;
        //                dbPrice.UpdatedDate = DateTime.Now;

        //                if (companyId == 1)
        //                {
        //                    dbPrice.UnitPrice = dtoPrice.AVFPrice;
        //                }
        //                else
        //                {
        //                    dbPrice.UnitPrice = dtoPrice.AVTPrice;
        //                }
        //            }
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = Library.Helper.GetInnerException(ex).Message;
        //        return false;
        //    }
        //    return true;
        //}

        public bool ApproveOption(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    BreakdownCategoryOption dbItem = context.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryOptionID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Option not found!");
                    }
                    if (!dbItem.IsConfirmed.HasValue || !dbItem.IsConfirmed.Value)
                    {
                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = userId;
                        dbItem.ConfirmedDate = DateTime.Now;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
            return true;
        }

        public bool UnApproveOption(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    BreakdownCategoryOption dbItem = context.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryOptionID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Option not found!");
                    }
                    dbItem.IsConfirmed = false;
                    dbItem.ConfirmedBy = null;
                    dbItem.ConfirmedDate = null;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
            return true;
        }

        public bool ApproveAllOption(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SelectedCategoryOptionDTO dtoApproveOption = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SelectedCategoryOptionDTO>();
            try
            {
                if(dtoApproveOption.materialCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.materialCostOptionID, out notification);
                if(dtoApproveOption.frameCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.frameCostOptionID, out notification);
                if(dtoApproveOption.subMaterialCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.subMaterialCostOptionID, out notification);
                if(dtoApproveOption.cushionCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.cushionCostOptionID, out notification);
                if(dtoApproveOption.packingCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.packingCostOptionID, out notification);
                if(dtoApproveOption.hardwareCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.hardwareCostOptionID, out notification);
                if(dtoApproveOption.otherMaterialCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.otherMaterialCostOptionID, out notification);
                if(dtoApproveOption.fscCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.fscCostOptionID, out notification);
                if(dtoApproveOption.specialRequirementCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.specialRequirementCostOptionID, out notification);
                if(dtoApproveOption.managementCostOptionID > 0)
                    ApproveOption(userId, dtoApproveOption.managementCostOptionID, out notification);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
            return true;
        }

        public bool UnApproveAllOption(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SelectedCategoryOptionDTO dtoUnApproveOption = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SelectedCategoryOptionDTO>();
            try
            {
                if (dtoUnApproveOption.materialCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.materialCostOptionID, out notification);
                if (dtoUnApproveOption.frameCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.frameCostOptionID, out notification);
                if (dtoUnApproveOption.subMaterialCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.subMaterialCostOptionID, out notification);
                if (dtoUnApproveOption.cushionCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.cushionCostOptionID, out notification);
                if (dtoUnApproveOption.packingCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.packingCostOptionID, out notification);
                if (dtoUnApproveOption.hardwareCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.hardwareCostOptionID, out notification);
                if (dtoUnApproveOption.otherMaterialCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.otherMaterialCostOptionID, out notification);
                if (dtoUnApproveOption.fscCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.fscCostOptionID, out notification);
                if (dtoUnApproveOption.specialRequirementCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.specialRequirementCostOptionID, out notification);
                if (dtoUnApproveOption.managementCostOptionID > 0)
                    UnApproveOption(userId, dtoUnApproveOption.managementCostOptionID, out notification);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
            return true;
        }

        public List<DTO.ProductionItemDTO> QuickSearchProductionItem(string query, string type, out Library.DTO.Notification notification)
        {
            List<DTO.ProductionItemDTO> data = new List<DTO.ProductionItemDTO>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    if (type == "m") // material type
                    {
                        data = converter.DB2DTO_ProductionItemMaterialTypeDTOList(context.BreakdownMng_ProductionItemMaterialType_View.Where(o => o.ProductionItemNM.Contains(query) || o.ProductionItemUD.Contains(query)).ToList());
                    }
                    else // cost type
                    {
                        data = converter.DB2DTO_ProductionItemCostTypeDTOList(context.BreakdownMng_ProductionItemCostType_View.Where(o => o.ProductionItemNM.Contains(query) || o.ProductionItemUD.Contains(query)).ToList());
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

        public List<DTO.ProductionItemDTO> GetDefaultProductionItem(int id, out Library.DTO.Notification notification)
        {
            List<DTO.ProductionItemDTO> data = new List<DTO.ProductionItemDTO>();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ProductionItemMaterialTypeDTOList(context.BreakdownMng_ProductionItemMaterialType_View.Where(o => o.BreakdownCategoryID == id).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }

        public object GetModel(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.SupportModelDTO> data = new List<DTO.SupportModelDTO>();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_SupportModel(context.BreakDownMng_function_SupportModel(searchQuery, userID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public object GetSampleProduct(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.SupportSampleProductDTO> data = new List<DTO.SupportSampleProductDTO>();

            try
            {
                string searchQuery = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString())) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    data = converter.DB2DTO_SupportSampleProduct(context.BreakDownMng_function_SupportSampleProduct(searchQuery, userID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public string GetArticleCode(int offerSeasonDetailID, int modelID, object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.BreakdownCategoryOptionIDDTO breakdownCategoryOptionIDs = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.BreakdownCategoryOptionIDDTO>();

            string result = string.Empty;

            try
            {
                using (var context = CreateContext())
                {
                    if(offerSeasonDetailID > 0)
                    {
                        result = context.BreakdownMng_OfferSeasonDetail_View.Where(s => s.OfferSeasonDetailID == offerSeasonDetailID).FirstOrDefault().ArticleCode;
                    }
                    else
                    {
                        result = context.BreakDownMng_function_GetArticleCode(breakdownCategoryOptionIDs.MaterialCostOptionID,
                            breakdownCategoryOptionIDs.FrameCostOptionID, breakdownCategoryOptionIDs.SubMaterialCostOptionID, breakdownCategoryOptionIDs.CushionCostOptionID,
                            breakdownCategoryOptionIDs.FscCostOptionID, modelID).FirstOrDefault();
                    }                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return result;
        }

        //
        // support tools: import from BOM
        //
        public DTO.BOMImportResultDTO ImportBOMDataSingle(int userId, int id, out Library.DTO.Notification notification)
        {
            int companyId = fwFactory.GetCompanyID(userId).Value;
            if (companyId != 1)
            {
                companyId = 3;
            }
            DTO.BOMImportResultDTO data = new DTO.BOMImportResultDTO();

            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    BreakdownMng_BOMProductOption_View dbProduct = context.BreakdownMng_BOMProductOption_View.FirstOrDefault(o => o.ProductionProcessID == id);
                    if (dbProduct != null)
                    {
                        ImportBOMData(dbProduct, userId, companyId, context, out notification);
                        if (dbProduct.OfferSeasonDetailID.HasValue)
                        {
                            data.OfferSeasonDetailID = dbProduct.OfferSeasonDetailID.Value;
                        }
                        else
                        {
                            data.OfferSeasonDetailID = dbProduct.ModelID.Value;
                        }
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
        public bool ImportBOMData(int userId, out Library.DTO.Notification notification)
        {
            int companyId = fwFactory.GetCompanyID(userId).Value;
            if (companyId != 1)
            {
                companyId = 3;
            }

            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BreakDownMngEntities context = CreateContext())
                {
                    foreach (BreakdownMng_BOMProductOption_View dbProduct in context.BreakdownMng_BOMProductOption_View.OrderByDescending(o => o.UpdatedDate).ToList())
                    {
                        ImportBOMData(dbProduct, userId, companyId, context, out notification);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return false;
        }
        public void ImportBOMData(BreakdownMng_BOMProductOption_View dbProduct, int userId, int companyId, BreakDownMngEntities context, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            int[] supportCompanyIDs = { 1, 3 };
            bool isNewBreakdown = false;
            try
            {
                if (dbProduct == null)
                {
                    throw new Exception("BOM info not found!");
                }

                //
                // retrieve material to be imported
                //
                List<DTO.BOMDetailDTO> materials = new List<DTO.BOMDetailDTO>();
                var dbDetails = context.BreakdownMng_BOMDetail_View.Where(o => o.ProductionProcessID == dbProduct.ProductionProcessID).ToList();
                //BreakdownMng_BOMDetail_View dtoGroupNode = null;
                BreakdownMng_BOMDetail_View dtoWorkCenterNode = null;
                BreakdownCategoryOption dbOption = null;
                BreakdownCategoryOptionDetail dbDetail = null;
                BreakdownMng_ProductionItemMaterialType_View dbDefaultItem = null;
                foreach (var dbDetailTmp in dbDetails.Where(o => o.ProductionItemTypeID.HasValue && (o.ProductionItemTypeID == 2 || o.ProductionItemTypeID == 7) && !string.IsNullOrEmpty(o.ProductionItemUD) && o.ProductionItemGroupID.HasValue)) // only care abot the material + cost type + which has UD and group
                {
                    //dtoGroupNode = GetGroupNode(dbDetails, dbDetailTmp);
                    dtoWorkCenterNode = GetWorkCenterID(dbDetails, dbDetailTmp);
                    if (dtoWorkCenterNode != null)
                    {
                        materials.Add(new DTO.BOMDetailDTO
                        {
                            ProductionItemID = dbDetailTmp.ProductionItemID.Value,
                            ProductionItemUD = dbDetailTmp.ProductionItemUD,
                            //GroupBOMStandardID = (dtoGroupNode != null ? dtoGroupNode.BOMStandardID : 0),
                            //GroupUD = (dtoGroupNode != null ? dtoGroupNode.ProductionItemUD : string.Empty),
                            WorkCenterID = (dtoWorkCenterNode != null ? dtoWorkCenterNode.WorkCenterID.Value : 0),
                            WorkCenterUD = (dtoWorkCenterNode != null ? dtoWorkCenterNode.WorkCenterUD : string.Empty),
                            Quantity = dbDetailTmp.Quantity,
                            UnitID = dbDetailTmp.SubUnitID.Value,
                            ProductionItemGroupID = dbDetailTmp.ProductionItemGroupID.Value
                        });
                    }
                }
                //Library.Helper.CreateExcelFileWithEPPlus<DTO.BOMDetailDTO>(materials);

                Breakdown dbBreakdown = context.Breakdown.FirstOrDefault(o => o.ModelID == dbProduct.ModelID);
                if (dbBreakdown == null)
                {
                    isNewBreakdown = true;
                    dbBreakdown = new Breakdown();
                    dbBreakdown.ModelID = dbProduct.ModelID;
                    dbBreakdown.CreatedBy = userId;
                    dbBreakdown.CreatedDate = DateTime.Now;
                    dbBreakdown.UpdatedBy = userId;
                    dbBreakdown.UpdatedDate = DateTime.Now;
                    context.Breakdown.Add(dbBreakdown);
                }

                // add material option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o =>
                    o.BreakdownCategoryID == 1
                    && o.MaterialID.HasValue && o.MaterialID == dbProduct.MaterialID
                    && o.MaterialTypeID.HasValue && o.MaterialTypeID == dbProduct.MaterialTypeID
                    && o.MaterialColorID.HasValue && o.MaterialColorID == dbProduct.MaterialColorID);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 1;
                        dbOption.MaterialID = dbProduct.MaterialID;
                        dbOption.MaterialTypeID = dbProduct.MaterialTypeID;
                        dbOption.MaterialColorID = dbProduct.MaterialColorID;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }

                        //
                        // add detail
                        //
                        if (dbProduct.MaterialID == 4) // WICKER
                        {
                            // 9: WEAVING
                            foreach (var materialInfo in materials.Where(o => o.WorkCenterID == 9).GroupBy(o => o.ProductionItemID).Select(
                                n => new
                                {
                                    ProductionItemID = n.First().ProductionItemID,
                                    TotalQuantity = n.Sum(c => c.Quantity),
                                    UnitID = n.First().UnitID
                                })
                            )
                            {
                                dbDetail = new BreakdownCategoryOptionDetail();
                                dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                dbDetail.ProductionItemID = materialInfo.ProductionItemID;
                                dbDetail.Quantity = materialInfo.TotalQuantity;
                                dbDetail.UnitID = materialInfo.UnitID;
                            }

                            // add additional item
                            // 16161: BR123456 OR 16171: COSTFINI
                            dbDefaultItem = context.BreakdownMng_ProductionItemMaterialType_View.FirstOrDefault(o => o.ProductionItemID == 16161 || o.ProductionItemID == 16171);
                            if (dbDefaultItem != null)
                            {
                                dbDetail = new BreakdownCategoryOptionDetail();
                                dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                dbDetail.ProductionItemID = dbDefaultItem.ProductionItemID;
                                dbDetail.UnitID = dbDefaultItem.UnitID;
                                if (dbDefaultItem.ItemCost.HasValue)
                                {
                                    dbDetail.QuantityPercent = dbDefaultItem.ItemCost;
                                }
                            }
                        }
                        else
                        {
                            // 7: FRAME; 8: PAINTED
                            foreach (var materialInfo in materials.Where(o => o.WorkCenterID == 7 || o.WorkCenterID == 8).GroupBy(o => o.ProductionItemID).Select(
                                n => new
                                {
                                    ProductionItemID = n.First().ProductionItemID,
                                    TotalQuantity = n.Sum(c => c.Quantity),
                                    UnitID = n.First().UnitID
                                })
                            )
                            {
                                dbDetail = new BreakdownCategoryOptionDetail();
                                dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                dbDetail.ProductionItemID = materialInfo.ProductionItemID;
                                dbDetail.Quantity = materialInfo.TotalQuantity;
                                dbDetail.UnitID = materialInfo.UnitID;
                            }

                            // add additional item
                            // 16169: COSTFRTE
                            dbDefaultItem = context.BreakdownMng_ProductionItemMaterialType_View.FirstOrDefault(o => o.ProductionItemID == 16169);
                            if (dbDefaultItem != null)
                            {
                                dbDetail = new BreakdownCategoryOptionDetail();
                                dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                dbDetail.ProductionItemID = dbDefaultItem.ProductionItemID;
                                dbDetail.UnitID = dbDefaultItem.UnitID;
                                if (dbDefaultItem.ItemCost.HasValue)
                                {
                                    dbDetail.QuantityPercent = dbDefaultItem.ItemCost;
                                }
                            }
                        }
                    }
                }

                // add frame option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o =>
                    o.BreakdownCategoryID == 2
                    && o.FrameMaterialID.HasValue && o.FrameMaterialID == dbProduct.FrameMaterialID
                    && o.FrameMaterialColorID.HasValue && o.FrameMaterialColorID == dbProduct.FrameMaterialColorID);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 2;
                        dbOption.FrameMaterialID = dbProduct.FrameMaterialID;
                        dbOption.FrameMaterialColorID = dbProduct.FrameMaterialColorID;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }

                        //
                        // add detail
                        //
                        if (dbProduct.MaterialID == 4) // WICKER
                        {
                            foreach (var materialInfo in materials.Where(o => o.WorkCenterID == 7 || o.WorkCenterID == 8).GroupBy(o => o.ProductionItemID).Select(
                                n => new
                                {
                                    ProductionItemID = n.First().ProductionItemID,
                                    TotalQuantity = n.Sum(c => c.Quantity),
                                    UnitID = n.First().UnitID
                                })
                            )
                            {
                                // 7: FRAME; 8: PAINTED
                                dbDetail = new BreakdownCategoryOptionDetail();
                                dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                dbDetail.ProductionItemID = materialInfo.ProductionItemID;
                                dbDetail.Quantity = materialInfo.TotalQuantity;
                                dbDetail.UnitID = materialInfo.UnitID;
                            }

                            // add additional item
                            // 16169: COSTFRTE
                            dbDefaultItem = context.BreakdownMng_ProductionItemMaterialType_View.FirstOrDefault(o => o.ProductionItemID == 16169);
                            if (dbDefaultItem != null)
                            {
                                dbDetail = new BreakdownCategoryOptionDetail();
                                dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                                dbDetail.ProductionItemID = dbDefaultItem.ProductionItemID;
                                dbDetail.UnitID = dbDefaultItem.UnitID;
                                if (dbDefaultItem.ItemCost.HasValue)
                                {
                                    dbDetail.QuantityPercent = dbDefaultItem.ItemCost;
                                }
                            }
                        }
                    }
                }

                // add sub material option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o =>
                    o.BreakdownCategoryID == 3
                    && o.SubMaterialID.HasValue && o.SubMaterialID == dbProduct.SubMaterialID
                    && o.SubMaterialColorID.HasValue && o.SubMaterialColorID == dbProduct.SubMaterialColorID);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 3;
                        dbOption.SubMaterialID = dbProduct.SubMaterialID;
                        dbOption.SubMaterialColorID = dbProduct.SubMaterialColorID;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }

                        //
                        // add detail
                        //
                        // 27: GLASS OR (11: PACKING & 15: WOOD) OR (13: ASSEMBLY & 15: WOOD)
                        foreach (var materialInfo in materials.Where(o => o.ProductionItemGroupID == 27 || (o.ProductionItemGroupID == 15 && (o.WorkCenterID == 11 || o.WorkCenterID == 13))).GroupBy(o => o.ProductionItemID).Select(
                            n => new
                            {
                                ProductionItemID = n.First().ProductionItemID,
                                TotalQuantity = n.Sum(c => c.Quantity),
                                UnitID = n.First().UnitID
                            })
                        )
                        {
                            dbDetail = new BreakdownCategoryOptionDetail();
                            dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            dbDetail.ProductionItemID = materialInfo.ProductionItemID;
                            dbDetail.Quantity = materialInfo.TotalQuantity;
                            dbDetail.UnitID = materialInfo.UnitID;
                        }

                        // add additional item
                        // 16385: VNLAPACK OR 16386: VNLALOAD
                        dbDefaultItem = context.BreakdownMng_ProductionItemMaterialType_View.FirstOrDefault(o => o.ProductionItemID == 16385 || o.ProductionItemID == 16386);
                        if (dbDefaultItem != null)
                        {
                            dbDetail = new BreakdownCategoryOptionDetail();
                            dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            dbDetail.ProductionItemID = dbDefaultItem.ProductionItemID;
                            dbDetail.UnitID = dbDefaultItem.UnitID;
                            if (dbDefaultItem.ItemCost.HasValue)
                            {
                                dbDetail.QuantityPercent = dbDefaultItem.ItemCost;
                            }
                        }
                    }
                }

                // add cushion option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o =>
                    o.BreakdownCategoryID == 4
                    && o.BackCushionID.HasValue && o.BackCushionID == dbProduct.BackCushionID
                    && o.SeatCushionID.HasValue && o.SeatCushionID == dbProduct.SeatCushionID
                    && o.CushionColorID.HasValue && o.CushionColorID == dbProduct.CushionColorID);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 4;
                        dbOption.CushionTypeID = dbProduct.CushionTypeID;
                        dbOption.BackCushionID = dbProduct.BackCushionID;
                        dbOption.SeatCushionID = dbProduct.SeatCushionID;
                        dbOption.CushionColorID = dbProduct.CushionColorID;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }

                        //
                        // add detail
                        //
                        // 46: CUSHION OR 19: SEAT CUSHION OR 18: BACK CUSHION
                        foreach (var materialInfo in materials.Where(o => o.ProductionItemGroupID == 46 || o.ProductionItemGroupID == 19 || o.ProductionItemGroupID == 18).GroupBy(o => o.ProductionItemID).Select(
                            n => new
                            {
                                ProductionItemID = n.First().ProductionItemID,
                                TotalQuantity = n.Sum(c => c.Quantity),
                                UnitID = n.First().UnitID
                            })
                        )
                        {
                            dbDetail = new BreakdownCategoryOptionDetail();
                            dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            dbDetail.ProductionItemID = materialInfo.ProductionItemID;
                            dbDetail.Quantity = materialInfo.TotalQuantity;
                            dbDetail.UnitID = materialInfo.UnitID;
                        }

                        // add additional item
                        // 16631: COSTASSEMBLY
                        dbDefaultItem = context.BreakdownMng_ProductionItemMaterialType_View.FirstOrDefault(o => o.ProductionItemID == 16631);
                        if (dbDefaultItem != null)
                        {
                            dbDetail = new BreakdownCategoryOptionDetail();
                            dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            dbDetail.ProductionItemID = dbDefaultItem.ProductionItemID;
                            dbDetail.UnitID = dbDefaultItem.UnitID;
                            if (dbDefaultItem.ItemCost.HasValue)
                            {
                                dbDetail.QuantityPercent = dbDefaultItem.ItemCost;
                            }
                        }
                    }
                }

                // add packing option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o =>
                    o.BreakdownCategoryID == 5
                    && o.PackagingMethodID.HasValue && o.PackagingMethodID == dbProduct.PackagingMethodID);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 5;
                        dbOption.PackagingMethodID = dbProduct.PackagingMethodID;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }

                        //
                        // add detail
                        //
                        // 11: PACKING && NOT IN (15: WOOD; 27: GLASS; 46: CUSHION; 18: BACK CUSHION; 19: SEAT CUSHION)
                        foreach (var materialInfo in materials.Where(o =>
                            o.WorkCenterID == 11
                            && o.ProductionItemGroupID != 15
                            && o.ProductionItemGroupID != 27
                            && o.ProductionItemGroupID != 46
                            && o.ProductionItemGroupID != 18
                            && o.ProductionItemGroupID != 19
                            ).GroupBy(o => o.ProductionItemID).Select(
                                n => new
                                {
                                    ProductionItemID = n.First().ProductionItemID,
                                    TotalQuantity = n.Sum(c => c.Quantity),
                                    UnitID = n.First().UnitID
                                })
                        )
                        {
                            dbDetail = new BreakdownCategoryOptionDetail();
                            dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            dbDetail.ProductionItemID = materialInfo.ProductionItemID;
                            dbDetail.Quantity = materialInfo.TotalQuantity;
                            dbDetail.UnitID = materialInfo.UnitID;
                        }
                    }
                }

                // add hardware option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == 6);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 6;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }

                        //
                        // add detail
                        //
                        // 10: FINISHING OR (13: ASSEMBLY & 39: OTHER GROUP)
                        foreach (var materialInfo in materials.Where(o => o.WorkCenterID == 10 || (o.WorkCenterID == 13 && o.ProductionItemGroupID == 39)).GroupBy(o => o.ProductionItemID).Select(
                            n => new
                            {
                                ProductionItemID = n.First().ProductionItemID,
                                TotalQuantity = n.Sum(c => c.Quantity),
                                UnitID = n.First().UnitID
                            })
                        )
                        {
                            dbDetail = new BreakdownCategoryOptionDetail();
                            dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            dbDetail.ProductionItemID = materialInfo.ProductionItemID;
                            dbDetail.Quantity = materialInfo.TotalQuantity;
                            dbDetail.UnitID = materialInfo.UnitID;
                        }
                    }
                }

                // add other material option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == 7);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 7;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }
                    }
                }

                // add fsc option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o =>
                    o.BreakdownCategoryID == 8
                    && o.FSCTypeID.HasValue && o.FSCTypeID == dbProduct.FSCTypeID
                    && o.FSCPercentID.HasValue && o.FSCPercentID == dbProduct.FSCPercentID);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 8;
                        dbOption.FSCTypeID = dbProduct.FSCTypeID;
                        dbOption.FSCPercentID = dbProduct.FSCPercentID;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }
                    }
                }

                // add special requirement option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == 9);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 9;
                        dbOption.Description = "NONE";
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }
                    }
                }

                // add management cost option
                dbOption = dbBreakdown.BreakdownCategoryOption.FirstOrDefault(o => o.BreakdownCategoryID == 10);
                if (dbOption == null)
                {
                    foreach (int breakdownCompanyId in supportCompanyIDs)
                    {
                        dbOption = new BreakdownCategoryOption();
                        dbBreakdown.BreakdownCategoryOption.Add(dbOption);
                        dbOption.BreakdownCategoryID = 10;
                        dbOption.CompanyID = breakdownCompanyId;
                        if (companyId == breakdownCompanyId)
                        {
                            dbOption.UpdatedBy = userId;
                            dbOption.UpdatedDate = DateTime.Now;
                        }

                        // add default item
                        foreach (BreakdownMng_ProductionItemMaterialType_View dbDefaultItemIndex in context.BreakdownMng_ProductionItemMaterialType_View.Where(o => o.BreakdownCategoryID == 10).ToList())
                        {
                            dbDetail = new BreakdownCategoryOptionDetail();
                            dbOption.BreakdownCategoryOptionDetail.Add(dbDetail);
                            dbDetail.ProductionItemID = dbDefaultItemIndex.ProductionItemID;
                            dbDetail.UnitID = dbDefaultItemIndex.UnitID;
                            if (dbDefaultItemIndex.ItemCost.HasValue)
                            {
                                dbDetail.QuantityPercent = dbDefaultItemIndex.ItemCost;
                            }
                        }
                    }
                }
                context.SaveChanges();

                if (isNewBreakdown)
                {
                    dbBreakdown.BreakdownUD = dbBreakdown.BreakdownID.ToString("D10");
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
        }
        private BreakdownMng_BOMDetail_View GetGroupNode(List<BreakdownMng_BOMDetail_View> nodes, BreakdownMng_BOMDetail_View node)
        {
            bool toBeContinue = true;
            BreakdownMng_BOMDetail_View result = null;
            BreakdownMng_BOMDetail_View tmpObj = node;
            while (toBeContinue)
            {
                tmpObj = nodes.FirstOrDefault(o => o.BOMStandardID == tmpObj.ParentBOMStandardID.Value);
                if (tmpObj == null)
                {
                    result = null;
                    toBeContinue = false;
                }
                else
                {
                    if (tmpObj.ProductionItemTypeID == 3 && tmpObj.WorkCenterID.HasValue)
                    {
                        result = tmpObj;
                        toBeContinue = false;
                    }
                }
            }
            return result;
        }
        private BreakdownMng_BOMDetail_View GetWorkCenterID(List<BreakdownMng_BOMDetail_View> nodes, BreakdownMng_BOMDetail_View node)
        {
            bool toBeContinue = true;
            BreakdownMng_BOMDetail_View result = null;
            BreakdownMng_BOMDetail_View tmpObj = node;
            while (toBeContinue)
            {
                if (!tmpObj.ParentBOMStandardID.HasValue)
                {
                    result = null;
                    toBeContinue = false;
                }
                else
                {
                    tmpObj = nodes.FirstOrDefault(o => o.BOMStandardID == tmpObj.ParentBOMStandardID.Value);
                    if (tmpObj == null)
                    {
                        result = null;
                        toBeContinue = false;
                    }
                    else
                    {
                        if (tmpObj.WorkCenterID.HasValue)
                        {
                            result = tmpObj;
                            toBeContinue = false;
                        }
                    }
                }
            }
            return result;
        }

        public string GetExcelReportBreakdown(Hashtable filters, out Library.DTO.Notification notification)
        {

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            BreakdownDataObject ds = new BreakdownDataObject();

            try
            {
                int? materialCostOptionID = null;
                int? frameCostOptionID = null;
                int? subMaterialCostOptionID = null;
                int? cushionCostOptionID = null;
                int? packingCostOptionID = null;
                int? hardwareCostOptionID = null;
                int? otherMaterialCostOptionID = null;
                int? fscCostOptionID = null;
                int? specialRequirementCostOptionID = null;
                int? managementCostOptionID = null;
                int? shippedFromFactoryID = null;
                int? breakdownID = null;
                int? companyID = null;

                if (filters.ContainsKey("materialCostOptionID") && !string.IsNullOrEmpty(filters["materialCostOptionID"].ToString()))
                {
                    materialCostOptionID = Convert.ToInt32(filters["materialCostOptionID"]);
                }
                if (filters.ContainsKey("frameCostOptionID") && !string.IsNullOrEmpty(filters["frameCostOptionID"].ToString()))
                {
                    frameCostOptionID = Convert.ToInt32(filters["frameCostOptionID"]);
                }
                if (filters.ContainsKey("cushionCostOptionID") && !string.IsNullOrEmpty(filters["cushionCostOptionID"].ToString()))
                {
                    cushionCostOptionID = Convert.ToInt32(filters["cushionCostOptionID"]);
                }
                if (filters.ContainsKey("packingCostOptionID") && filters["packingCostOptionID"]!= null && !string.IsNullOrEmpty(filters["packingCostOptionID"].ToString()))
                {
                    packingCostOptionID = Convert.ToInt32(filters["packingCostOptionID"]);
                }
                if (filters.ContainsKey("subMaterialCostOptionID") && !string.IsNullOrEmpty(filters["subMaterialCostOptionID"].ToString()))
                {
                    subMaterialCostOptionID = Convert.ToInt32(filters["subMaterialCostOptionID"]);
                }
                if (filters.ContainsKey("hardwareCostOptionID") && !string.IsNullOrEmpty(filters["hardwareCostOptionID"].ToString()))
                {
                    hardwareCostOptionID = Convert.ToInt32(filters["hardwareCostOptionID"]);
                }
                if (filters.ContainsKey("otherMaterialCostOptionID") && !string.IsNullOrEmpty(filters["otherMaterialCostOptionID"].ToString()))
                {
                    otherMaterialCostOptionID = Convert.ToInt32(filters["otherMaterialCostOptionID"]);
                }
                if (filters.ContainsKey("fscCostOptionID") && !string.IsNullOrEmpty(filters["fscCostOptionID"].ToString()))
                {
                    fscCostOptionID = Convert.ToInt32(filters["fscCostOptionID"]);
                }
                if (filters.ContainsKey("specialRequirementCostOptionID") && filters["specialRequirementCostOptionID"] != null && !string.IsNullOrEmpty(filters["specialRequirementCostOptionID"].ToString()))
                {
                    specialRequirementCostOptionID = Convert.ToInt32(filters["specialRequirementCostOptionID"]);
                }
                if (filters.ContainsKey("managementCostOptionID") && !string.IsNullOrEmpty(filters["managementCostOptionID"].ToString()))
                {
                    managementCostOptionID = Convert.ToInt32(filters["managementCostOptionID"]);
                }
                if (filters.ContainsKey("shippedFromFactoryID") && !string.IsNullOrEmpty(filters["shippedFromFactoryID"].ToString()))
                {
                    shippedFromFactoryID = Convert.ToInt32(filters["shippedFromFactoryID"]);
                }
                if (filters.ContainsKey("breakdownID") && !string.IsNullOrEmpty(filters["breakdownID"].ToString()))
                {
                    breakdownID = Convert.ToInt32(filters["breakdownID"]);
                }
                if (filters.ContainsKey("companyID") && !string.IsNullOrEmpty(filters["companyID"].ToString()))
                {
                    companyID = Convert.ToInt32(filters["companyID"]);
                }
                int? materialCostOptionIDOption = null;
                int? frameCostOptionIDOption = null;
                int? subMaterialCostOptionIDOption = null;
                int? cushionCostOptionIDOption = null;
                int? packingCostOptionIDOption = null;
                int? hardwareCostOptionIDOption = null;
                int? otherMaterialCostOptionIDOption = null;
                int? fscCostOptionIDOption = null;
                int? specialRequirementCostOptionIDOption = null;
                int? managementCostOptionIDOption = null;
                if (companyID == 1)
                {
                    materialCostOptionIDOption = materialCostOptionID + 1;
                    frameCostOptionIDOption = frameCostOptionID + 1;
                    subMaterialCostOptionIDOption = subMaterialCostOptionID + 1;
                    cushionCostOptionIDOption = cushionCostOptionID + 1;
                    packingCostOptionIDOption = packingCostOptionID + 1;
                    hardwareCostOptionIDOption = hardwareCostOptionID + 1;
                    otherMaterialCostOptionIDOption = otherMaterialCostOptionID + 1;
                    fscCostOptionIDOption = fscCostOptionID + 1;
                    specialRequirementCostOptionIDOption = specialRequirementCostOptionID + 1;
                    managementCostOptionIDOption = managementCostOptionID + 1;
                }
                if (companyID == 3)
                {
                    materialCostOptionIDOption = materialCostOptionID - 1;
                    frameCostOptionIDOption = frameCostOptionID - 1;
                    subMaterialCostOptionIDOption = subMaterialCostOptionID - 1;
                    cushionCostOptionIDOption = cushionCostOptionID - 1;
                    packingCostOptionIDOption = packingCostOptionID - 1;
                    hardwareCostOptionIDOption = hardwareCostOptionID - 1;
                    otherMaterialCostOptionIDOption = otherMaterialCostOptionID - 1;
                    fscCostOptionIDOption = fscCostOptionID - 1;
                    specialRequirementCostOptionIDOption = specialRequirementCostOptionID - 1;
                    managementCostOptionIDOption = managementCostOptionID - 1;
                }
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("BreakdownMng_function_ExportToExcel", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@materialCostOptionID", materialCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@frameCostOptionID", frameCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@cushionCostOptionID", cushionCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@packingCostOptionID", packingCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@subMaterialCostOptionID", subMaterialCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@hardwareCostOptionID", hardwareCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@otherMaterialCostOptionID", otherMaterialCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@fscCostOptionID", fscCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@specialRequirementCostOptionID", specialRequirementCostOptionID);
                adap.SelectCommand.Parameters.AddWithValue("@managementCostOptionID", managementCostOptionID);

                adap.SelectCommand.Parameters.AddWithValue("@materialCostOptionIDOption", materialCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@frameCostOptionIDOption", frameCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@subMaterialCostOptionIDOption", subMaterialCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@cushionCostOptionIDOption", cushionCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@packingCostOptionIDOption", packingCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@hardwareCostOptionIDOption", hardwareCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@otherMaterialCostOptionIDOption", otherMaterialCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@fscCostOptionIDOption", fscCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@specialRequirementCostOptionIDOption", specialRequirementCostOptionIDOption);
                adap.SelectCommand.Parameters.AddWithValue("@managementCostOptionIDOption", managementCostOptionIDOption);

                adap.SelectCommand.Parameters.AddWithValue("@shippedFromFactoryID", shippedFromFactoryID);
                adap.SelectCommand.Parameters.AddWithValue("@breakdownID", breakdownID);
                adap.SelectCommand.Parameters.AddWithValue("@Currency", "USD");
                adap.TableMappings.Add("Table", "Breakdown");
                adap.TableMappings.Add("Table1", "BreakdownCategoryOption");
                adap.TableMappings.Add("Table2", "BreakdownCategoryOptionGroup");
                adap.TableMappings.Add("Table3", "BreakdownCategoryOptionDetail");
                adap.TableMappings.Add("Table4", "BreakdownCategory");
                adap.TableMappings.Add("Table5", "MasterExchangeRate");
                adap.TableMappings.Add("Table6", "ShippingFactory");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "ListBreakdown");

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

        public List<DTO.BreakdownPriceDTO> GetPurchasingPriceData(int userID, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            List<DTO.BreakdownPriceDTO> data = new List<DTO.BreakdownPriceDTO>();

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                if (companyID.HasValue)
                {
                    // Only AVF have just update price from purchasing.
                    if (companyID.Value != 1)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "You are't not employee of AVF, so that you can't update price!";
                    }
                    else
                    {
                        using (var context = CreateContext())
                        {
                            data = converter.DB2DTO_BreakdownPriceDTOList(context.BreakdownMng_function_GetPurchasingPrice(id).ToList());
                        }
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

        public DTO.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();
            data.ModelStatuses = new List<Support.DTO.ModelStatus>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.ModelStatuses = supportFactory.GetModelStatus().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.GetPriceDetailDTO GetPriceQuotation(int userID, object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.GetPriceDetailDTO dtoPrices = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.GetPriceDetailDTO>();

            try
            {              
                using (BreakDownMngEntities context = CreateContext())
                {
                    int companyId = fwFactory.GetCompanyID(userID).Value;
                    if (companyId != 1)
                    {
                        companyId = 3;
                    }

                    foreach (var itemBreakDownBreakdownCategoryDTO in dtoPrices.BreakDownBreakdownCategoryDTOs)
                    {
                        foreach (var item in itemBreakDownBreakdownCategoryDTO.BreakDownPriceDefaultDTOs)
                        {
                            decimal? price = context.BreakDownMng_function_PurchaseQuotation(item.ProductionItemID, companyId).FirstOrDefault();
                            BreakdownPriceList dbPrice = context.BreakdownPriceList.FirstOrDefault(o => o.ProductionItemID == item.ProductionItemID && o.CompanyID == companyId);
                            if (price.HasValue && dbPrice == null)
                            {
                                //lấy giá trừ Purchase Quotation qua
                                item.UnitPrice = price;                               
                            }                          
                        }
                    }

                    //context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;                
            }
            return dtoPrices;
        }
    }
}
