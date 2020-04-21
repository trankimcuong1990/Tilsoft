using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Module.DashboardMng.DAL
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Framework.DAL.DataFactory dalFramework = new Framework.DAL.DataFactory();

        private DashboardMngEntities CreateContext()
        {
            return new DashboardMngEntities(Library.Helper.CreateEntityConnectionString("DAL.DashboardMngModel"));
        }

        private void BindDashboardDelta(List<DTO.DashboardDetalDTO> result
            , int year
            , int month
            , ref DTO.DashboardDeltaChartDTO DeltaOS
            , ref DTO.DashboardDeltaChartDTO DeltaPIConfirmed
            , ref DTO.DashboardDeltaChartDTO DeltaPIConfirmedPriceFromManySource
            , ref DTO.DashboardDeltaChartDTO DeltaPITotal)
        {
            DTO.DashboardDetalDTO temp = result.Where(s => s.YearData == year && s.MonthData == month && s.Type == 1).FirstOrDefault();
            DeltaOS.Data.Add((DeltaOS.Data.Count > 0 ? DeltaOS.Data.Last() : 0) + (temp != null ? temp.TotalPrice : 0));
            temp = result.Where(s => s.YearData == year && s.MonthData == month && s.Type == 2).FirstOrDefault();
            DeltaPIConfirmed.Data.Add((DeltaPIConfirmed.Data.Count > 0 ? DeltaPIConfirmed.Data.Last() : 0) + (temp != null ? temp.TotalPrice : 0));
            temp = result.Where(s => s.YearData == year && s.MonthData == month && s.Type == 3).FirstOrDefault();
            DeltaPIConfirmedPriceFromManySource.Data.Add((DeltaPIConfirmedPriceFromManySource.Data.Count > 0 ? DeltaPIConfirmedPriceFromManySource.Data.Last() : 0) + (temp != null ? temp.TotalPrice : 0));
            temp = result.Where(s => s.YearData == year && s.MonthData == month && s.Type == 4).FirstOrDefault();
            DeltaPITotal.Data.Add((DeltaPITotal.Data.Count > 0 ? DeltaPITotal.Data.Last() : 0) + (temp != null ? temp.TotalPrice : 0));
        }

        private List<string> GetMonthDisplaytDashboardDetal(List<DTO.DashboardDetalDTO> param
            , string season
            , ref DTO.DashboardDeltaChartDTO DeltaOS
            , ref DTO.DashboardDeltaChartDTO DeltaPIConfirmed
            , ref DTO.DashboardDeltaChartDTO DeltaPIConfirmedPriceFromManySource
            , ref DTO.DashboardDeltaChartDTO DeltaPITotal)
        {
            string[] arrSeason = season.Split('/');
            int preYear = int.Parse(arrSeason[0]);
            int nextYear = int.Parse(arrSeason[1]);

            DateTime minDate = param.Min(s => s.Date);
            DateTime maxDate = param.Max(s => s.Date);

            //số tháng giữa 2 min max time
            int monthDisplay = Math.Abs((maxDate.Month - minDate.Month) + 12 * (maxDate.Year - minDate.Year)) + 1;

            List<string> result = new List<string>();
            for (int i = 0; i < monthDisplay; i++)
            {
                DateTime temp = minDate.Date.AddMonths(i);
                string monthName = temp.Year.ToString() + "-" + temp.ToString("MMM", CultureInfo.InvariantCulture);
                BindDashboardDelta(param, temp.Year, temp.Month, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                result.Add(monthName);
            }

            return result;
        }
        public DTO.DashboardDetalResultDTO GetDashboardDetal(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DashboardDetalResultDTO result = new DTO.DashboardDetalResultDTO();
            result.Data = new List<DTO.DashboardDeltaChartDTO>();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    DTO.DashboardDeltaChartDTO DeltaOS = new DTO.DashboardDeltaChartDTO();
                    DeltaOS.Name = "DeltaOS";
                    DeltaOS.Data = new List<decimal>();
                    DTO.DashboardDeltaChartDTO DeltaPIConfirmed = new DTO.DashboardDeltaChartDTO();
                    DeltaPIConfirmed.Name = "DeltaPIConfirmed";
                    DeltaPIConfirmed.Data = new List<decimal>();
                    DTO.DashboardDeltaChartDTO DeltaPIConfirmedPriceFromManySource = new DTO.DashboardDeltaChartDTO();
                    DeltaPIConfirmedPriceFromManySource.Name = "DeltaPIConfirmedPriceFromManySource";
                    DeltaPIConfirmedPriceFromManySource.Data = new List<decimal>();
                    DTO.DashboardDeltaChartDTO DeltaPITotal = new DTO.DashboardDeltaChartDTO();
                    DeltaPITotal.Name = "DeltaPITotal";
                    DeltaPITotal.Data = new List<decimal>();

                    List<DTO.DashboardDetalDTO> temp = converter.DB2DTO_DashboardDetalDTO(context.AdminDashboardMng_function_ChartDelta(season, null, false, false).ToList());
                    result.Months = GetMonthDisplaytDashboardDetal(temp, season, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);

                    //for (int i = 0; i < result.Months.Count(); i++)
                    //{
                    //    switch (i)
                    //    {
                    //        case 0:
                    //            BindDashboardDelta(temp, preYear, 10, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);                             
                    //            break;
                    //        case 1:
                    //            BindDashboardDelta(temp, preYear, 11, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 2:
                    //            BindDashboardDelta(temp, preYear, 12, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 3:
                    //            BindDashboardDelta(temp, nextYear, 1, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 4:
                    //            BindDashboardDelta(temp, nextYear, 2, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 5:
                    //            BindDashboardDelta(temp, nextYear, 3, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 6:
                    //            BindDashboardDelta(temp, nextYear, 4, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 7:
                    //            BindDashboardDelta(temp, nextYear, 5, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 8:
                    //            BindDashboardDelta(temp, nextYear, 6, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 9:
                    //            BindDashboardDelta(temp, nextYear, 7, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 10:
                    //            BindDashboardDelta(temp, nextYear, 8, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        case 11:
                    //            BindDashboardDelta(temp, nextYear, 9, ref DeltaOS, ref DeltaPIConfirmed, ref DeltaPIConfirmedPriceFromManySource, ref DeltaPITotal);
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //}

                    result.Data.Add(DeltaOS);
                    result.Data.Add(DeltaPIConfirmed);
                    result.Data.Add(DeltaPIConfirmedPriceFromManySource);
                    result.Data.Add(DeltaPITotal);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return result;
        }

        // Main function to get data
        public DTO.DashboardReportData GetReportData(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DashboardReportData data = new DTO.DashboardReportData();

            // User 
            data.UserDataRpt = new List<DTO.UserDataRpt>();
            // Hit Count
            data.HitCountDataRpt = new List<DTO.HitCountDataRpt>();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    // User
                    var UserData = context.DashboardMng_function_getUserData(Library.OMSHelper.Helper.GetCurrentSeason());
                    data.UserDataRpt = converter.DB2DTO_UserDataRpt(UserData.ToList());

                    // HitCount
                    data.HitCountDataRpt = converter.DB2DTO_HitCountDataRpt(context.DashboardMng_HitCountDataRpt_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public DTO.DashboardUserData GetDataForUserDashBoard(int userId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification()
            {
                Type = Library.DTO.NotificationType.Success
            };

            DTO.DashboardUserData data = new DTO.DashboardUserData()
            {
                QuotationData = new List<DTO.DashboardQuotationData>(),
                PurchasingInvoiceData = new List<DTO.DashboardPurchasingInvoiceData>(),
                SampleProductionData = new List<DTO.DashboardSampleOrderData>(),
                LabelingPackagingData = new List<DTO.DashboardLabelingPackagingData>(),
                TotalHitPerWeekOfUserData = new List<DTO.DashboardTotalHitPerWeekOfUserData>(),
                TotalHitPerWeekOfFactoryData = new List<DTO.DashboardTotalHitPerWeekOfFactoryData>(),
                OfferApproveNotYets = new List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO>(),
            };

            try
            {
                using (var context = CreateContext())
                {
                    var dbCountQuotation = context.DashboardMng_function_CanPerformActionModule(userId, "PriceQuotationMng").FirstOrDefault();
                    data.IsModuleQuotation = (dbCountQuotation.Value > 0);

                    if (data.IsModuleQuotation)
                    {
                        var dbQuotation = context.DashboardMng_function_GetQuotation(userId, season).OrderByDescending(o => o.LastUpdatedDate);
                        data.QuotationData = converter.DB2DTO_Quotation(dbQuotation.ToList());
                    }

                    var dbCountPurchasingInvoice = context.DashboardMng_function_CanPerformActionModule(userId, "PurchasingInvoiceMng").FirstOrDefault();
                    data.IsModulePurchasingInvoice = (dbCountPurchasingInvoice.Value > 0);

                    if (data.IsModulePurchasingInvoice)
                    {
                        var dbPurchasingInvoice = context.DashboardMng_function_GetPurchasingInvoice(userId, season).OrderByDescending(o => o.UpdatedDate);
                        data.PurchasingInvoiceData = converter.DB2DTO_PurchasingInvoice(dbPurchasingInvoice.ToList());
                    }

                    var dbCountSampleProduction = context.DashboardMng_function_CanPerformActionModule(userId, "Sample2Mng").FirstOrDefault();
                    data.IsModuleSampleProduction = (dbCountSampleProduction.Value > 0);

                    if (data.IsModuleSampleProduction)
                    {
                        var dbSampleProduction = context.DashboardMng_function_GetSampleOrder(userId, season).OrderByDescending(o => o.UpdatedDate);
                        data.SampleProductionData = converter.DB2DTO_SampleProduction(dbSampleProduction.ToList());
                    }

                    var dbCountLabelingPackaging = context.DashboardMng_function_CanPerformActionModule(userId, "LabelingPackaging").FirstOrDefault();
                    data.IsModuleLabelingPackaging = (dbCountLabelingPackaging.Value > 0);

                    if (data.IsModuleLabelingPackaging)
                    {
                        var dbLabelingPackaging = context.DashboardMng_function_GetLabelingPackaging(userId, season).OrderByDescending(o => o.UpdatedDate);
                        data.LabelingPackagingData = converter.DB2DTO_LabelingPackaging(dbLabelingPackaging.ToList());
                    }

                    var dbTotalHitPerWeekOfUser = context.DashboardMng_function_TotalHitOfUsers(season, userId);
                    data.TotalHitPerWeekOfUserData = converter.DB2DTO_TotalHitPerWeekOfUser(dbTotalHitPerWeekOfUser.ToList());

                    int? companyId = dalFramework.GetCompanyID(userId);

                    var dbTotalHitPerWeekOfFactory = context.DashboardMng_function_TotalHitPerWeekFactory(season, companyId, userId);
                    data.TotalHitPerWeekOfFactoryData = converter.DB2DTO_TotalHitPerWeekOfFactory(dbTotalHitPerWeekOfFactory.ToList());

                    List<int?> companies = context.SupportMng_InternalCompany2_View.Select(o => o.InternalCompanyID).ToList();
                    data.IsCompany = companies.Contains(companyId);

                    // Offer
                    var dbOfferApprove = context.UserDashboardMng_OfferSeasonNotApprovedYet_View.ToList();
                    data.OfferApproveNotYets = converter.DB2DTO_UserDashboardOfferSeasonNotApprovedYetDTO(dbOfferApprove);

                    List<UserDashboardMng_function_getExchangeRate_Result> exchangeRates = context.UserDashboardMng_function_getExchangeRate().ToList();

                    foreach (var item in data.OfferApproveNotYets)
                    {
                        List<UserDashboardMng_function_OfferSeasonDetail_Result> offerLines = context.UserDashboardMng_function_OfferSeasonDetail(item.OfferSeasonID).ToList();
                        decimal exRate = 0;
                        UserDashboardMng_function_getExchangeRate_Result exchangeRate = exchangeRates.Where(s => s.Season == item.Season).FirstOrDefault();
                        if (exchangeRate != null)
                            exRate = exchangeRate.ExRate.Value;

                        UserDashboardMng_function_ClientEstimatedAdditionalCost_Result clientEstimatedAdditionalCost = context.UserDashboardMng_function_ClientEstimatedAdditionalCost(item.OfferSeasonID).FirstOrDefault();
                        UserDashboardMng_function_PaymentTerm_Result paymentTerm = context.UserDashboardMng_function_PaymentTerm(item.OfferSeasonID).FirstOrDefault();


                        decimal sumSalePriceInUSD = 0;
                        decimal sumPurchasingPriceInUSD = 0;
                        decimal sumCommissionInUSD = 0;
                        decimal sumLcCostInUSD = 0;
                        decimal sumInterestInUSD = 0;
                        decimal sumDamagesCostInUSD = 0;
                        decimal sumTransportationInUSD = 0;

                        foreach (var offerLine in offerLines)
                        {
                            UserDashboardMng_function_Shared_OfferSeasonDetailAbility_Result shared_OfferLineLoadAbility = context.UserDashboardMng_function_Shared_OfferSeasonDetailAbility(offerLine.OfferSeasonDetailID).FirstOrDefault();

                            // tính lại delta percent
                            decimal salePriceInUSD = 0;
                            decimal purchasingPriceInUSD = 0;
                            decimal commissionInUSD = 0;
                            decimal lcCostInUSD = 0;
                            decimal interestInUSD = 0;
                            decimal damagesCostInUSD = 0;
                            decimal transportationInUSD = 0;

                            decimal unitPrice = 0;
                            unitPrice = offerLine.SalePrice.HasValue ? offerLine.SalePrice.Value : 0;

                            decimal planingPurchasingPrice = 0;
                            planingPurchasingPrice = offerLine.PlaningPurchasingPrice.HasValue ? offerLine.PlaningPurchasingPrice.Value : 0;

                            decimal commissionPercent = 0;
                            commissionPercent = offerLine.CommissionPercent.HasValue ? offerLine.CommissionPercent.Value : 0;

                            decimal downValue = 0;
                            downValue = paymentTerm.DownValue.HasValue ? paymentTerm.DownValue.Value : 0;

                            decimal transportationFee = 0;
                            transportationFee = offerLine.TransportationFee.HasValue ? offerLine.TransportationFee.Value : 0;

                            //salePriceInUSD
                            if (item.Currency == "EUR")
                                salePriceInUSD = unitPrice * exRate;
                            else
                                salePriceInUSD = unitPrice;

                            //purchasingPriceInUSD
                            purchasingPriceInUSD = planingPurchasingPrice;

                            //commissionInUSD
                            if (item.Currency == "EUR")
                                commissionInUSD = unitPrice * exRate * commissionPercent / 100;
                            else
                                commissionInUSD = unitPrice * commissionPercent / 100;

                            //lcCostInUSD
                            if (clientEstimatedAdditionalCost.LCCostPercent.HasValue)
                            {
                                if (item.Currency == "EUR")
                                    lcCostInUSD = unitPrice * clientEstimatedAdditionalCost.LCCostPercent.Value / 100 * exRate;
                                else
                                    lcCostInUSD = unitPrice * clientEstimatedAdditionalCost.LCCostPercent.Value / 100;
                            }
                            else if (paymentTerm.PaymentTypeID == 4)
                            {
                                if (item.Currency == "EUR")
                                    lcCostInUSD = unitPrice / 100 * exRate;
                                else
                                    lcCostInUSD = unitPrice / 100;
                            }

                            //interestInUSD
                            if (clientEstimatedAdditionalCost.InterestCostPercent.HasValue)
                            {
                                interestInUSD = unitPrice * clientEstimatedAdditionalCost.InterestCostPercent.Value / 100 * (item.Currency == "EUR" ? exRate : 1);
                            }
                            else if (paymentTerm.InDays.HasValue)
                            {
                                interestInUSD = (unitPrice * paymentTerm.InDays.Value * (item.Currency == "EUR" ? exRate : 1) * (paymentTerm.PaymentMethod == "DP-PERCENT" ? (1 - downValue / 100) : 1) * (decimal)(4.5 / 100 / 360));
                            }

                            //damagesCostInUSD
                            damagesCostInUSD = (offerLine.PlaningPurchasingPrice.HasValue ? offerLine.PlaningPurchasingPrice.Value : (decimal)0.001) * (decimal)1.25 / 100;

                            //transportationInUSD
                            if (item.Currency == "EUR" && shared_OfferLineLoadAbility.Qnt40HC.HasValue && shared_OfferLineLoadAbility.Qnt40HC > 0)
                                transportationInUSD = transportationFee / shared_OfferLineLoadAbility.Qnt40HC.Value * exRate;

                            sumSalePriceInUSD += salePriceInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                            sumPurchasingPriceInUSD += purchasingPriceInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                            sumCommissionInUSD += commissionInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                            sumLcCostInUSD += lcCostInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                            sumInterestInUSD += interestInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                            sumDamagesCostInUSD += damagesCostInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                            sumTransportationInUSD += transportationInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                        }

                        item.DeltaPercent = (sumSalePriceInUSD
                                        - sumPurchasingPriceInUSD
                                        - sumCommissionInUSD
                                        - sumLcCostInUSD
                                        - sumInterestInUSD
                                        - sumDamagesCostInUSD
                                        - sumTransportationInUSD)
                                        / (sumPurchasingPriceInUSD > 0 ? sumPurchasingPriceInUSD : 1) * 100;
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

        // Production Oveview
        public DTO.DashboardProductionData GetProductionData(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DashboardProductionData data = new DTO.DashboardProductionData
            {
                ProductionOverview = new List<DTO.DashboardProductionOverview>(),
                FactoryAccessList = new List<DTO.DashboardFactoryAccessList>(),
                QuotationData = new List<DTO.DashboardQuotationData>(),
                PurchasingInvoiceData = new List<DTO.DashboardPurchasingInvoiceData>(),
                SampleProductionData = new List<DTO.DashboardSampleOrderData>(),
                LabelingPackagingData = new List<DTO.DashboardLabelingPackagingData>(),
                TotalHitPerWeekOfUserData = new List<DTO.DashboardTotalHitPerWeekOfUserData>(),
                TotalHitPerWeekOfFactoryData = new List<DTO.DashboardTotalHitPerWeekOfFactoryData>(),
                FinanceOverviews = new List<DTO.DashboardFinanceOverviewData>(),
                FinanceChart = new List<DTO.DashboardFinanceChart>(),
                Seasons = new List<Support.DTO.Season>(),
                OfferApproveNotYets = new List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO>(),
                OfferApprovedAccountManagers = new List<DTO.OfferApprovedAccountManagerDTO>(),
                OfferPricings = new List<DTO.OfferPricingDTO>(),
            };
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    var dbCountFactoryOrder = context.DashboardMng_function_CanPerformActionModule(userID, "FactoryOrderMng").FirstOrDefault();
                    if (dbCountFactoryOrder.Value > 0)
                    {
                        // Get Production Overview
                        var ProductionOverview = context.DashboardMng_function_getProductionOverview(userID, season).ToList();
                        data.ProductionOverview = converter.DB2DTO_ProductionOverview(ProductionOverview);

                        //Get Available Factory List
                        var FactoryAccessList = context.DashboardMng_function_SearchFactoryAccessList(userID).ToList();
                        data.FactoryAccessList = converter.DB2DTO_FactoryAccessList(FactoryAccessList);
                    }
                    else
                    {
                        data = null;
                    }

                    ////ObjectParameter output = new ObjectParameter("Output", typeof(int));
                    ////var rsOutput = context.DashboardMng_function_CheckFactoriesManufacturing(userID).FirstOrDefault().Value;
                    ////int cnvOutput = Convert.ToInt32(rsOutput.ToString());

                    ////data.IsFactoryManufacturing = (cnvOutput == 1);

                    ////if (cnvOutput == 1)
                    ////{
                    ////    data.FinanceOverviews = converter.DB2DTO_FinanceOverview(context.DashboardMng_function_FinanceOverview(season, userID).ToList());
                    ////    //data.FinanceChart = converter.DB2DTO_FinanceOverviewChart(context.Dashboard_function_GetFinanceOverviewChart(season, userID, data.FactoryAccessList[0].FactoryID).ToList());
                    ////}

                    //var dbCountQuotation = context.DashboardMng_function_CanPerformActionModule(userID, "PriceQuotationMng").FirstOrDefault();
                    //data.IsModuleQuotation = (dbCountQuotation.Value > 0);

                    //if (data.IsModuleQuotation)
                    //{
                    //    var dbQuotation = context.DashboardMng_function_GetQuotation(userID, season).OrderByDescending(o => o.LastUpdatedDate);
                    //    data.QuotationData = converter.DB2DTO_Quotation(dbQuotation.ToList());
                    //}

                    //var dbCountPurchasingInvoice = context.DashboardMng_function_CanPerformActionModule(userID, "PurchasingInvoiceMng").FirstOrDefault();
                    //data.IsModulePurchasingInvoice = (dbCountPurchasingInvoice.Value > 0);

                    //if (data.IsModulePurchasingInvoice)
                    //{
                    //    var dbPurchasingInvoice = context.DashboardMng_function_GetPurchasingInvoice(userID, season).OrderByDescending(o => o.UpdatedDate);
                    //    data.PurchasingInvoiceData = converter.DB2DTO_PurchasingInvoice(dbPurchasingInvoice.ToList());
                    //}

                    //var dbCountSampleProduction = context.DashboardMng_function_CanPerformActionModule(userID, "Sample2Mng").FirstOrDefault();
                    //data.IsModuleSampleProduction = (dbCountSampleProduction.Value > 0);

                    //if (data.IsModuleSampleProduction)
                    //{
                    //    var dbSampleProduction = context.DashboardMng_function_GetSampleOrder(userID, season).OrderByDescending(o => o.UpdatedDate);
                    //    data.SampleProductionData = converter.DB2DTO_SampleProduction(dbSampleProduction.ToList());
                    //}

                    //var dbCountLabelingPackaging = context.DashboardMng_function_CanPerformActionModule(userID, "LabelingPackaging").FirstOrDefault();
                    //data.IsModuleLabelingPackaging = (dbCountLabelingPackaging.Value > 0);

                    //if (data.IsModuleLabelingPackaging)
                    //{
                    //    var dbLabelingPackaging = context.DashboardMng_function_GetLabelingPackaging(userID, season).OrderByDescending(o => o.UpdatedDate);
                    //    data.LabelingPackagingData = converter.DB2DTO_LabelingPackaging(dbLabelingPackaging.ToList());
                    //}

                    //var dbTotalHitPerWeekOfUser = context.DashboardMng_function_TotalHitOfUsers(season, userID);
                    //data.TotalHitPerWeekOfUserData = converter.DB2DTO_TotalHitPerWeekOfUser(dbTotalHitPerWeekOfUser.ToList());

                    //int? companyId = dalFramework.GetCompanyID(userID);

                    //var dbTotalHitPerWeekOfFactory = context.DashboardMng_function_TotalHitPerWeekFactory(season, companyId, userID);
                    //data.TotalHitPerWeekOfFactoryData = converter.DB2DTO_TotalHitPerWeekOfFactory(dbTotalHitPerWeekOfFactory.ToList());

                    //List<int?> companies = context.SupportMng_InternalCompany2_View.Select(o => o.InternalCompanyID).ToList();
                    //data.IsCompany = companies.Contains(companyId);

                    //Support.DAL.DataFactory support_Factory = new Support.DAL.DataFactory();
                    //data.Seasons = support_Factory.GetSeason();

                    // Offer
                    //var dbOfferApprove = context.UserDashboardMng_OfferSeasonNotApprovedYet_View.ToList();
                    //data.OfferApproveNotYets = converter.DB2DTO_UserDashboardOfferSeasonNotApprovedYetDTO(dbOfferApprove);
                    data.OfferApproveNotYets = converter.DB2DTO_WidgetMng_function_GetOfferNeedAttention(context.WidgetMng_function_GetOfferNeedAttention("2019/2020", userID).ToList());

                    //List<UserDashboardMng_function_getExchangeRate_Result> exchangeRates = context.UserDashboardMng_function_getExchangeRate().ToList();

                    //foreach (var item in data.OfferApproveNotYets)
                    //{
                    //    List<UserDashboardMng_function_OfferSeasonDetail_Result> offerLines = context.UserDashboardMng_function_OfferSeasonDetail(item.OfferSeasonID).ToList();
                    //    decimal exRate = 0;
                    //    UserDashboardMng_function_getExchangeRate_Result exchangeRate = exchangeRates.Where(s => s.Season == item.Season).FirstOrDefault();
                    //    if (exchangeRate != null)
                    //        exRate = exchangeRate.ExRate.Value;

                    //    UserDashboardMng_function_ClientEstimatedAdditionalCost_Result clientEstimatedAdditionalCost = context.UserDashboardMng_function_ClientEstimatedAdditionalCost(item.OfferSeasonID).FirstOrDefault();
                    //    UserDashboardMng_function_PaymentTerm_Result paymentTerm = context.UserDashboardMng_function_PaymentTerm(item.OfferSeasonID).FirstOrDefault();


                    //    decimal sumSalePriceInUSD = 0;
                    //    decimal sumPurchasingPriceInUSD = 0;
                    //    decimal sumCommissionInUSD = 0;
                    //    decimal sumLcCostInUSD = 0;
                    //    decimal sumInterestInUSD = 0;
                    //    decimal sumDamagesCostInUSD = 0;
                    //    decimal sumTransportationInUSD = 0;

                    //    foreach (var offerLine in offerLines)
                    //    {
                    //        UserDashboardMng_function_Shared_OfferSeasonDetailAbility_Result shared_OfferLineLoadAbility = context.UserDashboardMng_function_Shared_OfferSeasonDetailAbility(offerLine.OfferSeasonDetailID).FirstOrDefault();

                    //        // tính lại delta percent
                    //        decimal salePriceInUSD = 0;
                    //        decimal purchasingPriceInUSD = 0;
                    //        decimal commissionInUSD = 0;
                    //        decimal lcCostInUSD = 0;
                    //        decimal interestInUSD = 0;
                    //        decimal damagesCostInUSD = 0;
                    //        decimal transportationInUSD = 0;

                    //        decimal unitPrice = 0;
                    //        unitPrice = offerLine.SalePrice.HasValue ? offerLine.SalePrice.Value : 0;

                    //        decimal planingPurchasingPrice = 0;
                    //        planingPurchasingPrice = offerLine.PlaningPurchasingPrice.HasValue ? offerLine.PlaningPurchasingPrice.Value : 0;

                    //        decimal commissionPercent = 0;
                    //        commissionPercent = offerLine.CommissionPercent.HasValue ? offerLine.CommissionPercent.Value : 0;

                    //        decimal downValue = 0;
                    //        downValue = paymentTerm.DownValue.HasValue ? paymentTerm.DownValue.Value : 0;

                    //        decimal transportationFee = 0;
                    //        transportationFee = offerLine.TransportationFee.HasValue ? offerLine.TransportationFee.Value : 0;

                    //        //salePriceInUSD
                    //        if (item.Currency == "EUR")
                    //            salePriceInUSD = unitPrice * exRate;
                    //        else
                    //            salePriceInUSD = unitPrice;

                    //        //purchasingPriceInUSD
                    //        purchasingPriceInUSD = planingPurchasingPrice;

                    //        //commissionInUSD
                    //        if (item.Currency == "EUR")
                    //            commissionInUSD = unitPrice * exRate * commissionPercent / 100;
                    //        else
                    //            commissionInUSD = unitPrice * commissionPercent / 100;

                    //        //lcCostInUSD
                    //        if (clientEstimatedAdditionalCost.LCCostPercent.HasValue)
                    //        {
                    //            if (item.Currency == "EUR")
                    //                lcCostInUSD = unitPrice * clientEstimatedAdditionalCost.LCCostPercent.Value / 100 * exRate;
                    //            else
                    //                lcCostInUSD = unitPrice * clientEstimatedAdditionalCost.LCCostPercent.Value / 100;
                    //        }
                    //        else if (paymentTerm.PaymentTypeID == 4)
                    //        {
                    //            if (item.Currency == "EUR")
                    //                lcCostInUSD = unitPrice / 100 * exRate;
                    //            else
                    //                lcCostInUSD = unitPrice / 100;
                    //        }

                    //        //interestInUSD
                    //        if (clientEstimatedAdditionalCost.InterestCostPercent.HasValue)
                    //        {
                    //            interestInUSD = unitPrice * clientEstimatedAdditionalCost.InterestCostPercent.Value / 100 * (item.Currency == "EUR" ? exRate : 1);
                    //        }
                    //        else if (paymentTerm.InDays.HasValue)
                    //        {
                    //            interestInUSD = (unitPrice * paymentTerm.InDays.Value * (item.Currency == "EUR" ? exRate : 1) * (paymentTerm.PaymentMethod == "DP-PERCENT" ? (1 - downValue / 100) : 1) * (decimal)(4.5 / 100 / 360));
                    //        }

                    //        //damagesCostInUSD
                    //        damagesCostInUSD = (offerLine.PlaningPurchasingPrice.HasValue ? offerLine.PlaningPurchasingPrice.Value : (decimal)0.001) * (decimal)1.25 / 100;

                    //        //transportationInUSD
                    //        if (item.Currency == "EUR" && shared_OfferLineLoadAbility.Qnt40HC.HasValue && shared_OfferLineLoadAbility.Qnt40HC > 0)
                    //            transportationInUSD = transportationFee / shared_OfferLineLoadAbility.Qnt40HC.Value * exRate;

                    //        sumSalePriceInUSD += salePriceInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //        sumPurchasingPriceInUSD += purchasingPriceInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //        sumCommissionInUSD += commissionInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //        sumLcCostInUSD += lcCostInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //        sumInterestInUSD += interestInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //        sumDamagesCostInUSD += damagesCostInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //        sumTransportationInUSD += transportationInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //    }

                    //    item.DeltaPercent = (sumSalePriceInUSD
                    //                    - sumPurchasingPriceInUSD
                    //                    - sumCommissionInUSD
                    //                    - sumLcCostInUSD
                    //                    - sumInterestInUSD
                    //                    - sumDamagesCostInUSD
                    //                    - sumTransportationInUSD)
                    //                    / (sumPurchasingPriceInUSD > 0 ? sumPurchasingPriceInUSD : 1) * 100;
                    //}

                    // Offer for team Account manager
                    //var teamAccountManager = context.UserDashboardMng_TeamAccountManager_View.FirstOrDefault(o => o.UserID == userID);
                    //if (teamAccountManager != null)
                    //{
                    //    data.IsTeamAccountManager = true;
                    //    data.OfferApprovedAccountManagers = converter.DB2DTO_OfferSeasonApprovedAccountManagerDTO(context.UserDashboardMng_OfferSeasonNotApprovedYet_View.ToList());

                    //    foreach (var item in data.OfferApprovedAccountManagers)
                    //    {
                    //        List<UserDashboardMng_function_OfferSeasonDetail_Result> offerLines = context.UserDashboardMng_function_OfferSeasonDetail(item.OfferSeasonID).ToList();
                    //        decimal exRate = 0;
                    //        UserDashboardMng_function_getExchangeRate_Result exchangeRate = exchangeRates.Where(s => s.Season == item.Season).FirstOrDefault();
                    //        if (exchangeRate != null)
                    //            exRate = exchangeRate.ExRate.Value;

                    //        UserDashboardMng_function_ClientEstimatedAdditionalCost_Result clientEstimatedAdditionalCost = context.UserDashboardMng_function_ClientEstimatedAdditionalCost(item.OfferSeasonID).FirstOrDefault();
                    //        UserDashboardMng_function_PaymentTerm_Result paymentTerm = context.UserDashboardMng_function_PaymentTerm(item.OfferSeasonID).FirstOrDefault();


                    //        decimal sumSalePriceInUSD = 0;
                    //        decimal sumPurchasingPriceInUSD = 0;
                    //        decimal sumCommissionInUSD = 0;
                    //        decimal sumLcCostInUSD = 0;
                    //        decimal sumInterestInUSD = 0;
                    //        decimal sumDamagesCostInUSD = 0;
                    //        decimal sumTransportationInUSD = 0;

                    //        foreach (var offerLine in offerLines)
                    //        {
                    //            UserDashboardMng_function_Shared_OfferSeasonDetailAbility_Result shared_OfferLineLoadAbility = context.UserDashboardMng_function_Shared_OfferSeasonDetailAbility(offerLine.OfferSeasonDetailID).FirstOrDefault();

                    //            // tính lại delta percent
                    //            decimal salePriceInUSD = 0;
                    //            decimal purchasingPriceInUSD = 0;
                    //            decimal commissionInUSD = 0;
                    //            decimal lcCostInUSD = 0;
                    //            decimal interestInUSD = 0;
                    //            decimal damagesCostInUSD = 0;
                    //            decimal transportationInUSD = 0;

                    //            decimal unitPrice = 0;
                    //            unitPrice = offerLine.SalePrice.HasValue ? offerLine.SalePrice.Value : 0;

                    //            decimal planingPurchasingPrice = 0;
                    //            planingPurchasingPrice = offerLine.PlaningPurchasingPrice.HasValue ? offerLine.PlaningPurchasingPrice.Value : 0;

                    //            decimal commissionPercent = 0;
                    //            commissionPercent = offerLine.CommissionPercent.HasValue ? offerLine.CommissionPercent.Value : 0;

                    //            decimal downValue = 0;
                    //            downValue = paymentTerm.DownValue.HasValue ? paymentTerm.DownValue.Value : 0;

                    //            decimal transportationFee = 0;
                    //            transportationFee = offerLine.TransportationFee.HasValue ? offerLine.TransportationFee.Value : 0;

                    //            //salePriceInUSD
                    //            if (item.Currency == "EUR")
                    //                salePriceInUSD = unitPrice * exRate;
                    //            else
                    //                salePriceInUSD = unitPrice;

                    //            //purchasingPriceInUSD
                    //            purchasingPriceInUSD = planingPurchasingPrice;

                    //            //commissionInUSD
                    //            if (item.Currency == "EUR")
                    //                commissionInUSD = unitPrice * exRate * commissionPercent / 100;
                    //            else
                    //                commissionInUSD = unitPrice * commissionPercent / 100;

                    //            //lcCostInUSD
                    //            if (clientEstimatedAdditionalCost.LCCostPercent.HasValue)
                    //            {
                    //                if (item.Currency == "EUR")
                    //                    lcCostInUSD = unitPrice * clientEstimatedAdditionalCost.LCCostPercent.Value / 100 * exRate;
                    //                else
                    //                    lcCostInUSD = unitPrice * clientEstimatedAdditionalCost.LCCostPercent.Value / 100;
                    //            }
                    //            else if (paymentTerm.PaymentTypeID == 4)
                    //            {
                    //                if (item.Currency == "EUR")
                    //                    lcCostInUSD = unitPrice / 100 * exRate;
                    //                else
                    //                    lcCostInUSD = unitPrice / 100;
                    //            }

                    //            //interestInUSD
                    //            if (clientEstimatedAdditionalCost.InterestCostPercent.HasValue)
                    //            {
                    //                interestInUSD = unitPrice * clientEstimatedAdditionalCost.InterestCostPercent.Value / 100 * (item.Currency == "EUR" ? exRate : 1);
                    //            }
                    //            else if (paymentTerm.InDays.HasValue)
                    //            {
                    //                interestInUSD = (unitPrice * paymentTerm.InDays.Value * (item.Currency == "EUR" ? exRate : 1) * (paymentTerm.PaymentMethod == "DP-PERCENT" ? (1 - downValue / 100) : 1) * (decimal)(4.5 / 100 / 360));
                    //            }

                    //            //damagesCostInUSD
                    //            damagesCostInUSD = (offerLine.PlaningPurchasingPrice.HasValue ? offerLine.PlaningPurchasingPrice.Value : (decimal)0.001) * (decimal)1.25 / 100;

                    //            //transportationInUSD
                    //            if (item.Currency == "EUR" && shared_OfferLineLoadAbility.Qnt40HC.HasValue && shared_OfferLineLoadAbility.Qnt40HC > 0)
                    //                transportationInUSD = transportationFee / shared_OfferLineLoadAbility.Qnt40HC.Value * exRate;

                    //            sumSalePriceInUSD += salePriceInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumPurchasingPriceInUSD += purchasingPriceInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumCommissionInUSD += commissionInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumLcCostInUSD += lcCostInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumInterestInUSD += interestInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumDamagesCostInUSD += damagesCostInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumTransportationInUSD += transportationInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //        }

                    //        item.DeltaPercent = (sumSalePriceInUSD
                    //                        - sumPurchasingPriceInUSD
                    //                        - sumCommissionInUSD
                    //                        - sumLcCostInUSD
                    //                        - sumInterestInUSD
                    //                        - sumDamagesCostInUSD
                    //                        - sumTransportationInUSD)
                    //                        / (sumPurchasingPriceInUSD > 0 ? sumPurchasingPriceInUSD : 1) * 100;
                    //    }
                    //}

                    /// Offer pricing
                    /// Permission can read AVT and Eurofar purchasing price
                    //bool? hasPermissionCanRead = dalFramework.HasPermissionCanRead(userID, "AVTPurchasingPriceMng,EurofarPurchasingPriceMng", out notification);
                    //if (hasPermissionCanRead.HasValue && hasPermissionCanRead.Value)
                    //{
                    //    data.IsTeamPricing = hasPermissionCanRead;

                    //    var dbItem = context.UserDashboardMng_function_OfferPricing(Library.Helper.GetCurrentSeason());
                    //    data.OfferPricings = converter.DB2DTO_OfferPricing(dbItem.ToList());

                    //    foreach (var item in data.OfferPricings)
                    //    {
                    //        List<UserDashboardMng_function_OfferSeasonDetail_Result> offerLines = context.UserDashboardMng_function_OfferSeasonDetail(item.OfferID).ToList();
                    //        decimal exRate = 0;
                    //        UserDashboardMng_function_getExchangeRate_Result exchangeRate = exchangeRates.Where(s => s.Season == item.Season).FirstOrDefault();
                    //        if (exchangeRate != null)
                    //            exRate = exchangeRate.ExRate.Value;

                    //        UserDashboardMng_function_ClientEstimatedAdditionalCost_Result clientEstimatedAdditionalCost = context.UserDashboardMng_function_ClientEstimatedAdditionalCost(item.OfferID).FirstOrDefault();
                    //        UserDashboardMng_function_PaymentTerm_Result paymentTerm = context.UserDashboardMng_function_PaymentTerm(item.OfferID).FirstOrDefault();


                    //        decimal sumSalePriceInUSD = 0;
                    //        decimal sumPurchasingPriceInUSD = 0;
                    //        decimal sumCommissionInUSD = 0;
                    //        decimal sumLcCostInUSD = 0;
                    //        decimal sumInterestInUSD = 0;
                    //        decimal sumDamagesCostInUSD = 0;
                    //        decimal sumTransportationInUSD = 0;

                    //        foreach (var offerLine in offerLines)
                    //        {
                    //            UserDashboardMng_function_Shared_OfferSeasonDetailAbility_Result shared_OfferLineLoadAbility = context.UserDashboardMng_function_Shared_OfferSeasonDetailAbility(offerLine.OfferSeasonDetailID).FirstOrDefault();

                    //            // tính lại delta percent
                    //            decimal salePriceInUSD = 0;
                    //            decimal purchasingPriceInUSD = 0;
                    //            decimal commissionInUSD = 0;
                    //            decimal lcCostInUSD = 0;
                    //            decimal interestInUSD = 0;
                    //            decimal damagesCostInUSD = 0;
                    //            decimal transportationInUSD = 0;

                    //            decimal unitPrice = 0;
                    //            unitPrice = offerLine.SalePrice.HasValue ? offerLine.SalePrice.Value : 0;

                    //            decimal planingPurchasingPrice = 0;
                    //            planingPurchasingPrice = offerLine.PlaningPurchasingPrice.HasValue ? offerLine.PlaningPurchasingPrice.Value : 0;

                    //            decimal commissionPercent = 0;
                    //            commissionPercent = offerLine.CommissionPercent.HasValue ? offerLine.CommissionPercent.Value : 0;

                    //            decimal downValue = 0;
                    //            downValue = paymentTerm.DownValue.HasValue ? paymentTerm.DownValue.Value : 0;

                    //            decimal transportationFee = 0;
                    //            transportationFee = offerLine.TransportationFee.HasValue ? offerLine.TransportationFee.Value : 0;

                    //            //salePriceInUSD
                    //            if (item.Currency == "EUR")
                    //                salePriceInUSD = unitPrice * exRate;
                    //            else
                    //                salePriceInUSD = unitPrice;

                    //            //purchasingPriceInUSD
                    //            purchasingPriceInUSD = planingPurchasingPrice;

                    //            //commissionInUSD
                    //            if (item.Currency == "EUR")
                    //                commissionInUSD = unitPrice * exRate * commissionPercent / 100;
                    //            else
                    //                commissionInUSD = unitPrice * commissionPercent / 100;

                    //            //lcCostInUSD
                    //            if (clientEstimatedAdditionalCost.LCCostPercent.HasValue)
                    //            {
                    //                if (item.Currency == "EUR")
                    //                    lcCostInUSD = unitPrice * clientEstimatedAdditionalCost.LCCostPercent.Value / 100 * exRate;
                    //                else
                    //                    lcCostInUSD = unitPrice * clientEstimatedAdditionalCost.LCCostPercent.Value / 100;
                    //            }
                    //            else if (paymentTerm.PaymentTypeID == 4)
                    //            {
                    //                if (item.Currency == "EUR")
                    //                    lcCostInUSD = unitPrice / 100 * exRate;
                    //                else
                    //                    lcCostInUSD = unitPrice / 100;
                    //            }

                    //            //interestInUSD
                    //            if (clientEstimatedAdditionalCost.InterestCostPercent.HasValue)
                    //            {
                    //                interestInUSD = unitPrice * clientEstimatedAdditionalCost.InterestCostPercent.Value / 100 * (item.Currency == "EUR" ? exRate : 1);
                    //            }
                    //            else if (paymentTerm.InDays.HasValue)
                    //            {
                    //                interestInUSD = (unitPrice * paymentTerm.InDays.Value * (item.Currency == "EUR" ? exRate : 1) * (paymentTerm.PaymentMethod == "DP-PERCENT" ? (1 - downValue / 100) : 1) * (decimal)(4.5 / 100 / 360));
                    //            }

                    //            //damagesCostInUSD
                    //            damagesCostInUSD = (offerLine.PlaningPurchasingPrice.HasValue ? offerLine.PlaningPurchasingPrice.Value : (decimal)0.001) * (decimal)1.25 / 100;

                    //            //transportationInUSD
                    //            if (item.Currency == "EUR" && shared_OfferLineLoadAbility.Qnt40HC.HasValue && shared_OfferLineLoadAbility.Qnt40HC > 0)
                    //                transportationInUSD = transportationFee / shared_OfferLineLoadAbility.Qnt40HC.Value * exRate;

                    //            sumSalePriceInUSD += salePriceInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumPurchasingPriceInUSD += purchasingPriceInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumCommissionInUSD += commissionInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumLcCostInUSD += lcCostInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumInterestInUSD += interestInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumDamagesCostInUSD += damagesCostInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //            sumTransportationInUSD += transportationInUSD * (offerLine.Quantity.HasValue ? offerLine.Quantity.Value : 1);
                    //        }

                    //        item.DeltaPercent = (sumSalePriceInUSD
                    //                        - sumPurchasingPriceInUSD
                    //                        - sumCommissionInUSD
                    //                        - sumLcCostInUSD
                    //                        - sumInterestInUSD
                    //                        - sumDamagesCostInUSD
                    //                        - sumTransportationInUSD)
                    //                        / (sumPurchasingPriceInUSD > 0 ? sumPurchasingPriceInUSD : 1) * 100;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        // Pending Price
        public DTO.DashboardPendingPriceDTO GetPendingPriceData(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DashboardPendingPriceDTO data = new DTO.DashboardPendingPriceDTO
            {
                FactoryDTOs = new List<DTO.FactoryDTO>()
            };
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    List<DTO.WaitForFactoryConclusionDTO> waitForFactoryConclusionDTOs = converter.DB2DTO_WaitForFactoryConclusionDTO(context.UserDashboardMng_function_GetWaitForFactoryConclusion(userID).ToList());
                    waitForFactoryConclusionDTOs.Select(o => new { o.Season, o.FactoryID, o.FactoryUD }).Distinct().ToList().ForEach(o => data.FactoryDTOs.Add(new DTO.FactoryDTO { Season = o.Season, FactoryID = o.FactoryID, FactoryUD = o.FactoryUD }));
                    foreach (DTO.FactoryDTO dtoFactory in data.FactoryDTOs)
                    {
                        if (waitForFactoryConclusionDTOs.FirstOrDefault(o => o.Season == dtoFactory.Season && o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 1) != null)
                            dtoFactory.LessThan1Week = waitForFactoryConclusionDTOs.FirstOrDefault(o => o.Season == dtoFactory.Season && o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 1).TotalItem;

                        if (waitForFactoryConclusionDTOs.FirstOrDefault(o => o.Season == dtoFactory.Season && o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 2) != null)
                            dtoFactory.From1To2Week = waitForFactoryConclusionDTOs.FirstOrDefault(o => o.Season == dtoFactory.Season && o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 2).TotalItem;

                        if (waitForFactoryConclusionDTOs.FirstOrDefault(o => o.Season == dtoFactory.Season && o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 3) != null)
                            dtoFactory.From2To3Week = waitForFactoryConclusionDTOs.FirstOrDefault(o => o.Season == dtoFactory.Season && o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 3).TotalItem;

                        if (waitForFactoryConclusionDTOs.FirstOrDefault(o => o.Season == dtoFactory.Season && o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 4) != null)
                            dtoFactory.Over3Week = waitForFactoryConclusionDTOs.FirstOrDefault(o => o.Season == dtoFactory.Season && o.FactoryID == dtoFactory.FactoryID && o.TotalWaitedWeeks == 4).TotalItem;
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

        // Production Oveview detail
        public List<DTO.DashboardProductionOverviewDetail> GetDataForProductionOverviewDetail(int factoryOrderID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.DashboardProductionOverviewDetail> data = new List<DTO.DashboardProductionOverviewDetail>();
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    var result = context.DashboardMng_function_getProductionOverviewDetail(factoryOrderID).ToList();
                    data = converter.DB2DTO_ProductionOverviewDetail(result);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        // Weekly Production Data
        public DTO.DashboardProductionRptData GetWeeklyProductionData(int userID, string season, int factoryId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.DashboardProductionRptData data = new DTO.DashboardProductionRptData();
            data.WeeklyProducedData = new List<DTO.DashboardWeeklyProduced>();
            data.WeeklyShippedData = new List<DTO.DashboardWeeklyShipped>();
            data.WeekInfoData = new List<DTO.WeekInfo>();
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    // Get week info
                    data.WeekInfoData = converter.DB2DTO_WeekInfo(context.DashboardMng_WeekInfoData_View.Where(o => o.Season == season).ToList());

                    // Get weekly produced
                    var WeeklyProducedData = context.DashboardMng_function_GetWeeklyProducedReportData(season, factoryId).ToList();
                    data.WeeklyProducedData = converter.DB2DTO_WeeklyProducedData(WeeklyProducedData);

                    // Get weekly shipped
                    var WeeklyShippedData = context.DashboardMng_function_GetWeeklyShippedReportData(season, factoryId).ToList();
                    data.WeeklyShippedData = converter.DB2DTO_WeeklyShippedData(WeeklyShippedData);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }

        public DTO.DashboardFinanceData GetFinanceOverviewByFactory(int userID, string season, int? factoryID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.DashboardFinanceData data = new DTO.DashboardFinanceData();
            data.FinanceGridview = new List<DTO.DashboardFinanceOverviewData>();
            data.FinanceChart = new List<DTO.DashboardFinanceChart>();
            data.IsFactoryManufacturing = true;

            try
            {
                using (var context = CreateContext())
                {
                    var rsOutput = context.DashboardMng_function_CheckFactoriesManufacturing(userID).FirstOrDefault().Value;
                    int cnvOutput = Convert.ToInt32(rsOutput.ToString());

                    data.IsFactoryManufacturing = (cnvOutput == 1);

                    if (cnvOutput == 1)
                    {
                        data.FinanceGridview = converter.DB2DTO_FinanceOverview(context.DashboardMng_function_FinanceOverview(season, userID, factoryID).ToList());
                        data.FinanceChart = converter.DB2DTO_FinanceOverviewChart(context.Dashboard_function_GetFinanceOverviewChart(season, userID, factoryID).ToList());
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

        // Offer not approved yet
        public DTO.OfferStatusDTO GetAdminDashboardOfferNotApprovedYetDTOs(int userId, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.OfferStatusDTO Data = new DTO.OfferStatusDTO();
            Data.Data = new List<DTO.AdminDashboardOfferSeasonNotApprovedYetDTO>();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    var dbResult = context.WidgetMng_function_GetOfferNeedAttention(season, userId).ToList();
                    Data.Data = converter.DB2DTO_AdminDashboardOfferSeasonNotApprovedYet(dbResult);

                    decimal? margin = 0;
                    decimal? purchasing = 0;

                    // approved offer
                    margin = dbResult.Where(o => o.IsNeedToBeApproved.HasValue && !o.IsNeedToBeApproved.Value && o.MarginAfter.HasValue).Sum(o => o.MarginAfter.Value);
                    purchasing = dbResult.Where(o => o.IsNeedToBeApproved.HasValue && !o.IsNeedToBeApproved.Value && o.PurchasingAmountUSD.HasValue).Sum(o => o.PurchasingAmountUSD.Value);
                    if (purchasing.HasValue && purchasing.Value > 0 && margin.HasValue)
                    {
                        Data.DeltaApproved = margin * 100 / purchasing;
                    }

                    margin = dbResult.Where(o => o.IsNeedToBeApproved.HasValue && o.IsNeedToBeApproved.Value && o.MarginAfter.HasValue).Sum(o => o.MarginAfter.Value);
                    purchasing = dbResult.Where(o => o.IsNeedToBeApproved.HasValue && o.IsNeedToBeApproved.Value && o.PurchasingAmountUSD.HasValue).Sum(o => o.PurchasingAmountUSD.Value);
                    if (purchasing.HasValue && purchasing.Value > 0 && margin.HasValue)
                    {
                        Data.DeltaNotApproved = margin * 100 / purchasing;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return Data;
        }


        public List<DTO.OfferAndPIDeltaComparisonDTO> GetOfferAndPIDeltaComparison(int userID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.OfferAndPIDeltaComparisonDTO> data = new List<DTO.OfferAndPIDeltaComparisonDTO>();
            string season = "2019/2020";
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_OfferAndPIDeltaComparison(context.DashboardMng_function_GetDeltaCompareOfferWithPILastYear(season, userID).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.DeltaByClientCompare> GetDeltaByClientCompare(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.DeltaByClientCompare> data = new List<DTO.DeltaByClientCompare>();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    var delta = context.DashboardMng_function_GetDeltaByClient(season, userID).ToList();
                    data = AutoMapper.Mapper.Map<List<DashboardMng_DeltaByClientCompare_View>,List<DTO.DeltaByClientCompare>>(delta);
                    data = data.OrderByDescending(o => o.DeltaOfDelta).ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public List<DTO.PendingOfferItemPrice> GetPendingOfferItemPrice(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.PendingOfferItemPrice> data = new List<DTO.PendingOfferItemPrice>();
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    var delta = context.DashboardMng_function_GetPendingOfferItemPrice(season, userID).ToList();
                    data = AutoMapper.Mapper.Map<List<DashboardMng_PendingOfferItemPrice_View>, List<DTO.PendingOfferItemPrice>>(delta);
                    data = data.OrderByDescending(o => o.QuotationDetailID).ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }

        public DTO.ItemDeltaNeedAttentionSearchForm GetItemDeltaNeedAttention(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ItemDeltaNeedAttentionSearchForm data = new DTO.ItemDeltaNeedAttentionSearchForm();
            data.Data = new List<DTO.ItemDeltaNeedAttentionDTO>();
            data.TotalRows = 0;

            string Season = null;
            string Description = null;
            string ClientNM = null;
            string OfferUD = null;
            string ProformaInvoiceNo = null; 
            int? PIStatus = null;
            int? OrderQnt = null;
            int? OrderQnt40HC = null;
            int? Delta5Percent = null;
            if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
            {
                ClientNM = filters["ClientNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("OfferUD") && !string.IsNullOrEmpty(filters["OfferUD"].ToString()))
            {
                OfferUD = filters["OfferUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("PIStatus") && filters["PIStatus"] != null && !string.IsNullOrEmpty(filters["PIStatus"].ToString()))
            {
                PIStatus = Library.Helper.ConvertStringToInt(filters["PIStatus"].ToString());
            }
            if (filters.ContainsKey("OrderQnt") && filters["OrderQnt"] != null && !string.IsNullOrEmpty(filters["OrderQnt"].ToString()))
            {
                OrderQnt = Library.Helper.ConvertStringToInt(filters["OrderQnt"].ToString());
            }
            if (filters.ContainsKey("OrderQnt40HC") && filters["OrderQnt40HC"] != null && !string.IsNullOrEmpty(filters["OrderQnt40HC"].ToString()))
            {
                OrderQnt40HC = Library.Helper.ConvertStringToInt(filters["OrderQnt40HC"].ToString());
            }
            if (filters.ContainsKey("Delta5Percent") && filters["Delta5Percent"] != null && !string.IsNullOrEmpty(filters["Delta5Percent"].ToString()))
            {
                Delta5Percent = Library.Helper.ConvertStringToInt(filters["Delta5Percent"].ToString());
            }

            //try to get data
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    data.TotalRows = context.WidgetMng_function_GetItemDeltaNeedAttention(Season, Description, ClientNM, OfferUD, ProformaInvoiceNo, PIStatus, OrderQnt, OrderQnt40HC, Delta5Percent, orderBy, orderDirection).Count();
                    var result = context.WidgetMng_function_GetItemDeltaNeedAttention(Season, Description, ClientNM, OfferUD, ProformaInvoiceNo, PIStatus, OrderQnt, OrderQnt40HC, Delta5Percent, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ItemDeltaNeedAttentionDTO(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return data;
        }


        //
        //  move from FactoryPerformance
        //
        public DTO.HtmlReportData GetHTMLReportData(int userID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.HtmlReportData data = new DTO.HtmlReportData();
            data.AnnualShippedData = new List<DTO.AnnualShipped>();
            data.WeeklyShippedData = new List<DTO.WeeklyShipped>();
            data.WeekInfoData = new List<DTO.WeekInfor>();
            data.FactoryInfoData = new List<DTO.FactoryInfo>();
            data.TotalCapacityAndOrderDatas = new List<DTO.TotalCapacityAndOrderData>();
            data.TotalCapacityAndOrderByWeekDatas = new List<DTO.TotalCapacityAndOrderByWeekData>();

            //try to get data
            try
            {
                using (DashboardMngEntities context = CreateContext())
                {
                    data.WeekInfoData = converter.DB2DTO_WeekInfor(context.DashboardMng_UserDashboard_WeekInfoReportData_View.Where(o => o.Season == season).ToList());
                    data.WeeklyShippedData = converter.DB2DTO_WeeklyShipped(context.DashBoardMng_function_UserDashBoard_GetWeeklyShippedReportData(season, userID).ToList());
                    data.AnnualShippedData = converter.DB2DTO_AnnualShipped(context.DashboardMng_function_UserDashboard_GetYearlyShippedReportData(season, userID).ToList());
                    data.FactoryInfoData = converter.DB2DTO_FactoryInfo(context.DashboardMng_function_UserDashboard_FactoryInfoReportData(season, userID).OrderBy(o => o.FactoryUD).ToList());
                    data.WeeklyProducedData = converter.DB2DTO_WeeklyProduced(context.DashboardMng_function_UserDashboard_GetWeeklyProducedReportData(season, userID).ToList());
                    data.AnnualProducedData = converter.DB2DTO_AnnualProduced(context.DashboardMng_function_UserDashboard_GetYearlyProducedReportData(season, userID).ToList());
                    data.TotalCapacityAndOrderDatas = converter.DB2DTO_TotalCapacityAndOrder(context.DashboardMng_function_UserDashboard_TotalCapacityAndOrder(season, userID).ToList());
                    data.TotalCapacityAndOrderByWeekDatas = converter.DB2DTO_TotalCapacityAndOrderByWeek(context.DashboardMng_function_UserDashboard_TotalCapacityAndOrderByWeekOfFactory(season, userID).ToList());
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

