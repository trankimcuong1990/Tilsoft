namespace Module.PriceQuotationMng.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EditOfferQuotationData
    {
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string OfferDate { get; set; }
        public decimal? OldPrice1 { get; set; }
        public decimal? OldPrice2 { get; set; }
        public decimal? OldPrice3 { get; set; }
        public int? PriceDifferenceID { get; set; }
        public string PriceDifferenceCode { get; set; }
        public decimal? PriceDifferenceRate { get; set; }
        public int IsSetPrice { get; set; }
        public int? QuotationID { get; set; }
        public int? QuotationDetailID { get; set; }
        public string Remark { get; set; }
        public decimal? SalePrice { get; set; }
        public string OldPriceRemark { get; set; }
    }
}
