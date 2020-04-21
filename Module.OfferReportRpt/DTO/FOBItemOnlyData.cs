using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferReportRpt.DTO
{
    public class FOBItemOnlyData
    {
        public int? UserID { get; set; }
        public List<FOBItemOnlyDTO> fOBItemOnlyDTOs { get; set; }
        public List<Support.DTO.Season> seasons { get; set; }

    }
    public class FOBItemOnlyDTO
    {
        public int OfferLineID { get; set; }
        public string OfferUD { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> PlaningPurchasingPrice { get; set; }
        public Nullable<int> PlaningPurchasingPriceSourceID { get; set; }
        public string PlaningPurchasingPriceSourceNM { get; set; }
        public string FactoryUD { get; set; }
        public int OfferID { get; set; }
        public string Season { get; set; }
    }
}
