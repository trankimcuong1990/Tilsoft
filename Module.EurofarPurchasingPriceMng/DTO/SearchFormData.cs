using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class SearchFormData
    {
        public List<FactoryQuotationSearchResultDTO> Data { get; set; }

        public DTO.SubTotalDTO SubTotalRow { get; set; }
        public DTO.SubTotalDTO TotalRow { get; set; }

        public int TotalItem { get; set; }
        public int TotalConfirmedItem { get; set; }
        public int TotalPendingItem
        {
            get
            {
                return TotalItem - TotalConfirmedItem;
            }
        }
        public int TotalWaitForEurofar { get; set; }
        public int TotalEstimated { get; set; }
        public int TotalWaitForFactory
        {
            get
            {
                return TotalPendingItem - TotalWaitForEurofar;
            }
        }

        public decimal TotalContainer { get; set; }
        public decimal TotalConfirmedContainer { get; set; }
        public decimal TotalPendingContainer
        {
            get
            {
                return TotalContainer - TotalConfirmedContainer;
            }
        }
        public decimal TotalContainerWaitForEurofar { get; set; }
        public decimal TotalContainerEstimated { get; set; }
        public decimal TotalContainerWaitForFactory
        {
            get
            {
                return TotalPendingContainer - TotalContainerWaitForEurofar;
            }
        }

        public decimal CurrentExchangeRate { get; set; }



        public List<DTO.EurofarBreakdownCategoryOptionDTO> eurofarBreakdownCategoryOptionDTOs { get; set; }
        public List<DTO.EurofarSeasonSpecPercentDTO> eurofarSeasonSpecPercentDTOs { get; set; }
        public List<DTO.EurofarBreakdownManagementFeeDTO> eurofarBreakdownManagementFeeDTOs { get; set; }

        public List<DTO.FactoryDTO> WaitForFactoryConclusionDTOs { get; set; }
        public List<DTO.FactoryDTO> WaitForFactoryProductionConclusionDTOs { get; set; }
        //public List<DTO.WaitForFactoryConclusionDTO> WaitForFactoryConclusionDTOs { get; set; }
        public List<DTO.EmailAddressReceivePriceRequestDTO> EmailAddressReceivePriceRequestDTO { get; set; }
    }
}
