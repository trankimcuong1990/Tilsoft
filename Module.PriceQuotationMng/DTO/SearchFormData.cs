using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceQuotationMng.DTO
{
    public class SearchFormData
    {
        public List<PriceQuotationSearchResultData> Data { get; set; }
        //public List<Support.DTO.Season> Seasons { get; set; }
        //public List<Support.DTO.Client> Clients { get; set; }
        //public List<Support.DTO.Factory> Factories { get; set; }
        //public List<Support.DTO.QuotationStatus> QuotationStatuses { get; set; }
        //public List<PriceDifferenceData> PriceDifferences { get; set; }
        //public List<QuotationOfferDirectionData> QuotationOfferDirections { get; set; }
        //public List<PriceQuotationSearchResultData> AllData { get; set; }

        //public string CurrentSeason { get; set; }
        public int? IsCompany { get; set; }
    }
}
