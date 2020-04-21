using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceQuotationMng.DTO
{
    public class QuotationDetailData
    {
        public int QuotationDetailID { get; set; }
        public int? QuotationID { get; set; }
        public string PriceDifferenceID { get; set; }
        public string PriceDifferenceUD { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Price { get; set; }
        public int? QuotationOfferDirectionID { get; set; }
        public string Remark { get; set; }
        public int? IsFactory { get; set; }
        public int? ModelID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public string Season { get; set; }
        public decimal? OldPrice1 { get; set; }
        public decimal? OldPrice2 { get; set; }
        public decimal? OldPrice3 { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
    }
}
