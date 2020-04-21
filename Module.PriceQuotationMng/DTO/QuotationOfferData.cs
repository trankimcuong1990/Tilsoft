namespace Module.PriceQuotationMng.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class QuotationOfferData
    {
        public int QuotationOfferID { get; set; }
        public int? QuotationID { get; set; }
        public int? QuotationOfferVersion { get; set; }
        public int? QuotationOfferDirectionID { get; set; }
        public string QuotationOfferDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public List<QuotationOfferDetailData> QuotationOfferDetail { get; set; }
    }
}
