using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library;
using System.Globalization;

namespace Module.DashboardMng.DAL
{
    public class DataConverter
    {
        public DataConverter()
        {
            string mapName = "DashboardMng";

            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<DashboardMng_UserDataRpt_View, DTO.UserDataRpt>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_HitCountDataRpt_View, DTO.HitCountDataRpt>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashBoardMng_Quotation_View, DTO.DashboardQuotationData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LastUpdatedDate, o => o.ResolveUsing(s => s.LastUpdatedDate.HasValue ? s.LastUpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashBoardMng_SampleOrder_View, DTO.DashboardSampleOrderData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.Deadline, o => o.ResolveUsing(s => s.Deadline.HasValue ? s.Deadline.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.SampleProductData, o => o.MapFrom(s => s.DashBoardMng_SampleProduct_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_PurchasingInvoice_View, DTO.DashboardPurchasingInvoiceData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_LabelingPackaging_View, DTO.DashboardLabelingPackagingData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_TotalHitPerWeekOfUser_View, DTO.DashboardTotalHitPerWeekOfUserData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_TotalHitPerWeekFactory_View, DTO.DashboardTotalHitPerWeekOfFactoryData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashBoardMng_SampleProduct_View, DTO.DashboardSampleProductData>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_ProductionOverview_View, DTO.DashboardProductionOverview>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS1, o => o.ResolveUsing(s => s.LDS1.HasValue ? s.LDS1.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.LDS2, o => o.ResolveUsing(s => s.LDS2.HasValue ? s.LDS2.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.OrderDate, o => o.ResolveUsing(s => s.OrderDate.HasValue ? s.OrderDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_FactoryAccessList_View, DTO.DashboardFactoryAccessList>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_ProductionOverviewDetail_View, DTO.DashboardProductionOverviewDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_WeeklyProducedReportData_View, DTO.DashboardWeeklyProduced>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_WeeklyShippedReportData_View, DTO.DashboardWeeklyShipped>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_WeekInfoData_View, DTO.WeekInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_FinanceOverview_View, DTO.DashboardFinanceOverviewData>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.InvoiceDate, o => o.ResolveUsing(s => s.InvoiceDate.HasValue ? s.InvoiceDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<Dashboard_ChartFinanceOverview_View, DTO.DashboardFinanceChart>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UserDashboardMng_function_GetWaitForFactoryConclusion_Result, DTO.WaitForFactoryConclusionDTO>()
                   .IgnoreAllNonExisting()
                   .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AdminDashboardMng_OfferSeasonNotApprovedYet_View, DTO.AdminDashboardOfferSeasonNotApprovedYetDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    //.ForMember(d => d.OfferApprovedDate, o => o.ResolveUsing(s => s.OfferApprovedDate.HasValue ? s.OfferApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WidgetMng_function_GetOfferNeedAttention_Result, DTO.AdminDashboardOfferSeasonNotApprovedYetDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.SetItemStatusDate.HasValue ? s.SetItemStatusDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.TotalOfferItem, o => o.MapFrom(s => s.TotalValidItem))
                    .ForMember(d => d.TotalReadyToApproved, o => o.MapFrom(s => s.TotalValidItem - s.TotalApprovedItem))
                    .ForMember(d => d.TotalApproved, o => o.MapFrom(s => s.TotalApprovedItem))
                    .ForMember(d => d.IsOfferNotApproved, o => o.ResolveUsing(s => s.IsNeedToBeApproved.HasValue && s.IsNeedToBeApproved.Value ? 1 : 0))
                    .ForMember(d => d.IsOfferApproved, o => o.ResolveUsing(s => s.IsNeedToBeApproved.HasValue && s.IsNeedToBeApproved.Value ? 0 : 1))
                    .ForMember(d => d.DeltaPercent, o => o.MapFrom(s => s.Delta7Percent))
                    .ForMember(d => d.SaleAmount, o => o.MapFrom(s => s.SaleAmountConvertedToUSD))
                    .ForMember(d => d.ApprovedUser, o => o.MapFrom(s => s.ApproverNM))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UserDashboardMng_OfferSeasonNotApprovedYet_View, DTO.UserDashboardOfferSeasonNotApprovedYetDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<WidgetMng_function_GetOfferNeedAttention_Result, DTO.UserDashboardOfferSeasonNotApprovedYetDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.SetItemStatusDate.HasValue ? s.SetItemStatusDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.TotalOfferItem, o => o.MapFrom(s => s.TotalValidItem))
                    .ForMember(d => d.TotalReadyToApproved, o => o.MapFrom(s => s.TotalValidItem - s.TotalApprovedItem))
                    .ForMember(d => d.TotalApproved, o => o.MapFrom(s => s.TotalApprovedItem))
                    .ForMember(d => d.IsOfferNotApproved, o => o.ResolveUsing(s => s.IsNeedToBeApproved.HasValue && s.IsNeedToBeApproved.Value ? 1 : 0))
                    .ForMember(d => d.IsOfferApproved, o => o.ResolveUsing(s => s.IsNeedToBeApproved.HasValue && s.IsNeedToBeApproved.Value ? 0 : 1))
                    .ForMember(d => d.DeltaPercent, o => o.MapFrom(s => s.Delta7Percent))
                    .ForMember(d => d.SaleAmount, o => o.MapFrom(s => s.SaleAmountConvertedToUSD))
                    .ForMember(d => d.ApprovedUser, o => o.MapFrom(s => s.ApproverNM))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<UserDashboardMng_OfferSeasonNotApprovedYet_View, DTO.OfferApprovedAccountManagerDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                /// Create mapping offer of team pricing
                /// For user depend on AVT price and Eurofar price(permission can read)
                AutoMapper.Mapper.CreateMap<UserDashboardMng_OfferPricing_View, DTO.OfferPricingDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_function_GetDeltaCompareOfferWithPILastYear_Result, DTO.OfferAndPIDeltaComparisonDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_DeltaByClientCompare_View, DTO.DeltaByClientCompare>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_PendingOfferItemPrice_View, DTO.PendingOfferItemPrice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                // widget item delta need attention
                AutoMapper.Mapper.CreateMap<WidgetMng_function_GetItemDeltaNeedAttention_Result, DTO.ItemDeltaNeedAttentionDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString("dd/MM/yyyy") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<AdminDashboardMng_function_ChartDelta_Result, DTO.DashboardDetalDTO>()
                    .IgnoreAllNonExisting()                  
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                #region move from FactoryPerformance
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<DashboardMng_UserDashboard_WeekInfoReportData_View, DTO.WeekInfor>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_UserDashboard_YearlyShippedReportData_View, DTO.AnnualShipped>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_UserDashboard_WeeklyShippedReportData_View, DTO.WeeklyShipped>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_UserDashboard_FactoryInfoReportData_View, DTO.FactoryInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_UserDashboard_YearlyProducedReportData_View, DTO.AnnualProduced>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_UserDashboard_WeeklyProducedReportData_View, DTO.WeeklyProduced>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //Factory Capacity
                AutoMapper.Mapper.CreateMap<DashboardMng_UserDashboard_TotalCapacityAndOrder_View, DTO.TotalCapacityAndOrderData>()
                   .IgnoreAllNonExisting();
                //.ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DashboardMng_UserDashboard_TotalCapacityAndOrderByWeekOfFactory_View, DTO.TotalCapacityAndOrderByWeekData>()
                    .ForMember(d => d.WeekStart, o => o.ResolveUsing(s => s.WeekStart.HasValue ? s.WeekStart.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.WeekEnd, o => o.ResolveUsing(s => s.WeekEnd.HasValue ? s.WeekEnd.Value.ToString("dd/MM/yyyy") : ""))
                    .IgnoreAllNonExisting();
                //    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                #endregion

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }
        
        public List<DTO.DashboardDetalDTO> DB2DTO_DashboardDetalDTO(List<AdminDashboardMng_function_ChartDelta_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AdminDashboardMng_function_ChartDelta_Result>, List<DTO.DashboardDetalDTO>>(dbItems);
        }
        public List<DTO.ItemDeltaNeedAttentionDTO> DB2DTO_ItemDeltaNeedAttentionDTO(List<WidgetMng_function_GetItemDeltaNeedAttention_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WidgetMng_function_GetItemDeltaNeedAttention_Result>, List<DTO.ItemDeltaNeedAttentionDTO>>(dbItems);
        }

        public List<DTO.WaitForFactoryConclusionDTO> DB2DTO_WaitForFactoryConclusionDTO(List<UserDashboardMng_function_GetWaitForFactoryConclusion_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UserDashboardMng_function_GetWaitForFactoryConclusion_Result>, List<DTO.WaitForFactoryConclusionDTO>>(dbItems);
        }

        public List<DTO.OfferAndPIDeltaComparisonDTO> DB2DTO_OfferAndPIDeltaComparison(List<DashboardMng_function_GetDeltaCompareOfferWithPILastYear_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_function_GetDeltaCompareOfferWithPILastYear_Result>, List<DTO.OfferAndPIDeltaComparisonDTO>>(dbItems);
        }

        public List<DTO.UserDataRpt> DB2DTO_UserDataRpt(List<DashboardMng_UserDataRpt_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDataRpt_View>, List<DTO.UserDataRpt>>(dbItems);
        }

        public List<DTO.HitCountDataRpt> DB2DTO_HitCountDataRpt(List<DashboardMng_HitCountDataRpt_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_HitCountDataRpt_View>, List<DTO.HitCountDataRpt>>(dbItems);
        }

        public List<DTO.DashboardQuotationData> DB2DTO_Quotation(List<DashBoardMng_Quotation_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashBoardMng_Quotation_View>, List<DTO.DashboardQuotationData>>(dbItems);
        }

        public List<DTO.DashboardSampleOrderData> DB2DTO_SampleProduction(List<DashBoardMng_SampleOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashBoardMng_SampleOrder_View>, List<DTO.DashboardSampleOrderData>>(dbItems);
        }

        public List<DTO.DashboardPurchasingInvoiceData> DB2DTO_PurchasingInvoice(List<DashboardMng_PurchasingInvoice_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_PurchasingInvoice_View>, List<DTO.DashboardPurchasingInvoiceData>>(dbItems);
        }

        public List<DTO.DashboardLabelingPackagingData> DB2DTO_LabelingPackaging(List<DashboardMng_LabelingPackaging_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_LabelingPackaging_View>, List<DTO.DashboardLabelingPackagingData>>(dbItems);
        }

        public List<DTO.DashboardTotalHitPerWeekOfUserData> DB2DTO_TotalHitPerWeekOfUser(List<DashboardMng_TotalHitPerWeekOfUser_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_TotalHitPerWeekOfUser_View>, List<DTO.DashboardTotalHitPerWeekOfUserData>>(dbItems);
        }

        public List<DTO.DashboardTotalHitPerWeekOfFactoryData> DB2DTO_TotalHitPerWeekOfFactory(List<DashboardMng_TotalHitPerWeekFactory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_TotalHitPerWeekFactory_View>, List<DTO.DashboardTotalHitPerWeekOfFactoryData>>(dbItems);
        }

        public List<DTO.DashboardProductionOverview> DB2DTO_ProductionOverview(List<DashboardMng_ProductionOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_ProductionOverview_View>, List<DTO.DashboardProductionOverview>>(dbItems);
        }

        public List<DTO.DashboardFactoryAccessList> DB2DTO_FactoryAccessList(List<DashboardMng_FactoryAccessList_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_FactoryAccessList_View>, List<DTO.DashboardFactoryAccessList>>(dbItems);
        }

        public List<DTO.DashboardProductionOverviewDetail> DB2DTO_ProductionOverviewDetail(List<DashboardMng_ProductionOverviewDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_ProductionOverviewDetail_View>, List<DTO.DashboardProductionOverviewDetail>>(dbItems);
        }

        public List<DTO.DashboardWeeklyProduced> DB2DTO_WeeklyProducedData(List<DashboardMng_WeeklyProducedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_WeeklyProducedReportData_View>, List<DTO.DashboardWeeklyProduced>>(dbItems);
        }

        public List<DTO.DashboardWeeklyShipped> DB2DTO_WeeklyShippedData(List<DashboardMng_WeeklyShippedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_WeeklyShippedReportData_View>, List<DTO.DashboardWeeklyShipped>>(dbItems);
        }

        public List<DTO.WeekInfo> DB2DTO_WeekInfo(List<DashboardMng_WeekInfoData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_WeekInfoData_View>, List<DTO.WeekInfo>>(dbItems);
        }

        public List<DTO.DashboardFinanceOverviewData> DB2DTO_FinanceOverview(List<DashboardMng_FinanceOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_FinanceOverview_View>, List<DTO.DashboardFinanceOverviewData>>(dbItems);
        }

        public List<DTO.DashboardFinanceChart> DB2DTO_FinanceOverviewChart(List<Dashboard_ChartFinanceOverview_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<Dashboard_ChartFinanceOverview_View>, List<DTO.DashboardFinanceChart>>(dbItems);
        }
        public List<DTO.AdminDashboardOfferSeasonNotApprovedYetDTO> DB2DTO_AdminDashboardOfferSeasonNotApprovedYetDTO(List<AdminDashboardMng_OfferSeasonNotApprovedYet_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<AdminDashboardMng_OfferSeasonNotApprovedYet_View>, List<DTO.AdminDashboardOfferSeasonNotApprovedYetDTO>>(dbItems);
        }

        public List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO> DB2DTO_UserDashboardOfferSeasonNotApprovedYetDTO(List<UserDashboardMng_OfferSeasonNotApprovedYet_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UserDashboardMng_OfferSeasonNotApprovedYet_View>, List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO>>(dbItems);
        }

        public List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO> DB2DTO_WidgetMng_function_GetOfferNeedAttention(List<WidgetMng_function_GetOfferNeedAttention_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WidgetMng_function_GetOfferNeedAttention_Result>, List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO>>(dbItems);
        }

        public List<DTO.AdminDashboardOfferSeasonNotApprovedYetDTO> DB2DTO_AdminDashboardOfferSeasonNotApprovedYet(List<WidgetMng_function_GetOfferNeedAttention_Result> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WidgetMng_function_GetOfferNeedAttention_Result>, List<DTO.AdminDashboardOfferSeasonNotApprovedYetDTO>>(dbItems);
        }

        public List<DTO.OfferApprovedAccountManagerDTO> DB2DTO_OfferSeasonApprovedAccountManagerDTO(List<UserDashboardMng_OfferSeasonNotApprovedYet_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<UserDashboardMng_OfferSeasonNotApprovedYet_View>, List<DTO.OfferApprovedAccountManagerDTO>>(dbItems);
        }

        /// Function converter database into data transfer object
        /// UserDashboardMng_OfferPricing_View -> DTO.OfferPricingDTO
        public List<DTO.OfferPricingDTO> DB2DTO_OfferPricing(List<UserDashboardMng_OfferPricing_View> dbItem)
        {
            return AutoMapper.Mapper.Map<List<UserDashboardMng_OfferPricing_View>, List<DTO.OfferPricingDTO>>(dbItem);
        }

        #region move from FactoryPerformance
        public List<DTO.WeekInfor> DB2DTO_WeekInfor(List<DashboardMng_UserDashboard_WeekInfoReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDashboard_WeekInfoReportData_View>, List<DTO.WeekInfor>>(dbItems);
        }

        public List<DTO.AnnualShipped> DB2DTO_AnnualShipped(List<DashboardMng_UserDashboard_YearlyShippedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDashboard_YearlyShippedReportData_View>, List<DTO.AnnualShipped>>(dbItems);
        }

        public List<DTO.WeeklyShipped> DB2DTO_WeeklyShipped(List<DashboardMng_UserDashboard_WeeklyShippedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDashboard_WeeklyShippedReportData_View>, List<DTO.WeeklyShipped>>(dbItems);
        }

        public List<DTO.FactoryInfo> DB2DTO_FactoryInfo(List<DashboardMng_UserDashboard_FactoryInfoReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDashboard_FactoryInfoReportData_View>, List<DTO.FactoryInfo>>(dbItems);
        }

        public List<DTO.AnnualProduced> DB2DTO_AnnualProduced(List<DashboardMng_UserDashboard_YearlyProducedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDashboard_YearlyProducedReportData_View>, List<DTO.AnnualProduced>>(dbItems);
        }

        public List<DTO.WeeklyProduced> DB2DTO_WeeklyProduced(List<DashboardMng_UserDashboard_WeeklyProducedReportData_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDashboard_WeeklyProducedReportData_View>, List<DTO.WeeklyProduced>>(dbItems);
        }

        //FactoryCapacity
        public List<DTO.TotalCapacityAndOrderData> DB2DTO_TotalCapacityAndOrder(List<DashboardMng_UserDashboard_TotalCapacityAndOrder_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDashboard_TotalCapacityAndOrder_View>, List<DTO.TotalCapacityAndOrderData>>(dbItems);
        }

        public List<DTO.TotalCapacityAndOrderByWeekData> DB2DTO_TotalCapacityAndOrderByWeek(List<DashboardMng_UserDashboard_TotalCapacityAndOrderByWeekOfFactory_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<DashboardMng_UserDashboard_TotalCapacityAndOrderByWeekOfFactory_View>, List<DTO.TotalCapacityAndOrderByWeekData>>(dbItems);
        }

        #endregion
    }
}
