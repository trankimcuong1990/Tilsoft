using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotationMng.DTO
{
    public class QuotationDetail
    {
        public int QuotationDetailID { get; set; }
        public string ClientUD { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LDS { get; set; }
        public string QuotationStatusNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? OrderQnt { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public decimal? NewOfferPrice { get; set; }

        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }

        public List<QuotationOfferDetail> QuotationOfferDetails { get; set; }
    }
}
