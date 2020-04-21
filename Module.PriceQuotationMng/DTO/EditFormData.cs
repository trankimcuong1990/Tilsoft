namespace Module.PriceQuotationMng.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EditFormData
    {
        public EditOfferQuotationData Data { get; set; }
        public List<HistoryPriceQuotationData> HistoryQuotationPrices { get; set; }
        public List<Support.DTO.PriceDifference> PriceDifferences { get; set; }
    }
}
