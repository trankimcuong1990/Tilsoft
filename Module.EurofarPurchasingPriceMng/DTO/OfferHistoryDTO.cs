using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class OfferHistoryDTO
    {
        public string QuotationOfferDirectionNM { get; set; }
        public decimal? Price { get; set; }
        public string Remark { get; set; }
        public string UpdatorNM { get; set; }
        public string UpdatedDate { get; set; }
        public int UserID { get; set; }
        public int QuotationDetailID { get; set; }
    }
}
