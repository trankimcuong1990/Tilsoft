namespace Module.PriceQuotationMng.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HistoryPriceQuotationData
    {
        public int PrimaryKey { get; set; }
        public string QuotationOfferDirectionNM { get; set; }
        public string QuotationOfferDate { get; set; }
        public decimal? Price { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? QuotationID { get; set; }
        public int? QuotationDetailID { get; set; }
        public string Remark { get; set; }
    }
}
