namespace Module.PriceQuotationMng.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class QuotationOfferDetailData
    {
        public int QuotationOfferDetailID { get; set; }
        public int? QuotationOfferID { get; set; }
        public int? QuotationDetailID { get; set; }
        public decimal? Price { get; set; }
        public string Remark { get; set; }
    }
}
