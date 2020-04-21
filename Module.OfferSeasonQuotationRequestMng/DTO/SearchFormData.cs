using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonQuotationRequestMng.DTO
{
    public class SearchFormData
    {
        public List<DTO.OfferSeasonQuotationRequestSearchResultDTO> Data { get; set; }
        public List<DTO.FactoryQuotationSearchResultDTO> FactoryQuotationSearchResultDTOs { get; set; }
        public List<DTO.PreferedFactoryDTO> PreferedFactoryDTOs { get; set; }
    }
}
