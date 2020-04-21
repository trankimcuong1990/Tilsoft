﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVTPurchasingPriceMng.DTO
{
    public class SearchFormData
    {
        public List<FactoryQuotationSearchResultDTO> Data { get; set; }
        public decimal Exchange { get; set; }
        public List<AVTBreakdownCategoryOptionDTO> avtBreakdownCategoryOptionDTOs { get; set; }
        public List<DTO.AVTSeasonSpecPercentDTO> avtSeasonSpecPercentDTOs { get; set; }
        public List<DTO.AVTBreakdownManagementFeeDTO> avtBreakdownManagementFeeDTOs { get; set; }

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
        public decimal TotalContainerWaitForFactory
        {
            get
            {
                return TotalPendingContainer - TotalContainerWaitForEurofar;
            }
        }
    }
}
