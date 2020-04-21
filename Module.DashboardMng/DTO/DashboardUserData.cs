using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardUserData
    {
        public bool IsModuleQuotation { get; set; } // Can perform action Quotation
        public bool IsModulePurchasingInvoice { get; set; } // Can perform action Purchasing Invoice
        public bool IsModuleSampleProduction { get; set; } // Can perform action Sample Production
        public bool IsModuleLabelingPackaging { get; set; } // Can perform action Labeling Packaging

        public bool IsCompany { get; set; } // Check user depend on company or factory

        public List<DTO.DashboardQuotationData> QuotationData { get; set; }
        public List<DTO.DashboardSampleOrderData> SampleProductionData { get; set; }
        public List<DTO.DashboardPurchasingInvoiceData> PurchasingInvoiceData { get; set; }
        public List<DTO.DashboardLabelingPackagingData> LabelingPackagingData { get; set; }
        public List<DTO.DashboardTotalHitPerWeekOfUserData> TotalHitPerWeekOfUserData { get; set; }
        public List<DTO.DashboardTotalHitPerWeekOfFactoryData> TotalHitPerWeekOfFactoryData { get; set; }  
        public List<DTO.UserDashboardOfferSeasonNotApprovedYetDTO> OfferApproveNotYets { get; set; }
    }
}
