using System.Collections.Generic;

namespace Module.DashboardMng.DTO
{
    public class DashboardProductionData
    {
        public bool IsModuleQuotation { get; set; } // Can perform action Quotation
        public bool IsModulePurchasingInvoice { get; set; } // Can perform action Purchasing Invoice
        public bool IsModuleSampleProduction { get; set; } // Can perform action Sample Production
        public bool IsModuleLabelingPackaging { get; set; } // Can perform action Labeling Packaging

        public bool IsCompany { get; set; } // Check user depend on company or factory
        public bool IsFactoryManufacturing { get; set; }
        public bool IsTeamAccountManager { get; set; }

        public List<DTO.DashboardQuotationData> QuotationData { get; set; }
        public List<DTO.DashboardSampleOrderData> SampleProductionData { get; set; }
        public List<DTO.DashboardPurchasingInvoiceData> PurchasingInvoiceData { get; set; }
        public List<DTO.DashboardLabelingPackagingData> LabelingPackagingData { get; set; }
        public List<DTO.DashboardTotalHitPerWeekOfUserData> TotalHitPerWeekOfUserData { get; set; }
        public List<DTO.DashboardTotalHitPerWeekOfFactoryData> TotalHitPerWeekOfFactoryData { get; set; }
        public List<DTO.DashboardProductionOverview> ProductionOverview { get; set; }
        public List<DTO.DashboardFactoryAccessList> FactoryAccessList { get; set; }

        public List<DTO.DashboardFinanceOverviewData> FinanceOverviews { get; set; }
        public List<DTO.DashboardFinanceChart> FinanceChart { get; set; }

        public List<Support.DTO.Season> Seasons { get; set; }
        public List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO> OfferApproveNotYets { get; set; }
        public List<DTO.OfferApprovedAccountManagerDTO> OfferApprovedAccountManagers { get; set; }

        /// Offer pricing
        /// Permission team pricing
        public bool? IsTeamPricing { get; set; }
        public List<OfferPricingDTO> OfferPricings { get; set; }
    }
}
