using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryQuotationMng.DTO
{
    public class QuotationOfferDetail
    {
        public int QuotationOfferDetailID { get; set; }
        public int QuotationOfferVersion { get; set; }
        public string QuotationOfferDirectionNM { get; set; }
        public decimal? Price { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
