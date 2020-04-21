using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class PlaningPurchasingPriceDTO
    {
        public int OfferLineID { get; set; }
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public int PlaningPurchasingPriceSourceID { get; set; }
        public int? QuotationDetailID { get; set; }
    }
}
